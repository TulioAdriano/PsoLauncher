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
using System.Runtime.InteropServices;
using Yggdrasill;

namespace Sylverant
{
    public unsafe class IDirectInputDevice8
    {
        #region Variables

        // A pointer to the native IDirectInputDevice8 object that we are providing overrides for.
        public DirectInput8.IDirectInputDevice8* Device
        {
            get;
            private set;
        }

        // Our link back to the launcher
        private YggdrasillInterface Interface;

        // Pointers to any of the old functions we override.
        private IntPtr oldGetDeviceState;
        private IntPtr oldSetCooperativeLevel;

        // Delegate storage
        private DGetDeviceState del_gds;
        private DSetCooperativeLevel del_scl;

        // Mouse stuff...
        private byte buttonState;

        #endregion

        #region Constructors

        public unsafe IDirectInputDevice8(DirectInput8.IDirectInputDevice8* InDevice, YggdrasillInterface iface)
        {
            Device = InDevice;
            Interface = iface;

            /* Hook up any functions we care about */
            OverrideFunctions();
        }

        private void OverrideFunctions()
        {
            // List of functions in the IDirectInputDevice8 interface:
            // 0:  STDMETHOD(QueryInterface)(THIS_ REFIID riid, LPVOID * ppvObj) PURE;
            // 1:  STDMETHOD_(ULONG,AddRef)(THIS) PURE;
            // 2:  STDMETHOD_(ULONG,Release)(THIS) PURE;
            // 3:  STDMETHOD(GetCapabilities)(THIS_ LPDIDEVCAPS) PURE;
            // 4:  STDMETHOD(EnumObjects)(THIS_ LPDIENUMDEVICEOBJECTSCALLBACKA,LPVOID,DWORD) PURE;
            // 5:  STDMETHOD(GetProperty)(THIS_ REFGUID,LPDIPROPHEADER) PURE;
            // 6:  STDMETHOD(SetProperty)(THIS_ REFGUID,LPCDIPROPHEADER) PURE;
            // 7:  STDMETHOD(Acquire)(THIS) PURE;
            // 8:  STDMETHOD(Unacquire)(THIS) PURE;
            // 9:  STDMETHOD(GetDeviceState)(THIS_ DWORD,LPVOID) PURE;
            // 10: STDMETHOD(GetDeviceData)(THIS_ DWORD,LPDIDEVICEOBJECTDATA,LPDWORD,DWORD) PURE;
            // 11: STDMETHOD(SetDataFormat)(THIS_ LPCDIDATAFORMAT) PURE;
            // 12: STDMETHOD(SetEventNotification)(THIS_ HANDLE) PURE;
            // 13: STDMETHOD(SetCooperativeLevel)(THIS_ HWND,DWORD) PURE;
            // 14: STDMETHOD(GetObjectInfo)(THIS_ LPDIDEVICEOBJECTINSTANCEA,DWORD,DWORD) PURE;
            // 15: STDMETHOD(GetDeviceInfo)(THIS_ LPDIDEVICEINSTANCEA) PURE;
            // 16: STDMETHOD(RunControlPanel)(THIS_ HWND,DWORD) PURE;
            // 17: STDMETHOD(Initialize)(THIS_ HINSTANCE,DWORD,REFGUID) PURE;
            // 18: STDMETHOD(CreateEffect)(THIS_ REFGUID,LPCDIEFFECT,LPDIRECTINPUTEFFECT *,LPUNKNOWN) PURE;
            // 19: STDMETHOD(EnumEffects)(THIS_ LPDIENUMEFFECTSCALLBACKA,LPVOID,DWORD) PURE;
            // 20: STDMETHOD(GetEffectInfo)(THIS_ LPDIEFFECTINFOA,REFGUID) PURE;
            // 21: STDMETHOD(GetForceFeedbackState)(THIS_ LPDWORD) PURE;
            // 22: STDMETHOD(SendForceFeedbackCommand)(THIS_ DWORD) PURE;
            // 23: STDMETHOD(EnumCreatedEffectObjects)(THIS_ LPDIENUMCREATEDEFFECTOBJECTSCALLBACK,LPVOID,DWORD) PURE;
            // 24: STDMETHOD(Escape)(THIS_ LPDIEFFESCAPE) PURE;
            // 25: STDMETHOD(Poll)(THIS) PURE;
            // 26: STDMETHOD(SendDeviceData)(THIS_ DWORD,LPCDIDEVICEOBJECTDATA,LPDWORD,DWORD) PURE;
            // 27: STDMETHOD(EnumEffectsInFile)(THIS_ LPCSTR,LPDIENUMEFFECTSINFILECALLBACK,LPVOID,DWORD) PURE;
            // 28: STDMETHOD(WriteEffectToFile)(THIS_ LPCSTR,DWORD,LPDIFILEEFFECT,DWORD) PURE;
            // 29: STDMETHOD(BuildActionMap)(THIS_ LPDIACTIONFORMATA,LPCSTR,DWORD) PURE;
            // 30: STDMETHOD(SetActionMap)(THIS_ LPDIACTIONFORMATA,LPCSTR,DWORD) PURE;
            // 31: STDMETHOD(GetImageInfo)(THIS_ LPDIDEVICEIMAGEINFOHEADERA) PURE;
            oldGetDeviceState = Device->vtable[9];
            del_gds = new DGetDeviceState(GetDeviceState);
            IntPtr ptr = Marshal.GetFunctionPointerForDelegate(del_gds);
            Device->vtable[9] = ptr;

            oldSetCooperativeLevel = Device->vtable[13];
            del_scl = new DSetCooperativeLevel(SetCooperativeLevel);
            ptr = Marshal.GetFunctionPointerForDelegate(del_scl);
            Device->vtable[13] = ptr;
        }

