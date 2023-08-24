using Aspose.Words;
using Avalonia.Controls;
using Npgsql;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace Full.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        //object? _n;

        object? _p;

        object? _pg;

        object? _m;

        object? _b;
        public string Greeting => "Welcome to Avalonia!";

        //public List<PeriodViewModel> Periods { get; set; }

        public ICommand MillsCommand { get; set; }

        public ICommand GoeloCommand { get; set; }

        //public object? Y { get; set; }

        //public object? Y { get; set; }

        //public object? Pob { get; set; }

        //public object? Pi { get; set; }

        //public double? Pop { get; set; }

        //public double? Pisp { get; set; }

        //public double? Pd { get; set; }

        //object? _r;

        //public object? N
        //{
        //    get
        //    {
        //        return _n;
        //    }
        //    set
        //    {
        //        if (value == _n)
        //            return;
        //        _n = value;
        //        try
        //        {
        //            this.RaisePropertyChanged(nameof(N));
        //            if (Convert.ToInt32(N) > 0)
        //            {
        //                Periods = new List<PeriodViewModel>();
        //                for (int i = 1; i <= Convert.ToInt32(N); i++)
        //                {
        //                    Periods.Add(new PeriodViewModel((i).ToString(), (i)));
        //                }
        //                this.RaisePropertyChanged(nameof(Periods));
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        public Window Window { get; set; }

        public Lr1ViewModel Lr1ViewModel { get; set; }

        public Lr2ViewModel Lr2ViewModel { get; set; }

        public Lr3ViewModel Lr3ViewModel { get; set; }

        public object? P
        {
            get
            {
                return _p;
            }
            set
            {
                if (value == _p)
                    return;
                _p = value;
                this.RaisePropertyChanged(nameof(P));
            }
        }

        public object? Pg
        {
            get
            {
                return _pg;
            }
            set
            {
                if (value == _pg)
                    return;
                _pg = value;
                this.RaisePropertyChanged(nameof(Pg));
            }
        }

        public object? B
        {
            get
            {
                return _b;
            }
            set
            {
                if (value == _b)
                    return;
                _b = value;
                this.RaisePropertyChanged(nameof(B));
            }
        }

        public object? M
        {
            get
            {
                return _m;
            }
            set
            {
                try
                {
                    if (value == _m)
                        return;
                    _m = value;
                    var newPeriods = new List<PeriodViewModel>();
                    for (var i = 1; i <= Convert.ToInt32(value); i++)
                    {
                        newPeriods.Add(new PeriodViewModel(i));
                    }
                    Periods = newPeriods;
                    this.RaisePropertyChanged(nameof(M));
                    this.RaisePropertyChanged(nameof(Periods));
                }
                catch
                {

                }
            }
        }

        public List<PeriodViewModel> Periods { get; set; }

        public object? K { get; set; }

        public ICommand SaveDataBaseMillsCommand { get; set; }

        public ICommand SaveDataBaseOkumotoCommand { get; set; }

        public ICommand ExportMillsCommand { get; set; }

        public ICommand ExportOkumotoCommand { get; set; }

        public ICommand SaveMillsCommand { get; set; }

        public ICommand SaveOkumotoCommand { get; set; }

        public object? V { get; set; }

        public object? S { get; set; }

        public object? T { get; set; }

        public object? N { get; set; }

        public object? Nex { get; set; }

        public MainWindowViewModel(Window window)
        {
            try
            {
                Window = window;
                //Periods = new List<PeriodViewModel>();
                Periods = new List<PeriodViewModel>();
                MillsCommand = ReactiveCommand.Create(MillsCalculation);
                GoeloCommand = ReactiveCommand.Create(GoeloCalculation);
                SaveDataBaseMillsCommand = ReactiveCommand.Create(SaveDatabaseMills);
                SaveDataBaseOkumotoCommand = ReactiveCommand.Create(SaveDatabaseOkumoto);
                ExportMillsCommand = ReactiveCommand.Create(ExportMills);
                ExportOkumotoCommand = ReactiveCommand.Create(ExportOkumoto);
                SaveOkumotoCommand = ReactiveCommand.Create(SaveOkumoto);
                SaveMillsCommand = ReactiveCommand.Create(SaveMills);
                Lr1ViewModel = new Lr1ViewModel(this);
                Lr2ViewModel = new Lr2ViewModel();
                Lr3ViewModel = new Lr3ViewModel();
            }
            catch
            {

            }
        }

        public void SaveMills()
        {
            try
            {
                var str = "Метод Миллса:\n" + S.ToString().Replace(",", ".") + "\n" + V.ToString().Replace(",", ".") + "\n" + K.ToString().Replace(",", ".") + "\n" + P.ToString().Replace(",", ".");
                File.WriteAllText("mills.txt", str);
                var doc = new Document("mills.txt");
                doc.Save("mills.txt".Replace(".xlsx", "") + ".pdf");
            }
            catch { }

        }

        public void SaveOkumoto()
        {
            try
            {
                var str = "Метод Гоело-Окумото:\n" + M.ToString().Replace(",", ".") + "\n" + T.ToString().Replace(",", ".") + "\n" + N.ToString().Replace(",", ".") + "\n" + Nex.ToString().Replace(",", ".") + "\n" + B.ToString().Replace(",", ".") + "\n" + Pg.ToString().Replace(",", ".");
                File.WriteAllText("okumoto.txt", str);
                var doc = new Document("okumoto.txt");
                doc.Save("okumoto.txt".Replace(".xlsx", "") + ".pdf");
            }
            catch
            {

            }

        }

        public void ExportMills()
        {
            try
            {
                var items = File.ReadAllText("mills.txt").Replace("Метод Миллса:\n", "").Split('\n');
                S = items[0].Replace(".", ",");
                this.RaisePropertyChanged(nameof(S));
                V = items[1].Replace(".", ",");
                this.RaisePropertyChanged(nameof(V));
                K = items[2].Replace(".", ",");
                this.RaisePropertyChanged(nameof(K));
                P = items[3].Replace(".", ",");
            }
            catch
            {

            }

        }

        public void ExportOkumoto()
        {
            try
            {
                var items = File.ReadAllText("okumoto.txt").Replace("Метод Гоело-Окумото:\n", "").Split('\n');
                M = items[0].Replace(".", ",");
                this.RaisePropertyChanged(nameof(M));
                T = items[1].Replace(".", ",");
                this.RaisePropertyChanged(nameof(T));
                N = items[2].Replace(".", ",");
                this.RaisePropertyChanged(nameof(N));
                Nex = items[3].Replace(".", ",");
                this.RaisePropertyChanged(nameof(Nex));
                B = items[4].Replace(".", ",");
                this.RaisePropertyChanged(nameof(B));
                Pg = items[5].Replace(".", ",");
                this.RaisePropertyChanged(nameof(Pg));
            }
            catch
            {

            }

        }

        public void SaveDatabaseMills()
        {
            try
            {
                string conn_param = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=20056865;Database=postgres;"; // "Server=127.0.0.1;Port=5432;User Id=postgres;Password=goodforyouatmonth1973;Database=postgres;"
                NpgsqlConnection conn = new NpgsqlConnection(conn_param);
                conn.Open();
                string sql = "insert into public.\"Mills\" (s,v,k,p) values(" +
                S.ToString().Replace(",", ".") + "," + V.ToString().Replace(",", ".") + "," +
                K.ToString().Replace(",", ".") + "," + P.ToString().Replace(",", ".") + ");";
                NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
                comm.ExecuteScalar();
                //Открываем соединение.

                //result = comm.ExecuteScalar().ToString(); //Выполняем нашу команду.
                conn.Close();
            }
            catch
            {

            }
        }

        public void SaveDatabaseOkumoto()
        {
            try
            {
                string conn_param = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=20056865;Database=postgres;"; // "Server=127.0.0.1;Port=5432;User Id=postgres;Password=goodforyouatmonth1973;Database=postgres;"
                NpgsqlConnection conn = new NpgsqlConnection(conn_param);
                conn.Open();
                string sql = "insert into public.\"Okumoto\" (m,t,n,nex,b,pg) values(" +
                M.ToString().Replace(",", ".") + "," + T.ToString().Replace(",", ".") + "," +
                N.ToString().Replace(",", ".") + "," + Nex.ToString().Replace(",", ".") + "," + B.ToString().Replace(",", ".") + "," + Pg.ToString().Replace(",", ".") + ");";
                NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
                comm.ExecuteScalar();
                //Открываем соединение.

                //result = comm.ExecuteScalar().ToString(); //Выполняем нашу команду.
                conn.Close();
            }
            catch
            {

            }
        }

        public static double Fact(double n)
        {
            double p;
            p = 1;
            for (int i = 1; i <= n; i++)
            {
                p = p * i;
            }
            return p;
        }

        public void MillsCalculation()
        {
            try
            {
                P = Convert.ToString(Math.Round(C(Convert.ToDouble(S), Convert.ToDouble(V), Convert.ToDouble(K)), 3));
            }
            catch
            {

            }
        }

        public static double C(double s, double v, double k)
        {
            return (Fact(s) * Fact(v + k)) / (Fact(v - 1) * Fact(s + k + 1));
        }

        //public void AddElements(int index)
        //{
        //    try
        //    {
        //        var newList = new List<PeriodViewModel>();
        //        var periods = new List<PeriodViewModel>();
        //        for (int i = 0; i < index; i++)
        //        {
        //            var newElement = new PeriodViewModel((i + 1).ToString(), (i + 1));
        //            periods.Add(newElement);
        //        }
        //        Periods = new List<PeriodViewModel>(periods);
        //        this.RaisePropertyChanged(nameof(Periods));
        //    }
        //    catch
        //    {

        //    }
        //}

        //public void Download(string fileName)
        //{
        //    try
        //    {
        //        var newFile = new FileInfo(fileName);
        //        var Excel_Package = new ExcelPackage(newFile);
        //        var workSheet = Excel_Package.Workbook.Worksheets[0];
        //        var cells = workSheet.Cells;
        //        //foreach (var type in TypeOperations)
        //        //{
        //        //    type.AddData(cells);
        //        //}
        //        Pk = Convert.ToDouble(cells["F3"].Value.ToString().Replace(".", ","));
        //        this.RaisePropertyChanged(nameof(Pk));
        //        Pob = Convert.ToDouble(cells["G3"].Value.ToString().Replace(".", ","));
        //        this.RaisePropertyChanged(nameof(Pob));
        //        Pi = Convert.ToDouble(cells["H3"].Value.ToString().Replace(".", ","));
        //        this.RaisePropertyChanged(nameof(Pi));
        //        this.RaisePropertyChanged(nameof(TypeOperations));

        //    }
        //    catch
        //    {
        //    }
        //}

        public void GoeloCalculation()
        {
            try
            {
                B = Math.Round(-(-Math.Log(Convert.ToDouble(M) / Convert.ToDouble(N) + 1)) / Convert.ToDouble(T), 3);
                Pg = Math.Round((1 - Convert.ToDouble(Nex) / Convert.ToDouble(N)), 3);
                foreach (var el in Periods)
                {
                    el.Lyambda = Math.Round((Convert.ToDouble(N) * Convert.ToDouble(B) * Math.Pow(Math.E, -Convert.ToDouble(B) * Convert.ToDouble(el.T))), 3);
                }
            }
            catch
            {

            }
        }

        //public void Calculation()
        //{
        //    try
        //    {
        //        TypeOperations.ForEach(o => o.Calculation());
        //        Pop = 1;
        //        foreach (var oper in TypeOperations)
        //        {
        //            Pop = Pop * Math.Pow(Convert.ToDouble(oper.P), Convert.ToDouble(oper.k));
        //        }
        //        Pisp = Convert.ToDouble(Pk) * Convert.ToDouble(Pob) * Convert.ToDouble(Pi);
        //        Pd = Pop + (1 - Pop) * Pisp;
        //        this.RaisePropertyChanged(nameof(Pop));
        //        this.RaisePropertyChanged(nameof(Pisp));
        //        this.RaisePropertyChanged(nameof(Pd));
        //    }
        //    catch
        //    {

        //    }
        //}

    }
}
