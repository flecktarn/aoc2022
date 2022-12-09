string[] getQuestionInput()
{
    return File.ReadAllLines($"input.txt");
}

void d1q1()
{
    var inputLines = getQuestionInput();
    int max = 0;
    int sum = 0;
    foreach(var line in inputLines)
    {
        if (Int32.TryParse(line, out int number)) 
        {
            //line is a number, add to current sum
            sum += number;
        }
        else
        {
            //line is not a number, check if current sum is greater than max, set to zero
            if (sum > max)
            {
                max = sum;
            }
            sum = 0;
        }
    }
    Console.WriteLine(max);
}

void d1q2()
{
    var inputLines = getQuestionInput();
    int sum = 0;

    List<int> top3 = new List<int>();

    foreach(var line in inputLines)
    {
        if (Int32.TryParse(line, out int number)) 
        {
            //line is a number, add to current sum
            sum += number;
        }
        else
        {
            if(top3.Count < 3)
            {
                top3.Add(sum);
            }
            else
            {
                top3.Sort();
                if(top3.Count > 0)
                {
                    if (sum > top3[0])
                    {
                        top3[0] = sum;
                    }
                }
                else
                {
                    top3.Add(sum);
                }
            }

            sum = 0;
        }
    }
    Console.WriteLine(top3.Sum());
}

d1q2();