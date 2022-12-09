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

int sum = 0;

foreach(string line in System.IO.File.ReadLines("input.txt"))
{
    int size = line.Length;
    string part1 = line.Substring(0, size/2); 
    string part2 = line.Substring(size/2,size/2);

    var part1list = new HashSet<char>();

    foreach(char c in part1)
    {
        part1list.Add(c);
    }
    foreach(char c in part2)
    {
        if (part1list.Contains(c)){
            sum += getPriority(c);
            break;
        }
    }
}

Console.WriteLine(sum);
Console.ReadKey();