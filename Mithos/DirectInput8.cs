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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Yggdrasill;

namespace Sylverant
{
    public unsafe class IDirectInput8
    {
        #region Variables

        // A pointer to the native IDirectInput8 object that we are providing overrides for.
        public DirectInput8.IDirectInput8* Context
        {
            get;
            private set;
        }

        // Our link back to the launcher
        private YggdrasillInterface Interface;

        // Pointers to any of the old functions we override.
        private IntPtr oldCreate;

        private DCreateDevice del_cd;

        private List<IDirectInputDevice8> devices = new List<IDirectInputDevice8>();

        #endregion

        #region Constructors

        // For the case where we already have a native IDirectInput8 object and we want to override some of it's functions.
        public unsafe IDirectInput8(DirectInput8.IDirectInput8* InNativeIDirectInput8, YggdrasillInterface iface)
        {
            Context = InNativeIDirectInput8;
            Interface = iface;

            /* Hook up any functions we care about */
            OverrideFunctions();
        }

        // For the case where we don't have a native IDirectInput8 object so we want one created for us.
        public unsafe IDirectInput8(IntPtr hInst, UInt32 dwVersion, IntPtr riidltf,
            DirectInput8.IDirectInput8** ppvOut, IntPtr punkOuter, YggdrasillInterface iface, out int rv)
        {
            // Create the real IDirectInput8 object.
            rv = DirectInput8.DirectInput8Create(hInst, dwVersion, riidltf, ppvOut, punkOuter);
            Context = *ppvOut;
            Interface = iface;

            /* Hook up any functions we care about */
            OverrideFunctions();
        }

        private void OverrideFunctions()
        {
            // List of functions in the IDirectInput8 interface:
            // 0:  STDMETHOD(QueryInterface)(THIS_ REFIID riid, LPVOID * ppvObj) PURE;
            // 1:  STDMETHOD_(ULONG,AddRef)(THIS) PURE;
            // 2:  STDMETHOD_(ULONG,Release)(THIS) PURE;
            // 3:  STDMETHOD(CreateDevice)(THIS_ REFGUID,LPDIRECTINPUTDEVICE8A *,LPUNKNOWN) PURE;
            // 4:  STDMETHOD(EnumDevices)(THIS_ DWORD,LPDIENUMDEVICESCALLBACKA,LPVOID,DWORD) PURE;
            // 5:  STDMETHOD(GetDeviceStatus)(THIS_ REFGUID) PURE;
            // 6:  STDMETHOD(RunControlPanel)(THIS_ HWND,DWORD) PURE;
            // 7:  STDMETHOD(Initialize)(THIS_ HINSTANCE,DWORD) PURE;
            // 8:  STDMETHOD(FindDevice)(THIS_ REFGUID,LPCSTR,LPGUID) PURE;
            // 9:  STDMETHOD(EnumDevicesBySemantics)(THIS_ LPCSTR,LPDIACTIONFORMATA,LPDIENUMDEVICESBYSEMANTICSCBA,LPVOID,DWORD) PURE;
            // 10: STDMETHOD(ConfigureDevices)(THIS_ LPDICONFIGUREDEVICESCALLBACK,LPDICONFIGUREDEVICESPARAMSA,DWORD,LPVOID) PURE;

            oldCreate = Context->vtable[3];
            del_cd = new DCreateDevice(CreateDevice);
            IntPtr ptr = Marshal.GetFunctionPointerForDelegate(del_cd);
            Context->vtable[3] = ptr;
        }

        #endregion

        #region IDirectInput8 Interface Function Implementations

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public unsafe delegate int DCreateDevice(DirectInput8.IDirectInput8* This, DirectInput8.GUID* rguid,
            IntPtr *lplpDirectInputDevice, IntPtr pUnkOuter);

        public unsafe int CreateDevice(DirectInput8.IDirectInput8* This, DirectInput8.GUID* rguid,
            IntPtr *lplpDirectInputDevice, IntPtr pUnkOuter)
        {
            DCreateDevice del = (DCreateDevice)Marshal.GetDelegateForFunctionPointer(oldCreate, typeof(DCreateDevice));
            int rv = del(This, rguid, lplpDirectInputDevice, pUnkOuter);

            IDirectInputDevice8 dev = new IDirectInputDevice8((DirectInput8.IDirectInputDevice8*)*lplpDirectInputDevice, Interface);

            devices.Add(dev);

            return rv;
        }

        #endregion
    }
}
