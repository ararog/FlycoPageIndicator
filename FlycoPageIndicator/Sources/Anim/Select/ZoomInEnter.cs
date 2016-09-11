using Android.Views;
using Com.Flyco.Pageindicator.Anim.Base;
using NineOldAndroids.Animation;

namespace Com.Flyco.PageIndicator.Anim.Select
{
	public class ZoomInEnter : IndicatorBaseAnimator
	{

		public ZoomInEnter()
		{
			this.duration = 200;
		}

		public override void SetAnimation(View view)
		{
			this.animatorSet.PlayTogether(new Animator[]{
					ObjectAnimator.OfFloat(view, "scaleX", new float[]{1.0F, 1.5F}),
					ObjectAnimator.OfFloat(view, "scaleY", new float[]{1.0F, 1.5F})});
		}
	}
}