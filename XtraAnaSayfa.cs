using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CariTakip
{
    public partial class XtraAnaSayfa : DevExpress.XtraEditors.XtraForm
    {
        // SQL Server bağlantı dizgesi
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-V7J7SRGI;Initial Catalog=YOUR_DATABASE_NAME;User ID=YOUR_USERNAME;Password=YOUR_PASSWORD");

        public XtraAnaSayfa()
        {
            InitializeComponent();
        }

        private void XtraAnaSayfa_Load(object sender, EventArgs e)
        {
            // SQL Server veri yükleme komutları
           
            {
                // SQL Server veri yükleme komutları.
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLCARI", baglanti);
                SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM TBLCARIHAREKET", baglanti);
                da.Fill(this.dbDataSet.TBLCARI);
                da2.Fill(this.dbDataSet.TBLCARIHAREKET);
            }
        }


        private void BtnCariEkle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraCari ca = new XtraCari();
            ca.Show();
        }

        private void BtnStokEkle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraStok st = new XtraStok();
            st.Show();
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
