using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace AforgeWebcam1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        private void btnStart_Click(object sender, EventArgs e)
        {
            videoCaptureDevice=new VideoCaptureDevice(filterInfoCollection[cboCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pic.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboCamera.Items.Add(filterInfo.Name);
            cboCamera.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string poop = ".jpg";
            string path = @"N:\Manufacturing\GSTest\CommonRW\case marking samples  2022";
            try
            {
                videoCaptureDevice.Stop();
                pic.Image = 
                pic.Image.Save(path + @" + LotID.Text + poop", ImageFormat.Jpeg);
            } catch (Exception error){
                Console.WriteLine(error);
                videoCaptureDevice.Stop();
                MessageBox.Show(error.Message);
                videoCaptureDevice.Start();
            }
        }
    }
}
