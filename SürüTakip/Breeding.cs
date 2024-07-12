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

namespace SürüTakip
{
    public partial class Breeding : Form
    {
        public Breeding()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT  CattleID,EarTagNumber,Name  FROM dbo.CATTLE WHERE Gender = 'DİŞİ' ", conn);
            dgv1.Columns.Clear();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv1.DataSource = dt;

            dgv1.Columns[0].HeaderText = "ID";
            dgv1.Columns[1].HeaderText = "Kulak No";
            dgv1.Columns[2].HeaderText = "Adı";
            conn.Close();
        }

        private void Breeding_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadBreedData();

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


        private void dgv1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;


            if (rowIndex >= 0)
            {
                var IDValue = dgv1.Rows[rowIndex].Cells[0].Value.ToString();
                var numberValue = dgv1.Rows[rowIndex].Cells[1].Value.ToString();
                var nameValue = dgv1.Rows[rowIndex].Cells[2].Value.ToString();

                lblNo.Text = numberValue;
                lblName.Text = nameValue;
                lblID.Text = IDValue;


            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            DateTime checkDate = selectedDate.AddDays(60);
            lblCheck.Text = checkDate.ToString("dd.MM.yyyy");

            DateTime Sd = dateTimePicker1.Value;
            DateTime KuruDate = Sd.AddDays(210);
            lblKuru.Text = KuruDate.ToString("dd.MM.yyyy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Breed (CattleID,EarTagNumber,Name,BreedDate,CheckDate,KuruDate) " +
               "VALUES (@id,@kulak,@name,@breed,@check,@kuru) ", conn);

            
            if(string.IsNullOrWhiteSpace(lblCheck.Text))
            {
                MessageBox.Show("Tarih Seçimi Yapınız");

            }
            else
            {
                cmd.Parameters.AddWithValue("@id", lblID.Text);
                cmd.Parameters.AddWithValue("@kulak", lblNo.Text);
                cmd.Parameters.AddWithValue("@name", lblName.Text);
                cmd.Parameters.AddWithValue("@breed", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@check", DateTime.Parse(lblCheck.Text));
                cmd.Parameters.AddWithValue("@kuru", DateTime.Parse(lblKuru.Text));
                cmd.ExecuteNonQuery();
                LoadBreedData();

            }

//cmd.ExecuteNonQuery();

            conn.Close();
            //LoadBreedData();
            



        }

        private void LoadBreedData()
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT EarTagNumber,Name,BreedDate,CheckDate,KuruDate  FROM dbo.Breed", conn);
            //dgv1.Columns.Clear();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv2.DataSource = dt;

            dgv2.Columns[0].HeaderText = "Kulak No";
            dgv2.Columns[1].HeaderText = "İsim";
            dgv2.Columns[2].HeaderText = "Tohumlama";
            dgv2.Columns[3].HeaderText = "Kontrol";
            dgv2.Columns[4].HeaderText = "Kuru";

            conn.Close();
            //LoadData();
        }
    }
}


