using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Com.Flyco.IndicatorSamples.Adapter
{

	public class SimpleHomeAdapter : BaseAdapter
	{

		private Context context;
		private string[] items;
		private DisplayMetrics dm;

		public SimpleHomeAdapter(Context context, string[] items)
		{
			this.context = context;
			this.items = items;
			dm = new DisplayMetrics();
			((Activity)context).WindowManager.DefaultDisplay.GetMetrics(dm);
		}


		public override int Count
		{
			get
			{
				return items.Length;
			}
		}

		public override Object GetItem(int position)
		{
			return null;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{

			int padding = (int)(dm.Density * 10);

			TextView tv = new TextView(context);
			tv.Text = items[position];
			tv.SetTextSize(ComplexUnitType.Sp, 18);
			tv.SetTextColor(Color.ParseColor("#468ED0"));
			// tv.setGravity(Gravity.CENTER);
			tv.SetPadding(padding, padding, padding, padding);
			AbsListView.LayoutParams lp = new AbsListView.LayoutParams(AbsListView.LayoutParams.MatchParent,
			                                                           AbsListView.LayoutParams.WrapContent);
			tv.LayoutParameters = lp;
			return tv;
		}
	}
}