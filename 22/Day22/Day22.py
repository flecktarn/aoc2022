
def put(s):
    print(s,end='')

def get_instructions(board):

    instructionline = board.pop(-1) 
    instructions = []

    if instructionline[0].isalpha():
        lastisnum = False
    else:
        lastisnum = True

    currentinstruction = []

    for c in instructionline:
        if c.isalpha():
            if lastisnum:
                instructions.append(int("".join(currentinstruction)))
                currentinstruction = []
                lastisnum = False

        elif c.isdigit():
            if not lastisnum:
                instructions.append("".join(currentinstruction))
                currentinstruction = []
                lastisnum = True

        currentinstruction.append(c)

    if len(currentinstruction):
        if(currentinstruction[0].isdigit()):
            instructions.append(int("".join(currentinstruction)))
        else:
            instructions.append("".join(currentinstruction))
    return instructions


def get_first_available_row(col,board):
    for row in range(len(board)):
        if board[row][col] != ' ':
            return [col,row]


def get_last_available_row(col,board):
    for row in range(len(board)-1,-1,-1):
        if board[row][col] != ' ':
            return [col,row]

def get_first_available_col(row,board):
    for col in range(len(board)):
        if board[row][col] != ' ':
            return [col,row]

def get_last_available_col(row,board):
    for col in range(len(board)-1,-1,-1):
        if board[row][col] != ' ':
            return [col,row]


def show_board(board):
    print()
    global trail
    global pos
    for y in range(len(board)):
        for x in range(len(board[0])):

            if x == pos[0][0] and y == pos[0][1]:
                put('*')

            elif f"{x},{y}" in trail:
                put(trail[f"{x},{y}"])

            else:
                put(board[y][x])

        put('\n')
    print()

def left(x,y,board):
    nx = x-1
    ny = y

    if not inbounds(nx,ny,board) or void(nx,ny,board):
        jump = get_last_available_col(y,board)

        if(blocked(jump[0],jump[1],board)):
            return [x,y]
        else:
            return jump

    if blocked(nx,ny,board):
        return [x,y]

    return [nx,y]

def right(x,y,board):

    nx = x+1
    ny = y

    if not inbounds(nx,ny,board) or void(nx,ny,board):
        jump = get_first_available_col(y,board)

        if(blocked(jump[0],jump[1],board)):
            return [x,y]
        else:
            return jump

    if blocked(nx,ny,board):
        return [x,y]

    return [nx,ny]

def up(x,y,board):

    nx = x
    ny = y-1

    if (not inbounds(nx,ny,board)) or void(nx,ny,board):
        jump = get_last_available_row(x,board)

        if(blocked(jump[0],jump[1],board)):
            return [x,y]
        else:
            return jump

    if blocked(nx,ny,board):
        return [x,y]

    return [nx,ny]

def down(x,y,board):

    nx = x
    ny = y+1

    if (not inbounds(nx,ny,board)) or void(nx,ny,board):
        jump = get_first_available_row(x,board)

        if(blocked(jump[0],jump[1],board)):
            return [x,y]
        else:
            return jump

    if blocked(nx,ny,board):
        return [x,y]

    return [nx,ny]

def get_symbol(direction):
    return ['>','V','<','^'][direction]

def move_forward(pos,board):

    trail[f"{pos[0][0]},{pos[0][1]}"] = get_symbol(pos[1])

    #right
    if pos[1] == 0:
        newpos = right(pos[0][0], pos[0][1],board)
        pos[0][0] = newpos[0]
        pos[0][1] = newpos[1]

    #down
    elif pos[1] == 1:
        newpos = down(pos[0][0], pos[0][1],board)
        pos[0][0] = newpos[0]
        pos[0][1] = newpos[1]

    #left
    elif pos[1] == 2:
        newpos = left(pos[0][0], pos[0][1],board)
        pos[0][0] = newpos[0]
        pos[0][1] = newpos[1]

    #up
    elif pos[1] == 3:
        newpos = up(pos[0][0], pos[0][1],board)
        pos[0][0] = newpos[0]
        pos[0][1] = newpos[1]



def blocked(x,y,board):
    return board[y][x] == '#'


def void(x,y,board):
    return board[y][x] == ' '

def inbounds(x,y,board):
    return x>=0 and y>=0 and x<len(board[0]) and y<len(board)

def turn_right(pos):
    pos[1] = (pos[1] + 1) % 4

def turn_left(pos):
    pos[1] = (pos[1] - 1) % 4



#main

board = open('input.txt').readlines()

maxlen = 500

instructions = get_instructions(board)

for r in range(len(board)):
    board[r] = board[r][:-1]
    if len(board[r]) > maxlen:
        maxlen = len(board[r])
    if board[r].isspace():
        board.pop(r)
        break

for i in range(len(board)):
    if len(board[i]) < maxlen:
        for j in range(maxlen - len(board[i])):
            board[i] += " "


trail = {}

pos = [get_first_available_col(0,board),0]
#pos = [[12,3],0]

print(instructions)


for i in instructions:
    if isinstance(i, int):
        print(i)
        for loop in range(i):
            move_forward(pos,board)

    else:
        if i == "R":
            turn_right(pos)
        elif i == "L":
            turn_left(pos)

show_board(board)


print(f"final col = {pos[0][0]}")
print(f"final row = {pos[0][1]}")
print(1000 * (pos[0][1]+1) + 4 * (pos[0][0]+1) + pos[1])












