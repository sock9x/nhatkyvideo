using QuartzTypeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace nhatkyvideo
{
    public partial class Form1 : Form
    {
        private const int WM_APP = 0x8000;
        private const int WM_GRAPHNOTIFY = WM_APP + 1;
        private const int EC_COMPLETE = 0x01;
        private const int WS_CHILD = 0x40000000;
        private const int WS_CLIPCHILDREN = 0x2000000;
        int newLocationX;
        int newLocationY;
        const int WM_NCHITTEST = 0x84;
        const int HTCLIENT = 0x1;
        const int HTCAPTION = 0x2;
        string formsach= @"D:\DPT\nhatkyvideo\nhatkyvideo\nhatkyvideo\img\biasach1.png";
        int biasach = 1;
        string thumuc = Directory.GetCurrentDirectory();
        DataTable db;
        // Giữ tham chiếu mức-form đến giao diện Media Control,     // để đoạn mã có thể điều khiển playback cho     // movie được nạp hiện tại.     
        private IMediaControl mc = null;

        // Giữ tham chiếu mức-form đến cửa sổ video trong     // trường hợp nó cần được thay đổi kích thước.    
        private IVideoWindow videoWindow = null;

        public Form1()
        {
          
           thumuc= thumuc.Remove(thumuc.Length - 10, 10);
            InitializeComponent();
            db = DBSQLServerUtils.getdata("select * from AnhDong ad join GhiChu gc on ad.IDanhdong=gc.IDanhdong join Video vd   on vd.IDvideo=gc.IDvideo ");
            Bitmap bmcntrol = new Bitmap(thumuc + @"\ico\if_icon-gear-a_211669.png");
            BitmapRegion.CreateRegion(btn_control1,bmcntrol);
            BitmapRegion.CreateRegion(btn_control2, bmcntrol);
            bmcntrol = new Bitmap(thumuc + @"\ico\delete-icon.png");
            BitmapRegion.CreateRegion(btn_close, bmcntrol);
            bmcntrol = new Bitmap(thumuc + @"\ico\math-minus-icon.png");
            BitmapRegion.CreateRegion(btn_minus, bmcntrol);
        }
      /*  protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }*/

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (biasach <=13)
            {
                

                formsach = thumuc + @"\img\biasach" + biasach.ToString() + ".png";
                Bitmap bmForm = new Bitmap(formsach);
                this.Height = bmForm.Height + 10;
                this.Width = bmForm.Width + 10;
                BitmapRegion.CreateRegion(ckb_time2, bmForm);
                ckb_time2.Image = bmForm;
                biasach++;
            }
            else
            {

                timer1.Stop();
            }
                
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap bmForm = new Bitmap(formsach);
          //  BitmapRegion.CreateRegion(this, bmForm);
            timer1.Start();
            string a = db.Rows[0][9].ToString();
            FilgraphManager graphManager = new FilgraphManager();
            graphManager.RenderFile(db.Rows[0][9].ToString());

            // Gắn cửa sổ video vào PictureBox trên form.             
            try
            {
                videoWindow = (IVideoWindow)graphManager;
                videoWindow.Owner = (int)ptb_1.Handle;
                videoWindow.WindowStyle = WS_CHILD | WS_CLIPCHILDREN;
                videoWindow.SetWindowPosition(ptb_1.ClientRectangle.Left, ptb_1.ClientRectangle.Top, ptb_1.ClientRectangle.Width, ptb_1.ClientRectangle.Height);
                videoWindow.WindowState = 100;
            }
            catch
            {
                // Lỗi có thể xảy ra nếu file không có                 // video source (chẳng hạn, file MP3).                 // Bạn có thể bỏ qua lỗi này và vẫn cho phép                  // playback tiếp tục (không có hình).      
            }

            // Bắt đầu playback (bất đồng bộ).             
            mc = (IMediaControl)graphManager;

            mc.Run();
            mc.Pause();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            newLocationX = e.X;
            newLocationY = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            Left = Left + (e.X - newLocationX);
            Top = Top + (e.Y - newLocationY);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
