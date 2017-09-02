namespace SocketClient
{
    partial class FormClient
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
            this.btnBeginListen = new System.Windows.Forms.Button();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbIp = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbMsg = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.tbSendMsg = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBeginListen
            // 
            this.btnBeginListen.Location = new System.Drawing.Point(302, 17);
            this.btnBeginListen.Name = "btnBeginListen";
            this.btnBeginListen.Size = new System.Drawing.Size(90, 24);
            this.btnBeginListen.TabIndex = 1;
            this.btnBeginListen.Text = "连接到服务器";
            this.btnBeginListen.UseVisualStyleBackColor = true;
            this.btnBeginListen.Click += new System.EventHandler(this.btnBeginListen_Click);
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(228, 19);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(68, 20);
            this.tbPort.TabIndex = 0;
            this.tbPort.Text = "8899";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbIp);
            this.groupBox1.Controls.Add(this.tbPort);
            this.groupBox1.Controls.Add(this.btnBeginListen);
            this.groupBox1.Location = new System.Drawing.Point(6, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 58);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // cbIp
            // 
            this.cbIp.FormattingEnabled = true;
            this.cbIp.Location = new System.Drawing.Point(12, 19);
            this.cbIp.Name = "cbIp";
            this.cbIp.Size = new System.Drawing.Size(210, 21);
            this.cbIp.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lbMsg);
            this.groupBox2.Location = new System.Drawing.Point(6, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(398, 297);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Messages";
            // 
            // lbMsg
            // 
            this.lbMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMsg.FormattingEnabled = true;
            this.lbMsg.HorizontalScrollbar = true;
            this.lbMsg.Location = new System.Drawing.Point(3, 16);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.ScrollAlwaysVisible = true;
            this.lbMsg.Size = new System.Drawing.Size(392, 278);
            this.lbMsg.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btnSendMsg);
            this.groupBox3.Controls.Add(this.tbSendMsg);
            this.groupBox3.Location = new System.Drawing.Point(9, 375);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(395, 74);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Send";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Message:";
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendMsg.Location = new System.Drawing.Point(314, 45);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(75, 23);
            this.btnSendMsg.TabIndex = 1;
            this.btnSendMsg.Text = "发送消息";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // tbSendMsg
            // 
            this.tbSendMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSendMsg.Location = new System.Drawing.Point(65, 19);
            this.tbSendMsg.Name = "tbSendMsg";
            this.tbSendMsg.Size = new System.Drawing.Size(324, 20);
            this.tbSendMsg.TabIndex = 0;
            this.tbSendMsg.Text = "Hello, this is CLIENT!";
            this.tbSendMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSendMsg_KeyDown);
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 461);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormClient";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClient_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnBeginListen;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.TextBox tbSendMsg;
        private System.Windows.Forms.ListBox lbMsg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbIp;
    }
}

