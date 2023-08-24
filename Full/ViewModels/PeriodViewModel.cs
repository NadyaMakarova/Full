using ReactiveUI;

namespace Full.ViewModels
{
    public class PeriodViewModel : ViewModelBase
    {
        object? _lyambda;

        object? _t;

        public object? T
        {
            get
            {
                return _t;
            }
            set
            {
                if (value == _t)
                    return;
                _t = value;
                this.RaisePropertyChanged(nameof(T));
            }
        }

        public object? Lyambda
        {
            get
            {
                return _lyambda;
            }
            set
            {
                if (value == _lyambda)
                    return;
                _lyambda = value;
                this.RaisePropertyChanged(nameof(Lyambda));
            }
        }

        public PeriodViewModel(object? t)
        {
            T = t;
            this.RaisePropertyChanged(nameof(T));
        }
    }
}
