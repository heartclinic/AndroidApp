using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Hardware;
using Android.Util;
using System.Threading;
namespace App4
{
    [Activity(Label = "App4", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ISensorEventListener
    {
        float count = 0;
        float countnul = 1;
        Button button;
        int a = 0;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
           button = FindViewById<Button>(Resource.Id.MyButton);
          Button  button2 = FindViewById<Button>(Resource.Id.MyButton2);
            button.Click += delegate { countnul = 0; button.Text = string.Format("0 steps"); };
            button2.Click += delegate {  button.Text = string.Format("{0} steps!", count); };

            SensorManager senMgr = (SensorManager)GetSystemService(SensorService);
            Sensor counter = senMgr.GetDefaultSensor(SensorType.StepCounter);
           
            if (counter != null)
            {
                senMgr.RegisterListener(this, counter, SensorDelay.Normal);
            }


        }
        public static bool IsKitKatWithStepCounter(PackageManager pm)
        {

            // Require at least Android KitKat
            int currentApiVersion = (int)Build.VERSION.SdkInt;
            // Check that the device supports the step counter and detector sensors
            return currentApiVersion >= 19
            && pm.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepCounter)
            && pm.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepDetector);


        }
        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            Log.Info("SensorManager", "Sensor accuracy changed");
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (countnul==0)  countnul = e.Values[0];
        
            count =e.Values[0]-countnul;
          
            button.Text = string.Format("{0} steps!", count);
        }

    }
}

