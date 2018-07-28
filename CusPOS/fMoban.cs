using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
using CDTDatabase;

using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
namespace CusPOS
{
    public partial class fMoban : DevExpress.XtraEditors.XtraForm
    {
        Database db = Database.NewDataDatabase();
        cBan _cban;
        public cBan Ban
        {
            get { return _cban; }
            set
            {
                _cban = value;
                bs = new BindingSource();
                bs.DataSource = _cban.tb;
                gridControl1.DataSource = bs;
                txtSoBan.Text = _cban.tSoBan;
                calcEdit1.EditValue = _cban.tTien;
            }
        }
        public fMoban()
        {
            InitializeComponent();
            this.Resize += new EventHandler(fMoban_Resize);
            Khoitao();
            gridControl1.KeyUp += new KeyEventHandler(gridControl1_KeyUp);
        }

        void gridControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (_cban.tb.Rows.Count > 0 && bs.Current != null && e.KeyCode == Keys.F4)
            {
                bs.RemoveCurrent();
            }
        }
        private void Khoitao()
        {
            string sql = "select maloaimon, tenloaimon from dmloaimon";
            tbLoaiMon = db.GetDataTable(sql);
            sql = "select mamon, tenmon, giaban, hinh, maloaimon from dmmon";
            tbMon = db.GetDataTable(sql);
            repositoryItemGridLookUpEdit1.DataSource = tbMon;
            pcLoaiMon.Visible = true;
            panelControl1.Controls.Add(pcLoaiMon);
            pcLoaiMon.Width = 10;
            pcLoaiMon.Height = panelControl1.Height;
            int x = 0;
            pcLoaiMon.Left = sLeft.Width;

            foreach (DataRow drLM in tbLoaiMon.Rows)
            {
                SimpleButton sp = new SimpleButton();
                sp.Text = drLM["tenloaimon"].ToString();
                sp.Tag = drLM["maloaimon"].ToString();
                sp.Width = 100;
                sp.Height = pcLoaiMon.Height;
                sp.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                sp.ForeColor = Color.Firebrick;
                sp.Left = x;
                sp.Top = 0;
                x += sp.Width;
                pcLoaiMon.Controls.Add(sp);
                pcLoaiMon.Width += sp.Width;
                sp.Click += new EventHandler(sp_Click);
                //Khởi tạo món
                DataRow[] ldrMon = tbMon.Select("maloaimon='" + sp.Tag.ToString() + "'");
                PanelControl pc = new PanelControl();
                pc.Text = sp.Tag.ToString();
                pc.Visible = true;
                this.Controls.Add(pc);
                pc.BringToFront();
                pc.Dock = DockStyle.Fill;

                lpc.Add(pc);
                int xt = 0;
                int yt = 0;
                foreach (DataRow drMon in ldrMon)
                {
                    SimpleButton sb = new SimpleButton();
                    sb.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                    sb.Width = 120;
                    sb.Height = 100;
                    sb.Visible = true;
                    sb.Text = drMon["Tenmon"].ToString() + "\n" + double.Parse(drMon["GiaBan"].ToString()).ToString("### ### ### ###");
                    
                    sb.Tag = drMon;
                    pc.Controls.Add(sb);
                    sb.Top = yt;
                    sb.Left = xt;
                    if (xt < this.Width - panelControl4.Width - 2 * sb.Width)
                    {
                        xt += sb.Width;
                    }
                    else
                    {
                        xt = 0;
                        yt += 100;
                    }
                    sb.Click += new EventHandler(sb_Click);
                }
            }
        }

        void sb_Click(object sender, EventArgs e)
        {
            DataRow drMon=(sender as SimpleButton).Tag as DataRow;
            DataRow[] ldr = _cban.tb.Select("MaMon='" + drMon["Mamon"].ToString() + "'");
            if (ldr.Length == 0)
            {
                DataRow dr = _cban.tb.NewRow();
                dr["mtposid"] = _cban.id;
                dr["ctposid"] = Guid.NewGuid();
                dr["mamon"] = drMon["Mamon"];
                dr["tenmon"] = drMon["TenMon"];
                dr["dongia"] = drMon["Giaban"];
                dr["soluong"] = 1;
                _cban.tb.Rows.Add(dr);
            }
            else
            {
                ldr[0]["soluong"] = double.Parse(ldr[0]["soluong"].ToString()) + 1;
            }
            calcEdit1.EditValue = _cban.tTien;
        }

        void fMoban_Resize(object sender, EventArgs e)
        {
            foreach (PanelControl p1 in lpc)
            {
                int x = 0; int y = 0;
                foreach (Control cb in p1.Controls)
                {
                    cb.Top = y;
                    cb.Left = x;
                    if (x > this.Width - panelControl4.Width - 2 * cb.Width)
                    {
                        x = 0;
                        y += cb.Height ;
                    }
                    else
                    {
                        x += cb.Width;
                    }
                }

            }
        }

