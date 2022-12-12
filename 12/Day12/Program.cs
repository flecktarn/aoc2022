int getHeight(char c)
{
    return (int)(c - 96);
}

void printMap(List<List<Square>> map, bool compact = true)
{
    Console.Clear();
    Console.Write('\n');

    foreach(List<Square> row in map)
    {
        foreach(Square cell in row)
        {

            if(cell.StepsToReach != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            if (cell.IsStart)
            {
                Console.BackgroundColor= ConsoleColor.DarkBlue;
            }

            if (cell.IsEnd)
            {
                Console.BackgroundColor= ConsoleColor.Red;
            }

            if (!compact)
            {
                Console.Write(cell);
                Console.Write(' ');
            }
            else
            {
                if (cell.IsStart)
                {
                    Console.Write('S');
                }
                else if (cell.IsEnd)
                {
                    Console.Write('E');
                }
                else
                {
                    Console.Write((char)(cell.Height + 96));
                }
            }

            Console.ResetColor();
        }

        if (!compact)
        {
            Console.Write('\n');
        }

        Console.Write('\n');
    }
    Console.Write('\n');
}

List<List<Square>> map = new List<List<Square>>();

Square? start = null;
Square? end = null;

int parsedRow = 0;
int parsedCol = 0;


foreach(string row in File.ReadLines("input.txt"))
{

    parsedCol = 0;

    List<Square> newRow = new List<Square>();
    foreach(char c in row)
    {
        if(c == 'S')
        {
            start = new Square(map, parsedCol, parsedRow, 1, true, false);
            newRow.Add(start);
        }
        else if (c == 'E')
        {
            end = new Square(map, parsedCol, parsedRow, 26, false, true);
            newRow.Add(end);
        }
        else
        {
            newRow.Add(new Square(map, parsedCol, parsedRow, getHeight(c)));
        }
        parsedCol++;
    }
    map.Add(newRow);
    parsedRow ++;
}


//let us explore!

void Part1()
{
    List<Square> routes = new List<Square>();


    routes.Add(start);


    bool endFound = false;

    while (routes.Count > 0 && endFound == false)
    {
        var nextSteps = new List<Square>();

        foreach(Square square in routes)
        {
            foreach(Square potentialNext in square.Surroundings)
            {
                if (potentialNext.StepsToReach == null && (square.Height + 1 >= potentialNext.Height))
                {
                    potentialNext.StepsToReach = square.StepsToReach + 1;


                    if (potentialNext.IsEnd)
                    {
                        Console.WriteLine($"REACHED ENDING SQUARE ({potentialNext.StepsToReach} STEPS)");
                        endFound = true;
                    }

                    nextSteps.Add(potentialNext);
                }
            }
        }

        routes = nextSteps;
    }


}

void Part2()
{
    List<Square> routes = new List<Square>();


    end.StepsToReach = 0;
    routes.Add(end);


    bool endFound = false;

    while (routes.Count > 0 && endFound == false)
    {
        var nextSteps = new List<Square>();

        foreach(Square square in routes)
        {
            foreach(Square potentialNext in square.Surroundings)
            {
                if (potentialNext.StepsToReach == null && (potentialNext.Height == square.Height - 1 || potentialNext.Height == square.Height || potentialNext.Height > square.Height))
                {
                    potentialNext.StepsToReach = square.StepsToReach + 1;


                    if (potentialNext.Height == 1)
                    {
                        Console.WriteLine($"REACHED 'a' SQUARE ({potentialNext.StepsToReach} STEPS)");
                        endFound = true;
                    }

                    nextSteps.Add(potentialNext);
                }
            }
        }

        routes = nextSteps;
    }



}

//part1 and part2 must be run exclusively on different runs of the program because im lazy and cant be bothered to make a function for the starting state

//Part1();
Part2();

Console.ReadKey();





class Square
{

    public int Height;
    public bool IsStart;
    public bool IsEnd;
    public int? StepsToReach;
    public bool Active = false;
    public int X;
    public int Y;

    private List<List<Square>> Map;

    public List<Square> Surroundings
    {
        get
        {
            List<Square> surrounds = new List<Square>();

            bool spaceAbove = Y > 0;
            bool spaceBelow = Y < Map.Count - 1;
            bool spaceLeft = X > 0;
            bool spaceRight = X < Map[0].Count - 1;

            //top
            if(spaceAbove)
            {
                surrounds.Add(Map[Y - 1][X]);
            }


            //bottom
            if(spaceBelow)
            {
                surrounds.Add(Map[Y + 1][X]);
            }

            //left
            if (spaceLeft)
            {
                surrounds.Add(Map[Y][X - 1]);
            }

            //right
            if (spaceRight)
            {
                surrounds.Add(Map[Y][X + 1]);
            }

            return surrounds;
        }
    }

    public Square(List<List<Square>> map, int x, int y, int height, bool isStart = false, bool isEnd = false)
    {

        Map = map;

        X = x;
        Y = y;


        Height = height;

        if (isStart)
        {
            StepsToReach = 0;
        }

        IsStart = isStart;
        IsEnd = isEnd;
    }

    public override string ToString()
    {
        char[] brackets = { ' ', ' ' };

        if (IsStart)
        {
            brackets = new char[] {'(',')'};
        }
        else if (IsEnd)
        {
            brackets = new char[] {'>','<'};
        }

        return $"{brackets[0]}{Height.ToString("00")}{brackets[1]}";
    }
}

