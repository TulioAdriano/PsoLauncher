/*
    This file is part of Mithos
    Copyright (C) 2013 Lawrence Sebald

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
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

using EasyHook;
using Yggdrasill;

namespace Sylverant
{
    public class Mithos : IEntryPoint
    {
        private static YggdrasillInterface Interface;
        private IDirect3D8 d3d8;
        private IDirectInput8 di8;

        // CreateWindowExA
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        delegate IntPtr DCreateWindowExA(uint dwExStyle, IntPtr lpClassName, string lpWindowName,
            uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu,
            IntPtr hInstance, IntPtr lParam);

        // Direct3DCreate8
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        unsafe delegate D3D8.IDirect3D8* DDirect3DCreate8(uint SDKVersion);

        // DirectInput8Create
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        unsafe delegate int DDirectInput8Create(IntPtr hInst, UInt32 dwVersion, IntPtr riidltf, DirectInput8.IDirectInput8** ppvOut, IntPtr punkOuter);

        // ImmGetOpenStatus
        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        unsafe delegate int DImmGetOpenStatus(IntPtr hIMC);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
        unsafe delegate int DImmGetConversionStatus(IntPtr hIMC, UInt32* conversion, UInt32* sentence);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
        delegate int DSetForegroundWindow(IntPtr pfwi);

        static IntPtr CreateWindowExA_Hooked(uint dwExStyle, IntPtr lpClassName, string lpWindowName,
            uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu,
            IntPtr hInstance, IntPtr lParam)
        {
            /* Damn you fullscreen! */
            dwStyle = 0x00cf0000;
            dwExStyle = 0;
            return User32.CreateWindowExA(dwExStyle, lpClassName, lpWindowName, dwStyle, x, y, nWidth, nHeight,
                hWndParent, hMenu, hInstance, lParam);
        }

        static unsafe D3D8.IDirect3D8* Direct3DCreate8_Hooked(uint SDKVersion)
        {
            Mithos This = (Mithos)HookRuntimeInfo.Callback;
            This.d3d8 = new IDirect3D8(SDKVersion, Interface);
            return This.d3d8.Context;
        }

        static unsafe int DirectInput8Create_Hooked(IntPtr hInst, UInt32 dwVersion, IntPtr riidltf,
            DirectInput8.IDirectInput8** ppvOut, IntPtr punkOuter)
        {
            Mithos This = (Mithos)HookRuntimeInfo.Callback;
            int rv;

            This.di8 = new IDirectInput8(hInst, dwVersion, riidltf, ppvOut, punkOuter, Interface, out rv);
            return rv;
        }

        static int SetForegroundWindow_Hooked(IntPtr pfwi)
        {
            return 1;
        }

        public Mithos(RemoteHooking.IContext InContext, string channel, bool disableime,
            bool windowed)
        {
            Interface = RemoteHooking.IpcConnectClient<YggdrasillInterface>(channel);
        }

        public unsafe void Run(RemoteHooking.IContext InContext, string channel, bool disableime,
            bool windowed)
        {
            if (windowed)
            {
                LocalHook CreateWindowExAHook = LocalHook.Create(
                    LocalHook.GetProcAddress("user32.dll", "CreateWindowExA"),
                    new DCreateWindowExA(CreateWindowExA_Hooked), this);
                CreateWindowExAHook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });

                LocalHook D3DCreateHook = LocalHook.Create(
                    LocalHook.GetProcAddress("d3d8.dll", "Direct3DCreate8"),
                    new DDirect3DCreate8(Direct3DCreate8_Hooked), this);
                D3DCreateHook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });

                LocalHook DICreateHook = LocalHook.Create(
                    LocalHook.GetProcAddress("dinput8.dll", "DirectInput8Create"),
                    new DDirectInput8Create(DirectInput8Create_Hooked), this);
                DICreateHook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });

                LocalHook FlashHook = LocalHook.Create(
                    LocalHook.GetProcAddress("user32.dll", "SetForegroundWindow"),
                    new DSetForegroundWindow(SetForegroundWindow_Hooked), this);
                FlashHook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
            }

            if(disableime)
                Imm32.ImmDisableIME(UInt32.MaxValue);

            while (true)
                Thread.Sleep(1000);
        }
    }
}
