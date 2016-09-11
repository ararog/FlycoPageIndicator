using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using FlycoPageIndicatorApp;
using Java.Lang;

namespace Com.Flyco.IndicatorSamples.Banner
{
	public class SimpleImageBanner : BaseBanner<Integer, SimpleImageBanner>
	{
		public SimpleImageBanner(Context context) : this(context, null, 0)
		{

		}

		public SimpleImageBanner(Context context, IAttributeSet attrs) : this(context, attrs, 0)
		{

		}

		public SimpleImageBanner(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
			
		}

		public override void OnTitleSelect(TextView tv, int position)
		{
		}

		public override View OnCreateItemView(int position)
		{
			View inflate = View.Inflate(context, Resource.Layout.adapter_simple_image, null);
			ImageView iv = ViewFindUtils.Find(inflate, Resource.Id.iv);

			Integer i = list.get(position);
			int itemWidth = dm.widthPixels;
			int itemHeight = (int)(itemWidth * 360 * 1.0f / 640);
			iv.SetScaleType(ImageView.ScaleType.CenterCrop);
			iv.LayoutParameters = new LinearLayout.LayoutParams(itemWidth, itemHeight);
			iv.SetImageResource(i);

			return inflate;
		}

		//    private RoundCornerIndicaor indicator;

		public override View OnCreateIndicator()
		{
			//        indicator = new RoundCornerIndicaor(context);
			//        indicator.setViewPager(vp, list.size());
			//        return indicator;
			return null;
		}

		public void SetCurrentIndicator(int i)
		{
			//  indicator.setCurrentItem(i);
		}
	}
}