
List<Structure> structures = new List<Structure>();

foreach(string line in File.ReadLines("test.txt"))
{
    structures.Add(new Structure(line));
}
//structures.Add(new Structure("0,168 -> 1000,168"));

//find lowest point in any structure, after a grain of sand has passed this it will never rest



Map map = new Map(structures);

Console.Clear();

while (map.AddGrain())
{
    continue;
}

map.DrawWindow();
Console.WriteLine($"Grain count before stopping: {map.GrainPositions.Count-1}");
Console.WriteLine($"Grain count: {map.GrainPositions.Count}");

Console.ReadKey();

class Map
{

    public List<int> LastGrainPosition = new List<int>() { 500, 0 };

    public int DropOff = 0;

    public HashSet<string> GrainPositions = new HashSet<string>();

    public bool AddGrain()
    {
        //returns false if the grain falls forever

        Grain newGrain;

        
        newGrain = new Grain(LastGrainPosition[0], LastGrainPosition[1], this);
        Console.WriteLine($"{LastGrainPosition[0]},{LastGrainPosition[1]}");

        newGrain.Fall();

        if (newGrain.FallingForever)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool PointFree(int x, int y)
    {
        return !PointOccupied(x, y);
    }

    public bool PointOccupied(int x, int y)
    {
        return PointOccupiedByStructure(x, y) || PointOccupiedByGrain(x, y);
    }

    bool PointOccupiedByStructure(int x, int y)
    {
        foreach(Structure s in Structures)
        {
            if (s.Contains(x,y))
            {
                return true;
            }
        }
        return false;
    }

    bool PointOccupiedByGrain(int x, int y)
    {
        return GrainPositions.Contains($"{x},{y}");
    }

    public Map(List<Structure> structures)
    {
        this.Structures = structures;

        foreach(Structure s in structures)
        {
            int structureLowestPoint = s.LowestPoint;
            if (DropOff < structureLowestPoint) ;
            DropOff = structureLowestPoint;
        }
    }

    public List<Structure> Structures;

    public void DrawWindow(int x=490, int y=0, int w=20, int h=20)
    {
        int startX = x;
        int endX = x + w;
        int endY = y + h;

        Console.WriteLine();

        for( y = y ; y < endY ; y++)
        {
            Console.Write(y.ToString(" 000"));

            x = startX;
            for( x = x; x < endX ; x++)
            {
                if(x == 500 && y == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('+');
                    Console.ResetColor();

                }
                else if (PointOccupiedByGrain(x, y))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    if(y > 170)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write('o');
                    Console.ResetColor();
                }
                else if(PointOccupiedByStructure(x,y))
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(' ');
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(' ');
                    Console.ResetColor();
                }
            }
            Console.Write('\n');
        }
    }
}

class Structure
{
    public int LowestPoint
    {
        get
        {
            int maxDepth = Coords[0][1];

            foreach(var point in Coords)
            {
                int pY = point[1];
                if(maxDepth < pY)
                {
                    maxDepth = pY;
                }
            }

            return maxDepth;
        }
    }


    //number lies between two 1d coordinates
    static bool isBetween(int n, List<int> coords)
    {
        if (n >= coords.Min() && n <= coords.Max())
        {
            return true;
        }
        return false;
    }

    //point lies on a line in 2d space defined by two line points
    static bool isOnLine( int x, int y, List<int> lineStart, List<int> lineEnd)
    {
        return
            isBetween(x , new List<int>() { lineStart[0], lineEnd[0] }) &&
            isBetween(y, new List<int>() { lineStart[1], lineEnd[1] });
    }

    public bool Contains(int x, int y)
    {
        for(int i=0; i<Coords.Count-1; i++)
        {
            var lineStart = Coords[i];
            var lineEnd = Coords[i+1];

            if(isOnLine(x, y, lineStart, lineEnd))
            {
                return true;
            }
        }
        return false;
    }

    List<List<int>> Coords = new List<List<int>>();

    public Structure(string input)
    {
        Console.WriteLine($"New structure from line {input}");

        string[] points = input.Split("->");

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = points[i].Trim();
        }

        foreach(string point in points)
        {
            Console.WriteLine($"- Point {point}");
            string[] coords = point.Split(',');
            foreach(string coord in coords)
            {
                Coords.Add(new List<int>() { Int32.Parse(coords[0]), Int32.Parse(coords[1]) });
            }
        }
    }
}

class Grain
{
    Map map;
    public bool FallingForever = false;
    public bool AtRest = false;


    
    public int xPos; public int yPos;
    public int xLast; public int yLast;

    public void Fall()
    {
        while (!AtRest && !FallingForever)
        {

            if(yPos > 200)
            {
                FallingForever = true;
            }

            if (map.PointFree(xPos, yPos + 1))
            {
                yPos++;
                yLast = yPos - 1;
            }
            else if (map.PointFree(xPos - 1 , yPos + 1))
            {
                xPos--;
                yPos++;
                yLast = yPos - 1;
                xLast = xPos + 1;
            }

            else if (map.PointFree(xPos + 1 , yPos + 1)){
                xPos++;
                yPos++;
                yLast = yPos - 1;
                xLast = xPos - 1;
            }
            else
            {
                AtRest = true;
                map.GrainPositions.Add($"{xPos},{yPos}");
                map.LastGrainPosition = new List<int>() { xLast, yLast };
            }
        }
    }

    public Grain(int startX, int startY, Map map)
    {
        xPos = startX;
        yPos = startY;
        xLast = startX;
        yLast = startY;
        this.map = map;
    }
}

