using ReactiveUI;
using System;

namespace Full.ViewModels
{
    public class TimeStampViewModel : ViewModelBase
    {
        object? _d;

        object? _time;

        object? _s;

        object? _sigma;

        public string Name { get; set; }

        public int Number { get; set; }

        //public DateTime Date
        //{
        //    get
        //    {
        //        return _date;
        //    }
        //    set
        //    {
        //        if (value == _date)
        //            return;
        //        _date = value;
        //        this.RaisePropertyChanged(nameof(Date));
        //    }
        //}

        //public int SelectedIndex { get; set; }

        //public Dictionary<int, double?> Items => new Dictionary<int, double?>() { { 0, 0.0001 }, { 1, 0.001 }, { 2, 0.003 },
        //{ 3, 0.03 }, { 4, 0.2 }, { 5, 0.3 },{ 6, 0.01 },{ 7, 0.1 }};

        //public object? T { get; set; }

        //public object? Time { get; set; }

        //public DateTime DateEnd { get; set; }

        //public object? N { get; set; }

        //public object? n { get; set; }

        public object? D
        {
            get
            {
                try
                {
                    if (_d != null && _d != "")
                        return Math.Round(Convert.ToDouble(_d), 2);
                    return "";
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                if (value == _d)
                    return;
                _d = value;
                this.RaisePropertyChanged(nameof(D));
            }
        }

        public object? S
        {
            get
            {
                try
                {
                    if (_s != null && _s != "")
                        return Math.Round(Convert.ToDouble(_s), 2);
                    return "";
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                if (value == _s)
                    return;
                _s = value;
                this.RaisePropertyChanged(nameof(S));
            }
        }

        public object? Sigma
        {
            get
            {
                try
                {
                    if (_sigma != null && _sigma != "")
                        return Math.Round(Convert.ToDouble(_sigma), 2);
                    return "";
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                if (value == _sigma)
                    return;
                _sigma = value;
                this.RaisePropertyChanged(nameof(Sigma));
            }
        }

        public object? Time
        {
            get
            {
                try
                {
                    if (_time != null && _time != "")
                        return Math.Round(Convert.ToDouble(_time), 2);
                    return "";
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                if (value == _time)
                    return;
                _time = value;
                this.RaisePropertyChanged(nameof(Time));
            }
        }

        //public object? k { get; set; }

        public TimeStampViewModel(string name, int number)
        {
            Name = name;
            Number = number;
            //Lyambda = 0.0001;
            //SelectedIndex = 0;
        }

        public void Calculation()
        {
            //try
            //{
            //    Lyambda = Math.Round(1 / Convert.ToDouble(T), 2);
            //    this.RaisePropertyChanged(nameof(Lyambda));
            //}
            //catch
            //{

            //}
        }


        //public void AddData(ExcelRange cells)
        //{
        //    Lyambda = Convert.ToDouble(cells["B" + (Number + 2).ToString()].Value.ToString().Replace(".", ","));
        //    if (Items.Any(o => o.Value == Convert.ToDouble(Lyambda)))
        //        SelectedIndex = Items.Where(o => o.Value == Convert.ToDouble(Lyambda)).First().Key;
        //    this.RaisePropertyChanged(nameof(SelectedIndex));
        //    N = Convert.ToDouble(cells["C" + (Number + 2).ToString()].Value.ToString().Replace(".", ","));
        //    T = Convert.ToDouble(cells["D" + (Number + 2).ToString()].Value.ToString().Replace(".", ","));
        //    k = Convert.ToDouble(cells["E" + (Number + 2).ToString()].Value.ToString().Replace(".", ","));
        //    this.RaisePropertyChanged(nameof(Lyambda));
        //    this.RaisePropertyChanged(nameof(N));
        //    this.RaisePropertyChanged(nameof(T));
        //    this.RaisePropertyChanged(nameof(k));
        //}

        //public void Calculation()
        //{
        //    n = Convert.ToDouble(Lyambda) * Convert.ToDouble(N) * Convert.ToDouble(T);
        //    this.RaisePropertyChanged(nameof(n));
        //    P = 1 - Convert.ToDouble(n) / Convert.ToDouble(N);
        //    this.RaisePropertyChanged(nameof(P));
        //}
    }
}
