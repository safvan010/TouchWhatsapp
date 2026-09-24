namespace WhatsappAutomation
{
    partial class MainForm
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
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.tabControls = new System.Windows.Forms.TabControl();
            this.tabAutoSend = new System.Windows.Forms.TabPage();
            this.grpDbConfig = new System.Windows.Forms.GroupBox();
            this.lblDbInstance = new System.Windows.Forms.Label();
            this.txtDbInstance = new System.Windows.Forms.TextBox();
            this.lblDbName = new System.Windows.Forms.Label();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.btnTestDbConn = new System.Windows.Forms.Button();
            this.btnSaveDbConfig = new System.Windows.Forms.Button();
            this.grpAutoEngine = new System.Windows.Forms.GroupBox();
            this.btnToggleAutoSend = new System.Windows.Forms.Button();
            this.lblAutoStatus = new System.Windows.Forms.Label();
            this.chkSpecificDate = new System.Windows.Forms.CheckBox();
            this.dtpFilterDate = new System.Windows.Forms.DateTimePicker();
            this.lblAutoLog = new System.Windows.Forms.Label();
            this.txtAutoLog = new System.Windows.Forms.TextBox();
            this.tabManualSend = new System.Windows.Forms.TabPage();
            this.lblMobileNo = new System.Windows.Forms.Label();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.lblMobileHint = new System.Windows.Forms.Label();
            this.lblDocument = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.btnClearFile = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblManualLog = new System.Windows.Forms.Label();
            this.txtManualLog = new System.Windows.Forms.TextBox();
            this.pnlBottomButtons = new System.Windows.Forms.Panel();
            this.btnInjectWaJs = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.pnlBrowserHost = new System.Windows.Forms.Panel();
            this.statusTimer = new System.Windows.Forms.Timer(this.components);
            this.sqlPollTimer = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trayOpenItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayToggleAutoItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traySeparator = new System.Windows.Forms.ToolStripSeparator();
            this.trayExitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.grpTemplate = new System.Windows.Forms.GroupBox();
            this.lblTemplatePrompt = new System.Windows.Forms.Label();
            this.txtMsgTemplate = new System.Windows.Forms.TextBox();
            this.lblTemplateHelp = new System.Windows.Forms.Label();
            this.grpInvoicePdf = new System.Windows.Forms.GroupBox();
            this.chkSendPdf = new System.Windows.Forms.CheckBox();
            this.lblPaperSize = new System.Windows.Forms.Label();
            this.rbPaperA4 = new System.Windows.Forms.RadioButton();
            this.rbPaperThermal = new System.Windows.Forms.RadioButton();
            this.lblHeader1 = new System.Windows.Forms.Label();
            this.txtHeader1 = new System.Windows.Forms.TextBox();
            this.lblHeader2 = new System.Windows.Forms.Label();
            this.txtHeader2 = new System.Windows.Forms.TextBox();
            this.lblHeader3 = new System.Windows.Forms.Label();
            this.txtHeader3 = new System.Windows.Forms.TextBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.lblSettingsStatus = new System.Windows.Forms.Label();
            this.grpWaJsUpdate = new System.Windows.Forms.GroupBox();
            this.lblWaJsVersion = new System.Windows.Forms.Label();
            this.lblWaJsStatus = new System.Windows.Forms.Label();
            this.chkAutoUpdateWaJs = new System.Windows.Forms.CheckBox();
            this.btnCheckWaJsUpdate = new System.Windows.Forms.Button();
            this.grpAppUpdate = new System.Windows.Forms.GroupBox();
            this.lblAppVersion = new System.Windows.Forms.Label();
            this.lblAppUpdateStatus = new System.Windows.Forms.Label();
            this.chkAutoCheckAppUpdates = new System.Windows.Forms.CheckBox();
            this.btnCheckAppUpdate = new System.Windows.Forms.Button();
            this.lblAppUpdateSourcePrompt = new System.Windows.Forms.Label();
            this.txtAppUpdateSource = new System.Windows.Forms.TextBox();
            this.prgAppUpdate = new System.Windows.Forms.ProgressBar();
            this.lblAppUpdateProgress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.tabControls.SuspendLayout();
            this.tabAutoSend.SuspendLayout();
            this.grpDbConfig.SuspendLayout();
            this.grpAutoEngine.SuspendLayout();
            this.tabManualSend.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.grpTemplate.SuspendLayout();
            this.grpInvoicePdf.SuspendLayout();
            this.grpWaJsUpdate.SuspendLayout();
            this.grpAppUpdate.SuspendLayout();
            this.pnlBottomButtons.SuspendLayout();
            this.trayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Margin = new System.Windows.Forms.Padding(4);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.splitMain.Panel1.Controls.Add(this.pnlLeft);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlBrowserHost);
            this.splitMain.Size = new System.Drawing.Size(1260, 740);
            this.splitMain.SplitterDistance = 450;
            this.splitMain.SplitterWidth = 5;
            this.splitMain.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lblTitle);
            this.pnlLeft.Controls.Add(this.lblSubtitle);
            this.pnlLeft.Controls.Add(this.lblStatusBadge);
            this.pnlLeft.Controls.Add(this.tabControls);
            this.pnlLeft.Controls.Add(this.pnlBottomButtons);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(450, 740);
            this.pnlLeft.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(140)))), ((int)(((byte)(126)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(234, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "WhatsApp Automation";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(14, 37);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(264, 13);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "CefSharp + WA-JS + SQL Server 2008 R2 Auto-Send";
            // 
            // lblStatusBadge
            // 
            this.lblStatusBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(205)))));
            this.lblStatusBadge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(100)))), ((int)(((byte)(4)))));
            this.lblStatusBadge.Location = new System.Drawing.Point(12, 56);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Padding = new System.Windows.Forms.Padding(6);
            this.lblStatusBadge.Size = new System.Drawing.Size(426, 30);
            this.lblStatusBadge.TabIndex = 2;
            this.lblStatusBadge.Text = "Status: Initializing WhatsApp Web...";
            this.lblStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControls
            // 
            this.tabControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControls.Controls.Add(this.tabAutoSend);
            this.tabControls.Controls.Add(this.tabManualSend);
            this.tabControls.Controls.Add(this.tabSettings);
            this.tabControls.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControls.Location = new System.Drawing.Point(12, 92);
            this.tabControls.Name = "tabControls";
            this.tabControls.SelectedIndex = 0;
            this.tabControls.Size = new System.Drawing.Size(426, 600);
            this.tabControls.TabIndex = 3;
            // 
            // tabAutoSend
            // 
            this.tabAutoSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabAutoSend.Controls.Add(this.grpDbConfig);
            this.tabAutoSend.Controls.Add(this.grpAutoEngine);
            this.tabAutoSend.Controls.Add(this.lblAutoLog);
            this.tabAutoSend.Controls.Add(this.txtAutoLog);
            this.tabAutoSend.Location = new System.Drawing.Point(4, 24);
            this.tabAutoSend.Name = "tabAutoSend";
            this.tabAutoSend.Padding = new System.Windows.Forms.Padding(8);
            this.tabAutoSend.Size = new System.Drawing.Size(418, 572);
            this.tabAutoSend.TabIndex = 0;
            this.tabAutoSend.Text = "SQL Auto-Send";
            // 
            // grpDbConfig
            // 
            this.grpDbConfig.Controls.Add(this.lblDbInstance);
            this.grpDbConfig.Controls.Add(this.txtDbInstance);
            this.grpDbConfig.Controls.Add(this.lblDbName);
            this.grpDbConfig.Controls.Add(this.txtDbName);
            this.grpDbConfig.Controls.Add(this.btnTestDbConn);
            this.grpDbConfig.Controls.Add(this.btnSaveDbConfig);
            this.grpDbConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDbConfig.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDbConfig.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.grpDbConfig.Location = new System.Drawing.Point(8, 8);
            this.grpDbConfig.Name = "grpDbConfig";
            this.grpDbConfig.Size = new System.Drawing.Size(402, 115);
            this.grpDbConfig.TabIndex = 0;
            this.grpDbConfig.TabStop = false;
            this.grpDbConfig.Text = "SQL Server 2008 R2 Connection";
            // 
            // lblDbInstance
            // 
            this.lblDbInstance.AutoSize = true;
            this.lblDbInstance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDbInstance.Location = new System.Drawing.Point(10, 22);
            this.lblDbInstance.Name = "lblDbInstance";
            this.lblDbInstance.Size = new System.Drawing.Size(76, 13);
            this.lblDbInstance.TabIndex = 0;
            this.lblDbInstance.Text = "SQL Instance:";
            // 
            // txtDbInstance
            // 
            this.txtDbInstance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDbInstance.Location = new System.Drawing.Point(100, 19);
            this.txtDbInstance.Name = "txtDbInstance";
            this.txtDbInstance.Size = new System.Drawing.Size(292, 23);
            this.txtDbInstance.TabIndex = 1;
            // 
            // lblDbName
            // 
            this.lblDbName.AutoSize = true;
            this.lblDbName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDbName.Location = new System.Drawing.Point(10, 51);
            this.lblDbName.Name = "lblDbName";
            this.lblDbName.Size = new System.Drawing.Size(89, 13);
            this.lblDbName.TabIndex = 2;
            this.lblDbName.Text = "Database Name:";
            // 
            // txtDbName
            // 
            this.txtDbName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDbName.Location = new System.Drawing.Point(100, 48);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(292, 23);
            this.txtDbName.TabIndex = 3;
            // 
            // btnTestDbConn
            // 
            this.btnTestDbConn.BackColor = System.Drawing.Color.White;
            this.btnTestDbConn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestDbConn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnTestDbConn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestDbConn.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestDbConn.Location = new System.Drawing.Point(100, 77);
            this.btnTestDbConn.Name = "btnTestDbConn";
            this.btnTestDbConn.Size = new System.Drawing.Size(140, 28);
            this.btnTestDbConn.TabIndex = 4;
            this.btnTestDbConn.Text = "Test Connection";
            this.btnTestDbConn.UseVisualStyleBackColor = false;
            this.btnTestDbConn.Click += new System.EventHandler(this.btnTestDbConn_Click);
            // 
            // btnSaveDbConfig
            // 
            this.btnSaveDbConfig.BackColor = System.Drawing.Color.White;
            this.btnSaveDbConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveDbConfig.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSaveDbConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDbConfig.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveDbConfig.Location = new System.Drawing.Point(252, 77);
            this.btnSaveDbConfig.Name = "btnSaveDbConfig";
            this.btnSaveDbConfig.Size = new System.Drawing.Size(140, 28);
            this.btnSaveDbConfig.TabIndex = 5;
            this.btnSaveDbConfig.Text = "Save Config";
            this.btnSaveDbConfig.UseVisualStyleBackColor = false;
            this.btnSaveDbConfig.Click += new System.EventHandler(this.btnSaveDbConfig_Click);
            // 
            // grpAutoEngine
            // 
            this.grpAutoEngine.Controls.Add(this.btnToggleAutoSend);
            this.grpAutoEngine.Controls.Add(this.lblAutoStatus);
            this.grpAutoEngine.Controls.Add(this.chkSpecificDate);
            this.grpAutoEngine.Controls.Add(this.dtpFilterDate);
            this.grpAutoEngine.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpAutoEngine.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAutoEngine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.grpAutoEngine.Location = new System.Drawing.Point(8, 123);
            this.grpAutoEngine.Name = "grpAutoEngine";
            this.grpAutoEngine.Size = new System.Drawing.Size(402, 118);
            this.grpAutoEngine.TabIndex = 1;
            this.grpAutoEngine.TabStop = false;
            this.grpAutoEngine.Text = "Sales Auto-Messaging (tbSalesDetails)";
            // 
            // btnToggleAutoSend
            // 
            this.btnToggleAutoSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(211)))), ((int)(((byte)(102)))));
            this.btnToggleAutoSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleAutoSend.FlatAppearance.BorderSize = 0;
            this.btnToggleAutoSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleAutoSend.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggleAutoSend.ForeColor = System.Drawing.Color.White;
            this.btnToggleAutoSend.Location = new System.Drawing.Point(10, 22);
            this.btnToggleAutoSend.Name = "btnToggleAutoSend";
            this.btnToggleAutoSend.Size = new System.Drawing.Size(382, 34);
            this.btnToggleAutoSend.TabIndex = 0;
            this.btnToggleAutoSend.Text = "▶ Start Auto-Send (Every 10s)";
            this.btnToggleAutoSend.UseVisualStyleBackColor = false;
            this.btnToggleAutoSend.Click += new System.EventHandler(this.btnToggleAutoSend_Click);
            // 
            // lblAutoStatus
            // 
            this.lblAutoStatus.AutoSize = true;
            this.lblAutoStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutoStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblAutoStatus.Location = new System.Drawing.Point(10, 62);
            this.lblAutoStatus.Name = "lblAutoStatus";
            this.lblAutoStatus.Size = new System.Drawing.Size(126, 15);
            this.lblAutoStatus.TabIndex = 1;
            this.lblAutoStatus.Text = "Auto-Send Status: Idle";
            // 
            // chkSpecificDate
            // 
            this.chkSpecificDate.AutoSize = true;
            this.chkSpecificDate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSpecificDate.Location = new System.Drawing.Point(13, 87);
            this.chkSpecificDate.Name = "chkSpecificDate";
            this.chkSpecificDate.Size = new System.Drawing.Size(95, 17);
            this.chkSpecificDate.TabIndex = 2;
            this.chkSpecificDate.Text = "Specific Date:";
            this.chkSpecificDate.UseVisualStyleBackColor = true;
            this.chkSpecificDate.CheckedChanged += new System.EventHandler(this.chkSpecificDate_CheckedChanged);
            // 
            // dtpFilterDate
            // 
            this.dtpFilterDate.CustomFormat = "yyyy-MM-dd";
            this.dtpFilterDate.Enabled = false;
            this.dtpFilterDate.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFilterDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFilterDate.Location = new System.Drawing.Point(114, 84);
            this.dtpFilterDate.Name = "dtpFilterDate";
            this.dtpFilterDate.Size = new System.Drawing.Size(140, 23);
            this.dtpFilterDate.TabIndex = 3;
            // 
            // lblAutoLog
            // 
            this.lblAutoLog.AutoSize = true;
            this.lblAutoLog.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutoLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblAutoLog.Location = new System.Drawing.Point(8, 250);
            this.lblAutoLog.Name = "lblAutoLog";
            this.lblAutoLog.Size = new System.Drawing.Size(130, 15);
            this.lblAutoLog.TabIndex = 2;
            this.lblAutoLog.Text = "Auto-Send Activity Log:";
            // 
            // txtAutoLog
            // 
            this.txtAutoLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAutoLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtAutoLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAutoLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAutoLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtAutoLog.Location = new System.Drawing.Point(8, 270);
            this.txtAutoLog.Multiline = true;
            this.txtAutoLog.Name = "txtAutoLog";
            this.txtAutoLog.ReadOnly = true;
            this.txtAutoLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAutoLog.Size = new System.Drawing.Size(402, 290);
            this.txtAutoLog.TabIndex = 3;
            // 
            // tabManualSend
            // 
            this.tabManualSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabManualSend.Controls.Add(this.lblMobileNo);
            this.tabManualSend.Controls.Add(this.txtMobileNo);
            this.tabManualSend.Controls.Add(this.lblMobileHint);
            this.tabManualSend.Controls.Add(this.lblDocument);
            this.tabManualSend.Controls.Add(this.txtFilePath);
            this.tabManualSend.Controls.Add(this.btnSelectFile);
            this.tabManualSend.Controls.Add(this.btnClearFile);
            this.tabManualSend.Controls.Add(this.lblMessage);
            this.tabManualSend.Controls.Add(this.txtMessage);
            this.tabManualSend.Controls.Add(this.btnSend);
            this.tabManualSend.Controls.Add(this.lblManualLog);
            this.tabManualSend.Controls.Add(this.txtManualLog);
            this.tabManualSend.Location = new System.Drawing.Point(4, 24);
            this.tabManualSend.Name = "tabManualSend";
            this.tabManualSend.Padding = new System.Windows.Forms.Padding(8);
            this.tabManualSend.Size = new System.Drawing.Size(418, 572);
            this.tabManualSend.TabIndex = 1;
            this.tabManualSend.Text = "Manual Send";
            // 
            // lblMobileNo
            // 
            this.lblMobileNo.AutoSize = true;
            this.lblMobileNo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMobileNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblMobileNo.Location = new System.Drawing.Point(8, 8);
            this.lblMobileNo.Name = "lblMobileNo";
            this.lblMobileNo.Size = new System.Drawing.Size(69, 15);
            this.lblMobileNo.TabIndex = 0;
            this.lblMobileNo.Text = "MobileNo:";
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.Location = new System.Drawing.Point(11, 26);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(395, 25);
            this.txtMobileNo.TabIndex = 1;
            // 
            // lblMobileHint
            // 
            this.lblMobileHint.AutoSize = true;
            this.lblMobileHint.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMobileHint.ForeColor = System.Drawing.Color.Gray;
            this.lblMobileHint.Location = new System.Drawing.Point(11, 54);
            this.lblMobileHint.Name = "lblMobileHint";
            this.lblMobileHint.Size = new System.Drawing.Size(262, 13);
            this.lblMobileHint.TabIndex = 2;
            this.lblMobileHint.Text = "Include country code without + or 0 (e.g. 919876543210)";
            // 
            // lblDocument
            // 
            this.lblDocument.AutoSize = true;
            this.lblDocument.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocument.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblDocument.Location = new System.Drawing.Point(8, 75);
            this.lblDocument.Name = "lblDocument";
            this.lblDocument.Size = new System.Drawing.Size(161, 15);
            this.lblDocument.TabIndex = 3;
            this.lblDocument.Text = "Document / File (Optional):";
            // 
            // txtFilePath
            // 
            this.txtFilePath.BackColor = System.Drawing.Color.White;
            this.txtFilePath.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.Location = new System.Drawing.Point(11, 93);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(275, 23);
            this.txtFilePath.TabIndex = 4;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnSelectFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(212)))), ((int)(((byte)(218)))));
            this.btnSelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.btnSelectFile.Location = new System.Drawing.Point(292, 92);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(78, 25);
            this.btnSelectFile.TabIndex = 5;
            this.btnSelectFile.Text = "Browse...";
            this.btnSelectFile.UseVisualStyleBackColor = false;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnClearFile
            // 
            this.btnClearFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnClearFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(212)))), ((int)(((byte)(218)))));
            this.btnClearFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFile.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClearFile.Location = new System.Drawing.Point(376, 92);
            this.btnClearFile.Name = "btnClearFile";
            this.btnClearFile.Size = new System.Drawing.Size(30, 25);
            this.btnClearFile.TabIndex = 6;
            this.btnClearFile.Text = "✕";
            this.btnClearFile.UseVisualStyleBackColor = false;
            this.btnClearFile.Click += new System.EventHandler(this.btnClearFile_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblMessage.Location = new System.Drawing.Point(8, 126);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(117, 15);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.Text = "Message / Caption:";
            // 
            // txtMessage
            // 
            this.txtMessage.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(11, 145);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(395, 75);
            this.txtMessage.TabIndex = 8;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(211)))), ((int)(((byte)(102)))));
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(11, 230);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(395, 38);
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "Send Message";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblManualLog
            // 
            this.lblManualLog.AutoSize = true;
            this.lblManualLog.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManualLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblManualLog.Location = new System.Drawing.Point(8, 278);
            this.lblManualLog.Name = "lblManualLog";
            this.lblManualLog.Size = new System.Drawing.Size(76, 15);
            this.lblManualLog.TabIndex = 10;
            this.lblManualLog.Text = "Activity Log:";
            // 
            // txtManualLog
            // 
            this.txtManualLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtManualLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtManualLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtManualLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManualLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtManualLog.Location = new System.Drawing.Point(11, 298);
            this.txtManualLog.Multiline = true;
            this.txtManualLog.Name = "txtManualLog";
            this.txtManualLog.ReadOnly = true;
            this.txtManualLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtManualLog.Size = new System.Drawing.Size(395, 262);
            this.txtManualLog.TabIndex = 11;
            // 
            // tabSettings
            // 
            this.tabSettings.AutoScroll = true;
            this.tabSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabSettings.Controls.Add(this.grpTemplate);
            this.tabSettings.Controls.Add(this.grpInvoicePdf);
            this.tabSettings.Controls.Add(this.grpWaJsUpdate);
            this.tabSettings.Controls.Add(this.grpAppUpdate);
            this.tabSettings.Controls.Add(this.btnSaveSettings);
            this.tabSettings.Controls.Add(this.lblSettingsStatus);
            this.tabSettings.Location = new System.Drawing.Point(4, 24);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(8);
            this.tabSettings.Size = new System.Drawing.Size(418, 572);
            this.tabSettings.TabIndex = 2;
            this.tabSettings.Text = "Settings";
            // 
            // grpTemplate
            // 
            this.grpTemplate.Controls.Add(this.lblTemplatePrompt);
            this.grpTemplate.Controls.Add(this.txtMsgTemplate);
            this.grpTemplate.Controls.Add(this.lblTemplateHelp);
            this.grpTemplate.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTemplate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.grpTemplate.Location = new System.Drawing.Point(8, 8);
            this.grpTemplate.Name = "grpTemplate";
            this.grpTemplate.Padding = new System.Windows.Forms.Padding(8);
            this.grpTemplate.Size = new System.Drawing.Size(400, 185);
            this.grpTemplate.TabIndex = 0;
            this.grpTemplate.TabStop = false;
            this.grpTemplate.Text = "WhatsApp Message Template";
            // 
            // lblTemplatePrompt
            // 
            this.lblTemplatePrompt.AutoSize = true;
            this.lblTemplatePrompt.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplatePrompt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.lblTemplatePrompt.Location = new System.Drawing.Point(10, 20);
            this.lblTemplatePrompt.Name = "lblTemplatePrompt";
            this.lblTemplatePrompt.Size = new System.Drawing.Size(209, 13);
            this.lblTemplatePrompt.TabIndex = 0;
            this.lblTemplatePrompt.Text = "Template text (used during Auto-Send):";
            // 
            // txtMsgTemplate
            // 
            this.txtMsgTemplate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsgTemplate.Location = new System.Drawing.Point(10, 38);
            this.txtMsgTemplate.Multiline = true;
            this.txtMsgTemplate.Name = "txtMsgTemplate";
            this.txtMsgTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMsgTemplate.Size = new System.Drawing.Size(380, 85);
            this.txtMsgTemplate.TabIndex = 1;
            // 
            // lblTemplateHelp
            // 
            this.lblTemplateHelp.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplateHelp.ForeColor = System.Drawing.Color.DimGray;
            this.lblTemplateHelp.Location = new System.Drawing.Point(10, 128);
            this.lblTemplateHelp.Name = "lblTemplateHelp";
            this.lblTemplateHelp.Size = new System.Drawing.Size(380, 48);
            this.lblTemplateHelp.TabIndex = 2;
            this.lblTemplateHelp.Text = "Available Tags:\r\n{CustomerName}, {GrandTotal}, {VoucherNo}, {SoldDate},\r\n{NetTotal}, {Discount}, {ShopName}";
            // 
            // grpInvoicePdf
            // 
            this.grpInvoicePdf.Controls.Add(this.chkSendPdf);
            this.grpInvoicePdf.Controls.Add(this.lblPaperSize);
            this.grpInvoicePdf.Controls.Add(this.rbPaperA4);
            this.grpInvoicePdf.Controls.Add(this.rbPaperThermal);
            this.grpInvoicePdf.Controls.Add(this.lblHeader1);
            this.grpInvoicePdf.Controls.Add(this.txtHeader1);
            this.grpInvoicePdf.Controls.Add(this.lblHeader2);
            this.grpInvoicePdf.Controls.Add(this.txtHeader2);
            this.grpInvoicePdf.Controls.Add(this.lblHeader3);
            this.grpInvoicePdf.Controls.Add(this.txtHeader3);
            this.grpInvoicePdf.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInvoicePdf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.grpInvoicePdf.Location = new System.Drawing.Point(8, 200);
            this.grpInvoicePdf.Name = "grpInvoicePdf";
            this.grpInvoicePdf.Padding = new System.Windows.Forms.Padding(8);
            this.grpInvoicePdf.Size = new System.Drawing.Size(400, 265);
            this.grpInvoicePdf.TabIndex = 1;
            this.grpInvoicePdf.TabStop = false;
            this.grpInvoicePdf.Text = "Bill / Invoice PDF";
            // 
            // chkSendPdf
            // 
            this.chkSendPdf.AutoSize = true;
            this.chkSendPdf.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSendPdf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(140)))), ((int)(((byte)(126)))));
            this.chkSendPdf.Location = new System.Drawing.Point(10, 22);
            this.chkSendPdf.Name = "chkSendPdf";
            this.chkSendPdf.Size = new System.Drawing.Size(306, 19);
            this.chkSendPdf.TabIndex = 0;
            this.chkSendPdf.Text = "Send Bill / Invoice PDF with WhatsApp message";
            this.chkSendPdf.UseVisualStyleBackColor = true;
            // 
            // lblPaperSize
            // 
            this.lblPaperSize.AutoSize = true;
            this.lblPaperSize.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaperSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.lblPaperSize.Location = new System.Drawing.Point(10, 48);
            this.lblPaperSize.Name = "lblPaperSize";
            this.lblPaperSize.Size = new System.Drawing.Size(78, 13);
            this.lblPaperSize.TabIndex = 1;
            this.lblPaperSize.Text = "Paper Format:";
            // 
            // rbPaperA4
            // 
            this.rbPaperA4.AutoSize = true;
            this.rbPaperA4.Checked = true;
            this.rbPaperA4.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPaperA4.Location = new System.Drawing.Point(100, 45);
            this.rbPaperA4.Name = "rbPaperA4";
            this.rbPaperA4.Size = new System.Drawing.Size(77, 19);
            this.rbPaperA4.TabIndex = 2;
            this.rbPaperA4.TabStop = true;
            this.rbPaperA4.Text = "A4 Invoice";
            this.rbPaperA4.UseVisualStyleBackColor = true;
            // 
            // rbPaperThermal
            // 
            this.rbPaperThermal.AutoSize = true;
            this.rbPaperThermal.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPaperThermal.Location = new System.Drawing.Point(195, 45);
            this.rbPaperThermal.Name = "rbPaperThermal";
            this.rbPaperThermal.Size = new System.Drawing.Size(150, 19);
            this.rbPaperThermal.TabIndex = 3;
            this.rbPaperThermal.Text = "3-inch Thermal (80mm)";
            this.rbPaperThermal.UseVisualStyleBackColor = true;
            // 
            // lblHeader1
            // 
            this.lblHeader1.AutoSize = true;
            this.lblHeader1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.lblHeader1.Location = new System.Drawing.Point(10, 72);
            this.lblHeader1.Name = "lblHeader1";
            this.lblHeader1.Size = new System.Drawing.Size(182, 13);
            this.lblHeader1.TabIndex = 4;
            this.lblHeader1.Text = "Shop / Business Name (Header 1):";
            // 
            // txtHeader1
            // 
            this.txtHeader1.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeader1.Location = new System.Drawing.Point(10, 89);
            this.txtHeader1.Name = "txtHeader1";
            this.txtHeader1.Size = new System.Drawing.Size(380, 23);
            this.txtHeader1.TabIndex = 5;
            // 
            // lblHeader2
            // 
            this.lblHeader2.AutoSize = true;
            this.lblHeader2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.lblHeader2.Location = new System.Drawing.Point(10, 117);
            this.lblHeader2.Name = "lblHeader2";
            this.lblHeader2.Size = new System.Drawing.Size(158, 13);
            this.lblHeader2.TabIndex = 6;
            this.lblHeader2.Text = "Address / Tagline (Header 2):";
            // 
            // txtHeader2
            // 
            this.txtHeader2.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeader2.Location = new System.Drawing.Point(10, 134);
            this.txtHeader2.Name = "txtHeader2";
            this.txtHeader2.Size = new System.Drawing.Size(380, 23);
            this.txtHeader2.TabIndex = 7;
            // 
            // lblHeader3
            // 
            this.lblHeader3.AutoSize = true;
            this.lblHeader3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.lblHeader3.Location = new System.Drawing.Point(10, 163);
            this.lblHeader3.Name = "lblHeader3";
            this.lblHeader3.Size = new System.Drawing.Size(155, 13);
            this.lblHeader3.TabIndex = 8;
            this.lblHeader3.Text = "Contact & GSTIN (Header 3):";
            // 
            // txtHeader3
            // 
            this.txtHeader3.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeader3.Location = new System.Drawing.Point(10, 180);
            this.txtHeader3.Name = "txtHeader3";
            this.txtHeader3.Size = new System.Drawing.Size(380, 23);
            this.txtHeader3.TabIndex = 9;
            // 
            // 
            // grpWaJsUpdate
            // 
            this.grpWaJsUpdate.Controls.Add(this.lblWaJsVersion);
            this.grpWaJsUpdate.Controls.Add(this.lblWaJsStatus);
            this.grpWaJsUpdate.Controls.Add(this.chkAutoUpdateWaJs);
            this.grpWaJsUpdate.Controls.Add(this.btnCheckWaJsUpdate);
            this.grpWaJsUpdate.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpWaJsUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.grpWaJsUpdate.Location = new System.Drawing.Point(8, 473);
            this.grpWaJsUpdate.Name = "grpWaJsUpdate";
            this.grpWaJsUpdate.Padding = new System.Windows.Forms.Padding(8);
            this.grpWaJsUpdate.Size = new System.Drawing.Size(400, 88);
            this.grpWaJsUpdate.TabIndex = 2;
            this.grpWaJsUpdate.TabStop = false;
            this.grpWaJsUpdate.Text = "WA-JS Engine Updates";
            // 
            // lblWaJsVersion
            // 
            this.lblWaJsVersion.AutoSize = true;
            this.lblWaJsVersion.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaJsVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(140)))), ((int)(((byte)(126)))));
            this.lblWaJsVersion.Location = new System.Drawing.Point(10, 20);
            this.lblWaJsVersion.Name = "lblWaJsVersion";
            this.lblWaJsVersion.Size = new System.Drawing.Size(125, 15);
            this.lblWaJsVersion.TabIndex = 0;
            this.lblWaJsVersion.Text = "Local Version: v4.6.0";
            // 
            // lblWaJsStatus
            // 
            this.lblWaJsStatus.AutoSize = true;
            this.lblWaJsStatus.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaJsStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblWaJsStatus.Location = new System.Drawing.Point(10, 38);
            this.lblWaJsStatus.Name = "lblWaJsStatus";
            this.lblWaJsStatus.Size = new System.Drawing.Size(78, 13);
            this.lblWaJsStatus.TabIndex = 1;
            this.lblWaJsStatus.Text = "Status: Ready";
            // 
            // chkAutoUpdateWaJs
            // 
            this.chkAutoUpdateWaJs.AutoSize = true;
            this.chkAutoUpdateWaJs.Checked = true;
            this.chkAutoUpdateWaJs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoUpdateWaJs.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoUpdateWaJs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.chkAutoUpdateWaJs.Location = new System.Drawing.Point(10, 58);
            this.chkAutoUpdateWaJs.Name = "chkAutoUpdateWaJs";
            this.chkAutoUpdateWaJs.Size = new System.Drawing.Size(193, 17);
            this.chkAutoUpdateWaJs.TabIndex = 2;
            this.chkAutoUpdateWaJs.Text = "Auto-check && update on startup";
            this.chkAutoUpdateWaJs.UseVisualStyleBackColor = true;
            // 
            // btnCheckWaJsUpdate
            // 
            this.btnCheckWaJsUpdate.BackColor = System.Drawing.Color.White;
            this.btnCheckWaJsUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckWaJsUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(210)))));
            this.btnCheckWaJsUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckWaJsUpdate.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckWaJsUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.btnCheckWaJsUpdate.Location = new System.Drawing.Point(240, 20);
            this.btnCheckWaJsUpdate.Name = "btnCheckWaJsUpdate";
            this.btnCheckWaJsUpdate.Size = new System.Drawing.Size(150, 32);
            this.btnCheckWaJsUpdate.TabIndex = 3;
            this.btnCheckWaJsUpdate.Text = "🔄 Check Update";
            this.btnCheckWaJsUpdate.UseVisualStyleBackColor = false;
            this.btnCheckWaJsUpdate.Click += new System.EventHandler(this.btnCheckWaJsUpdate_Click);
            // 
            // grpAppUpdate
            // 
            this.grpAppUpdate.Controls.Add(this.lblAppVersion);
            this.grpAppUpdate.Controls.Add(this.lblAppUpdateStatus);
            this.grpAppUpdate.Controls.Add(this.chkAutoCheckAppUpdates);
            this.grpAppUpdate.Controls.Add(this.btnCheckAppUpdate);
            this.grpAppUpdate.Controls.Add(this.lblAppUpdateSourcePrompt);
            this.grpAppUpdate.Controls.Add(this.txtAppUpdateSource);
            this.grpAppUpdate.Controls.Add(this.prgAppUpdate);
            this.grpAppUpdate.Controls.Add(this.lblAppUpdateProgress);
            this.grpAppUpdate.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAppUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.grpAppUpdate.Location = new System.Drawing.Point(8, 568);
            this.grpAppUpdate.Name = "grpAppUpdate";
            this.grpAppUpdate.Padding = new System.Windows.Forms.Padding(8);
            this.grpAppUpdate.Size = new System.Drawing.Size(400, 165);
            this.grpAppUpdate.TabIndex = 3;
            this.grpAppUpdate.TabStop = false;
            this.grpAppUpdate.Text = "Application Software Updates";
            // 
            // lblAppVersion
            // 
            this.lblAppVersion.AutoSize = true;
            this.lblAppVersion.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(140)))), ((int)(((byte)(126)))));
            this.lblAppVersion.Location = new System.Drawing.Point(10, 20);
            this.lblAppVersion.Name = "lblAppVersion";
            this.lblAppVersion.Size = new System.Drawing.Size(125, 15);
            this.lblAppVersion.TabIndex = 0;
            this.lblAppVersion.Text = "App Version: v1.0.0";
            // 
            // lblAppUpdateStatus
            // 
            this.lblAppUpdateStatus.AutoSize = true;
            this.lblAppUpdateStatus.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppUpdateStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblAppUpdateStatus.Location = new System.Drawing.Point(10, 38);
            this.lblAppUpdateStatus.Name = "lblAppUpdateStatus";
            this.lblAppUpdateStatus.Size = new System.Drawing.Size(78, 13);
            this.lblAppUpdateStatus.TabIndex = 1;
            this.lblAppUpdateStatus.Text = "Status: Ready";
            // 
            // chkAutoCheckAppUpdates
            // 
            this.chkAutoCheckAppUpdates.AutoSize = true;
            this.chkAutoCheckAppUpdates.Checked = true;
            this.chkAutoCheckAppUpdates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCheckAppUpdates.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoCheckAppUpdates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.chkAutoCheckAppUpdates.Location = new System.Drawing.Point(10, 56);
            this.chkAutoCheckAppUpdates.Name = "chkAutoCheckAppUpdates";
            this.chkAutoCheckAppUpdates.Size = new System.Drawing.Size(182, 17);
            this.chkAutoCheckAppUpdates.TabIndex = 2;
            this.chkAutoCheckAppUpdates.Text = "Auto-check updates on startup";
            this.chkAutoCheckAppUpdates.UseVisualStyleBackColor = true;
            // 
            // btnCheckAppUpdate
            // 
            this.btnCheckAppUpdate.BackColor = System.Drawing.Color.White;
            this.btnCheckAppUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckAppUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(210)))));
            this.btnCheckAppUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckAppUpdate.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckAppUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.btnCheckAppUpdate.Location = new System.Drawing.Point(240, 20);
            this.btnCheckAppUpdate.Name = "btnCheckAppUpdate";
            this.btnCheckAppUpdate.Size = new System.Drawing.Size(150, 32);
            this.btnCheckAppUpdate.TabIndex = 3;
            this.btnCheckAppUpdate.Text = "🔄 Check App Update";
            this.btnCheckAppUpdate.UseVisualStyleBackColor = false;
            this.btnCheckAppUpdate.Click += new System.EventHandler(this.btnCheckAppUpdate_Click);
            // 
            // lblAppUpdateSourcePrompt
            // 
            this.lblAppUpdateSourcePrompt.AutoSize = true;
            this.lblAppUpdateSourcePrompt.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppUpdateSourcePrompt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.lblAppUpdateSourcePrompt.Location = new System.Drawing.Point(10, 78);
            this.lblAppUpdateSourcePrompt.Name = "lblAppUpdateSourcePrompt";
            this.lblAppUpdateSourcePrompt.Size = new System.Drawing.Size(262, 12);
            this.lblAppUpdateSourcePrompt.TabIndex = 4;
            this.lblAppUpdateSourcePrompt.Text = "Update Source (GitHub owner/repo or version.json URL):";
            // 
            // txtAppUpdateSource
            // 
            this.txtAppUpdateSource.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAppUpdateSource.Location = new System.Drawing.Point(10, 94);
            this.txtAppUpdateSource.Name = "txtAppUpdateSource";
            this.txtAppUpdateSource.Size = new System.Drawing.Size(380, 22);
            this.txtAppUpdateSource.TabIndex = 5;
            // 
            // prgAppUpdate
            // 
            this.prgAppUpdate.Location = new System.Drawing.Point(10, 121);
            this.prgAppUpdate.Name = "prgAppUpdate";
            this.prgAppUpdate.Size = new System.Drawing.Size(380, 14);
            this.prgAppUpdate.TabIndex = 6;
            this.prgAppUpdate.Visible = false;
            // 
            // lblAppUpdateProgress
            // 
            this.lblAppUpdateProgress.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppUpdateProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblAppUpdateProgress.Location = new System.Drawing.Point(10, 139);
            this.lblAppUpdateProgress.Name = "lblAppUpdateProgress";
            this.lblAppUpdateProgress.Size = new System.Drawing.Size(380, 16);
            this.lblAppUpdateProgress.TabIndex = 7;
            this.lblAppUpdateProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAppUpdateProgress.Visible = false;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(140)))), ((int)(((byte)(126)))));
            this.btnSaveSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveSettings.FlatAppearance.BorderSize = 0;
            this.btnSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSettings.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSettings.ForeColor = System.Drawing.Color.White;
            this.btnSaveSettings.Location = new System.Drawing.Point(8, 742);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(400, 36);
            this.btnSaveSettings.TabIndex = 4;
            this.btnSaveSettings.Text = "💾 Save Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = false;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // lblSettingsStatus
            // 
            this.lblSettingsStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettingsStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblSettingsStatus.Location = new System.Drawing.Point(8, 782);
            this.lblSettingsStatus.Name = "lblSettingsStatus";
            this.lblSettingsStatus.Size = new System.Drawing.Size(400, 20);
            this.lblSettingsStatus.TabIndex = 5;
            this.lblSettingsStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlBottomButtons
            // 
            this.pnlBottomButtons.Controls.Add(this.btnInjectWaJs);
            this.pnlBottomButtons.Controls.Add(this.btnReload);
            this.pnlBottomButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomButtons.Location = new System.Drawing.Point(12, 696);
            this.pnlBottomButtons.Name = "pnlBottomButtons";
            this.pnlBottomButtons.Size = new System.Drawing.Size(426, 34);
            this.pnlBottomButtons.TabIndex = 4;
            // 
            // btnInjectWaJs
            // 
            this.btnInjectWaJs.BackColor = System.Drawing.Color.White;
            this.btnInjectWaJs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInjectWaJs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(210)))));
            this.btnInjectWaJs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInjectWaJs.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInjectWaJs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnInjectWaJs.Location = new System.Drawing.Point(4, 3);
            this.btnInjectWaJs.Name = "btnInjectWaJs";
            this.btnInjectWaJs.Size = new System.Drawing.Size(185, 28);
            this.btnInjectWaJs.TabIndex = 0;
            this.btnInjectWaJs.Text = "Re-Inject WA-JS";
            this.btnInjectWaJs.UseVisualStyleBackColor = false;
            this.btnInjectWaJs.Click += new System.EventHandler(this.btnInjectWaJs_Click);
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.White;
            this.btnReload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(210)))));
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnReload.Location = new System.Drawing.Point(237, 3);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(185, 28);
            this.btnReload.TabIndex = 1;
            this.btnReload.Text = "Reload Page";
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // pnlBrowserHost
            // 
            this.pnlBrowserHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBrowserHost.Location = new System.Drawing.Point(0, 0);
            this.pnlBrowserHost.Name = "pnlBrowserHost";
            this.pnlBrowserHost.Size = new System.Drawing.Size(805, 740);
            this.pnlBrowserHost.TabIndex = 0;
            // 
            // statusTimer
            // 
            this.statusTimer.Interval = 3000;
            this.statusTimer.Tick += new System.EventHandler(this.statusTimer_Tick);
            // 
            // sqlPollTimer
            // 
            this.sqlPollTimer.Interval = 10000;
            this.sqlPollTimer.Tick += new System.EventHandler(this.sqlPollTimer_Tick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "All Files (*.*)|*.*|PDF Documents (*.pdf)|*.pdf|Word Documents (*.doc;*.docx)|*.doc;*.docx|Excel Worksheets (*.xls;*.xlsx)|*.xls;*.xlsx|Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            this.openFileDialog.Title = "Select Document or File to Send";
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayMenu;
            this.trayIcon.Text = "WhatsApp Automation";
            this.trayIcon.Visible = true;
            this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
            // 
            // trayMenu
            // 
            this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trayOpenItem,
            this.trayToggleAutoItem,
            this.traySeparator,
            this.trayExitItem});
            this.trayMenu.Name = "trayMenu";
            this.trayMenu.Size = new System.Drawing.Size(181, 76);
            // 
            // trayOpenItem
            // 
            this.trayOpenItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.trayOpenItem.Name = "trayOpenItem";
            this.trayOpenItem.Size = new System.Drawing.Size(180, 22);
            this.trayOpenItem.Text = "Open Window";
            this.trayOpenItem.Click += new System.EventHandler(this.trayOpenItem_Click);
            // 
            // trayToggleAutoItem
            // 
            this.trayToggleAutoItem.Name = "trayToggleAutoItem";
            this.trayToggleAutoItem.Size = new System.Drawing.Size(180, 22);
            this.trayToggleAutoItem.Text = "Start Auto-Send";
            this.trayToggleAutoItem.Click += new System.EventHandler(this.trayToggleAutoItem_Click);
            // 
            // traySeparator
            // 
            this.traySeparator.Name = "traySeparator";
            this.traySeparator.Size = new System.Drawing.Size(177, 6);
            // 
            // trayExitItem
            // 
            this.trayExitItem.Name = "trayExitItem";
            this.trayExitItem.Size = new System.Drawing.Size(180, 22);
            this.trayExitItem.Text = "Exit Application";
            this.trayExitItem.Click += new System.EventHandler(this.trayExitItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 740);
            this.Controls.Add(this.splitMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(950, 650);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WhatsApp Automation - CefSharp + WA-JS + SQL 2008 R2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.tabControls.ResumeLayout(false);
            this.tabAutoSend.ResumeLayout(false);
            this.tabAutoSend.PerformLayout();
            this.grpDbConfig.ResumeLayout(false);
            this.grpDbConfig.PerformLayout();
            this.grpAutoEngine.ResumeLayout(false);
            this.grpAutoEngine.PerformLayout();
            this.tabManualSend.ResumeLayout(false);
            this.tabManualSend.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.grpTemplate.ResumeLayout(false);
            this.grpTemplate.PerformLayout();
            this.grpInvoicePdf.ResumeLayout(false);
            this.grpInvoicePdf.PerformLayout();
            this.grpWaJsUpdate.ResumeLayout(false);
            this.grpWaJsUpdate.PerformLayout();
            this.grpAppUpdate.ResumeLayout(false);
            this.grpAppUpdate.PerformLayout();
            this.pnlBottomButtons.ResumeLayout(false);
            this.trayMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlBrowserHost;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblStatusBadge;
        private System.Windows.Forms.TabControl tabControls;
        private System.Windows.Forms.TabPage tabAutoSend;
        private System.Windows.Forms.GroupBox grpDbConfig;
        private System.Windows.Forms.Label lblDbInstance;
        private System.Windows.Forms.TextBox txtDbInstance;
        private System.Windows.Forms.Label lblDbName;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Button btnTestDbConn;
        private System.Windows.Forms.Button btnSaveDbConfig;
        private System.Windows.Forms.GroupBox grpAutoEngine;
        private System.Windows.Forms.Button btnToggleAutoSend;
        private System.Windows.Forms.Label lblAutoStatus;
        private System.Windows.Forms.CheckBox chkSpecificDate;
        private System.Windows.Forms.DateTimePicker dtpFilterDate;
        private System.Windows.Forms.Label lblAutoLog;
        private System.Windows.Forms.TextBox txtAutoLog;
        private System.Windows.Forms.TabPage tabManualSend;
        private System.Windows.Forms.Label lblMobileNo;
        private System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.Label lblMobileHint;
        private System.Windows.Forms.Label lblDocument;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnClearFile;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblManualLog;
        private System.Windows.Forms.TextBox txtManualLog;
        private System.Windows.Forms.Panel pnlBottomButtons;
        private System.Windows.Forms.Button btnInjectWaJs;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Timer statusTimer;
        private System.Windows.Forms.Timer sqlPollTimer;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.ToolStripMenuItem trayOpenItem;
        private System.Windows.Forms.ToolStripMenuItem trayToggleAutoItem;
        private System.Windows.Forms.ToolStripSeparator traySeparator;
        private System.Windows.Forms.ToolStripMenuItem trayExitItem;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.GroupBox grpTemplate;
        private System.Windows.Forms.Label lblTemplatePrompt;
        private System.Windows.Forms.TextBox txtMsgTemplate;
        private System.Windows.Forms.Label lblTemplateHelp;
        private System.Windows.Forms.GroupBox grpInvoicePdf;
        private System.Windows.Forms.CheckBox chkSendPdf;
        private System.Windows.Forms.Label lblPaperSize;
        private System.Windows.Forms.RadioButton rbPaperA4;
        private System.Windows.Forms.RadioButton rbPaperThermal;
        private System.Windows.Forms.Label lblHeader1;
        private System.Windows.Forms.TextBox txtHeader1;
        private System.Windows.Forms.Label lblHeader2;
        private System.Windows.Forms.TextBox txtHeader2;
        private System.Windows.Forms.Label lblHeader3;
        private System.Windows.Forms.TextBox txtHeader3;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Label lblSettingsStatus;
        private System.Windows.Forms.GroupBox grpWaJsUpdate;
        private System.Windows.Forms.Label lblWaJsVersion;
        private System.Windows.Forms.Label lblWaJsStatus;
        private System.Windows.Forms.CheckBox chkAutoUpdateWaJs;
        private System.Windows.Forms.Button btnCheckWaJsUpdate;
        private System.Windows.Forms.GroupBox grpAppUpdate;
        private System.Windows.Forms.Label lblAppVersion;
        private System.Windows.Forms.Label lblAppUpdateStatus;
        private System.Windows.Forms.CheckBox chkAutoCheckAppUpdates;
        private System.Windows.Forms.Button btnCheckAppUpdate;
        private System.Windows.Forms.Label lblAppUpdateSourcePrompt;
        private System.Windows.Forms.TextBox txtAppUpdateSource;
        private System.Windows.Forms.ProgressBar prgAppUpdate;
        private System.Windows.Forms.Label lblAppUpdateProgress;
    }
}
