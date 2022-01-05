using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace sudoku21
{
    public partial class Form1 : Form
    {
       
        SudokuCell[,] cells = new SudokuCell[21, 21];
        static char[,] Sudoku = new char[21, 21];
        static int[,] Sudokuu = new int[21, 21];
        static int[,] Sudoku0 = new int[9, 9];
        static int[,] Sudoku1 = new int[9, 9];
        static int[,] Sudoku2 = new int[9, 9];
        static int[,] Sudoku3 = new int[9, 9];
        static int[,] Sudoku4 = new int[9, 9];
        static int graf0 = 0;
        static int graf1 = 0;
        static int graf2 = 0;
        static int graf3 = 0;
        static int graf4 = 0;
        static List<Edgee> liste = new List<Edgee>();
        public Form1()
        {
            InitializeComponent();
            noktalariOku();
            donustur();
            eksiknoktasayisi();
            t4.Start();
            t4.Join();
            t0.Start();
            t0.Join();
            t1.Start();
            t1.Join();
            t2.Start();
            t2.Join();
            donustur2();
            donustur();
            Sudoku3 = SudokuCoz2(Sudoku3);
            t3.Start();
            t3.Join();
            donustur22();
            txtBosalt();
            dosyayaYaz(0);
            dosyayaYaz(1);
            dosyayaYaz(2);
            dosyayaYaz(3);
            dosyayaYaz(4);
            createCells(Sudokuu);
        }
        Thread t0 = new Thread(t =>
        {
            SudokuCoz(Sudoku0, 0, 0, 0);


        });
        Thread t1 = new Thread(t =>
        {
            SudokuCoz(Sudoku1, 0, 0, 1);


        });
        Thread t2 = new Thread(t =>
        {
            SudokuCoz(Sudoku2, 0, 0, 2);


        });
        Thread t3 = new Thread(t =>
        {
            SudokuCoz(Sudoku3, 0, 0, 3);


        });
        Thread t4 = new Thread(t =>
        {
            SudokuCoz(Sudoku4, 0, 0, 4);


        });
        public void txtBosalt()
        {
            TextWriter tw0 = new StreamWriter(@"C:\Users\LENOVO\Desktop\Sudoku\thread0.txt");
            tw0.Write("");
            tw0.Close();
            TextWriter tw1 = new StreamWriter(@"C:\Users\LENOVO\Desktop\Sudoku\thread1.txt");
            tw1.Write("");
            tw1.Close();
            TextWriter tw2 = new StreamWriter(@"C:\Users\LENOVO\Desktop\Sudoku\thread2.txt");
            tw2.Write("");
            tw2.Close();
            TextWriter tw3 = new StreamWriter(@"C:\Users\LENOVO\Desktop\Sudoku\thread3.txt");
            tw3.Write("");
            tw3.Close();
            TextWriter tw4 = new StreamWriter(@"C:\Users\LENOVO\Desktop\Sudoku\thread4.txt");
            tw4.Write("");
            tw4.Close();
        }
        public int[] Doldurmap()
        {
            int[] dizii = new int[9];
            for (int i = 0; i < 9; i++)
            {
                dizii[i] = i + 1;
            }
            return dizii;
        }

        public int[] Doldurman()
        {
            int[] dizii = new int[9];
            for (int i = 0; i < 9; i++)
            {
                dizii[i] = 0;
            }
            return dizii;
        }
        public int[,] SudokuCoz2(int[,] dizi)
        {
            List<Edge> atama = new List<Edge>();

            for (int ıop = 0; ıop < 300; ıop++)
            {
                if (ıop == 0)
                {
                    int k = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (dizi[i, j] == 0)
                            {
                                atama.Add(new Edge()
                                {
                                    x = i,
                                    y = j,
                                    value = Doldurmap()
                                });
                            }
                            else
                            {
                                atama.Add(new Edge()
                                {
                                    x = i,
                                    y = j,
                                    value = Doldurman()
                                });
                                atama[k].value[dizi[i, j] - 1] = dizi[i, j];
                            }
                            k++;
                        }
                    }
                }
                for (int i = 0; i < 81; i++)
                {
                    int x = i / 9;
                    int y = i % 9;

                    if (dizi[x, y] != 0)
                        continue;

                    for (int j = 0; j < 9; j++)
                    {
                        if (dizi[j, y] != 0)
                        {
                            atama[i].value[dizi[j, y] - 1] = 0;
                        }
                        if (dizi[x, j] != 0)
                        {
                            atama[i].value[dizi[x, j] - 1] = 0;
                        }

                    }

                    for (int a = 0; a < 9; a++)
                    {
                        for (int b = 0; b < 9; b++)
                        {
                            if ((x / 3 < 1 && y / 3 < 1) && (a / 3 < 1 && b / 3 < 1) && dizi[a, b] != 0)
                            {
                                atama[i].value[dizi[a, b] - 1] = 0;
                            }
                            if ((x / 3 < 1 && y / 3 == 1) && (a / 3 < 1 && b / 3 == 1) && dizi[a, b] != 0)
                            {
                                atama[i].value[dizi[a, b] - 1] = 0;
                            }
                            if ((x / 3 < 1 && y / 3 > 1) && (a / 3 < 1 && b / 3 > 1) && dizi[a, b] != 0)
                            {
                                atama[i].value[dizi[a, b] - 1] = 0;
                            }
                            if ((x / 3 == 1 && y / 3 < 1) && (a / 3 == 1 && b / 3 < 1) && dizi[a, b] != 0)
                            {
                                atama[i].value[dizi[a, b] - 1] = 0;
                            }
                            if ((x / 3 == 1 && y / 3 == 1) && (a / 3 == 1 && b / 3 == 1) && dizi[a, b] != 0)
                            {
                                atama[i].value[dizi[a, b] - 1] = 0;
                            }
                            if ((x / 3 == 1 && y / 3 > 1) && (a / 3 == 1 && b / 3 > 1) && dizi[a, b] != 0)
                            {
                                atama[i].value[dizi[a, b] - 1] = 0;
                            }
                            if ((x / 3 > 1 && y / 3 < 1) && (a / 3 > 1 && b / 3 < 1) && dizi[a, b] != 0)
                            {
                                atama[i].value[dizi[a, b] - 1] = 0;
                            }
                            if ((x / 3 > 1 && y / 3 == 1) && (a / 3 > 1 && b / 3 == 1) && dizi[a, b] != 0)
                            {
                                atama[i].value[dizi[a, b] - 1] = 0;
                            }
                            if ((x / 3 > 1 && y / 3 > 1) && (a / 3 > 1 && b / 3 > 1) && dizi[a, b] != 0)
                            {
                                atama[i].value[dizi[a, b] - 1] = 0;
                            }
                        }
                    }

                    int r = 0;
                    for (int o = 0; o < 9; o++)
                    {
                        if (atama[i].value[o] == 0)
                        {
                            r++;
                        }
                    }

                    if (r == 8)
                    {
                        for (int l = 0; l < 9; l++)
                        {
                            if (atama[i].value[l] != 0)
                            {
                                dizi[x, y] = atama[i].value[l];

                            }
                        }
                    }
                    r = 0;
                }
            }
            return dizi;
        }



        static int N = 9;

        static bool SudokuCoz(int[,] Sudokum, int satir, int sutun, int uy)
        {
            if (satir == N - 1 && sutun == N)
                return true;

            if (sutun == N)
            {
                satir++;
                sutun = 0;
            }

            if (Sudokum[satir, sutun] != 0)
                return SudokuCoz(Sudokum, satir, sutun + 1, uy);

            for (int deger = 1; deger < 10; deger++)
            {
                if (dogruMu(Sudokum, satir, sutun, deger))
                {
                    Sudokum[satir, sutun] = deger;
                    liste.Add(new Edgee()
                    {
                        referans = uy,
                        x = satir,
                        y = sutun,
                        value = deger
                    });
                    if (SudokuCoz(Sudokum, satir, sutun + 1, uy))
                        return true;
                }

                Sudokum[satir, sutun] = 0;
            }
            return false;
        }
        static bool dogruMu(int[,] Sudokum, int satir, int sutun, int deger)
        {
            for (int a = 0; a <= 8; a++)
                if (Sudokum[satir, a] == deger)
                    return false;


            for (int b = 0; b <= 8; b++)
                if (Sudokum[b, sutun] == deger)
                    return false;

            int startSatir = satir - satir % 3, startsutun  = sutun - sutun % 3;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (Sudokum[i + startSatir, j + startsutun] == deger)
                        return false;

            return true;
        }
        private static void dosyayaYaz(int uy)
        {
            string dosya_yolu = "";
            if (uy == 0)
            {
                dosya_yolu = @"C:\Users\LENOVO\Desktop\Sudoku\thread0.txt";
                FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                for (int i = 0; i < liste.Count; i++)
                {
                    if (liste[i].referans == uy)
                    {
                        sw.WriteLine(liste[i].x.ToString() + "." + liste[i].y.ToString() + ":" + liste[i].value.ToString());
                    }

                }
                sw.WriteLine();
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            else if (uy == 1)
            {
                dosya_yolu = @"C:\Users\LENOVO\Desktop\Sudoku\thread1.txt";
                FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                for (int i = 0; i < liste.Count; i++)
                {
                    if (liste[i].referans == uy)
                    {
                        sw.WriteLine(liste[i].x.ToString() + "." + liste[i].y.ToString() + ":" + liste[i].value.ToString());
                    }

                }
                sw.WriteLine();
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            else if (uy == 2)
            {
                dosya_yolu = @"C:\Users\LENOVO\Desktop\Sudoku\thread2.txt";
                FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                for (int i = 0; i < liste.Count; i++)
                {
                    if (liste[i].referans == uy)
                    {
                        sw.WriteLine(liste[i].x.ToString() + "." + liste[i].y.ToString() + ":" + liste[i].value.ToString());
                    }

                }
                sw.WriteLine();
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            else if (uy == 3)
            {
                dosya_yolu = @"C:\Users\LENOVO\Desktop\Sudoku\thread3.txt";
                FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                for (int i = 0; i < liste.Count; i++)
                {
                    if (liste[i].referans == uy)
                    {
                        sw.WriteLine(liste[i].x.ToString() + "." + liste[i].y.ToString() + ":" + liste[i].value.ToString());
                    }

                }
                sw.WriteLine();
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            else if (uy == 4)
            {
                dosya_yolu = @"C:\Users\LENOVO\Desktop\Sudoku\thread4.txt";
                FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                for (int i = 0; i < liste.Count; i++)
                {
                    if (liste[i].referans == uy)
                    {
                        sw.WriteLine(liste[i].x.ToString() + "." + liste[i].y.ToString() + ":" + liste[i].value.ToString());
                    }

                }
                sw.WriteLine();
                sw.Flush();
                sw.Close();
                fs.Close();
            }
        }

        public void createCells(int[,] Sudoku)
        {
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    cells[i, j] = new SudokuCell();
                    cells[i, j].Font = new Font("Arial", 14);
                    cells[i, j].Size = new Size(30, 30);
                    cells[i, j].ForeColor = Color.Black;
                    cells[i, j].Location = new Point(i * 30, j * 30);
                    cells[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : System.Drawing.ColorTranslator.FromHtml("#4666a1");
                    cells[i, j].FlatStyle = FlatStyle.Popup;
                    cells[i, j].FlatAppearance.BorderColor = Color.Black;
                    cells[i, j].Text = Sudoku[j, i].ToString();
                    if (cells[i, j].Text == (10).ToString())
                    {
                        cells[i, j].Visible = false;
                    }
                    if (cells[i, j].Text == "0".ToString())
                    {

                        cells[i, j].Text = " ".ToString();

                    }
                    panel1.Controls.Add(cells[i, j]);
                    cells[i, j].Value = Convert.ToChar(Sudoku[j, i]);
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int a = graf0;
            int b = graf1;
            int c = graf2;
            int d = graf3;
            int f = graf4;

            chart1.Series["Thread"].Points.Add(a);
            chart1.Series["Thread"].Points.Add(b);
            chart1.Series["Thread"].Points.Add(c);
            chart1.Series["Thread"].Points.Add(d);
            chart1.Series["Thread"].Points.Add(f);

            chart1.Series["Thread"].Points[0].AxisLabel = "Thread 0";
            chart1.Series["Thread"].Points[1].AxisLabel = "Thread 1";
            chart1.Series["Thread"].Points[2].AxisLabel = "Thread 2";
            chart1.Series["Thread"].Points[3].AxisLabel = "Thread 3";
            chart1.Series["Thread"].Points[4].AxisLabel = "Thread 4";

            chart1.Series["Thread"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#4666a1");
            chart1.Series["Thread"].Points[1].Color = System.Drawing.ColorTranslator.FromHtml("#4666a1");
            chart1.Series["Thread"].Points[2].Color = System.Drawing.ColorTranslator.FromHtml("#4666a1");
            chart1.Series["Thread"].Points[3].Color = System.Drawing.ColorTranslator.FromHtml("#4666a1");
            chart1.Series["Thread"].Points[4].Color = System.Drawing.ColorTranslator.FromHtml("#4666a1");
            chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#f0f0f0");

        }
        public void noktalariOku()
        {
            string path = @"C:\Users\LENOVO\Desktop\Sudoku\noktalar.txt";
            string[] txt = System.IO.File.ReadAllLines(path);
            for (int x = 0; x < 21; x++)
            {
                char[] noktalar = txt[x].ToCharArray();
                for (int y = 0; y < 21; y++)
                {
                    if (noktalar.Length == 18)
                    {
                        if (y < 9)
                        {
                            Sudoku[x, y] = noktalar[y];
                        }
                        else if (y >= 12 && y < 21)
                        {
                            Sudoku[x, y] = noktalar[y - 3];
                        }
                        else
                        {
                            Sudoku[x, y] = '-';
                        }
                    }
                    else if (noktalar.Length == 21)
                    {
                        Sudoku[x, y] = noktalar[y];
                    }
                    else if (noktalar.Length == 9)
                    {
                        if (y < 6)
                        {
                            Sudoku[x, y] = '-';

                        }
                        else if (y >= 6 && y < 15)
                        {
                            Sudoku[x, y] = noktalar[y - 6];
                        }
                        else
                        {
                            Sudoku[x, y] = '-';
                        }
                    }

                }

            }

            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    if (Sudoku[i, j] != '*' && Sudoku[i, j] != '-')
                    {
                        Sudokuu[i, j] = Convert.ToInt32(Sudoku[i, j].ToString());
                    }
                    else if (Sudoku[i, j] == '*')
                    {
                        Sudokuu[i, j] = 0;
                    }
                    else if (Sudoku[i, j] == '-')
                    {
                        Sudokuu[i, j] = 10;
                    }

                }

            }
        }
        public void donustur()
        {
            int r = 0;
            int t = 0;
            r = 0;
            for (int i = 6; i < 15; i++)
            {
                t = 0;
                for (int j = 6; j < 15; j++)
                {
                    Sudoku4[r, t] = Sudokuu[i, j];
                    t++;
                }
                r++;
            }
            r = 0;
            for (int i = 0; i < 9; i++)
            {
                t = 0;
                for (int j = 0; j < 9; j++)
                {
                    Sudoku0[r, t] = Sudokuu[i, j];
                    t++;
                }
                r++;
            }
            r = 0;
            for (int i = 0; i < 9; i++)
            {
                t = 0;
                for (int j = 12; j < 21; j++)
                {
                    Sudoku1[r, t] = Sudokuu[i, j];
                    t++;
                }
                r++;
            }
            r = 0;
            for (int i = 12; i < 21; i++)
            {
                t = 0;
                for (int j = 0; j < 9; j++)
                {
                    Sudoku2[r, t] = Sudokuu[i, j];
                    t++;
                }
                r++;
            }

            r = 0;
            for (int i = 12; i < 21; i++)
            {
                t = 0;
                for (int j = 12; j < 21; j++)
                {
                    Sudoku3[r, t] = Sudokuu[i, j];
                    t++;
                }
                r++;
            }

        }
        static public void donustur22()
        {
            int r = 0;
            int t = 0;
            r = 0;
            for (int i = 6; i < 15; i++)
            {
                t = 0;
                for (int j = 6; j < 15; j++)
                {
                    Sudokuu[i, j] = Sudoku4[r, t];
                    t++;
                }
                r++;
            }
            r = 0;
            r = 0;
            for (int i = 0; i < 9; i++)
            {
                t = 0;
                for (int j = 0; j < 9; j++)
                {
                    Sudokuu[i, j] = Sudoku0[r, t];
                    t++;
                }
                r++;
            }
            r = 0;
            for (int i = 0; i < 9; i++)
            {
                t = 0;
                for (int j = 12; j < 21; j++)
                {
                    Sudokuu[i, j] = Sudoku1[r, t];
                    t++;
                }
                r++;
            }
            r = 0;
            for (int i = 12; i < 21; i++)
            {
                t = 0;
                for (int j = 0; j < 9; j++)
                {
                    Sudokuu[i, j] = Sudoku2[r, t];
                    t++;
                }
                r++;
            }

            r = 0;
            for (int i = 12; i < 21; i++)
            {
                t = 0;
                for (int j = 12; j < 21; j++)
                {
                    Sudokuu[i, j] = Sudoku3[r, t];
                    t++;
                }
                r++;
            }

        }
        static public void donustur2()
        {
            int r = 0;
            int t = 0;
            r = 0;
            for (int i = 6; i < 15; i++)
            {
                t = 0;
                for (int j = 6; j < 15; j++)
                {
                    Sudokuu[i, j] = Sudoku4[r, t];
                    t++;
                }
                r++;
            }
            r = 0;
            for (int i = 0; i < 9; i++)
            {
                t = 0;
                for (int j = 0; j < 9; j++)
                {
                    Sudokuu[r, t] = Sudoku0[i, j];
                    t++;
                }
                r++;
            }
            r = 0;
            for (int i = 0; i < 9; i++)
            {
                t = 0;
                for (int j = 12; j < 21; j++)
                {
                    Sudokuu[i, j] = Sudoku1[r, t];
                    t++;
                }
                r++;
            }
            r = 0;
            for (int i = 12; i < 21; i++)
            {
                t = 0;
                for (int j = 0; j < 9; j++)
                {
                    Sudokuu[i, j] = Sudoku2[r, t];
                    t++;
                }
                r++;
            }

            r = 0;
            for (int i = 12; i < 21; i++)
            {
                t = 0;
                for (int j = 12; j < 21; j++)
                {
                    Sudokuu[i, j] = Sudoku3[r, t];
                    t++;
                }
                r++;
            }

        }
        static public void eksiknoktasayisi()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Sudoku0[i, j] == 0)
                    {
                        graf0++;
                    }
                    if (Sudoku1[i, j] == 0)
                    {
                        graf1++;
                    }
                    if (Sudoku2[i, j] == 0)
                    {
                        graf2++;
                    }
                    if (Sudoku3[i, j] == 0)
                    {
                        graf3++;
                    }
                    if (Sudoku4[i, j] == 0)
                    {
                        graf4++;
                    }
                }
            }
        }
        class SudokuCell : Button
        {
            public char Value { get; set; }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}