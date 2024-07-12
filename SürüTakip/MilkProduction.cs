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
    public partial class MilkProduction : Form
    {
        public MilkProduction()
        {
            InitializeComponent();
        }

        private void MilkProduction_Load(object sender, EventArgs e)
        {
            LoadData();
            //LoadDataMilk();
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "MM/yyyy";
            dateTimePicker2.ShowUpDown = true; // Yukarı aşağı okları göster
            LoadMonthData();


        }

        private void LoadMonthData()
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // Seçilen ayın başlangıç ve bitiş tarihlerini al
            DateTime selectedDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);
           
            


            SqlDataAdapter da = new SqlDataAdapter(@"SELECT EarTagNumber AS KulakNo, ProductionDate AS Tarih ,MilkQuantity AS Litre  
                                             FROM dbo.MilkProduction 
                                             WHERE  ProductionDate = @selectedDate
                                             GROUP BY EarTagNumber, ProductionDate, MilkQuantity", conn);
            
            da.SelectCommand.Parameters.AddWithValue("@selectedDate", selectedDate);
            

            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv3.DataSource = dt;
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

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;


            if (rowIndex >= 0)
            {
                var IDValue = dgv1.Rows[rowIndex].Cells[0].Value.ToString();
                var numberValue = dgv1.Rows[rowIndex].Cells[1].Value.ToString();
                var nameValue = dgv1.Rows[rowIndex].Cells[2].Value.ToString();
                
                lblNumber.Text = numberValue;
                lblName.Text = nameValue;
                lblID.Text = IDValue;


            }
           

           
            LoadMilkData();







        }

        private void LoadMilkData()
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT ProductionDate AS Tarih,MilkQuantity AS Litre  " +
               " FROM dbo.MilkProduction" +
               " WHERE EarTagNumber = @kulakno " +
               "GROUP BY ProductionDate,MilkQuantity ", conn);
            da.SelectCommand.Parameters.AddWithValue("@kulakno", lblNumber.Text);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv2.DataSource = dt;
            conn.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.MilkProduction (CattleID,EarTagNumber,Name,ProductionDate,MilkQuantity) " +
                "VALUES (@id,@kulak,@name, @date, @litre) ", conn);

            cmd.Parameters.AddWithValue("@id", lblID.Text);
            cmd.Parameters.AddWithValue("@kulak", lblNumber.Text);
            cmd.Parameters.AddWithValue("@name",lblName.Text);
            cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@litre", txtLitre.Text);

            cmd.ExecuteNonQuery();
            
            
            conn.Close();
            //LoadDataMilk();
            LoadMilkData();

        }

        private void LoadDataMilk()
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT EarTagNumber,Name,ProductionDate,MilkQuantity  FROM dbo.MilkProduction", conn);
            dgv1.Columns.Clear();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv2.DataSource = dt;

            dgv2.Columns[0].HeaderText = "Kulak No";
            dgv2.Columns[1].HeaderText = "İsim";
            dgv2.Columns[2].HeaderText = "Tarih";
            dgv2.Columns[3].HeaderText = "Miktar(L)";
            conn.Close();
            LoadData();

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainMenuForm frm = new MainMenuForm();

            frm.Show();
            this.Close();

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_FontChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_FormatChanged(object sender, EventArgs e)
        {
            
        }
    }
}
