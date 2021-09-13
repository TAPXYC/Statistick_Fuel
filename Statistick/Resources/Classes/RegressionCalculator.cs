using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;




class RegressionCalculator
{
    public static void LinearLeastSquares(double[] x, double[] y, out double a, out double b)
    {
        double a11 = 0.0, a12 = 0.0, a22 = x.Length, b1 = 0.0, b2 = 0.0;

        for (int i = 0; i < x.Length; i++)
        {
            a11 += x[i] * x[i];
            a12 += x[i];
            b1 += x[i] * y[i];
            b2 += y[i];
        }

        double det = a11 * a22 - a12 * a12;


        a = (b1 * a22 - a12 * b2) / det;
        b = (a11 * b2 - b1 * a12) / det;
    }



    public static double GetDualCorrelation(double[] data1, double[] data2)
    {
        return Correlation.Pearson(data1, data2);
    }

    public static Matrix<double> GetDualCorrelation(List<double[]> data)
    {
        return Correlation.PearsonMatrix(data);
    }




    public static double GetParticleCorrelationCoeff(List<double[]> data)
    {
        double Res = 0;
        Matrix<double> CorrelationMatrix = GetDualCorrelation(data);
        Matrix<double> InverseMatrix = CorrelationMatrix.Inverse();
        Vector<double> Diagonal = InverseMatrix.Diagonal();

        Diagonal = Diagonal.PointwiseSqrt();


        return Res;
    }






    public static double[,] GetParticleCorrelation(List<double[]> data)
    {
        Matrix<double> CorrelationMatrix = GetDualCorrelation(data);
        Matrix<double> InverseMatrix = CorrelationMatrix.Inverse();
        Vector<double> Diagonal = InverseMatrix.Diagonal();

        double[,] Result = InverseMatrix.ToArray();

        for(int i = 0; i < Result.GetLength(0); i++)
        {
            for (int j = 0; j < Result.GetLength(1); j++)
            {
                Result[i, j] = -Result[i, j] / Math.Sqrt(Diagonal[i] * Diagonal[j]);
            }
        }


        return Result;
    }




    public static double GetCovariance(double[] data1, double[] data2)
    {
        return Statistics.Covariance(data1, data2);
    }
}
