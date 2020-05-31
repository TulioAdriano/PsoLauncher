/*
    This file is part of Yggdrasill
    Copyright (C) 2012, 2013 Lawrence Sebald

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License version 3 as
    published by  the Free Software Foundation.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Yggdrasill
{
    class ProcessHaxxor
    {
        #region Instance Variables

        private Process proc;
        private IntPtr handle;

        #endregion

        #region Constructor

        public ProcessHaxxor(Process p)
        {
            this.proc = p;
            this.handle = p.Handle;
        }

        #endregion

        #region Low-level Instance Methods

        public bool ReadProcessMemory(uint addr, uint sz, out byte[] buffer,
            out int bytesRead)
        {
            buffer = new byte[sz];

            return Kernel32.ReadProcessMemory(handle, addr, buffer, sz, out bytesRead);
        }

        public bool WriteProcessMemory(uint addr, uint sz, byte[] buffer,
            out int bytesWritten)
        {
            return Kernel32.WriteProcessMemory(handle, addr, buffer, sz,
                out bytesWritten);
        }

        #endregion

        #region Higher-level Instance Methods

        public bool ReadUInt32(uint addr, out UInt32 rv)
        {
            byte[] buf;
            int bytesRead;

            rv = 0;

            if (!ReadProcessMemory(addr, 4, out buf, out bytesRead))
                return false;
            if (bytesRead != 4)
                return false;

            rv = BitConverter.ToUInt32(buf, 0);
            return true;
        }

        public bool WriteUInt32(uint addr, UInt32 val)
        {
            byte[] buf = BitConverter.GetBytes(val);
            int bytesRead;

            if (!WriteProcessMemory(addr, 4, buf, out bytesRead))
                return false;
            if (bytesRead != 4)
                return false;

            return true;
        }

        #endregion

        #region libc stuff...

        private bool memEqual(byte[] b1, byte[] b2, int len, int off1, int off2)
        {
            int i;

            for (i = 0; i < len; ++i)
            {
                if (b1[off1 + i] != b2[off2 + i])
                    return false;
            }

            return true;
        }

        private void memset(byte[] b, byte val, int len, int off)
        {
            for (int i = 0; i < len; ++i)
            {
                b[off + i] = val;
            }
        }

        private void memcpy(byte[] b1, byte[] b2, int len, int off1, int off2)
        {
            for (int i = 0; i < len; ++i)
            {
                b1[off1 + i] = b2[i + off2];
            }
        }

        #endregion

        #region PSO Patching Methods

        private bool PatchGServer(IMAGE_SECTION_HEADER sec, byte[] data, string serverName = "sylverant.net")
        {
            int y, bytesWritten;
            const string origServ = "pso20.sonic.isao.net";
            const string origServ2 = "sg207634.sonicteam.com";
            const string origServ3 = "pso-mp01.sonic.isao.net";
            const string origServ4 = "gsproduc.ath.cx";
            string serv = serverName;
            byte[] opBytes = Encoding.ASCII.GetBytes(origServ);
            byte[] opBytes2 = Encoding.ASCII.GetBytes(origServ2);
            byte[] opBytes3 = Encoding.ASCII.GetBytes(origServ3);
            byte[] opBytes4 = Encoding.ASCII.GetBytes(origServ4);
            byte[] pBytes = Encoding.ASCII.GetBytes(serv);
            bool rv = false;

            for (y = 0; y < sec.virtSz - serv.Length; ++y)
            {
                if (memEqual(data, opBytes, origServ.Length, y, 0))
                {
                    memset(data, 0, origServ.Length, y);
                    memcpy(data, pBytes, serv.Length, y, 0);
                    rv = true;
                }
                else if (memEqual(data, opBytes2, origServ2.Length, y, 0))
                {
                    memset(data, 0, origServ2.Length, y);
                    memcpy(data, pBytes, serv.Length, y, 0);
                    rv = true;
                }
                else if (memEqual(data, opBytes3, origServ3.Length, y, 0))
                {
                    memset(data, 0, origServ3.Length, y);
                    memcpy(data, pBytes, serv.Length, y, 0);
                    rv = true;
                }
                else if (memEqual(data, opBytes4, origServ4.Length, y, 0))
                {
                    memset(data, 0, origServ4.Length, y);
                    memcpy(data, pBytes, serv.Length, y, 0);
                    rv = true;
                }
            }

            if (rv && !WriteProcessMemory(0x00400000 + sec.virtAddr, sec.virtSz, data, out bytesWritten))
                return false;

            return rv;
        }

        public bool PatchPSO(bool v1, bool cuss, bool music, bool mapfix, string serverName = "sylverant.net") //Adding serverName variable to allow connecting a different server
        {
            PEHeader hdr = new PEHeader(this);
            long start = DateTime.Now.Ticks;
            bool done = false;
            byte[] data;
            int bytesRead;

            while ((start + 100000 > DateTime.Now.Ticks) && !done)
            {
                foreach (IMAGE_SECTION_HEADER sec in hdr.Sections)
                {
                    if (!ReadProcessMemory(0x00400000 + sec.virtAddr, sec.virtSz, out data, out bytesRead))
                        return false;

                    if (serverName != null)
                    {
                        if (!serverName.Trim().Equals(string.Empty))
                        {
                            PatchGServer(sec, data, serverName);
                        }
                        else
                        {
                            PatchGServer(sec, data);
                        }
                    }

                    if (music)
                        MusicPatch(sec, data);

                    if (mapfix && sec.name.Equals(".data"))
                    {
                        if (!DetectMapfix(sec, data))
                            PerformMapfix(sec, data);
                        else
                            Console.Out.WriteLine("Mapfix appears to have been done manually!");
                    }
                }

                if (v1)
                    PatchV1Names(hdr);

                if (cuss)
                    PatchCusshack(hdr);
            }

            return done;
        }

        /* Universal V1 name patch! */
        private bool PatchV1Names(PEHeader hdr)
        {
            byte[] data;
            int bytesRead;
            UInt32 loc;

            /* Find the instruction we want to patch. */
            foreach (IMAGE_SECTION_HEADER sec in hdr.Sections)
            {
                if (sec.name.Equals(".text"))
                {
                    if (!ReadProcessMemory(0x00400000 + sec.virtAddr, sec.virtSz, out data, out bytesRead))
                        return false;

                    loc = FindNextPtr(sec, data, 0xFFFFAE35, 0);

                    while (loc != UInt32.MaxValue)
                    {
                        /* See if this is one of the two mov dword ptr [edi+18h], 0FFFFAE35h
                            * occurances, and if it is, is it the correct one... */
                        if (data[loc - 1] == 0x18 && data[loc - 2] == 0x47 &&
                            data[loc - 3] == 0xC7 && data[loc + 4] == 0xE8)
                        {
                            /* Patch the value in correctly. */
                            data[loc] = 0xFF;
                            data[loc + 1] = 0xFF;

                            if (!WriteProcessMemory(0x00400000 + sec.virtAddr, sec.virtSz, data, out bytesRead))
                                return false;

                            return true;
                        }

                        loc = FindNextPtr(sec, data, 0xFFFFAE35, loc);
                    }
                }
            }

            Console.Out.WriteLine("Couldn't find location to patch for v1 name color.");
            return false;
        }

        /* Universal Cusshack! */
        private bool PatchCusshack(PEHeader hdr)
        {
            byte[] data;
            int bytesRead;
            string censorStr = "#!@%#!@%#!@%#!@%#!@%#!@%#!@%#!@%#!@%";
            UInt32 censorStrLoc = UInt32.MaxValue, ptrLoc;

            /* First, find the location of the censoring string. It should
             * always be in the .data segment of the binary. */
            foreach (IMAGE_SECTION_HEADER sec in hdr.Sections)
            {
                if (sec.name.Equals(".data"))
                {
                    if (!ReadProcessMemory(0x00400000 + sec.virtAddr, sec.virtSz, out data, out bytesRead))
                        return false;

                    censorStrLoc = FindString(sec, data, Encoding.ASCII.GetBytes(censorStr));
                    if (censorStrLoc == UInt32.MaxValue)
                        return false;
                }
            }

            if (censorStrLoc == UInt32.MaxValue)
                return false;

            /* Next, find the one and only absolute reference to it in the
             * .text segment. */
            foreach (IMAGE_SECTION_HEADER sec in hdr.Sections)
            {
                if (sec.name.Equals(".text"))
                {
                    if (!ReadProcessMemory(0x00400000 + sec.virtAddr, sec.virtSz, out data, out bytesRead))
                        return false;

                    ptrLoc = FindPtr(sec, data, censorStrLoc);
                    if (ptrLoc == UInt32.MaxValue)
                        return false;

                    ptrLoc -= 0x00400000 + sec.virtAddr;

                    /* See if the value 46 bytes up is a jz instruction, as we expect. */
                    if (data[ptrLoc - 46] == 0x74)
                    {
                        /* Patch the instruction to two nops instead. */
                        data[ptrLoc - 46] = 0x90;
                        data[ptrLoc - 45] = 0x90;
                        if (!WriteProcessMemory(0x00400000 + sec.virtAddr, sec.virtSz, data, out bytesRead))
                            return false;

                        return true;
                    }
                    else if (data[ptrLoc - 46] == 0x90 && data[ptrLoc - 45] == 0x90)
                    {
                        Console.Out.WriteLine("Cusshack appears to have been applied manually!");
                        return true;
                    }
                    else
                    {
                        Console.Out.WriteLine("Invalid data where jz was expected.");
                        return false;
                    }
                }
            }

            return false;
        }

        private UInt32 FindString(IMAGE_SECTION_HEADER sec, byte[] data, byte[] search)
        {
            UInt32 y;

            for (y = 0; y < sec.virtSz - search.Length; ++y)
            {
                if (memEqual(data, search, search.Length, (int)y, 0) && data[y + search.Length] == 0)
                    return y + 0x00400000 + sec.virtAddr;
            }

            return UInt32.MaxValue;
        }

        private UInt32 FindNextPtr(IMAGE_SECTION_HEADER sec, byte[] data, UInt32 search, UInt32 y)
        {
            byte[] b = BitConverter.GetBytes(search);

            /* If we're resuming, then don't look at the same position. */
            if (y != 0)
                ++y;

            for (; y < sec.virtSz - 4; ++y)
            {
                if (memEqual(data, b, 4, (int)y, 0))
                    return y;
            }

            return UInt32.MaxValue;
        }

        private UInt32 FindPtr(IMAGE_SECTION_HEADER sec, byte[] data, UInt32 search)
        {
            UInt32 rv = FindNextPtr(sec, data, search, 0);
            if (rv != UInt32.MaxValue)
                rv += 0x00400000 + sec.virtAddr;
            return rv;
        }

        private bool MusicPatch(IMAGE_SECTION_HEADER sec, byte[] data)
        {
            UInt32 mamboPtr = FindString(sec, data, Encoding.ASCII.GetBytes("mambo.adx"));

            if (mamboPtr == UInt32.MaxValue)
                return false;

            UInt32 chuPtr = FindString(sec, data, Encoding.ASCII.GetBytes("chu_f.adx"));

            if (chuPtr == UInt32.MaxValue)
                return false;

            UInt32 duel1Ptr = FindString(sec, data, Encoding.ASCII.GetBytes("duel1.adx"));

            if (duel1Ptr == UInt32.MaxValue)
                return false;

            UInt32 duel2Ptr = FindString(sec, data, Encoding.ASCII.GetBytes("duel2.adx"));

            if (duel2Ptr == UInt32.MaxValue)
                return false;

            UInt32 mambo2Ptr = FindPtr(sec, data, mamboPtr);

            if (mambo2Ptr == UInt32.MaxValue)
                return false;

            UInt32 chu2byo = FindPtr(sec, data, chuPtr);

            if (chu2byo == UInt32.MaxValue)
                return false;

            WriteUInt32(mambo2Ptr, duel1Ptr);
            WriteUInt32(chu2byo, duel2Ptr);

            return true;
        }

        private bool DetectMapfix(IMAGE_SECTION_HEADER sec, byte[] data)
        {
            /* Assume that if one map has been fixed that all of them have. */
            UInt32 acaveptr = FindString(sec, data, Encoding.ASCII.GetBytes("map_acave01_05"));

            /* If we find that string, then the Cave 1 mapset is full, so the mapfix must have
             * already been applied. */
            if (acaveptr != UInt32.MaxValue)
                return true;

            return false;
        }

        private bool PerformMapfix(IMAGE_SECTION_HEADER sec, byte[] data)
        {
            const string acave01_05 = "map_acave01_05";         // 15
            const string acave03_05 = "map_acave03_05";         // 15
            const string amachine01_05 = "map_amachine01_05";   // 18
            const string amachine02_05 = "map_amachine02_05";   // 18
            int bytesWritten;
            uint loc = 0, loc2;

            /* First, allocate enough space for the strings... */
            IntPtr mapptr = Kernel32.VirtualAllocEx(proc.Handle, IntPtr.Zero, 512, Kernel32.MEM_COMMIT, Kernel32.PAGE_READWRITE);

            if (mapptr == IntPtr.Zero)
                return false;

            /* Write the map strings into the process' heap. */
            if (!WriteProcessMemory((uint)mapptr, 15, Encoding.ASCII.GetBytes(acave01_05), out bytesWritten) ||
                bytesWritten != 15)
                return false;

            loc += (uint)bytesWritten;

            if (!WriteProcessMemory(((uint)mapptr) + loc, 15, Encoding.ASCII.GetBytes(acave03_05), out bytesWritten) ||
                bytesWritten != 15)
                return false;

            loc += (uint)bytesWritten;

            if (!WriteProcessMemory(((uint)mapptr) + loc, 18, Encoding.ASCII.GetBytes(amachine01_05), out bytesWritten) ||
                bytesWritten != 18)
                return false;

            loc += (uint)bytesWritten;

            if (!WriteProcessMemory(((uint)mapptr) + loc, 18, Encoding.ASCII.GetBytes(amachine02_05), out bytesWritten) ||
                bytesWritten != 18)
                return false;

            loc += (uint)bytesWritten;

            loc2 = WriteSingleMap(sec, data, "map_acave01", "map_acave01_00", ((uint)mapptr) + loc, (uint)mapptr);
            if (loc2 == UInt32.MaxValue)
                return false;

            loc2 = WriteSingleMap(sec, data, "map_acave03", "map_acave03_00", loc2, ((uint)mapptr) + 15);
            if (loc2 == UInt32.MaxValue)
                return false;

            loc2 = WriteSingleMap(sec, data, "map_amachine01", "map_amachine01_00", loc2, ((uint)mapptr) + 30);
            if (loc2 == UInt32.MaxValue)
                return false;

            loc2 = WriteSingleMap(sec, data, "map_amachine02", "map_amachine02_00", loc2, ((uint)mapptr) + 48);
            if (loc2 == UInt32.MaxValue)
                return false;

            return true;
        }

        private UInt32 WriteSingleMap(IMAGE_SECTION_HEADER sec, byte[] data,
            string mapbase, string map, UInt32 start, UInt32 mapptr)
        {
            /* Now, find the map tables... */
            UInt32 acave01_00_loc = FindString(sec, data, Encoding.ASCII.GetBytes(map));
            UInt32 acave01_loc = FindString(sec, data, Encoding.ASCII.GetBytes(mapbase));
            UInt32 acave01_00_ptr = FindPtr(sec, data, acave01_00_loc);
            /* The acave01_00_ptr should point at the middle of the first entry for ult maps
             * for the specified area. */
            UInt32 acave01_ptr_ptr = FindPtr(sec, data, acave01_00_ptr - 4);
            /* acave01_ptr_ptr should point at the first thing we'll have to change now. */
            int bytesWritten;
            byte[] tmp;

            if (!ReadProcessMemory(acave01_00_ptr - 4, 40, out tmp, out bytesWritten) ||
                bytesWritten != 40)
                return UInt32.MaxValue;

            WriteUInt32(acave01_ptr_ptr, start);
            WriteUInt32(acave01_ptr_ptr + 4, 6);

            if (!WriteProcessMemory(start, 40, tmp, out bytesWritten) ||
                bytesWritten != 40)
                return UInt32.MaxValue;

            start += 40;
            WriteUInt32(start, acave01_loc);
            WriteUInt32(start + 4, (uint)mapptr);
            start += 8;

            return start;
        }

        #endregion
    }
}
