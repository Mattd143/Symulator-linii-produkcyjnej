using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Symulator_linii_produkcyjnej
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.glowne = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "projekt")
            {
                this.glowne = new Main(textBox1.Text);
                glowne.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nieprawidłowe hasło.");
            }
        }
    }
}
