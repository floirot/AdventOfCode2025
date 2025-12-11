namespace AdventOfCode2025;

public class Puzzle3 : Puzzle
{
    public string[] Input { get; init; }

    public int ActiveBatteriesCount { get; init; }

    private char[] _joltage { get; set; }

    private long _joltagesSum { get; set; }

    public Puzzle3(string inputFileName, int turnedOnBatteriesCount) : base(inputFileName)
    {
        Input = File.ReadAllLines(inputFileName);
        ActiveBatteriesCount = turnedOnBatteriesCount;
        _joltage = PrepareJoltageField();
        _joltagesSum = 0;
    }

    protected override void SolveFirstPartInternal()
    {
        foreach(var bank in Input)
        {
            int batteryPosition = 0;

            while(batteryPosition < bank.Length)
            {
                var battery = bank[batteryPosition];

                if(battery > _joltage[0] && batteryPosition < (bank.Length - 1))
                {
                    _joltage[0] = battery;
                    _joltage[1] = '1';
                    batteryPosition++;
                    continue;
                }

                if(battery > _joltage[0] && batteryPosition == (bank.Length - 1))
                {
                    _joltage[1] = battery;
                    break;
                }

                if(battery > _joltage[1])
                {   
                    _joltage[1] = battery;
                    batteryPosition++;
                    continue; 
                }

                batteryPosition++;
            }

            long joltageNum = long.Parse(_joltage);
            _joltagesSum += joltageNum;

            // Console.WriteLine($"bank: {bank}");
            // Console.WriteLine($"joltage = {joltageNum}, joltagesSum(prelimary) = {JoltagesSum}");
        }

        Console.WriteLine($"Final sum of joltages = {_joltagesSum}");
    }

    protected override void SolveSecondPartInternal()
    {
        foreach(var bank in Input)
        {
            _joltagesSum += CountJoltagesSumForBank(bank);
            Console.WriteLine($"bank: {bank}, joltagesSum(prelimary) = {_joltagesSum}");
            ResetJoltageFromIndex(0);
        }

        Console.WriteLine($"Final sum of joltages = {_joltagesSum}");
    }

    protected override void Cleanup()
    {
        _joltage = PrepareJoltageField();
        _joltagesSum = 0;
    } 

    private char[] PrepareJoltageField()
    {
        char[] joltage = new char[ActiveBatteriesCount];
        for(int i = 0; i < ActiveBatteriesCount; i++)
        {
            joltage[i] = '1';
        }
        return joltage;
    }

    private long CountJoltagesSumForBank(string bank)
    {
        Console.WriteLine($"NEW BANK: {bank}");

        int batteryPositionInBank = 0;
        while(batteryPositionInBank < bank.Length)
        {
            SetJoltageFromPosition(batteryPositionInBank, bank);

            batteryPositionInBank++;
        }        

        return long.Parse(_joltage);
    }

    private void SetJoltageFromPosition(int batteryPositionInBank, string bank)
    {
        var battery = bank[batteryPositionInBank];
        int positionsTillEndOfBank = (bank.Length - 1) - batteryPositionInBank;

        Console.WriteLine($"battery={battery},batteryPositionInBank={batteryPositionInBank},positionsTillEndOfBank={positionsTillEndOfBank}");

        Console.WriteLine("Checking joltage...");
        for(int joltagePosition = 0; joltagePosition < ActiveBatteriesCount; joltagePosition++)
        { 
            int positionsTillEndOfJoltage = (ActiveBatteriesCount - 1) - joltagePosition;
            bool enoughSpaceForNewJoltageSet = positionsTillEndOfBank >= positionsTillEndOfJoltage;
            Console.WriteLine($"joltage={_joltage[joltagePosition]},joltagePosition={joltagePosition}");
            Console.WriteLine($"positionsTillEndOfJoltage={positionsTillEndOfJoltage},enoughSpaceForNewJoltageSet={enoughSpaceForNewJoltageSet}");

            if(battery > _joltage[joltagePosition] && enoughSpaceForNewJoltageSet)
            {
                _joltage[joltagePosition] = battery;
                ResetJoltageFromIndex(joltagePosition + 1);
                break;
            }
        }

        PrintJoltage();
        //Console.ReadLine();
    }

    private void ResetJoltageFromIndex(int index)
    {
        for(int i = index; i < ActiveBatteriesCount; i++)
        {
            _joltage[i] = '1';
        }
    }

    private void PrintJoltage()
    {
        Console.Write("Current Joltage: [");
        for(int i = 0; i < ActiveBatteriesCount; i++)
        {
            Console.Write($"{_joltage[i]}");
            if(i != ActiveBatteriesCount - 1)
                Console.Write(",");
        }
        Console.Write("]\n");
    }

    
}
