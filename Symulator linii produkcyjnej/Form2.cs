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
        
        private Timer zegarawarii = new Timer();
        private Timer zegarobecnosci = new Timer();
        private Random rng = new Random();
        public bool nieobecnosc = false;
        
        public Main(string uzytkownik)
        {
            InitializeComponent();
            label1.Text = $"Zalogowany operator: {uzytkownik}";
            zegarawarii.Tick += timer1_Tick;
            zegarobecnosci.Tick += timer2_Tick;
            LosoweAwarie();
            Obecnosc();

        }

        public void LosoweAwarie()
        {
            int awaria = rng.Next(10000, 30001);  // awaria wystapi w czasie od 10 do 30 sekund
            zegarawarii.Interval= awaria;
            zegarawarii.Start();
        }

        public void Obecnosc()
        { 
            zegarobecnosci.Interval = 10000;
            zegarobecnosci.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            zegarawarii.Stop();
            MessageBox.Show("Nastąpiła nieprzewidziana awaria, proszę zresetuj linię produkcyjną");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible= false;
            Obecnosc();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            button1.Visible = true;
            if (!nieobecnosc)
            {
                zegarobecnosci.Stop();
                zegarobecnosci.Interval = 10000;
                zegarobecnosci.Start();
                nieobecnosc = true;
            }
            else
            {
                MessageBox.Show("Uwaga wykryto nieobecność operatura, następuje automatyczne zatrzymanie linii produkcyjnej i wylogowanie użytkownika.");
                Application.Restart();
            }

        }
    }
}
