using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using FormFactory;
using DataFactory;
using CDTLib;
using System.IO;
using CDTControl;
using CDTDatabase;
using System.Media;
namespace CDTClient
{
    public partial class fNotify : DevExpress.XtraEditors.XtraForm
    {
        Database _structdb = CDTDatabase.Database.NewStructDatabase();
        DataTable tb;
        public fNotify()
        {
            InitializeComponent();
            notifyIcon1.Click += new EventHandler(notifyIcon1_DoubleClick);
            pictureEdit1.Click += new EventHandler(pictureEdit1_EditValueChanged);
            pictureEdit3.Click += new EventHandler(pictureEdit3_EditValueChanged);
        }

         


        private void DoNotNofityAgain()
        {
            if (tb == null || tb.Rows.Count == 0) return;
            foreach (DataRow dr in tb.Rows)
            {
                string sql = "update sysNotify set sStatus=0 where stt=" + dr["Stt"].ToString();
                _structdb.UpdateByNonQuery(sql);
            }
            timer1.Interval = 15000;
        }

        private void deleteNotify()
        {            
            timer1.Interval = 15000;
        }

        void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
            this.BringToFront();
        }

        private void fNotify_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            timer1_Tick(timer1, new EventArgs());

            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width;
            this.Top = Screen.PrimaryScreen.Bounds.Height - this.Height;
            this.notifyIcon1.Text = Config.GetValue("FullName").ToString();
        }
        SoundPlayer player = new SoundPlayer();
        private void timer1_Tick(object sender, EventArgs e)  
        {
            if (chkDisNo.Checked) return;
            tb = _structdb.GetDataSetByStore("GetNofify", new string[] { "@sysuser" }, new object[] { Config.GetValue("sysUserID").ToString() });
            if (tb == null || tb.Rows.Count == 0)
            {
               // timer1.Enabled = false;
                this.Visible= false;
                return;
            }
            
            player.SoundLocation = "stelephone.wav";            
            player.LoadCompleted += new AsyncCompletedEventHandler(player_LoadCompleted);
            
            //nạp vào bộ nhớ
            try
            {
                if (!this.Visible)
                {
                    player.LoadAsync();
                    panelControl1.Controls.Clear();
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        DataRow dr = tb.Rows[i];
                        LabelControl l = new LabelControl();
                        l.Appearance.Font = new System.Drawing.Font("Times New Roman", 11F);
                        l.Appearance.ForeColor = System.Drawing.Color.Blue;
                        l.Appearance.Options.UseFont = true;
                        l.Appearance.Options.UseForeColor = true;
                        l.Size = new System.Drawing.Size(500, 32);
                        l.Location = new System.Drawing.Point(10, 10+ l.Height * i);
                        l.TabIndex = 0;
                        l.Tag = dr;
                        l.Visible = true;
                        panelControl1.Controls.Add(l);
                        l.Click += new System.EventHandler(this.lbWanning_Click);
                        l.Text += "\n Process: " + dr["WFName"].ToString() + ", Task: " + dr["TaskLabel"].ToString() + ",   VC No:" + dr["SoCt"].ToString();
                    }
                    this.Visible = true;
                }
            }
            catch 
            {

            }  
        }
        void player_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //kiểm tra đã nạp xong chưa
            if (player.IsLoadCompleted)
                try
                {
                    player.Play();
                }
                catch (Exception err)
                {
                   
                }
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {
            DoNotNofityAgain();
            this.Visible = false;
            timer1.Enabled = true;
        }

        private void pictureEdit3_EditValueChanged(object sender, EventArgs e)
        {
            this.Visible = false;
            timer1.Interval = 300000;
        }

        private void pictureEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }
        public CDTData GetDataForVoucher(string maCT, string linkItem)
        {
            string sysPackageID = Config.GetValue("sysPackageID").ToString();
            string s = "select * from sysTable where MaCT = '" + maCT + "' and sysPackageID = " + sysPackageID;
            DataTable dt = _structdb.GetDataTable(s);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            CDTData data = DataFactory.DataFactory.Create(DataType.MasterDetail, dt.Rows[0]);
            data.ConditionMaster = data.DrTableMaster["Pk"].ToString() + " = '" + linkItem + "'";
            data.GetData();
            return data;

        }
        private void lbWanning_Click(object sender, EventArgs e)
        {
            LabelControl l = sender as LabelControl;
            DataRow dr = l.Tag as DataRow;

            BindingSource _bindingSource = new BindingSource();
            string maCT;//= gridViewReport.GetFocusedRowCellValue("MACT").ToString();
            maCT = dr["MaCT"].ToString();
            CDTData data1 = GetDataForVoucher(maCT, dr["PkID"].ToString());
            FormDesigner _frmDesigner = new FormDesigner(data1);
            _bindingSource = new BindingSource();
            _bindingSource.DataSource = data1.DsData;
            _bindingSource.DataMember = data1.DsData.Tables[0].TableName;
            _frmDesigner = new FormDesigner(data1, _bindingSource);
            _frmDesigner.formAction = FormAction.Edit;
            FrmMasterDetailDt frmMtDtCt = new FrmMasterDetailDt(_frmDesigner);
            frmMtDtCt.ShowDialog();
            string sql = "update sysNotify set sStatus=0 where stt=" + dr["Stt"].ToString();
            _structdb.UpdateByNonQuery(sql);
            this.Visible = false;
            timer1_Tick(timer1, new EventArgs());
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoNotNofityAgain();
            this.Visible = false;
            timer1.Enabled = true;
        }

        private void chkDisNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}