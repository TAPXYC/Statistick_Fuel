using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Statistick.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для MultipleRegressionAnalysis.xaml
    /// </summary>
    public partial class MultipleRegressionAnalysis : Page
    {
        public MultipleRegressionAnalysis()
        {
            InitializeComponent();
            MainWindow.SetStage("Регрессионное моделирование");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Calculate();
            LoadData();
        }


        void LoadData()
        {
            LinearRegressionAnalize();
            ParticleRegression();
        }




        void LinearRegressionAnalize()
        {
            spRegressionData.Children.Add(new TextBlock()
            {
                Text = "Анализ множественной регрессии",
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Margin = new Thickness(0, 5, 5, 30),
                TextWrapping = TextWrapping.Wrap
            });

            spRegressionData.Children.Add(CreateData("Корреляционный анализ",
                $"Множественный R: 0.9887\nR-квадрат: 0.9775\nСтандартная ошибка: 2.08431485163318\nНаблюдения: 16", Brushes.Cyan));

            spRegressionData.Children.Add(CreateData("t-критерий Стьюдента",
                $"y\tx1\tx2\tx3\tx4\tx5\tx6\tx7\tx8\n" +
                $"0.153\t0.258\t0.011\t0.477\t0.359\t0.0354\t0.0126\t0.609\t0.674\n\np = 0.05", Brushes.Magenta));


            spRegressionData.Children.Add(CreateData("Уравнение регрессии",
                $"y = -86.626 + 0.698x1 + 0.759x2 + 0.219x3 + 18.534x4 + 0.177x5 - 0.303x6 - 0.022x7 - 0.038x8", Brushes.LightCoral));
        }




        void ParticleRegression()
        {
            spParticleCorrelation.Children.Add(new TextBlock()
            {
                Text = "Анализ с учетом только значимых факторов",
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Margin = new Thickness(0, 5, 5, 10),
                TextWrapping = TextWrapping.Wrap
            });

            spParticleCorrelation.Children.Add(CreateData("Уравнения множественной регрессии",
                $"y = 0.823x2 + 0.178x5 - 0.297x6 - 30.641", Brushes.LightCoral));
        }




        Border CreateData(string tittle, string data, Brush color)
        {
            Border brdAnalize = new Border()
            {
                BorderThickness = new Thickness(3, 1, 3, 5),
                BorderBrush = color,
                CornerRadius = new CornerRadius(4),
                Margin = new Thickness(10),
                Padding = new Thickness(5)
            };

            Grid grdAnalizeContainer = new Grid();
            brdAnalize.Child = grdAnalizeContainer;

            grdAnalizeContainer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
            TextBlock tbHeader = new TextBlock()
            {
                Text = tittle,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                FontSize = 12,
                Margin = new Thickness(0, 5, 0, 5),
                TextWrapping = TextWrapping.Wrap
            };

            grdAnalizeContainer.Children.Add(tbHeader);



            //Отображение значения из выборки
            grdAnalizeContainer.RowDefinitions.Add(new RowDefinition());

            Border distributionInfo = new Border()
            {
                BorderBrush = color,
                BorderThickness = new Thickness(2),
            };


            TextBlock tbDistributionInfo = new TextBlock()
            {
                AllowDrop = true,
                Text = data,
                FontSize = 10,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5)
            };
            distributionInfo.Child = tbDistributionInfo;

            Grid.SetRow(distributionInfo, grdAnalizeContainer.RowDefinitions.Count - 1);
            grdAnalizeContainer.Children.Add(distributionInfo);

            return brdAnalize;
        }







        private void ShowData(object sender, RoutedEventArgs e)
        {
            LoadNextPage(new DataViewer());
        }


        private void ShowPointAnalize(object sender, RoutedEventArgs e)
        {
            LoadNextPage(new PointAnalize());
        }


        private void ShowParticleCorrelation(object sender, RoutedEventArgs e)
        {
            LoadNextPage(new ParticalCorrelation());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //(Application.Current.MainWindow as MainWindow).
            LoadNextPage(new RegressionIntervalAnalize());
        }



        private Page CurrentPage;

        private async void LoadNextPage(Page page)
        {
            CurrentPage = page;
            await Task.Run(() => Dispatcher.Invoke(() => dataFrame.Navigate(CurrentPage)));
        }









        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }



        void Calculate()
        {
            double x1, x2, x3, x4, x5, x6, x7, x8;

            x1 = CheckTB(tbx1);
            x2 = CheckTB(tbx2);
            x3 = CheckTB(tbx3);
            x4 = CheckTB(tbx4);
            x5 = CheckTB(tbx5);
            x6 = CheckTB(tbx6);
            x7 = CheckTB(tbx7);
            x8 = CheckTB(tbx8);

            tbRes1.Text = (-86.626 + 0.698 * x1 + 0.759 * x2 + 0.219 * x3 + 18.534 * x4 + 0.177 * x5 - 0.303 * x6 - 0.022 * x7 - 0.038 * x8).ToString();
            tbRes2.Text = (0.823 * x2 + 0.178 * x5 - 0.297 * x6 - 30.641).ToString();
        }



        double CheckTB(TextBox text)
        {
            double Res = 0;

            if (!double.TryParse(text.Text, out Res))
                text.Text = "0";

            text.Text = Res.ToString();

            return Res;
        }

        private void tbx_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = "";
        }

    }
}
