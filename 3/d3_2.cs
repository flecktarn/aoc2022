int getPriority(char c)
{
    int code = (int)c;
    if(code >= 96)
    {
        //a-z
        return code - 96;
    }
    else
    {
        return code - 38;
    }
}

int index = 0;

string[] threeLines = {"","",""};

int sum = 0;

foreach(string line in System.IO.File.ReadLines("input.txt"))
{
    index++;

    threeLines[0] = threeLines[1];
    threeLines[1] = threeLines[2];
    threeLines[2] = line;


    if (index % 3 == 0) 
    {
        var firstRow = new HashSet<char>();
        var firstRowAndSecondRow = new HashSet<char>();

        foreach(char c in threeLines[0])
        {
            firstRow.Add(c);
        }

        foreach(char c in threeLines[1])
        {
            if (firstRow.Contains(c))
            {
                firstRowAndSecondRow.Add(c);
            }
        }

        foreach(char c in threeLines[2])
        {
            if (firstRowAndSecondRow.Contains(c))
            {
                sum += getPriority(c);
                break;
            }
        }
    }
}

Console.WriteLine(sum);
Console.ReadKey();