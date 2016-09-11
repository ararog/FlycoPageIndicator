using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Com.Flyco.IndicatorSamples.Utils;
using Com.Flyco.PageIndicator.Anim.Select;
using FlycoPageIndicatorApp;
using Java.Lang;

namespace Com.Flyco.IndicatorSamples.UI
{
	[Activity(Label = "Sample", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait)]
	public class FlycoPageIndicatorActivity : AppCompatActivity
	{
		private View decorView;
		private ViewPager pager;
		private ViewPagerAdapter adapter;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_api);

			adapter = new ViewPagerAdapter();

			decorView = Window.DecorView;
			pager = ViewFindUtils.Find<ViewPager>(decorView, Resource.Id.viewPager);
			pager.Adapter = adapter;

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
			PageIndicator.Indicator.FlycoPageIndicator indicator = ViewFindUtils.Find<Com.Flyco.PageIndicator.Indicator.FlycoPageIndicator>(decorView, indicatorId);
			indicator.SetViewPager(pager, adapter.Count);
		}

		private void IndicatorAnim()
		{
			PageIndicator.Indicator.FlycoPageIndicator indicator = ViewFindUtils.Find<PageIndicator.Indicator.FlycoPageIndicator>(decorView, Resource.Id.indicator_circle_anim);
			indicator
				.SetIsSnap(true)
				.SetSelectAnimClass(typeof(ZoomInEnter))
                .SetViewPager(pager, adapter.Count);

	    }

		private void IndicatorRes()
		{
			PageIndicator.Indicator.FlycoPageIndicator indicator_res = ViewFindUtils.Find<PageIndicator.Indicator.FlycoPageIndicator>(decorView, Resource.Id.indicator_res);
			indicator_res
				.SetViewPager(pager, adapter.Count);
		}

		class ViewPagerAdapter : PagerAdapter
		{
			public override Object InstantiateItem(ViewGroup collection, int position)
			{

				int resId = 0;
				switch (position)
				{
					case 0:
						resId = Resource.Id.page_one;
						break;
					case 1:
						resId = Resource.Id.page_two;
						break;
					case 2:
						resId = Resource.Id.page_three;
						break;						
				}
				return collection.FindViewById(resId);
			}

			public override void DestroyItem(ViewGroup container, int position, Object obj)
			{
				
			}

			public override int Count
			{
				get
				{
					return 3;
				}
			}

			public override bool IsViewFromObject(View arg0, Object arg1)
			{
				return arg0 == ((View)arg1);
			}
		}
	}
}