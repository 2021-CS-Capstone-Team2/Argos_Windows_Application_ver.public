namespace Stop_Cheating
{
    partial class Student_Form
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Student_Form));
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.LoginPage = new MetroFramework.Controls.MetroTabPage();
            this.Startingmode = new MetroFramework.Controls.MetroTabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.data_label = new MetroFramework.Controls.MetroLabel();
            this.Detectingmode = new MetroFramework.Controls.MetroTabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.process_listbox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.device_listbox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.IP_label = new System.Windows.Forms.Label();
            this.Userinfo_label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DBconnection_label = new System.Windows.Forms.Label();
            this.AudioProcess = new MetroFramework.Controls.MetroTabPage();
            this.audio_panel = new System.Windows.Forms.Panel();
            this.VideoProcess = new MetroFramework.Controls.MetroTabPage();
            this.video_panel = new System.Windows.Forms.Panel();
            this.Tray_icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.Tray_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Submission_button = new System.Windows.Forms.PictureBox();
            this.Zoom_start = new System.Windows.Forms.PictureBox();
            this.RestartButton = new System.Windows.Forms.PictureBox();
            this.Detect_start = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Detecting_mode_label = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Restart_label = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.Wait_second_label = new System.Windows.Forms.Label();
            this.metroTabControl1.SuspendLayout();
            this.Startingmode.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Detectingmode.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.AudioProcess.SuspendLayout();
            this.VideoProcess.SuspendLayout();
            this.Tray_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Submission_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Zoom_start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RestartButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Detect_start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.LoginPage);
            this.metroTabControl1.Controls.Add(this.Startingmode);
            this.metroTabControl1.Controls.Add(this.Detectingmode);
            this.metroTabControl1.Controls.Add(this.AudioProcess);
            this.metroTabControl1.Controls.Add(this.VideoProcess);
            this.metroTabControl1.Location = new System.Drawing.Point(219, 89);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 1;
            this.metroTabControl1.Size = new System.Drawing.Size(1150, 651);
            this.metroTabControl1.TabIndex = 5;
            this.metroTabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.metroTabControl1_Selected);
            // 
            // LoginPage
            // 
            this.LoginPage.HorizontalScrollbarBarColor = true;
            this.LoginPage.Location = new System.Drawing.Point(4, 36);
            this.LoginPage.Name = "LoginPage";
            this.LoginPage.Size = new System.Drawing.Size(1142, 611);
            this.LoginPage.TabIndex = 4;
            this.LoginPage.Text = "Login Page";
            this.LoginPage.VerticalScrollbarBarColor = true;
            // 
            // Startingmode
            // 
            this.Startingmode.Controls.Add(this.Wait_second_label);
            this.Startingmode.Controls.Add(this.label12);
            this.Startingmode.Controls.Add(this.groupBox2);
            this.Startingmode.Controls.Add(this.data_label);
            this.Startingmode.HorizontalScrollbarBarColor = true;
            this.Startingmode.Location = new System.Drawing.Point(4, 36);
            this.Startingmode.Name = "Startingmode";
            this.Startingmode.Size = new System.Drawing.Size(1142, 611);
            this.Startingmode.TabIndex = 0;
            this.Startingmode.Text = "Starting mode";
            this.Startingmode.VerticalScrollbarBarColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Corbel Light", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(64, 92);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(423, 42);
            this.label12.TabIndex = 9;
            this.label12.Text = "* Convert to Detecting Mode";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(71, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 164);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("나눔스퀘어", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.Location = new System.Drawing.Point(81, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(173, 70);
            this.label11.TabIndex = 8;
            this.label11.Text = "Time";
            // 
            // data_label
            // 
            this.data_label.AutoSize = true;
            this.data_label.Location = new System.Drawing.Point(71, 44);
            this.data_label.Name = "data_label";
            this.data_label.Size = new System.Drawing.Size(63, 19);
            this.data_label.TabIndex = 7;
            this.data_label.Text = "datalabel";
            this.data_label.TextChanged += new System.EventHandler(this.data_label_TextChanged);
            // 
            // Detectingmode
            // 
            this.Detectingmode.Controls.Add(this.label5);
            this.Detectingmode.Controls.Add(this.process_listbox);
            this.Detectingmode.Controls.Add(this.label2);
            this.Detectingmode.Controls.Add(this.device_listbox);
            this.Detectingmode.Controls.Add(this.groupBox1);
            this.Detectingmode.HorizontalScrollbarBarColor = true;
            this.Detectingmode.Location = new System.Drawing.Point(4, 36);
            this.Detectingmode.Name = "Detectingmode";
            this.Detectingmode.Size = new System.Drawing.Size(1142, 611);
            this.Detectingmode.TabIndex = 1;
            this.Detectingmode.Text = "Detecting mode";
            this.Detectingmode.VerticalScrollbarBarColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("맑은 고딕 Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(247, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Process Event list";
            // 
            // process_listbox
            // 
            this.process_listbox.BackColor = System.Drawing.Color.Azure;
            this.process_listbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.process_listbox.Font = new System.Drawing.Font("맑은 고딕 Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.process_listbox.FormattingEnabled = true;
            this.process_listbox.ItemHeight = 15;
            this.process_listbox.Location = new System.Drawing.Point(247, 29);
            this.process_listbox.Name = "process_listbox";
            this.process_listbox.Size = new System.Drawing.Size(572, 180);
            this.process_listbox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("맑은 고딕 Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(250, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Device Event list";
            // 
            // device_listbox
            // 
            this.device_listbox.BackColor = System.Drawing.Color.Azure;
            this.device_listbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.device_listbox.Font = new System.Drawing.Font("맑은 고딕 Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.device_listbox.FormattingEnabled = true;
            this.device_listbox.ItemHeight = 15;
            this.device_listbox.Location = new System.Drawing.Point(250, 261);
            this.device_listbox.Name = "device_listbox";
            this.device_listbox.Size = new System.Drawing.Size(572, 180);
            this.device_listbox.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.IP_label);
            this.groupBox1.Controls.Add(this.Userinfo_label);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.DBconnection_label);
            this.groupBox1.Location = new System.Drawing.Point(3, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 205);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // IP_label
            // 
            this.IP_label.AutoSize = true;
            this.IP_label.BackColor = System.Drawing.Color.Transparent;
            this.IP_label.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.IP_label.Location = new System.Drawing.Point(135, 67);
            this.IP_label.Name = "IP_label";
            this.IP_label.Size = new System.Drawing.Size(10, 15);
            this.IP_label.TabIndex = 7;
            this.IP_label.Text = ".";
            // 
            // Userinfo_label
            // 
            this.Userinfo_label.AutoSize = true;
            this.Userinfo_label.BackColor = System.Drawing.Color.Transparent;
            this.Userinfo_label.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Userinfo_label.Location = new System.Drawing.Point(83, 42);
            this.Userinfo_label.Name = "Userinfo_label";
            this.Userinfo_label.Size = new System.Drawing.Size(10, 15);
            this.Userinfo_label.TabIndex = 6;
            this.Userinfo_label.Text = ".";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(14, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "External IP Address  : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(14, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "User info  : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "DB connection status  : ";
            // 
            // DBconnection_label
            // 
            this.DBconnection_label.AutoSize = true;
            this.DBconnection_label.BackColor = System.Drawing.Color.Transparent;
            this.DBconnection_label.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DBconnection_label.ForeColor = System.Drawing.Color.Black;
            this.DBconnection_label.Location = new System.Drawing.Point(152, 17);
            this.DBconnection_label.Name = "DBconnection_label";
            this.DBconnection_label.Size = new System.Drawing.Size(60, 15);
            this.DBconnection_label.TabIndex = 3;
            this.DBconnection_label.Text = "No action";
            // 
            // AudioProcess
            // 
            this.AudioProcess.Controls.Add(this.audio_panel);
            this.AudioProcess.HorizontalScrollbarBarColor = true;
            this.AudioProcess.Location = new System.Drawing.Point(4, 36);
            this.AudioProcess.Name = "AudioProcess";
            this.AudioProcess.Size = new System.Drawing.Size(1142, 611);
            this.AudioProcess.TabIndex = 2;
            this.AudioProcess.Text = "Audio Process";
            this.AudioProcess.VerticalScrollbarBarColor = true;
            // 
            // audio_panel
            // 
            this.audio_panel.Location = new System.Drawing.Point(110, 77);
            this.audio_panel.Name = "audio_panel";
            this.audio_panel.Size = new System.Drawing.Size(486, 337);
            this.audio_panel.TabIndex = 3;
            // 
            // VideoProcess
            // 
            this.VideoProcess.Controls.Add(this.video_panel);
            this.VideoProcess.HorizontalScrollbarBarColor = true;
            this.VideoProcess.Location = new System.Drawing.Point(4, 36);
            this.VideoProcess.Name = "VideoProcess";
            this.VideoProcess.Size = new System.Drawing.Size(1142, 611);
            this.VideoProcess.TabIndex = 3;
            this.VideoProcess.Text = "Video Process";
            this.VideoProcess.VerticalScrollbarBarColor = true;
            // 
            // video_panel
            // 
            this.video_panel.Location = new System.Drawing.Point(75, 62);
            this.video_panel.Name = "video_panel";
            this.video_panel.Size = new System.Drawing.Size(486, 337);
            this.video_panel.TabIndex = 2;
            // 
            // Tray_icon
            // 
            this.Tray_icon.Icon = ((System.Drawing.Icon)(resources.GetObject("Tray_icon.Icon")));
            this.Tray_icon.Text = "Argos";
            this.Tray_icon.Visible = true;
            // 
            // Tray_menu
            // 
            this.Tray_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.Tray_menu.Name = "Tray_menu";
            this.Tray_menu.Size = new System.Drawing.Size(105, 48);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Submission_button
            // 
            this.Submission_button.BackColor = System.Drawing.Color.SteelBlue;
            this.Submission_button.Image = global::Stop_Cheating.Properties.Resources.submission__1_;
            this.Submission_button.Location = new System.Drawing.Point(44, 596);
            this.Submission_button.Name = "Submission_button";
            this.Submission_button.Size = new System.Drawing.Size(88, 86);
            this.Submission_button.TabIndex = 16;
            this.Submission_button.TabStop = false;
            this.Submission_button.Click += new System.EventHandler(this.Submission_button_Click);
            // 
            // Zoom_start
            // 
            this.Zoom_start.BackColor = System.Drawing.Color.SteelBlue;
            this.Zoom_start.Image = global::Stop_Cheating.Properties.Resources.zoom__1_;
            this.Zoom_start.Location = new System.Drawing.Point(44, 217);
            this.Zoom_start.Name = "Zoom_start";
            this.Zoom_start.Size = new System.Drawing.Size(88, 86);
            this.Zoom_start.TabIndex = 15;
            this.Zoom_start.TabStop = false;
            this.Zoom_start.Click += new System.EventHandler(this.Zoom_start_Click);
            // 
            // RestartButton
            // 
            this.RestartButton.BackColor = System.Drawing.Color.SteelBlue;
            this.RestartButton.Image = global::Stop_Cheating.Properties.Resources.restart__1_;
            this.RestartButton.Location = new System.Drawing.Point(44, 408);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(88, 86);
            this.RestartButton.TabIndex = 14;
            this.RestartButton.TabStop = false;
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // Detect_start
            // 
            this.Detect_start.BackColor = System.Drawing.Color.SteelBlue;
            this.Detect_start.Image = global::Stop_Cheating.Properties.Resources.cctv1;
            this.Detect_start.Location = new System.Drawing.Point(44, 63);
            this.Detect_start.Name = "Detect_start";
            this.Detect_start.Size = new System.Drawing.Size(88, 86);
            this.Detect_start.TabIndex = 13;
            this.Detect_start.TabStop = false;
            this.Detect_start.Click += new System.EventHandler(this.Detect_start_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.pictureBox1.Location = new System.Drawing.Point(-6, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(178, 775);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Image = global::Stop_Cheating.Properties.Resources.logo_argos_gray2;
            this.label6.Location = new System.Drawing.Point(216, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 58);
            this.label6.TabIndex = 11;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Detecting_mode_label
            // 
            this.Detecting_mode_label.AutoSize = true;
            this.Detecting_mode_label.BackColor = System.Drawing.Color.SteelBlue;
            this.Detecting_mode_label.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Detecting_mode_label.ForeColor = System.Drawing.Color.White;
            this.Detecting_mode_label.Location = new System.Drawing.Point(27, 152);
            this.Detecting_mode_label.Name = "Detecting_mode_label";
            this.Detecting_mode_label.Size = new System.Drawing.Size(120, 20);
            this.Detecting_mode_label.TabIndex = 17;
            this.Detecting_mode_label.Text = "Detecting Mode";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.SteelBlue;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(47, 306);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 20);
            this.label8.TabIndex = 18;
            this.label8.Text = "Run Zoom";
            // 
            // Restart_label
            // 
            this.Restart_label.AutoSize = true;
            this.Restart_label.BackColor = System.Drawing.Color.SteelBlue;
            this.Restart_label.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Restart_label.ForeColor = System.Drawing.Color.White;
            this.Restart_label.Location = new System.Drawing.Point(58, 501);
            this.Restart_label.Name = "Restart_label";
            this.Restart_label.Size = new System.Drawing.Size(55, 20);
            this.Restart_label.TabIndex = 19;
            this.Restart_label.Text = "Restart";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.SteelBlue;
            this.label10.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(44, 685);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 20);
            this.label10.TabIndex = 20;
            this.label10.Text = "Submission";
            // 
            // Timer1
            // 
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Wait_second_label
            // 
            this.Wait_second_label.AutoSize = true;
            this.Wait_second_label.BackColor = System.Drawing.Color.Transparent;
            this.Wait_second_label.Font = new System.Drawing.Font("Corbel Light", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Wait_second_label.Location = new System.Drawing.Point(64, 376);
            this.Wait_second_label.Name = "Wait_second_label";
            this.Wait_second_label.Size = new System.Drawing.Size(0, 42);
            this.Wait_second_label.TabIndex = 10;
            // 
            // Student_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 763);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Restart_label);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Detecting_mode_label);
            this.Controls.Add(this.Submission_button);
            this.Controls.Add(this.Zoom_start);
            this.Controls.Add(this.RestartButton);
            this.Controls.Add(this.Detect_start);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.metroTabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Student_Form";
            this.Style = MetroFramework.MetroColorStyle.White;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Student_Form_FormClosing);
            this.metroTabControl1.ResumeLayout(false);
            this.Startingmode.ResumeLayout(false);
            this.Startingmode.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Detectingmode.ResumeLayout(false);
            this.Detectingmode.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.AudioProcess.ResumeLayout(false);
            this.VideoProcess.ResumeLayout(false);
            this.Tray_menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Submission_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Zoom_start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RestartButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Detect_start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage Startingmode;
        private MetroFramework.Controls.MetroTabPage Detectingmode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox process_listbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox device_listbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label IP_label;
        private System.Windows.Forms.Label Userinfo_label;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DBconnection_label;
        private System.Windows.Forms.Label label6;
        private MetroFramework.Controls.MetroTabPage AudioProcess;
        private MetroFramework.Controls.MetroTabPage LoginPage;
        private MetroFramework.Controls.MetroTabPage VideoProcess;
        private System.Windows.Forms.Panel video_panel;
        private System.Windows.Forms.Panel audio_panel;
        private System.Windows.Forms.NotifyIcon Tray_icon;
        private System.Windows.Forms.ContextMenuStrip Tray_menu;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox Detect_start;
        private System.Windows.Forms.PictureBox RestartButton;
        private System.Windows.Forms.PictureBox Zoom_start;
        private System.Windows.Forms.PictureBox Submission_button;
        private System.Windows.Forms.Label Detecting_mode_label;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label Restart_label;
        private System.Windows.Forms.Label label10;
        private MetroFramework.Controls.MetroLabel data_label;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label Wait_second_label;
    }
}

