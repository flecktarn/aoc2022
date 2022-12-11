
List<Monkey> monkeys = new List<Monkey>();

int lineNo = 0;

//construct the monkeys

Monkey newMonkey = new Monkey();

Console.WriteLine("== PARSING ==");

foreach(string line in File.ReadLines("input.txt"))
{
    Console.WriteLine($"{lineNo.ToString("000")} {line}");

    switch(lineNo % 7)
    {
        case 0:
            //initialise new monkey
            newMonkey = new Monkey();
            newMonkey.MonkeyID = lineNo/7;
            newMonkey.MonkeyGroup = monkeys;
            break;

        case 1:
            //starting items
            string[] itemStrings = (line.Split(':')[1]).Split(',');
            int[] items = Array.ConvertAll(itemStrings, i => Int32.Parse(i));
            foreach(int item in items)
            {
                newMonkey.Items.Add(item);
            }
            break;

        case 2:
            //operation
            string[] words = line.Split(" ");
            string operationType = words[words.Length - 2];
            string operationNumberString = words[words.Length - 1];

            if (line.Contains("old * old"))
            {
                newMonkey.OperationType = OperationType.square;
            }
            else if (operationType == "+")
            {
                newMonkey.OperationType = OperationType.add;

                int operationNumber = Int32.Parse(operationNumberString);
                newMonkey.OperationNo = operationNumber;
            }
            else
            {
                newMonkey.OperationType = OperationType.multiply;

                int operationNumber = Int32.Parse(operationNumberString);
                newMonkey.OperationNo = operationNumber;
            }
            break;

        case 3:
            //test
            string testNoString = line.Split(' ')[5];
            int testNo = Int32.Parse(testNoString);
            newMonkey.TestNo = testNo;
            break;

        case 4:
            //throw to monkey if true
            string TrueTargetMonkeyString = line.Split(' ')[9];
            newMonkey.TrueTargetMonkey = Int32.Parse(TrueTargetMonkeyString);
            break;

        case 5:
            //throw to monkey if false and then add monkey to group
            string FalseTargetMonkeyString = line.Split(' ')[9];
            newMonkey.FalseTargetMonkey = Int32.Parse(FalseTargetMonkeyString);
            monkeys.Add(newMonkey);
            newMonkey = null;
            break;
    }

    lineNo++;
}

Console.WriteLine();
Console.WriteLine($"{monkeys.Count} monkeys parsed.");


foreach(Monkey monkey in monkeys)
{
    Console.WriteLine(monkey);
}


//20 rounds of monkey business
for(int i=0; i<20; i++)
{
    foreach(Monkey monkey in monkeys)
    {
        monkey.InspectItems();
    }
}


List<int> timesInspectedList = new List<int>();

foreach(Monkey monkey in monkeys)
{
    Console.WriteLine($"Monkey {monkey.MonkeyID} inspected items {monkey.NumberOfItemsInspected} times.");
    timesInspectedList.Add(monkey.NumberOfItemsInspected);
}

timesInspectedList.Sort();

int highest = timesInspectedList[timesInspectedList.Count - 1];
int secondHighest = timesInspectedList[timesInspectedList.Count - 2];

Console.WriteLine($"Top 2 product = {highest} * {secondHighest} = {highest*secondHighest}");



Console.ReadKey();

enum OperationType
{
    add,
    multiply,
    square
}

class Monkey 
{
    public List<Monkey> MonkeyGroup;

    public int MonkeyID;

    public List<int> Items = new List<int>();

    public int TestNo;

    public int OperationNo;

    public int TrueTargetMonkey;

    public int FalseTargetMonkey;

    public int NumberOfItemsInspected = 0;

    public override string ToString()
    {
        return $"Monkey: {MonkeyID}:\n  Starting items: {string.Join(",", Items.Select(n => n.ToString()).ToArray())}\n{OperationNo} {TestNo} {TrueTargetMonkey} {FalseTargetMonkey}";                
    }

    //true for add, false for multiply
    public OperationType OperationType;

    public void ThrowTo(int targetMonkey, int value)
    {
        Monkey target = MonkeyGroup[targetMonkey];
        target.Items.Add(value);
        Console.WriteLine($"  Item with worry level {value} is thrown to monkey {targetMonkey}");
    }

    public void InspectItems()
    {
        Console.WriteLine($"\nMonkey {MonkeyID}:");

        while (Items.Count > 0)
        {

            NumberOfItemsInspected++;

            //take item out of list
            int itemValue = Items[0];
            Items.RemoveAt(0);

            Console.WriteLine($" Monkey inspects an item with a worry level of {itemValue}");

            if(OperationType == OperationType.square)
            {
                itemValue *= itemValue;
                Console.WriteLine($"  Worry level is multiplied by itself to {itemValue}");
            }

            else if (OperationType == OperationType.multiply)
            {
                itemValue *= OperationNo;
                Console.WriteLine($"  Worry level is multiplied by {OperationNo} to {itemValue}");
            }

            else if(OperationType == OperationType.add)
            {
                itemValue += OperationNo;
                Console.WriteLine($"  Worry level increases by {OperationNo} to {itemValue}");
            }

            itemValue /= 3;
            Console.WriteLine($"  Monkey gets bored with item. Worry level is divided by 3 to {itemValue}");

            if(itemValue % TestNo == 0)
            {
                Console.WriteLine($"  Current worry level is divisible by {TestNo}");
                ThrowTo(TrueTargetMonkey, itemValue);
            }
            else
            {
                Console.WriteLine($"  Current worry level is not divisible by {TestNo}");
                ThrowTo(FalseTargetMonkey, itemValue);
            }
        }
    }
}

