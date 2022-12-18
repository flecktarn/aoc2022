
def adjacents(x,y,z):
    cubes = []
    cubes.append((x-1,y,z))
    cubes.append((x+1,y,z))
    cubes.append((x,y+1,z))
    cubes.append((x,y-1,z))
    cubes.append((x,y,z+1))
    cubes.append((x,y,z-1))

    return cubes


exposedareas = {}

with open('input.txt','r') as f:
    lines = f.readlines()

cubes = []

for line in lines:
    coords = line.split(",")
    x = int(coords[0])
    y = int(coords[1])
    z = int(coords[2])
    cubes.append([x,y,z])

    exposedarea = 6

    for adj in adjacents(x,y,z):
        adjindex = f"{adj[0]},{adj[1]},{adj[2]}"
        if adjindex in exposedareas:
            exposedareas[adjindex] -= 1
            exposedarea -= 1
    
    index = f"{x},{y},{z}"
    exposedareas[index] =  exposedarea 

collector = 0

for key in exposedareas:
    collector += exposedareas[key]

print("PART 1:")
print(collector)




minx = cubes[0][0]
maxx = minx
miny = cubes[0][1]
maxy = miny
minz = cubes[0][2]
maxz = minz

for cube in cubes:

    cubex = cube[0]
    cubey = cube[1]
    cubez = cube[2]

    minx = min(cubex,minx)
    miny = min(cubey,miny)
    minz = min(cubez,minz)

    maxx = max(cubex,maxx)
    maxy = max(cubey,maxy)
    maxz = max(cubez,maxz)

#create box around structure
minx -= 1
miny -= 1
minz -= 1
maxx += 1
maxy += 1
maxz += 1


def inbounds(x,y,z):
    global maxx
    global maxy
    global maxy

    global minx
    global miny
    global minz

    if x < minx:
        return False
    
    if x > maxx:
        return False
    
    if y < miny:
        return False
    
    if y > maxy:
        return False

    if z < minz:
        return False

    if z > maxz:
        return False

    return True

#3d bfs models the spread of steam
steamspreading = True

#create steam source at minimum coords
steampositions = [[minx,miny,minz]]
steamnextpositions = []
positionsvisited = {}
facesseen = 0

step = 1

while(steamspreading):
    for position in steampositions:

        posx = position[0]
        posy = position[1]
        posz = position[2]

        posstring = f"{posx},{posy},{posz}"

        #log position as visited

        #spread
        for adj in adjacents(posx,posy,posz):

            adjx = adj[0]
            adjy = adj[1]
            adjz = adj[2]

            adjstring = f"{adjx},{adjy},{adjz}"

            if (adjstring not in positionsvisited) and inbounds(adjx,adjy,adjz):
                if adjstring in exposedareas:
                    facesseen += 1
                else:
                    positionsvisited[adjstring] = 1
                    steamnextpositions.append(adj)

    steampositions.clear()
    for nextpos in steamnextpositions:
        steampositions.append(nextpos)
    steamnextpositions.clear()

    if len(steampositions) == 0:
        steamspreading = False

print("PART 2:")
print(f"{facesseen} faces seen")
