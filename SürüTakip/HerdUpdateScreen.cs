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
    public partial class HerdUpdateScreen : Form
    {
        public HerdUpdateScreen()
        {
            InitializeComponent();
        }

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;


            if (rowIndex >= 0)
            {

                var id = dgv1.Rows[rowIndex].Cells[0].Value.ToString();
                var numberValue = dgv1.Rows[rowIndex].Cells[1].Value.ToString().Substring(2);
                var nameValue = dgv1.Rows[rowIndex].Cells[2].Value.ToString();
                var gender = dgv1.Rows[rowIndex].Cells[3].Value.ToString();
                var breed = dgv1.Rows[rowIndex].Cells[4].Value.ToString();
                var date = dgv1.Rows[rowIndex].Cells[5].Value.ToString();

                label15.Text = id;
                txtNo.Text = numberValue;
                txtName.Text = nameValue;
                cmbGender.Text = gender;
                cmbBreed.Text = breed;
                dateTimePicker1.Text = date;



            }

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

          
           


            dgv1.Columns[0].HeaderText = "ID";
            dgv1.Columns[1].HeaderText = "Kulak No";
            dgv1.Columns[2].HeaderText = "Adı";
            dgv1.Columns[3].HeaderText = "Cinsi";
            dgv1.Columns[4].HeaderText = "Irkı";
            dgv1.Columns[5].HeaderText = "Doğum Tarihi";
           
            


            

            conn.Close();

        }

        private void HerdUpdateScreen_Load(object sender, EventArgs e)
        {
            LoadData();
            label15.Visible = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {
           

        }
        private void ClearForm()
        {
            txtName.Text = "";
            txtNo.Text = "";
            cmbGender.SelectedText = null;
            cmbBreed.SelectedText = "";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {


                SqlCommand cmd = new SqlCommand("UPDATE dbo.CATTLE " +
                    "SET EarTagNumber = @no, Name = @name, Gender = @gender, Breed = @irk, Birthdate = @dt  WHERE CattleID = @id"
                   , conn);
                cmd.Parameters.AddWithValue("@id", label15.Text);
                cmd.Parameters.AddWithValue("@no", "TR" + txtNo.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@gender", cmbGender.SelectedItem);
                cmd.Parameters.AddWithValue("@irk", cmbBreed.SelectedItem);
                cmd.Parameters.AddWithValue("@dt", dateTimePicker1.Value);

                SqlCommand cmdMilkProduction = new SqlCommand("UPDATE dbo.MilkProduction " +
            "SET EarTagNumber = @no " +
            "WHERE CattleID = @id", conn);
                cmdMilkProduction.Parameters.AddWithValue("@id", label15.Text);
                cmdMilkProduction.Parameters.AddWithValue("@no", "TR" + txtNo.Text);

                cmdMilkProduction.ExecuteNonQuery();


                SqlCommand cmdBreed = new SqlCommand("UPDATE dbo.Breed " +
            "SET EarTagNumber = @no " +
            "WHERE CattleID = @id", conn);
                cmdBreed.Parameters.AddWithValue("@id", label15.Text);
                cmdBreed.Parameters.AddWithValue("@no", "TR" + txtNo.Text);

                cmdBreed.ExecuteNonQuery();



                if (txtNo.Text != "" )
                {
                    cmd.ExecuteNonQuery();
                    LoadData();
                    ClearForm();
                    label6.ForeColor = Color.Green;
                    label6.Text = "Güncelleme Başarılı!";

                }
                else
                {
                    label6.Text = "Kulak Numarası veya Cinsiyeti Giriniz!!";
                    label6.ForeColor = Color.Red;
                }


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Girdiğiniz Bilgiler Hatalı veya Daha önce girilmiş\nHata Detayı: " + ex.Message);
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
                LoadData();

            }



        }
    }
}
