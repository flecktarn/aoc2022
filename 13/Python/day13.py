import time

file = open('test.txt','r')

lines = file.readlines()

    
def compare(left, right):


    print(f"- Compare {left} vs {right}")


    #if left and right are ints, just do the standard comparison
    if isinstance(left,int) and isinstance(right,int):

        if right < left:
            print(f"Right is smaller, so inputs are in the WRONG order.")
            return -1 

        elif left < right:
            print(f"Left is smaller, so inputs are in the RIGHT order.")
            return 1
        
        else:
            return 0

    else:
        #arrays
        
        if isinstance(left, int):
            left = [left]
            print(f"- Compare {left} vs {right}")
            print(f"- Mixed types; convert left to {left} and retry comparison")

        if isinstance(right, int):
            right = [right]
            print(f"- Compare {left} vs {right}")
            print(f"- Mixed types; convert right to {right} and retry comparison")


        while True:

            if len(left) == 0 and len(right) == 0:
                return 0

            if len(left) == 0:
                print(f"- Left ran out of items, so inputs are in the RIGHT order.")
                return 1

            if len(right) == 0:
                print(f"- Right ran out of items, so inputs are in the WRONG order.")
                return -1
            
            leftfirst = left.pop(0)
            rightfirst = right.pop(0)

            firstcomparison = compare(leftfirst,rightfirst)

            if firstcomparison != 0:
                return firstcomparison







    
p1arrays = []

for index,line in enumerate(lines):
    if line.strip():

        ancestors = []

        current_item = []

        parsed_int_string = ""

        for c in line:

            if c.isdigit():
                parsed_int_string += c

            elif len(parsed_int_string):
                newint = int(parsed_int_string)
                parsed_int_string = ""
                current_item.append(newint)
            
            if c == '[':
                #move down into new list
                current_item.append([])
                ancestors.append(current_item)
                current_item = current_item[-1]
            
            elif c == ']':
                #move back up into parent
                current_item = ancestors.pop()


        p1arrays.append(current_item)



from copy import deepcopy

p2arrays = deepcopy(p1arrays)

rightitems = []

for i in range(0, len(p1arrays),2):
    leftside = p1arrays[i]
    rightside = p1arrays[i+1]
    pairnumber = i // 2 + 1
    print(f"\n== Pair {pairnumber} ==")

    if compare(leftside,rightside) == 1:
        rightitems.append(pairnumber)




    
print(f"right orders : {rightitems}")
print(f"sum: {sum(rightitems)}")

import functools


for array in p2arrays:
    print(array)
