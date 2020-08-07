namespace Assistant.Forms
{
    partial class SeriPortOkumaFormu
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.port = new System.IO.Ports.SerialPort(this.components);
            this.cmbComSelect = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.nudInput = new System.Windows.Forms.NumericUpDown();
            this.btnIptal = new DevExpress.XtraEditors.SimpleButton();
            this.btPortYenile = new DevExpress.XtraEditors.SimpleButton();
            this.lblStokAd = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInput)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 104);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(581, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsStatus
            // 
            this.stsStatus.Name = "stsStatus";
            this.stsStatus.Size = new System.Drawing.Size(62, 17);
            this.stsStatus.Text = "Port seçin!";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(548, 106);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(54, 21);
            this.btnSend.TabIndex = 6;
            this.btnSend.TabStop = false;
            this.btnSend.Text = "Gönder";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtOut
            // 
            this.txtOut.Location = new System.Drawing.Point(204, 106);
            this.txtOut.Name = "txtOut";
            this.txtOut.Size = new System.Drawing.Size(338, 21);
            this.txtOut.TabIndex = 5;
            this.txtOut.TabStop = false;
            // 
            // port
            // 
            this.port.PortName = "X";
            this.port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.port_DataReceived_1);
            // 
            // cmbComSelect
            // 
            this.cmbComSelect.FormattingEnabled = true;
            this.cmbComSelect.Location = new System.Drawing.Point(12, 76);
            this.cmbComSelect.Name = "cmbComSelect";
            this.cmbComSelect.Size = new System.Drawing.Size(68, 21);
            this.cmbComSelect.Sorted = true;
            this.cmbComSelect.TabIndex = 0;
            this.cmbComSelect.TabStop = false;
            this.cmbComSelect.SelectedIndexChanged += new System.EventHandler(this.cmbComSelect_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.Sienna;
            this.label1.Location = new System.Drawing.Point(103, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Veri Gönder :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.UseMnemonic = false;
            // 
            // btKaydet
            // 
            this.btKaydet.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btKaydet.Location = new System.Drawing.Point(433, 33);
            this.btKaydet.Name = "btKaydet";
            this.btKaydet.Size = new System.Drawing.Size(67, 64);
            this.btKaydet.TabIndex = 2;
            this.btKaydet.Text = "Kaydet";
            this.btKaydet.Click += new System.EventHandler(this.btKapat_Click);
            // 
            // nudInput
            // 
            this.nudInput.DecimalPlaces = 4;
            this.nudInput.Font = new System.Drawing.Font("Tahoma", 35F);
            this.nudInput.Location = new System.Drawing.Point(86, 33);
            this.nudInput.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudInput.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.nudInput.Name = "nudInput";
            this.nudInput.Size = new System.Drawing.Size(341, 64);
            this.nudInput.TabIndex = 7;
            // 
            // btnIptal
            // 
            this.btnIptal.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnIptal.Location = new System.Drawing.Point(506, 33);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(67, 64);
            this.btnIptal.TabIndex = 8;
            this.btnIptal.Text = "Ýptal";
            this.btnIptal.Click += new System.EventHandler(this.btIptal_Click);
            // 
            // btPortYenile
            // 
            this.btPortYenile.Location = new System.Drawing.Point(12, 33);
            this.btPortYenile.Name = "btPortYenile";
            this.btPortYenile.Size = new System.Drawing.Size(68, 37);
            this.btPortYenile.TabIndex = 9;
            this.btPortYenile.Text = "Port Yenile";
            this.btPortYenile.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // lblStokAd
            // 
            this.lblStokAd.AutoSize = true;
            this.lblStokAd.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblStokAd.Location = new System.Drawing.Point(88, 6);
            this.lblStokAd.Name = "lblStokAd";
            this.lblStokAd.Size = new System.Drawing.Size(45, 25);
            this.lblStokAd.TabIndex = 10;
            this.lblStokAd.Text = "***";
            // 
            // SeriPortOkumaFormu
            // 
            this.AcceptButton = this.btKaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnIptal;
            this.ClientSize = new System.Drawing.Size(581, 126);
            this.ControlBox = false;
            this.Controls.Add(this.lblStokAd);
            this.Controls.Add(this.btPortYenile);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.nudInput);
            this.Controls.Add(this.btKaydet);
            this.Controls.Add(this.cmbComSelect);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOut);
            this.Controls.Add(this.btnSend);
            this.MaximumSize = new System.Drawing.Size(624, 165);
            this.Name = "SeriPortOkumaFormu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SeriPortOkumaFormu";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSerialPort_FormClosing);
            this.Load += new System.EventHandler(this.frmSerialPort_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsStatus;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtOut;
        private System.IO.Ports.SerialPort port;
        private System.Windows.Forms.ComboBox cmbComSelect;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btKaydet;
        private System.Windows.Forms.NumericUpDown nudInput;
        private DevExpress.XtraEditors.SimpleButton btnIptal;
        private DevExpress.XtraEditors.SimpleButton btPortYenile;
        private System.Windows.Forms.Label lblStokAd;
    }
}

