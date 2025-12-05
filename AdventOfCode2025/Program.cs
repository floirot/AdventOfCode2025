
Puzzle1();
//Puzzle2();

static void Puzzle1()
{
    string fileName = "C:\\Users\\florian.chmelar\\Desktop\\projects\\AdventOfCode2025\\AdventOfCode2025\\input1.txt";
    string[] lines = File.ReadAllLines(fileName);

    int presentNum = 50;
    int zeroCounterEndOnly = 0;

    foreach (string line in lines)
    {
        if (line.Length < 2)
            continue;

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

    Console.WriteLine($"zeroCounterEndOnly={zeroCounterEndOnly}");

    presentNum = 50;
    int startNum;
    int zeroCounterTotal = 0;

    foreach (string line in lines)
    {
        if (line.Length < 2)
            continue;

        startNum = presentNum;
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

        if (presentNum <= 0 && startNum != 0)
            zeroCounterTotal++;

        presentNum %= 100;

        if (presentNum < 0)
            presentNum += 100;
    }

    Console.WriteLine($"zeroCounterTotal={zeroCounterTotal}");
}

static void Puzzle2()
{
    string fileName = "C:\\Users\\florian.chmelar\\Desktop\\projects\\AdventOfCode2025\\AdventOfCode2025\\input2.txt";
    string text = File.ReadAllText(fileName);
    string[] idRanges = text.Split(',');
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




