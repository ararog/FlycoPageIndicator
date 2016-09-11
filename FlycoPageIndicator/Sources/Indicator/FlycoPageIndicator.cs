using System;
using System.Collections.Generic;
using Android.Animation;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Com.Flyco.Pageindicator.Anim.Base;
using Com.Flyco.PageIndicator.Indicator.Base;
using FlycoPageIndicator;
using Java.Lang;
using NineOldAndroids.View;

namespace Com.Flyco.PageIndicator.Indicator
{

	public class FlycoPageIndicator : LinearLayout, IPageIndicator
	{

		private Context context;
		private ViewPager vp;
		private RelativeLayout rl_parent;
		private View selectView;
		private List<ImageView> indicatorViews = new List<ImageView>();
		private int count;

		private int currentItem;
		private int lastItem;
		private int indicatorWidth;
		private int indicatorHeight;
		private int indicatorGap;
		private int cornerRadius;
		private Drawable selectDrawable;
		private Drawable unSelectDrawable;
		private int strokeWidth;
		private int strokeColor;
		private bool isSnap;

		private Type selectAnimClass;
		private Type unselectAnimClass;

		public FlycoPageIndicator(Context context) : this(context, null)
		{

		}

		public FlycoPageIndicator(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			this.context = context;
			SetClipChildren(false);
			SetClipToPadding(false);

			rl_parent = new RelativeLayout(context);
			rl_parent.SetClipChildren(false);
			rl_parent.SetClipToPadding(false);
			AddView(rl_parent);

			SetGravity(GravityFlags.Center);
			TypedArray ta = context.ObtainStyledAttributes(attrs, Resource.Styleable.FlycoPageIndicaor);
			indicatorWidth = ta.GetDimensionPixelSize(Resource.Styleable.FlycoPageIndicaor_fpi_width, dp2px(6));
			indicatorHeight = ta.GetDimensionPixelSize(Resource.Styleable.FlycoPageIndicaor_fpi_height, dp2px(6));
			indicatorGap = ta.GetDimensionPixelSize(Resource.Styleable.FlycoPageIndicaor_fpi_gap, dp2px(8));
			cornerRadius = ta.GetDimensionPixelSize(Resource.Styleable.FlycoPageIndicaor_fpi_cornerRadius, dp2px(3));
			strokeWidth = ta.GetDimensionPixelSize(Resource.Styleable.FlycoPageIndicaor_fpi_strokeWidth, dp2px(0));
			strokeColor = ta.GetColor(Resource.Styleable.FlycoPageIndicaor_fpi_strokeColor, Color.ParseColor("#ffffff"));
			isSnap = ta.GetBoolean(Resource.Styleable.FlycoPageIndicaor_fpi_isSnap, false);

			int selectColor = ta.GetColor(Resource.Styleable.FlycoPageIndicaor_fpi_selectColor, Color.ParseColor("#ffffff"));
			int unselectColor = ta.GetColor(Resource.Styleable.FlycoPageIndicaor_fpi_unselectColor, Color.ParseColor("#88ffffff"));
			int selectRes = ta.GetResourceId(Resource.Styleable.FlycoPageIndicaor_fpi_selectRes, 0);
			int unselectRes = ta.GetResourceId(Resource.Styleable.FlycoPageIndicaor_fpi_unselectRes, 0);
			ta.Recycle();

			if (selectRes != 0)
			{
				this.selectDrawable = Resources.GetDrawable(selectRes);
			}
			else {
				this.selectDrawable = getDrawable(selectColor, cornerRadius);
			}

			if (unselectRes != 0)
			{
				this.unSelectDrawable = Resources.GetDrawable(unselectRes);
			}
			else {
				this.unSelectDrawable = getDrawable(unselectColor, cornerRadius);
			}
		}

		/** call before setViewPager. set indicator width, unit dp, default 6dp */
		public FlycoPageIndicator SetIndicatorWidth(float indicatorWidth)
		{
			this.indicatorWidth = dp2px(indicatorWidth);
			return this;
		}

