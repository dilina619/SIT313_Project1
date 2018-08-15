using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;


namespace UtilityBox
{
    [Activity(Label = "Activity1", MainLauncher = true)]
    public class Activity1 : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.menu);

            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += Button_Click;
            
        }

        private void Button_Click(object sender, EventArgs e)
        {

            SetContentView(Resource.Layout.Main);
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

    }
}