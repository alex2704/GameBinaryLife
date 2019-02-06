using LifeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLife
{
    public partial class Form1 : Form
    {
        int box_w = 20;
        int box_h = 20;
        int w, h;

        Label[,] lab;
        Life life;

        Color co_none = Color.White;
        Color co_live = Color.Black;
        Color co_born = Color.Yellow;
        Color co_dies = Color.Gray;

        public Form1()
        {
            InitializeComponent();
            Init_labels();
        }

        private void Init_labels()
        {
            w = (panel.Width - 1) / box_w;
            h = (panel.Height - 1) / box_h;

            life = new Life(w, h);
            lab = new Label[w, h];

            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    Add_label(x,y);
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = ((Label)sender).Location.X / box_w;
            int y = ((Label)sender).Location.Y / box_h;
            int color = life.Turn(x,y);
            lab[x, y].BackColor = color == 1 ? co_live : co_none;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            life.Step1();
            F_refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            life.Step2();
            F_refresh();
        }

        private void F_refresh()
        {
            for (int x = 0; x < w; x++)
                for(int y = 0; y < h; y++)
                    switch (life.Get_map(x, y))
                    {
                        case 0: lab[x, y].BackColor = co_none; break;
                        case 1: lab[x, y].BackColor = co_live; break;
                        case 2: lab[x, y].BackColor = co_dies; break;
                        case -1: lab[x, y].BackColor = co_born; break;
                    }
        }

        private void Result_btn_Click(object sender, EventArgs e)
        {
            life.Step1();
            life.Step2();
            F_refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            life.Step1();
            life.Step2();
            F_refresh();
        }

        private void Add_label(int x, int y)
        {
            lab[x, y] = new Label();

            lab[x, y].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lab[x, y].Location = new System.Drawing.Point(x * box_w, y * box_h);
            lab[x, y].Size = new System.Drawing.Size(box_w+1, box_h+1);
            lab[x, y].Parent = panel;
            lab[x, y].MouseClick += new System.Windows.Forms.MouseEventHandler(this.label1_MouseClick);
        }

    }
}
