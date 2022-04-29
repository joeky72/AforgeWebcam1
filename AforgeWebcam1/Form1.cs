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
        string poop;
        string pee;
        string poo;
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

        public void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
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
            poop = ".jpg";
            pee = @"\\leosv02ecc1.ec.local\userdata\Manufacturing\GSTest\DeptData\Conlan\";
            poo = @"\\leosv02ecc1.ec.local\userdata\Manufacturing\GSTest\CommonRW\case marking samples  2022";

            // @"\\leosv02ecc1.ec.local\userdata\Manufacturing\GSTest\CommonRW\case marking samples  2022\"
            // I cannot save to this path from the code, still working on why

            // The program will attempt to save the image as a .Jpeg to specified path then close the project
            // If an error occurs, the error will be displayed to the user then close the project.
            try
            {
                Bitmap bm = new Bitmap(pic.Image);
                bm.Save(pee + LotID.Text + poop, ImageFormat.Jpeg);
                System.IO.File.Move(pee + LotID.Text + poop, poo + LotID.Text + poop);
                videoCaptureDevice.Stop();
                MessageBox.Show(LotID.Text + " has been saved to " + poo);
                Environment.Exit(0);
            } 
            catch (Exception error){
                Console.WriteLine(error);
                videoCaptureDevice.Stop();
                MessageBox.Show(error.Message);
                Environment.Exit(0);
            }
        }
    }
}
