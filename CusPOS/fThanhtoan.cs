using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
using CDTDatabase;
namespace CusPOS
{
    public partial class fThanhtoan : DevExpress.XtraEditors.XtraForm
    {
        Database _db = Database.NewDataDatabase();
        DataTable dmphong;
        public int returnValue = -1;
        public string maphong = "";
        public fThanhtoan()
        {
            InitializeComponent();
            string sql;
            sql = "select MaPhong,TenPhong from dmPhong where MaTT='IN'";
            dmphong = _db.GetDataTable(sql);
            gridLookUpEdit1.Properties.DataSource = dmphong;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                returnValue = 0;
                this.Dispose();
            }
            else
            {
                if (gridLookUpEdit1.EditValue != null)
                {
                    maphong = gridLookUpEdit1.EditValue.ToString();
                    this.returnValue = 1;
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Chưa chọn phòng");
                }
            }
        }
    }
}