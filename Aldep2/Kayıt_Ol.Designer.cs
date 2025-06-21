namespace Aldep2
{
    partial class Kayıt_Ol
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
            DevExpress.XtraEditors.Controls.ImageListBoxItemImageOptions ımageListBoxItemImageOptions1 = new DevExpress.XtraEditors.Controls.ImageListBoxItemImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kayıt_Ol));
            DevExpress.XtraEditors.Controls.ImageListBoxItemImageOptions ımageListBoxItemImageOptions2 = new DevExpress.XtraEditors.Controls.ImageListBoxItemImageOptions();
            DevExpress.XtraEditors.Controls.ImageListBoxItemImageOptions ımageListBoxItemImageOptions3 = new DevExpress.XtraEditors.Controls.ImageListBoxItemImageOptions();
            txtTCNo = new DevExpress.XtraEditors.TextEdit();
            txtEposta = new DevExpress.XtraEditors.TextEdit();
            labelControl7 = new DevExpress.XtraEditors.LabelControl();
            ımgYetkiTalep = new DevExpress.XtraEditors.ImageListBoxControl();
            labelControl6 = new DevExpress.XtraEditors.LabelControl();
            txtSifreOnay = new DevExpress.XtraEditors.TextEdit();
            labelControl5 = new DevExpress.XtraEditors.LabelControl();
            txtSifre = new DevExpress.XtraEditors.TextEdit();
            labelControl4 = new DevExpress.XtraEditors.LabelControl();
            txtKullaniciAdi = new DevExpress.XtraEditors.TextEdit();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            txtAdSoyad = new DevExpress.XtraEditors.TextEdit();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)txtTCNo.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtEposta.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ımgYetkiTalep).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSifreOnay.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSifre.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtKullaniciAdi.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtAdSoyad.Properties).BeginInit();
            SuspendLayout();
            // 
            // txtTCNo
            // 
            txtTCNo.Location = new System.Drawing.Point(380, 79);
            txtTCNo.Margin = new System.Windows.Forms.Padding(4);
            txtTCNo.Name = "txtTCNo";
            txtTCNo.Properties.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            txtTCNo.Properties.Appearance.Options.UseFont = true;
            txtTCNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtTCNo.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.SimpleMaskManager));
            txtTCNo.Properties.MaskSettings.Set("mask", "00000000000");
            txtTCNo.Size = new System.Drawing.Size(166, 24);
            txtTCNo.TabIndex = 1;
            // 
            // txtEposta
            // 
            txtEposta.Location = new System.Drawing.Point(380, 229);
            txtEposta.Margin = new System.Windows.Forms.Padding(4);
            txtEposta.Name = "txtEposta";
            txtEposta.Properties.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            txtEposta.Properties.Appearance.Options.UseFont = true;
            txtEposta.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtEposta.Size = new System.Drawing.Size(166, 24);
            txtEposta.TabIndex = 4;
            // 
            // labelControl7
            // 
            labelControl7.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            labelControl7.Appearance.Options.UseFont = true;
            labelControl7.Location = new System.Drawing.Point(274, 236);
            labelControl7.Margin = new System.Windows.Forms.Padding(4);
            labelControl7.Name = "labelControl7";
            labelControl7.ShowLineShadow = false;
            labelControl7.Size = new System.Drawing.Size(60, 18);
            labelControl7.TabIndex = 29;
            labelControl7.Text = "E Posta:";
            // 
            // ımgYetkiTalep
            // 
            ımgYetkiTalep.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            ımgYetkiTalep.Appearance.Options.UseFont = true;
            ımgYetkiTalep.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            ımageListBoxItemImageOptions1.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ımageListBoxItemImageOptions1.SvgImage");
            ımageListBoxItemImageOptions2.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ımageListBoxItemImageOptions2.SvgImage");
            ımageListBoxItemImageOptions3.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ımageListBoxItemImageOptions3.SvgImage");
            ımgYetkiTalep.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageListBoxItem[] { new DevExpress.XtraEditors.Controls.ImageListBoxItem(null, "Yönetici", ımageListBoxItemImageOptions1, null), new DevExpress.XtraEditors.Controls.ImageListBoxItem(null, "Personel", ımageListBoxItemImageOptions2, null), new DevExpress.XtraEditors.Controls.ImageListBoxItem(null, "Misafir", ımageListBoxItemImageOptions3, null) });
            ımgYetkiTalep.Location = new System.Drawing.Point(380, 384);
            ımgYetkiTalep.Margin = new System.Windows.Forms.Padding(4);
            ımgYetkiTalep.Name = "ımgYetkiTalep";
            ımgYetkiTalep.Size = new System.Drawing.Size(166, 111);
            ımgYetkiTalep.TabIndex = 7;
            // 
            // labelControl6
            // 
            labelControl6.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            labelControl6.Appearance.Options.UseFont = true;
            labelControl6.Location = new System.Drawing.Point(250, 384);
            labelControl6.Margin = new System.Windows.Forms.Padding(4);
            labelControl6.Name = "labelControl6";
            labelControl6.Size = new System.Drawing.Size(81, 18);
            labelControl6.TabIndex = 28;
            labelControl6.Text = "Yetki Talep:";
            // 
            // txtSifreOnay
            // 
            txtSifreOnay.Location = new System.Drawing.Point(380, 330);
            txtSifreOnay.Margin = new System.Windows.Forms.Padding(4);
            txtSifreOnay.Name = "txtSifreOnay";
            txtSifreOnay.Properties.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            txtSifreOnay.Properties.Appearance.Options.UseFont = true;
            txtSifreOnay.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtSifreOnay.Size = new System.Drawing.Size(166, 24);
            txtSifreOnay.TabIndex = 6;
            // 
            // labelControl5
            // 
            labelControl5.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            labelControl5.Appearance.Options.UseFont = true;
            labelControl5.Location = new System.Drawing.Point(254, 337);
            labelControl5.Margin = new System.Windows.Forms.Padding(4);
            labelControl5.Name = "labelControl5";
            labelControl5.Size = new System.Drawing.Size(77, 18);
            labelControl5.TabIndex = 27;
            labelControl5.Text = "Şifre Onay:";
            // 
            // txtSifre
            // 
            txtSifre.Location = new System.Drawing.Point(380, 279);
            txtSifre.Margin = new System.Windows.Forms.Padding(4);
            txtSifre.Name = "txtSifre";
            txtSifre.Properties.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            txtSifre.Properties.Appearance.Options.UseFont = true;
            txtSifre.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtSifre.Size = new System.Drawing.Size(166, 24);
            txtSifre.TabIndex = 5;
            // 
            // labelControl4
            // 
            labelControl4.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            labelControl4.Appearance.Options.UseFont = true;
            labelControl4.Location = new System.Drawing.Point(301, 287);
            labelControl4.Margin = new System.Windows.Forms.Padding(4);
            labelControl4.Name = "labelControl4";
            labelControl4.Size = new System.Drawing.Size(37, 18);
            labelControl4.TabIndex = 25;
            labelControl4.Text = "Şifre:";
            // 
            // txtKullaniciAdi
            // 
            txtKullaniciAdi.Location = new System.Drawing.Point(380, 183);
            txtKullaniciAdi.Margin = new System.Windows.Forms.Padding(4);
            txtKullaniciAdi.Name = "txtKullaniciAdi";
            txtKullaniciAdi.Properties.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            txtKullaniciAdi.Properties.Appearance.Options.UseFont = true;
            txtKullaniciAdi.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtKullaniciAdi.Size = new System.Drawing.Size(166, 24);
            txtKullaniciAdi.TabIndex = 3;
            // 
            // labelControl3
            // 
            labelControl3.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            labelControl3.Appearance.Options.UseFont = true;
            labelControl3.Location = new System.Drawing.Point(243, 187);
            labelControl3.Margin = new System.Windows.Forms.Padding(4);
            labelControl3.Name = "labelControl3";
            labelControl3.ShowLineShadow = false;
            labelControl3.Size = new System.Drawing.Size(87, 18);
            labelControl3.TabIndex = 22;
            labelControl3.Text = "Kullanıcı Adı:";
            // 
            // txtAdSoyad
            // 
            txtAdSoyad.Location = new System.Drawing.Point(380, 130);
            txtAdSoyad.Margin = new System.Windows.Forms.Padding(4);
            txtAdSoyad.Name = "txtAdSoyad";
            txtAdSoyad.Properties.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            txtAdSoyad.Properties.Appearance.Options.UseFont = true;
            txtAdSoyad.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtAdSoyad.Size = new System.Drawing.Size(166, 24);
            txtAdSoyad.TabIndex = 2;
            // 
            // labelControl2
            // 
            labelControl2.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            labelControl2.Appearance.Options.UseFont = true;
            labelControl2.Location = new System.Drawing.Point(259, 138);
            labelControl2.Margin = new System.Windows.Forms.Padding(4);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new System.Drawing.Size(73, 18);
            labelControl2.TabIndex = 19;
            labelControl2.Text = "Ad Soyad:";
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Location = new System.Drawing.Point(232, 86);
            labelControl1.Margin = new System.Windows.Forms.Padding(4);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(96, 18);
            labelControl1.TabIndex = 16;
            labelControl1.Text = "TC Kimlik No:";
            // 
            // simpleButton1
            // 
            simpleButton1.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            simpleButton1.Appearance.Options.UseFont = true;
            simpleButton1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton1.ImageOptions.Image");
            simpleButton1.Location = new System.Drawing.Point(358, 556);
            simpleButton1.Margin = new System.Windows.Forms.Padding(4);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new System.Drawing.Size(188, 60);
            simpleButton1.TabIndex = 8;
            simpleButton1.Text = "Kayıt Ol";
            simpleButton1.Click += simpleButton1_Click;
            // 
            // Kayıt_Ol
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            BackgroundImageStore = (System.Drawing.Image)resources.GetObject("$this.BackgroundImageStore");
            ClientSize = new System.Drawing.Size(952, 728);
            Controls.Add(txtTCNo);
            Controls.Add(txtEposta);
            Controls.Add(labelControl7);
            Controls.Add(ımgYetkiTalep);
            Controls.Add(labelControl6);
            Controls.Add(txtSifreOnay);
            Controls.Add(labelControl5);
            Controls.Add(txtSifre);
            Controls.Add(labelControl4);
            Controls.Add(txtKullaniciAdi);
            Controls.Add(labelControl3);
            Controls.Add(txtAdSoyad);
            Controls.Add(labelControl2);
            Controls.Add(labelControl1);
            Controls.Add(simpleButton1);
            IconOptions.Image = (System.Drawing.Image)resources.GetObject("Kayıt_Ol.IconOptions.Image");
            Margin = new System.Windows.Forms.Padding(4);
            Name = "Kayıt_Ol";
            Text = "Kayıt Ol";
            ((System.ComponentModel.ISupportInitialize)txtTCNo.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtEposta.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ımgYetkiTalep).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSifreOnay.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSifre.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtKullaniciAdi.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtAdSoyad.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtTCNo;
        private DevExpress.XtraEditors.TextEdit txtEposta;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ImageListBoxControl ımgYetkiTalep;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtSifreOnay;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtSifre;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtKullaniciAdi;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtAdSoyad;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}