namespace Motor_Encoder
{
    partial class UI_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_Form));
            this.panelTabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabAutoalign = new MetroFramework.Controls.MetroTabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Status = new System.Windows.Forms.TextBox();
            this.labelTime = new System.Windows.Forms.Label();
            this.label_minloss = new System.Windows.Forms.Label();
            this.label_loss = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Lock = new System.Windows.Forms.Button();
            this.Unlock = new System.Windows.Forms.Button();
            this.curing = new System.Windows.Forms.Button();
            this.PreCure = new System.Windows.Forms.Button();
            this.Re_align = new System.Windows.Forms.Button();
            this.auto_align = new System.Windows.Forms.Button();
            this.tabEngineering = new MetroFramework.Controls.MetroTabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LoadAlignmentChart = new System.Windows.Forms.Button();
            this.btn_LoadCurnigChart = new System.Windows.Forms.Button();
            this.btn_LoadChart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ClosedLoopY = new System.Windows.Forms.Button();
            this.yMoveCount = new System.Windows.Forms.TextBox();
            this.yMoveSteps = new System.Windows.Forms.TextBox();
            this.btn_MoveY = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_MoveZ = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ClosedLoopZ = new System.Windows.Forms.Button();
            this.zMoveCount = new System.Windows.Forms.TextBox();
            this.zMoveSteps = new System.Windows.Forms.TextBox();
            this.ReadLoss = new System.Windows.Forms.Button();
            this.getCountsButton = new System.Windows.Forms.Button();
            this.loss = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.xMoveCount = new System.Windows.Forms.TextBox();
            this.xMoveSteps = new System.Windows.Forms.TextBox();
            this.btn_CloseLoopX = new System.Windows.Forms.Button();
            this.btn_MoveX = new System.Windows.Forms.Button();
            this.alignment_test = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.xy_Scan_Area = new System.Windows.Forms.Button();
            this.panelTab = new MetroFramework.Controls.MetroTabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cartesianChart2 = new LiveCharts.WinForms.CartesianChart();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cartesianChart3 = new LiveCharts.WinForms.CartesianChart();
            this.labelbtn = new System.Windows.Forms.Button();
            this.ErrorCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Product = new System.Windows.Forms.ComboBox();
            this.ComPorts = new System.Windows.Forms.ComboBox();
            this.Btn_Init = new System.Windows.Forms.Button();
            this.Btn_Connect = new System.Windows.Forms.Button();
            this.Reference = new System.Windows.Forms.Button();
            this.UI_Timer = new System.Windows.Forms.Timer(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelTabControl.SuspendLayout();
            this.tabAutoalign.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabEngineering.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panelTab.SuspendLayout();
            this.panel5.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTabControl
            // 
            this.panelTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.panelTabControl.Controls.Add(this.tabAutoalign);
            this.panelTabControl.Controls.Add(this.tabEngineering);
            this.panelTabControl.Controls.Add(this.panelTab);
            this.panelTabControl.Controls.Add(this.metroTabPage1);
            this.panelTabControl.Controls.Add(this.metroTabPage2);
            this.panelTabControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular;
            this.panelTabControl.HotTrack = true;
            this.panelTabControl.Location = new System.Drawing.Point(0, 117);
            this.panelTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.panelTabControl.Name = "panelTabControl";
            this.panelTabControl.SelectedIndex = 0;
            this.panelTabControl.Size = new System.Drawing.Size(1009, 532);
            this.panelTabControl.Style = MetroFramework.MetroColorStyle.Orange;
            this.panelTabControl.TabIndex = 0;
            this.panelTabControl.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.panelTabControl.UseSelectable = true;
            // 
            // tabAutoalign
            // 
            this.tabAutoalign.BackColor = System.Drawing.SystemColors.Control;
            this.tabAutoalign.Controls.Add(this.panel3);
            this.tabAutoalign.Controls.Add(this.panel2);
            this.tabAutoalign.HorizontalScrollbarBarColor = true;
            this.tabAutoalign.HorizontalScrollbarHighlightOnWheel = false;
            this.tabAutoalign.HorizontalScrollbarSize = 10;
            this.tabAutoalign.Location = new System.Drawing.Point(4, 41);
            this.tabAutoalign.Name = "tabAutoalign";
            this.tabAutoalign.Size = new System.Drawing.Size(1001, 487);
            this.tabAutoalign.TabIndex = 0;
            this.tabAutoalign.Text = "Auto Alignment";
            this.tabAutoalign.VerticalScrollbarBarColor = true;
            this.tabAutoalign.VerticalScrollbarHighlightOnWheel = false;
            this.tabAutoalign.VerticalScrollbarSize = 10;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.panel3.Controls.Add(this.Status);
            this.panel3.Controls.Add(this.labelTime);
            this.panel3.Controls.Add(this.label_minloss);
            this.panel3.Controls.Add(this.label_loss);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Location = new System.Drawing.Point(260, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(745, 491);
            this.panel3.TabIndex = 3;
            // 
            // Status
            // 
            this.Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status.ForeColor = System.Drawing.Color.LightGray;
            this.Status.Location = new System.Drawing.Point(16, 309);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(695, 35);
            this.Status.TabIndex = 8;
            this.Status.Text = "Status";
            this.Status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.LightGray;
            this.labelTime.Location = new System.Drawing.Point(524, 174);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(199, 73);
            this.labelTime.TabIndex = 7;
            this.labelTime.Text = "00:00";
            // 
            // label_minloss
            // 
            this.label_minloss.AutoSize = true;
            this.label_minloss.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_minloss.ForeColor = System.Drawing.Color.Firebrick;
            this.label_minloss.Location = new System.Drawing.Point(134, 17);
            this.label_minloss.Name = "label_minloss";
            this.label_minloss.Size = new System.Drawing.Size(84, 37);
            this.label_minloss.TabIndex = 5;
            this.label_minloss.Text = "0.00";
            // 
            // label_loss
            // 
            this.label_loss.AutoSize = true;
            this.label_loss.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_loss.ForeColor = System.Drawing.Color.LightGray;
            this.label_loss.Location = new System.Drawing.Point(115, 170);
            this.label_loss.Name = "label_loss";
            this.label_loss.Size = new System.Drawing.Size(162, 73);
            this.label_loss.TabIndex = 5;
            this.label_loss.Text = "0.00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.LightGray;
            this.label8.Location = new System.Drawing.Point(340, 170);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(197, 73);
            this.label8.TabIndex = 5;
            this.label8.Text = "Time:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Firebrick;
            this.label10.Location = new System.Drawing.Point(9, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 37);
            this.label10.TabIndex = 5;
            this.label10.Text = "Min IL:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.LightGray;
            this.label9.Location = new System.Drawing.Point(3, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 73);
            this.label9.TabIndex = 5;
            this.label9.Text = "IL:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panel2.Controls.Add(this.Lock);
            this.panel2.Controls.Add(this.Unlock);
            this.panel2.Controls.Add(this.curing);
            this.panel2.Controls.Add(this.PreCure);
            this.panel2.Controls.Add(this.Re_align);
            this.panel2.Controls.Add(this.auto_align);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 487);
            this.panel2.TabIndex = 2;
            // 
            // Lock
            // 
            this.Lock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.Lock.FlatAppearance.BorderSize = 0;
            this.Lock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.Lock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Lock.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lock.ForeColor = System.Drawing.Color.LightGray;
            this.Lock.Image = ((System.Drawing.Image)(resources.GetObject("Lock.Image")));
            this.Lock.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Lock.Location = new System.Drawing.Point(0, 246);
            this.Lock.Name = "Lock";
            this.Lock.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.Lock.Size = new System.Drawing.Size(260, 51);
            this.Lock.TabIndex = 0;
            this.Lock.Text = "Engage Motors";
            this.Lock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Lock.UseVisualStyleBackColor = false;
            this.Lock.Click += new System.EventHandler(this.Lock_Click);
            // 
            // Unlock
            // 
            this.Unlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.Unlock.FlatAppearance.BorderSize = 0;
            this.Unlock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.Unlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Unlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Unlock.ForeColor = System.Drawing.Color.LightGray;
            this.Unlock.Image = ((System.Drawing.Image)(resources.GetObject("Unlock.Image")));
            this.Unlock.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Unlock.Location = new System.Drawing.Point(0, 196);
            this.Unlock.Name = "Unlock";
            this.Unlock.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.Unlock.Size = new System.Drawing.Size(260, 51);
            this.Unlock.TabIndex = 0;
            this.Unlock.Text = "Unlock Motors";
            this.Unlock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Unlock.UseVisualStyleBackColor = false;
            this.Unlock.Click += new System.EventHandler(this.Unlock_Click);
            // 
            // curing
            // 
            this.curing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.curing.FlatAppearance.BorderSize = 0;
            this.curing.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.curing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.curing.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.curing.ForeColor = System.Drawing.Color.LightGray;
            this.curing.Image = ((System.Drawing.Image)(resources.GetObject("curing.Image")));
            this.curing.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.curing.Location = new System.Drawing.Point(0, 149);
            this.curing.Name = "curing";
            this.curing.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.curing.Size = new System.Drawing.Size(260, 51);
            this.curing.TabIndex = 0;
            this.curing.Text = "Curing";
            this.curing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.curing.UseVisualStyleBackColor = false;
            this.curing.Click += new System.EventHandler(this.curingAsync_Click);
            // 
            // PreCure
            // 
            this.PreCure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.PreCure.FlatAppearance.BorderSize = 0;
            this.PreCure.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.PreCure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PreCure.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreCure.ForeColor = System.Drawing.Color.LightGray;
            this.PreCure.Image = ((System.Drawing.Image)(resources.GetObject("PreCure.Image")));
            this.PreCure.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.PreCure.Location = new System.Drawing.Point(0, 101);
            this.PreCure.Name = "PreCure";
            this.PreCure.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.PreCure.Size = new System.Drawing.Size(260, 51);
            this.PreCure.TabIndex = 0;
            this.PreCure.Text = "Pre Curing";
            this.PreCure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PreCure.UseVisualStyleBackColor = false;
            this.PreCure.Click += new System.EventHandler(this.PreCureAsync_Click);
            // 
            // Re_align
            // 
            this.Re_align.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.Re_align.FlatAppearance.BorderSize = 0;
            this.Re_align.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.Re_align.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Re_align.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Re_align.ForeColor = System.Drawing.Color.LightGray;
            this.Re_align.Image = ((System.Drawing.Image)(resources.GetObject("Re_align.Image")));
            this.Re_align.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Re_align.Location = new System.Drawing.Point(0, 53);
            this.Re_align.Name = "Re_align";
            this.Re_align.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.Re_align.Size = new System.Drawing.Size(260, 51);
            this.Re_align.TabIndex = 0;
            this.Re_align.Text = "Move Out and Align";
            this.Re_align.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Re_align.UseVisualStyleBackColor = false;
            this.Re_align.Click += new System.EventHandler(this.OutAlignAsync_Click);
            // 
            // auto_align
            // 
            this.auto_align.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.auto_align.FlatAppearance.BorderSize = 0;
            this.auto_align.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.auto_align.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.auto_align.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.auto_align.ForeColor = System.Drawing.Color.LightGray;
            this.auto_align.Image = ((System.Drawing.Image)(resources.GetObject("auto_align.Image")));
            this.auto_align.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.auto_align.Location = new System.Drawing.Point(0, 3);
            this.auto_align.Name = "auto_align";
            this.auto_align.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.auto_align.Size = new System.Drawing.Size(260, 51);
            this.auto_align.TabIndex = 0;
            this.auto_align.Text = "Alignment";
            this.auto_align.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.auto_align.UseVisualStyleBackColor = false;
            this.auto_align.Click += new System.EventHandler(this.auto_alginAsync_Click);
            // 
            // tabEngineering
            // 
            this.tabEngineering.Controls.Add(this.panel1);
            this.tabEngineering.HorizontalScrollbarBarColor = true;
            this.tabEngineering.HorizontalScrollbarHighlightOnWheel = false;
            this.tabEngineering.HorizontalScrollbarSize = 10;
            this.tabEngineering.Location = new System.Drawing.Point(4, 41);
            this.tabEngineering.Name = "tabEngineering";
            this.tabEngineering.Size = new System.Drawing.Size(1001, 487);
            this.tabEngineering.TabIndex = 1;
            this.tabEngineering.Text = "Engineering";
            this.tabEngineering.VerticalScrollbarBarColor = true;
            this.tabEngineering.VerticalScrollbarHighlightOnWheel = false;
            this.tabEngineering.VerticalScrollbarSize = 10;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.panel1.Controls.Add(this.LoadAlignmentChart);
            this.panel1.Controls.Add(this.btn_LoadCurnigChart);
            this.panel1.Controls.Add(this.btn_LoadChart);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.ReadLoss);
            this.panel1.Controls.Add(this.getCountsButton);
            this.panel1.Controls.Add(this.loss);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.alignment_test);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.xy_Scan_Area);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1001, 490);
            this.panel1.TabIndex = 41;
            // 
            // LoadAlignmentChart
            // 
            this.LoadAlignmentChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.LoadAlignmentChart.FlatAppearance.BorderSize = 0;
            this.LoadAlignmentChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadAlignmentChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadAlignmentChart.ForeColor = System.Drawing.Color.LightGray;
            this.LoadAlignmentChart.Location = new System.Drawing.Point(767, 123);
            this.LoadAlignmentChart.Name = "LoadAlignmentChart";
            this.LoadAlignmentChart.Size = new System.Drawing.Size(212, 56);
            this.LoadAlignmentChart.TabIndex = 41;
            this.LoadAlignmentChart.Text = "Load Align Chart";
            this.LoadAlignmentChart.UseVisualStyleBackColor = false;
            this.LoadAlignmentChart.Click += new System.EventHandler(this.LoadAlignmentChart_Click);
            // 
            // btn_LoadCurnigChart
            // 
            this.btn_LoadCurnigChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.btn_LoadCurnigChart.FlatAppearance.BorderSize = 0;
            this.btn_LoadCurnigChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LoadCurnigChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LoadCurnigChart.ForeColor = System.Drawing.Color.LightGray;
            this.btn_LoadCurnigChart.Location = new System.Drawing.Point(767, 61);
            this.btn_LoadCurnigChart.Name = "btn_LoadCurnigChart";
            this.btn_LoadCurnigChart.Size = new System.Drawing.Size(212, 56);
            this.btn_LoadCurnigChart.TabIndex = 41;
            this.btn_LoadCurnigChart.Text = "Load Curing Chart";
            this.btn_LoadCurnigChart.UseVisualStyleBackColor = false;
            this.btn_LoadCurnigChart.Click += new System.EventHandler(this.btn_LoadCurigChart_Click);
            // 
            // btn_LoadChart
            // 
            this.btn_LoadChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.btn_LoadChart.FlatAppearance.BorderSize = 0;
            this.btn_LoadChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LoadChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LoadChart.ForeColor = System.Drawing.Color.LightGray;
            this.btn_LoadChart.Location = new System.Drawing.Point(767, -1);
            this.btn_LoadChart.Name = "btn_LoadChart";
            this.btn_LoadChart.Size = new System.Drawing.Size(212, 56);
            this.btn_LoadChart.TabIndex = 41;
            this.btn_LoadChart.Text = "Load All Chart";
            this.btn_LoadChart.UseVisualStyleBackColor = false;
            this.btn_LoadChart.Click += new System.EventHandler(this.btn_LoadChart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.ClosedLoopY);
            this.groupBox2.Controls.Add(this.yMoveCount);
            this.groupBox2.Controls.Add(this.yMoveSteps);
            this.groupBox2.Controls.Add(this.btn_MoveY);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.LightGray;
            this.groupBox2.Location = new System.Drawing.Point(14, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 104);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Y axis";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(232, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "Position";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Steps";
            // 
            // ClosedLoopY
            // 
            this.ClosedLoopY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClosedLoopY.FlatAppearance.BorderSize = 0;
            this.ClosedLoopY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClosedLoopY.Location = new System.Drawing.Point(9, 63);
            this.ClosedLoopY.Name = "ClosedLoopY";
            this.ClosedLoopY.Size = new System.Drawing.Size(99, 26);
            this.ClosedLoopY.TabIndex = 22;
            this.ClosedLoopY.Text = "ClosedLoop Y";
            this.ClosedLoopY.UseMnemonic = false;
            this.ClosedLoopY.UseVisualStyleBackColor = false;
            this.ClosedLoopY.Click += new System.EventHandler(this.ClosedLoopY_Click);
            // 
            // yMoveCount
            // 
            this.yMoveCount.Location = new System.Drawing.Point(235, 50);
            this.yMoveCount.Name = "yMoveCount";
            this.yMoveCount.Size = new System.Drawing.Size(59, 24);
            this.yMoveCount.TabIndex = 19;
            this.yMoveCount.Text = "0";
            // 
            // yMoveSteps
            // 
            this.yMoveSteps.Location = new System.Drawing.Point(134, 50);
            this.yMoveSteps.Name = "yMoveSteps";
            this.yMoveSteps.Size = new System.Drawing.Size(56, 24);
            this.yMoveSteps.TabIndex = 17;
            this.yMoveSteps.Text = "10";
            // 
            // btn_MoveY
            // 
            this.btn_MoveY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btn_MoveY.FlatAppearance.BorderSize = 0;
            this.btn_MoveY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MoveY.Location = new System.Drawing.Point(9, 23);
            this.btn_MoveY.Name = "btn_MoveY";
            this.btn_MoveY.Size = new System.Drawing.Size(99, 23);
            this.btn_MoveY.TabIndex = 16;
            this.btn_MoveY.Text = "Move Y Counts";
            this.btn_MoveY.UseMnemonic = false;
            this.btn_MoveY.UseVisualStyleBackColor = false;
            this.btn_MoveY.Click += new System.EventHandler(this.moveY_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.groupBox1.Controls.Add(this.btn_MoveZ);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ClosedLoopZ);
            this.groupBox1.Controls.Add(this.zMoveCount);
            this.groupBox1.Controls.Add(this.zMoveSteps);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.LightGray;
            this.groupBox1.Location = new System.Drawing.Point(14, 278);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 113);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Z axis";
            // 
            // btn_MoveZ
            // 
            this.btn_MoveZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btn_MoveZ.FlatAppearance.BorderSize = 0;
            this.btn_MoveZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MoveZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_MoveZ.Location = new System.Drawing.Point(9, 19);
            this.btn_MoveZ.Name = "btn_MoveZ";
            this.btn_MoveZ.Size = new System.Drawing.Size(99, 23);
            this.btn_MoveZ.TabIndex = 18;
            this.btn_MoveZ.Text = "Move Z Counts";
            this.btn_MoveZ.UseMnemonic = false;
            this.btn_MoveZ.UseVisualStyleBackColor = false;
            this.btn_MoveZ.Click += new System.EventHandler(this.moveZ_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(232, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 18);
            this.label7.TabIndex = 9;
            this.label7.Text = "Position";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(131, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 18);
            this.label6.TabIndex = 9;
            this.label6.Text = "Steps";
            // 
            // ClosedLoopZ
            // 
            this.ClosedLoopZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClosedLoopZ.FlatAppearance.BorderSize = 0;
            this.ClosedLoopZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClosedLoopZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClosedLoopZ.Location = new System.Drawing.Point(9, 60);
            this.ClosedLoopZ.Name = "ClosedLoopZ";
            this.ClosedLoopZ.Size = new System.Drawing.Size(99, 29);
            this.ClosedLoopZ.TabIndex = 23;
            this.ClosedLoopZ.Text = "ClosedLoop Z";
            this.ClosedLoopZ.UseMnemonic = false;
            this.ClosedLoopZ.UseVisualStyleBackColor = false;
            this.ClosedLoopZ.Click += new System.EventHandler(this.ClosedLoopZ_Click);
            // 
            // zMoveCount
            // 
            this.zMoveCount.Location = new System.Drawing.Point(235, 46);
            this.zMoveCount.Name = "zMoveCount";
            this.zMoveCount.Size = new System.Drawing.Size(59, 24);
            this.zMoveCount.TabIndex = 21;
            this.zMoveCount.Text = "0";
            // 
            // zMoveSteps
            // 
            this.zMoveSteps.Location = new System.Drawing.Point(134, 46);
            this.zMoveSteps.Name = "zMoveSteps";
            this.zMoveSteps.Size = new System.Drawing.Size(56, 24);
            this.zMoveSteps.TabIndex = 20;
            this.zMoveSteps.Text = "10";
            // 
            // ReadLoss
            // 
            this.ReadLoss.Location = new System.Drawing.Point(376, 31);
            this.ReadLoss.Name = "ReadLoss";
            this.ReadLoss.Size = new System.Drawing.Size(75, 23);
            this.ReadLoss.TabIndex = 33;
            this.ReadLoss.Text = "ReadLoss";
            this.ReadLoss.UseMnemonic = false;
            this.ReadLoss.UseVisualStyleBackColor = true;
            this.ReadLoss.Click += new System.EventHandler(this.ReadLoss_Click);
            // 
            // getCountsButton
            // 
            this.getCountsButton.Location = new System.Drawing.Point(376, 79);
            this.getCountsButton.Name = "getCountsButton";
            this.getCountsButton.Size = new System.Drawing.Size(75, 23);
            this.getCountsButton.TabIndex = 38;
            this.getCountsButton.Text = "Get counts";
            this.getCountsButton.UseMnemonic = false;
            this.getCountsButton.UseVisualStyleBackColor = true;
            this.getCountsButton.Click += new System.EventHandler(this.getCountsButton_Click);
            // 
            // loss
            // 
            this.loss.Location = new System.Drawing.Point(457, 33);
            this.loss.Name = "loss";
            this.loss.Size = new System.Drawing.Size(56, 20);
            this.loss.TabIndex = 34;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.xMoveCount);
            this.groupBox3.Controls.Add(this.xMoveSteps);
            this.groupBox3.Controls.Add(this.btn_CloseLoopX);
            this.groupBox3.Controls.Add(this.btn_MoveX);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.LightGray;
            this.groupBox3.Location = new System.Drawing.Point(14, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(332, 112);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "X axis";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(232, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "Position";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "Steps";
            // 
            // xMoveCount
            // 
            this.xMoveCount.Location = new System.Drawing.Point(235, 50);
            this.xMoveCount.Name = "xMoveCount";
            this.xMoveCount.Size = new System.Drawing.Size(59, 24);
            this.xMoveCount.TabIndex = 8;
            // 
            // xMoveSteps
            // 
            this.xMoveSteps.Location = new System.Drawing.Point(134, 50);
            this.xMoveSteps.Name = "xMoveSteps";
            this.xMoveSteps.Size = new System.Drawing.Size(56, 24);
            this.xMoveSteps.TabIndex = 7;
            this.xMoveSteps.Text = "10";
            // 
            // btn_CloseLoopX
            // 
            this.btn_CloseLoopX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btn_CloseLoopX.FlatAppearance.BorderSize = 0;
            this.btn_CloseLoopX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CloseLoopX.Location = new System.Drawing.Point(9, 63);
            this.btn_CloseLoopX.Name = "btn_CloseLoopX";
            this.btn_CloseLoopX.Size = new System.Drawing.Size(99, 30);
            this.btn_CloseLoopX.TabIndex = 0;
            this.btn_CloseLoopX.Text = "ClosedLoop";
            this.btn_CloseLoopX.UseMnemonic = false;
            this.btn_CloseLoopX.UseVisualStyleBackColor = false;
            this.btn_CloseLoopX.Click += new System.EventHandler(this.ClosedLoopX_Click);
            // 
            // btn_MoveX
            // 
            this.btn_MoveX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btn_MoveX.FlatAppearance.BorderSize = 0;
            this.btn_MoveX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MoveX.Location = new System.Drawing.Point(9, 23);
            this.btn_MoveX.Name = "btn_MoveX";
            this.btn_MoveX.Size = new System.Drawing.Size(99, 23);
            this.btn_MoveX.TabIndex = 0;
            this.btn_MoveX.Text = "Move X Counts";
            this.btn_MoveX.UseMnemonic = false;
            this.btn_MoveX.UseVisualStyleBackColor = false;
            this.btn_MoveX.Click += new System.EventHandler(this.moveX_Click);
            // 
            // alignment_test
            // 
            this.alignment_test.Location = new System.Drawing.Point(376, 137);
            this.alignment_test.Name = "alignment_test";
            this.alignment_test.Size = new System.Drawing.Size(102, 23);
            this.alignment_test.TabIndex = 40;
            this.alignment_test.Text = "AlignmentTest";
            this.alignment_test.UseVisualStyleBackColor = true;
            this.alignment_test.Click += new System.EventHandler(this.alignment_test_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(376, 251);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 23);
            this.button2.TabIndex = 39;
            this.button2.Text = "XY align";
            this.button2.UseMnemonic = false;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.XYAlign_Click);
            // 
            // xy_Scan_Area
            // 
            this.xy_Scan_Area.Location = new System.Drawing.Point(376, 197);
            this.xy_Scan_Area.Name = "xy_Scan_Area";
            this.xy_Scan_Area.Size = new System.Drawing.Size(91, 23);
            this.xy_Scan_Area.TabIndex = 39;
            this.xy_Scan_Area.Text = "xyScanArea";
            this.xy_Scan_Area.UseMnemonic = false;
            this.xy_Scan_Area.UseVisualStyleBackColor = true;
            this.xy_Scan_Area.Click += new System.EventHandler(this.xy_Scan_Area_Click);
            // 
            // panelTab
            // 
            this.panelTab.Controls.Add(this.panel5);
            this.panelTab.HorizontalScrollbarBarColor = true;
            this.panelTab.HorizontalScrollbarHighlightOnWheel = false;
            this.panelTab.HorizontalScrollbarSize = 10;
            this.panelTab.Location = new System.Drawing.Point(4, 41);
            this.panelTab.Name = "panelTab";
            this.panelTab.Size = new System.Drawing.Size(1001, 487);
            this.panelTab.TabIndex = 2;
            this.panelTab.Text = "X axis Plot";
            this.panelTab.VerticalScrollbarBarColor = true;
            this.panelTab.VerticalScrollbarHighlightOnWheel = false;
            this.panelTab.VerticalScrollbarSize = 10;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.panel5.Controls.Add(this.cartesianChart1);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(983, 491);
            this.panel5.TabIndex = 2;
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(0, 0);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(984, 487);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.panel7);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 41);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(1001, 487);
            this.metroTabPage1.TabIndex = 3;
            this.metroTabPage1.Text = "Y Axis Plot";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.panel7.Controls.Add(this.cartesianChart2);
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(983, 491);
            this.panel7.TabIndex = 2;
            // 
            // cartesianChart2
            // 
            this.cartesianChart2.Location = new System.Drawing.Point(0, 0);
            this.cartesianChart2.Name = "cartesianChart2";
            this.cartesianChart2.Size = new System.Drawing.Size(983, 491);
            this.cartesianChart2.TabIndex = 4;
            this.cartesianChart2.Text = "cartesianChart1";
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.panel6);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 41);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(1001, 487);
            this.metroTabPage2.TabIndex = 4;
            this.metroTabPage2.Text = "Z Axis Plot";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.panel6.Controls.Add(this.cartesianChart3);
            this.panel6.Location = new System.Drawing.Point(-4, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(987, 491);
            this.panel6.TabIndex = 2;
            // 
            // cartesianChart3
            // 
            this.cartesianChart3.Location = new System.Drawing.Point(4, 0);
            this.cartesianChart3.Name = "cartesianChart3";
            this.cartesianChart3.Size = new System.Drawing.Size(983, 491);
            this.cartesianChart3.TabIndex = 1;
            this.cartesianChart3.Text = "cartesianChart1";
            // 
            // labelbtn
            // 
            this.labelbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.labelbtn.Enabled = false;
            this.labelbtn.FlatAppearance.BorderSize = 0;
            this.labelbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.labelbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelbtn.ForeColor = System.Drawing.Color.LightGray;
            this.labelbtn.Image = ((System.Drawing.Image)(resources.GetObject("labelbtn.Image")));
            this.labelbtn.Location = new System.Drawing.Point(278, 76);
            this.labelbtn.Name = "labelbtn";
            this.labelbtn.Size = new System.Drawing.Size(108, 36);
            this.labelbtn.TabIndex = 6;
            this.labelbtn.Text = "Product";
            this.labelbtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.labelbtn.UseVisualStyleBackColor = false;
            // 
            // ErrorCode
            // 
            this.ErrorCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.ErrorCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorCode.ForeColor = System.Drawing.Color.LightGray;
            this.ErrorCode.Location = new System.Drawing.Point(545, 55);
            this.ErrorCode.Name = "ErrorCode";
            this.ErrorCode.Size = new System.Drawing.Size(313, 26);
            this.ErrorCode.TabIndex = 4;
            this.ErrorCode.Text = "ErrorCode";
            this.ErrorCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(273, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Controller Port";
            // 
            // Product
            // 
            this.Product.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Product.FormattingEnabled = true;
            this.Product.Items.AddRange(new object[] {
            "1XN (SM)",
            "1XN (MM)",
            "VOA"});
            this.Product.Location = new System.Drawing.Point(392, 81);
            this.Product.Name = "Product";
            this.Product.Size = new System.Drawing.Size(112, 28);
            this.Product.TabIndex = 1;
            this.Product.SelectionChangeCommitted += new System.EventHandler(this.Product_SelectionChangeCommitted);
            // 
            // ComPorts
            // 
            this.ComPorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComPorts.FormattingEnabled = true;
            this.ComPorts.Location = new System.Drawing.Point(392, 43);
            this.ComPorts.Name = "ComPorts";
            this.ComPorts.Size = new System.Drawing.Size(112, 28);
            this.ComPorts.TabIndex = 1;
            // 
            // Btn_Init
            // 
            this.Btn_Init.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.Btn_Init.FlatAppearance.BorderSize = 0;
            this.Btn_Init.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.Btn_Init.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Init.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Init.ForeColor = System.Drawing.Color.LightGray;
            this.Btn_Init.Location = new System.Drawing.Point(540, 3);
            this.Btn_Init.Name = "Btn_Init";
            this.Btn_Init.Size = new System.Drawing.Size(128, 46);
            this.Btn_Init.TabIndex = 0;
            this.Btn_Init.Text = "Initialize";
            this.Btn_Init.UseVisualStyleBackColor = false;
            this.Btn_Init.Click += new System.EventHandler(this.Btn_Init_Click);
            // 
            // Btn_Connect
            // 
            this.Btn_Connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.Btn_Connect.FlatAppearance.BorderSize = 0;
            this.Btn_Connect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.Btn_Connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Connect.ForeColor = System.Drawing.Color.LightGray;
            this.Btn_Connect.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Connect.Image")));
            this.Btn_Connect.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Btn_Connect.Location = new System.Drawing.Point(264, 3);
            this.Btn_Connect.Name = "Btn_Connect";
            this.Btn_Connect.Size = new System.Drawing.Size(241, 43);
            this.Btn_Connect.TabIndex = 0;
            this.Btn_Connect.Text = "Connect Controller";
            this.Btn_Connect.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Btn_Connect.UseVisualStyleBackColor = false;
            this.Btn_Connect.Click += new System.EventHandler(this.Btn_Connect_Click);
            // 
            // Reference
            // 
            this.Reference.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.Reference.FlatAppearance.BorderSize = 0;
            this.Reference.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.Reference.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Reference.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reference.ForeColor = System.Drawing.Color.LightGray;
            this.Reference.Location = new System.Drawing.Point(674, 3);
            this.Reference.Name = "Reference";
            this.Reference.Size = new System.Drawing.Size(184, 46);
            this.Reference.TabIndex = 0;
            this.Reference.Text = "Take Reference";
            this.Reference.UseVisualStyleBackColor = false;
            this.Reference.Click += new System.EventHandler(this.Reference_Click);
            // 
            // UI_Timer
            // 
            this.UI_Timer.Enabled = true;
            this.UI_Timer.Interval = 150;
            this.UI_Timer.Tick += new System.EventHandler(this.UI_Timer_Tick);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.labelbtn);
            this.panel4.Controls.Add(this.Btn_Connect);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.ComPorts);
            this.panel4.Controls.Add(this.Product);
            this.panel4.Controls.Add(this.ErrorCode);
            this.panel4.Controls.Add(this.Btn_Init);
            this.panel4.Controls.Add(this.Reference);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1009, 117);
            this.panel4.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(49, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 71);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // UI_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1009, 649);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UI_Form";
            this.Text = "Auto Alignment";
            this.Load += new System.EventHandler(this.UI_Form_Load);
            this.panelTabControl.ResumeLayout(false);
            this.tabAutoalign.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabEngineering.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panelTab.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl panelTabControl;
        private MetroFramework.Controls.MetroTabPage tabAutoalign;
        private MetroFramework.Controls.MetroTabPage tabEngineering;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button curing;
        private System.Windows.Forms.Button PreCure;
        private System.Windows.Forms.Button Re_align;
        private System.Windows.Forms.Button auto_align;
        private System.Windows.Forms.TextBox ErrorCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComPorts;
        private System.Windows.Forms.Button Btn_Init;
        private System.Windows.Forms.Button Btn_Connect;
        private System.Windows.Forms.Button Reference;
        private System.Windows.Forms.Button Unlock;
        private System.Windows.Forms.Button alignment_test;
        private System.Windows.Forms.Button xy_Scan_Area;
        private System.Windows.Forms.Button getCountsButton;
        private System.Windows.Forms.TextBox loss;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_MoveZ;
        private System.Windows.Forms.Button ClosedLoopZ;
        private System.Windows.Forms.TextBox zMoveCount;
        private System.Windows.Forms.TextBox zMoveSteps;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ClosedLoopY;
        private System.Windows.Forms.TextBox yMoveCount;
        private System.Windows.Forms.TextBox yMoveSteps;
        private System.Windows.Forms.Button btn_MoveY;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox xMoveCount;
        private System.Windows.Forms.TextBox xMoveSteps;
        private System.Windows.Forms.Button btn_CloseLoopX;
        private System.Windows.Forms.Button btn_MoveX;
        private System.Windows.Forms.Button ReadLoss;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Product;
        private System.Windows.Forms.Timer UI_Timer;
        private System.Windows.Forms.Label label_loss;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button labelbtn;
        private System.Windows.Forms.Button Lock;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label label_minloss;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox Status;
        private MetroFramework.Controls.MetroTabPage panelTab;
        private System.Windows.Forms.Panel panel5;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Button btn_LoadChart;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private System.Windows.Forms.Panel panel7;
        private LiveCharts.WinForms.CartesianChart cartesianChart2;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private System.Windows.Forms.Panel panel6;
        private LiveCharts.WinForms.CartesianChart cartesianChart3;
        private System.Windows.Forms.Button LoadAlignmentChart;
        private System.Windows.Forms.Button btn_LoadCurnigChart;
    }
}

