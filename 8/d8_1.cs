List<List<char>> map = new List<List<char>>();

void display(List<List<char>> map)
{
    foreach(List<char> row in map)
    {
        foreach(char val in row)
        {
            Console.Write(val);
        }
        Console.Write("\n");
    }
}

void displayVisibles(List<List<char>> map, HashSet<string> visibles)
{
    for (int y = 0; y < map.Count; y++)
    {
        for (int x = 0; x < map[y].Count; x++)
        {
            if (visibles.Contains($"{y} - {x}"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(map[y][x]);
            Console.ResetColor();
        }
        Console.Write("\n");
    }

}




//load values into map


foreach(string line in System.IO.File.ReadLines("input.txt"))
{
    map.Add(new List<char>());

    foreach(char c in line)
    {
        map.Last().Add(c);
    }
}



int h = map.Count;
int w = map[0].Count;

display(map);
Console.WriteLine();
Console.WriteLine($"{w} x {h}");

//check cols 

HashSet<string> visibles = new HashSet<string>();


void addVisible(List<List<char>> map, int row, int col, ref int max, ref HashSet<string> visibles)
{

    char c = map[row][col];

    int value = Convert.ToInt16(c);

    if(value > max)
    {
        visibles.Add($"{row} - {col}");
        max = value;
        return; 
    }
    else
    {
        return;
    }
}

int max = -1;

for(int col = 0; col < w; col++)
{
    //check visibility from top

    max = -1;

    for(int row = 0; row < h; row++)
    {
        addVisible(map, row, col, ref max, ref visibles);
    }

    max = -1;

    //check visibility from bottom
    for(int row = h-1; row >=0; row--)
    {
        addVisible(map, row, col, ref max, ref visibles);
    }
}


//check rows 

for(int row = 0; row < h; row++)
{
    max = -1;

    //check visibility from left
    for(int col = 0; col < w; col++) 
    {
        addVisible(map, row, col, ref max, ref visibles);
    }


    max = -1;

    //check visibility from right
    for(int col = w-1; col >=0; col--) 
    {
        addVisible(map, row, col, ref max, ref visibles);

    }
}

Console.WriteLine();
display(map);
Console.WriteLine();
displayVisibles(map,visibles);
Console.WriteLine();
Console.WriteLine($"{visibles.Count} visible trees");


Console.ReadKey();


