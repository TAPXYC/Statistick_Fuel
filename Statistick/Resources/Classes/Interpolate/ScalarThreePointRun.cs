
class ScalarThreePointRun
{
    private double[] RightPart;

    private int RowCount;
    private int ColumCount;

    private double[] MainDiagonal;
    private double[] UpSecondaryDiagonal;
    private double[] DownSecondaryDiagonal;
    private double[] alpha;
    private double[] beta;

    public ScalarThreePointRun(double[,] Matrix, double[] RightPart)
    {
        this.RightPart = RightPart.Clone() as double[];

        RowCount = Matrix.GetLength(0);
        ColumCount = Matrix.GetLength(1);

        MainDiagonal = new double[RowCount];
        UpSecondaryDiagonal = new double[RowCount];
        DownSecondaryDiagonal = new double[RowCount];

        for (int i = 0; i < ColumCount; i++)
        {
            MainDiagonal[i] = Matrix[i, i];

            if (i + 1 == ColumCount)
                break;

            DownSecondaryDiagonal[i + 1] = Matrix[i + 1, i];
            UpSecondaryDiagonal[i] = Matrix[i, i + 1];
        }
    }


    private void CalcAlphaBeta()
    {
        alpha = new double[RowCount];
        beta = new double[RowCount];

        alpha[0] = -UpSecondaryDiagonal[0] / MainDiagonal[0];
        beta[0] = RightPart[0] / MainDiagonal[0];

        for (int i = 1; i < ColumCount; i++)
        {
            alpha[i] = -UpSecondaryDiagonal[i] / (DownSecondaryDiagonal[i] * alpha[i - 1] + MainDiagonal[i]);

            beta[i] = (RightPart[i] - DownSecondaryDiagonal[i] * beta[i - 1]) / (DownSecondaryDiagonal[i] * alpha[i - 1] + MainDiagonal[i]);
        }
    }    
    
    
    
    private double[] ReverceStep()
    {
        double[] Result = new double[RowCount];

        int n = ColumCount - 1;
        Result[n] = (RightPart[n] - DownSecondaryDiagonal[n] * beta[n - 1]) / (DownSecondaryDiagonal[n] * alpha[n - 1] + MainDiagonal[n]);

        for (int i = n - 1; i >= 0; i--)
            Result[i] = alpha[i] * Result[i + 1] + beta[i];

        return Result;
    }




    public double[] SolveMatrix()
    {
        CalcAlphaBeta();
        return ReverceStep();
    }
}
