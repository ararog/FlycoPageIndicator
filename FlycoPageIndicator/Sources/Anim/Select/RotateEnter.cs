using Android.Views;
using Com.Flyco.Pageindicator.Anim.Base;
using NineOldAndroids.Animation;

namespace Com.Flyco.PageIndicator.Anim.Select {

	public class RotateEnter : IndicatorBaseAnimator {
	    public RotateEnter() {
	        this.duration = 250;
	    }

	    public override void SetAnimation(View view) {
	        this.animatorSet.PlayTogether(new Animator[]{
	                ObjectAnimator.OfFloat(view, "rotation", 0, 180)});
	    }
	}
}