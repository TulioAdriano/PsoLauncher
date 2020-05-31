/*
    This file is part of Yggdrasill
    Copyright (C) 2012 Lawrence Sebald

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
using System.Runtime.InteropServices;

namespace Yggdrasill
{
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    struct IMAGE_FILE_HEADER
    {
        public UInt32 signature;
        public UInt16 machine;
        public UInt16 numSections;
        public UInt32 timestamp;
        public UInt32 symbolTablePtr;
        public UInt32 numSymbols;
        public UInt16 optHdrSize;
        public UInt16 characteristics;
    }

    /* Unused at the moment, but included for completeness... */
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    struct IMAGE_DATA_DIRECTORY
    {
        public UInt32 virtAddr;
        public UInt32 sz;
    }

    /* Unused at the moment, but included for completeness... */
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    struct IMAGE_OPTIONAL_HEADER
    {
        public UInt16 magic;
        public Byte majorLinkerVer;
        public Byte minorLinkerVer;
        public UInt32 codeSz;
        public UInt32 initDataSz;
        public UInt32 uninitDataSz;
        public UInt32 entryPoint;
        public UInt32 codeBase;
        public UInt32 dataBase;
        public UInt32 imageBase;
        public UInt32 sectionAlignment;
        public UInt32 fileAlignment;
        public UInt16 majorOSVersion;
        public UInt16 minorOSVersion;
        public UInt16 majorImageVersion;
        public UInt16 minorImageVersion;
        public UInt16 majorSubsysVersion;
        public UInt16 minorSubsysVersion;
        public UInt32 reserved;
        public UInt32 imageSz;
        public UInt32 hdrSz;
        public UInt32 crc;
        public UInt16 subsystem;
        public UInt16 dllCharacteristics;
        public UInt32 stackReserve;
        public UInt32 stackCommit;
        public UInt32 heapReserve;
        public UInt32 heapCommit;
        public UInt32 loaderFlags;
        public UInt32 numRVAAndSz;
    }

    [StructLayout(LayoutKind.Sequential, Pack=1)]
    struct IMAGE_SECTION_HEADER 
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=8)]
        public string name;
        public UInt32 virtSz;
        public UInt32 virtAddr;
        public UInt32 rawDataSz;
        public UInt32 rawDataPtr;
        public UInt32 relocPtr;
        public UInt32 linePtr;
        public UInt16 numRelocs;
        public UInt16 numLines;
        public UInt32 characteristics;
    }

    class PEHeader
    {
        #region Instance Variables

        private ProcessHaxxor hax;
        private IMAGE_FILE_HEADER hdr;
        private IMAGE_SECTION_HEADER[] sections;
        
        #endregion

        #region Constructor

        public PEHeader(ProcessHaxxor haxxor)
        {
            const uint baseAddr = 0x00400000;
            int secSz = Marshal.SizeOf(typeof(IMAGE_SECTION_HEADER));
            byte[] buf;
            int bytesRead;
            GCHandle pin;
            UInt32 hdrAddr;

            hax = haxxor;

            /* Find the PE header */
            if (!hax.ReadUInt32(baseAddr + 0x3c, out hdrAddr))
                throw new InvalidOperationException("Cannot read PE Header Address");

            /* Read in the header */
            if (!hax.ReadProcessMemory(baseAddr + hdrAddr, (uint)Marshal.SizeOf(hdr), out buf,
                out bytesRead))
                throw new InvalidOperationException("Cannot read PE header");

            if (bytesRead != Marshal.SizeOf(hdr))
                throw new InvalidOperationException("Cannot read PE header");

            pin = GCHandle.Alloc(buf, GCHandleType.Pinned);
            hdr = (IMAGE_FILE_HEADER)Marshal.PtrToStructure(pin.AddrOfPinnedObject(),
                typeof(IMAGE_FILE_HEADER));
            pin.Free();

            if (hdr.signature != 0x00004550)
                throw new InvalidOperationException("Invalid PE Signature");

            if (hdr.optHdrSize != Marshal.SizeOf(typeof(IMAGE_OPTIONAL_HEADER)) +
                16 * Marshal.SizeOf(typeof(IMAGE_DATA_DIRECTORY)))
                throw new InvalidOperationException("Invalid Optional Header Size");

            /* Read in the section headers. */
            sections = new IMAGE_SECTION_HEADER[hdr.numSections];
            for (UInt16 i = 0; i < hdr.numSections; ++i)
            {
                if (!hax.ReadProcessMemory((uint)(baseAddr + hdrAddr + hdr.optHdrSize + 0x18 + secSz * i),
                    (uint)secSz, out buf, out bytesRead))
                    throw new InvalidOperationException("Cannot read section header");

                if (bytesRead != secSz)
                    throw new InvalidOperationException("Cannot read section header");

                pin = GCHandle.Alloc(buf, GCHandleType.Pinned);
                sections[i] = (IMAGE_SECTION_HEADER)Marshal.PtrToStructure(pin.AddrOfPinnedObject(),
                    typeof(IMAGE_SECTION_HEADER));
                pin.Free();
            }
        }

        #endregion

        public IMAGE_SECTION_HEADER[] Sections
        {
            get { return sections; }
        }

        public IMAGE_FILE_HEADER Header
        {
            get { return hdr; }
        }
    }
}
