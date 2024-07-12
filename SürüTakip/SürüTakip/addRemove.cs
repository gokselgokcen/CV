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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SürüTakip
{
    public partial class addRemove : Form
    {
        public addRemove()
        {
            InitializeComponent();
        }

        private void addRemove_Load(object sender, EventArgs e)
        {
            LoadData();
            //AddDeleteButtonColumn();



        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
               

                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.CATTLE (EarTagNumber,Name,Gender,Breed,Birthdate)" +
                    "VALUES (@no,@name,@gender,@irk,@dt)", conn);

                cmd.Parameters.AddWithValue("@no", "TR" + txtNo.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@gender", cmbGender.SelectedItem);
                cmd.Parameters.AddWithValue("@irk", cmbBreed.SelectedItem);
                cmd.Parameters.AddWithValue("@dt", dateTimePicker1.Value);

                if (txtNo.Text != "" && cmbGender.SelectedIndex != -1)
                {
                    cmd.ExecuteNonQuery();
                    LoadData();
                    ClearForm();
                    label6.ForeColor = Color.Green;
                    label6.Text = "Kayıt Başarılı!";

                }
                else
                {
                    label6.Text = "Kulak Numarası ve Cinsiyeti Giriniz!!";
                    label6.ForeColor = Color.Red;
                }
                             

            }
            catch (SqlException )
            {
                MessageBox.Show("Girdiğiniz Bilgiler Hatalı veya Daha önce girilmiş");

            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }

            }
           
            
                                 
        
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtNo.Text = "";
            cmbGender.SelectedText = null;
            cmbBreed.SelectedText = null;

        }

        private void LoadData()
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

            DataColumn ageColumn = new DataColumn("Yaş", typeof(int));
            dt.Columns.Add(ageColumn);

            // Şimdi her bir satır için yaş hesaplayalım
            foreach (DataRow row in dt.Rows)
            {
                DateTime birthDate = Convert.ToDateTime(row["Birthdate"]);
                int age = CalculateAge(birthDate);
                row["Yaş"] = age;
            }


            dgv1.Columns[0].HeaderText = "ID";
            dgv1.Columns[1].HeaderText = "Kulak No";
            dgv1.Columns[2].HeaderText = "Adı";
            dgv1.Columns[3].HeaderText = "Cinsi";
            dgv1.Columns[4].HeaderText = "Irkı";
            dgv1.Columns[5].HeaderText = "Doğum Tarihi";
            dgv1.Columns["Yaş"].HeaderText = "Yaş";
            AddDeleteButtonColumn();


            //dgv1.Columns[6].HeaderText = "Oluşturulma tarihi";

            conn.Close();

        }
        private int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age))
                age--;
            return age;
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex ==6)
            {
                DialogResult result = MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if(result == DialogResult.Yes)
                {
                    string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.CATTLE WHERE CattleID=@no", conn);
                    cmd.Parameters.AddWithValue("@no", dgv1.Rows[e.RowIndex].Cells[0].Value);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LoadData();
                    label6.ForeColor = Color.Green;
                    label6.Text = "Silme işlemi başarılı!";
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edildi.");
                }
                

            }

        }
        private void AddDeleteButtonColumn()
        {
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.HeaderText = "SİLME";
            deleteButtonColumn.Text = "Sil";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dgv1.Columns.Add(deleteButtonColumn);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

            MainMenuForm frm = new MainMenuForm();

            frm.Show();
            this.Close();
        }

        private void txtNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Basılan tuşun bir rakam veya kontrol tuşu olup olmadığını kontrol edin
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Eğer rakam veya kontrol tuşu değilse, olayı işlenmiş olarak ayarla
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.CATTLE");
            SqlDataAdapter da = new SqlDataAdapter("SELECT CattleID, EarTagNumber,Name,Gender,Breed,Birthdate  FROM dbo.CATTLE WHERE EarTagNumber LIKE @no ", conn);
            da.SelectCommand.Parameters.AddWithValue("@no", "%" + txtSearch.Text + "%");
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
            AddDeleteButtonColumn();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HerdUpdateScreen updateS = new HerdUpdateScreen();

            updateS.Show();
            

        }
    }
}
