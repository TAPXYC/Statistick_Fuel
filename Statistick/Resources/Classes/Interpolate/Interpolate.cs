using System;
using System.Collections.Generic;
using System.Linq;


class Interpolate
{
    public double[] a, b, c, d;
    List<Vector2> Data;


    public Interpolate(List<Vector2> data)
    {
        Data = data;
    }

    public List<Vector2> GetInterpolateData(double step = 0.001)
    {
        CalculateSlineCoeff();


        List<Vector2> result = new List<Vector2>();

        double min = Data.Min(v => v.X);
        double max = Data.Max(v => v.X);


        for (double x = min; x <= max; x += step)
            result.Add(Spline(x));

        return result;
    }


    void CalculateSlineCoeff()
    {
        int Size = Data.Count;

        a = new double[Size - 1];
        b = new double[Size - 1];
        c = new double[Size - 1];
        d = new double[Size - 1];

        double[] h = new double[Size - 1];

        for (int i = 0; i < Size - 1; i++)
        {
            h[i] = Data[i + 1].X - Data[i].X;
        }


        double[,] CMatrix = new double[Size - 2, Size - 2];
        double[] RightPart = new double[Size - 2];


        for (int i = 0; i < Size - 2; i++)
        {
            for (int j = i - 1, type = 0; j <= i + 1; j++, type++)
            {
                if (j == -1 || j == Size - 2)
                    continue;

                if (type == 0)
                    CMatrix[i, j] = h[j];

                if (type == 1)
                    CMatrix[i, j] = 2 * (h[j] + h[j + 1]);

                if (type == 2)
                    CMatrix[i, j] = h[j];
            }


            RightPart[i] = 3 * ((Data[i].Y - Data[i + 1].Y) / h[i] - (Data[i + 1].Y - Data[i + 2].Y) / h[i]);
        }

        var cRes = new ScalarThreePointRun(CMatrix, RightPart).SolveMatrix().ToList();
        cRes.Insert(0, 0);

        c = cRes.ToArray();

        for (int i = 0; i < Size - 1; i++)
        {
            a[i] = Data[i].Y;
            d[i] = (((i + 1 == d.Length) ? 0 : c[i + 1]) - c[i]) / (3 * h[i]);

            b[i] = ((Data[i + 1].Y - Data[i].Y) / h[i])
                -
                (h[i] / 3 * (((i + 1 == d.Length) ? 0 : c[i + 1]) + 2 * c[i]));
        }


    }


    Vector2 Spline(double x)
    {
        double Res = 0;
        int i = 0;

        for (int j = 0; j < Data.Count; j++)
        {
            if (Data[j].X >= x)
            {
                i = j;
                break;
            }
        }

        i = i == 0 ? 0 : i - 1;

        double A = a[i];
        double B = b[i];
        double C = c[i];
        double D = d[i];

        double Delta = x - (i == 0 ? Data[0].X : Data[i].X);

        Res = A + B * Delta + C * Math.Pow(Delta, 2) + D * Math.Pow(Delta, 3);

        return new Vector2(x, Res);
    }
}
