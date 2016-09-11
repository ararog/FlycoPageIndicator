using System.Collections.Generic;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Com.Flyco.PageIndicator.Indicator.Base;
using FlycoPageIndicator;
using Java.Lang;

namespace Com.Flyco.Pageindicator.Indicator
{

	/** A pratice demo use GradientDrawable to realize the effect of JakeWharton's CirclePageIndicator */
	public class RoundCornerIndicator : View, IPageIndicator
	{

		private Context context;
		private ViewPager vp;
		private List<GradientDrawable> unselectDrawables = new List<GradientDrawable>();
		private List<Rect> unselectRects = new List<Rect>();
		private GradientDrawable selectDrawable = new GradientDrawable();
		private Rect selectRect = new Rect();
		private int count;
		private int currentItem;
		private float positionOffset;
		private int indicatorWidth;
		private int indicatorHeight;
		private int indicatorGap;
		private int cornerRadius;
		private int selectColor;
		private int unselectColor;
		private int strokeWidth;
		private int strokeColor;
		private bool isSnap;

		public RoundCornerIndicator(Context context) : this(context, null)
		{

		}

		public RoundCornerIndicator(Context context, IAttributeSet attrs) : this(context, attrs, 0)
		{

		}

		public RoundCornerIndicator(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
			this.context = context;

			TypedArray ta = context.ObtainStyledAttributes(attrs, Resource.Styleable.RoundCornerIndicaor);
			indicatorWidth = ta.GetDimensionPixelSize(Resource.Styleable.RoundCornerIndicaor_rci_width, dp2px(6));
			indicatorHeight = ta.GetDimensionPixelSize(Resource.Styleable.RoundCornerIndicaor_rci_height, dp2px(6));
			indicatorGap = ta.GetDimensionPixelSize(Resource.Styleable.RoundCornerIndicaor_rci_gap, dp2px(8));
			cornerRadius = ta.GetDimensionPixelSize(Resource.Styleable.RoundCornerIndicaor_rci_cornerRadius, dp2px(3));
			strokeWidth = ta.GetDimensionPixelSize(Resource.Styleable.RoundCornerIndicaor_rci_strokeWidth, dp2px(0));
			selectColor = ta.GetColor(Resource.Styleable.RoundCornerIndicaor_rci_selectColor, Color.ParseColor("#ffffff"));
			unselectColor = ta.GetColor(Resource.Styleable.RoundCornerIndicaor_rci_unselectColor, Color.ParseColor("#88ffffff"));
			strokeColor = ta.GetColor(Resource.Styleable.RoundCornerIndicaor_rci_strokeColor, Color.ParseColor("#ffffff"));
			isSnap = ta.GetBoolean(Resource.Styleable.RoundCornerIndicaor_rci_isSnap, false);

			ta.Recycle();
		}

		public void SetViewPager(ViewPager vp)
		{
			if (IsValid(vp))
			{
				this.vp = vp;
				this.count = vp.Adapter.Count;
				vp.RemoveOnPageChangeListener(this);
				vp.AddOnPageChangeListener(this);

				unselectDrawables.Clear();
				unselectRects.Clear();
				for (int i = 0; i < count; i++)
				{
					unselectDrawables.Add(new GradientDrawable());
					unselectRects.Add(new Rect());
				}

				Invalidate();
			}
		}

		public void SetViewPager(ViewPager vp, int realCount)
		{
			if (IsValid(vp))
			{
				this.vp = vp;
				this.count = realCount;
				vp.RemoveOnPageChangeListener(this);
				vp.AddOnPageChangeListener(this);

				unselectDrawables.Clear();
				unselectRects.Clear();
				for (int i = 0; i < count; i++)
				{
					unselectDrawables.Add(new GradientDrawable());
					unselectRects.Add(new Rect());
				}

				Invalidate();
			}
		}

		public void SetCurrentItem(int item)
		{
			if (IsValid(vp))
			{
				vp.CurrentItem = item;
			}
		}

		public int IndicatorWidth
		{
			set 
			{
				this.indicatorWidth = value;
				Invalidate();
			}
			get
			{
				return indicatorWidth;
			}
		}

		public int IndicatorHeight
		{
			set
			{
				this.indicatorHeight = value;
				Invalidate();
			}
			get
			{
				return indicatorHeight;
			}
		}

		public int IndicatorGap
		{
			set
			{
				this.indicatorGap = value;
				Invalidate();
			}
			get
			{
				return indicatorGap;
			}
		}

		public int CornerRadius
		{
			set
			{
				this.cornerRadius = value;
				Invalidate();
			}
			get
			{
				return cornerRadius;
			}
		}

		public int SelectColor
		{
			set
			{
				this.selectColor = value;
				Invalidate();
			}
			get
			{
				return selectColor;
			}
		}

