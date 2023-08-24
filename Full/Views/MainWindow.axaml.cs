using Avalonia.Controls;
using Avalonia.Interactivity;
using Full.ViewModels;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Drawing;
using Avalonia.Input;
using ScottPlot.Avalonia;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System.Collections.Generic;
using SukiUI.Controls;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using ScottPlot.Statistics.Interpolation;
using DynamicData;
using Aspose.Words;

namespace Full.Views
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel MainWindowViewModel { get; set; } /*=> DataContext as MainWindowViewModel;*/

        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel = new MainWindowViewModel(this);
            var goeloCalculation = this.FindControl<Button>("goeloCalculation");
            goeloCalculation.DataContext = MainWindowViewModel;
            var millsCalculation = this.FindControl<Button>("millsCalculation");
            millsCalculation.DataContext = MainWindowViewModel;
            var M = this.FindControl<TextBox>("M");
            M.DataContext = MainWindowViewModel;
            var B = this.FindControl<TextBox>("B");
            B.DataContext = MainWindowViewModel;
            var T = this.FindControl<TextBox>("T");
            T.DataContext = MainWindowViewModel;
            var N = this.FindControl<TextBox>("N");
            N.DataContext = MainWindowViewModel;
            var Nex = this.FindControl<TextBox>("Nex");
            Nex.DataContext = MainWindowViewModel;
            var P = this.FindControl<TextBox>("P");
            P.DataContext = MainWindowViewModel;
            var periods = this.FindControl<TreeView>("periods");
            periods.DataContext = MainWindowViewModel;
            var S = this.FindControl<TextBox>("S");
            S.DataContext = MainWindowViewModel;
            var V = this.FindControl<TextBox>("V");
            V.DataContext = MainWindowViewModel;
            var K = this.FindControl<TextBox>("K");
            K.DataContext = MainWindowViewModel;
            var Pg = this.FindControl<TextBox>("Pg");
            Pg.DataContext = MainWindowViewModel;
            var lr1 = this.FindControl<Grid>("Lr1");
            lr1.DataContext = MainWindowViewModel.Lr1ViewModel;
            var lr2 = this.FindControl<Grid>("Lr2");
            lr2.DataContext = MainWindowViewModel.Lr2ViewModel;
            var lr3 = this.FindControl<Grid>("Lr3");
            lr3.DataContext = MainWindowViewModel.Lr3ViewModel;
            var comboBox = this.FindControl<ComboBox>("comboBox");
            comboBox.SelectedIndex = 0;
            var tree = this.FindControl<TreeView>("tree");
            tree.DataContext = MainWindowViewModel.Lr1ViewModel;
            var mills = this.FindControl<StackPanel>("mills");
            mills.DataContext = MainWindowViewModel;
            var okumoto = this.FindControl<StackPanel>("okumoto");
            okumoto.DataContext = MainWindowViewModel;
            //var side = this.FindControl<SideMenu>("side");
            //side.DataContext = MainWindowViewModel;
            //side.DataContext = MainWindowViewModel;
            //MainWindowViewModel = new MainWindowViewModel();
            //DataContext = MainWindowViewModel;
            //var comboBox = this.FindControl<ComboBox>("comboBox");
            //comboBox.DataContext = mainWindow;
        }

        //public void SelectionChanged(object sender, SelectionChangedEventArgs args)
        //{
        //    if ((sender as ComboBox).DataContext != null)
        //        ((sender as ComboBox).DataContext as TypeOperationViewModel).Lyambda = Convert.ToDouble(((args.AddedItems[0] as ComboBoxItem).Content as TextBlock).Text);
        //    //MainWindowViewModel.
        //    //var json = JsonConvert.SerializeObject(ViewModel.DtConfiguration);
        //    //File.WriteAllText("Models\\DtConfiguration.json", json);
        //}

        public void AddImage(string imageStr)
        {
            var image1 = this.FindControl<Avalonia.Controls.Image>(imageStr);
            var imageDock = this.FindControl<DockPanel>("imageDock");
            var image = new Avalonia.Controls.Image();
            image.Source = image1.Source;
            image.Width = image1.Width;
            imageDock.Children.Add(image);
        }

        public void Date_Clicked(object sender, RoutedEventArgs args)
        {
            var dialog = new DatePicker();
            dialog.ShowDialog(this);
            dialog.DataContext = (sender as TextBox).DataContext;
        }

        public async void Clicked(object sender, RoutedEventArgs args)
        {
            try
            {
                AvaPlot avaPlot1 = this.Find<AvaPlot>("avaPlot1");
                avaPlot1.Plot.Clear();
                var xList = new List<double>();
                var yList = new List<double>();
                foreach (var period in MainWindowViewModel.Periods)
                {
                    xList.Add(Convert.ToDouble(period.T));
                    yList.Add(Convert.ToDouble(period.Lyambda));
                }
                avaPlot1.Plot.AddScatter(xList.ToArray(), yList.ToArray());
                avaPlot1.Refresh();
            }
            catch
            {

            }
        }

        public void SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if ((sender as ComboBox).DataContext != null)
                ((sender as ComboBox).DataContext as TypeOperationViewModel).Lyambda = Convert.ToDouble(((args.AddedItems[0] as ComboBoxItem).Content as TextBlock).Text);
            //MainWindowViewModel.
            //var json = JsonConvert.SerializeObject(ViewModel.DtConfiguration);
            //File.WriteAllText("Models\\DtConfiguration.json", json);
        }

        //public async void Download_Clicked(object sender, RoutedEventArgs args)
        //{
        //    var fileDialog = new OpenFileDialog();
        //    fileDialog.Filters.Add(new FileDialogFilter() { Name = "Excel", Extensions = { "xlsx" } });
        //    var result = await fileDialog.ShowAsync(this);
        //    if (result != null)
        //    {
        //        (DataContext as MainWindowViewModel).Lr1ViewModel.Download(result.FirstOrDefault());
        //    }
        //}

        public async void Export_Clicked(object sender, RoutedEventArgs args)
        {
            try
            {
                var fileDialog = new OpenFileDialog();
                fileDialog.Filters.Add(new FileDialogFilter() { Name = "Excel", Extensions = { "xlsx" } });
                var result = await fileDialog.ShowAsync(this);
                if (result != null)
                {
                    var newFile = new FileInfo(result.First());
                    var Excel_Package = new ExcelPackage(newFile);
                    var workSheet = Excel_Package.Workbook.Worksheets.First();
                    var cells = workSheet.Cells;
                    for (int i = 3; i < Convert.ToInt32(MainWindowViewModel.Lr1ViewModel.R) + 3; i++)
                    {
                        cells["I" + i.ToString()].Value = MainWindowViewModel.Lr1ViewModel.TypeOperations[i - 3].n;
                        cells["J" + i.ToString()].Value = MainWindowViewModel.Lr1ViewModel.TypeOperations[i - 3].P;

                    }
                    cells["K3"].Value = MainWindowViewModel.Lr1ViewModel.Pop;
                    cells["L3"].Value = MainWindowViewModel.Lr1ViewModel.Pisp;
                    cells["M3"].Value = MainWindowViewModel.Lr1ViewModel.Pd;
                    Excel_Package.Save();
                    var doc = new Document(result.First());
                    doc.Save(result.First().Replace(".xlsx", "") + ".pdf");
                }
            }
            catch
            {

            }
        }

        public void Add_Clicked(object sender, RoutedEventArgs args)
        {
            try
            {
                var index = this.FindControl<ComboBox>("comboBox").SelectedIndex + 1;
                AddImage("image" + index.ToString());
                MainWindowViewModel.Lr2ViewModel.AddElements(index);
                var treeLr2 = this.FindControl<TreeView>("treeLr2");
                treeLr2.Items = MainWindowViewModel.Lr2ViewModel.Elements;
                //var fileDialog = new OpenFileDialog();
                //fileDialog.Filters.Add(new FileDialogFilter() { Name = "Excel", Extensions = { "xlsx" } });
                //var result = await fileDialog.ShowAsync(this);
                //if (result != null)
                //{
                //    var newFile = new FileInfo(result.First());
                //    var Excel_Package = new ExcelPackage(newFile);
                //    var workSheet = Excel_Package.Workbook.Worksheets.First();
                //    var cells = workSheet.Cells;
                //    for (int i = 3; i < Convert.ToInt32(MainWindowViewModel.R) + 3; i++)
                //    {
                //        cells["I" + i.ToString()].Value = MainWindowViewModel.TypeOperations[i - 3].n;
                //        cells["J" + i.ToString()].Value = MainWindowViewModel.TypeOperations[i - 3].P;

                //    }
                //    cells["K3"].Value = MainWindowViewModel.Pop;
                //    cells["L3"].Value = MainWindowViewModel.Pisp;
                //    cells["M3"].Value = MainWindowViewModel.Pd;
                //    Excel_Package.Save();
                //}
            }
            catch
            {

            }
        }

        public async void Clicked_plot(object sender, RoutedEventArgs args)
        {
            try
            {
                AvaPlot avaPlot1 = this.Find<AvaPlot>("AvaPlot1");
                avaPlot1.Plot.Clear();
                double F = 0.828944;
                double Y = Convert.ToDouble(MainWindowViewModel.Lr3ViewModel.Y);
                double S = 1;
                double tCurr = 0;
                double sum = 0;
                var xList = new List<double>();
                var yList = new List<double>();
                var yListUp = new List<double>();
                var yListDown = new List<double>();
                //foreach (var row in dataGridView1.Rows)
                //{
                //    var d = Convert.ToInt32((row as DataGridViewRow).Cells[1].Value);
                //    Y -= d;
                //    S *= (Y - d) / Y;
                //}
                //Y = Convert.ToInt32(textBox2.Text);
                var sigma = S * Math.Sqrt(sum);
                foreach (var period in MainWindowViewModel.Lr3ViewModel.Periods)
                {
                    sigma = S * Math.Sqrt(sum);
                    period.S = S;
                    period.Sigma = sigma;
                    xList.Add(tCurr);
                    yList.Add(S);
                    yListUp.Add(S + sigma * F);
                    yListDown.Add(S - sigma * F);
                    //var newPoint = new DataPoint(tCurr, S);
                    //chart1.Series[0].Points.Add(newPoint);
                    //var newPointUp = new DataPoint(tCurr, S + sigma * F);
                    //var newPointDown = new DataPoint(tCurr, S - sigma * F);
                    //chart1.Series[1].Points.Add(newPointUp);
                    //chart1.Series[2].Points.Add(newPointDown);
                    Y -= Convert.ToDouble(period.D);
                    sum += Convert.ToDouble(period.D) / (Y * (Y - Convert.ToDouble(period.D)));
                    S *= (Y - Convert.ToDouble(period.D)) / Y;
                    tCurr += Convert.ToDouble(period.Time);
                }
                xList.Add(tCurr);
                yList.Add(S);
                yListUp.Add(S + sigma * F);
                yListDown.Add(S - sigma * F);
                avaPlot1.Plot.AddScatterStep(xList.ToArray(), yList.ToArray());
                avaPlot1.Plot.AddScatterStep(xList.ToArray(), yListUp.ToArray());
                avaPlot1.Plot.AddScatterStep(xList.ToArray(), yListDown.ToArray());
                avaPlot1.Refresh();
            }
            catch
            {
            }
        }

        public async void Years_Clicked(object sender, RoutedEventArgs args)
        {
            try
            {
                var yearText = this.FindControl<TextBox>("yearText");
                var years = Convert.ToInt32(yearText.Text);
                var current = DateTime.Now.Year;
                var xList = new List<double>();
                var yList = new List<double>();
                for (int i = 0; i < years; i++)
                {
                    xList.Add(current + i);
                    yList.Add(MainWindowViewModel.Lr2ViewModel.Elements.Where(o => o.DateEnd.Year > current + i).ToList().Count);
                }
                AvaPlot avaPlot2 = this.Find<AvaPlot>("AvaPlot2");
                avaPlot2.Plot.Clear();
                //avaPlot2.Plot.AddScatterStep(xList.ToArray(), yList.ToArray());
                //avaPlot2.Plot.AddScatterStep(xList.ToArray(), yListUp.ToArray());
                avaPlot2.Plot.AddScatterStep(xList.ToArray(), yList.ToArray());
                avaPlot2.Refresh();
            }
            catch
            {

            }
        }
    }

}
