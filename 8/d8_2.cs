class Program
{
    class Tree
    {
        public Tree(int row, int col, int Height, List<List<Tree>> forest)
        {
            this.Row = row;
            this.Col = col;
            this.Height = Height;
            this.Forest = forest;
        }

        //the height of the tree
        int Height;

        //the row index of the tree within the forest 
        int Row;

        //the column index of the tree within the forest
        int Col;

        //the forest the tree is contained in
        List<List<Tree>> Forest;

        public override string ToString()
        {
            return $"[({Col},{Row}) h={Height}]";
        }

        Tree? TopNeighbor
        {
            get
            {
                //null if tree on top edge
                if (Row == 0)
                {
                    return null;
                }
                else
                {
                    return Forest[Row - 1][Col];
                }
            }
        }
        Tree? BottomNeighbor
        {
            get
            {
                //null if tree on bottom edge
                if(Row == Forest.Count - 1) 
                { 
                    return null; 
                }
                else
                {
                    return Forest[Row + 1][Col];
                }
            }
        }
        Tree? LeftNeighbor
        {
            get
            {
                //null if tree on left edge
                if(Col == 0)
                {
                    return null;
                }
                else
                {
                    return Forest[Row][Col - 1];
                }
            }
        }
        Tree? RightNeighbor
        {
            get
            {
                //null if tree on right edge
                if(Col == Forest[0].Count - 1)
                {
                    return null;
                }
                else
                {
                    return Forest[Row][Col + 1];
                }
            }
        }


        public int Score
        {
            get
            {
                return LeftVis * RightVis * TopVis * BottomVis;
            }
        }

        int LeftVis 
        {
            get
            {
                int count = 0;

                Tree? left = this.LeftNeighbor;

                while(left != null)
                {
                    count ++;
                    if(left.Height >= this.Height)
                    {
                        break;
                    }
                    else
                    {
                        left = left.LeftNeighbor;
                    }
                }
                return count;
            }
        }

        int RightVis 
        {
            get
            {
                int count = 0;

                Tree? right = this.RightNeighbor;

                while(right != null)
                {
                    count ++;
                    if(right.Height >= this.Height)
                    {
                        break;
                    }
                    else
                    {
                        right = right.RightNeighbor;
                    }
                }
                return count;
            }
        }

        int TopVis 
        {
            get
            {
                int count = 0;

                Tree? top = this.TopNeighbor;

                while(top != null)
                {
                    count ++;
                    if(top.Height >= this.Height)
                    {
                        break;
                    }
                    else
                    {
                        top = top.TopNeighbor;
                    }
                }
                return count;
            }
        }


        int BottomVis 
        {
            get
            {
                int count = 0;

                Tree? bottom = this.BottomNeighbor;

                while(bottom != null)
                {
                    count ++;
                    if(bottom.Height >= this.Height)
                    {
                        break;
                    }
                    else
                    {
                        bottom = bottom.BottomNeighbor; 
                    }
                }
                return count;
            }
        }
    }


    //populate forest

    public static void Main()
    {

        List<List<Tree>> forest = new List<List<Tree>>();

        int row = 0;
        int col = 0;

        //populate forest with tree objects
        foreach (string line in System.IO.File.ReadLines("input.txt"))
        {
            col = 0;

            forest.Add(new List<Tree>());

            foreach (char c in line)
            {
                int height = (int)(char.GetNumericValue(c));
                forest[forest.Count - 1].Add(new Tree(row , col, height, forest));

                col++;
            }

            row++;
        }

        int max = 0;

        foreach(List<Tree> treeLine in forest)
        {
            foreach(Tree tree in treeLine)
            {
                int score = tree.Score;
                if(score > max)
                {
                    max = score;
                }
            }
        }

        Console.WriteLine(max);

        Console.ReadKey();


    }

}