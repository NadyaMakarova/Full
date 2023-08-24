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
    public class Lr2ViewModel : ViewModelBase
    {
        public List<ElementViewModel> Elements { get; set; }

        public List<(int, List<ElementViewModel>)> SystemElements { get; set; }

        public ICommand CalculationCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public object? Ptr { get; set; }

        public Lr2ViewModel()
        {
            try
            {
                Elements = new List<ElementViewModel>();
                SystemElements = new List<(int, List<ElementViewModel>)>();
                CalculationCommand = ReactiveCommand.Create(Calculation);
                SaveCommand = ReactiveCommand.Create(Save);
            }
            catch
            {

            }
        }

        public void Calculation()
        {
            try
            {
                foreach (var item in Elements)
                {
                    item.Calculation();
                }
                double count = Convert.ToDouble(Elements.Count);
                double coren = (1 / count);
                var p = Math.Pow(Convert.ToDouble(Convert.ToDouble(Ptr)), coren);
                foreach (var item in SystemElements)
                {
                    switch (item.Item1)
                    {
                        case 1:
                            {
                                item.Item2.First().P = p;
                                break;
                            }
                        case 2:
                            {
                                foreach (var it in item.Item2)
                                {
                                    it.P = (2 - Math.Sqrt(4 - 4 * p)) / 2;
                                }
                                break;
                            }
                        case 3:
                            {
                                item.Item2.First().P = (2 - Math.Sqrt(4 - 4 * p)) / 2;
                                item.Item2[1].P = Math.Sqrt((2 - Math.Sqrt(4 - 4 * p)) / 2);
                                item.Item2[2].P = Math.Sqrt((2 - Math.Sqrt(4 - 4 * p)) / 2);
                                break;
                            }
                        case 4:
                            {
                                foreach (var it in item.Item2)
                                {
                                    it.P = Math.Sqrt((2 - Math.Sqrt(4 - 4 * p)) / 2);
                                }
                                break;
                            }
                    }
                }
                var str = "";
                foreach (var it in SystemElements.SelectMany(o => o.Item2))
                {
                    var hours = -Math.Log(Convert.ToDouble(it.P)) / Convert.ToDouble(it.Lyambda) * 10000;
                    it.Time = hours;
                    var newDate = it.Date.AddHours(hours);
                    it.DateEnd = newDate;
                    str += "Элемент " + it.Name + " нужно заменить:" + it.DateEnd + "\n";
                }
                File.WriteAllText("lr2.txt", str);
                var doc = new Document("lr2.txt");
                doc.Save("lr2.txt".Replace(".xlsx", "") + ".pdf");
            }
            catch
            {

            }
        }



        public void Save()
        {
            try
            {
                string conn_param = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=20056865;Database=postgres;"; //Например: "Server=127.0.0.1;Port=5432;User Id=postgres;Password=goodforyouatmonth1973;Database=postgres;"
                NpgsqlConnection conn = new NpgsqlConnection(conn_param);
                conn.Open();
                foreach (var it in SystemElements.SelectMany(o => o.Item2))
                {
                    var date = it.Date.Year + "-" + it.Date.Month + "-" + it.Date.Day.ToString();
                    var dateEnd = it.DateEnd.Year + "-" + it.DateEnd.Month + "-" + it.DateEnd.Day.ToString();
                    string sql = "insert into public.\"Elements\" (name,date,\"T\",a,\"P\",t,\"dateEnd\") values('" + it.Name + "','" + date + "'," + it.T.ToString().Replace(",", ".") + "," + it.Lyambda.ToString().Replace(",", ".") + "," + it.P.ToString().Replace(",", ".") + "," + it.Time.ToString().Replace(",", ".") + ",'" + dateEnd + "'" + ");";
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

        public void AddElements(int systemIndex)
        {
            try
            {
                var newList = new List<ElementViewModel>();
                var elements = new List<ElementViewModel>(Elements);
                for (int i = 0; i < systemIndex; i++)
                {
                    var newElement = new ElementViewModel(systemIndex.ToString() + "." + (i + 1).ToString(), Elements.Count + 1);
                    newList.Add(newElement);
                    elements.Add(newElement);
                }
                SystemElements.Add((systemIndex, newList));
                Elements = new List<ElementViewModel>(elements);
                this.RaisePropertyChanged(nameof(Elements));
            }
            catch
            {

            }
        }
    }
}