		public int UnselectColor
		{
			set
			{
				this.unselectColor = value;
				Invalidate();
			}
			get
			{
				return unselectColor;
			}
		}

		public int StrokeWidth
		{
			set
			{
				this.strokeWidth = value;
				Invalidate();
			}
			get
			{
				return strokeWidth;
			}
		}

		public int StrokeColor
		{
			set
			{
				this.strokeColor = value;
				Invalidate();
			}
			get
			{
				return strokeColor;
			}
		}

		public bool IsSnap
		{
			set
			{
				this.isSnap = value;
				Invalidate();
			}
			get
			{
				return isSnap;
			}
		}

		private bool IsValid(ViewPager vp)
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

		public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
		{

			if (!isSnap)
			{
				currentItem = position;
				this.positionOffset = positionOffset;
				Invalidate();
			}
		}

		public void OnPageSelected(int position)
		{
			if (isSnap)
			{
				currentItem = position;
				Invalidate();
			}
		}

		public void OnPageScrollStateChanged(int state)
		{
		}

		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);

			if (count <= 0)
			{
				return;
			}

			int verticalOffset = PaddingTop + (Height - PaddingTop - PaddingBottom) / 2 - indicatorHeight / 2;
			int indicatorLayoutWidth = indicatorWidth * count + indicatorGap * (count - 1);
			int horizontalOffset = PaddingLeft + (Width - PaddingLeft - PaddingRight) / 2 - indicatorLayoutWidth / 2;

			DrawUnselect(canvas, count, verticalOffset, horizontalOffset);
			DrawSelect(canvas, verticalOffset, horizontalOffset);
		}

		private void DrawUnselect(Canvas canvas, int count, int verticalOffset, int horizontalOffset)
		{
			for (int i = 0; i < count; i++)
			{
				Rect rect = unselectRects[i];
				rect.Left = horizontalOffset + (indicatorWidth + indicatorGap) * i;
				rect.Top = verticalOffset;
				rect.Right = rect.Left + indicatorWidth;
				rect.Bottom = rect.Top + indicatorHeight;

				GradientDrawable unselectDrawable = unselectDrawables[i];
				unselectDrawable.SetCornerRadius(cornerRadius);
				unselectDrawable.SetColor(unselectColor);
				unselectDrawable.SetStroke(strokeWidth, new Color(strokeColor));
				unselectDrawable.Bounds = rect;
				unselectDrawable.Draw(canvas);
			}
		}

		private void DrawSelect(Canvas canvas, int verticalOffset, int horizontalOffset)
		{
			int delta = (int)((indicatorGap + indicatorWidth) * (isSnap ? 0 : positionOffset));

			selectRect.Left = horizontalOffset + (indicatorWidth + indicatorGap) * currentItem + delta;
			selectRect.Top = verticalOffset;
			selectRect.Right = selectRect.Left + indicatorWidth;
			selectRect.Bottom = selectRect.Top + indicatorHeight;

			selectDrawable.SetCornerRadius(cornerRadius);
			selectDrawable.SetColor(selectColor);
			selectDrawable.Bounds = selectRect;
			selectDrawable.Draw(canvas);
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			SetMeasuredDimension(MeasureWidth(widthMeasureSpec), MeasureHeight(heightMeasureSpec));
		}

		private int MeasureWidth(int widthMeasureSpec)
		{
			int result;
			MeasureSpecMode specMode = MeasureSpec.GetMode(widthMeasureSpec);
			int specSize = MeasureSpec.GetSize(widthMeasureSpec);
			if (specMode == MeasureSpecMode.Exactly || count == 0)
			{
				result = specSize;
			}
			else {
				int padding = PaddingLeft + PaddingRight;
				result = padding + indicatorWidth * count + indicatorGap * (count - 1);
				if (specMode == MeasureSpecMode.AtMost)
				{
					result = Math.Min(result, specSize);
				}
			}

			return result;
		}

		private int MeasureHeight(int heightMeasureSpec)
		{
			int result;
			MeasureSpecMode specMode = MeasureSpec.GetMode(heightMeasureSpec);
			int specSize = MeasureSpec.GetSize(heightMeasureSpec);
			if (specMode == MeasureSpecMode.Exactly)
			{
				result = specSize;
			}
			else {
				int padding = PaddingTop + PaddingBottom;
				result = padding + indicatorHeight;
				if (specMode == MeasureSpecMode.AtMost)
				{
					result = Math.Min(result, specSize);
				}
			}

			return result;
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
			if (state is Bundle)
			{
				Bundle bundle = (Bundle)state;
				currentItem = bundle.GetInt("currentItem");
				state = bundle.GetParcelable("instanceState") as IParcelable;
			}
			base.OnRestoreInstanceState(state);
		}

		protected int dp2px(float dp)
		{
			float scale = context.Resources.DisplayMetrics.Density;
			return (int)(dp * scale + 0.5f);
		}
	}
}