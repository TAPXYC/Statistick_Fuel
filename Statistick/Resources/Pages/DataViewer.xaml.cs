using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Statistick.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для DataViewer.xaml
    /// </summary>
    public partial class DataViewer : Page
    {
        int StartYear = 2005;
        bool IsNormal = false;
        int HeaderSize = 16;


        public DataViewer()
        {
            InitializeComponent();
            MainWindow.SetStage("Исходные данные");
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTable();
        }



        void CreateTable()
        {
            grdDataGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(120) });

            grdDataGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(80) });


            foreach (KeyValuePair<string, List<double>> d in Dataset.Data)
            {
                grdDataGrid.ColumnDefinitions.Add(new ColumnDefinition());
                int currentRow = grdDataGrid.ColumnDefinitions.Count - 1;

                grdDataGrid.Children.Add(CreateTextBlock(d.Key, 0, currentRow, Brushes.Transparent, HeaderSize, true));

                for (int i = 0; i < d.Value.Count; i++)
                {
                    if (grdDataGrid.RowDefinitions.Count < i + 2)
                    {
                        grdDataGrid.RowDefinitions.Add(new RowDefinition());
                        grdDataGrid.Children.Add(CreateTextBlock((StartYear++).ToString(), i + 1, 0, i % 2 == 0 ? Brushes.Transparent :
                            Brushes.Cyan, HeaderSize, true));
                    }

                    grdDataGrid.Children.Add(CreateTextBlock(d.Value[i].ToString(), i + 1,
                        currentRow, i % 2 == 0 ? Brushes.Transparent : Brushes.Cyan));
                }
            }
        }


        private void ClearGrid()
        {
            StartYear = 2005;
            Button btn = grdDataGrid.Children[0] as Button;
            grdDataGrid.RowDefinitions.Clear();
            grdDataGrid.ColumnDefinitions.Clear();
            grdDataGrid.Children.Clear();
            grdDataGrid.Children.Add(btn);
        }



        void CreateNormalizeTable()
        {

            grdDataGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(120) });

            grdDataGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(80) });


            foreach (KeyValuePair<string, List<double>> d in Dataset.Data)
            {
                grdDataGrid.ColumnDefinitions.Add(new ColumnDefinition());
                int currentRow = grdDataGrid.ColumnDefinitions.Count - 1;

                grdDataGrid.Children.Add(CreateTextBlock(d.Key, 0, currentRow, Brushes.Transparent, HeaderSize, true));


                var normalizeData = Dataset.Normalize(d.Value);

                for (int i = 0; i < normalizeData.Count; i++)
                {
                    if (grdDataGrid.RowDefinitions.Count < i + 2)
                    {
                        grdDataGrid.RowDefinitions.Add(new RowDefinition());
                        grdDataGrid.Children.Add(CreateTextBlock((StartYear++).ToString(), i + 1, 0, i % 2 == 0 ? Brushes.Transparent :
                            Brushes.Cyan, HeaderSize, true));
                    }

                    grdDataGrid.Children.Add(CreateTextBlock(Math.Round(normalizeData[i], 3).ToString(), i + 1,
                        currentRow, i % 2 == 0 ? Brushes.Transparent : Brushes.Cyan));
                }
            }
        }








        Border CreateTextBlock(string text, int row, int coll, Brush background = null, int size=16, bool bold = false)
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

        private void btnChangeTable_Click(object sender, RoutedEventArgs e)
        {
            ClearGrid();

            if (IsNormal)
            {
                MainWindow.SetStage("Исходные данные");
                CreateTable();
            }
            else
            {
                MainWindow.SetStage("Нормированные данные");
                CreateNormalizeTable();
            }

            IsNormal = !IsNormal;
        }
    }
}
