namespace CDTClient
{
    partial class fChonNgayLV
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
            this.vDateEdit1 = new CBSControls.VDateEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.vDateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vDateEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // vDateEdit1
            // 
            this.vDateEdit1.EditValue = null;
            this.vDateEdit1.EnterMoveNextControl = true;
            this.vDateEdit1.Location = new System.Drawing.Point(130, 23);
            this.vDateEdit1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.vDateEdit1.Name = "vDateEdit1";
            this.vDateEdit1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.vDateEdit1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.vDateEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.vDateEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.vDateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.vDateEdit1.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.vDateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.vDateEdit1.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.vDateEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.vDateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.vDateEdit1.Size = new System.Drawing.Size(290, 30);
            this.vDateEdit1.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(180, 104);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(150, 44);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Ok";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // fChonNgayLV
            // 
            this.AcceptButton = this.simpleButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 192);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.vDateEdit1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fChonNgayLV";
            this.Text = "Chọn ngày làm việc";
            this.Load += new System.EventHandler(this.fChonNgayLV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vDateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vDateEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CBSControls.VDateEdit vDateEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}