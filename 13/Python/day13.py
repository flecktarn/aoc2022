import time
from copy import deepcopy

file = open('input.txt','r')

lines = file.readlines()

    
def compare(leftarray, rightarray):
    left = deepcopy(leftarray)
    right = deepcopy(rightarray)

    #if left and right are ints, just do the standard comparison
    if isinstance(left,int) and isinstance(right,int):

        if right < left:
            return -1 

        elif left < right:
            return 1
        
        else:
            return 0

    else:
        #arrays
        
        if isinstance(left, int):
            left = [left]

        if isinstance(right, int):
            right = [right]


        while True:

            if len(left) == 0 and len(right) == 0:
                return 0

            if len(left) == 0:
                return 1

            if len(right) == 0:
                return -1
            
            leftfirst = left.pop(0)
            rightfirst = right.pop(0)

            firstcomparison = compare(leftfirst,rightfirst)

            if firstcomparison != 0:
                return firstcomparison







    
arrays = []

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


        arrays.append(current_item)




p2arrays = deepcopy(arrays)

rightitems = []

for i in range(0, len(arrays),2):
    leftside = arrays[i]
    rightside = arrays[i+1]
    pairnumber = i // 2 + 1
    if compare(leftside,rightside) == 1:
        rightitems.append(pairnumber)




    
print(f"incides in the right order: {rightitems}")
print(f"sum: {sum(rightitems)}")

import functools


key1 = [[2]]
key2 = [[6]]

arrays.append(key1)
arrays.append(key2)

sortedarrays = sorted(arrays, key=functools.cmp_to_key(compare))

'''
for array in sortedarrays:
    print(array)
'''

sortedarrays = sortedarrays[::-1]

k1i = 0
k2i = 0

for i in range(len(sortedarrays)):
    if sortedarrays[i] == key1:
        print(f"key1: {i}")
        k1i = i+1


    if sortedarrays[i] == key2:
        print(f"key2: {i}")
        k2i = i+1

print(f'Decoder key: {k1i * k2i}')


