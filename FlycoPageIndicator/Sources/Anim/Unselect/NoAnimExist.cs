using Android.Views;
using Com.Flyco.Pageindicator.Anim.Base;
using NineOldAndroids.Animation;

namespace Com.Flyco.PageIndicator.Anim.Unselect
{
	public class NoAnimExist : IndicatorBaseAnimator {
	    public NoAnimExist() {
	        this.duration = 200;
	    }

		public override void SetAnimation(View view) {
	        this.animatorSet.PlayTogether(new Animator[]{
	                ObjectAnimator.OfFloat(view, "alpha", 1, 1)});
	    }
	}
}