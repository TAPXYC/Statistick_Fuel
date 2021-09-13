using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Statistick.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для CorrPleyad.xaml
    /// </summary>
    public partial class CorrPleyad : Page
    {
        public CorrPleyad()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CreateLink(cnvDual, RegressionCalculator.GetDualCorrelation(DictionaryToList(Dataset.Data)).ToArray());
            CreateLink(cnvParticle, RegressionCalculator.GetParticleCorrelation(DictionaryToList(Dataset.Data)));
        }


        List<double[]> DictionaryToList(Dictionary<string, List<double>> dictionary)
        {
            List<double[]> Res = new List<double[]>();

            foreach (KeyValuePair<string, List<double>> entry in dictionary)
                Res.Add(Dataset.Normalize(entry.Value).ToArray());

            return Res;
        }


        private void CreateLink(Canvas canvas, double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == j)
                        continue;

                    double startL = Canvas.GetLeft(canvas.Children[i]) + 20;
                    double startT = Canvas.GetTop(canvas.Children[i]) + 20;

                    double endL = Canvas.GetLeft(canvas.Children[j]) + 20;
                    double endT = Canvas.GetTop(canvas.Children[j]) + 20;

                    double v = Math.Abs(matrix[i, j]);

                    Line line = new Line()
                    {
                        Stroke = v>0.7? Brushes.Cyan : 
                                v>0.5? Brushes.Yellow :
                                v>0.3? Brushes.Magenta :
                                Brushes.Gray,
                        StrokeThickness = v > 0.7 ? 4 :
                                v > 0.5 ? 3 :
                                v > 0.3 ? 2 :
                                1,
                        X1 = startL,
                        Y1 = startT,
                        X2 = endL,
                        Y2 = endT
                    };
                    canvas.Children.Add(line);
                }
            }
        }
    }
}
