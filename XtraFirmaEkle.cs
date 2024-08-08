using System;
using System.Data.SqlClient; // OLEDB yerine SqlClient kullanın
using System.Windows.Forms;

namespace CariTakip
{
    public partial class XtraFirmaEkle : DevExpress.XtraEditors.XtraForm
    {
        // SQL Server bağlantı dizgesi
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-VTJ7SRGI;Initial Catalog=YOUR_DATABASE_NAME;User ID=YOUR_USERNAME;Password=YOUR_PASSWORD");

        public XtraFirmaEkle()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirmaAdi.Text))
            {
                MessageBox.Show("Firma adı boş olamaz.");
                return;
            }

            try
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO TBLFIRMA (ADI) VALUES (@ADI)", baglanti); // OLEDB yerine SQL komutu kullanın
                cmd.Parameters.AddWithValue("@ADI", txtFirmaAdi.Text); // Parametreyi ekleyin
                cmd.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Firma başarıyla eklendi.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == System.Data.ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }

        private void XtraFirmaEkle_Load(object sender, EventArgs e)
        {
            // Load işlemleri (eğer varsa)
        }
    }
}
