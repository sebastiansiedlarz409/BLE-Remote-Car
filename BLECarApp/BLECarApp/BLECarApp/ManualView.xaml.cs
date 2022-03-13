using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BLECarApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManualView : ContentPage
    {
        private BtConnector bt;

        private bool connected = false;

        private byte SPEED = 0;
        private sbyte DIR = 0;

        public ManualView()
        {
            InitializeComponent();

            ConnectionBtn.Clicked += ConnectBtn_Clicked;

            SpeedUpBtn.Clicked += SpeedUpBtn_Clicked;
            SpeedDwBtn.Clicked += SpeedDwBtn_Clicked;
            StopBtn.Clicked += StopBtn_Clicked;

            ParamsBtn.IsEnabled = false;
        }

        private void RightBtn_Clicked(object sender, EventArgs e)
        {
            if (DIR <= 50)
                DIR += 10;

            SendCommands();
        }

        private void LeftBtn_Clicked(object sender, EventArgs e)
        {
            if (DIR >= -50)
                DIR -= 10;

            SendCommands();
        }

        private void StopBtn_Clicked(object sender, EventArgs e)
        {
            SPEED = 0;
            DIR = 0;

            SendCommands();
        }

        private void SpeedDwBtn_Clicked(object sender, EventArgs e)
        {
            if (SPEED >= 10)
                SPEED -= 10;

            SendCommands();
        }

        private void SpeedUpBtn_Clicked(object sender, EventArgs e)
        {
            if (SPEED < 100)
                SPEED += 10;

            SendCommands();
        }

        private void OnConnection()
        {
            ConnectionBtn.Text = "Disconnect!";
            connected = true;
        }

        private void Disconnect()
        {
            DIR = 0;
            SPEED = 0;

            bt.Disconnect();
            ConnectionBtn.Text = "Connect!";
            connected = false;
        }

        private void ConnectBtn_Clicked(object sender, EventArgs e)
        {
            if (connected)
            {
                if(bt != null)
                    Disconnect();
            }
            else
            {
                if (bt == null)
                    bt = new BtConnector();

                bt.Connect(OnConnection);
            }
        }

        private void SendCommands()
        {
            if(bt != null)
                bt.Send(new byte[] { 0xAA, SPEED, (byte)DIR }, 3);
        }
    }
}