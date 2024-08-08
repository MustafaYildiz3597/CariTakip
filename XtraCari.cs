using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CariTakip
{
    public partial class XtraCari : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection baglanti = new SqlConnection("Data Source=YOUR_SERVER_NAME;Initial Catalog=YOUR_DATABASE_NAME;User ID=YOUR_USERNAME;Password=YOUR_PASSWORD");
        public XtraAnaSayfa af;
        public XtraCari ca;

        public XtraCari()
        {
            InitializeComponent();
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            goster();
            this.Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (kaydet == true)
            {
                SqlCommand kontrol = new SqlCommand("Select * From TBLCARI where ADISOYADI=@ADISOYADI", baglanti);
                kontrol.Parameters.AddWithValue("@ADISOYADI", TxtAdıSoyadı.Text);
                SqlDataReader dr;
                baglanti.Open();
                dr = kontrol.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Bu isimde kayıtlı bir cari var!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtAdıSoyadı.Focus();
                    baglanti.Close();
                    return;
                }

                baglanti.Close();

                if (MessageBox.Show("Kaydetmek istiyor musunuz?", "Kaydet", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlCommand kmt = new SqlCommand("insert into TBLCARI(ADISOYADI, BAKIYE, DURUM) values(@ADISOYADI, @BAKIYE, @DURUM)", baglanti);
                    kmt.Parameters.AddWithValue("@ADISOYADI", TxtAdıSoyadı.Text);
                    kmt.Parameters.AddWithValue("@BAKIYE", 0);
                    if (cBDurum.Checked == true)
                    {
                        kmt.Parameters.AddWithValue("@DURUM", true);
                    }
                    else
                    {
                        kmt.Parameters.AddWithValue("@DURUM", false);
                    }
                    baglanti.Open();
                    kmt.ExecuteNonQuery();
                    baglanti.Close();

                    goster();
                }
            }
            else
            {
                if (MessageBox.Show("Cari kaydı güncellemek istiyor musunuz?", "Güncelle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlCommand kmt = new SqlCommand("update TBLCARI set ADISOYADI=@ADISOYADI, DURUM=@DURUM where ID=@ID", baglanti);
                    kmt.Parameters.AddWithValue("@ADISOYADI", TxtAdıSoyadı.Text);
                    kmt.Parameters.AddWithValue("@ID", c_id);
                    if (cBDurum.Checked == true)
                    {
                        kmt.Parameters.AddWithValue("@DURUM", true);
                    }
                    else
                    {
                        kmt.Parameters.AddWithValue("@DURUM", false);
                    }

                    baglanti.Open();
                    kmt.ExecuteNonQuery();
                    baglanti.Close();

                    goster();
                }
            }
        }

        private void TxtAdıSoyadı_Leave(object sender, EventArgs e)
        {
            TxtAdıSoyadı.Text = TxtAdıSoyadı.Text.ToUpper();
        }

        private void XtraCari_Load(object sender, EventArgs e)
        {
            goster();
        }
        bool kaydet = true;
        int c_id = 0;
        void goster()
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * From TBLCARI order by ADISOYADI", baglanti);
            DataTable da = new DataTable();
            da.Clear();
            adp.Fill(da);

            kaydet = true;
            BtnTemizle.Enabled = false;
            TxtAdıSoyadı.Text = "";
            cBDurum.Checked = true;
            gC1.DataSource = da;
        }

        private void BtnGüncelle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            c_id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID").ToString());
            TxtAdıSoyadı.Text = gridView1.GetFocusedRowCellValue("ADISOYADI").ToString();
            bool durum = Convert.ToBoolean(gridView1.GetFocusedRowCellValue("DURUM").ToString());
            if (durum == true)
            {
                cBDurum.Checked = true;
            }
            else
            {
                cBDurum.Checked = false;
            }
            kaydet = false;
            TxtAdıSoyadı.Focus();
            BtnTemizle.Enabled = true;
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                var rowH = gridView1.FocusedRowHandle;
                var focusRowView = (DataRowView)gridView1.GetFocusedRow();
                if (focusRowView == null || focusRowView.IsNew) return;

                if (rowH >= 0)
                {
                    popupMenu1.ShowPopup(barManager1, new Point(MousePosition.X, MousePosition.Y));
                }
                else
                {
                    popupMenu1.HidePopup();
                }
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            goster();
        }

        private void XtraCari_Leave(object sender, EventArgs e)
        {
            goster();
        }

        private void XtraCari_FormClosing(object sender, FormClosingEventArgs e)
        {
            goster();
        }
    }
}