		/** call before setViewPager. set indicator height, unit dp, default 6dp */
		public FlycoPageIndicator SetIndicatorHeight(float indicatorHeight)
		{
			this.indicatorHeight = dp2px(indicatorHeight);
			return this;
		}

		/** call before setViewPager. set gap between two indicators, unit dp, default 6dp */
		public FlycoPageIndicator SetIndicatorGap(float indicatorGap)
		{
			this.indicatorGap = dp2px(indicatorGap);
			return this;
		}

		/** call before setViewPager. set indicator select color, default "#ffffff" "#88ffffff" */
		public FlycoPageIndicator SetIndicatorSelectColor(int selectColor, int unselectColor)
		{
			this.selectDrawable = getDrawable(selectColor, cornerRadius);
			this.unSelectDrawable = getDrawable(unselectColor, cornerRadius);
			return this;
		}

		/** call before setViewPager. set indicator corner raduis, unit dp, default 3dp */
		public FlycoPageIndicator SetCornerRadius(float cornerRadius)
		{
			this.cornerRadius = dp2px(cornerRadius);
			return this;
		}

		/** call before setViewPager. set width of the stroke used to draw the indicators, unit dp, default 0dp */
		public FlycoPageIndicator SetStrokeWidth(int strokeWidth)
		{
			this.strokeWidth = strokeWidth;
			return this;
		}

		/** call before setViewPager. set color of the stroke used to draw the indicators. default "#ffffff" */
		public FlycoPageIndicator SetStrokeColor(int strokeColor)
		{
			this.strokeColor = strokeColor;
			return this;
		}

		/** call before setViewPager. Whether or not the selected indicator snaps to the indicators. default false */
		public FlycoPageIndicator SetIsSnap(bool isSnap)
		{
			this.isSnap = isSnap;
			return this;
		}

		/** call before setViewPager. set indicator select anim. only valid when isSnap is true */
		public FlycoPageIndicator SetSelectAnimClass(Type selectAnimClass)
		{
			this.selectAnimClass = selectAnimClass;
			return this;
		}

		/** call before setViewPager. set indicator unselect anim. only valid when isSnap is true */
		public FlycoPageIndicator setUnselectAnimClass(Type unselectAnimClass)
		{
			this.unselectAnimClass = unselectAnimClass;
			return this;
		}

		public int getCurrentItem()
		{
			return currentItem;
		}

		public int IndicatorWidth
		{
			get
			{
				return indicatorWidth;
			}
		}

		public int IndicatorHeight
		{
			get
			{
				return indicatorHeight;
			}
		}

		public int IndicatorGap
		{
			get
			{
				return indicatorGap;
			}
		}

		public int CornerRadius
		{
			get
			{
				return cornerRadius;
			}
		}

		public int StrokeWidth
		{
			get
			{
				return strokeWidth;
			}
		}

		public int StrokeColor
		{
			get
			{
				return strokeColor;
			}
		}

		public bool IsSnap
		{
			get
			{
				return isSnap;
			}
		}

		public void SetCurrentItem(int item)
		{
			if (isValid())
			{
				vp.CurrentItem = item;
			}
		}

		public void SetViewPager(ViewPager vp)
		{
			this.vp = vp;
			if (isValid())
			{
				this.count = vp.Adapter.Count;
				vp.RemoveOnPageChangeListener(this);
				vp.AddOnPageChangeListener(this);

				CreateIndicators();
			}
		}

		public void SetViewPager(ViewPager vp, int realCount)
		{
			this.vp = vp;
			if (isValid())
			{
				this.count = realCount;
				vp.RemoveOnPageChangeListener(this);
				vp.AddOnPageChangeListener(this);

				CreateIndicators();
			}
		}

		public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
		{
			if (!isSnap)
			{
				this.currentItem = position;

				float tranlationX = (indicatorWidth + indicatorGap) * (currentItem + positionOffset);
				ViewHelper.SetTranslationX(selectView, tranlationX);
			}
		}

		public void OnPageSelected(int position)
		{
			if (isSnap)
			{
				this.currentItem = position;

				for (int i = 0; i < indicatorViews.Count; i++)
				{
					indicatorViews[i].SetImageDrawable(i == position ? selectDrawable : unSelectDrawable);
				}
				AnimSwitch(position);
				lastItem = position;
			}
		}

