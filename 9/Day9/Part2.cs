namespace Day9
{
    class Part2
    {
        static bool Touching(int[] p1, int[] p2)
        {
            int dx = Math.Abs(p1[0] - p2[0]);
            int dy = Math.Abs(p1[1] - p2[1]);

            if(dx <=1 && dy <= 1)
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

            if(dX > 0)
            {
                moveX = 1;
            }
            else if(dX < 0)
            {
                moveX = -1;
            }
            else
            {
                moveX = 0;
            }

            if(dY > 0)
            {
                moveY = 1;
            }
            else if(dY < 0)
            {
                moveY = -1;
            }
            else
            {
                moveY = 0;
            }

            return new int[] {moveX,moveY};
        }


        static void DrawPosition(List<int[]> rope)
        {
            for(int y = 20; y >= -5; y--)
            {
                for(int x = -14; x < 15; x++)
                {

                    char display = '.';

                    for(int i=0; i<rope.Count; i++)
                    {
                        if(x == rope[i][0] && y == rope[i][1])
                        {
                            display = $"{i}"[0];
                        }
                    }

                    if(x==0 && y == 0)
                    {
                        display = 's';
                    }

                    Console.Write(display);
                }
                Console.Write('\n');
            }
            Console.Write('\n');
        }

        public static void Solve()
        {

            List<int[]> rope = new List<int[]>();

            for(int i=0; i<10; i++)
            {
                rope.Add(new int[] { 0, 0 });
            }

            HashSet<string> visited = new HashSet<string>();

            Console.WriteLine("== Initial State ==");

            DrawPosition(rope);

            visited.Add($"{rope[9][0]},{rope[9][1]}");

            foreach (string line in System.IO.File.ReadLines("input.txt"))
            {

                char direction = line.Split(' ')[0][0];
                int moveCount = Int32.Parse(line.Split(' ')[1]);
                Console.WriteLine($"== {direction} {moveCount} ==");


                for (int move = 0; move < moveCount; move++)
                {
                    //move head

                    switch (direction)
                    {
                        case 'U':
                            rope[0][1] += 1;
                            break;

                        case 'R':
                            rope[0][0] += 1;
                            break;

                        case 'L':
                            rope[0][0] -= 1;
                            break;

                        case 'D':
                            rope[0][1] -= 1;
                            break;
                    }

                    //all knots in the rope chase one another
                    for(int knotIndex = 1; knotIndex <= 9; knotIndex++)
                    {
                        if (!Touching(rope[knotIndex], rope[knotIndex-1]))
                        {
                            int[] tailDirection = NextStep(rope[knotIndex], rope[knotIndex-1]);

                            rope[knotIndex][0] += tailDirection[0];
                            rope[knotIndex][1] += tailDirection[1];
                        }
                    }

                    visited.Add($"{rope[9][0]},{rope[9][1]}");
                }
                //DrawPosition(rope);


            }
            Console.WriteLine(visited.Count);

            Console.ReadKey();
        }

    }
}
