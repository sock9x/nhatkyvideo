﻿using QuartzTypeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nhatkyvideo
{
    public partial class Form1 : Form
    {
        const int WM_NCHITTEST = 0x84;
        const int HTCLIENT = 0x1;
        const int HTCAPTION = 0x2;
        string formsach= @"D:\DPT\nhatkyvideo\nhatkyvideo\nhatkyvideo\img\biasach0.png";
        int biasach = 0;
        public Form1()
        {
           


            //Bitmap bmForm = new Bitmap(@"D:\DPT\nhatkyvideo\nhatkyvideo\nhatkyvideo\img\trangsach2.png");
            InitializeComponent();

           // BitmapRegion.CreateRegion(btnExit, bmExit);
        }
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (biasach <=6)
            {
                biasach++;

                formsach = @"D:\DPT\nhatkyvideo\nhatkyvideo\nhatkyvideo\img\biasach" + biasach.ToString() + ".png";
                Bitmap bmForm = new Bitmap(formsach);
                BitmapRegion.CreateRegion(this, bmForm);
            }
            else
            {

                timer1.Stop();
            }
                
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap bmForm = new Bitmap(formsach);
            BitmapRegion.CreateRegion(this, bmForm);
            timer1.Start();
        }
    }
}