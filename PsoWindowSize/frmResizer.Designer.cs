namespace PsoWindowSize
{
    partial class frmResizer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResizer));
            this.cmdResize = new System.Windows.Forms.Button();
            this.fraWindowed = new System.Windows.Forms.GroupBox();
            this.rdoCustom = new System.Windows.Forms.RadioButton();
            this.rdoScreenHeight = new System.Windows.Forms.RadioButton();
            this.fraCustom = new System.Windows.Forms.GroupBox();
            this.lblRatio = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtH = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtW = new System.Windows.Forms.NumericUpDown();
            this.rdoPerfect = new System.Windows.Forms.RadioButton();
            this.lblPsoStatus = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdOptions = new System.Windows.Forms.Button();
            this.cmdSerial = new System.Windows.Forms.Button();
            this.cmdScreenshot = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoOffline = new System.Windows.Forms.RadioButton();
            this.cmdLaunch = new System.Windows.Forms.Button();
            this.cmdStartOffline = new System.Windows.Forms.Button();
            this.cmdStartOnline = new System.Windows.Forms.Button();
            this.fraPatches = new System.Windows.Forms.GroupBox();
            this.cmdExit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chkBypassPatch = new System.Windows.Forms.CheckBox();
            this.cmdKillPSO = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkKillPSO = new System.Windows.Forms.CheckBox();
            this.chkControllerCheck = new System.Windows.Forms.CheckBox();
            this.chkIpPatch = new System.Windows.Forms.CheckBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.chkVista = new System.Windows.Forms.CheckBox();
            this.chkWhiteNames = new System.Windows.Forms.CheckBox();
            this.chkMusicFix = new System.Windows.Forms.CheckBox();
            this.chkWordFilter = new System.Windows.Forms.CheckBox();
            this.chkMapFix = new System.Windows.Forms.CheckBox();
            this.chkWindowed = new System.Windows.Forms.CheckBox();
            this.rdoOnline = new System.Windows.Forms.RadioButton();
            this.chkEmbedFullScreen = new System.Windows.Forms.CheckBox();
            this.chkCenterWindow = new System.Windows.Forms.CheckBox();
            this.chkAutoResize = new System.Windows.Forms.CheckBox();
            this.chkLockRatio = new System.Windows.Forms.CheckBox();
            this.cboRatio = new System.Windows.Forms.ComboBox();
            this.fraWindowed.SuspendLayout();
            this.fraCustom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtW)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.fraPatches.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdResize
            // 
            this.cmdResize.Location = new System.Drawing.Point(314, 338);
            this.cmdResize.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdResize.Name = "cmdResize";
            this.cmdResize.Size = new System.Drawing.Size(150, 44);
            this.cmdResize.TabIndex = 7;
            this.cmdResize.Text = "Resize PSO Window";
            this.cmdResize.UseVisualStyleBackColor = true;
            this.cmdResize.Click += new System.EventHandler(this.button1_Click);
            // 
            // fraWindowed
            // 
            this.fraWindowed.Controls.Add(this.rdoCustom);
            this.fraWindowed.Controls.Add(this.rdoScreenHeight);
            this.fraWindowed.Controls.Add(this.chkEmbedFullScreen);
            this.fraWindowed.Controls.Add(this.chkCenterWindow);
            this.fraWindowed.Controls.Add(this.chkAutoResize);
            this.fraWindowed.Controls.Add(this.fraCustom);
            this.fraWindowed.Controls.Add(this.rdoPerfect);
            this.fraWindowed.Controls.Add(this.cboRatio);
            this.fraWindowed.Controls.Add(this.cmdResize);
            this.fraWindowed.Location = new System.Drawing.Point(308, 23);
            this.fraWindowed.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.fraWindowed.Name = "fraWindowed";
            this.fraWindowed.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.fraWindowed.Size = new System.Drawing.Size(482, 400);
            this.fraWindowed.TabIndex = 2;
            this.fraWindowed.TabStop = false;
            // 
            // rdoCustom
            // 
            this.rdoCustom.AutoSize = true;
            this.rdoCustom.Location = new System.Drawing.Point(12, 125);
            this.rdoCustom.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rdoCustom.Name = "rdoCustom";
            this.rdoCustom.Size = new System.Drawing.Size(164, 29);
            this.rdoCustom.TabIndex = 7;
            this.rdoCustom.TabStop = true;
            this.rdoCustom.Text = "Custom Size";
            this.rdoCustom.UseVisualStyleBackColor = true;
            this.rdoCustom.CheckedChanged += new System.EventHandler(this.rdoCustom_CheckedChanged);
            // 
            // rdoScreenHeight
            // 
            this.rdoScreenHeight.AutoSize = true;
            this.rdoScreenHeight.Location = new System.Drawing.Point(12, 81);
            this.rdoScreenHeight.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rdoScreenHeight.Name = "rdoScreenHeight";
            this.rdoScreenHeight.Size = new System.Drawing.Size(423, 29);
            this.rdoScreenHeight.TabIndex = 6;
            this.rdoScreenHeight.TabStop = true;
            this.rdoScreenHeight.Text = "4:3 Screen Height (Good for full screen)";
            this.rdoScreenHeight.UseVisualStyleBackColor = true;
            this.rdoScreenHeight.CheckedChanged += new System.EventHandler(this.rdoScreenHeight_CheckedChanged);
            // 
            // fraCustom
            // 
            this.fraCustom.Controls.Add(this.chkLockRatio);
            this.fraCustom.Controls.Add(this.lblRatio);
            this.fraCustom.Controls.Add(this.label2);
            this.fraCustom.Controls.Add(this.txtH);
            this.fraCustom.Controls.Add(this.label1);
            this.fraCustom.Controls.Add(this.txtW);
            this.fraCustom.Enabled = false;
            this.fraCustom.Location = new System.Drawing.Point(12, 133);
            this.fraCustom.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.fraCustom.Name = "fraCustom";
            this.fraCustom.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.fraCustom.Size = new System.Drawing.Size(454, 158);
            this.fraCustom.TabIndex = 7;
            this.fraCustom.TabStop = false;
            // 
            // lblRatio
            // 
            this.lblRatio.AutoSize = true;
            this.lblRatio.Location = new System.Drawing.Point(256, 81);
            this.lblRatio.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblRatio.Name = "lblRatio";
            this.lblRatio.Size = new System.Drawing.Size(74, 25);
            this.lblRatio.TabIndex = 8;
            this.lblRatio.Text = "Ratio: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Dimensions:";
            // 
            // txtH
            // 
            this.txtH.Location = new System.Drawing.Point(346, 37);
            this.txtH.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtH.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.txtH.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtH.Name = "txtH";
            this.txtH.Size = new System.Drawing.Size(96, 31);
            this.txtH.TabIndex = 8;
            this.txtH.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.txtH.ValueChanged += new System.EventHandler(this.txtH_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(316, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "x";
            // 
            // txtW
            // 
            this.txtW.Location = new System.Drawing.Point(212, 37);
            this.txtW.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtW.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.txtW.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtW.Name = "txtW";
            this.txtW.Size = new System.Drawing.Size(96, 31);
            this.txtW.TabIndex = 7;
            this.txtW.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.txtW.ValueChanged += new System.EventHandler(this.txtW_ValueChanged);
            // 
            // rdoPerfect
            // 
            this.rdoPerfect.AutoSize = true;
            this.rdoPerfect.Checked = true;
            this.rdoPerfect.Location = new System.Drawing.Point(12, 37);
            this.rdoPerfect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rdoPerfect.Name = "rdoPerfect";
            this.rdoPerfect.Size = new System.Drawing.Size(200, 29);
            this.rdoPerfect.TabIndex = 4;
            this.rdoPerfect.TabStop = true;
            this.rdoPerfect.Text = "4:3 Pixel Perfect";
            this.rdoPerfect.UseVisualStyleBackColor = true;
            this.rdoPerfect.CheckedChanged += new System.EventHandler(this.rdoPerfect_CheckedChanged);
            // 
            // lblPsoStatus
            // 
            this.lblPsoStatus.AutoSize = true;
            this.lblPsoStatus.Location = new System.Drawing.Point(36, 762);
            this.lblPsoStatus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPsoStatus.Name = "lblPsoStatus";
            this.lblPsoStatus.Size = new System.Drawing.Size(206, 25);
            this.lblPsoStatus.TabIndex = 8;
            this.lblPsoStatus.Text = "PSO is NOT running";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdOptions);
            this.groupBox2.Controls.Add(this.cmdSerial);
            this.groupBox2.Controls.Add(this.cmdScreenshot);
            this.groupBox2.Location = new System.Drawing.Point(24, 192);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Size = new System.Drawing.Size(272, 231);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tools";
            // 
            // cmdOptions
            // 
            this.cmdOptions.Location = new System.Drawing.Point(36, 100);
            this.cmdOptions.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdOptions.Name = "cmdOptions";
            this.cmdOptions.Size = new System.Drawing.Size(198, 44);
            this.cmdOptions.TabIndex = 13;
            this.cmdOptions.Text = "PSO Options";
            this.cmdOptions.UseVisualStyleBackColor = true;
            this.cmdOptions.Click += new System.EventHandler(this.cmdOptions_Click);
            // 
            // cmdSerial
            // 
            this.cmdSerial.Location = new System.Drawing.Point(36, 156);
            this.cmdSerial.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdSerial.Name = "cmdSerial";
            this.cmdSerial.Size = new System.Drawing.Size(198, 44);
            this.cmdSerial.TabIndex = 14;
            this.cmdSerial.Text = "Manage Serials";
            this.cmdSerial.UseVisualStyleBackColor = true;
            this.cmdSerial.Click += new System.EventHandler(this.cmdSerial_Click);
            // 
            // cmdScreenshot
            // 
            this.cmdScreenshot.Location = new System.Drawing.Point(36, 42);
            this.cmdScreenshot.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdScreenshot.Name = "cmdScreenshot";
            this.cmdScreenshot.Size = new System.Drawing.Size(198, 44);
            this.cmdScreenshot.TabIndex = 12;
            this.cmdScreenshot.Text = "Screenshot";
            this.cmdScreenshot.UseVisualStyleBackColor = true;
            this.cmdScreenshot.Click += new System.EventHandler(this.cmdScreenshot_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoOffline);
            this.groupBox3.Controls.Add(this.rdoOnline);
            this.groupBox3.Controls.Add(this.cmdLaunch);
            this.groupBox3.Location = new System.Drawing.Point(24, 23);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Size = new System.Drawing.Size(272, 158);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Launcher";
            // 
            // rdoOffline
            // 
            this.rdoOffline.AutoSize = true;
            this.rdoOffline.Location = new System.Drawing.Point(150, 42);
            this.rdoOffline.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rdoOffline.Name = "rdoOffline";
            this.rdoOffline.Size = new System.Drawing.Size(105, 29);
            this.rdoOffline.TabIndex = 1;
            this.rdoOffline.Text = "Offline";
            this.rdoOffline.UseVisualStyleBackColor = true;
            // 
            // cmdLaunch
            // 
            this.cmdLaunch.Location = new System.Drawing.Point(36, 87);
            this.cmdLaunch.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdLaunch.Name = "cmdLaunch";
            this.cmdLaunch.Size = new System.Drawing.Size(198, 44);
            this.cmdLaunch.TabIndex = 2;
            this.cmdLaunch.Text = "Launch";
            this.cmdLaunch.UseVisualStyleBackColor = true;
            this.cmdLaunch.Click += new System.EventHandler(this.cmdLaunch_Click);
            // 
            // cmdStartOffline
            // 
            this.cmdStartOffline.Enabled = false;
            this.cmdStartOffline.Location = new System.Drawing.Point(480, 902);
            this.cmdStartOffline.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdStartOffline.Name = "cmdStartOffline";
            this.cmdStartOffline.Size = new System.Drawing.Size(150, 44);
            this.cmdStartOffline.TabIndex = 1;
            this.cmdStartOffline.Text = "Offline";
            this.cmdStartOffline.UseVisualStyleBackColor = true;
            this.cmdStartOffline.Click += new System.EventHandler(this.cmdStartOffline_Click);
            // 
            // cmdStartOnline
            // 
            this.cmdStartOnline.Enabled = false;
            this.cmdStartOnline.Location = new System.Drawing.Point(134, 902);
            this.cmdStartOnline.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdStartOnline.Name = "cmdStartOnline";
            this.cmdStartOnline.Size = new System.Drawing.Size(150, 44);
            this.cmdStartOnline.TabIndex = 0;
            this.cmdStartOnline.Text = "Online";
            this.cmdStartOnline.UseVisualStyleBackColor = true;
            this.cmdStartOnline.Click += new System.EventHandler(this.cmdStartOnline_Click);
            // 
            // fraPatches
            // 
            this.fraPatches.Controls.Add(this.chkIpPatch);
            this.fraPatches.Controls.Add(this.txtServer);
            this.fraPatches.Controls.Add(this.chkVista);
            this.fraPatches.Controls.Add(this.chkWhiteNames);
            this.fraPatches.Controls.Add(this.chkMusicFix);
            this.fraPatches.Controls.Add(this.chkWordFilter);
            this.fraPatches.Controls.Add(this.chkMapFix);
            this.fraPatches.Location = new System.Drawing.Point(24, 435);
            this.fraPatches.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.fraPatches.Name = "fraPatches";
            this.fraPatches.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.fraPatches.Size = new System.Drawing.Size(766, 177);
            this.fraPatches.TabIndex = 17;
            this.fraPatches.TabStop = false;
            this.fraPatches.Text = "Run-time Patches";
            // 
            // cmdExit
            // 
            this.cmdExit.Location = new System.Drawing.Point(336, 794);
            this.cmdExit.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(150, 44);
            this.cmdExit.TabIndex = 20;
            this.cmdExit.Text = "Exit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(676, 902);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 83);
            this.button1.TabIndex = 21;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // chkBypassPatch
            // 
            this.chkBypassPatch.Checked = global::PsoWindowSize.Properties.Settings.Default.BypassPatch;
            this.chkBypassPatch.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "BypassPatch", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkBypassPatch.Location = new System.Drawing.Point(368, 21);
            this.chkBypassPatch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkBypassPatch.Name = "chkBypassPatch";
            this.chkBypassPatch.Size = new System.Drawing.Size(388, 63);
            this.chkBypassPatch.TabIndex = 22;
            this.chkBypassPatch.Text = "Bypass all patches \r\n(Use when running a patched EXE)";
            this.chkBypassPatch.UseVisualStyleBackColor = true;
            // 
            // cmdKillPSO
            // 
            this.cmdKillPSO.Location = new System.Drawing.Point(134, 958);
            this.cmdKillPSO.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdKillPSO.Name = "cmdKillPSO";
            this.cmdKillPSO.Size = new System.Drawing.Size(150, 44);
            this.cmdKillPSO.TabIndex = 20;
            this.cmdKillPSO.Text = "Kill PSO";
            this.cmdKillPSO.UseVisualStyleBackColor = true;
            this.cmdKillPSO.Click += new System.EventHandler(this.cmdKillPSO_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkKillPSO);
            this.groupBox1.Controls.Add(this.chkBypassPatch);
            this.groupBox1.Controls.Add(this.chkControllerCheck);
            this.groupBox1.Location = new System.Drawing.Point(24, 623);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(766, 123);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extra Options";
            // 
            // chkKillPSO
            // 
            this.chkKillPSO.AutoSize = true;
            this.chkKillPSO.Checked = global::PsoWindowSize.Properties.Settings.Default.KillPso;
            this.chkKillPSO.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "KillPso", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkKillPSO.Location = new System.Drawing.Point(18, 37);
            this.chkKillPSO.Margin = new System.Windows.Forms.Padding(6);
            this.chkKillPSO.Name = "chkKillPSO";
            this.chkKillPSO.Size = new System.Drawing.Size(308, 29);
            this.chkKillPSO.TabIndex = 24;
            this.chkKillPSO.Text = "Kill PSO if window is closed";
            this.chkKillPSO.UseVisualStyleBackColor = true;
            // 
            // chkControllerCheck
            // 
            this.chkControllerCheck.AutoSize = true;
            this.chkControllerCheck.Checked = global::PsoWindowSize.Properties.Settings.Default.CheckController;
            this.chkControllerCheck.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "CheckController", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkControllerCheck.Location = new System.Drawing.Point(18, 79);
            this.chkControllerCheck.Margin = new System.Windows.Forms.Padding(4);
            this.chkControllerCheck.Name = "chkControllerCheck";
            this.chkControllerCheck.Size = new System.Drawing.Size(242, 29);
            this.chkControllerCheck.TabIndex = 23;
            this.chkControllerCheck.Text = "Check for controllers";
            this.chkControllerCheck.UseVisualStyleBackColor = true;
            // 
            // chkIpPatch
            // 
            this.chkIpPatch.AutoSize = true;
            this.chkIpPatch.Checked = global::PsoWindowSize.Properties.Settings.Default.IpPatch;
            this.chkIpPatch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIpPatch.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "IpPatch", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIpPatch.Location = new System.Drawing.Point(368, 125);
            this.chkIpPatch.Margin = new System.Windows.Forms.Padding(6);
            this.chkIpPatch.Name = "chkIpPatch";
            this.chkIpPatch.Size = new System.Drawing.Size(124, 29);
            this.chkIpPatch.TabIndex = 22;
            this.chkIpPatch.Text = "IP Patch";
            this.chkIpPatch.UseVisualStyleBackColor = true;
            this.chkIpPatch.CheckedChanged += new System.EventHandler(this.chkIpPatch_CheckedChanged);
            this.chkIpPatch.CheckStateChanged += new System.EventHandler(this.chkIpPatch_CheckStateChanged);
            // 
            // txtServer
            // 
            this.txtServer.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PsoWindowSize.Properties.Settings.Default, "ServerName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtServer.Location = new System.Drawing.Point(506, 121);
            this.txtServer.Margin = new System.Windows.Forms.Padding(6);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(238, 31);
            this.txtServer.TabIndex = 21;
            this.txtServer.Text = global::PsoWindowSize.Properties.Settings.Default.ServerName;
            // 
            // chkVista
            // 
            this.chkVista.AutoSize = true;
            this.chkVista.Checked = global::PsoWindowSize.Properties.Settings.Default.KeyboardFix;
            this.chkVista.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVista.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "KeyboardFix", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkVista.Location = new System.Drawing.Point(18, 37);
            this.chkVista.Margin = new System.Windows.Forms.Padding(6);
            this.chkVista.Name = "chkVista";
            this.chkVista.Size = new System.Drawing.Size(338, 29);
            this.chkVista.TabIndex = 15;
            this.chkVista.Text = "Keyboard fix for Vista or newer";
            this.chkVista.UseVisualStyleBackColor = true;
            this.chkVista.CheckStateChanged += new System.EventHandler(this.chkVista_CheckStateChanged);
            // 
            // chkWhiteNames
            // 
            this.chkWhiteNames.AutoSize = true;
            this.chkWhiteNames.Checked = global::PsoWindowSize.Properties.Settings.Default.NameFix;
            this.chkWhiteNames.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "NameFix", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkWhiteNames.Location = new System.Drawing.Point(368, 37);
            this.chkWhiteNames.Margin = new System.Windows.Forms.Padding(6);
            this.chkWhiteNames.Name = "chkWhiteNames";
            this.chkWhiteNames.Size = new System.Drawing.Size(345, 29);
            this.chkWhiteNames.TabIndex = 16;
            this.chkWhiteNames.Text = "PSO v1 and GC names in white";
            this.chkWhiteNames.UseVisualStyleBackColor = true;
            this.chkWhiteNames.CheckStateChanged += new System.EventHandler(this.chkWhiteNames_CheckStateChanged);
            // 
            // chkMusicFix
            // 
            this.chkMusicFix.AutoSize = true;
            this.chkMusicFix.Checked = global::PsoWindowSize.Properties.Settings.Default.MusicFix;
            this.chkMusicFix.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "MusicFix", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkMusicFix.Location = new System.Drawing.Point(18, 81);
            this.chkMusicFix.Margin = new System.Windows.Forms.Padding(6);
            this.chkMusicFix.Name = "chkMusicFix";
            this.chkMusicFix.Size = new System.Drawing.Size(325, 29);
            this.chkMusicFix.TabIndex = 17;
            this.chkMusicFix.Text = "Battle music in normal quests";
            this.chkMusicFix.UseVisualStyleBackColor = true;
            this.chkMusicFix.CheckStateChanged += new System.EventHandler(this.chkMusicFix_CheckStateChanged);
            // 
            // chkWordFilter
            // 
            this.chkWordFilter.AutoSize = true;
            this.chkWordFilter.Checked = global::PsoWindowSize.Properties.Settings.Default.WordFilter;
            this.chkWordFilter.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "WordFilter", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkWordFilter.Location = new System.Drawing.Point(18, 125);
            this.chkWordFilter.Margin = new System.Windows.Forms.Padding(6);
            this.chkWordFilter.Name = "chkWordFilter";
            this.chkWordFilter.Size = new System.Drawing.Size(215, 29);
            this.chkWordFilter.TabIndex = 19;
            this.chkWordFilter.Text = "Disable word filter";
            this.chkWordFilter.UseVisualStyleBackColor = true;
            this.chkWordFilter.CheckStateChanged += new System.EventHandler(this.chkWordFilter_CheckStateChanged);
            // 
            // chkMapFix
            // 
            this.chkMapFix.AutoSize = true;
            this.chkMapFix.Checked = global::PsoWindowSize.Properties.Settings.Default.MapFix;
            this.chkMapFix.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "MapFix", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkMapFix.Location = new System.Drawing.Point(368, 81);
            this.chkMapFix.Margin = new System.Windows.Forms.Padding(6);
            this.chkMapFix.Name = "chkMapFix";
            this.chkMapFix.Size = new System.Drawing.Size(256, 29);
            this.chkMapFix.TabIndex = 18;
            this.chkMapFix.Text = "Ultimate mode map fix";
            this.chkMapFix.UseVisualStyleBackColor = true;
            this.chkMapFix.CheckStateChanged += new System.EventHandler(this.chkMapFix_CheckStateChanged);
            // 
            // chkWindowed
            // 
            this.chkWindowed.AutoSize = true;
            this.chkWindowed.Checked = global::PsoWindowSize.Properties.Settings.Default.Windowed;
            this.chkWindowed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWindowed.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "Windowed", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkWindowed.Location = new System.Drawing.Point(320, 21);
            this.chkWindowed.Margin = new System.Windows.Forms.Padding(6);
            this.chkWindowed.Name = "chkWindowed";
            this.chkWindowed.Size = new System.Drawing.Size(144, 29);
            this.chkWindowed.TabIndex = 3;
            this.chkWindowed.Text = "Windowed";
            this.chkWindowed.UseVisualStyleBackColor = true;
            this.chkWindowed.CheckedChanged += new System.EventHandler(this.chkWindowed_CheckedChanged);
            this.chkWindowed.CheckStateChanged += new System.EventHandler(this.chkWindowed_CheckStateChanged);
            // 
            // rdoOnline
            // 
            this.rdoOnline.AutoSize = true;
            this.rdoOnline.Checked = global::PsoWindowSize.Properties.Settings.Default.Online;
            this.rdoOnline.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "Online", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rdoOnline.Location = new System.Drawing.Point(12, 42);
            this.rdoOnline.Margin = new System.Windows.Forms.Padding(6);
            this.rdoOnline.Name = "rdoOnline";
            this.rdoOnline.Size = new System.Drawing.Size(105, 29);
            this.rdoOnline.TabIndex = 0;
            this.rdoOnline.TabStop = true;
            this.rdoOnline.Text = "Online";
            this.rdoOnline.UseVisualStyleBackColor = true;
            // 
            // chkEmbedFullScreen
            // 
            this.chkEmbedFullScreen.AutoSize = true;
            this.chkEmbedFullScreen.Checked = global::PsoWindowSize.Properties.Settings.Default.EmbedFullScreen;
            this.chkEmbedFullScreen.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "EmbedFullScreen", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkEmbedFullScreen.Location = new System.Drawing.Point(12, 346);
            this.chkEmbedFullScreen.Margin = new System.Windows.Forms.Padding(6);
            this.chkEmbedFullScreen.Name = "chkEmbedFullScreen";
            this.chkEmbedFullScreen.Size = new System.Drawing.Size(226, 29);
            this.chkEmbedFullScreen.TabIndex = 3;
            this.chkEmbedFullScreen.Text = "Embed Full Screen";
            this.chkEmbedFullScreen.UseVisualStyleBackColor = true;
            this.chkEmbedFullScreen.CheckStateChanged += new System.EventHandler(this.chkEmbedFullScreen_CheckStateChanged);
            // 
            // chkCenterWindow
            // 
            this.chkCenterWindow.AutoSize = true;
            this.chkCenterWindow.Checked = global::PsoWindowSize.Properties.Settings.Default.LaunchCentered;
            this.chkCenterWindow.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "LaunchCentered", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkCenterWindow.Location = new System.Drawing.Point(176, 302);
            this.chkCenterWindow.Margin = new System.Windows.Forms.Padding(6);
            this.chkCenterWindow.Name = "chkCenterWindow";
            this.chkCenterWindow.Size = new System.Drawing.Size(291, 29);
            this.chkCenterWindow.TabIndex = 11;
            this.chkCenterWindow.Text = "Launch Window Centered";
            this.chkCenterWindow.UseVisualStyleBackColor = true;
            this.chkCenterWindow.CheckStateChanged += new System.EventHandler(this.chkCenterWindow_CheckStateChanged);
            // 
            // chkAutoResize
            // 
            this.chkAutoResize.AutoSize = true;
            this.chkAutoResize.Checked = global::PsoWindowSize.Properties.Settings.Default.AutoResize;
            this.chkAutoResize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoResize.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "AutoResize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkAutoResize.Location = new System.Drawing.Point(12, 302);
            this.chkAutoResize.Margin = new System.Windows.Forms.Padding(6);
            this.chkAutoResize.Name = "chkAutoResize";
            this.chkAutoResize.Size = new System.Drawing.Size(160, 29);
            this.chkAutoResize.TabIndex = 10;
            this.chkAutoResize.Text = "Auto Resize";
            this.chkAutoResize.UseVisualStyleBackColor = true;
            this.chkAutoResize.CheckStateChanged += new System.EventHandler(this.chkAutoResize_CheckStateChanged);
            // 
            // chkLockRatio
            // 
            this.chkLockRatio.AutoSize = true;
            this.chkLockRatio.Checked = global::PsoWindowSize.Properties.Settings.Default.LockRatio;
            this.chkLockRatio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLockRatio.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PsoWindowSize.Properties.Settings.Default, "LockRatio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkLockRatio.Location = new System.Drawing.Point(212, 112);
            this.chkLockRatio.Margin = new System.Windows.Forms.Padding(6);
            this.chkLockRatio.Name = "chkLockRatio";
            this.chkLockRatio.Size = new System.Drawing.Size(146, 29);
            this.chkLockRatio.TabIndex = 9;
            this.chkLockRatio.Text = "Lock Ratio";
            this.chkLockRatio.UseVisualStyleBackColor = true;
            this.chkLockRatio.CheckStateChanged += new System.EventHandler(this.chkLockRatio_CheckStateChanged);
            // 
            // cboRatio
            // 
            this.cboRatio.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PsoWindowSize.Properties.Settings.Default, "PresetRezText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cboRatio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRatio.FormattingEnabled = true;
            this.cboRatio.Items.AddRange(new object[] {
            "1x (640 x 480)",
            "2x (1280 x 960)",
            "3x (1920 x 1440)"});
            this.cboRatio.Location = new System.Drawing.Point(226, 35);
            this.cboRatio.Margin = new System.Windows.Forms.Padding(6);
            this.cboRatio.Name = "cboRatio";
            this.cboRatio.Size = new System.Drawing.Size(238, 33);
            this.cboRatio.TabIndex = 5;
            this.cboRatio.Text = global::PsoWindowSize.Properties.Settings.Default.PresetRezText;
            this.cboRatio.SelectedIndexChanged += new System.EventHandler(this.cboRatio_SelectedIndexChanged);
            // 
            // frmResizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 852);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdKillPSO);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.fraPatches);
            this.Controls.Add(this.cmdStartOffline);
            this.Controls.Add(this.cmdStartOnline);
            this.Controls.Add(this.chkWindowed);
            this.Controls.Add(this.lblPsoStatus);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.fraWindowed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "frmResizer";
            this.Text = "PSO Launcher Corey Edition";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmResizer_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.fraWindowed.ResumeLayout(false);
            this.fraWindowed.PerformLayout();
            this.fraCustom.ResumeLayout(false);
            this.fraCustom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtW)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.fraPatches.ResumeLayout(false);
            this.fraPatches.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdResize;
        private System.Windows.Forms.ComboBox cboRatio;
        private System.Windows.Forms.GroupBox fraWindowed;
        private System.Windows.Forms.RadioButton rdoCustom;
        private System.Windows.Forms.GroupBox fraCustom;
        private System.Windows.Forms.CheckBox chkLockRatio;
        private System.Windows.Forms.Label lblRatio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtW;
        private System.Windows.Forms.RadioButton rdoPerfect;
        private System.Windows.Forms.Label lblPsoStatus;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdScreenshot;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdStartOffline;
        private System.Windows.Forms.Button cmdStartOnline;
        private System.Windows.Forms.CheckBox chkAutoResize;
        private System.Windows.Forms.Button cmdOptions;
        private System.Windows.Forms.Button cmdLaunch;
        private System.Windows.Forms.CheckBox chkWindowed;
        private System.Windows.Forms.CheckBox chkVista;
        private System.Windows.Forms.CheckBox chkWhiteNames;
        private System.Windows.Forms.CheckBox chkMusicFix;
        private System.Windows.Forms.CheckBox chkMapFix;
        private System.Windows.Forms.CheckBox chkWordFilter;
        private System.Windows.Forms.Button cmdSerial;
        private System.Windows.Forms.RadioButton rdoOffline;
        private System.Windows.Forms.RadioButton rdoOnline;
        private System.Windows.Forms.GroupBox fraPatches;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.CheckBox chkCenterWindow;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.CheckBox chkIpPatch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkEmbedFullScreen;
        private System.Windows.Forms.RadioButton rdoScreenHeight;
        private System.Windows.Forms.CheckBox chkBypassPatch;
        private System.Windows.Forms.CheckBox chkControllerCheck;
        private System.Windows.Forms.Button cmdKillPSO;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkKillPSO;
    }
}

