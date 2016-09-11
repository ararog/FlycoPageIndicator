using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Com.Flyco.IndicatorSamples.Utils;
using Com.Flyco.IndicatorSamples.Banner;
using FlycoPageIndicatorApp;
using System.Collections.Generic;
using Android.Content.PM;
using Android.App;
using Com.Flyco.Pageindicator.Indicator;

namespace Com.Flyco.IndicatorSamples.UI
{
	[Activity(Label = "Sample", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = ScreenOrientation.Portrait)]
	public class RoundCornerIndicatorActivity : AppCompatActivity
	{
		private int[] resIds = {Resource.Mipmap.item1, Resource.Mipmap.item2,
			Resource.Mipmap.item3, Resource.Mipmap.item4};
		private List<int> resList;
		private View decorView;
		private SimpleImageBanner banner;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_rci);

			resList = new List<int>();
			for (int i = 0; i < resIds.Length; i++)
			{
				resList.Add(resIds[i]);
			}

			decorView = Window.DecorView;
			banner = ViewFindUtils.Find(decorView, Resource.Id.banner_circle);
			banner.SetSource(resList).startScroll();

			Indicator(Resource.Id.indicator_circle);
			Indicator(Resource.Id.indicator_square);
			Indicator(Resource.Id.indicator_round_rectangle);
			Indicator(Resource.Id.indicator_circle_stroke);
			Indicator(Resource.Id.indicator_square_stroke);
			Indicator(Resource.Id.indicator_round_rectangle_stroke);
		}

		private void Indicator(int indicatorId)
		{
			RoundCornerIndicator indicator = ViewFindUtils.Find<RoundCornerIndicator>(decorView, indicatorId);
			indicator.SetViewPager(banner.ViewPager, resList.Count);
		}
	}
}