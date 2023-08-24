using Aspose.Words;
using Npgsql;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Full.ViewModels
{
    public class Lr3ViewModel : ViewModelBase
    {
        object? _n;
        public string Greeting => "Welcome to Avalonia!";

        public List<TimeStampViewModel> Periods { get; set; }

        public ICommand CalculationCommand { get; set; }

        public ICommand SaveDatabaseCommand { get; set; }

        public ICommand ExportCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        //public object? Y { get; set; }

        public object? Y { get; set; }

        //public object? Pob { get; set; }

        //public object? Pi { get; set; }

        //public double? Pop { get; set; }

        //public double? Pisp { get; set; }

        //public double? Pd { get; set; }

        //object? _r;

        public void SaveDatabase()
        {
            try
            {
                string conn_param = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=20056865;Database=postgres;"; // "Server=127.0.0.1;Port=5432;User Id=postgres;Password=goodforyouatmonth1973;Database=postgres;"
                NpgsqlConnection conn = new NpgsqlConnection(conn_param);
                conn.Open();
                foreach (var period in Periods)
                {
                    string sql = "insert into public.\"Module3\" (number,d,t,\"S\",sigma) values(" +
                   period.Number.ToString().Replace(",", ".") + "," + period.D.ToString().Replace(",", ".") + "," +
                         period.Time.ToString().Replace(",", ".") + "," + period.S.ToString().Replace(",", ".") + "," +
                        period.Sigma.ToString().Replace(",", ".") + ");";
                    NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
                    comm.ExecuteScalar();
                }
                //Открываем соединение.

                //result = comm.ExecuteScalar().ToString(); //Выполняем нашу команду.
                conn.Close();
            }
            catch
            {

            }
        }

        public void Save()
        {
            try
            {
                var str = "Данные 3 модуля:\n";
                foreach (var period in Periods)
                {
                    str += period.Number.ToString().Replace(",", ".") + ";" + period.D.ToString().Replace(",", ".") + ";" +
                         period.Time.ToString().Replace(",", ".") + ";" + period.S.ToString().Replace(",", ".") + ";" +
                        period.Sigma.ToString().Replace(",", ".") + "\n";

                }
                File.WriteAllText("lr3.txt", str);
                var doc = new Document("lr3.txt");
                doc.Save("lr3.txt".Replace(".xlsx", "") + ".pdf");
            }
            catch
            {
            }
        }

        public void Export()
        {
            try
            {
                var newPeriods = new List<TimeStampViewModel>();
                Periods.Clear();
                foreach (var item in File.ReadAllText("lr3.txt").Replace("Данные 3 модуля:\n", "").Split('\n'))
                {
                    var items = item.Split(';').ToList();
                    if (items.Count == 5)
                    {
                        newPeriods.Add(new TimeStampViewModel(items[0], Convert.ToInt32(items[0]))
                        {
                            D = items[1].Replace(".",","),
                            Time = items[2].Replace(".", ","),
                            S = items[3].Replace(".", ","),
                            Sigma = items[4].Replace(".", ",")
                        });

                    }
                }
                N = newPeriods.Count;
                Periods = new List<TimeStampViewModel>(newPeriods);
                this.RaisePropertyChanged(nameof(Periods));
            }
            catch
            {

            }
        }

        public object? N
        {
            get
            {
                return _n;
            }
            set
            {
                if (value == _n)
                    return;
                _n = value;
                try
                {
                    this.RaisePropertyChanged(nameof(N));
                    if (Convert.ToInt32(N) > 0)
                    {
                        Periods = new List<TimeStampViewModel>();
                        for (int i = 1; i <= Convert.ToInt32(N); i++)
                        {
                            Periods.Add(new TimeStampViewModel((i).ToString(), (i)));
                        }
                        this.RaisePropertyChanged(nameof(Periods));
                    }
                }
                catch
                {
                }
            }
        }



        public Lr3ViewModel()
        {
            try
            {
                Periods = new List<TimeStampViewModel>();
                CalculationCommand = ReactiveCommand.Create(Calculation);
                SaveDatabaseCommand = ReactiveCommand.Create(SaveDatabase);
                SaveCommand = ReactiveCommand.Create(Save);
                ExportCommand= ReactiveCommand.Create(Export);
            }
            catch
            {

            }
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

        public void Calculation()
        {
            try
            {

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
