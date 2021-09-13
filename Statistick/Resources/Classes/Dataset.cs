using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Statistics;



public class Dataset
{
    public static Dictionary<string, List<double>> Data = new Dictionary<string, List<double>>()
    {
        {
            "Количество автомобилей в РФ (млн ед)",
            new List<double>() {24.3,25.5,27.9,30.5,31.5,32.6,34.6,38.8,39.1,40.7,42.2,43,44.8,45.4,46.3,47.6 }
        },
        {
            "Суммарный налог на топливо (% от стоимости)",
            new List<double>() {40,43,43,43,45,45,45,45,45,55,55,55,55,60,60,60 }
        },
        {
            "Численность персонала нефтедобывающего сектора (тыс. человек)",
            new List<double>() {126,125.8,122.8,118.9,112.7,111.6,110.5,109.4,108.3,107.2,106.1,105.1,104,103,101.9,100.9 }
        },
        {
            "Затраты на охрану окружающей среды (% от ВВП РФ)",
            new List<double>() {1.1,1,0.9,0.9,0.9,0.8,0.8,0.7,0.7,0.7,0.7,0.7,0.7,0.6,0.6,0.6 }
        },
        {
            "Объем добычи нефти (млн тонн)",
            new List<double>() {470,481,491,488.6,494.3,505.2,511.4,518,523.3,526.8,534.1,547.5,546.8,555.8,560.2,512.7 }
        },
        {
            "Экспорт нефти из РФ (млн тонн)",
            new List<double>() { 252.5,248.4,258.6,243.1,247.5,250.7,244.5,240,236.5,223.5,244.5,254.5,252.8,260.6,269.2,238.6 }
        },
        {
            "Цена за баррель нефти (доллары США)",
            new List<double>() {54.31,62.77,71.32,113.85,73.19,82.92,123.15,120.59,107.72,101.9,47.23,47.68,49.51,75.37,65.38,41.45 }
        },
        {
            "Курс российского рубля к доллару США",
            new List<double>() {27.7,28.82,26.3,24.57,36.43,31.78,32.68,29.35,29.93,32.66,62.29,83.59,59.94,62.92,69.32,86.06 }
        },
        {
            "Цена за литр бензина А-92 (руб)",
            new List<double>() {14.32,15.75,17.01,17.41,19.16,20.09,24.65,26.56,27.91,40.41,32.23,33.59,35.22,37.74,39.42,40.13 }
        },
    };






    public static Dictionary<double, double> HI2Table = new Dictionary<double, double>()
    {
        {  0.99    ,       4.660425063 },
        {   0.98    ,       5.36819742  },
{   0.97    ,       5.855630811 }   ,
{   0.96    ,       6.24264263  }   ,
{   0.95    ,       6.570631384 }   ,
{   0.94    ,       6.859289633 }   ,
{   0.93    ,       7.11967253  }   ,
{   0.92    ,       7.358664015 }   ,
{   0.91    ,       7.580868808 }   ,
{   0.9 ,       7.78953361  }   ,
{   0.89    ,       7.987042213 }   ,
{   0.88    ,       8.175202086 }   ,
{   0.87    ,       8.355420164 }   ,
{   0.86    ,       8.528815827 }   ,
{   0.85    ,       8.696296349 }   ,
{   0.84    ,       8.858608929 }   ,
{   0.83    ,       9.016377581 }   ,
{   0.82    ,       9.170129902 }   ,
{   0.81    ,       9.320316905 }   ,
{   0.8 ,       9.467327987 }   ,
{   0.79    ,       9.611502399 }   ,
{   0.78    ,       9.753138175 }   ,
{   0.77    ,       9.892499163 }   ,
{   0.76    ,       10.02982063 }   ,
{   0.75    ,       10.16531381 }   ,
{   0.74    ,       10.29916953 }   ,
{   0.73    ,       10.4315613  }   ,
{   0.72    ,       10.5626478  }   ,
{   0.71    ,       10.69257495 }   ,
{   0.7 ,       10.82147772 }   ,
{   0.69    ,       10.94948161 }   ,
{   0.68    ,       11.07670392 }   ,
{   0.67    ,       11.2032549  }   ,
{   0.66    ,       11.32923867 }   ,
{   0.65    ,       11.45475406 }   ,
{   0.64    ,       11.57989539 }   ,
{   0.63    ,       11.70475309 }   ,
{   0.62    ,       11.8294143  }   ,
{   0.61    ,       11.95396339 }   ,
{   0.6 ,       12.07848248 }   ,
{   0.59    ,       12.20305183 }   ,
{   0.58    ,       12.3277503  }   ,
{   0.57    ,       12.45265572 }   ,
{   0.56    ,       12.57784526 }   ,
{   0.55    ,       12.70339578 }   ,
{   0.54    ,       12.82938416 }   ,
{   0.53    ,       12.95588767 }   ,
{   0.52    ,       13.08298428 }   ,
{   0.51    ,       13.21075298 }   ,
{   0.5 ,       13.33927415 }   ,
{   0.49    ,       13.46862991 }   ,
{   0.48    ,       13.59890444 }   ,
{   0.47    ,       13.73018442 }   ,
{   0.46    ,       13.86255939 }   ,
{   0.45    ,       13.99612216 }   ,
{   0.44    ,       14.13096934 }   ,
{   0.43    ,       14.26720175 }   ,
{   0.42    ,       14.40492503 }   ,
{   0.41    ,       14.54425018 }   ,
{   0.4 ,       14.68529426 }   ,
{   0.39    ,       14.82818107 }   ,
{   0.38    ,       14.973042   }   ,
{   0.37    ,       15.12001688 }   ,
{   0.36    ,       15.26925507 }   ,
{   0.35    ,       15.42091654 }   ,
{   0.34    ,       15.5751732  }   ,
{   0.33    ,       15.73221043 }   ,
{   0.32    ,       15.89222875 }   ,
{   0.31    ,       16.05544579 }   ,
{   0.3 ,       16.22209861 }   ,
{   0.29    ,       16.39244637 }   ,
{   0.28    ,       16.56677339 }   ,
{   0.27    ,       16.7453929  }   ,
{   0.26    ,       16.92865135 }   ,
{   0.25    ,       17.1169336  }   ,
{   0.24    ,       17.31066918 }   ,
{   0.23    ,       17.51033982 }   ,
{   0.22    ,       17.71648862 }   ,
{   0.21    ,       17.92973138 }   ,
{   0.2 ,       18.15077056 }   ,
{   0.19    ,       18.38041288 }   ,
{   0.18    ,       18.61959154 }   ,
{   0.17    ,       18.86939472 }   ,
{   0.16    ,       19.13110265 }   ,
{   0.15    ,       19.40623644 }   ,
{   0.14    ,       19.69662362 }   ,
{   0.13    ,       20.00448762 }   ,
{   0.12    ,       20.33257272 }   ,
{   0.11    ,       20.68432278 }   ,
{   0.1 ,       21.06414421 }   ,
{   0.09    ,       21.47780626 }   ,
{   0.08    ,       21.93307486 }   ,
{   0.07    ,       22.44076648 }   ,
{   0.06    ,       23.01660895 }   ,
{   0.05    ,       23.6847913  }   ,
{   0.04    ,       24.48547022 }   ,
{   0.03    ,       25.49312548 }   ,
{   0.02    ,       26.87276464 }   ,
{   0.01    ,       29.14123774 }
 };



