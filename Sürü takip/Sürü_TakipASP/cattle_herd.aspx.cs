using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Reflection.Emit;

namespace Sürü_Takip
{
    public partial class cattle_herd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Page.IsPostBack == false)
            {
                BindData();

            }
        }

        private void BindData()
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Kulak_NO,Sigir_Adı,Gender,Breed FROM dbo.COWS ", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-NA96GMK\GG_MSSQLSERVER1;Initial Catalog=NG_surutakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.COWS (Kulak_NO , Sigir_adı, Gender , Breed) VALUES (@numara,@isim,@cinsiyet,@irk)",conn);
            cmd.Parameters.AddWithValue("@numara", TextBox1.Text);
            cmd.Parameters.AddWithValue("isim", TextBox2.Text);
            cmd.Parameters.AddWithValue("@cinsiyet", DropDownList1.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@irk", DropDownList2.SelectedItem.Text);
            if(DropDownList1.SelectedIndex!=0 && DropDownList2.SelectedIndex != 0)
            {
                cmd.ExecuteNonQuery();
                Label5.Text = "Ekleme Başarılı!";
                conn.Close();
                BindData();

                TextBox1.Text = "";
                TextBox2.Text = "";
                DropDownList1.Text = null;
                DropDownList2.Text = null;


            }
            else
            {
                Label5.Text = "Cinsiyet veya Irk Seçimini yapınız!!";

            }

            
            





        }

        protected void btnHome_Click(object sender, EventArgs e)
        {

        }
    }
}