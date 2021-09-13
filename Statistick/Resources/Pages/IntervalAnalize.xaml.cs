using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Statistick.Resources.Pages
{
    public partial class IntervalAnalize : Page
    {
        List<IntervalVisual> intervalVisuals = new List<IntervalVisual>();

        SolidColorBrush MatAwaitBrush;
        SolidColorBrush DispersionIntervalBrush;


        public IntervalAnalize()
        {
            InitializeComponent();
            MainWindow.SetStage("Проверка на нормальность распределения");
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Color MatAwaitColor = new Color();
            MatAwaitColor.A = 0xFF;
            MatAwaitColor.R = 0xD1;
            MatAwaitColor.G = 0x00;
            MatAwaitColor.B = 0xFF;

            MatAwaitBrush = new SolidColorBrush(MatAwaitColor);

            DispersionIntervalBrush = new SolidColorBrush(Colors.Yellow);


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

                //информация

                Border brdAnalize = new Border()
                {
                    BorderThickness = new Thickness(3,1,3,5),
                    BorderBrush = Brushes.Cyan,
                    CornerRadius = new CornerRadius(4),
                    Padding = new Thickness(5)
                };
                Grid.SetColumn(brdAnalize, currentRow);
                Grid.SetRow(brdAnalize, 1);
                grdDataGrid.Children.Add(brdAnalize);


                Grid grdAnalizeContainer = new Grid();
                brdAnalize.Child = grdAnalizeContainer;


                #region Info

                grdAnalizeContainer.RowDefinitions.Add(new RowDefinition() { Height=new GridLength(60)});
                TextBlock tbHeader = new TextBlock()
                {
                    Text = "Интервальная оценка",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.Bold,
                    FontSize = 12,
                    Margin = new Thickness(0,5,0,5),
                    TextWrapping = TextWrapping.Wrap
                };

                grdAnalizeContainer.Children.Add(tbHeader);





                //Отображение значения из выборки
                grdAnalizeContainer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });

                Border valueInfo = new Border()
                {
                    BorderBrush = Brushes.Cyan,
                    BorderThickness = new Thickness(2),
                };

                TextBlock tbValueInfo = new TextBlock()
                {
                    Text = "Значение",
                    FontSize = 10,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                    Foreground = Brushes.White
                };
                valueInfo.Child = tbValueInfo;

                Grid.SetRow(valueInfo, grdAnalizeContainer.RowDefinitions.Count - 1);
                grdAnalizeContainer.Children.Add(valueInfo);






                //Легенда мат ожидания
                grdAnalizeContainer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });

                TextBlock tbMatAwaitInfo;
                StackPanel spMatAwaitInfo = CreateLegend(MatAwaitBrush, out tbMatAwaitInfo);


                Grid.SetRow(spMatAwaitInfo, grdAnalizeContainer.RowDefinitions.Count - 1);
                grdAnalizeContainer.Children.Add(spMatAwaitInfo);







                //легенда дисперсии
                grdAnalizeContainer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });

                TextBlock tbDispIntervalInfo;
                StackPanel spDispIntervalInfo = CreateLegend(DispersionIntervalBrush, out tbDispIntervalInfo);


                Grid.SetRow(spDispIntervalInfo, grdAnalizeContainer.RowDefinitions.Count - 1);
                grdAnalizeContainer.Children.Add(spDispIntervalInfo);





                //Отображение значения из выборки
                grdAnalizeContainer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(65) });

                Border distributionInfo = new Border()
                {
                    BorderBrush = Brushes.Cyan,
                    BorderThickness = new Thickness(2),
                };

                double Pirson = Dataset.GetPirson(data, 9);
                double Crit = Dataset.GetCritical(data, 9);


                TextBlock tbDistributionInfo = new TextBlock()
                {
                    AllowDrop = true,
                    Text = $"Критерий Пирсона: {Pirson}\nКритич.: {Crit}\nРаспределение {(Crit > Pirson ? "нормальное" : "не нормальное")}",
                    FontSize = 10,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                    Foreground = Brushes.White,
                    TextWrapping = TextWrapping.Wrap
                };
                distributionInfo.Child = tbDistributionInfo;

                Grid.SetRow(distributionInfo, grdAnalizeContainer.RowDefinitions.Count - 1);
                grdAnalizeContainer.Children.Add(distributionInfo);






                //график распределения
                grdAnalizeContainer.RowDefinitions.Add(new RowDefinition());

                Border Graphyc = CreateRateGraphyc(data, 9);
                Grid.SetRow(Graphyc, grdAnalizeContainer.RowDefinitions.Count - 1);

                grdAnalizeContainer.Children.Add(Graphyc);
                #endregion




                double min = Dataset.GetMin(data);
                double max = Dataset.GetMax(data);
                double avg = Dataset.GetAverage(data);

                double DiagrammHeight = 80;


                #region Diagramm
                grdAnalizeContainer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(DiagrammHeight+20) });

                Grid grdSequence = new Grid();
                Grid.SetRow(grdSequence, grdAnalizeContainer.RowDefinitions.Count - 1);

                grdAnalizeContainer.Children.Add(grdSequence);

                for (int i = 0; i < data.Count; i++)
                {
                    grdSequence.ColumnDefinitions.Add(new ColumnDefinition());

                    Rectangle rectangle = CreateRectangle(data[i], min, max, DiagrammHeight);

                    Panel.SetZIndex(rectangle, 5);

                    rectangle.MouseEnter += (s, a) => tbValueInfo.Text = rectangle.Tag.ToString();
                    rectangle.MouseLeave += (s, a) => tbValueInfo.Text = "Значение";

                    Grid.SetColumn(rectangle, i);

                    grdSequence.Children.Add(rectangle);
                }
                #endregion

                #region MatAwait

                ProgressBar RightMatAwait = new ProgressBar()
                {
                    BorderBrush = Brushes.Transparent,
                    Background = Brushes.Transparent,
                    Foreground = MatAwaitBrush,
                    Minimum = avg - 0.01,
                    Maximum = max,
                    Height = DiagrammHeight + 10,
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                Grid.SetColumnSpan(RightMatAwait, data.Count / 2);
                Grid.SetColumn(RightMatAwait, data.Count / 2);

                Panel.SetZIndex(RightMatAwait, 4);

                grdSequence.Children.Add(RightMatAwait);



                ProgressBar LeftMatAwait = new ProgressBar()
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    BorderBrush = Brushes.Transparent,
                    Background = Brushes.Transparent,
                    Foreground = MatAwaitBrush,
                    Minimum = min - 0.01,
                    Maximum = avg,
                    Height = DiagrammHeight + 10,
                    VerticalAlignment = VerticalAlignment.Bottom
                };
                Grid.SetColumnSpan(LeftMatAwait, entry.Value.Count / 2);

                Panel.SetZIndex(LeftMatAwait, 4);

                grdSequence.Children.Add(LeftMatAwait);


                intervalVisuals.Add(new IntervalVisual(LeftMatAwait, RightMatAwait, tbMatAwaitInfo, data, sdrA.Value, 
                    Dataset.GetMatAwaitConfidenceInterval));

                #endregion

                #region DispersionLimit

                ProgressBar RightDispersionLimit = new ProgressBar()
                {
                    BorderBrush = Brushes.Transparent,
                    Background = Brushes.Transparent,
                    Foreground = DispersionIntervalBrush,
                    Minimum = avg - 0.01,
                    Maximum = max,
                    Height = DiagrammHeight + 15,
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                Grid.SetColumnSpan(RightDispersionLimit, data.Count / 2);
                Grid.SetColumn(RightDispersionLimit, data.Count / 2);

                grdSequence.Children.Add(RightDispersionLimit);



                ProgressBar LeftDispersionLimit = new ProgressBar()
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    BorderBrush = Brushes.Transparent,
                    Background = Brushes.Transparent,
                    Foreground = DispersionIntervalBrush,
                    Minimum = min - 0.01,
                    Maximum = avg,
                    Height = DiagrammHeight + 15,
                    VerticalAlignment = VerticalAlignment.Bottom
                };
                Grid.SetColumnSpan(LeftDispersionLimit, entry.Value.Count / 2);

                grdSequence.Children.Add(LeftDispersionLimit);


                intervalVisuals.Add(new IntervalVisual(LeftDispersionLimit, RightDispersionLimit, tbDispIntervalInfo, data, sdrA.Value,
                    Dataset.GetDispersionConfidenceInterval));

                #endregion
            }
        }





        


        

        StackPanel CreateLegend(Brush rectBrush, out TextBlock tbDisplayInfo)
        {
            StackPanel spLegend = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center
            };

            Rectangle rctColor = new Rectangle()
            {
                Fill = rectBrush,
                Width = 10,
                Height = 20,
                Stroke = Brushes.White,
                StrokeThickness = 1
            };
            spLegend.Children.Add(rctColor);

            tbDisplayInfo = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Foreground = Brushes.White,
                FontSize = 10,
                Margin = new Thickness(10, 0, 10, 0)
            };
            spLegend.Children.Add(tbDisplayInfo);

            return spLegend;
        }



        Border CreateRateGraphyc(List<double> data, int segment)
        {
            Border brd = new Border()
            {
                VerticalAlignment = VerticalAlignment.Stretch
            };

            List<Vector2> ValueRate = Dataset.GetRate(data, segment);

            OxyPlot.Wpf.LineSeries function = new OxyPlot.Wpf.LineSeries()
            {
                Background = Brushes.Transparent,
                Color = Colors.Cyan
            };

            function.ItemsSource = new Interpolate(ValueRate).GetInterpolateData(0.01).Select(v => new OxyPlot.DataPoint(v.X, v.Y)).ToList();

            Plot plot = new Plot()
            {
                TitleFontSize = 10,
                TitleColor = Colors.White,
                Background = Brushes.Transparent,
                TextColor = Colors.Cyan,
                PlotAreaBorderColor = Colors.Cyan,
                Title = "Частотное распределение",
                Margin = new Thickness(-8,0,8,0)
            };
            plot.Series.Add(function);
            brd.Child = plot;

            return brd;
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



        Rectangle CreateRectangle(double height, double min, double max, double containerHeight)
        {

            return new Rectangle()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Bottom,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                Fill = Brushes.Cyan,
                Height = ((height - min) / (max - min)) * containerHeight + 5,
                Margin = new Thickness(1,1,1,-1),
                Tag = Math.Round(height, 3)
            };
        }



        private void sdrA_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double a = (sender as Slider).Value;

            tbValue.Text = Math.Round(a, 3).ToString();

            foreach (IntervalVisual v in intervalVisuals)
                v.ChangeAlpha(a);
        }
    }
}