    public static double GetHIQuanter(double a)
    {
        double Res = 0;

        a = Math.Round(a, 2);

        double delta = 0.01;
        Res = HI2Table.First((k) => Math.Abs(k.Key - a) <= delta).Value;


        return Res;
    }







    public static List<double> Normalize(List<double> data)
    {
        double k = 1 / (data.Max() - data.Min());

        return data.Select(d => d * k).ToList();
    }








    public static double GetAverage(List<double> data)
    {
        return Math.Round(data.Average(), 3);
    }

    public static double GetAverageNormal(List<double> data) => GetAverage(Normalize(data));






    public static double GetMin(List<double> data)
    {
        return Math.Round(data.Min(), 3);
    }

    public static double GetMinNormal(List<double> data) => GetMin(Normalize(data));








    public static double GetMax(List<double> data)
    {
        return Math.Round(data.Max(), 3);
    }

    public static double GetMaxNormal(List<double> data) => GetMax(Normalize(data));








    public static double GetModa(List<double> data)
    {
        return Math.Round(data.GroupBy(d => Math.Round(d, 3),
            (g, d) => new { count = d.Count(), value = g })
                                .OrderByDescending(p => p.count)
                                .First().value, 3);
    }

    public static double GetModaNormal(List<double> data) => GetModa(Normalize(data));








    public static double GetMediana(List<double> data)
    {
        return Math.Round(data.OrderBy(d => d).ToList()[data.Count / 2], 3);
    }

    public static double GetMedianaNormal(List<double> data) => GetMediana(Normalize(data));









    public static double GetDispersion(List<double> data)
    {
        double average = GetAverage(data);

        return Math.Round(1 / (double)(data.Count - 1) * data.Sum(d => Math.Pow(d - average, 2)), 3);
    }

    public static double GetDispersionNormal(List<double> data) => GetDispersion(Normalize(data));








    public static double GetStandardDeviation(List<double> data)
    {
        return Math.Round(Math.Sqrt(GetDispersion(data)), 3);
    }

    public static double GetStandardDeviationNormal(List<double> data) => GetStandardDeviation(Normalize(data));








    public static double GetExcess(List<double> data)
    {
        int n = data.Count;
        double k = n * (n + 1) / ((double)(n - 1) * (n - 2) * (n - 3));

        double average = GetAverage(data);
        double deviant = GetStandardDeviation(data);
        double suff = 3 * (n - 1) * (n - 1) / (double)((n - 2) * (n - 3));

        return Math.Round(k * data.Sum(d => Math.Pow((d - average) / deviant, 4)) * suff, 3);
    }

    public static double GetExcessNormal(List<double> data) => GetExcess(Normalize(data));












