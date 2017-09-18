using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace iotAC_Remote
{
    [Activity(Label = "iotAC_Remote", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public Button OnButton, OffButton;
        SignalrClient signalrClient = new SignalrClient();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        protected override async void OnStart()
        {
            base.OnStart();
            //connect with asp.net core signalr server
            await signalrClient.connet();
            //初始化UI
            initView();
        }

        public void initView()
        {
            OnButton = FindViewById<Button>(Resource.Id.OnButton);
            OnButton.Click += OnOnButtonClicked;
            OffButton = FindViewById<Button>(Resource.Id.OffButton);
            OffButton.Click += OnOffButtonClicked;
        }

        public async void OnOnButtonClicked(object sender, EventArgs e)
        {
            await signalrClient.send("on");
        }

        public async void OnOffButtonClicked(object sender, EventArgs e)
        {
            await signalrClient.send("off");
        }
    }
}

