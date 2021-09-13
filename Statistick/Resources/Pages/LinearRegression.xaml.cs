using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;



namespace Statistick.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для LinearRegression.xaml
    /// </summary>
    public partial class DispersionVisualization : Page
    {
        public DispersionVisualization()
        {
            InitializeComponent();
            MainWindow.SetStage("Визуализация дисперсии");
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTable();
        }


        void CreateTable()
        {
            foreach (KeyValuePair<string, List<double>> entry in Dataset.Data)
            {
                grdDataGrid.ColumnDefinitions.Add(new ColumnDefinition());
                int currentRow = grdDataGrid.ColumnDefinitions.Count - 1;

                //Шапка
                grdDataGrid.Children.Add(CreateTextBlock(entry.Key, 0, currentRow, Brushes.Transparent, 14, true));



                List<double> data = Dataset.Normalize(entry.Value);

                List<Vector2> Values = data.Select((v, i) => new Vector2(i, v)).ToList();

                //информация

                Border brdAnalize = new Border()
                {
                    BorderThickness = new Thickness(3, 1, 3, 5),
                    BorderBrush = Brushes.Cyan,
                    CornerRadius = new CornerRadius(4),
                    Padding = new Thickness(5)
                };
                Grid.SetColumn(brdAnalize, currentRow);
                Grid.SetRow(brdAnalize, 1);
                grdDataGrid.Children.Add(brdAnalize);


                Grid grdAnalizeContainer = new Grid();
                brdAnalize.Child = grdAnalizeContainer;






                //Отображение значения из выборки
                grdAnalizeContainer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(80) });

                Border distributionInfo = new Border()
                {
                    BorderBrush = Brushes.Cyan,
                    BorderThickness = new Thickness(2),
                };

                double Correlation = Math.Round(RegressionCalculator.GetDualCorrelation(Values.Select(v => v.X).ToArray(), Values.Select(v => v.Y).ToArray()), 3);
                double Determination = Math.Round(Correlation * Correlation, 3);


                TextBlock tbDistributionInfo = new TextBlock()
                {
                    AllowDrop = true,
                    Text = $"Данные параметра {(Determination > 0.8 ? "линейно" : "не линейно")} связаны",
                    FontSize = 10,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment= VerticalAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                    Foreground = Brushes.White,
                    TextWrapping = TextWrapping.Wrap
                };
                distributionInfo.Child = tbDistributionInfo;

                Grid.SetRow(distributionInfo, grdAnalizeContainer.RowDefinitions.Count - 1);
                grdAnalizeContainer.Children.Add(distributionInfo);







                //график распределения
                grdAnalizeContainer.RowDefinitions.Add(new RowDefinition());

                Border Graphyc = CreateRegressionGraphyc(Values);
                Grid.SetRow(Graphyc, grdAnalizeContainer.RowDefinitions.Count - 1);

                grdAnalizeContainer.Children.Add(Graphyc);

            }
        }






        Border CreateRegressionGraphyc(List<Vector2> Values)
        {
            Border brd = new Border()
            {
                VerticalAlignment = VerticalAlignment.Stretch
            };

            Plot plot = new Plot()
            {
                TitleFontSize = 12,
                TitleColor = Colors.White,
                Background = Brushes.Transparent,
                TextColor = Colors.Cyan,
                PlotAreaBorderColor = Colors.Cyan,
                Title = "Уравнение Трента",
                SubtitleColor = Colors.Magenta,
                SubtitleFontSize = 10,
                Margin = new Thickness(-8, 0, 8, 0)
            };

            //Данные

            plot.Series.Add(CreateGraphyc(Values, true, Colors.Cyan, Colors.Cyan, 1));


            //Линейная регрессия
            double a, b;
            RegressionCalculator.LinearLeastSquares(Values.Select(v => v.X).ToArray(), Values.Select(v => v.Y).ToArray(), out a, out b);

            List<Vector2> LinearRegression = new List<Vector2>();
            LinearRegression.Add(new Vector2(0, b));
            LinearRegression.Add(new Vector2(Values.Count, Values.Count * a + b));

            plot.Series.Add(CreateGraphyc(LinearRegression, false, Colors.Magenta, Colors.Cyan, 3));


            brd.Child = plot;

            return brd;
        }






        LineSeries CreateGraphyc(List<Vector2> data, bool needShowPoint, Color graficColor, Color dotColor, int strokeThickness)
        {
            LineSeries function = new LineSeries()
            {
                Background = Brushes.Transparent,
                Color = graficColor,
                StrokeThickness = strokeThickness,
                MarkerFill = dotColor,
                MarkerSize = 3,
                MarkerStroke = Colors.White,
                MarkerType = needShowPoint ? OxyPlot.MarkerType.Circle : OxyPlot.MarkerType.None
            };

            function.ItemsSource = data.Select(v => new OxyPlot.DataPoint(v.X, v.Y)).ToList();

            return function;
        }





        Border CreateTextBlock(string text, int row, int coll, Brush background = null, int size = 12, bool bold = false)
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

    }
}