int containsCount = 0;
int overlapCount = 0;

foreach (string line in System.IO.File.ReadLines("input.txt"))
{

    string[] pair = line.Split(',');

    int[] numbers = {0,0,0,0};
    numbers[0] = Int32.Parse(pair[0].Split('-')[0]);
    numbers[1] = Int32.Parse(pair[0].Split('-')[1]);
    numbers[2] = Int32.Parse(pair[1].Split('-')[0]);
    numbers[3] = Int32.Parse(pair[1].Split('-')[1]);


    //check if one pair totally encompasses another
    bool firstContainedInSecond = numbers[0] >= numbers[2] && numbers[1] <= numbers[3];
    bool secondContainedInFirst = numbers[0] <= numbers[2] && numbers[1] >= numbers[3];

    if (firstContainedInSecond || secondContainedInFirst)
    {
        containsCount++;
    }

    //check if pairs overlap at all
    bool firstBelowSecond = numbers[1] < numbers[2];
    bool secondBelowFirst = numbers[3] < numbers[0];

    if(!firstBelowSecond && !secondBelowFirst)
    {
        Console.WriteLine($"{numbers[0]} {numbers[1]} {numbers[2]} {numbers[3]}");
        overlapCount ++;
    }
}


Console.WriteLine(containsCount);
Console.WriteLine(overlapCount);
Console.ReadKey();