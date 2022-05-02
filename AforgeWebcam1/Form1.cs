using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace AforgeWebcam1
{
    public partial class Form1 : Form
    {
        string path1;
        string ndrive;
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
            path1 = @"\\leosv02ecc1.ec.local\userdata\Manufacturing\GSTest\CommonRW\case marking samples  2022\"; 
            ndrive = "N:\\Manufacturing\\GSTest\\CommonRW\\case marking samples  2022";
            
            // The program will attempt to save the image as a .Jpeg to specified path then close the project
            // If an error occurs, the error will be displayed to the user then close the project.
            if (videoCaptureDevice.IsRunning)
            {
                Thread.Sleep(3000);
                try
                {
                    
                    Bitmap bm = new Bitmap(pic.Image);
                    bm.Save(path1 + LotID.Text + ".jpg", ImageFormat.Jpeg);
                    videoCaptureDevice.Stop();
                    MessageBox.Show(LotID.Text + " has been saved to " + ndrive);
                    Application.Exit();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    videoCaptureDevice.Stop();
                    MessageBox.Show(error.Message);
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("Please select Start Camera before you select save.");
                Application.Restart();
            }
        }
    }
}