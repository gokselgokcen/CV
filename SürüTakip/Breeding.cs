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

            DateTime KuruDate = selectedDate.AddDays(210);
            lblKuru.Text = KuruDate.ToString("dd.MM.yyyy");

           
            DateTime BirthDate = selectedDate.AddDays(280);
            lblBirthDate.Text = BirthDate.ToString("dd.MM.yyyy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Breed (CattleID,EarTagNumber,Name,BreedDate,CheckDate,KuruDate,BirthDate) " +
               "VALUES (@id,@kulak,@name,@breed,@check,@kuru,@birth) ", conn);

            
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
                cmd.Parameters.AddWithValue("@birth", DateTime.Parse(lblBirthDate.Text));
                cmd.ExecuteNonQuery();
                LoadBreedData();

            }


            conn.Close();
            //LoadBreedData();
            



        }

        private void LoadBreedData()
        {
            dgv2.Columns.Clear();
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT ID,EarTagNumber,Name,BreedDate,CheckDate,KuruDate,BirthDate  FROM dbo.Breed", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv2.DataSource = dt;

            dgv2.Columns[0].HeaderText = "ID";
            dgv2.Columns[1].HeaderText = "KulakNo";
            dgv2.Columns[2].HeaderText = "İsim";
            dgv2.Columns[3].HeaderText = "Tohumlama";
            dgv2.Columns[4].HeaderText = "Kontrol";
            dgv2.Columns[5].HeaderText = "Kuru";
            dgv2.Columns[6].HeaderText = "Doğum";
            AddDeleteButtonColumn();



            conn.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainMenuForm frm = new MainMenuForm();

            frm.Show();
            this.Close();
        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void dgv2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.ColumnIndex != 3)
            {
                var cellValue = dgv2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (cellValue is DateTime date && date != null)
                {
                    
                    DateTime today = DateTime.Today;

                    TimeSpan difference = today - date;

                    int daysDifference = (int)difference.TotalDays;

                    if (daysDifference < 7 && daysDifference > 3)
                    {
                        dgv2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Yellow;
                    }
                    else if (daysDifference <=3  && daysDifference >= -2)
                    {
                        dgv2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dgv2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = dgv2.DefaultCellStyle.BackColor;
                    }
                }
            }

        }
        private void AddDeleteButtonColumn()
        {

            bool deleteButtonColumnExists = false;

            // Zaten mevcut olup olmadığını kontrol edin
            foreach (DataGridViewColumn column in dgv2.Columns)
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
                dgv2.Columns.Add(deleteButtonColumn);
            }

        }

        private void dgv2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sadece "SİLME" başlıklı sütunda bir düğmeye tıklanırsa işlem yap
            if (dgv2.Columns[e.ColumnIndex].HeaderText == "SİLME" && e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Breed WHERE ID=@id ", conn);
                        cmd.Parameters.AddWithValue("@id", dgv2.Rows[e.RowIndex].Cells[0].Value);



                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Silme işlemi Başarılı");
                        dgv2.Rows.RemoveAt(e.RowIndex);
                        LoadBreedData();

                    }
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edildi.");
                }
                
            }
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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dgv2_Sorted(object sender, EventArgs e)
        {

        }
    }
}


