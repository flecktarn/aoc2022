Stack<char> tempStack = new Stack<char>();

for (int i = 0; i < times; i++)
{
  moveBox(stacks[source - 1],tempStack);
}
for (int i = 0; i < times; i++)
{
    moveBox(tempStack, stacks[destination - 1]);
}