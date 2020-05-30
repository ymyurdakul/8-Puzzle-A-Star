using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _8PuzzleAStar
{
    public partial class Form1 : Form
    {
        public static int[,] sonDurumMatrisi = new int[3, 3] { { 1, 2, 3 }, { 8, 0, 4 }, { 7, 6, 5 } }; 
        int[,] sıfırMatris;
       
        AStar aStar;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           MessageBox.Show( File.ReadAllText("not.txt"),"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            cmbSezgiselYöntem.SelectedIndex = 0;
           
            
        }
        
       
        // Textboxlar txtBx ön ekinden sonra Giris veya Cikis takip etmekte sonrasında satır ve sutun numaraları yazmata
        private void btnÇöz_Click(object sender, EventArgs e)
        {
            int[,] girisMatris = new int[3, 3];
         

            int satir = 3, sutun = 3;
           
                
                for (int i = 0; i < satir; i++)
                {
                    for (int j = 0; j < sutun; j++)
                    {
                        TextBox tb = (TextBox)grpBoxGirisTutucu.Controls["txtBxGiris" + i + "" + j];
                        if (tb.Text == "-")
                        {
                            MessageBox.Show("Giris ve çıkış matrislerini kontrol edin eksik giriş var","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            return;
                        }
                   
                            girisMatris[i, j] = int.Parse(tb.Text);


                        tb = (TextBox)grpBoxCikisTutucu.Controls["txtBxCikis" + i + "" + j];
                        if (tb.Text == "-")
                        {
                            MessageBox.Show("Giris ve çıkış matrislerini kontrol edin eksik giriş var", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                            sonDurumMatrisi[i, j] = int.Parse(tb.Text);
                    }
                }
           // listBox1.Items.Clear();

         //   Fonksiyon.MatrisKopyala(cikisMatris,sonDurumMatrisi);
            Node giris = new Node(girisMatris,null);
            String yöntem = cmbSezgiselYöntem.SelectedItem.ToString();
            //selected indexi kontrol etmedim çünkü formun load kısmında comboboxa atama yaptım

            aStar = new AStar(giris,panel1,nudAcıkListe,nudKapalıListe,nudDerinlik,nudMaxNodeSayısı,yöntem);
            aStar.Cöz();
           
          
        }
        

        /*
         * Matrislere deger atamaya bütün butonların click eventi buraya bağlandı.
        */
        private void btnGiris1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int rakam=0;
            rakam = int.Parse(btn.Text);
            if (btn.Name.StartsWith("btnGiris"))
            {
               
                //bos olan ilk giris matrisine  rakamı atacak
                formGirisMatrisYerlestir(rakam);
            }
            else {
                //bos olan ilk cikis matris indeksine rakamı atacak
                formCikisMatrisYerlestir(rakam);
            }
            btn.Enabled = false;

        }
        public void formGirisMatrisYerlestir(int digit)
        {
            bool isFound = false;
            int satir = 3, sutun = 3;
            for (int i = 0; i < satir; i++)
            {
                if (isFound)
                    break;
                for (int j = 0; j < sutun; j++)
                {
                    TextBox tb = (TextBox)grpBoxGirisTutucu.Controls["txtBxGiris" + i + "" + j];
                    if (tb.Text == "-")
                    {
                        tb.Text = digit.ToString();
                        isFound = true;
                        break;
                    } 
                }
            }

        }

        public void formCikisMatrisYerlestir(int digit)
        {
            bool isFound = false;
            int satir = 3, sutun = 3;
            for (int i = 0; i < satir; i++)
            {
                if (isFound)
                    break;
                for (int j = 0; j < sutun; j++)
                {
                    TextBox tb = (TextBox)grpBoxCikisTutucu.Controls["txtBxCikis" + i + "" + j];
                    if (tb.Text == "-")
                    {
                        tb.Text = digit.ToString();
                        isFound = true;
                        break;
                    }
                }
            }

        }

        private void btnGirisTemizle_Click(object sender, EventArgs e)
        {
          
            for (int i = 0; i < 3; i++)
            {
                
                for (int j = 0; j < 3; j++)
                {
                    TextBox tb = (TextBox)grpBoxGirisTutucu.Controls["txtBxGiris" + i + "" + j];
                    tb.Text = "-";

                    
                }
            }
            for(int i=0;i<9;i++)
                ( (Button)grpBoxGirisTutucu.Controls["btnGiris" + i]).Enabled=true;
                 
        }

        private void btnCikisTemizle_Click(object sender, EventArgs e)
        {
           
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    TextBox tb = (TextBox)grpBoxCikisTutucu.Controls["txtBxCikis" + i + "" + j];
                    tb.Text = "-";

                }
            }
            for (int i = 0; i < 9; i++)
                ((Button)grpBoxCikisTutucu.Controls["btnCikis" + i]).Enabled = true;
        }

        private void btnGirisDoldur_Click(object sender, EventArgs e)
        {
            //Çözümü 5000 node dan az olan matrisleri depoladım
            String[]hazırMatrisler=File.ReadAllLines("data.txt");
            Random rnd = new Random();
            int x = rnd.Next(0,hazırMatrisler.Length);
            String matris=(hazırMatrisler[x]);
            txtBxGiris00.Text = matris[0].ToString();
            txtBxGiris01.Text = matris[1].ToString();
            txtBxGiris02.Text = matris[2].ToString();
            txtBxGiris10.Text = matris[3].ToString();
            txtBxGiris11.Text = matris[4].ToString();
            txtBxGiris12.Text = matris[5].ToString();
            txtBxGiris20.Text = matris[6].ToString();
            txtBxGiris21.Text = matris[7].ToString();
            txtBxGiris22.Text = matris[8].ToString();

            for (int i = 0; i < 9; i++)
                ((Button)grpBoxGirisTutucu.Controls["btnGiris" + i]).Enabled = false;

        }

        private void btnCıkısDoldur_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Her Giriş matrisi bu matris çözüm bulmaktadır.");
            //Hazır matrisler için en iyi çıkıs matrisini yazdım hepsi içinde çözüme gidebilen
            txtBxCikis00.Text = "1";
            txtBxCikis01.Text = "2";
            txtBxCikis02.Text = "3";
            txtBxCikis10.Text = "4";
            txtBxCikis11.Text = "5";
            txtBxCikis12.Text = "6";
            txtBxCikis20.Text = "7";
            txtBxCikis21.Text = "8";
            txtBxCikis22.Text = "0";
            for (int i = 0; i < 9; i++)
                ((Button)grpBoxCikisTutucu.Controls["btnCikis" + i]).Enabled = false;
        }
    }
}
