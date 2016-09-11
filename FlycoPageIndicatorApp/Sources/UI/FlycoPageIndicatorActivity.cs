using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Com.Flyco.IndicatorSamples.Banner;
using Com.Flyco.IndicatorSamples.Utils;
using Com.Flyco.PageIndicator.Anim.Select;
using FlycoPageIndicatorApp;
using Java.Lang;

namespace Com.Flyco.IndicatorSamples.UI
{
	[Activity(Label = "Sample", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = ScreenOrientation.Portrait)]
	public class FlycoPageIndicatorActivity : AppCompatActivity
	{

		private int[] resIds = {Resource.Mipmap.item1, Resource.Mipmap.item2,
			Resource.Mipmap.item3, Resource.Mipmap.item4};
		
		private List<Integer> resList;
		private View decorView;
		private SimpleImageBanner banner;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_api);

			resList = new List<Integer>();
			for (int i = 0; i < resIds.Length; i++)
			{
				resList.Add(resIds[i]);
			}

			decorView = Window.DecorView;
			banner = ViewFindUtils.Find(decorView, Resource.Id.banner);
			banner.setSource(resList).startScroll();

			Indicator(Resource.Id.indicator_circle);
			Indicator(Resource.Id.indicator_square);
			Indicator(Resource.Id.indicator_round_rectangle);
			Indicator(Resource.Id.indicator_circle_stroke);
			Indicator(Resource.Id.indicator_square_stroke);
			Indicator(Resource.Id.indicator_round_rectangle_stroke);
			Indicator(Resource.Id.indicator_circle_snap);

			IndicatorAnim();
			IndicatorRes();
		}

		private void Indicator(int indicatorId)
		{
			FlycoPageIndicator indicator = ViewFindUtils.Find<FlycoPageIndicator>(decorView, indicatorId);
			indicator.setViewPager(banner.ViewPager, resList.Count);
		}

		private void IndicatorAnim()
		{
			FlycoPageIndicator indicator = ViewFindUtils.Find(decorView, Resource.Id.indicator_circle_anim);
			indicator
				.setIsSnap(true)
				.setSelectAnimClass(ZoomInEnter.GetType())
                .setViewPager(banner.ViewPager, resList.Count);

	    }

		private void IndicatorRes()
		{
			FlycoPageIndicator indicator_res = ViewFindUtils.Find<FlycoPageIndicator>(decorView, Resource.Id.indicator_res);
			indicator_res
				.setViewPager(banner.ViewPager, resList.Count);
		}
	}
}