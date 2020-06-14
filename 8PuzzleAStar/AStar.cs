using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _8PuzzleAStar
{
    class AStar
    {
        public String heuristicMethod { get; set; }
        public Panel panel{ get; set; }
        private int MaxNod = 5000;
        public Node KontrolNode { get; set; }
        public Node AcikNodeKontrol { get ;set; }
        public Node GirisNode { get; set; }
        public  List<Node> Path { get; set; }
        public NumericUpDown nudDerinlik { get; set; }
        public NumericUpDown nudAçıkListe { get; set; }
        public NumericUpDown nudKapalıListe { get; set; }
        public AStar(Node input , Panel panel,NumericUpDown açıkListe,NumericUpDown kapalıListe,NumericUpDown derinlik,NumericUpDown numericUpDownMAxNode,String heuristicType) {
            Path = new List<Node>();
            this.nudDerinlik = derinlik;
            this.nudAçıkListe = açıkListe;
            this.nudKapalıListe = kapalıListe;
            this.GirisNode = input;
            this.KontrolNode = null;
            this.AcikNodeKontrol = null;
            this.panel = panel;
            this.MaxNod =(int) numericUpDownMAxNode.Value;
            this.heuristicMethod = heuristicType;
        }
        public void Cöz()
        {
            Path.Clear();
            GirisNode.MaliyetAyarla();
            List<Node> kapalıListe = new List<Node>();
            ÖncelikliKuyruk quee = new ÖncelikliKuyruk();
            quee.Koy(GirisNode);
           // GirisNode.MatrisiYazdır();
            while (quee.Count() != 0)
            {
                
                Node q = quee.Al();
                kapalıListe.Add(q);
                Path.Add(q);
                if (quee.Count()>=MaxNod)
                {
                    MessageBox.Show("Max Node Aşıldı Yeni Bir Giris Yapınız");
                    break;
                }
                if (Fonksiyon.MatrisKarsilatır(q.Matris, Form1.sonDurumMatrisi))
                {

                    MessageBox.Show("Bulundu","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Yaz(q);
                    nudAçıkListe.Value = quee.Count()-1;
                    return;
                }

                List<Node> cocuklar = q.CocuklarıOlustur();
                foreach (Node cocuk in cocuklar)
                {
                    cocuk.MaliyetAyarla();
                    if (IsInAcıkListe(cocuk, quee))
                    {

                        if (cocuk.FDegeri <= AcikNodeKontrol.FDegeri)
                        {
                            quee.AcikListe.Remove(AcikNodeKontrol);
                            quee.Koy(cocuk);
                            continue;
                        }
                        continue;


                    }
                    else if (IsInKapalıListe(cocuk, kapalıListe))
                    {


                        if ((cocuk.FDegeri <= KontrolNode.FDegeri))
                        {
                            
                            quee.Koy(cocuk);
                            kapalıListe.Remove(KontrolNode);
                            continue;

                        }

                        continue;
                    }

                    else
                        quee.Koy(cocuk);

                }
            

            }


        }
        
        public  Node RemoveFromKapalıListe(Node node, List<Node> kapalıListe)
        {
            Node temp = null;
            foreach (Node item in kapalıListe)
            {
                if (Fonksiyon.MatrisKarsilatır(node.Matris, item.Matris))
                {
                    temp = item;
                    kapalıListe.Remove(item);
                    break;
                }
            }
            return temp;
        }
        public  bool IsInKapalıListe(Node node, List<Node> kapalıListe)
        {
            foreach (Node item in kapalıListe)
            {
                if (Fonksiyon.MatrisKarsilatır(item.Matris, node.Matris))
                {
                    KontrolNode = item;
                    return true;
                }


            }
            return false;
        }
        public  bool IsInAcıkListe(Node node, ÖncelikliKuyruk quee)
        {
            foreach (Node item in quee.AcikListe)
            {
                if (Fonksiyon.MatrisKarsilatır(item.Matris, node.Matris))
                {
                    AcikNodeKontrol = item;
                    return true;

                }

            }
            return false;
        }
        public void Yaz(Node q)
        {
           
            panel.Controls.Clear();
            /*
            RichTextBox a = new RichTextBox();
            a.Dock = DockStyle.Top;
            a.Text = q.MatrisiStringOlarakAl();
            a.Parent = panel;
            a.Font=new Font("Times New Roman", 10, FontStyle.Bold);
            a.ReadOnly = true;
            panel.Controls.Add(a);

           */
            int count = 0;
            while (q!=null)
            {
                RichTextBox b = new RichTextBox();
                b.Dock = DockStyle.Top;
                b.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                b.Text = q.MatrisiStringOlarakAl();
                b.Parent = panel;
                b.ReadOnly = true;
                panel.Controls.Add(b);
                q = q.Parent;
                count++;
               
                
            }
            nudDerinlik.Value = count-1;
            nudKapalıListe.Value = Path.Count();
          
        }
    }
}
