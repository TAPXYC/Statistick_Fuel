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
    /// <summary>
    /// Логика взаимодействия для DataAnalize.xaml
    /// </summary>
    public partial class PointAnalize : Page
    {

        int HeaderSize = 16;


        Dictionary<string, Func<List<double>, double>> AnalizeOperations = new Dictionary<string, Func<List<double>, double>>()
        {
            {"Среднее арифметическое", Dataset.GetAverageNormal },
            {"Мода", Dataset.GetModaNormal },
            {"Медиана", Dataset.GetMedianaNormal },
            { "Дисперсия", Dataset.GetDispersionNormal},
            {"Стандартное отклонение", Dataset.GetStandardDeviationNormal },
            {"Эксцесс",Dataset.GetExcessNormal },
            {"Ассиметрия", Dataset.GetAsimetrionNormal },
            { "Коэффициент вариации", Dataset.GetVariationNormal},
            { "Предельная ошибка", Dataset.GetLimErrorNormal},
            { "Перцентиль 40%", Dataset.GetPercentil40Normal},
            { "Перцентиль 80%", Dataset.GetPercentil80Normal}
        };


        public PointAnalize()
        {
            InitializeComponent();
            MainWindow.SetStage("Точечный анализ");
        }




        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTable();
        }



        void CreateTable()
        {
            grdAnalizeGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(120) });

            grdAnalizeGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });

            int currentRow = 0;

            foreach (KeyValuePair<string, Func<List<double>, double>> op in AnalizeOperations)
            {
                grdAnalizeGrid.RowDefinitions.Add(new RowDefinition());
                grdAnalizeGrid.Children.Add(CreateTextBlock(op.Key, currentRow + 1, 0, 
                    currentRow % 2 == 0 ? Brushes.Transparent : Brushes.Cyan, HeaderSize, true));


                int currentColl = 1;

                foreach (KeyValuePair<string, List<double>> d in Dataset.Data)
                {

                    if (currentRow == 0)
                    {
                        grdAnalizeGrid.ColumnDefinitions.Add(new ColumnDefinition());

                        grdAnalizeGrid.Children.Add(CreateTextBlock(d.Key, 0, grdAnalizeGrid.ColumnDefinitions.Count-1, Brushes.Transparent,
                            HeaderSize, true));
                    }

                    grdAnalizeGrid.Children.Add(CreateTextBlock(op.Value(d.Value).ToString(), currentRow + 1,
                        currentColl++, currentRow % 2 == 0 ? Brushes.Transparent : Brushes.Cyan));

                }

                currentRow++;
            }
        }




        Border CreateTextBlock(string text, int row, int coll, Brush background = null, int size = 14, bool bold = false)
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

    }
}
