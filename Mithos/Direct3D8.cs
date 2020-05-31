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
using System.Runtime.InteropServices;
using Yggdrasill;

/* For some unknown reason, I can't seem to get DirectX and DirectInput to
 * hook the same way. At some point, I'd like to clean up the Direct3D hook
 * code here to look more like the DirectInput one, but for now this will
 * have to do. */
namespace Sylverant
{
    public unsafe class IDirect3D8 : IDisposable
    {
        #region Variables

        // A pointer to the native IDirect3D8 object that we are providing overrides for.
        public D3D8.IDirect3D8* Context
        {
            get;
            private set;
        }

        // A pointer to the original array of virtual functions.  We keep this around so we can call the originals.
        private IntPtr* vtable = null;

        // Our link back to the launcher
        private YggdrasillInterface Interface;

        private DCreateDevice del_cd;

        #endregion

        #region Constructors

        // For the case where we already have a native IDirect3D8 object and we want to override some of it's functions.
        public unsafe IDirect3D8(D3D8.IDirect3D8* InContext, YggdrasillInterface iface)
        {
            Context = InContext;
            Interface = iface;

            // Override the functions in the vtable of the context with our own.
            OverrideFunctions();
        }

        // For the case where we don't have a native IDirect3D object so we want one created for us.
        public IDirect3D8(uint SdkVersion, YggdrasillInterface iface)
        {
            // Create the real IDirect3D8 object.
            Context = D3D8.Direct3DCreate8(SdkVersion);
            Interface = iface;

            // Override the functions in the vtable of the context with our own.
            OverrideFunctions();
        }

        #endregion

        #region Destructors/Disposal methods

        ~IDirect3D8()
        {
            Dispose(true);
        }

        public void Dispose()
        {
            Dispose(false);
        }

        // Cleanup resources.  Destructing == true means we are getting garbage collected so don't reference any managed resources.
        private void Dispose(bool Destructing)
        {
            if (vtable != null)
            {
                Kernel32.HeapFree(Kernel32.GetProcessHeap(), 0, *vtable);
                vtable = null;
            }
        }

        #endregion

        #region Vtable Management

        // Backup the original native vtable and overwrite the pointer to it with our own (which is a copy of the original).
        private void InitVtable()
        {
            // If we don't have a real IDirect3D8 object yet then do nothing.
            if (Context == null) return;
            
            // Save off the original vtable (only if it really is the original).
            if (vtable == null) vtable = Context->vtable;
            
            // IDirect3D8 has 17 members.
            UInt32 vtblen = 17;
            // Allocate space for our new vtable.
            IntPtr* nvtable = (IntPtr*)Kernel32.HeapAlloc(Kernel32.GetProcessHeap(), 0, (UIntPtr)(vtblen * sizeof(IntPtr)));
            
            // Copy all of the original function pointers into our new vtable.
            for (int i = 0; i < vtblen; i++)
            {
                nvtable[i] = vtable[i];
            }
            
            // Set the Real IDirect3D8 implementation's vtable to point at our custom one.
            Context->vtable = nvtable;
        }

        private void OverrideFunctions()
        {
            InitVtable();

            // 0:  STDMETHOD(QueryInterface)(THIS_ REFIID riid, void** ppvObj) PURE;
            // 1:  STDMETHOD_(ULONG,AddRef)(THIS) PURE;
            // 2:  STDMETHOD_(ULONG,Release)(THIS) PURE;
            // 3:  STDMETHOD(RegisterSoftwareDevice)(THIS_ void* pInitializeFunction) PURE;
            // 4:  STDMETHOD_(UINT, GetAdapterCount)(THIS) PURE;
            // 5:  STDMETHOD(GetAdapterIdentifier)(THIS_ UINT Adapter,DWORD Flags,D3DADAPTER_IDENTIFIER8* pIdentifier) PURE;
            // 6:  STDMETHOD_(UINT, GetAdapterModeCount)(THIS_ UINT Adapter) PURE;
            // 7:  STDMETHOD(EnumAdapterModes)(THIS_ UINT Adapter,UINT Mode,D3DDISPLAYMODE* pMode) PURE;
            // 8:  STDMETHOD(GetAdapterDisplayMode)(THIS_ UINT Adapter,D3DDISPLAYMODE* pMode) PURE;
            // 9:  STDMETHOD(CheckDeviceType)(THIS_ UINT Adapter,D3DDEVTYPE CheckType,D3DFORMAT DisplayFormat,D3DFORMAT BackBufferFormat,BOOL Windowed) PURE;
            // 10: STDMETHOD(CheckDeviceFormat)(THIS_ UINT Adapter,D3DDEVTYPE DeviceType,D3DFORMAT AdapterFormat,DWORD Usage,D3DRESOURCETYPE RType,D3DFORMAT CheckFormat) PURE;
            // 11: STDMETHOD(CheckDeviceMultiSampleType)(THIS_ UINT Adapter,D3DDEVTYPE DeviceType,D3DFORMAT SurfaceFormat,BOOL Windowed,D3DMULTISAMPLE_TYPE MultiSampleType) PURE;
            // 12: STDMETHOD(CheckDepthStencilMatch)(THIS_ UINT Adapter,D3DDEVTYPE DeviceType,D3DFORMAT AdapterFormat,D3DFORMAT RenderTargetFormat,D3DFORMAT DepthStencilFormat) PURE;
            // 13: STDMETHOD(GetDeviceCaps)(THIS_ UINT Adapter,D3DDEVTYPE DeviceType,D3DCAPS8* pCaps) PURE;
            // 14: STDMETHOD_(HMONITOR, GetAdapterMonitor)(THIS_ UINT Adapter) PURE;
            // 15: STDMETHOD(CreateDevice)(THIS_ UINT Adapter,D3DDEVTYPE DeviceType,HWND hFocusWindow,DWORD BehaviorFlags,D3DPRESENT_PARAMETERS* pPresentationParameters,IDirect3DDevice8** ppReturnedDeviceInterface) PURE;
            
            del_cd = new DCreateDevice(CreateDevice);
            IntPtr ptr = Marshal.GetFunctionPointerForDelegate(del_cd);
            Context->vtable[15] = ptr;
        }

        #endregion

        #region IDirect3D8 Interface Function Implementations

        public unsafe delegate int DCreateDevice(D3D8.IDirect3D8* This, uint Adapter, uint DeviceType, IntPtr hFocusWindow,
            UInt32 BehaviorFlags, D3D8.D3DPRESENT_PARAMETERS* pPresentationParameters, IntPtr ppReturnedDeviceInterface);

        public unsafe int CreateDevice(D3D8.IDirect3D8* This, uint Adapter, uint DeviceType, IntPtr hFocusWindow,
            UInt32 BehaviorFlags, D3D8.D3DPRESENT_PARAMETERS* pPresentationParameters, IntPtr ppReturnedDeviceInterface)
        {
            DCreateDevice del = (DCreateDevice)Marshal.GetDelegateForFunctionPointer(vtable[15], typeof(DCreateDevice));

            /* I want windowed mode, damn you! */
            pPresentationParameters->hDeviceWindow = (IntPtr)1;
            pPresentationParameters->FullScreen_RefreshRateInHz = 0;

            return del(This, Adapter, DeviceType, hFocusWindow, BehaviorFlags, pPresentationParameters, ppReturnedDeviceInterface);
        }

        #endregion
    }
}
