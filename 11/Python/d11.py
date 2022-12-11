from math import gcd

def getmonkeystartstate():

    with open('input.txt') as f:
        lines = f.readlines()
        
    monkeys = []

    class Monkey:
        monkeyID = 0
        items = []
        operation = "add"
        operationarg = 0
        testnum = 0
        truetarget = 0
        falsetarget = 0
        inspectioncount = 0


    for i in range(len(lines) // 7 + 1):
        print(f"monkey {i}")
        monkeyinput = lines[i*7:i*7+7]

        monkey = Monkey()

        monkey.monkeyID = i

        inputItems = []
        inputItems = (monkeyinput[1].split(":")[1]).split(',')

        for i in range(len(inputItems)):
            inputItems[i] = int(inputItems[i])

        monkey.items = []

        for item in inputItems:
            monkey.items.append(item)

        if "old * old" in monkeyinput[2]:
            monkey.operation = "square"
        
        elif "*" in monkeyinput[2]:
            monkey.operation = "times"
            monkey.operationarg = int(monkeyinput[2].split(" ")[-1])
        
        else:
            monkey.operation = "add"
            monkey.operationarg = int(monkeyinput[2].split(" ")[-1])


        monkey.testnum = int(monkeyinput[3].split(" ")[-1])

        monkey.truetarget = int(monkeyinput[4].split(" ")[-1])
        monkey.falsetarget = int(monkeyinput[5].split(" ")[-1])


        monkeys.append(monkey)
        del monkey


    
    return monkeys





def inspect(monkeys):

    for monkey in monkeys:

        while len(monkey.items) > 0:

            monkey.inspectioncount += 1

            item = monkey.items.pop(0)

            global lcm

            if monkey.operation == "add":
                item += monkey.operationarg

            elif monkey.operation == "times":
                item *= monkey.operationarg

            elif monkey.operation == "square":
                item *= item

            global part

            if part == 1:
                item //= 3


            
            if item % monkey.testnum == 0:
                monkeys[monkey.truetarget].items.append(item)
            else:
                monkeys[monkey.falsetarget].items.append(item)

            
monkeys = getmonkeystartstate()

testnumarray = []

for monkey in monkeys:
    print(monkey.monkeyID)
    print(monkey.items)
    print(monkey.operation)
    print(monkey.operationarg)
    print(monkey.testnum)
    print(monkey.truetarget)
    print(monkey.falsetarget)
    print()

    testnumarray.append(monkey.testnum)




print(testnumarray)
from math import gcd
lcm = 1
for i in testnumarray:
    lcm = lcm*i//gcd(lcm, i)
print(lcm)



print("p1")
part = 1

for i in range(20):
    inspect(monkeys)


inspectioncounts = []

for monkey in monkeys:
    print(f"monkey {monkey.monkeyID} inspected {monkey.inspectioncount}")
    inspectioncounts.append(monkey.inspectioncount)

inspectioncounts.sort()
print(f"top 2 product: {inspectioncounts[-1]} * {inspectioncounts[-2]} = {inspectioncounts[-1]*inspectioncounts[-2]}")

'''

print("p2")
part = 2

for i in range(10000):
    inspect(monkeys)


inspectioncounts = []

for monkey in monkeys:
    print(f"monkey {monkey.monkeyID} inspected {monkey.inspectioncount}")
    inspectioncounts.append(monkey.inspectioncount)

inspectioncounts.sort()
print(f"top 2 product: {inspectioncounts[-1]} * {inspectioncounts[-2]} = {inspectioncounts[-1]*inspectioncounts[-2]}")
'''
