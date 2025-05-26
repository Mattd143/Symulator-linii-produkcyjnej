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
        private Timer zegarsymulacji = new Timer();
        private Random rng = new Random();
        private bool nieobecnosc = false;
        private int temperatura = 60;
        private int obroty = 1200;
        private int cpu = 50;
        private int cisnienie = 100;

        public Main(string uzytkownik)
        {
            InitializeComponent();
            label1.Text = $"Zalogowany operator: {uzytkownik}";
            zegarawarii.Tick += timer1_Tick;
            zegarobecnosci.Tick += timer2_Tick;
            zegarsymulacji.Tick += timer3_Tick;
            LosoweAwarie();
            Obecnosc();
            Symulacja();

        }

        public void LosoweAwarie()
        {
            int awaria = rng.Next(30000, 60001);  // awaria wystapi w czasie od 30 do 60 sekund
            zegarawarii.Interval= awaria;
            zegarawarii.Start();
        }

        public void Obecnosc()
        { 
            zegarobecnosci.Interval = 25000;  // co 25 sekund trzeba potwierdzic obecnosc
            zegarobecnosci.Start();
        }

        public void Symulacja()
        {
            zegarsymulacji.Interval = 2000;  // zmienia zmienne co 2 sekundy
            zegarsymulacji.Start();
            
            // Symulacja zmiennych
            temperatura += rng.Next(-1, 5);
            obroty += rng.Next(-10, 10);
            if (cpu > 35)
            {
                cpu += rng.Next(-2, 5);
            }
            else
            {
                cpu += rng.Next(0, 5);
            }
            cisnienie += rng.Next(0, 2);

            // Wypisywanie nowych zmiennych
            label6.Text = $"{temperatura} °C";
            label7.Text = $"{obroty} RPM";
            label8.Text = $"{cpu} %";
            label9.Text = $"{cisnienie} bar";

            // krytyczne wartosci - awarie
            if (temperatura > 80)
            {
                label10.Visible = true;
                label10.Text = $"UWAGA: Temperatura zbyt wysoka! Zwiększ obroty wentylatorów";
            }
            else if (temperatura < 40)
            {
                label10.Visible = true;
                label10.Text = $"UWAGA: Temperatura zbyt niska! Zmniejsz obroty wentylatorów";
            }
            else if (cisnienie > 120)
            {
                label10.Visible= true;  
                label10.Text = $"UWAGA: Ciśnienie zbyt wysokie! Otwórz zawór bezpieczeństwa";
            }
            else if (cpu > 70)
            {
                label10.Visible = true;
                button5.Visible = true;
                label10.Text = $"UWAGA: Wykorzystanie cpu zbyt wysokie! Włącz tryb oszczędny";
            }
            else
            {
                label10.Visible = false;
            }


            // uzaleznienie temperatury od obrotow wentylatora
            if (obroty > 1300)
            {
                temperatura -= 5;
            }
            else if (obroty < 1100)
            {
                temperatura += 5;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible= false;
            nieobecnosc=false;
            Obecnosc();
        }


        private void timer1_Tick(object sender, EventArgs e)  // timer od losowych awarii
        {
            zegarawarii.Stop();
            MessageBox.Show("Nastąpiła nieprzewidziana awaria, proszę zresetuj linię produkcyjną");
        }

        private void timer2_Tick(object sender, EventArgs e)  // timer od sprawdzania obecnosci
        {
            button1.Visible = true;
            if (!nieobecnosc)
            {
                zegarobecnosci.Stop();
                zegarobecnosci.Interval = 20000;
                zegarobecnosci.Start();
                nieobecnosc = true;
            }
            else
            {
                zegarobecnosci.Stop();
                zegarawarii.Stop();
                MessageBox.Show("Uwaga wykryto nieobecność operatura, następuje automatyczne zatrzymanie linii produkcyjnej i wylogowanie użytkownika.");
                Application.Restart();
            }

        }

        private void timer3_Tick(object sender, EventArgs e)   // timer od symulacji zmiennych
        {
            zegarsymulacji.Stop();
            Symulacja();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            obroty -= 50;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            obroty+= 50;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cpu -= 25;
            button5.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cisnienie -= 40;
        }
    }
}
