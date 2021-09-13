using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Statistick.Resources.Pages
{
    public partial class RegressionIntervalAnalize : Page
    {
        int StartYear = 2005;
        int HeaderSize = 16;


        double Regression(double x2, double x5, double x6)
        {
            return 0.823 * x2 + 0.178 * x5 - 0.297 * x6 - 30.641;
        }




        public RegressionIntervalAnalize()
        {
            InitializeComponent();
            MainWindow.SetStage("Интервальный анализ");
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTable();
        }



        void CreateTable()
        {
            var data = Dataset.Data;

            var x2 = data["Суммарный налог на топливо (% от стоимости)"];
            var x5 = data["Объем добычи нефти (млн тонн)"];
            var x6 = data["Экспорт нефти из РФ (млн тонн)"];
            var y = data["Цена за литр бензина А-92 (руб)"];

            List<Vector2> Reqpoints = new List<Vector2>();
            List<Vector2> Truepoints = new List<Vector2>();

            for (int i = 0; i < y.Count; i++, StartYear++)
            {
                if (grdDataGrid.RowDefinitions.Count < i + 3)
                {
                    grdDataGrid.RowDefinitions.Add(new RowDefinition());
                    grdDataGrid.Children.Add(CreateTextBlock((StartYear).ToString(), i + 1, 0, i % 2 == 0 ? Brushes.Transparent :
                        Brushes.Cyan, HeaderSize, true));
                }


                double req = Regression(x2[i], x5[i], x6[i]);
                double Y = y[i];

                Reqpoints.Add(new Vector2(StartYear, req));
                Truepoints.Add(new Vector2(StartYear, Y));

                grdDataGrid.Children.Add(CreateTextBlock(req.ToString(), i + 1,
                    1, i % 2 == 0 ? Brushes.Transparent : Brushes.Cyan));

                grdDataGrid.Children.Add(CreateTextBlock(Y.ToString(), i + 1,
                    2, i % 2 == 0 ? Brushes.Transparent : Brushes.Cyan));

                grdDataGrid.Children.Add(CreateTextBlock(Math.Abs(Math.Round(Y - req, 3)).ToString(), i + 1,
                    3, i % 2 == 0 ? Brushes.Transparent : Brushes.Cyan));
            }

            var Grap = CreateRegressionGraphyc(Reqpoints, Truepoints);
            Grid.SetColumn(Grap, 4);
            Grid.SetRow(Grap, 1);
            Grid.SetRowSpan(Grap, y.Count);

            grdDataGrid.Children.Add(Grap);
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
                FontWeight = FontWeights.Bold,
                Foreground = background == Brushes.Transparent ? Brushes.White : Brushes.Black
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




        Border CreateRegressionGraphyc(List<Vector2> Req, List<Vector2> True)
        {
            Border brd = new Border()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                Margin = new Thickness(10)
            };

            Plot plot = new Plot()
            {
                Background = Brushes.Transparent,
                TextColor = Colors.Cyan,
                PlotAreaBorderColor = Colors.Cyan,
                SubtitleColor = Colors.Magenta,
                LegendFontSize=20,
                LegendPosition = OxyPlot.LegendPosition.BottomRight
            };


            //Данные

            plot.Series.Add(CreateGraphyc("Истинное значение", new Interpolate(True).GetInterpolateData(), false, Colors.Cyan, Colors.Cyan, 1));

            plot.Series.Add(CreateGraphyc("Регрессия", new Interpolate(Req).GetInterpolateData(), false, Colors.Magenta, Colors.Cyan, 3));


            brd.Child = plot;

            return brd;
        }



        LineSeries CreateGraphyc(string name, List<Vector2> data, bool needShowPoint, Color graficColor, Color dotColor, int strokeThickness)
        {
            LineSeries function = new LineSeries()
            {
                Title = name,
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
    }
}
