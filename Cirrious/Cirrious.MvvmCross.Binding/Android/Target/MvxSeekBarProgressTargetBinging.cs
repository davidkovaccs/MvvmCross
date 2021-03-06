using System.Reflection;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Bindings.Target;
using Cirrious.MvvmCross.Binding.Interfaces;
using Cirrious.MvvmCross.Interfaces.Platform.Diagnostics;

namespace Cirrious.MvvmCross.Binding.Android.Target
{
    public class MvxSeekBarProgressTargetBinging : MvxPropertyInfoTargetBinding<SeekBar>
    {
        public MvxSeekBarProgressTargetBinging(object target, PropertyInfo targetPropertyInfo)
            : base(target, targetPropertyInfo)
        {
            var seekBar = View;
            if (seekBar == null)
            {
                MvxBindingTrace.Trace(MvxTraceLevel.Error, "Error - SeekBar is null in MvxSeekBarProgressTargetBinging");
            }
            else
            {
                seekBar.SetOnSeekBarChangeListener(new SeekBarChangeListener(this));
            }
        }

        // this variable isn't used, but including this here prevents Mono from optimising the call out!
        private int JustForReflection
        {
            get { return View.Progress; }
            set { View.Progress = value; }
        }

        public class SeekBarChangeListener : 
            Java.Lang.Object
            , SeekBar.IOnSeekBarChangeListener
        {
            private readonly MvxSeekBarProgressTargetBinging _parent;

            public SeekBarChangeListener(MvxSeekBarProgressTargetBinging parent)
            {
                _parent = parent;
            }

            public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
            {
                if (fromUser)
                    _parent.FireValueChanged(progress);
            }

            public void OnStartTrackingTouch(SeekBar seekBar)
            {
                // ignore
            }

            public void OnStopTrackingTouch(SeekBar seekBar)
            {
                // ignore
            }
        }

        public override MvxBindingMode DefaultMode
        {
            get
            {
                return MvxBindingMode.TwoWay;
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (View != null)
                {
                    View.SetOnSeekBarChangeListener(null);
                }
            }
            base.Dispose(isDisposing);
        }
    }
}