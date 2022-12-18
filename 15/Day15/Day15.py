from pickle import FALSE
import re

with open("input.txt","r") as f:
    lines = f.readlines()

sensors = []

for line in lines:
    parts = re.split('=|,|:', line)
    sx = int(parts[1])
    sy = int(parts[3])
    bx = int(parts[5])
    by = int(parts[7])
    sensors.append([sx,sy,bx,by])

#the taxicab distance beetween a sensor and its closest beacon
def dtaxi(sensor):
    return abs(sensor[0] - sensor[2]) + abs(sensor[1] - sensor[3])

#helper to print without newline
def write(s):
    print(s, end='')

#p1

def blockedxrange(sensor, lineno):
    d = dtaxi(sensor)

    #if line is further away from the sensor than the maximum possible distance of the beacon, no squares are blocked 
    if abs(lineno - sensor[1])  > d:
        return 0,0

    #otherwise, we need to remove some squares
    origin = sensor[0]
    width = (2*(d - abs(lineno - sensor[1])) + 1)

    return range(origin - (width //2), origin + width // 2  + 1)



def getblockedony(y,sensors):
    blockedony = set()
    for sensor in sensors:
        for x in blockedxrange(sensor,y):
            blockedony.add(x)
    print(len(blockedony) - 1)



def blockedxbounds(sensor, lineno):
    d = dtaxi(sensor)

    #if line is further away from the sensor than the maximum possible distance of the beacon, no squares are blocked 
    if abs(lineno - sensor[1])  > d:
        return None

    #otherwise, we need to remove some squares
    origin = sensor[0]
    width = (2*(d - abs(lineno - sensor[1])) + 1)

    return [origin - (width //2), origin + width // 2]




#returns the combination of 2 ranges if they overlap, otherwise returns false
def overlap(range1, range2):
    if range1[0] < range2[0] -1 and range1[1] < range2[0] -1:
        print(f"NO OVERLAP BETWEEN {range1} {range2}")
        return False

    if range1[0] > range2[1] +1 and range1[1] > range2[1] +1:
        print(f"NO OVERLAP BETWEEN {range1} {range2}")
        return False
    
    else:
        return [min(range1[0], range2[0]), max(range1[1], range2[1])]


#maxcoord = 4000000
maxcoord = 20

def addnewrange(ranges, newrange):
    global maxcoord

    #clamp values

    if newrange[0] < 0:
        newrange[0] = 0

    if newrange[1] > maxcoord:
        newrange[1] = maxcoord 

    ranges.append(newrange)


def sortranges(ranges):
    ranges.sort(key = min)

def combineranges(ranges):
    sortranges(ranges)
    while len(ranges) > 1 and overlap(ranges[0], ranges[1]):
        ranges[0] = overlap(ranges[0], ranges[1])
        ranges.pop(1)



print("== PART 1 == ")
getblockedony(2000000,sensors)

print("== PART 2 == ")

print(" ")

for i in range(40000001):
    ranges = []
    for sensor in sensors:
        newrange = blockedxbounds(sensor,i)
        if newrange != None:
            ranges.append(newrange)

    combineranges(ranges)
    print(i / 4000000, end='\r')

    if len(ranges) > 1:
        x = ranges[0][1] + 1
        y = i   
        print(f"{x},{y} {x*4000000 + y}")
        break