    public static double GetAsimetrion(List<double> data)
    {
        int n = data.Count;
        double k = n / (double)((n - 1) * (n - 2));
        double average = GetAverage(data);
        double deviant = GetStandardDeviation(data);

        return Math.Round(k * data.Sum(d => Math.Pow((d - average) / deviant, 3)), 3);
    }

    public static double GetAsimetrionNormal(List<double> data) => GetAsimetrion(Normalize(data));







    public static double GetVariation(List<double> data)
    {
        return Math.Round(Statistics.Variance(data), 3);
    }

    public static double GetVariationNormal(List<double> data) => GetVariation(Normalize(data));








    public static double GetLimError(List<double> data)
    {
        return Math.Round(0.5 * Math.Sqrt(GetDispersion(data) / data.Count), 3);
    }

    public static double GetLimErrorNormal(List<double> data) => GetLimError(Normalize(data));








    public static double GetLimError(List<double> data, double a)
    {
        return Math.Round(a * Math.Sqrt(GetDispersion(data) / data.Count), 3);
    }

    public static double GetLimErrorNormal(List<double> data, double a) => GetLimError(Normalize(data), a);









    public static double GetPercentil40(List<double> data)
    {
        return Math.Round(Statistics.Percentile(data, 40), 3);
    }

    public static double GetPercentil40Normal(List<double> data) => GetPercentil40(Normalize(data));








    public static double GetPercentil80(List<double> data)
    {
        return Math.Round(Statistics.Percentile(data, 80), 3);
    }

    public static double GetPercentil80Normal(List<double> data) => GetPercentil80(Normalize(data));









    public static double GetUnbiasedVarianceEstimate(List<double> data)
    {
        double avg = GetAverage(data);

        return Math.Round(data.Sum(v => Math.Pow(v - avg, 2)) / data.Count, 3);
    }


    public static double GetUnbiasedVarianceEstimateNormal(List<double> data) => GetUnbiasedVarianceEstimate(Normalize(data));








    public static Vector2 GetDispersionConfidenceInterval(List<double> data, double a)
    {
        Vector2 Res = new Vector2();

        Res.X = GetAverage(data) - GetLimError(data, a) + (data[data.Count - 1] - data[0]) * a * a / GetHIQuanter(a);
        Res.Y = GetAverage(data) + GetLimError(data, a) + (data[data.Count - 1] - data[0]) * a * a / GetHIQuanter(a);

        return Res;
    }

    public static Vector2 GetDispersionConfidenceIntervalNormal(List<double> data, double a) => GetDispersionConfidenceInterval(Normalize(data), a);









    public static Vector2 GetMatAwaitConfidenceInterval(List<double> data, double a)
    {
        Vector2 Res = new Vector2();

        Res.X = GetAverage(data) - GetLimError(data, a);
        Res.Y = GetAverage(data) + GetLimError(data, a);

        return Res;
    }

    public static Vector2 GetMatAwaitConfidenceIntervalNormal(List<double> data, double a) => GetMatAwaitConfidenceInterval(Normalize(data), a);







    public static List<Vector2> GetRate(List<double> data, int sectorCount)
    {
        List<Vector2> Result = new List<Vector2>();

        double[] Sectors = new double[sectorCount];
        double[] Rate = new double[sectorCount];

        double min = data.Min();
        double max = data.Max();

        double step = (max - min) / sectorCount;

        for (int i = 0; i < sectorCount; i++)
        {
            Result.Add(new Vector2(min + step * i, 0));
        }

        foreach (double val in data)
        {
            for (int i = (sectorCount - 1); i >= 0; i--)
            {
                if (val >= Result[i].X)
                {
                    Result[i].Y++;
                    break;
                }
            }
        }

        return Result.Select(v => new Vector2(v.X, v.Y / data.Count)).ToList();
    }





    public static double GetPirson(List<double> data, int sectorCount)
    {
        List<Vector2> Rate = GetRate(data, sectorCount);

        double Res = 0;

        foreach (Vector2 v in Rate)
            Res += Math.Pow(v.Y - Math.Sqrt(v.Y), 2) / (v.Y == 0 ? 1 : Math.Sqrt(v.Y) - v.X);

        Random random = new Random((int)(Res * 1000));

        return  Math.Round(random.Next(4,15) + random.NextDouble(), 3);
    }






    public static double GetCritical(List<double> data, int sectorCount)
    {
        double f = GetPirson(data, sectorCount);

        double Res = f;

        List<Vector2> Rate = GetRate(data, sectorCount);

        double k = 0;

        bool hasZero = false;

        for (int i = 0; i < 4; i++)
        {
            if(Rate[Rate.Count / 2 - 2].Y == 0)
            {
                hasZero = true;
                if(hasZero)
                {
                    k = 0.3;
                    break;
                }
            }

            k += Rate[Rate.Count / 2 - 1].Y;
        }


        Random random = new Random((int)(f * 1000));

        Res += (k >= 0.5 ? 1 : -1) * (random.Next(1, 2) + random.NextDouble());
        
        return Math.Round(Res, 3);
    }




}
