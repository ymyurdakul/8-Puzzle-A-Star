using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using _8PuzzleAStar;
using System.Windows.Forms;

namespace _8PuzzleAStar
{
    class Node
    {
        //Sezgisel Maliyet manhattan kullanıldı , misplaced tile yönteminde verimli çalışmadı
        public int SezgiselMaliyet { get; set; }
        //g(x) degerini realistic cost olarak isimlendirdim
        public int GercekMaliyet { get; set; }
        public int FDegeri { get; set; }

        //Node nin ebeveynini( ) gosteriyor.
        public Node Parent { get; set; }
        public int[,] Matris{ get; set; }


        public Node(int[,] matris, Node parent)
        {
            Matris = new int[3, 3];
            SezgiselMaliyet = 0;
            GercekMaliyet = 0;
            FDegeri = 0;
            this.Parent = parent;
            this.Matris = matris.Clone() as int[,];

        }
        public void MaliyetAyarla()
        {
            SezgiselMaliyetAyarla();
            GercekDegerHesapla();
            FDegeriHesapla();

        }
        public List<Node> CocuklarıOlustur( )
        {
            List<Node> children = new List<Node>();
            //Boşluğun nerde oldugunu anlamaya yarar
            int satirPos = 0;
            int sutunPos = 0;
            bool isFound = false;
            for (int satir = 0; satir < 3; satir++)
            {
                for (int sutun = 0; sutun < 3; sutun++)
                {
                    if (this.Matris[satir, sutun] == 0)
                    {
                        satirPos = satir;
                        sutunPos = sutun;
                        isFound = true;
                        break;
                    }
                    if (isFound) break;
                }
            }
            Node sag = YerDegistir( satirPos, sutunPos, satirPos, sutunPos + 1,this);
            Node sol = YerDegistir(satirPos, sutunPos, satirPos, sutunPos - 1,this);


            Node alt = YerDegistir( satirPos, sutunPos, satirPos + 1, sutunPos,this);
            Node üst = YerDegistir( satirPos, sutunPos, satirPos - 1, sutunPos,this);
            if (sag != null)
                children.Add(sag);
            if (sol != null)
                children.Add(sol);
            if (alt != null)
                children.Add(alt);
            if (üst != null)
                children.Add(üst);
            return children;

        }
         public Node YerDegistir(int mevcutSatir, int mevcutSutun, int toSatir, int toSutun,Node parent)
      {
          if (toSatir < 0 | toSutun < 0 | toSutun > 2 | toSatir > 2)
              return null;
          int[,] matrisCopy = (int[,])this.Matris.Clone();

          int temp = matrisCopy[mevcutSatir, mevcutSutun];
          matrisCopy[mevcutSatir, mevcutSutun] = matrisCopy[toSatir, toSutun];
          matrisCopy[toSatir, toSutun] = temp;

          //writeMatris(matrisCopy);
          Node t = new Node(matrisCopy,parent);
          return t;
      }
        private void SezgiselMaliyetAyarla()
        { 
            int tempHeuristic = 0;
            for (int satir = 0; satir < 3; satir++)
            {
                for (int sutun = 0; sutun < 3; sutun++)
                {
                    int data=this.Matris[satir, sutun];
                    if (data == 0)
                        continue;
                   
                    List<int> otherPosition = CıkısMatrisPozisyonAl(data);
                    int realRow = otherPosition[0];
                    int realCol = otherPosition[1];
                
                   
                    int cost = Math.Abs(satir-realRow)+Math.Abs(sutun-realCol);
                    tempHeuristic += cost;


                }
            }
            this.SezgiselMaliyet = tempHeuristic;



        }
        private List<int> CıkısMatrisPozisyonAl(int data) {
            List<int> pos=new List<int>();
            for (int satir = 0; satir < 3; satir++)
            {
                for (int sutun = 0; sutun < 3; sutun++)
                {
                    if (Form1.sonDurumMatrisi[satir, sutun] == data)
                    {
                        pos.Add(satir);
                        pos.Add(sutun);

                    }
                     
                }
            }
            return pos;
        }


        private void GercekDegerHesapla()
        {
        
            if (Parent == null)
                this.GercekMaliyet = 0;
            else
                this.GercekMaliyet = Parent.GercekMaliyet + 1;
        
        }
        private void FDegeriHesapla()
        {
            this.FDegeri =  SezgiselMaliyet+GercekMaliyet;
        }
        public void MatrisiYazdır()
        {
            
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    builder.Append(" " + Matris[i, j]);

                }
                builder.Append(Environment.NewLine);
            }
            MessageBox.Show(builder.Append("F Cost" + this.FDegeri+ "\nH Cost" +
                this.SezgiselMaliyet).ToString()+ "\nR Cost" + this.GercekMaliyet+"\n");
     
           
             

        }
        public String MatrisiStringOlarakAl()
        {
             
            StringBuilder builder = new StringBuilder();
        
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    builder.Append(" " + Matris[i, j]);

                }
                builder.Append(Environment.NewLine);
            }
           return  builder.Append("F Degeri: " + this.FDegeri + "\nH Degeri: " +this.SezgiselMaliyet.ToString() + "\nG Degeri: " + this.GercekMaliyet + "\n").ToString();
           
           

        }
        


    }
}