        #endregion

        #region IDirectInputDevice8 Interface Function Implementations

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public unsafe delegate int DGetDeviceState(DirectInput8.IDirectInputDevice8* This, UInt32 cbData, IntPtr lpData);

        public unsafe int GetDeviceState(DirectInput8.IDirectInputDevice8* This, UInt32 cbData, IntPtr lpData)
        {
            DGetDeviceState del = (DGetDeviceState)Marshal.GetDelegateForFunctionPointer(oldGetDeviceState, typeof(DGetDeviceState));
            int rv = del(This, cbData, lpData);

            /* XXXX: HACK!! 20 is the size of mouse data... Hopefully it isn't the size of anything else we care about. */
            if (cbData == 20)
            {
                DirectInput8.DIMOUSESTATE2* ms = (DirectInput8.DIMOUSESTATE2*)lpData;
                Process p = Process.GetCurrentProcess();

                /* We only care when we go from not clicked to clicked... */
                if (buttonState == 0 && ms->rgbButtons_0 != 0)
                {
                    User32.RECT wndRect = new User32.RECT();
                    User32.POINT point = new User32.POINT();

                    User32.GetWindowRect(p.MainWindowHandle, ref wndRect);
                    User32.GetCursorPos(out point);

                    /* Check bounds. */
                    if (point.X < wndRect.Left || point.X > wndRect.Right || point.Y < wndRect.Top || point.Y > wndRect.Bottom)
                    {
                        ms->rgbButtons_0 = 0;
                        ms->rgbButtons_1 = 0;
                    }
                }

                buttonState = ms->rgbButtons_0;
            }

            /* La de dah... Imma collect some garbage now. */
            GC.Collect();

            return rv;
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public unsafe delegate int DSetCooperativeLevel(DirectInput8.IDirectInputDevice8* This, IntPtr hWnd, UInt32 dwFlags);

        public unsafe int SetCooperativeLevel(DirectInput8.IDirectInputDevice8* This, IntPtr hWnd, UInt32 dwFlags)
        {
            DSetCooperativeLevel del = (DSetCooperativeLevel)Marshal.GetDelegateForFunctionPointer(oldSetCooperativeLevel, typeof(DSetCooperativeLevel));
            
            /* Ask for non-exclusive, foreground-only access. Might need to revisit this for gamepads... */
            dwFlags = DirectInput8.DISCL_NONEXCLUSIVE | DirectInput8.DISCL_FOREGROUND;
            return del(This, hWnd, dwFlags);
        }

        #endregion
    }
}
