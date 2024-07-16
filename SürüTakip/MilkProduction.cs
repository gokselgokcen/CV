using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
            //dateTimePicker2.Format = DateTimePickerFormat.Custom;
            //dateTimePicker2.CustomFormat = "MM/yyyy";
            //dateTimePicker2.ShowUpDown = true; // Yukarı aşağı okları göster
            // LoadMonthData();

            label2.Visible = false;
            lblID.Visible = false;
            
        }

        private void LoadMonthData()
        {
            //dgv3.DataSource = null;
            //dgv3.Rows.Clear();
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();


            DateTime startDate = dateTimePicker2.Value.AddDays(-1);
            DateTime endDate = dateTimePicker3.Value.AddDays(1);





            SqlDataAdapter da = new SqlDataAdapter(@"SELECT ProductionID, EarTagNumber AS KulakNo, ProductionDate AS Tarih, MilkQuantity AS Litre  
                     FROM dbo.MilkProduction 
                     WHERE ProductionDate >= @startDate AND ProductionDate < @endDate", conn);

            da.SelectCommand.Parameters.AddWithValue("@startDate", startDate);
            da.SelectCommand.Parameters.AddWithValue("@endDate", endDate);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv3.DataSource = dt;
            dgv3.Columns["ProductionID"].Visible = false;
           

            

            
            
               
            



            conn.Close();
        }
        private void AddDeleteButtonColumn()
        {

            bool deleteButtonColumnExists = false;

            // Zaten mevcut olup olmadığını kontrol edin
            foreach (DataGridViewColumn column in dgv3.Columns)
            {
                if (column is DataGridViewButtonColumn && column.Name == "deleteButtonColumn")
                {
                    deleteButtonColumnExists = true;
                    break;
                }
            }

            // Eğer silme butonu sütunu yoksa ekle
            if (!deleteButtonColumnExists)
            {
                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
                deleteButtonColumn.Name = "deleteButtonColumn"; // Sütun ismi
                deleteButtonColumn.HeaderText = "SİLME";
                deleteButtonColumn.Text = "Sil";
                deleteButtonColumn.UseColumnTextForButtonValue = true;
                dgv3.Columns.Add(deleteButtonColumn);
            }

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
            cmd.Parameters.AddWithValue("@name", lblName.Text);
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
        private void button1_Click(object sender, EventArgs e)
        {
            
            LoadMonthData();
            CalculateMilk();
            AddDeleteButtonColumn();
        }

        private void CalculateMilk()
        {
            DataTable dt = (DataTable)dgv3.DataSource;
            string columnName = "Litre";

            double sum = 0;
            int count = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row[columnName] != DBNull.Value)
                {
                    sum += Convert.ToDouble(row[columnName]);
                    count++;
                }
            }
            double average = sum / count;
            lblSum.Visible = true;

            lblSum.Text = average.ToString();


        }

        private void dgv3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sadece "SİLME" başlıklı sütunda bir düğmeye tıklanırsa işlem yap
            if (dgv3.Columns[e.ColumnIndex].HeaderText == "SİLME" && e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM dbo.MilkProduction WHERE ProductionID=@id", conn);
                        cmd.Parameters.AddWithValue("@id", dgv3.Rows[e.RowIndex].Cells["ProductionID"].Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Silme işlemi Başarılı");
                        LoadData(); // DataGridView'i yeniden yükle
                        LoadMonthData(); // Gerekiyorsa diğer verileri yeniden yükle
                    }
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edildi.");
                }
            }
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





            private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
            {

            }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

           
            SqlDataAdapter da = new SqlDataAdapter("SELECT CattleID, EarTagNumber,Name  FROM dbo.CATTLE WHERE Gender='DİŞİ'AND EarTagNumber LIKE @no ", conn);
            da.SelectCommand.Parameters.AddWithValue("@no", "%" + txtSearch.Text + "%");
            dgv1.Columns.Clear();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv1.DataSource = dt;
            dgv1.Columns[0].HeaderText = "ID";
            dgv1.Columns[1].HeaderText = "Kulak No";
            dgv1.Columns[2].HeaderText = "Adı";
            
        }
    }
    }

