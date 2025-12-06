using System.Diagnostics;

namespace AdventOfCode2025;

public abstract class Puzzle 
{
    public string InputFileName { get; set; }

    public Puzzle(string inputFileName)
    {
        InputFileName = inputFileName;
    }

    public void SolveFirstPart()
    {
        ExecuteWithTiming(SolveFirstPartInternal);
    }

    public void SolveSecondPart()
    {
        ExecuteWithTiming(SolveSecondPartInternal);
    }

    protected abstract void SolveFirstPartInternal();

    protected abstract void SolveSecondPartInternal();

    private void ExecuteWithTiming(Action solveAction)
    {
        var stopwatch = Stopwatch.StartNew();
        solveAction();
        stopwatch.Stop();
        Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds}ms\n");
    }
}
