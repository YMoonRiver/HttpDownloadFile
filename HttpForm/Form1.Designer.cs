namespace HttpForm
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.downloadFileURI = new System.Windows.Forms.TextBox();
            this.saveFileName = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.downloadButton = new System.Windows.Forms.Button();
            this.progress = new System.Windows.Forms.Label();
            this.downloadLog = new System.Windows.Forms.ListBox();
            this.pauseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "下载文件URI：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "保存文件名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "下载进度：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "下载日志：";
            // 
            // downloadFileURI
            // 
            this.downloadFileURI.Location = new System.Drawing.Point(119, 26);
            this.downloadFileURI.Name = "downloadFileURI";
            this.downloadFileURI.Size = new System.Drawing.Size(410, 21);
            this.downloadFileURI.TabIndex = 4;
            this.downloadFileURI.Text = "https://mirrors.tuna.tsinghua.edu.cn/gnu/bool/bool-0.1.1.tar.gz";
            // 
            // saveFileName
            // 
            this.saveFileName.Location = new System.Drawing.Point(119, 64);
            this.saveFileName.Name = "saveFileName";
            this.saveFileName.Size = new System.Drawing.Size(317, 21);
            this.saveFileName.TabIndex = 5;
            this.saveFileName.Text = "F:\\\\HttpDownload\\\\bool-0.1.1.tar.gz";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(119, 100);
            this.progressBar.Maximum = 5;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(354, 18);
            this.progressBar.TabIndex = 6;
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(454, 62);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(75, 23);
            this.downloadButton.TabIndex = 7;
            this.downloadButton.Text = "下载";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.DownloadFile);
            // 
            // progress
            // 
            this.progress.AutoSize = true;
            this.progress.Location = new System.Drawing.Point(488, 103);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(17, 12);
            this.progress.TabIndex = 9;
            this.progress.Text = "0%";
            // 
            // downloadLog
            // 
            this.downloadLog.FormattingEnabled = true;
            this.downloadLog.ItemHeight = 12;
            this.downloadLog.Location = new System.Drawing.Point(43, 166);
            this.downloadLog.Name = "downloadLog";
            this.downloadLog.Size = new System.Drawing.Size(486, 220);
            this.downloadLog.TabIndex = 10;
            // 
            // pauseButton
            // 
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new System.Drawing.Point(454, 62);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 23);
            this.pauseButton.TabIndex = 8;
            this.pauseButton.Text = "暂停";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.StopDownload);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 417);
            this.Controls.Add(this.downloadLog);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.saveFileName);
            this.Controls.Add(this.downloadFileURI);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.downloadButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "多线程文件下载";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox downloadFileURI;
        private System.Windows.Forms.TextBox saveFileName;
        public System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button downloadButton;
        public System.Windows.Forms.Label progress;
        public System.Windows.Forms.ListBox downloadLog;
        private System.Windows.Forms.Button pauseButton;
    }
}

