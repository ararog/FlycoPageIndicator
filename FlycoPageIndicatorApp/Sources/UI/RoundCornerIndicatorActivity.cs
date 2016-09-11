using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Com.Flyco.IndicatorSamples.Utils;
using FlycoPageIndicatorApp;
using System.Collections.Generic;
using Android.Content.PM;
using Android.App;
using Com.Flyco.Pageindicator.Indicator;
using Android.Support.V4.View;
using Java.Lang;

namespace Com.Flyco.IndicatorSamples.UI
{
	[Activity(Label = "Sample", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = ScreenOrientation.Portrait)]
	public class RoundCornerIndicatorActivity : AppCompatActivity
	{
		private int[] resIds = {Resource.Mipmap.item1, Resource.Mipmap.item2,
			Resource.Mipmap.item3, Resource.Mipmap.item4};
		private List<int> resList;
		private View decorView;
		private ViewPager pager;

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
			pager = ViewFindUtils.Find<ViewPager>(decorView, Resource.Id.viewPager);
			pager.Adapter = new ViewPagerAdapter();

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
			indicator.SetViewPager(pager, resList.Count);
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
				}
				return collection.FindViewById(resId);
			}

			public override int Count
			{
				get
				{
					return 2;
				}
			}

			public override bool IsViewFromObject(View arg0, Object arg1)
			{
				return arg0 == ((View)arg1);
			}
		}
	}
}