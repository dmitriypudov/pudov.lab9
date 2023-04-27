using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pudovlab9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double[] ver, freq; int[] stat;
        int N; double temp, X, chi = 9.488;

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            double deltaM, deltaD, Memp = 0, M = 0, Demp = 0, D = 0;
            ver = new double[5]; 
            stat = new int[5]; 
            freq = new double[5];
            N = (int)numericUpDown5.Value;
            ver[0] = (double)numericUpDown1.Value;
            ver[1] = (double)numericUpDown2.Value;
            ver[2] = (double)numericUpDown3.Value;
            ver[3] = (double)numericUpDown4.Value;
            ver[4] = 1 - ver[0] - ver[1] - ver[2] - ver[3];
            p5.Text = ver[4].ToString();
            for (int i = 0; i < N; i++)
            {
                temp = rnd.NextDouble();
                double sum = 0;
                for (int k = 0; k < 5; k++)  
                {
                    sum += ver[k];
                    if (temp <= sum)
                    {
                        stat[k]++;
                        break;
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                freq[i] = stat[i] / (double)N;
            }
            for (int i = 0; i < 5; i++)
            {
                M += ver[i] * stat[i];
                Memp += freq[i] * stat[i];
                D += ver[i] * stat[i] * stat[i];
                Demp += freq[i] * stat[i] * stat[i];
            }
            D -= M * M; 
            Demp -= Memp * Memp;

            deltaM = Math.Abs(M - Memp) / Math.Abs(M);
            deltaD = Math.Abs(D - Demp) / Math.Abs(D);

            textBox3.Text = deltaM.ToString("F3");
            textBox5.Text = deltaD.ToString("F3");
            textBox2.Text = Memp.ToString("F3");
            textBox4.Text = Demp.ToString("F3");

            chart1.Series[0].Points.Clear();
            for (int i = 0; i < 5; i++)
            {
                chart1.Series[0].Points.AddXY(i + 1, freq[i]);
            }

            X = 0;
            for (int i = 0; i < 5; i++)
            {
                X += (stat[i] * stat[i]) / (N * ver[i]);
            }
            X -= N;

            if (X < chi)
            {
                textBox1.Text = "Истина";
            } else
            {
                textBox1.Text = "Ложь";
            }
        }
    }
}

