using AdventOfCode2025;

public class Puzzle3 : Puzzle
{
    public string[] Input { get; set; }

    public Puzzle3(string inputFileName) : base(inputFileName)
    {
        Input = File.ReadAllLines(inputFileName);
    }

    protected override void SolveFirstPartInternal()
    {
        int joltagesSum = 0;

        foreach(var bank in Input)
        {
            char[] joltage = ['1','1'];
            int batteryPosition = 0;

            while(batteryPosition < bank.Length)
            {
                var battery = bank[batteryPosition];

                if(battery > joltage[0] && batteryPosition < (bank.Length - 1))
                {
                    joltage[0] = battery;
                    joltage[1] = '1';
                    batteryPosition++;
                    continue;
                }

                if(battery > joltage[0] && batteryPosition == (bank.Length - 1))
                {
                    joltage[1] = battery;
                    break;
                }

                if(battery > joltage[1])
                {   
                    joltage[1] = battery;
                    batteryPosition++;
                    continue; 
                }

                batteryPosition++;
            }

            int joltageNum = int.Parse(joltage);
            joltagesSum += joltageNum;

            Console.WriteLine($"bank: {bank}");
            Console.WriteLine($"joltage = {joltageNum}, joltagesSum(prelimary) = {joltagesSum}");
        }

        Console.WriteLine($"Final sum of joltages = {joltagesSum}");
    }

    protected override void SolveSecondPartInternal()
    {
        throw new NotImplementedException();
    }
}
