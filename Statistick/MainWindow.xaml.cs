using Statistick.Resources.Pages;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Statistick
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static TextBlock _tbStage;

        public MainWindow()
        {
            InitializeComponent();

            _tbStage = tbStage;
        }


        public static void SetStage(string stage)
        {
            _tbStage.Text = $"  ({stage})";
        }



        private void ShowPointAnalize(object sender, RoutedEventArgs e)
        {
            LoadNextPage(new PointAnalize());
        }

        private void ShowData(object sender, RoutedEventArgs e)
        {
            LoadNextPage(new DataViewer());
        }

        private void ShowIntervalAnalize(object sender, RoutedEventArgs e)
        {
            LoadNextPage(new IntervalAnalize());
        }

        private void ShowLinearRegression(object sender, RoutedEventArgs e)
        {
            LoadNextPage(new DispersionVisualization());
        }


        private void ShowParticleCorrelation(object sender, RoutedEventArgs e)
        {
            LoadNextPage(new ParticalCorrelation());
        }

        private void ShowMultipleRegression(object sender, RoutedEventArgs e)
        {
            LoadNextPage(new MultipleRegressionAnalysis());
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadNextPage(new CorrPleyad());
        }


        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            Close();
        }





        private Page CurrentPage;

        public void LoadNextPage(Page page)
        {
            CurrentPage = page;
            stbOff.Begin();
        }

        private async void stbOff_Completed(object sender, EventArgs e)
        {
            await Task.Run(() => Dispatcher.Invoke(() => mainFrame.Navigate(CurrentPage)));
            stbOn.Begin();
        }
    }
}
