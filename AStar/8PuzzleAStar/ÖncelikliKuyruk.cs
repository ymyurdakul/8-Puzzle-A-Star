using System;
using System.Collections.Generic;
using System.Text;

namespace _8PuzzleAStar
{
    class ÖncelikliKuyruk
    {
        public List<Node> AcikListe { get; set; }
        public ÖncelikliKuyruk()
        {
            AcikListe = new List<Node>();
        }
        public void Koy(Node node)
        {
            AcikListe.Add(node);
        }
        //
        public Node Al()
        {
            Node temp = null;
            if (AcikListe.Count <= 0)
                return temp;
            temp = AcikListe[0];
            for (int i = 1; i < AcikListe.Count; i++)
                if (AcikListe[i].FDegeri < temp.FDegeri)
                    temp = AcikListe[i];
            AcikListe.Remove(temp);
            return temp;
        }
        public void KuyruguYazdır()
        {
             
            AcikListe.ForEach(x=>x.MatrisiYazdır());

        }
        //Count
        public int Count()
        {
            return AcikListe.Count;
        }
    }
}
