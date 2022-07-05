using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace odev_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int gap;
        int match;
        int misMatch;
        int a = 0;
        int b = 0;
        int c = 0;
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 45)
            {
                e.Handled = true;
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 45)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 45)
            {
                e.Handled = true;
            }
        }

        Stopwatch sw = new Stopwatch();
        private void button1_Click(object sender, EventArgs e)
        {
            sw.Start();

            bool deneme = false;
            gap = Convert.ToInt32(textBox3.Text);
            misMatch = Convert.ToInt32(textBox2.Text);
            match = Convert.ToInt32(textBox1.Text);
            string text1 = textBox4.Text;
            string text2 = textBox5.Text;
            string ttext1 = text1;
            string ttext2 = text2;
            int t1 = text1.Length;
            int t2 = text2.Length;
            if (text1.Length > text2.Length)
            {
                for (int i = 0; i < text1.Length - t2; i++)
                {
                    text2 = text2 + " ";

                }

            }
            else if (text1.Length < text2.Length)
            {
                for (int i = 0; i < text2.Length - t1; i++)
                {
                    text1 = text1 + " ";
                }
            }

            int[,] dizi = new int[text1.Length + 1, text2.Length + 1];

            for (int i = 0; i < text1.Length + 1; i++)
            {
                for (int j = 0; j < text2.Length + 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        dizi[i, j] = 0;
                    }
                    else if (i == 0 && j != 0)
                    {
                        c = dizi[i, j - 1] + gap;
                        dizi[i, j] = c;
                    }
                    else if (i != 0 && j == 0)
                    {
                        b = dizi[i - 1, j] + gap;
                        dizi[i, j] = b;
                    }
                    else
                    {
                        if (text1.Substring(j - 1, 1) == text2.Substring(i - 1, 1))
                        {
                            a = dizi[i - 1, j - 1] + match;
                        }
                        else if (text1.Substring(j - 1, 1) != text2.Substring(i - 1, 1))
                        {
                            if (text1.Substring(j - 1, 1) == " " || text2.Substring(i - 1, 1) == " ")
                            {

                                a = 0;
                                deneme = true;

                            }
                            else
                            {
                                a = dizi[i - 1, j - 1] + misMatch;
                            }

                        }
                        if (deneme == false)
                        {
                            b = dizi[i - 1, j] + gap;
                            c = dizi[i, j - 1] + gap;
                            dizi[i, j] = Math.Max(Math.Max(a, b), c);
                        }
                        else
                        {
                            dizi[i, j] = a;
                            deneme = false;
                        }

                    }
                }
            }

            string newnewtext2 = text2;
            text2 = " " + text2;
            dataGridView1.ColumnCount = t1 + 2;
            dataGridView1.Columns[0].Name = "";
            dataGridView1.Columns[1].Name = "";

         
            for (int i = 0; i < dataGridView1.ColumnCount - 2; i++)
            {
                dataGridView1.Columns[i + 2].Name = text1.Substring(i, 1);
            }
            if (t1 < t2)
            {
                for (int i = 0; i < t2 + 1; i++)
                {
                    int t = dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = text2.Substring(i, 1);
                    for (int j = 0; j < t1 + 1; j++)
                    {
                        dataGridView1.Rows[i].Cells[j + 1].Value = dizi[i, j];
                    }
                }

            }
            else if (t1 > t2)
            {
                for (int i = 0; i < t2 + 1; i++)
                {
                    int t = dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = text2.Substring(i, 1);
                    for (int j = 0; j < text2.Length; j++)
                    {
                        dataGridView1.Rows[i].Cells[j + 1].Value = dizi[i, j];
                    }
                }

            }
            else
            {

                for (int i = 0; i < text2.Length; i++)
                {
                    int t = dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = text2.Substring(i, 1);
                    for (int j = 0; j < text2.Length; j++)
                    {
                        dataGridView1.Rows[i].Cells[j + 1].Value = dizi[i, j];
                    }

                }

            }

            int left, top, diag, sonuc;
            int x = t1, y = t2;
            int dongu = 0;
            TimeSpan süre;
            süre = sw.Elapsed;
            label9.Text = label9.Text + süre.ToString();
            button1.Enabled = false;
            button3.Enabled = true;

            if (t1 < t2)
            {
                dongu = t2;
            }
            else if (t1 > t2)
            {
                dongu = t1;
            }
            else
            {
                dongu = t1;
            }


            string newText1 = "";
            string newText2 = "";
            string[] koordinat = new string[dongu];

            for (int i = 0; i < dongu; i++)
            {
                if (x != 0 && y != 0)
                {
                    left = dizi[y, x - 1];
                    top = dizi[y - 1, x];
                    diag = dizi[y - 1, x - 1];

                    if (left < top)
                    {
                        if (top < diag)
                        {
                            x--;
                            y--;

                            koordinat[i] = "Ç-";

                        }
                        else if (top > diag)
                        {
                            y--;

                            koordinat[i] = "Y-";
                        }
                        else
                        {
                            x--;
                            y--;
                            koordinat[i] = "Ç-";
                        }
                    }
                    else if (left > top)
                    {
                        if (left < diag)
                        {
                            x--;
                            y--;

                            koordinat[i] = "Ç-";

                        }
                        else if (left > diag)
                        {
                            x--;
                            koordinat[i] = "S-";
                        }
                        else
                        {
                            x--;
                            y--;
                            koordinat[i] = "Ç-";

                        }
                    }
                    else
                    {
                        if (top < diag)
                        {
                            x--;
                            y--;

                            koordinat[i] = "Ç-";
                        }
                    }

                }
            }
            
            for (int i = 0; i < koordinat.Length; i++)
            {
                label1.Text=label1.Text + koordinat[i];
            }

            /*
            label10.Text = "";
            label12.Text = "";
            label13.Text = "";
            string h1="";
            string h2="";

            for (int i = 0; i < dongu; i++)
            {
                label10.Text = label10.Text + koordinat[i]+" ";
                h1 = text1.Substring(i, 1);
                h2 = text2.Substring(i, 1);
                if(koordinat[i] == "left")
                {
                    label12.Text = label12.Text + h1;
                    label13.Text = label13.Text + "_"+h2;
                }
                else if (koordinat[i] == "top")
                {
                    label12.Text= label12.Text +"_"+ h1;
                    label13.Text = label13.Text+h2;
                }
                else if (koordinat[i]=="diag")
                {
                    label12.Text = label12.Text + h1;
                    label13.Text = label13.Text + h2;
                }
            }
           
            string ters1="",ters2="";
            for (int i = ttext1.Length-1; i>=0 ; i--)
            {
                ters1 = ters1+ttext1[i];
            }
            for (int i = ttext2.Length - 1; i >= 0; i--)
            {
                ters2 = ters2 + ttext2[i];
            }
            label10.Text = ters1+"   "+ters2;
            
            for (int i = label1.Text.Length-1; i > 0; i--)
            {
                if (label1.Text.Substring(i-1, 1) == "L")
                {
                    label2.Text=ters1[i]+"";
                }
                else if(label1.Text.Substring(i-1, 1) == "D")
                {
                    label2.Text = ters1[i] + "";
                    label3.Text = ters2[i] + "";
                }
                else if (label1.Text.Substring(i - 1, 1) == "T")
                {
                    
                }
            }
            */

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string[] satir = File.ReadAllLines(appDirectory + "/seq1.txt");
            string[] satir1 = File.ReadAllLines(appDirectory + "/seq2.txt");


            if (textBox4.Text == "" && textBox5.Text == "")
            {

                if (satir.Length > 0)
                {
                    textBox4.Text = satir[1];
                }
                if (satir1.Length > 0)
                {
                    textBox5.Text = satir1[1];
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
        }
    }
}
