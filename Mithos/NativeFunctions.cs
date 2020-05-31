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
using System.Security;
using System.Runtime.InteropServices;

namespace Sylverant
{
    public class User32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CreateWindowExA(uint dwExStyle, IntPtr lpClassName, string lpWindowName,
            uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu,
            IntPtr hInstance, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
    }

    public class Kernel32
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, UIntPtr dwBytes);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcessHeap();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }

    public class Imm32
    {
        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern int ImmDisableIME(UInt32 idThread);
    }

    public unsafe class D3D8
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public unsafe struct IDirect3D8
        {
            public IntPtr* vtable;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct D3DPRESENT_PARAMETERS
        {
            public uint BackBufferWidth;
            public uint BackBufferHeight;
            public int BackBufferFormat;
            public uint BackBufferCount;
            public uint MultiSampleType;
            public UInt32 MultiSampleQuality;
            public uint SwapEffect;
            public IntPtr hDeviceWindow;
            public int Windowed;
            public int EnableAutoDepthStencil;
            public int AutoDepthStencilFormat;
            public UInt32 Flags;
            public uint FullScreen_RefreshRateInHz;
            public uint PresentationInterval;
        }

        [DllImport("d3d8.dll", EntryPoint = "Direct3DCreate8", CallingConvention = CallingConvention.StdCall), SuppressUnmanagedCodeSecurity]
        public static extern IDirect3D8* Direct3DCreate8(uint SDKVersion);
    }

    public unsafe class DirectInput8
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct GUID {
            public UInt32 Data1;
            public UInt16 Data2;
            public UInt16 Data3;
            public byte Data4_1;
            public byte Data4_2;
            public byte Data4_3;
            public byte Data4_4;
            public byte Data4_5;
            public byte Data4_6;
            public byte Data4_7;
            public byte Data4_8;
        }

        public const int DISCL_EXCLUSIVE = 0x00000001;
        public const int DISCL_NONEXCLUSIVE = 0x00000002;
        public const int DISCL_FOREGROUND = 0x00000004;
        public const int DISCL_BACKGROUND = 0x00000008;
        public const int DISCL_NOWINKEY = 0x00000010;

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public unsafe struct IDirectInput8
        {
            public IntPtr* vtable;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public unsafe struct IDirectInputDevice8
        {
            public IntPtr* vtable;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DIMOUSESTATE2
        {
            public Int32 lX;
            public Int32 lY;
            public Int32 lZ;
            public byte rgbButtons_0;
            public byte rgbButtons_1;
            public byte rgbButtons_2;
            public byte rgbButtons_3;
            public byte rgbButtons_4;
            public byte rgbButtons_5;
            public byte rgbButtons_6;
            public byte rgbButtons_7;
        }

        [DllImport("dinput8.dll", EntryPoint = "DirectInput8Create", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
        public static extern int DirectInput8Create(IntPtr hInst, UInt32 dwVersion, IntPtr riidltf, DirectInput8.IDirectInput8** ppvOut, IntPtr punkOuter);
    }
}
