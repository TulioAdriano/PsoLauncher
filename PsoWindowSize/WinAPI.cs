/*
    This file is part of PsoWindowResize
    Copyright (C) 2015 Tulio Gonçalves

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

/* THIS FILE NEEDS A LOT OF CLEANUP
 * There's a lot of commented source code that coulde definitely go... */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PsoWindowSize
{
    /// <summary>
    /// Switches between color and monochrome on color printers.
    /// </summary>
    //internal enum DMCOLOR : short
    //{
    //    DMCOLOR_UNKNOWN = 0,
    //    DMCOLOR_MONOCHROME = 1,
    //    DMCOLOR_COLOR = 2
    //}

    ///// <summary>
    ///// Specifies whether collation should be used when printing multiple copies.
    ///// </summary>
    //internal enum DMCOLLATE : short
    //{
    //    /// <summary>
    //    /// Do not collate when printing multiple copies.
    //    /// </summary>
    //    DMCOLLATE_FALSE = 0,

    //    /// <summary>
    //    /// Collate when printing multiple copies.
    //    /// </summary>
    //    DMCOLLATE_TRUE = 1
    //}

    //[Flags()]
    //enum DM : int
    //{
    //    Orientation = 0x1,
    //    PaperSize = 0x2,
    //    PaperLength = 0x4,
    //    PaperWidth = 0x8,
    //    Scale = 0x10,
    //    Position = 0x20,
    //    NUP = 0x40,
    //    DisplayOrientation = 0x80,
    //    Copies = 0x100,
    //    DefaultSource = 0x200,
    //    PrintQuality = 0x400,
    //    Color = 0x800,
    //    Duplex = 0x1000,
    //    YResolution = 0x2000,
    //    TTOption = 0x4000,
    //    Collate = 0x8000,
    //    FormName = 0x10000,
    //    LogPixels = 0x20000,
    //    BitsPerPixel = 0x40000,
    //    PelsWidth = 0x80000,
    //    PelsHeight = 0x100000,
    //    DisplayFlags = 0x200000,
    //    DisplayFrequency = 0x400000,
    //    ICMMethod = 0x800000,
    //    ICMIntent = 0x1000000,
    //    MediaType = 0x2000000,
    //    DitherType = 0x4000000,
    //    PanningWidth = 0x8000000,
    //    PanningHeight = 0x10000000,
    //    DisplayFixedOutput = 0x20000000
    //}

    //[StructLayout(LayoutKind.Sequential)]
    //public struct DEVMODE
    //{
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    //    public string dmDeviceName;
    //    public short dmSpecVersion;
    //    public short dmDriverVersion;
    //    public short dmSize;
    //    public short dmDriverExtra;
    //    public int dmFields;

    //    public short dmOrientation;
    //    public short dmPaperSize;
    //    public short dmPaperLength;
    //    public short dmPaperWidth;

    //    public short dmScale;
    //    public short dmCopies;
    //    public short dmDefaultSource;
    //    public short dmPrintQuality;
    //    public short dmColor;
    //    public short dmDuplex;
    //    public short dmYResolution;
    //    public short dmTTOption;
    //    public short dmCollate;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    //    public string dmFormName;
    //    public short dmLogPixels;
    //    public short dmBitsPerPel;
    //    public int dmPelsWidth;
    //    public int dmPelsHeight;

    //    public int dmDisplayFlags;
    //    public int dmDisplayFrequency;

    //    public int dmICMMethod;
    //    public int dmICMIntent;
    //    public int dmMediaType;
    //    public int dmDitherType;
    //    public int dmReserved1;
    //    public int dmReserved2;

    //    public int dmPanningWidth;
    //    public int dmPanningHeight;
    //};


    public enum TernaryRasterOperations : uint
    {
        SRCCOPY = 0x00CC0020,
        SRCPAINT = 0x00EE0086,
        SRCAND = 0x008800C6,
        SRCINVERT = 0x00660046,
        SRCERASE = 0x00440328,
        NOTSRCCOPY = 0x00330008,
        NOTSRCERASE = 0x001100A6,
        MERGECOPY = 0x00C000CA,
        MERGEPAINT = 0x00BB0226,
        PATCOPY = 0x00F00021,
        PATPAINT = 0x00FB0A09,
        PATINVERT = 0x005A0049,
        DSTINVERT = 0x00550009,
        BLACKNESS = 0x00000042,
        WHITENESS = 0x00FF0062,
        CAPTUREBLT = 0x40000000 //only if WinVer >= 5.0.0 (see wingdi.h)
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    /// <summary>
    /// Window handles (HWND) used for hWndInsertAfter
    /// </summary>
    //public static class HWND
    //{
    //    public static IntPtr
    //    NoTopMost = new IntPtr(-2),
    //    TopMost = new IntPtr(-1),
    //    Top = new IntPtr(0),
    //    Bottom = new IntPtr(1);
    //}

    public static class nIndex
    {
        public static readonly int
        GWL_EXSTYLE = -20,
        GWL_HINSTANCE = -6,
        GWL_ID = -12,
        GWL_STYLE = -16,
        GWL_USERDATA = -21,
        GWL_WNDPROC = -4;
    }

    public static class dwNewLong
    {
        public static readonly uint
        WS_BORDER = 0x00800000,
        WS_CAPTION = 0x00C00000,
        WS_CHILD = 0x40000000,
        WS_CHILDWINDOW = 0x40000000,
        WS_CLIPCHILDREN = 0x02000000,
        WS_CLIPSIBLINGS = 0x04000000,
        WS_DISABLED = 0x08000000,
        WS_DLGFRAME = 0x00400000,
        WS_GROUP = 0x00020000,
        WS_HSCROLL = 0x00100000,
        WS_ICONIC = 0x20000000,
        WS_MAXIMIZE = 0x01000000,
        WS_MAXIMIZEBOX = 0x00010000,
        WS_MINIMIZE = 0x20000000,
        WS_MINIMIZEBOX = 0x00020000,
        WS_OVERLAPPED = 0x00000000,
        WS_POPUP = 0x80000000,
        WS_SIZEBOX = 0x00040000,
        WS_SYSMENU = 0x00080000,
        WS_TABSTOP = 0x00010000,
        WS_THICKFRAME = 0x00040000,
        WS_TILED = 0x00000000,
        WS_VISIBLE = 0x10000000,
        WS_VSCROLL = 0x00200000;

        public static readonly long WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX;
        public static readonly long WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU;
        public static readonly long WS_TILEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX;
    }

    /// <summary>
    /// SetWindowPos Flags
    /// </summary>
    public static class SWP
    {
        public static readonly uint
        NOSIZE = 0x0001,
        NOMOVE = 0x0002,
        NOZORDER = 0x0004,
        NOREDRAW = 0x0008,
        NOACTIVATE = 0x0010,
        DRAWFRAME = 0x0020,
        FRAMECHANGED = 0x0020,
        SHOWWINDOW = 0x0040,
        HIDEWINDOW = 0x0080,
        NOCOPYBITS = 0x0100,
        NOOWNERZORDER = 0x0200,
        NOREPOSITION = 0x0200,
        NOSENDCHANGING = 0x0400,
        DEFERERASE = 0x2000,
        ASYNCWINDOWPOS = 0x4000;
    }

    /// <summary>Enumeration of the different ways of showing a window using 
    /// ShowWindow</summary>
    public static class WindowShowStyle 
    {
        public static uint
            /// <summary>Hides the window and activates another window.</summary>
            /// <remarks>See SW_HIDE</remarks>
        Hide = 0,
            /// <summary>Activates and displays a window. If the window is minimized 
            /// or maximized, the system restores it to its original size and 
            /// position. An application should specify this flag when displaying 
            /// the window for the first time.</summary>
            /// <remarks>See SW_SHOWNORMAL</remarks>
        ShowNormal = 1,
            /// <summary>Activates the window and displays it as a minimized window.</summary>
            /// <remarks>See SW_SHOWMINIMIZED</remarks>
        ShowMinimized = 2,
            /// <summary>Activates the window and displays it as a maximized window.</summary>
            /// <remarks>See SW_SHOWMAXIMIZED</remarks>
        ShowMaximized = 3,
            /// <summary>Maximizes the specified window.</summary>
            /// <remarks>See SW_MAXIMIZE</remarks>
        Maximize = 3,
            /// <summary>Displays a window in its most recent size and position. 
            /// This value is similar to "ShowNormal", except the window is not 
            /// actived.</summary>
            /// <remarks>See SW_SHOWNOACTIVATE</remarks>
        ShowNormalNoActivate = 4,
            /// <summary>Activates the window and displays it in its current size 
            /// and position.</summary>
            /// <remarks>See SW_SHOW</remarks>
        Show = 5,
            /// <summary>Minimizes the specified window and activates the next 
            /// top-level window in the Z order.</summary>
            /// <remarks>See SW_MINIMIZE</remarks>
        Minimize = 6,
            /// <summary>Displays the window as a minimized window. This value is 
            /// similar to "ShowMinimized", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
        ShowMinNoActivate = 7,
            /// <summary>Displays the window in its current size and position. This 
            /// value is similar to "Show", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWNA</remarks>
        ShowNoActivate = 8,
            /// <summary>Activates and displays the window. If the window is 
            /// minimized or maximized, the system restores it to its original size 
            /// and position. An application should specify this flag when restoring 
            /// a minimized window.</summary>
            /// <remarks>See SW_RESTORE</remarks>
        Restore = 9,
            /// <summary>Sets the show state based on the SW_ value specified in the 
            /// STARTUPINFO structure passed to the CreateProcess function by the 
            /// program that started the application.</summary>
            /// <remarks>See SW_SHOWDEFAULT</remarks>
        ShowDefault = 10,
            /// <summary>Windows 2000/XP: Minimizes a window, even if the thread 
            /// that owns the window is hung. This flag should only be used when 
            /// minimizing windows from a different thread.</summary>
            /// <remarks>See SW_FORCEMINIMIZE</remarks>
        ForceMinimized = 11;
    }


    public static class WinAPI
    {
        //public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        //public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        //public static readonly IntPtr HWND_TOP = new IntPtr(0);
        //public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        //[DllImport("user32.dll")]
        //public static extern int EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);
        //[DllImport("user32.dll")]
        //public static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);

        //public const int ENUM_CURRENT_SETTINGS = -1;
        //public const int CDS_UPDATEREGISTRY = 0x01;
        //public const int CDS_TEST = 0x02;
        //public const int DISP_CHANGE_SUCCESSFUL = 0;
        //public const int DISP_CHANGE_RESTART = 1;
        //public const int DISP_CHANGE_FAILED = -1;

        [DllImport("psapi.dll")]
        public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In] [MarshalAs(UnmanagedType.U4)] uint nSize);

        [DllImport("psapi.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int EnumProcessModules(IntPtr hProcess, [Out] IntPtr lphModule, uint cb, out uint lpcbNeeded);

        /// <summary>
        /// Changes an attribute of the specified window. The function also sets the 32-bit (long) value at the specified offset into the extra window memory.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs..</param>
        /// <param name="nIndex">The zero-based offset to the value to be set. Valid values are in the range zero through the number of bytes of extra window memory, minus the size of an integer. To set any other value, specify one of the following values: GWL_EXSTYLE, GWL_HINSTANCE, GWL_ID, GWL_STYLE, GWL_USERDATA, GWL_WNDPROC </param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified 32-bit integer. 
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError. </returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr child, IntPtr newParent);

        [DllImport("user32.dll")]
        public static extern IntPtr ShowWindow(IntPtr handle, uint command);

        // For Windows Mobile, replace user32.dll with coredll.dll 
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        /// <summary>
        ///    Performs a bit-block transfer of the color data corresponding to a
        ///    rectangle of pixels from the specified source device context into
        ///    a destination device context.
        /// </summary>
        /// <param name="hdc">Handle to the destination device context.</param>
        /// <param name="nXDest">The leftmost x-coordinate of the destination rectangle (in pixels).</param>
        /// <param name="nYDest">The topmost y-coordinate of the destination rectangle (in pixels).</param>
        /// <param name="nWidth">The width of the source and destination rectangles (in pixels).</param>
        /// <param name="nHeight">The height of the source and the destination rectangles (in pixels).</param>
        /// <param name="hdcSrc">Handle to the source device context.</param>
        /// <param name="nXSrc">The leftmost x-coordinate of the source rectangle (in pixels).</param>
        /// <param name="nYSrc">The topmost y-coordinate of the source rectangle (in pixels).</param>
        /// <param name="dwRop">A raster-operation code.</param>
        /// <returns>
        ///    <c>true</c> if the operation succeedes, <c>false</c> otherwise. To get extended error information, call <see cref="System.Runtime.InteropServices.Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, ref RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
    }
}
