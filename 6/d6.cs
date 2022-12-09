int streakLength = 14;

var buffer = new List<char>();

for(int i=0; i<streakLength; i++)
{
    buffer.Add('_');
}

char c;

int count = 0;

bool allUnique(List<char> chars)
{
    HashSet<char> set = new HashSet<char>();
    foreach(char c in chars)
    {
        if (set.Contains(c))
        {
            return false;
        }
        set.Add(c);
    }
    return true;
}

var reader = new StreamReader("input.txt");

while (!reader.EndOfStream)
{

    count++;

    c = (char)reader.Read();

    //shift last4 

    for(int i=0; i<buffer.Count-1; i++)
    {
        buffer[i] = buffer[i + 1];
    }
    buffer[buffer.Count-1] = c;

    Console.Write(count);
    Console.Write(" ");
    Console.Write(string.Concat(buffer));
    Console.Write("\n");
    if (allUnique(buffer) && count >= buffer.Count)
    {
        break;
    }
}

Console.ReadKey();