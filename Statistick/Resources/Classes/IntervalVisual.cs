using System;
using System.Collections.Generic;
using System.Windows.Controls;

public class IntervalVisual
{
    ProgressBar Left;
    ProgressBar Right;
    TextBlock Info;
    List<double> Data;
    Func<List<double>, double, Vector2> CalculateFunc;

    public IntervalVisual(ProgressBar l, ProgressBar r, TextBlock info, List<double> data, double startAlpha,
        Func<List<double>, double, Vector2> calculateFunc)
    {
        Left = l;
        Right = r;
        Info = info;

        Data = data;
        CalculateFunc = calculateFunc;

        ChangeAlpha(startAlpha);
    }


    public void ChangeAlpha(double a)
    {
        a = 1 - a;
        Vector2 Interval = CalculateFunc(Data, a);

        Left.Value = Left.Minimum + Left.Maximum - Interval.X;
        Right.Value = Interval.Y;

        Info.Text = $"({Math.Round(Interval.X, 2)} ; {Math.Round(Interval.Y, 2)})";
    }
}