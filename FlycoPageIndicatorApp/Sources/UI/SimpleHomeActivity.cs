using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Com.Flyco.IndicatorSamples.Adapter;

namespace Com.Flyco.IndicatorSamples.UI
{
	[Activity(Icon = "@mipmap/icon", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SimpleHomeActivity : AppCompatActivity
	{
		private string[] items = {"RoundCornerIndicator", "FlycoPageIndicator"};
		private Type[] classes = { typeof(RoundCornerIndicatorActivity), typeof(FlycoPageIndicatorActivity) };

		protected override void OnCreate(Bundle savedInstanceState) 
		{
			base.OnCreate(savedInstanceState);
			ListView lv = new ListView(this);
			lv.CacheColorHint = Color.Transparent;
			lv.SetFadingEdgeLength(0);
			lv.Adapter = new SimpleHomeAdapter(this, items);

			lv.ItemClick += (sender, e) =>
			{
				Intent intent = new Intent(this, classes[e.Position]);
				StartActivity(intent);
			};


			SetContentView(lv);
	    }
	}
}