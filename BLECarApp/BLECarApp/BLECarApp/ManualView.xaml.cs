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
        public ManualView()
        {
            InitializeComponent();

            ConnectionBtn.Clicked += ConnectBtn_Clicked;
            SendDataBtn.Clicked += SendDataBtn_Clicked;

            SpeedUpBtn.Clicked += SpeedUpBtn_Clicked;
            SpeedDwBtn.Clicked += SpeedDwBtn_Clicked;
            StopBtn.Clicked += StopBtn_Clicked;
            LeftBtn.Clicked += LeftBtn_Clicked;
            RightBtn.Clicked += RightBtn_Clicked;
        }

        private void RightBtn_Clicked(object sender, EventArgs e)
        {
            bt.Send(new byte[] { 0xEE, 0xEE, 0xEE }, 3);
        }

        private void LeftBtn_Clicked(object sender, EventArgs e)
        {
            bt.Send(new byte[] { 0xDD, 0xDD, 0xDD }, 3);
        }

        private void StopBtn_Clicked(object sender, EventArgs e)
        {
            bt.Send(new byte[] { 0xCC, 0xCC, 0xCC }, 3);
        }

        private void SpeedDwBtn_Clicked(object sender, EventArgs e)
        {
            bt.Send(new byte[] { 0xBB, 0xBB, 0xBB }, 3);
        }

        private void SpeedUpBtn_Clicked(object sender, EventArgs e)
        {
            bt.Send(new byte[] { 0xAA, 0xAA, 0xAA }, 3);
        }

        private void OnConnection(string text)
        {
            ConnectionBtn.Text = text;
        }

        private void ConnectBtn_Clicked(object sender, EventArgs e)
        {
            if (bt == null)
                bt = new BtConnector();

            bt.Connect(OnConnection);
        }

        private void SendDataBtn_Clicked(object sender, EventArgs e)
        {
            bt.Send(new byte[] { 0xAA, 0xAA, 0xAA }, 3);
        }
    }
}