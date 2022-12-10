
List<int> targets = new List<int>{ 20, 60, 100, 140, 180, 220};

void printState(int cycle, int register)
{
    Console.WriteLine($"{cycle.ToString("0000")} {register} {cycle * register}");
}

void printCurrentPixel(int cycle, int register)
{
    int pixelPosition = (cycle) % 40;

    if(pixelPosition == 1)
    {
        Console.Write('\n');
    }
    else if(pixelPosition % 5 == 0)
    {
        Console.Write(' ');
    }

    if(pixelPosition == register || pixelPosition == register+1 || pixelPosition == register+2)
    {
        Console.Write('#');
    }
    else
    {
        Console.Write(' ');
    }
}

void Solve(int part)
{
    int register = 1;
    int value;
    int cycle = 1;
    int p1sum = 0;
    Queue<int> instructions = new Queue<int>();

    IEnumerable<string> lines = File.ReadLines("input.txt");
    foreach (string line in lines)
    {

        if (targets.Contains(cycle))
        {
            if(part == 1)
            {
                printState(cycle, register);
            }
            p1sum += cycle * register;
        }


        if(part == 2)
        {
            printCurrentPixel(cycle, register);
        }

        value = 0;


        if (line.Contains("addx"))
        {
            value = Int32.Parse(line.Split(' ')[1]);
            instructions.Enqueue(value);
            cycle ++;


            if (targets.Contains(cycle))
            {
                if(part == 1)
                {
                    printState(cycle, register);
                }
                p1sum += cycle * register;
            }

            if(part == 2)
            {
                printCurrentPixel(cycle, register);
            }

        }


        if (instructions.Count > 0)
        {
            register += instructions.Dequeue();
        }

        cycle++;
    }

    if(part == 1)
    {
        Console.WriteLine($"PART 1 SUM = {p1sum}");
    }

}


//part 1
Solve(1);

//part 2
Solve(2);

Console.ReadKey();
