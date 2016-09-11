using Android.Views;
using Android.Views.Animations;
using NineOldAndroids.Animation;
using NineOldAndroids.View;

namespace Com.Flyco.Pageindicator.Anim.Base {

	public abstract class IndicatorBaseAnimator {

	    protected long duration = 200;
	    protected AnimatorSet animatorSet = new AnimatorSet();
	    private IInterpolator interpolator;
	    private long delay;
	    private AnimatorListener listener;

	    public abstract void SetAnimation(View view);

	    protected void Start(View view) {
	        Reset(view);
	        SetAnimation(view);

	        animatorSet.SetDuration(duration);
	        if (interpolator != null) {
	            animatorSet.SetInterpolator(interpolator);
	        }

	        if (delay > 0) {
	            animatorSet.StartDelay = delay;
	        }

	        if (listener != null) {
	        	animatorSet.AnimationStart += (o, e) => {
	        		listener.onAnimationStart(e.Animation);
	        	};

	        	animatorSet.AnimationEnd += (o, e) => {
	        		listener.onAnimationEnd(e.Animation);
	        	};

	        	animatorSet.AnimationRepeat += (o, e) => {
	        		listener.onAnimationRepeat(e.Animation);
	        	};

	        	animatorSet.AnimationCancel += (o, e) => {
	        		listener.onAnimationCancel(e.Animation);
	        	};
	        }

	        animatorSet.SetTarget(view);
	        animatorSet.Start();
	    }

	    public static void Reset(View view) {
	        ViewHelper.SetAlpha(view, 1);
	        ViewHelper.SetScaleX(view, 1);
	        ViewHelper.SetScaleY(view, 1);
	        ViewHelper.SetTranslationX(view, 0);
	        ViewHelper.SetTranslationY(view, 0);
	        ViewHelper.SetRotation(view, 0);
	        ViewHelper.SetRotationY(view, 0);
	        ViewHelper.SetRotationX(view, 0);
	    }

	    public IndicatorBaseAnimator Duration(long duration) {
	        this.duration = duration;
	        return this;
	    }

	    public IndicatorBaseAnimator Delay(long delay) {
	        this.delay = delay;
	        return this;
	    }

	    public IndicatorBaseAnimator Interpolator(IInterpolator interpolator) {
	        this.interpolator = interpolator;
	        return this;
	    }

	    public IndicatorBaseAnimator Listener(AnimatorListener listener) {
	        this.listener = listener;
	        return this;
	    }

	    public void PlayOn(View view) {
	        Start(view);
	    }

	    public interface AnimatorListener {
	        void onAnimationStart(Animator animator);

	        void onAnimationRepeat(Animator animator);

	        void onAnimationEnd(Animator animator);

	        void onAnimationCancel(Animator animator);
	    }
	}
}