		public void OnPageScrollStateChanged(int state)
		{

		}

		private void AnimSwitch(int position)
		{
			try
			{
				if (selectAnimClass != null)
				{
					if (position == lastItem)
					{
						IndicatorBaseAnimator animator = Activator.CreateInstance(selectAnimClass) as IndicatorBaseAnimator;
						animator.PlayOn(indicatorViews[position]);
					}
					else {
						IndicatorBaseAnimator animator = Activator.CreateInstance(selectAnimClass) as IndicatorBaseAnimator;
						animator.PlayOn(indicatorViews[position]);
						if (unselectAnimClass == null)
						{
							animator = Activator.CreateInstance(selectAnimClass) as IndicatorBaseAnimator;
							animator.Interpolator(new ReverseInterpolator()).PlayOn(indicatorViews[lastItem]);
						}
						else {
							animator = Activator.CreateInstance(unselectAnimClass) as IndicatorBaseAnimator;
							animator.PlayOn(indicatorViews[lastItem]);
						}
					}
				}
			}
			catch (Java.Lang.Exception e)
			{
				e.PrintStackTrace();
			}
		}

		private void CreateIndicators()
		{
			if (count <= 0)
			{
				return;
			}

			indicatorViews.Clear();
			rl_parent.RemoveAllViews();

			LinearLayout ll_unselect_views = new LinearLayout(context);
			rl_parent.AddView(ll_unselect_views);

			for (int i = 0; i < count; i++)
			{
				ImageView iv = new ImageView(context);
				iv.SetImageDrawable(!isSnap ? unSelectDrawable : (currentItem == i ? selectDrawable : unSelectDrawable));
				LayoutParams lp = new LayoutParams(indicatorWidth,
						indicatorHeight);
				lp.LeftMargin = i == 0 ? 0 : indicatorGap;
				ll_unselect_views.AddView(iv, lp);
				indicatorViews.Add(iv);
			}

			if (!isSnap)
			{
				RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams(indicatorWidth,
						indicatorHeight);
				lp.LeftMargin = (indicatorWidth + indicatorGap) * currentItem;
				selectView = new View(context);
				selectView.SetBackgroundDrawable(selectDrawable);
				rl_parent.AddView(selectView, lp);
			}

			AnimSwitch(currentItem);
		}

		private bool isValid()
		{
			if (vp == null)
			{
				throw new IllegalStateException("ViewPager can not be NULL!");
			}

			if (vp.Adapter == null)
			{
				throw new IllegalStateException("ViewPager adapter can not be NULL!");
			}

			return true;
		}

		private class ReverseInterpolator : Java.Lang.Object, IInterpolator
		{
			float ITimeInterpolator.GetInterpolation(float input)
			{
				return System.Math.Abs(1.0f - input);
			}

			float IInterpolator.GetInterpolation(float input)
			{
				return System.Math.Abs(1.0f - input);
			}
		}

		private GradientDrawable getDrawable(int color, float raduis)
		{
			GradientDrawable drawable = new GradientDrawable();
			drawable.SetCornerRadius(raduis);
			drawable.SetStroke(strokeWidth, new Color(strokeColor));
			drawable.SetColor(color);

			return drawable;
		}


		private int dp2px(float dp)
		{
			float scale = context.Resources.DisplayMetrics.Density;
			return (int)(dp * scale + 0.5f);
		}


		protected override IParcelable OnSaveInstanceState()
		{
			Bundle bundle = new Bundle();
			bundle.PutParcelable("instanceState", base.OnSaveInstanceState());
			bundle.PutInt("currentItem", currentItem);
			return bundle;
		}

		protected override void OnRestoreInstanceState(IParcelable state)
		{
			if (state is Bundle) {
				Bundle bundle = (Bundle)state;
				currentItem = bundle.GetInt("currentItem");
				state = bundle.GetParcelable("instanceState") as IParcelable;
			}

			base.OnRestoreInstanceState(state);
		}
	}
}