using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using CDTLib;
using CDTControl;
namespace CDTClient
{
    public partial class Xoasolieu : DevExpress.XtraEditors.XtraForm
    {
        Database _db = Database.NewDataDatabase();
        private LabelControl labelControl1;
        private LabelControl labelControl2;
        private LabelControl labelControl3;
        private CheckEdit checkEdit1;
        private SimpleButton simpleButton1;
        private LabelControl labelControl4;
        private TextEdit textEdit1;
        private DateEdit dateEdit1;
        Database _Structdb = Database.NewStructDatabase();
        public Xoasolieu()
        {
            InitializeComponent();
        }


        private void XtraForm1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Location = new System.Drawing.Point(12, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(572, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Cảnh báo: khởi tạo dữ liệu mới, dữ liệu cũ sẽ mất hoàn toàn";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(148, 99);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(59, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Trước ngày:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(148, 124);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(46, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Password";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(244, 154);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Xóa danh mục";
            this.checkEdit1.Size = new System.Drawing.Size(102, 19);
            this.checkEdit1.TabIndex = 3;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(246, 196);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Chấp nhận";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click_1);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl4.Location = new System.Drawing.Point(148, 67);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(240, 23);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "Bạn có chắc chắn không?";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(246, 121);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.PasswordChar = '*';
            this.textEdit1.Size = new System.Drawing.Size(100, 20);
            this.textEdit1.TabIndex = 6;
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(246, 96);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit1.Size = new System.Drawing.Size(100, 20);
            this.dateEdit1.TabIndex = 7;
            // 
            // Xoasolieu
            // 
            this.ClientSize = new System.Drawing.Size(590, 230);
            this.Controls.Add(this.dateEdit1);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "Xoasolieu";
            this.Text = "Xóa số liệu";
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            _db.HasErrors = false;
            string sysUserID = Config.GetValue("sysUserID").ToString();
            string sysPackageID = Config.GetValue("sysPackageID").ToString();
            string sql = "select * from sysuserpackage a inner join sysuser b on a.sysuserid=b.sysuserid where a.sysUserID=" + sysUserID + "  and syspackageID=" + sysPackageID + " and isAdmin=1 and password='"+ Security.EnCode(textEdit1.Text) + "'";

            DataTable tbtmp = _Structdb.GetDataTable(sql);
            if (tbtmp.Rows.Count == 0)
            {
                sql = "select * from sysuser where sysuserID=" + sysUserID + " and CoreAdmin=1";
                tbtmp = _Structdb.GetDataTable(sql);
                if (tbtmp.Rows.Count == 0)
                {
                    MessageBox.Show("Bạn không có quyền xóa dữ liệu");
                    return;
                }
            }
           List< string> paraname =new List<string>();
            List< object> paravalue =new List<object>();
            paraname.Add("NgayCT");
            paraname.Add("XoaDM");
            if (dateEdit1.EditValue != null)
            {
                paravalue.Add(DateTime.Parse(dateEdit1.EditValue.ToString()));
            }
            else
            {
                paravalue.Add(DateTime.Parse("01/01/1900"));
            }

            if (bool.Parse(checkEdit1.EditValue.ToString()))
            {
                
                paravalue.Add(1);
            }
            else
            {
                paravalue.Add(0);
            }
            _db.UpdateDatabyStore("xoasolieu", paraname.ToArray(), paravalue.ToArray());
            if (!_db.HasErrors) MessageBox.Show("Đã xóa dữ liệu");
        }
    }
}