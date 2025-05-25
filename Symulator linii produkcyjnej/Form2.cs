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
    public partial class Main : Form
    {
        
        private Timer zegar = new Timer();
        private Random rng = new Random();
        
        
        public Main(string uzytkownik)
        {
            InitializeComponent();
            label1.Text = $"Zalogowany operator: {uzytkownik}";
            zegar.Tick += timer1_Tick;
            LosoweAwarie();

        }

        public void LosoweAwarie()
        {
            int awaria = rng.Next(10000, 30001);  // awaria wystapi w czasie od 10 do 30 sekund
            zegar.Interval= awaria;
            zegar.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            zegar.Stop();
            MessageBox.Show("Nastąpiła nieprzewidziana awaria, proszę zresetuj linię produkcyjną");
        }
    }
}
