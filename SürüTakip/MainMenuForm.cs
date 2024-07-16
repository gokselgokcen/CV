using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SürüTakip
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addRemove addRemove = new addRemove();

            addRemove.Show();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MilkProduction mp = new MilkProduction();

            mp.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Breeding bd = new Breeding();
            bd.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Report rep = new Report();
            rep.Show();
            this.Close();

        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {

        }
    }
}
