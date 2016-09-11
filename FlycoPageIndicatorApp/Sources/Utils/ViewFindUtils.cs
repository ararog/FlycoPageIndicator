using Android.Util;
using Android.Views;

namespace Com.Flyco.IndicatorSamples.Utils
{
	public class ViewFindUtils
	{
		public static T Hold<T>(View view, int id) where T : View
		{
			SparseArray<View> viewHolder = (SparseArray<View>)view.Tag;

			if (viewHolder == null)
			{
				viewHolder = new SparseArray<View>();
				view.Tag = viewHolder;
			}

			View childView = viewHolder.Get(id);

			if (childView == null)
			{
				childView = view.FindViewById(id);
				viewHolder.Put(id, childView);
			}

			return (T)childView;
		}

		public static T Find<T>(View view, int id) where T : View
		{
			return (T)view.FindViewById(id);
		}
	}
}