        private void sLeft_Click(object sender, EventArgs e)
        {
          if(pcLoaiMon.Left<0)  pcLoaiMon.Left += 100;
        }

        private void sRight_Click(object sender, EventArgs e)
        {
           if(pcLoaiMon.Width+ pcLoaiMon.Left>100) pcLoaiMon.Left += -100;
        }
        PanelControl pcLoaiMon = new PanelControl();
        List<PanelControl> lpc = new List<PanelControl>();
        DataTable tbLoaiMon;
        DataTable tbMon;
        BindingSource bs = new BindingSource();
        private void fMoban_Load(object sender, EventArgs e)
        {
            //Khởi tạo nhóm món

            
        }

        void sp_Click(object sender, EventArgs e)
        {
            foreach (PanelControl pn in lpc)
            {
                if (pn.Text == (sender as SimpleButton).Tag.ToString()) pn.BringToFront();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            _cban.Save();
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Print();
        }
        private void Print()
        {
            string path;
            DevExpress.XtraReports.UI.XtraReport rptTmp = null;
            if (Config.GetValue("DuongDanBaoCao") != null)
                path = Config.GetValue("DuongDanBaoCao").ToString() + "\\" + Config.GetValue("Package").ToString() + "\\" + "billPOS.repx";
            else
                path = Application.StartupPath + "\\Reports\\" + Config.GetValue("Package").ToString() + "\\" + "billPOS.repx";

            if (System.IO.File.Exists(path))
                rptTmp = DevExpress.XtraReports.UI.XtraReport.FromFile(path, true);
            else
                rptTmp = new DevExpress.XtraReports.UI.XtraReport();

            if (rptTmp != null)
            {
                rptTmp.DataSource = _cban.tb;
                rptTmp.ScriptReferences = new string[] { Application.StartupPath + "\\CDTLib.dll" };
                SetVariables(rptTmp);
                DevExpress.XtraReports.UI.XRControl xrcTongTien = rptTmp.FindControl("TongTien", true);
                if (xrcTongTien != null)
                    xrcTongTien.Text = double.Parse(_cban.tTien.ToString()).ToString("### ### ### ###");
                //DevExpress.XtraReports.UI.XRControl xrcDatt = rptTmp.FindControl("DaTT", true);
                //if (xrcDatt != null)
                //    xrcDatt.Text = double.Parse(cDaTT.EditValue.ToString()).ToString("### ### ### ###");
                //DevExpress.XtraReports.UI.XRControl xrcConlai = rptTmp.FindControl("Conlai", true);
                //if (xrcConlai != null)
                //    xrcConlai.Text = double.Parse(cConlai.EditValue.ToString()).ToString("### ### ### ###");
                DevExpress.XtraReports.UI.XRControl xrcID = rptTmp.FindControl("ID", true);
                if (xrcID != null)
                    xrcID.Text = _cban.id;
                //DevExpress.XtraReports.UI.XRControl xrbID = rptTmp.FindControl("BCID", true);
                //if (xrbID != null)
                //    xrbID.Text = txtCode.Text;

                ////rptTmp.Print();
                rptTmp.ShowPreview();
            }
        }
        private void SetVariables(DevExpress.XtraReports.UI.XtraReport rptTmp)
        {
            foreach (DictionaryEntry de in Config.Variables)
            {
                string key = de.Key.ToString();
                if (key.Contains("@"))
                    key = key.Remove(0, 1);
                XRControl xrc = rptTmp.FindControl(key, true);
                if (xrc != null)
                {
                    string value = de.Value.ToString();
                    try
                    {
                        if (value.Contains("/"))
                            xrc.Text = DateTime.Parse(value).ToShortDateString();
                        else
                            xrc.Text = value;
                    }
                    catch
                    {
                        xrc.Text = value;
                    }
                    xrc = null;
                }
            }
        }
        private void editPrint()
        {
            string path;
            DevExpress.XtraReports.UI.XtraReport rptTmp = null;
            if (Config.GetValue("DuongDanBaoCao") != null)
                path = Config.GetValue("DuongDanBaoCao").ToString() + "\\" + Config.GetValue("Package").ToString() + "\\" + "billPOS.repx";
            else
                path = Application.StartupPath + "\\Reports\\" + Config.GetValue("Package").ToString() + "\\" + "billPOS.repx";

            if (System.IO.File.Exists(path))
                rptTmp = DevExpress.XtraReports.UI.XtraReport.FromFile(path, true);
            else
                rptTmp = new DevExpress.XtraReports.UI.XtraReport();
            if (rptTmp != null)
            {
                rptTmp.DataSource = _cban.tb;
                XRDesignFormEx designForm = new XRDesignFormEx();
                designForm.OpenReport(rptTmp);
                if (System.IO.File.Exists(path))
                    designForm.FileName = path;
                designForm.KeyPreview = true;
                designForm.Show();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            editPrint();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            _cban.Thanhtoan();
            this.Close();
        }

    }
}