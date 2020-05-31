/*
    This file is part of PsoWindowResize
    Copyright (C) 2015 Tulio Gonçalves

    This program also contains parts of code that are part of Yggdrasill. 
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

using EasyHook;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Media;
using System.Reflection;
//using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Yggdrasill;
using SharpDX.DirectInput;

namespace PsoWindowSize
{

    public partial class frmResizer : Form
    {
        struct Ratio
        {
            public int W;
            public int H;
        }

        Ratio ratio = new Ratio();
        bool updatingValue = false;
        bool hasExePath = false;
        string exePath = string.Empty;
        string psoDir = string.Empty;
        uint ctrlFlagValue = 0;

        int ScreenWidth = 0;
        int ScreenHeight = 0;

        const int ERROR_DEVICE_NOT_CONNECTED = 0x48F;
        const int ERROR_SUCCESS = 0;

        frmPSO psoForm = null;
        IntPtr wnd = IntPtr.Zero;
        Process psoProc = null;
        private bool applyPatch = true;

        private Ratio GetRatio(int resW, int resH)
        {
            ratio = new Ratio();

            int gcd = GetGCD(resW, resH);

            ratio.W = resW / gcd;
            ratio.H = resH / gcd;

            return ratio;
        }

        public frmResizer()
        {
            InitializeComponent();
        }

        public int GetGCD(int num1, int num2)
        {
            while (num1 != num2)
            {
                if (num1 > num2)
                    num1 = num1 - num2;

                if (num2 > num1)
                    num2 = num2 - num1;
            }
            return num1;
        }

        private string GetProcessFileName(Process p)
        {
            StringBuilder strbld = new StringBuilder(1024);

            // Setting up the variable for the second argument for EnumProcessModules
            IntPtr[] hMods = new IntPtr[1024];

            GCHandle gch = GCHandle.Alloc(hMods, GCHandleType.Pinned); // Don't forget to free this later
            IntPtr pModules = gch.AddrOfPinnedObject();

            // Setting up the rest of the parameters for EnumProcessModules
            uint uiSize = (uint)(Marshal.SizeOf(typeof(IntPtr)) * (hMods.Length));
            uint cbNeeded = 0;
            if (WinAPI.EnumProcessModules(p.Handle, pModules, uiSize, out cbNeeded) == 1)
            {
                Int32 uiTotalNumberofModules = (Int32)(cbNeeded / (Marshal.SizeOf(typeof(IntPtr))));

                WinAPI.GetModuleFileNameEx(p.Handle, hMods[0], strbld, (uint)(strbld.Capacity));
            }

            // Must free the GCHandle object
            gch.Free();

            return strbld.ToString();
        }

        public static Bitmap PrintWindow(IntPtr hwnd)
        {
            RECT rc = new RECT();
            WinAPI.GetWindowRect(hwnd, ref rc);

            int rcWidth = rc.Right - rc.Left;
            int rcHeight = rc.Bottom - rc.Top;

            Bitmap bmp = new Bitmap(rcWidth, rcHeight, PixelFormat.Format24bppRgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();

            WinAPI.PrintWindow(hwnd, hdcBitmap, 0);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();

            return bmp;
        }

        public static Bitmap PrintWindowBlt(IntPtr hwnd)
        {
            RECT rc = new RECT();
            WinAPI.GetClientRect(hwnd, ref rc);

            int rcWidth = rc.Right - rc.Left;
            int rcHeight = rc.Bottom - rc.Top;

            if (rcHeight.Equals(0) || rcWidth.Equals(0))
            {
                return null;
            }

            Bitmap bmp = new Bitmap(rcWidth, rcHeight, PixelFormat.Format24bppRgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();

            IntPtr windowDC = WinAPI.GetDC(hwnd);

            WinAPI.BitBlt(hdcBitmap, 0, 0, bmp.Width, bmp.Height, windowDC, 0, 0, TernaryRasterOperations.SRCCOPY);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();

            WinAPI.ReleaseDC(hwnd, windowDC);

            return bmp;
        }

        //public void ChangeResolution(int iWidth, int iHeight)
        //{
        //    Screen screen = Screen.PrimaryScreen;

        //    DEVMODE dm = new DEVMODE();
        //    dm.dmDeviceName = new String(new char[32]);
        //    dm.dmFormName = new String(new char[32]);
        //    dm.dmSize = (short)Marshal.SizeOf(dm);

        //    if (0 != WinAPI.EnumDisplaySettings(null, WinAPI.ENUM_CURRENT_SETTINGS, ref dm))
        //    {

        //        dm.dmPelsWidth = iWidth;
        //        dm.dmPelsHeight = iHeight;

        //        int iRet = WinAPI.ChangeDisplaySettings(ref dm, WinAPI.CDS_TEST);

        //        if (iRet == WinAPI.DISP_CHANGE_FAILED)
        //        {
        //            MessageBox.Show("Unable to process your request");
        //            MessageBox.Show("Description: Unable To Process Your Request. Sorry For This Inconvenience.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            iRet = WinAPI.ChangeDisplaySettings(ref dm, WinAPI.CDS_UPDATEREGISTRY);

        //            switch (iRet)
        //            {
        //                case WinAPI.DISP_CHANGE_SUCCESSFUL:
        //                    {
        //                        break;

        //                        //successfull change
        //                    }
        //                case WinAPI.DISP_CHANGE_RESTART:
        //                    {

        //                        MessageBox.Show("Description: You Need To Reboot For The Change To Happen.\n If You Feel Any Problem After Rebooting Your Machine\nThen Try To Change Resolution In Safe Mode.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        break;
        //                        //windows 9x series you have to restart
        //                    }
        //                default:
        //                    {

        //                        MessageBox.Show("Description: Failed To Change The Resolution.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        break;
        //                        //failed to change
        //                    }
        //            }
        //        }

        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("PSO");
            if (processes.Length < 1)
            {
                MessageBox.Show("Error: PSO is not running.\r\nPlease open PSO before trying to resize it.", "PSO not running", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            RECT windowRect = new RECT();
            RECT clientRect = new RECT();
            int windowWidth = 0;
            int windowHeight = 0;
            int clientWidth = 0;
            int clientHeight = 0;

            WinAPI.GetWindowRect(processes[0].MainWindowHandle, ref windowRect);
            windowHeight = windowRect.Bottom - windowRect.Top;
            windowWidth = windowRect.Right - windowRect.Left;

            WinAPI.GetClientRect(processes[0].MainWindowHandle, ref clientRect);
            clientHeight = clientRect.Bottom - clientRect.Top;
            clientWidth = clientRect.Right - clientRect.Left;

            if (rdoPerfect.Checked)
            {
                //ChangeResolution(ScreenWidth, ScreenHeight);



                //WinAPI.SetWindowLong(processes[0].MainWindowHandle, nIndex.GWL_STYLE, dwNewLong.WS_OVERLAPPEDWINDOW);

                //WinAPI.SetWindowPos(processes[0].MainWindowHandle, HWND.Top, 0, 0, ((640 * (cboRatio.SelectedIndex + 1)) + (windowWidth - clientWidth)),
                //                                                                   ((480 * (cboRatio.SelectedIndex + 1)) + (windowHeight - clientHeight)), 
                //                                                                   SetWindowPosFlags.SHOWWINDOW);

                WinAPI.MoveWindow(processes[0].MainWindowHandle, windowRect.Left, windowRect.Top, ((640 * (cboRatio.SelectedIndex + 1)) + (windowWidth - clientWidth)),
                                                                                                  ((480 * (cboRatio.SelectedIndex + 1)) + (windowHeight - clientHeight)), true);
            }
            else if (rdoScreenHeight.Checked)
            {
                int desiredHeight = Screen.PrimaryScreen.Bounds.Height + (windowHeight - clientHeight);
                int desiredWidth = ((desiredHeight * 4) / 3) + (windowWidth - clientWidth);

                WinAPI.MoveWindow(processes[0].MainWindowHandle, windowRect.Left, windowRect.Top, desiredWidth, desiredHeight, true);
            }
            else
            {
                WinAPI.MoveWindow(processes[0].MainWindowHandle, windowRect.Left, windowRect.Top, ((int)txtW.Value + (windowWidth - clientWidth)),
                                                                                                  ((int)txtH.Value + (windowHeight - clientHeight)), true);
            }
        }

        private void UpdateRatio()
        {
            ratio = GetRatio((int)txtW.Value, (int)txtH.Value);
            lblRatio.Text = string.Format("Ratio: {0}:{1}", ratio.W, ratio.H);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\SonicTeam\PSOV2");
                psoDir = key.GetValue("Dir").ToString();
                ctrlFlagValue = Convert.ToUInt32(key.GetValue("CTRLFLAG1"));
                key.Close();
            }
            catch
            {
                MessageBox.Show("PSO is not installed properly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }

            ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            //chkAutoResize.Checked = true;
            //chkWindowed.Checked = true;
            //cboRatio.SelectedIndex = 0;
            cmdResize.Enabled = false;
            cmdScreenshot.Enabled = false;
            timer.Enabled = true;

            if (!rdoOnline.Checked)
            {
                rdoOffline.Checked = true;
            }

            switch (Properties.Settings.Default.ResizeMode)
            {
                case "PixelPerfect":
                    rdoPerfect.Checked = true;
                    break;
                case "ScreenHeight":
                    rdoScreenHeight.Checked = true;
                    break;
                case "Custom":
                    rdoCustom.Checked = true;
                    break;
                default:
                    break;
            }

            UpdateRatio();

            txtW.Value = Properties.Settings.Default.CustomWidth;
            txtH.Value = Properties.Settings.Default.CustomHeight;
            UpdateRatio();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("PSO");
            try
            {
                if (processes.Length > 0)
                {
                    if (!hasExePath)
                    {
                        exePath = GetProcessFileName(processes[0]);
                        hasExePath = true;
                    }

                    RECT clientRect = new RECT();
                    WinAPI.GetClientRect(processes[0].MainWindowHandle, ref clientRect);

                    if (chkKillPSO.Checked && clientRect.Right.Equals(0) 
                                           && clientRect.Bottom.Equals(0))
                    {
                        processes[0].Kill();
                    }

                    lblPsoStatus.Text = string.Format("PSO is running. Dimensions: {0} x {1}.", clientRect.Right, clientRect.Bottom);

                    if (rdoPerfect.Checked)
                    {
                        txtW.Value = clientRect.Right;
                        txtH.Value = clientRect.Bottom;
                        UpdateRatio();
                    }
                    cmdResize.Enabled = true;
                    cmdScreenshot.Enabled = true;
                    cmdStartOffline.Enabled = false;
                    cmdStartOnline.Enabled = false;
                    cmdOptions.Enabled = false;
                    cmdSerial.Enabled = false;
                    cmdLaunch.Enabled = false;
                    if (chkIpPatch.Checked)
                    {
                        txtServer.Enabled = false;
                    }
                    fraPatches.Enabled = false;

                    if (chkWindowed.Checked && chkAutoResize.Checked)
                    {
                        int desiredWidth = 0;
                        int desiredHeight = 0;
                        if (rdoPerfect.Checked)
                        {
                            int multiplier = cboRatio.SelectedIndex + 1;
                            desiredWidth = 640 * multiplier;
                            desiredHeight = 480 * multiplier;
                        }
                        else
                        {
                            desiredWidth = (int)txtW.Value;
                            desiredHeight = (int)txtH.Value;
                        }

                        if ((clientRect.Right != desiredWidth) || (clientRect.Bottom != desiredHeight))
                        {
                            button1_Click(sender, e);
                        }
                    }
                }
                else
                {
                    if (psoForm != null)
                    {
                        psoForm.Close();
                        psoForm.Dispose();
                    }

                    lblPsoStatus.Text = "PSO is NOT running.";

                    cmdResize.Enabled = false;
                    cmdScreenshot.Enabled = false;
                    cmdStartOffline.Enabled = true;
                    cmdStartOnline.Enabled = true;
                    cmdOptions.Enabled = true;
                    cmdSerial.Enabled = true;
                    cmdLaunch.Enabled = true;
                    if (chkIpPatch.Checked)
                    {
                        txtServer.Enabled = true;
                    }
                    fraPatches.Enabled = true;

                    hasExePath = false;
                }
            }
            catch
            {
                return;
            }
        }

        private void rdoCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCustom.Checked)
            {
                fraCustom.Enabled = true;
                cboRatio.Enabled = false;
                Properties.Settings.Default.ResizeMode = "Custom";
                Properties.Settings.Default.Save();
            }
        }

        private void rdoScreenHeight_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoScreenHeight.Checked)
            {
                cboRatio.Enabled = false;
                fraCustom.Enabled = false;
                Properties.Settings.Default.ResizeMode = "ScreenHeight";
                Properties.Settings.Default.Save();
            }
        }

        private void rdoPerfect_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPerfect.Checked)
            {
                cboRatio.Enabled = true;
                fraCustom.Enabled = false;
                Properties.Settings.Default.ResizeMode = "PixelPerfect";
                Properties.Settings.Default.Save();
            }
        }

        private void txtW_ValueChanged(object sender, EventArgs e)
        {
            if (!updatingValue)
            {
                updatingValue = true;
                if (chkLockRatio.Checked)
                {
                    txtH.Value = (int)(txtW.Value * ratio.H / ratio.W);
                    Properties.Settings.Default.CustomWidth = txtW.Value;
                    Properties.Settings.Default.CustomHeight = txtH.Value;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    UpdateRatio();
                }
                updatingValue = false;
            }
        }

        private void txtH_ValueChanged(object sender, EventArgs e)
        {
            if (!updatingValue)
            {
                updatingValue = true;
                if (chkLockRatio.Checked)
                {
                    txtW.Value = (int)(txtH.Value * ratio.W / ratio.H);
                    Properties.Settings.Default.CustomWidth = txtW.Value;
                    Properties.Settings.Default.CustomHeight = txtH.Value;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    UpdateRatio();
                }
                updatingValue = false;
            }
        }

        private void cmdScreenshot_Click(object sender, EventArgs e)
        {
            //            Process[] processes = Process.GetProcessesByName("PSO");

            string psoBasePath = exePath.Substring(0, exePath.LastIndexOf('\\'));

            FileInfo[] files = new DirectoryInfo(psoBasePath + "\\Backup").GetFiles("pso_image_???.bmp");

            int highest = -1;
            foreach (FileInfo file in files)
            {
                int number = int.Parse(file.Name.Substring(10, 3));
                highest = (number > highest) ? number : highest;
            }

            try
            {
                Bitmap bmp = PrintWindowBlt(wnd);//processes[0].MainWindowHandle);

                if (bmp.Width.Equals(640) && bmp.Height.Equals(480))
                {
                    bmp.Save(string.Format("{0}\\Backup\\pso_image_{1}.bmp", psoBasePath, (++highest).ToString("000")), ImageFormat.Bmp);
                }
                else
                {
                    Bitmap resizedBmp = new Bitmap(640, 480);
                    Graphics gx = Graphics.FromImage(resizedBmp);
                    ImageAttributes imgAtt = new ImageAttributes();

                    gx.InterpolationMode = InterpolationMode.NearestNeighbor;
                    gx.DrawImage(bmp, 0, 0, 640, 480);

                    resizedBmp.Save(string.Format("{0}\\Backup\\pso_image_{1}.bmp", psoBasePath, (++highest).ToString("000")), ImageFormat.Bmp);

                    gx.Dispose();
                    resizedBmp.Dispose();
                }
                bmp.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Screenshot saving failed.\r\n{0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                PrintWindow(wnd);//processes[0].MainWindowHandle); //This is a cheap trick just to flash the screen. 
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Screenshot flash failed.\r\n{0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Assembly assembly = Assembly.GetExecutingAssembly();
            SoundPlayer player = new SoundPlayer(assembly.GetManifestResourceStream("PsoWindowSize.05_26.wav"));
            player.Play();
        }

        private void cmdStartOnline_Click(object sender, EventArgs e)
        {
            timer.Stop();
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\SonicTeam\PSOV2", true);
            key.SetValue("CTRLFLAG1", (uint)0x0000001e, RegistryValueKind.DWord);
            key.Close();

            Directory.SetCurrentDirectory(psoDir);

            ProcessStartInfo psoProcessInfo = new ProcessStartInfo(psoDir + "\\pso.exe", "-online");
            psoProcessInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                Process.Start(psoProcessInfo);
            }
            catch
            {
                MessageBox.Show("Error: To launch the game, the launcher must be in the\r\nsame folder as \"pso.exe\".", "PSO not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            timer.Start();
        }

        private void cmdStartOffline_Click(object sender, EventArgs e)
        {
            timer.Stop();
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\SonicTeam\PSOV2", true);
            key.SetValue("CTRLFLAG1", (uint)0x0000000e, RegistryValueKind.DWord);
            key.Close();

            Directory.SetCurrentDirectory(psoDir);

            ProcessStartInfo psoProcessInfo = new ProcessStartInfo(psoDir + "\\pso.exe", "-offline");
            psoProcessInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                Process.Start(psoProcessInfo);
            }
            catch
            {
                MessageBox.Show("Error: To launch the game, the launcher must be in the\r\nsame folder as \"pso.exe\".", "PSO not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            timer.Start();
        }

        private void cmdOptions_Click(object sender, EventArgs e)
        {
            timer.Stop();

            Directory.SetCurrentDirectory(psoDir);

            ProcessStartInfo processInfo = new ProcessStartInfo(Path.Combine(psoDir, "option.exe"), "-autorun.exe -s");
            processInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                Process process = Process.Start(processInfo);
                process.WaitForExit();
            }
            catch
            {
                MessageBox.Show("Error: To launch the game, the launcher must be in the\r\nsame folder as \"options.exe\".", "Options not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            timer.Start();
        }

        public struct ThdArgs
        {
            public Process psoProc;
            public RegistryKey key;
            public UInt32 value;
        }

        private void cmdLaunch_Click(object sender, EventArgs e)
        {
            if (chkControllerCheck.Checked)
            {
                //Check if a controller is connected
                try
                {
                    XInputState controller = new XInputState();
                    if (XInput.XInputGetState(0, ref controller).Equals(ERROR_DEVICE_NOT_CONNECTED))
                    {
                        DirectInput dinput = new DirectInput();
                        IList<DeviceInstance> joysticks = dinput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices);
                        if (joysticks.Count == 0)
                        {
                            joysticks = dinput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices);

                            if (joysticks.Count == 0)
                            {
                                if (MessageBox.Show(this, "There are no controllers connected. Do you wish to start anyway?", "No controller found", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.No))
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Detecting controller");
                }
            }

            /* Don't try to start the game twice... */
            bool windowed = chkWindowed.Checked;

            uint value = 0;
            if (rdoOnline.Checked)
            {
                value = (uint)(ctrlFlagValue | 0x00000010);
            }
            else
            {
                value = (uint)(ctrlFlagValue & 0xffffffef);
            }


            /* Figure out the working directory to set and set the flag in the registry... */
            RegistryKey k = Registry.CurrentUser.OpenSubKey(@"Software\SonicTeam\PSOV2", true);
            k.SetValue("CTRLFLAG1", value, RegistryValueKind.DWord);
            k.Close();

            string dir = psoDir;

            Kernel32.STARTUPINFO si = new Kernel32.STARTUPINFO();
            Kernel32.PROCESS_INFORMATION pi = new Kernel32.PROCESS_INFORMATION();

            Directory.SetCurrentDirectory(dir);

            /* Get the icon from one of the exes that have an icon */
            System.Drawing.Icon icon = System.Drawing.Icon.ExtractAssociatedIcon("online.exe");

            /* Start PSO... */
            bool ran = Kernel32.CreateProcess(null, "pso.exe -online",
                IntPtr.Zero, IntPtr.Zero, true,
                (uint)Kernel32.ProcessCreationFlags.CREATE_SUSPENDED,
                IntPtr.Zero, dir, ref si, out pi);

            if (!ran)
            {
                Console.Out.WriteLine("CANNOT START PSO!");
                //k.SetValue("CTRLFLAG1", value, RegistryValueKind.DWord);
                return;
            }

            psoProc = Process.GetProcessById((int)pi.dwProcessId);

            if (!chkBypassPatch.Checked)
            {
                string ChannelName = null;
                RemoteHooking.IpcCreateServer<YggdrasillInterface>(ref ChannelName, WellKnownObjectMode.SingleCall);

                try
                {
                    RemoteHooking.Inject((int)pi.dwProcessId, Path.Combine(Application.StartupPath, "Mithos.dll"), null, ChannelName,
                    chkVista.Checked, windowed);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    psoProc.Kill();
                    return;
                }

                /* Set up for doing the stuff we need to to the binary... */
                ProcessHaxxor haxxor = new ProcessHaxxor(psoProc);

                Kernel32.ResumeThread(pi.hThread);

                Thread.Sleep(1000);

                /* Pause it while we hax it up. */
                foreach (ProcessThread pT in psoProc.Threads)
                {
                    IntPtr pOpenThread = Kernel32.OpenThread(Kernel32.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                    if (pOpenThread == IntPtr.Zero)
                    {
                        break;
                    }

                    Kernel32.SuspendThread(pOpenThread);
                }

                if (chkIpPatch.Checked)
                {
                    haxxor.PatchPSO(chkWhiteNames.Checked, chkWordFilter.Checked, chkMusicFix.Checked, chkMapFix.Checked, txtServer.Text.Trim());
                }
                else
                {
                    haxxor.PatchPSO(chkWhiteNames.Checked, chkWordFilter.Checked, chkMusicFix.Checked, chkMapFix.Checked, null);
                }
            }

            /* Wake it up. */
            foreach (ProcessThread pT in psoProc.Threads)
            {
                IntPtr pOpenThread = Kernel32.OpenThread(Kernel32.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }

                Kernel32.ResumeThread(pOpenThread);
            }

            if (windowed)
            {
                RECT windowRect = new RECT();
                RECT clientRect = new RECT();

                int windowHeight = 0;
                int windowWidth = 0;
                int clientHeight = 0;
                int clientWidth = 0;

                Thread.Sleep(750);
                wnd = User32.FindWindow("PSO for PC", "PSO for PC");

                WinAPI.GetWindowRect(wnd, ref windowRect);
                windowHeight = windowRect.Bottom - windowRect.Top;
                windowWidth = windowRect.Right - windowRect.Left;

                WinAPI.GetClientRect(wnd, ref clientRect);
                clientHeight = clientRect.Bottom - clientRect.Top;
                clientWidth = clientRect.Right - clientRect.Left;

                int startX = 0;
                int startY = 0;

                int desiredWidth = 0;
                int desiredHeight = 0;

                if (rdoPerfect.Checked)
                {
                    desiredWidth = (int)((640 * (cboRatio.SelectedIndex + 1)) + (windowWidth - clientWidth));
                    desiredHeight = (int)((480 * (cboRatio.SelectedIndex + 1)) + (windowHeight - clientHeight));
                }
                else if (rdoScreenHeight.Checked)
                {
                    desiredHeight = Screen.PrimaryScreen.Bounds.Height + (windowHeight - clientHeight);
                    desiredWidth = ((desiredHeight * 4) / 3) + (windowWidth - clientWidth);
                }
                else
                {
                    desiredWidth = (int)((int)txtW.Value + (windowWidth - clientWidth));
                    desiredHeight = (int)((int)txtH.Value + (windowHeight - clientHeight));
                }

                if (chkCenterWindow.Checked)
                {
                    startX = (ScreenWidth - desiredWidth) / 2;
                    startY = (Screen.PrimaryScreen.WorkingArea.Height - desiredHeight) / 2;
                }

                if (chkEmbedFullScreen.Checked)
                {
                    uint lStyle = WinAPI.GetWindowLong(wnd, nIndex.GWL_STYLE);
                    lStyle &= ~(dwNewLong.WS_CAPTION | dwNewLong.WS_THICKFRAME | dwNewLong.WS_MINIMIZE | dwNewLong.WS_MAXIMIZE | dwNewLong.WS_SYSMENU);
                    WinAPI.SetWindowLong(wnd, nIndex.GWL_STYLE, lStyle);


                    int wWidth = 640;
                    int wHeight = 480;

                    if (rdoPerfect.Checked)
                    {
                        wWidth *= (cboRatio.SelectedIndex + 1);
                        wHeight *= (cboRatio.SelectedIndex + 1);
                    }
                    else if (rdoScreenHeight.Checked)
                    {
                        wHeight = Screen.PrimaryScreen.Bounds.Height;
                        wWidth = (wHeight * 4) / 3;
                    }
                    else
                    {
                        wWidth = (int)txtW.Value;
                        wHeight = (int)txtH.Value;
                    }

                    psoForm = new frmPSO();
                    psoForm.TopMost = true;
                    psoForm.WindowState = FormWindowState.Normal;
                    psoForm.FormBorderStyle = FormBorderStyle.None;
                    psoForm.pnlPSO.Width = wWidth;
                    psoForm.pnlPSO.Height = wHeight;
                    psoForm.Bounds = Screen.PrimaryScreen.Bounds;
                    psoForm.PSOhWnd = wnd;

                    WinAPI.SetWindowPos(wnd, IntPtr.Zero, 0, 0, 0, 0, (SWP.NOSIZE | SWP.SHOWWINDOW));
                    WinAPI.MoveWindow(wnd, 0, 0, wWidth, wHeight, true);
                    WinAPI.SetParent(wnd, psoForm.pnlPSO.Handle);
                    //WinAPI.ShowWindow(wnd, WindowShowStyle.ShowNormal);

                    psoForm.Show();
                    psoForm.Activate();
                    WinAPI.SetForegroundWindow(wnd);
                }
                else
                {
                    User32.SetWindowPos(wnd, IntPtr.Zero, startX, startY, desiredWidth, desiredHeight, 2);
                    WinAPI.MoveWindow(wnd, startX, startY, desiredWidth, desiredHeight, true);

                    User32.SendMessage(wnd, User32.WM_SETICON, User32.ICON_BIG, icon.Handle);
                }
            }

            Properties.Settings.Default.Save();

            /* Make a thread to wait until the game exits to clean up. */
            ThdArgs args = new ThdArgs();
            args.psoProc = psoProc;
            args.key = k;
            args.value = value;
            Thread thd = new Thread(this.WaitThd);
            thd.Start(args);

        }

        private void WaitThd(object thdArgs)
        {
            ThdArgs args = (ThdArgs)thdArgs;

            args.psoProc.WaitForExit();
            args.key = Registry.CurrentUser.OpenSubKey(@"Software\SonicTeam\PSOV2", true);
            args.key.SetValue("CTRLFLAG1", args.value, RegistryValueKind.DWord);
            args.key.Close();
        }


        private void cmdSerial_Click(object sender, EventArgs e)
        {
            frmSerial serialForm = new frmSerial();
            serialForm.ShowDialog();
            serialForm.Dispose();
        }

        private void frmResizer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (psoForm != null && psoProc != null)
            {
                if (!psoProc.HasExited)
                {
                    psoProc.Kill();
                }
            }


            RegistryKey k = Registry.CurrentUser.OpenSubKey(@"Software\SonicTeam\PSOV2", true);
            k.SetValue("CTRLFLAG1", ctrlFlagValue, RegistryValueKind.DWord);
            k.Close();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Application.Exit();
        }

        private void chkVista_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void chkWhiteNames_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void chkMusicFix_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void chkMapFix_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void chkWordFilter_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void chkAutoResize_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void chkCenterWindow_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void chkLockRatio_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void chkWindowed_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void cboRatio_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void txtW_Leave(object sender, EventArgs e)
        {
        }

        private void txtH_Leave(object sender, EventArgs e)
        {
        }

        private void chkWindowed_CheckedChanged(object sender, EventArgs e)
        {
            fraWindowed.Enabled = ((CheckBox)sender).Checked;
        }

        private void chkIpPatch_CheckedChanged(object sender, EventArgs e)
        {
            txtServer.Enabled = ((CheckBox)sender).Checked;
        }

        private void chkIpPatch_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmPSO psoForm = new frmPSO();
            psoForm.WindowState = FormWindowState.Normal;
            psoForm.FormBorderStyle = FormBorderStyle.None;
            psoForm.Bounds = Screen.PrimaryScreen.Bounds;
            psoForm.Show();
        }

        private void chkEmbedFullScreen_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void cmdKillPSO_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("PSO");
            try
            {
                if (processes.Length > 0)
                {
                    if (!hasExePath)
                    {
                        exePath = GetProcessFileName(processes[0]);
                        hasExePath = true;
                    }
                }
            }
            catch { }
            processes[0].Kill();
        }
    }
}
