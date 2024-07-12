using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SürüTakip
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
            LoadCattles();
            label1.Visible = false;
        }

        private void LoadCattles()
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.CATTLE");
            SqlDataAdapter da = new SqlDataAdapter("SELECT CattleID, EarTagNumber,Name,Gender,Breed,Birthdate  FROM dbo.CATTLE", conn);
            dgv1.Columns.Clear();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv1.DataSource = dt;





            dgv1.Columns[0].HeaderText = "ID";
            dgv1.Columns[1].HeaderText = "Kulak No";
            dgv1.Columns[2].HeaderText = "Adı";
            dgv1.Columns[3].HeaderText = "Cinsi";
            dgv1.Columns[4].HeaderText = "Irkı";
            dgv1.Columns[5].HeaderText = "Doğum Tarihi";
            conn.Close();
        }

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;


            if (rowIndex >= 0)
            {

                var id = dgv1.Rows[rowIndex].Cells[1].Value.ToString();


                label1.Text = id;


            }
            LoadMilk();
            LoadBreed();



        }

        private void LoadBreed()
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT BreedDate AS Tohumlanma,CheckDate AS Kontrol, KuruDate AS Kuru  " +
               " FROM dbo.Breed" +
               " WHERE EarTagNumber = @kulakno " +
               "GROUP BY BreedDate,CheckDate,KuruDate ", conn);
            da.SelectCommand.Parameters.AddWithValue("@kulakno", label1.Text);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv3.DataSource = dt;
            conn.Close();

        }

        private void LoadMilk()
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT ProductionDate AS Tarih,MilkQuantity AS Litre  " +
               " FROM dbo.MilkProduction" +
               " WHERE EarTagNumber = @kulakno " +
               "GROUP BY ProductionDate,MilkQuantity ", conn);
            da.SelectCommand.Parameters.AddWithValue("@kulakno", label1.Text);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv2.DataSource = dt;
            conn.Close();


        }

        private void dgv3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

