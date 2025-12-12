namespace AdventOfCode2025;

public class Puzzle4 : Puzzle
{
    public string[] Input { get; set; }

    public int MaxAdjacentRolls { get; init; }

    public Puzzle4(string inputFileName, int maxAdjacentRolls) : base(inputFileName)
    {
        Input = File.ReadAllLines(inputFileName);
        MaxAdjacentRolls = maxAdjacentRolls;
    }

    protected override void SolveFirstPartInternal()
    {
        int accessibleRollOfPaperCount = 0;

        for(int y = 0; y < Input.Length; y++)
        {
            for(int x = 0; x < Input[y].Length; x++)
            {
                bool isRollOfPaperAccessible = IsRollOfPaperAccessible(x, y);
                if (isRollOfPaperAccessible)
                    accessibleRollOfPaperCount++;
            }
        }

        Console.WriteLine($"Final count of accessibleRollOfPapers: {accessibleRollOfPaperCount}");
    }

    protected override void SolveSecondPartInternal()
    {
        int removedRollOfPaperTotalCount = 0;
        int removedRollOfPaperThisRoundCount;

        do
        {
            removedRollOfPaperThisRoundCount = 0;
            for(int y = 0; y < Input.Length; y++)
            {
                char[] row = Input[y].ToCharArray();

                for(int x = 0; x < Input[y].Length; x++)
                {
                    bool isRollOfPaperAccessible = IsRollOfPaperAccessible(x, y);
                    if (isRollOfPaperAccessible)
                    {
                        removedRollOfPaperTotalCount++;
                        removedRollOfPaperThisRoundCount++;
                        row[x] = 'x';
                    }
                }

                Input[y] = new string(row);
            }

        } while(removedRollOfPaperThisRoundCount > 0 );

        

        Console.WriteLine($"Final count of removed rolls of paper: {removedRollOfPaperTotalCount}");
    }

    private bool IsRollOfPaperAccessible(int x, int y)
    {
        char currentPosition = Input[y][x];
        int adjacentRollOfPaperCount = 0;

        if (currentPosition != '@')
        {
            return false;
        }

        if(IsRollOfPaper(x+1,y))
            adjacentRollOfPaperCount++;

        if(IsRollOfPaper(x+1,y+1))
            adjacentRollOfPaperCount++;
        
        if(IsRollOfPaper(x+1,y-1))
            adjacentRollOfPaperCount++;

        if(IsRollOfPaper(x,y+1))
            adjacentRollOfPaperCount++;

        if(IsRollOfPaper(x,y-1))
            adjacentRollOfPaperCount++;

        if(IsRollOfPaper(x-1,y))
            adjacentRollOfPaperCount++;

        if(IsRollOfPaper(x-1,y+1))
            adjacentRollOfPaperCount++;
        
        if(IsRollOfPaper(x-1,y-1))
            adjacentRollOfPaperCount++;

        if(adjacentRollOfPaperCount>MaxAdjacentRolls)
        {
            return false;
        }
        
        return true;
    }

    private bool IsRollOfPaper(int x, int y)
    {
        if(!AreCoordinatesValid(x,y))
        {
            return false;
        }

        if(Input[y][x] == '@')
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool AreCoordinatesValid(int x, int y)
    {
        if(y < 0 || y >= Input.Length)
        {
            return false;
        }

        if(x < 0 || x >= Input[y].Length)
        {
            return false;
        }

        return true;
    }

}