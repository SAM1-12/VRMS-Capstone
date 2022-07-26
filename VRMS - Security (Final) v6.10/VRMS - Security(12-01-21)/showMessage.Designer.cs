﻿namespace VRMS___Security__12_01_21_
{
    partial class showMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(showMessage));
            this.lblAdminID2 = new System.Windows.Forms.Label();
            this.curve = new Guna.UI.WinForms.GunaElipse(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pbQCU = new System.Windows.Forms.PictureBox();
            this.btnOk = new Guna.UI.WinForms.GunaGradientButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbQCU)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAdminID2
            // 
            this.lblAdminID2.AutoSize = true;
            this.lblAdminID2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblAdminID2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(49)))));
            this.lblAdminID2.Location = new System.Drawing.Point(70, 90);
            this.lblAdminID2.Name = "lblAdminID2";
            this.lblAdminID2.Size = new System.Drawing.Size(347, 24);
            this.lblAdminID2.TabIndex = 12;
            this.lblAdminID2.Text = "Vehicle Already Inside the Premises";
            this.lblAdminID2.Click += new System.EventHandler(this.lblAdminID2_Click);
            // 
            // curve
            // 
            this.curve.Radius = 10;
            this.curve.TargetControl = this;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(188)))), ((int)(((byte)(239)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pbQCU);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 57);
            this.panel1.TabIndex = 13;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(49)))));
            this.label1.Location = new System.Drawing.Point(57, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 29);
            this.label1.TabIndex = 15;
            this.label1.Text = "NOTICE";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // pbQCU
            // 
            this.pbQCU.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbQCU.Image = ((System.Drawing.Image)(resources.GetObject("pbQCU.Image")));
            this.pbQCU.Location = new System.Drawing.Point(6, 6);
            this.pbQCU.Name = "pbQCU";
            this.pbQCU.Size = new System.Drawing.Size(45, 45);
            this.pbQCU.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbQCU.TabIndex = 14;
            this.pbQCU.TabStop = false;
            this.pbQCU.Click += new System.EventHandler(this.pbQCU_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.AnimationHoverSpeed = 0.07F;
            this.btnOk.AnimationSpeed = 0.03F;
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.BaseColor1 = System.Drawing.Color.White;
            this.btnOk.BaseColor2 = System.Drawing.Color.White;
            this.btnOk.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(49)))));
            this.btnOk.BorderSize = 2;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOk.FocusedColor = System.Drawing.Color.Empty;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(49)))));
            this.btnOk.Image = null;
            this.btnOk.ImageSize = new System.Drawing.Size(20, 20);
            this.btnOk.Location = new System.Drawing.Point(155, 130);
            this.btnOk.Name = "btnOk";
            this.btnOk.OnHoverBaseColor1 = System.Drawing.Color.White;
            this.btnOk.OnHoverBaseColor2 = System.Drawing.Color.White;
            this.btnOk.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(188)))), ((int)(((byte)(239)))));
            this.btnOk.OnHoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(188)))), ((int)(((byte)(239)))));
            this.btnOk.OnHoverImage = null;
            this.btnOk.OnPressedColor = System.Drawing.Color.Black;
            this.btnOk.Radius = 10;
            this.btnOk.Size = new System.Drawing.Size(165, 40);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "OKAY";
            this.btnOk.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // showMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(219)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(500, 200);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblAdminID2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "showMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "showMessage";
            this.Load += new System.EventHandler(this.showMessage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbQCU)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblAdminID2;
        private Guna.UI.WinForms.GunaElipse curve;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbQCU;
        public System.Windows.Forms.Label label1;
        private Guna.UI.WinForms.GunaGradientButton btnOk;
    }
}