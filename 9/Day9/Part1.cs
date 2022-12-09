class Part1
{

    static bool Touching(int[] p1, int[] p2)
    {
        int dx = Math.Abs(p1[0] - p2[0]);
        int dy = Math.Abs(p1[1] - p2[1]);

        if (dx <= 1 && dy <= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    static int[] NextStep(int[] position, int[] target)
    {
        //vector tail should travel in to minimize distance to head

        int currentX = position[0];
        int currentY = position[1];

        int targetX = target[0];
        int targetY = target[1];

        int dX = targetX - currentX;
        int dY = targetY - currentY;

        int moveX = 0;
        int moveY = 0;

        if (dX > 0)
        {
            moveX = 1;
        }
        else if (dX < 0)
        {
            moveX = -1;
        }
        else
        {
            moveX = 0;
        }

        if (dY > 0)
        {
            moveY = 1;
        }
        else if (dY < 0)
        {
            moveY = -1;
        }
        else
        {
            moveY = 0;
        }

        return new int[] { moveX, moveY };
    }

    static void DrawPosition(int[] head, int[] tail)
    {
        for (int y = 6; y >= 0; y--)
        {
            for (int x = 0; x < 6; x++)
            {
                if (x == head[0] && y == head[1])
                {
                    Console.Write('H');
                }
                else if (x == tail[0] && y == tail[1])
                {
                    Console.Write('T');
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.Write('\n');
        }
        Console.Write('\n');
    }

    public static void Solve()
    {
        int[] head = { 0, 0 };
        int[] tail = { 0, 0 };

        HashSet<string> visited = new HashSet<string>();

        Console.WriteLine("== Initial State ==");
        DrawPosition(head, tail);

        visited.Add($"{tail[0]},{tail[1]}");

        foreach (string line in System.IO.File.ReadLines("input.txt"))
        {
            char direction = line.Split(' ')[0][0];
            int count = Int32.Parse(line.Split(' ')[1]);
            Console.WriteLine($"== {direction} {count} ==");


            for (int i = 0; i < count; i++)
            {

                switch (direction)
                {
                    case 'U':
                        head[1] += 1;
                        break;

                    case 'R':
                        head[0] += 1;
                        break;

                    case 'L':
                        head[0] -= 1;
                        break;

                    case 'D':
                        head[1] -= 1;
                        break;
                }

                //if the head and tail are not touching, move the tail towards the head
                if (!Touching(head, tail))
                {
                    int[] tailDirection = NextStep(tail, head);

                    tail[0] += tailDirection[0];
                    tail[1] += tailDirection[1];
                    visited.Add($"{tail[0]},{tail[1]}");
                }


                DrawPosition(head, tail);
            }


        }
        Console.WriteLine(visited.Count);

        Console.ReadKey();
    }

}
