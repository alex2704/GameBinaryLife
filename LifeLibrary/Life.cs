using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeLibrary
{
    public class Life
    {
        public int[,] Map { get; set; }// 0 -пусто, 1- живой, -1 - рождается, 2 - умирают
        public int W { get; set; }
        public int H { get; set; }
        int[,] sum; //sum[x,y] - сколько рганизмов правее и ниже клетки x,y 

        public Life(int w, int h)
        {
            W = w;
            H = h;
            Map = new int[w,h];
            sum = new int[w,h];
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    Map[x, y] = 0;
        }

        public int Turn(int x, int y)
        {
            Map[x, y] = Map[x, y] == 0 ? 1 : 0;
            return Map[x, y];
        }

        public int Get_map(int x, int y)
        {
            if (x < 0 || x >= W)
                return 0;
            if (y < 0 || y >= H)
                return 0;
            return Map[x,y];
        }

        public int Get_sum(int x, int y)
        {
            if (x >= W)
                return 0;
            if (y >= H)
                return 0;
            if (x < 0)
                x = 0;
            if (y < 0)
                y = 0;
            return sum[x, y];
        }

        private int Around(int x, int y)
        {
            int sum = 0;
            for (int sx = -1; sx <= 1; sx++)
                for (int sy = -1; sy <= 1; sy++) 
                {
                    if (Get_map(x + sx, y + sy) > 0)
                        sum++;
                }
            return sum;
        }

        private void Prepare()//подготовка массива перед функцией Arround 2
        {
            for (int x = W - 1; x >= 0; x--)
                for (int y = H - 1; y >= 0; y--) 
                {
                    sum[x, y] = Get_map(x, y) + Get_sum(x + 1, y) + Get_sum(x, y + 1) - Get_sum(x + 1, y + 1);
                }
        }

        private int Around2(int x, int y)//более легкая сложность алгоритма по сравнению с Arround для поиска ячеек
        {
            return Get_sum(x - 1, y - 1) - Get_sum(x + 2, y - 1) - Get_sum(x - 1, y + 2) + Get_sum(x + 2, y + 2);
        }

        public void Step1() // Отмечаем где родятся и умрут существа
        {
            Prepare();
            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    //int a = Around(x,y);
                    int a = Around2(x,y);
                    if (Map[x, y] == 1)
                    {
                        if (a <= 2)
                            Map[x, y] = 2;
                        if (a >= 5)
                            Map[x, y] = 2;
                    }
                    else
                    {
                        if (a == 3)
                            Map[x, y] = -1;
                    }
                }
            }
        }

        public void Step2()  //убираем умергих и размещаем родившихся
        {
            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    if (Map[x, y] == -1)
                        Map[x, y] = 1;
                    else if (Map[x, y] == 2)
                        Map[x, y] = 0;
                }
            }
        }
    }
}
