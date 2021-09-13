using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Statistick.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для ParticalCorrelation.xaml
    /// </summary>
    public partial class ParticalCorrelation : Page
    {
        private int CurrentCollumn => grdDataGrid.ColumnDefinitions.Count - 1;
        private int CurrentRow => grdDataGrid.RowDefinitions.Count - 1;


        private event Action OnCollinearEnter;
        private event Action OnCollinearExit;

        private event Action OnUncollinearEnter;
        private event Action OnUncollinearExit;



        public ParticalCorrelation()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.SetStage("Частные корреляции");
            CreateParticalCorrTable();
        }


        void CreateParticalCorrTable()
        {
            grdDataGrid.ColumnDefinitions.Add(new ColumnDefinition());
            grdDataGrid.RowDefinitions.Add(new RowDefinition());



            List<double[]> Data = DictionaryToList(Dataset.Data);

            double[,] ParticleCorrelationMatrix = RegressionCalculator.GetParticleCorrelation(Data);


            foreach (KeyValuePair<string, List<double>> entry in Dataset.Data)
            {
                grdDataGrid.ColumnDefinitions.Add(new ColumnDefinition());
                grdDataGrid.RowDefinitions.Add(new RowDefinition());

                //Шапка
                grdDataGrid.Children.Add(CreateHead(entry.Key, 0, CurrentCollumn, Brushes.Transparent, 10, true));

                grdDataGrid.Children.Add(CreateHead(entry.Key, CurrentRow, 0, Brushes.Transparent, 10, true));

                List<double> data = Dataset.Normalize(entry.Value);
            }



            for (int i = 0; i < Data.Count; i++)
            {
                for (int j = 0; j < Data.Count; j++)
                {
                    Border brdAnalize = ShowCorrelation(ParticleCorrelationMatrix[i,j]);
                    Grid.SetColumn(brdAnalize, i + 1);
                    Grid.SetRow(brdAnalize, j + 1);
                    grdDataGrid.Children.Add(brdAnalize);
                }
            }
        }



        List<double[]> DictionaryToList(Dictionary<string, List<double>> dictionary)
        {
            List<double[]> Res = new List<double[]>();

            foreach (KeyValuePair<string, List<double>> entry in dictionary)
                Res.Add(Dataset.Normalize(entry.Value).ToArray());

            return Res;
        }

        Border ShowCorrelation(double value)
        {

            Border brdAnalize = new Border()
            {
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Cyan,
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(5)
            };


            Grid grdAnalizeContainer = new Grid();
            brdAnalize.Child = grdAnalizeContainer;


            //Отображение значения из выборки
            grdAnalizeContainer.RowDefinitions.Add(new RowDefinition());

            Border distributionInfo = new Border()
            {
                BorderThickness = new Thickness(3)
            };

            grdAnalizeContainer.Children.Add(distributionInfo);


            double Znach = Math.Round(Math.Abs(value * (Math.Sqrt(5) / Math.Sqrt(1 - value * value)) / 10), 5);//value == -1 ? double.NaN : Math.Round(0.05 * Math.Abs(value) * new Random((int)(value * 10000)).Next(1,10), 3);

            StackPanel sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Center
            };


            sp.Children.Add(CreatetextBlock($"{Math.Round(value, 3)}", 14, Brushes.White, true));
            sp.Children.Add(CreatetextBlock($"Уровень значимости: {Znach}", 12, Brushes.Magenta));

            distributionInfo.BorderBrush = Brushes.Black;

            if (Math.Abs(value) > 0.5)
            {
                OnCollinearEnter += () => distributionInfo.BorderBrush = Brushes.Lime;
                OnCollinearExit += () => distributionInfo.BorderBrush = Brushes.Black;
            }
            else
            {
                OnUncollinearEnter += () => distributionInfo.BorderBrush = Brushes.Yellow;
                OnUncollinearExit += () => distributionInfo.BorderBrush = Brushes.Black;
            }


            distributionInfo.Child = sp;

            return brdAnalize;
        }








        void CreateDualCorrTable()
        {
            grdDataGrid.ColumnDefinitions.Add(new ColumnDefinition());
            grdDataGrid.RowDefinitions.Add(new RowDefinition());



            List<List<double>> Data = DualDictionaryToList(Dataset.Data);


            foreach (KeyValuePair<string, List<double>> entry in Dataset.Data)
            {
                grdDataGrid.ColumnDefinitions.Add(new ColumnDefinition());
                grdDataGrid.RowDefinitions.Add(new RowDefinition());

                //Шапка
                grdDataGrid.Children.Add(CreateHead(entry.Key, 0, CurrentCollumn, Brushes.Transparent, 10, true));

                grdDataGrid.Children.Add(CreateHead(entry.Key, CurrentRow, 0, Brushes.Transparent, 10, true));

                List<double> data = Dataset.Normalize(entry.Value);
            }



            for (int i = 0; i < Data.Count; i++)
            {
                for (int j = 0; j < Data.Count; j++)
                {
                    Border brdAnalize = CalculateDualCorrelation(Data[i], Data[j]);
                    Grid.SetColumn(brdAnalize, i + 1);
                    Grid.SetRow(brdAnalize, j + 1);
                    grdDataGrid.Children.Add(brdAnalize);
                }
            }
        }


        List<List<double>> DualDictionaryToList(Dictionary<string, List<double>> dictionary)
        {
            List<List<double>> Res = new List<List<double>>();

            foreach (KeyValuePair<string, List<double>> entry in dictionary)
                Res.Add(Dataset.Normalize(entry.Value));

            return Res;
        }


        Border CalculateDualCorrelation(List<double> data1, List<double> data2)
        {

            Border brdAnalize = new Border()
            {
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Cyan,
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(5)
            };


            Grid grdAnalizeContainer = new Grid();
            brdAnalize.Child = grdAnalizeContainer;


            //Отображение значения из выборки
            grdAnalizeContainer.RowDefinitions.Add(new RowDefinition());

            Border distributionInfo = new Border()
            {
                BorderThickness = new Thickness(3)
            };

            grdAnalizeContainer.Children.Add(distributionInfo);


            //Линейная регрессия
            double a, b;
            RegressionCalculator.LinearLeastSquares(data1.ToArray(), data2.ToArray(), out a, out b);

            double Correlation = Math.Round(RegressionCalculator.GetDualCorrelation(data1.ToArray(), data2.ToArray()), 3);
            double Znach = Math.Round(Math.Abs(Correlation * (Math.Sqrt(5) / Math.Sqrt(1 - Correlation * Correlation)) / 10), 5);//data1 == data2 ? double.NaN : Math.Round(0.05 * Math.Abs(Correlation) * new Random((int)(Correlation * 10000)).Next(1, 10), 3);

            StackPanel sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Center
            };


            //sp.Children.Add(CreatetextBlock($"Корреляция:", 8, Brushes.Magenta));
            sp.Children.Add(CreatetextBlock($"{Correlation}", 14, Brushes.White, true));
            sp.Children.Add(CreatetextBlock($"Уровень значимости: {Znach}", 12, Brushes.Magenta));
            //sp.Children.Add(CreatetextBlock($"y = {Math.Round(a, 3)}x {(b > 0 ? "+" : "-")} {Math.Round(Math.Abs(b), 3)}", 8, Brushes.Magenta));

            distributionInfo.BorderBrush = Brushes.Black;

            if (Math.Abs(Correlation) > 0.5)
            {
                OnCollinearEnter += () => distributionInfo.BorderBrush = Brushes.Lime;
                OnCollinearExit += () => distributionInfo.BorderBrush = Brushes.Black;
            }
            else
            {
                OnUncollinearEnter += () => distributionInfo.BorderBrush = Brushes.Yellow;
                OnUncollinearExit += () => distributionInfo.BorderBrush = Brushes.Black;
            }


            distributionInfo.Child = sp;

            return brdAnalize;
        }





        





        void ClearBorder()
        {
            var brdStartValue = grdDataGrid.Children[0];

            grdDataGrid.Children.Clear();
            grdDataGrid.RowDefinitions.Clear();
            grdDataGrid.ColumnDefinitions.Clear();
            grdDataGrid.Children.Add(brdStartValue);

            OnCollinearEnter = null;
            OnCollinearExit = null;
            OnUncollinearEnter = null;
            OnUncollinearExit = null;
        }









        TextBlock CreatetextBlock(string text, int fontsize, Brush foreground, bool bold = false)
        {
            return new TextBlock()
            {
                AllowDrop = true,
                Text = text,
                FontWeight = bold ? FontWeights.Bold : FontWeights.Normal,
                FontSize = fontsize,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Foreground = foreground,
                TextWrapping = TextWrapping.Wrap
            };
        }





        Border CreateHead(string text, int row, int coll, Brush background = null, int size = 12, bool bold = false)
        {
            TextBlock newTextBlock = new TextBlock()
            {
                Text = text,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                FontSize = size,
                FontWeight = bold ? FontWeights.Bold : FontWeights.Normal,
                Foreground = Brushes.White
            };

            Thickness brdThickness = new Thickness(1);
            brdThickness.Left = coll == 0 ? 5 : 1;
            brdThickness.Top = row == 0 ? 5 : 1;


            Border brd = new Border()
            {
                BorderBrush = Brushes.Magenta,
                CornerRadius = new CornerRadius(8),
                BorderThickness = brdThickness,
                Background = background == null ? Brushes.Transparent : background
            };

            brd.Child = newTextBlock;


            Grid.SetRow(brd, row);
            Grid.SetColumn(brd, coll);

            return brd;
        }





        private void UncollinearEnter(object sender, MouseEventArgs e) => OnUncollinearEnter?.Invoke();

        private void UncollinearExit(object sender, MouseEventArgs e) => OnUncollinearExit?.Invoke();

        private void CollinearEnter(object sender, MouseEventArgs e) => OnCollinearEnter?.Invoke();

        private void CollinearExit(object sender, MouseEventArgs e) => OnCollinearExit?.Invoke();


        bool IsDual = true;

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearBorder();

            if (IsDual)
            {
                MainWindow.SetStage("Парные корреляции");
                CreateDualCorrTable();
            }
            else
            {
                MainWindow.SetStage("Частные корреляции");
                CreateParticalCorrTable();
            }

            IsDual = !IsDual;
        }
    }
}