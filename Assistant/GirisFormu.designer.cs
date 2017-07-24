using System.ComponentModel;
using DevExpress.Utils.Behaviors;
using DevExpress.XtraEditors;

namespace Assistant
{
    partial class GirisFormu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtKullanici = new DevExpress.XtraEditors.TextEdit();
            this.txtSifre = new DevExpress.XtraEditors.TextEdit();
            this.btnGiris = new DevExpress.XtraEditors.SimpleButton();
            this.btnVazgec = new DevExpress.XtraEditors.SimpleButton();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtKullanici.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSifre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(37, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Kullanıcı:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(37, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(38, 19);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Şifre:";
            // // txtKullanici
            // 
            this.txtKullanici.EditValue = "";
            this.txtKullanici.Location = new System.Drawing.Point(120, 23);
            this.txtKullanici.Name = "txtKullanici";
            this.txtKullanici.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtKullanici.Properties.Appearance.Options.UseFont = true;
            this.txtKullanici.Size = new System.Drawing.Size(100, 26);
            this.txtKullanici.TabIndex = 2;
            // 
            // txtSifre
            // 
            this.txtSifre.EditValue = "";
            this.txtSifre.Location = new System.Drawing.Point(120, 55);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtSifre.Properties.Appearance.Options.UseFont = true;
            this.txtSifre.Properties.PasswordChar = '*';
            this.txtSifre.Size = new System.Drawing.Size(100, 26);
            this.txtSifre.TabIndex = 3;
            // 
            // btnGiris
            // 
            this.btnGiris.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGiris.Appearance.Options.UseFont = true;
            this.btnGiris.Location = new System.Drawing.Point(120, 95);
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.Size = new System.Drawing.Size(100, 33);
            this.btnGiris.TabIndex = 4;
            this.btnGiris.Text = "Giriş";
            this.btnGiris.Click += new System.EventHandler(this.btnGiris_Click);
            // 
            // btnVazgec
            // 
            this.btnVazgec.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnVazgec.Appearance.Options.UseFont = true;
            this.btnVazgec.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnVazgec.Location = new System.Drawing.Point(14, 95);
            this.btnVazgec.Name = "btnVazgec";
            this.btnVazgec.Size = new System.Drawing.Size(100, 33);
            this.btnVazgec.TabIndex = 5;
            this.btnVazgec.Text = "Vazgeç";
            this.btnVazgec.Click += new System.EventHandler(this.btnVazgec_Click);
            // 
            // GirisFormu
            // 
            this.AcceptButton = this.btnGiris;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnVazgec;
            this.ClientSize = new System.Drawing.Size(248, 140);
            this.Controls.Add(this.btnVazgec);
            this.Controls.Add(this.btnGiris);
            this.Controls.Add(this.txtSifre);
            this.Controls.Add(this.txtKullanici);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "GirisFormu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GirisFormu";
            this.Load += new System.EventHandler(this.GirisFormu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtKullanici.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSifre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelControl labelControl1;
        private LabelControl labelControl2;
        private TextEdit txtKullanici;
        private TextEdit txtSifre;
        private SimpleButton btnGiris;
        private SimpleButton btnVazgec;
        private BehaviorManager behaviorManager1;
    }
}