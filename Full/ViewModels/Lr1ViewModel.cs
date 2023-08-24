using Avalonia.Controls;
using Npgsql;
using OfficeOpenXml;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Full.ViewModels
{
    public class Lr1ViewModel : ViewModelBase
    {
        public List<TypeOperationViewModel> TypeOperations { get; set; }

        public ICommand CalculationCommand { get; set; }

        public ICommand DownloadCommand { get; set; }

        public ICommand SaveDatabaseCommand{ get; set; }

        public object? Pk { get; set; }

        public object? Pob { get; set; }

        public object? Pi { get; set; }

        public double? Pop { get; set; }

        public double? Pisp { get; set; }

        public double? Pd { get; set; }

        object? _r;

        public object? R
        {
            get
            {
                return _r;
            }
            set
            {
                if (value == _r)
                    return;
                _r = value;
                try
                {
                    this.RaisePropertyChanged(nameof(R));
                    if (Convert.ToInt32(R) > 0)
                    {
                        TypeOperations = new List<TypeOperationViewModel>();
                        for (int i = 1; i <= Convert.ToInt32(R); i++)
                        {
                            TypeOperations.Add(new TypeOperationViewModel("Операция " + i.ToString(), i));
                        }
                        this.RaisePropertyChanged(nameof(TypeOperations));
                    }
                }
                catch
                {
                }
            }
        }

        public void SaveDatabase()
        {
            try
            {
                string conn_param = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=20056865;Database=postgres;";
                NpgsqlConnection conn = new NpgsqlConnection(conn_param);
                conn.Open();
                foreach (var type in TypeOperations)
                {
                    string sql = "insert into public.\"Module1\" (rj,aj,\"Nj\",\"Tj\",kj,\"Pk\",\"Pob\",\"Pi\",nj,\"Pj\",\"Pop\",\"Pisp\",\"Pd\") values(" +
                       type.Number.ToString().Replace(",", ".") + ',' + type.Lyambda.ToString().Replace(",", ".") + ',' + type.N.ToString().Replace(",", ".") + ',' + type.T.ToString().Replace(",", ".") +
                       ',' + type.k.ToString().Replace(",", ".") + ',' + Pk.ToString().Replace(",", ".") + ',' + Pob.ToString().Replace(",", ".") + ',' + Pi.ToString().Replace(",", ".") +
                       ',' + type.n.ToString().Replace(",", ".") + ',' + type.P.ToString().Replace(",", ".") + ',' +Pop.ToString().Replace(",", ".") + ',' + Pisp.ToString().Replace(",", ".") + ',' + Pd.ToString().Replace(",", ".") + ");";
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

        public MainWindowViewModel MainWindowViewModel { get; set; }

        public Lr1ViewModel(MainWindowViewModel main)
        {
            MainWindowViewModel = main;
            TypeOperations = new List<TypeOperationViewModel>();
            CalculationCommand = ReactiveCommand.Create(Calculation);
            DownloadCommand = ReactiveCommand.Create(Download);
            SaveDatabaseCommand = ReactiveCommand.Create(SaveDatabase);
        }

        public async void Download()
        {
            try
            {
                var fileDialog = new OpenFileDialog();
                fileDialog.Filters.Add(new FileDialogFilter() { Name = "Excel", Extensions = { "xlsx" } });
                var result = await fileDialog.ShowAsync(MainWindowViewModel.Window);
                if (result != null)
                {
                    var newFile = new FileInfo(result.FirstOrDefault());
                    var Excel_Package = new ExcelPackage(newFile);
                    var workSheet = Excel_Package.Workbook.Worksheets[0];
                    var cells = workSheet.Cells;
                    foreach (var type in TypeOperations)
                    {
                        type.AddData(cells);
                    }
                    Pk = Convert.ToDouble(cells["F3"].Value.ToString().Replace(".", ","));
                    this.RaisePropertyChanged(nameof(Pk));
                    Pob = Convert.ToDouble(cells["G3"].Value.ToString().Replace(".", ","));
                    this.RaisePropertyChanged(nameof(Pob));
                    Pi = Convert.ToDouble(cells["H3"].Value.ToString().Replace(".", ","));
                    this.RaisePropertyChanged(nameof(Pi));
                    this.RaisePropertyChanged(nameof(TypeOperations));
                }

            }
            catch
            {
            }
        }

        public void Calculation()
        {
            try
            {
                TypeOperations.ForEach(o => o.Calculation());
                Pop = 1;
                foreach (var oper in TypeOperations)
                {
                    Pop = Pop * Math.Pow(Convert.ToDouble(oper.P), Convert.ToDouble(oper.k));
                }
                Pisp = Convert.ToDouble(Pk) * Convert.ToDouble(Pob) * Convert.ToDouble(Pi);
                Pd = Pop + (1 - Pop) * Pisp;
                this.RaisePropertyChanged(nameof(Pop));
                this.RaisePropertyChanged(nameof(Pisp));
                this.RaisePropertyChanged(nameof(Pd));
            }
            catch
            {

            }
        }
    }
}
