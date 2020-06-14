using System;
using System.Collections.Generic;
using System.Text;

namespace _8PuzzleAStar
{
    class Fonksiyon
    {
        

        public static bool MatrisKarsilatır(int[,] m1, int[,] m2)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (m1[i, j] != m2[i, j])
                        return false;
                }
            }
            return true;
        }
        public static void MatrisKopyala(int[,] m1, int[,] m2)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    m2[i, j] = m1[i,j];
                }
            }
           
        }
    }
}
