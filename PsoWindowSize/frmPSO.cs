using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PsoWindowSize
{
    public partial class frmPSO : Form
    {
        public frmPSO()
        {
            InitializeComponent();
        }

        public IntPtr PSOhWnd
        {
            get;
            set;
        }

        private void frmPSO_Resize(object sender, EventArgs e)
        {
            if (this.Width > pnlPSO.Width)
            {
                pnlPSO.Left = (this.ClientRectangle.Width - pnlPSO.Width) / 2;
            }
            else
            {
                pnlPSO.Left = 0;
            }
            if (this.Height > pnlPSO.Height)
            {
                pnlPSO.Top = (this.ClientRectangle.Height - pnlPSO.Height) / 2;
            }
            else
            {
                pnlPSO.Top = 0;
            }
        }

        private void frmPSO_Activated(object sender, EventArgs e)
        {
            if (!(PSOhWnd == null || PSOhWnd.Equals(IntPtr.Zero)))
            {
                this.BringToFront();
                WinAPI.SetForegroundWindow(PSOhWnd);
            }
        }
    }
}
