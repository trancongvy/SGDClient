using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using CDTControl;
namespace CusPOS
{
    public partial class cBan :DevExpress.XtraEditors.XtraUserControl
    {
       public DataTable tb;

        Database db = Database.NewDataDatabase();
        public string tSoBan = "";
        public double tTien = 0;
        public string id;
        public string MaKH;
        public cBan(string soban)
        {
            InitializeComponent();
            tSoBan = soban;
            this.Width = 146;
            this.Height = 110;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.MouseMove += new MouseEventHandler(cBan_MouseHover);
            this.MouseLeave += new EventHandler(cBan_MouseLeave);
            this.MouseHover += new EventHandler(cBan_MouseHover);
            this.MouseDown += new MouseEventHandler(cBan_MouseDown);
            this.MouseUp += new MouseEventHandler(cBan_MouseUp);
            this.Click += new EventHandler(cBan_Click);
            getdata();
        }

        private void getdata()
        {
            string sql = "select a.mamon,b.tenmon, soluong, dongia, thanhtien, ctposid, c.mtposid, c.MaKH from ctpos a inner join dmmon b on a.mamon=b.mamon inner join  mtpos c on a.mtposid=c.mtposid where  c.maban='" + tSoBan + "' and (c.DaTT=0 or c.daTT is null) ";
            tb = db.GetDataTable(sql);
            if (tb.Rows.Count > 0) { id = tb.Rows[0]["mtposid"].ToString(); MaKH = tb.Rows[0]["MaKH"].ToString(); }
            else
            {
                id = Guid.NewGuid().ToString();MaKH = "KL";
            }
            GetSum();
            tb.ColumnChanged += new DataColumnChangeEventHandler(tb_ColumnChanged);
        }
        internal void Save()
        {
            //Kiểm tra bàn này có chưa
            db.BeginMultiTrans();
            try
            {
                string sql = "select count(mtposid) from mtpos where mtposid='" + id.ToString() + "'";
                object ex = db.GetValue(sql);
                if (tb.Rows.Count == 0) return;
                if (ex.ToString() == "" || ex.ToString() == "0") //chưa tồn tại
                {
                    sql = "insert into Mtpos (MTPOSID, ngayct, MaPOSloaigia, tile, Maban, TTien, TTienH, Ptck, Ck, Datt, MaKH) values('";
                    sql += id.ToString() + "','" + DateTime.Now.ToShortDateString() + "','BT',1,'" + tSoBan + "'," + tTien.ToString("##############.##") + "," + tTien.ToString("##############.##") + ",0,0,0,'" + MaKH + "')";

                    db.UpdateByNonQuery(sql);
                    if (db.HasErrors)
                    {
                        db.RollbackMultiTrans();
                        return;
                    }
                }
                else //đã tồn tại
                {
                    sql = "update mtpos set TTienH=" + tTien.ToString("##############.##") + ", TTien=" + tTien.ToString("##############.##");
                    sql += " where mtposid='" + id + "'";
                    db.UpdateByNonQuery(sql);
                    if (db.HasErrors)
                    {
                        db.RollbackMultiTrans();
                        return;
                    }
                }
                foreach (DataRow dr in tb.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted)
                    {
                        dr.RejectChanges();
                        string ctid = dr["ctposid"].ToString();
                        sql = "delete ctpos where ctposid='" + ctid + "'";
                        db.UpdateByNonQuery(sql);
                        dr.Delete();
                        if (db.HasErrors)
                        {
                            db.RollbackMultiTrans();
                            return;
                        }
                    }
                    else
                    {
                        sql = "select count(ctposid) from ctpos where ctposid='" + dr["ctposid"].ToString() + "'";
                        object exct = db.GetValue(sql);
                        if (exct.ToString() == "" || exct.ToString() == "0") //chưa tồn tại
                        {
                            sql = "insert into ctpos(mtposid, ctposid,maban,mamon, soluong, dongia, thanhtien,datt) values ('";
                            sql += id.ToString() + "','" + dr["ctposid"].ToString() + "','" + tSoBan + "','" + dr["mamon"].ToString() + "'," + dr["soluong"].ToString() + "," + dr["dongia"].ToString() + "," + dr["thanhtien"].ToString() + ",0)";
                            db.UpdateByNonQuery(sql);
                            if (db.HasErrors)
                            {
                                db.RollbackMultiTrans();
                                return;
                            }
                        }
                        else
                        {
                            sql = "update ctpos set soluong=" + dr["soluong"].ToString() + ", thanhtien=" + dr["thanhtien"].ToString() + " where ctposid='" + dr["ctposid"].ToString() + "'";
                            db.UpdateByNonQuery(sql);
                            if (db.HasErrors)
                            {
                                db.RollbackMultiTrans();
                                return;
                            }
                        }
                    }
                }//end for
                db.EndMultiTrans();
                tb.AcceptChanges();
            }catch
            {
                MessageBox.Show("udpate không thành công");
            }
        }
        internal void Thanhtoan()
        {
            this.Save();
            fThanhtoan f = new fThanhtoan();
            f.ShowDialog();
            if (f.returnValue == -1) return;
            string sql ;
            if (f.returnValue == 0)
            {
                sql = "Update mtpos set ngayct=getdate(),DaTT=1 where MTPOSID='" + id.ToString() + "'";
            }
            else
            {
                sql = "Update mtpos set ngayct=getdate(),DaTT=1,Maphong='" + f.tk + "' where MTPOSID='" + id.ToString() + "'";
            }
            db.UpdateByNonQuery(sql);
            this.getdata();

        }
        void tb_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            try
            {
                if (e.Column.ColumnName == "soluong")
                    e.Row["thanhtien"] = double.Parse(e.Row["dongia"].ToString()) * double.Parse(e.Row["soluong"].ToString());
                GetSum();
            }
            catch { }
        }

        public void GetSum()
        {
            tTien = 0;
            foreach (DataRow dr in tb.Rows)
            {
                tTien += double.Parse(dr["thanhtien"].ToString());
            }
        }
        public event EventHandler ChonBan;
        void cBan_Click(object sender, EventArgs e)
        {
            ChonBan(this, e);
        }

        #region Hiệu ứng
        
        void cBan_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            LinearGradientBrush myBrush = null;
            myBrush = new LinearGradientBrush(ClientRectangle, Color.AliceBlue, Color.CornflowerBlue, LinearGradientMode.ForwardDiagonal);
            g.FillRectangle(myBrush, ClientRectangle);
            Brush brush = new SolidBrush(Color.Navy);

            g.DrawString("Bàn số", new Font("Times New Roman", 10), brush, new PointF(20, 17));
            g.DrawString(tSoBan, new Font("Times New Roman", 15, FontStyle.Bold), brush, new PointF(60, 13));
            g.DrawString("Số tiền", new Font("Times New Roman", 10), brush, new PointF(20, 41));
            brush = new SolidBrush(Color.Red);
            g.DrawString(tTien.ToString("### ### ### ###"), new Font("Times New Roman", 15, FontStyle.Bold), brush, new PointF(20, 64));

        }

        void cBan_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            LinearGradientBrush myBrush = null;
            myBrush = new LinearGradientBrush(ClientRectangle,   Color.AliceBlue, Color.CornflowerBlue,LinearGradientMode.BackwardDiagonal);
            g.FillRectangle(myBrush, ClientRectangle);
            Brush brush = new SolidBrush(Color.Navy);

            g.DrawString("Bàn số", new Font("Times New Roman", 10), brush, new PointF(20, 17));
            g.DrawString(tSoBan, new Font("Times New Roman", 15, FontStyle.Bold), brush, new PointF(60, 13));
            g.DrawString("Số tiền", new Font("Times New Roman", 10), brush, new PointF(20, 41));
            brush = new SolidBrush(Color.Red);
            g.DrawString(tTien.ToString("### ### ### ###"), new Font("Times New Roman", 15, FontStyle.Bold), brush, new PointF(20, 64));

        }

        void cBan_MouseHover(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            LinearGradientBrush myBrush = null;
            myBrush = new LinearGradientBrush(ClientRectangle, Color.CornflowerBlue, Color.AliceBlue,  LinearGradientMode.ForwardDiagonal);
            g.FillRectangle(myBrush, ClientRectangle);
             Brush brush = new SolidBrush(Color.Navy);
            
            g.DrawString("Bàn số", new Font("Times New Roman", 10), brush, new PointF(20, 17));
            g.DrawString(tSoBan, new Font("Times New Roman", 15,FontStyle.Bold), brush, new PointF(60, 13));
            g.DrawString("Số tiền", new Font("Times New Roman", 10), brush, new PointF(20, 41));
            brush = new SolidBrush(Color.Red);
            g.DrawString(tTien.ToString("### ### ### ###"), new Font("Times New Roman", 15, FontStyle.Bold), brush, new PointF(20, 64));

        }

        void cBan_MouseLeave(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            LinearGradientBrush myBrush = null;
            myBrush = new LinearGradientBrush(ClientRectangle, Color.AliceBlue, Color.CornflowerBlue, LinearGradientMode.ForwardDiagonal);
            g.FillRectangle(myBrush, ClientRectangle);
            Brush brush = new SolidBrush(Color.Navy);
           
            g.DrawString("Bàn số", new Font("Times New Roman", 10), brush, new PointF(20, 17));
            g.DrawString(tSoBan, new Font("Times New Roman", 12), brush, new PointF(76, 13));
            g.DrawString("Số tiền", new Font("Times New Roman", 10), brush, new PointF(20, 41));
            brush = new SolidBrush(Color.Red);
            g.DrawString(tTien.ToString("### ### ### ###"), new Font("Times New Roman", 15, FontStyle.Bold), brush, new PointF(20, 64));

        }
        
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            LinearGradientBrush myBrush = null;
            myBrush = new LinearGradientBrush(ClientRectangle, Color.AliceBlue, Color.CornflowerBlue, LinearGradientMode.ForwardDiagonal);
            pevent.Graphics.FillRectangle(myBrush, ClientRectangle);
            Brush brush = new SolidBrush(Color.Navy);
            Graphics g = this.CreateGraphics();
            g.DrawString("Bàn số", new Font("Times New Roman", 10), brush, new PointF(20, 17));
            g.DrawString(tSoBan, new Font("Times New Roman", 12), brush, new PointF(76, 13));
            g.DrawString("Số tiền", new Font("Times New Roman", 10), brush, new PointF(20, 41));
            brush = new SolidBrush(Color.Red);
            g.DrawString(tTien.ToString("### ### ### ###"), new Font("Times New Roman", 15, FontStyle.Bold), brush, new PointF(20, 64));

        }
        #endregion








        
    }
}
