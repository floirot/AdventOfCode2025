using AdventOfCode2025;

public class Puzzle1 : Puzzle
{
    public string[] Input { get; init; }
    public int StartNum { get; set; }

    public Puzzle1(string inputFileName, int startNum = 50) : base(inputFileName)
    {
        Input = File.ReadAllLines(inputFileName);
        StartNum = startNum;
    }

    protected override void SolveFirstPartInternal()
    {
        int presentNum = StartNum;
        int zeroCounterEndOnly = 0;

        foreach(string line in Input)
        {
            bool isRight = line[0] == 'R';
            int moveNum = int.Parse(line.Substring(1));

            if (isRight)
                presentNum += moveNum;
            else
                presentNum -= moveNum;

            presentNum %= 100;

            if (presentNum == 0)
                zeroCounterEndOnly++;
        }

        Console.WriteLine($"Result of the first part = {zeroCounterEndOnly}");
    }

    protected override void SolveSecondPartInternal()
    {
        int presentNum = StartNum;
        int zeroCounterTotal = 0;

        foreach (string line in Input)
        {
            if (line.Length < 2)
                continue;

            int startNumThisRound = presentNum;
            bool isRight = line[0] == 'R';
            int moveNum = int.Parse(line.Substring(1));

            if (isRight)
            {
                presentNum += moveNum;
                zeroCounterTotal += presentNum / 100;
            }
            else
            {
                presentNum -= moveNum;
                zeroCounterTotal += Math.Abs(presentNum) / 100;
            }

            if (presentNum <= 0 && startNumThisRound != 0)
                zeroCounterTotal++;

            presentNum %= 100;

            if (presentNum < 0)
                presentNum += 100;
        }

        Console.WriteLine($"Result of the second part = {zeroCounterTotal}");
    }

    protected override void Cleanup()
    {
        throw new NotImplementedException();
    }
}
