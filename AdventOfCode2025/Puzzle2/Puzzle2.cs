using AdventOfCode2025;

public class Puzzle2 : Puzzle
{
    public string Input { get; init; }
    public int StartNum { get; set; }

    public Puzzle2(string inputFileName, int startNum = 50) : base(inputFileName)
    {
        Input = File.ReadAllText(inputFileName);
    }

    protected override void SolveFirstPartInternal()
    {
        string[] idRanges = Input.Split(',');
        Console.WriteLine($"{idRanges}");
        long result = 0;

        foreach (string idRange in idRanges)
        {
            string[] parts = idRange.Split('-');
            string startId = parts[0];
            string endId = parts[1];
            long endIdNum = long.Parse(endId);

            Console.WriteLine($"Checking id range: startId={startId}, endId={endId}");
            Console.ReadLine();

            int startIdFigures = startId.Length;
            int endIdFigures = endId.Length;
            int currentFigures = startIdFigures;
            string currentStartId = startId;
            long currentStartIdNum = long.Parse(currentStartId);

            if (currentFigures % 2 != 0)
            {
                currentFigures++;
                currentStartIdNum = (long)Math.Pow(10, currentFigures - 1);
                currentStartId = currentStartIdNum.ToString();
            }

            if(currentFigures <= endIdFigures)
            {
                Console.WriteLine($"Current id range: currentStartId={currentStartId}, endIdNum={endIdNum}");

                string firstHalf = currentStartId.Substring(0, currentFigures / 2);
                long firstHalfNum = long.Parse(firstHalf);
                long currentNum;

                while (true)
                {
                    string secondHalf = firstHalf;

                    currentNum = long.Parse(firstHalf + secondHalf);

                    if (currentNum > endIdNum)
                    {
                        Console.WriteLine($"VALID ID: {currentNum} > endId({endIdNum})");
                        break;
                    }

                    if (currentNum < currentStartIdNum)
                    {
                        Console.WriteLine($"VALID ID: {currentNum} < endId({endIdNum})");
                    }
                    else
                    {
                        Console.WriteLine($"INVALID ID: {currentNum} < endId({endIdNum})");
                        result += currentNum;
                    }

                    firstHalfNum++;
                    firstHalf = firstHalfNum.ToString();
                }
            }

            Console.WriteLine($"current RESULT: {result}");
            Console.ReadLine();
        }
        
    }

    protected override void SolveSecondPartInternal()
    {
        throw new NotImplementedException();
    }
}
