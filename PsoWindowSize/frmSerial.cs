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
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PsoWindowSize
{
    public partial class frmSerial : Form
    {


        public frmSerial()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            string serialText = txtSerial.Text;
            string accessText = txtAccessKey.Text;
            string emailText = txtEmail.Text;
            UInt32 nserial;

            try
            {
                nserial = UInt32.Parse(txtSerial.Text, NumberStyles.None);
            }
            catch
            {
                // Invalid serial.
                MessageBox.Show("Invalid Serial Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (accessText.Length != 8)
            {
                // Invalid access key
                MessageBox.Show("Invalid Access Key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int atIdx = emailText.IndexOf('@');

            if (emailText.Length < 3 || atIdx == -1 || atIdx == 0 || atIdx == emailText.Length - 1 || emailText.Length > 64)
            {
                // Invalid Email address
                MessageBox.Show("Invalid E-Mail Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            /* Write the data back to the registry. */
            RegistryKey k = Registry.CurrentUser.OpenSubKey("Software\\SonicTeam\\PSOV2", true);
            byte[] key = { 0x4a, 0x43, 0x0a, 0x13, 0x5e, 0x6f, 0x58, 0x5b,
                           0x46, 0x18, 0x25, 0x51, 0x60, 0x15, 0x7d, 0x64,
                           0x0b, 0x71, 0x0d, 0x1e, 0x7c, 0x27, 0x43, 0x7e,
                           0x10, 0x2c, 0x4f, 0x15, 0x31, 0x32, 0x04, 0x40,
                           0x51, 0x21, 0x4d, 0x63, 0x6b, 0x4a, 0x6e, 0x7e,
                           0x62, 0x56, 0x49, 0x16, 0x1c, 0x07, 0x1f, 0x01,
                           0x16, 0x03, 0x5c, 0x72, 0x0b, 0x06, 0x30, 0x0a,
                           0x72, 0x69, 0x46, 0x7b, 0x04, 0x0e, 0x6d, 0x48 };
            string serialStr = nserial.ToString("X");

            while (serialStr.Length < 8)
            {
                serialStr = "0" + serialStr;
            }

            byte[] cookedSerial = Encoding.ASCII.GetBytes(serialStr);
            byte[] cookedAccess = Encoding.ASCII.GetBytes(accessText);
            byte[] cookedEmail = Encoding.ASCII.GetBytes(emailText);
            byte[] serial = new byte[8], access = new byte[8], email = new byte[64];
            int i;

            if (cookedSerial.Length != 8)
            {
                // Invalid serial
                MessageBox.Show("Invalid Serial Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cookedAccess.Length != 8)
            {
                // Invalid access key
                MessageBox.Show("Invalid Access Key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cookedEmail.Length < 3 || cookedEmail.Length > 64)
            {
                // Invalid Email address
                MessageBox.Show("Invalid E-Mail Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (i = 0; i < 8; ++i)
            {
                if (cookedSerial[i] < 0x30 || cookedSerial[i] > 0x46 ||
                    (cookedSerial[i] > 0x39 && cookedSerial[i] < 0x41))
                {
                    MessageBox.Show("Invalid Serial Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                serial[i] = (byte)(cookedSerial[i] ^ key[i]);
            }

            for (i = 0; i < 8; ++i)
            {
                if (cookedAccess[i] < 0x30 || cookedAccess[i] > 0x7a ||
                    (cookedAccess[i] > 0x39 && cookedAccess[i] < 0x41) ||
                    (cookedAccess[i] > 0x5a && cookedAccess[i] < 0x61))
                {
                    MessageBox.Show("Invalid Access Key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                access[i] = (byte)(cookedAccess[i] ^ key[i]);
            }

            for (i = 0; i < emailText.Length && i < 64; ++i)
            {
                email[i] = (byte)(cookedEmail[i] ^ key[i]);
            }

            /* Fail, Sega. */
            for (; i < 64; ++i)
            {
                email[i] = key[i];
            }

            k.SetValue("SERIAL", serial);
            k.SetValue("ACCESS", access);
            k.SetValue("E-MAIL", email);

            this.Close();

        }

        private void frmSerial_Load(object sender, EventArgs e)
        {
            RegistryKey k = Registry.CurrentUser.OpenSubKey("Software\\SonicTeam\\PSOV2", false);
            byte[] key = { 0x4a, 0x43, 0x0a, 0x13, 0x5e, 0x6f, 0x58, 0x5b,
                           0x46, 0x18, 0x25, 0x51, 0x60, 0x15, 0x7d, 0x64,
                           0x0b, 0x71, 0x0d, 0x1e, 0x7c, 0x27, 0x43, 0x7e,
                           0x10, 0x2c, 0x4f, 0x15, 0x31, 0x32, 0x04, 0x40,
                           0x51, 0x21, 0x4d, 0x63, 0x6b, 0x4a, 0x6e, 0x7e,
                           0x62, 0x56, 0x49, 0x16, 0x1c, 0x07, 0x1f, 0x01,
                           0x16, 0x03, 0x5c, 0x72, 0x0b, 0x06, 0x30, 0x0a,
                           0x72, 0x69, 0x46, 0x7b, 0x04, 0x0e, 0x6d, 0x48 };
            byte[] rawSerial = (byte[])k.GetValue("SERIAL");
            byte[] rawAccess = (byte[])k.GetValue("ACCESS");
            byte[] rawEmail = (byte[])k.GetValue("E-MAIL");
            byte[] serial = new byte[8], access = new byte[8], email = new byte[64];
            int i, emailLen = 64;
            UInt32 nserial;

            if (rawSerial != null && rawSerial.Length == 8)
            {
                for (i = 0; i < 8; ++i)
                {
                    serial[i] = (byte)(rawSerial[i] ^ key[i]);
                }
            }

            if (rawAccess != null && rawAccess.Length == 8)
            {
                for (i = 0; i < 8; ++i)
                {
                    access[i] = (byte)(rawAccess[i] ^ key[i]);
                }
            }

            if (rawEmail != null && rawEmail.Length != 0)
            {
                for (i = 0; i < rawEmail.Length && i < 64; ++i)
                {
                    email[i] = (byte)(rawEmail[i] ^ key[i]);
                    if (email[i] == 0)
                    {
                        emailLen = i;
                        break;
                    }
                }
            }

            if (UInt32.TryParse(Encoding.ASCII.GetString(serial), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out nserial))
            {
                txtSerial.Text = nserial.ToString();
                txtAccessKey.Text = Encoding.ASCII.GetString(access);
                txtEmail.Text = Encoding.ASCII.GetString(email, 0, emailLen);
            }

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
