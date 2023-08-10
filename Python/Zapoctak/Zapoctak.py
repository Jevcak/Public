class Tree:
    def __init__(self, root):
        print("Initialization of the tree")
        self.root = root
    def insert(self,key):
        self.root.insert(key)



class Node:
    def __init__(self, key, parent = None):
        self.key = key
        self.parent = parent
        self.left = None
        self.right = None
        self.balance = self.getBalance()
        
    def getBalance(self):
        if self.left != None:
            left = self.left.getHeight()
        else:
            left = 0
        if self.right != None:
            right = self.right.getHeight()
        else:
            right = 0
        return left-right

    def getHeight(self):
        if self.left != None and self.right != None:
            return max(self.right.getHeight(),self.left.getHeight())+1
        elif self.right == None and self.left != None:
            return self.left.getHeight() + 1
        elif self.right != None and self.left == None:
            return self.right.getHeight() + 1
        else:
            return 1
    def insert(self,key):
        if self.key == key:
            return
        elif self.key < key:
            if self.right != None:
                self.right.insert(key)
            else:
                self.right = Node(key, self)
        elif self.key > key:
            if self.left != None:
                self.left.insert(key)
            else:
                self.left = Node(key, self)
        self.balance = self.getBalance()
def RotationR(x, y): #x is at the top
    a = y.left
    b = y.right
    c = x.right
    y.parent = x.parent
    x.parent = y
    y.right = x
    y.left = a
    x.left = b
    x.right = c
def RotationL(x, y): #x is at the top
    a = x.left
    b = y.left
    c = y.right
    y.parent = x.parent
    x.parent = y
    y.left = x
    y.right = c
    x.left = a
    x.right = b
def RotationLR(x,y,z): #  z  at the top
    RotationL(x,y)     # x   is the left son of z
    RotationR(z,y)     #  y  is the right son of x

strom = Tree(Node(input()))
strom.root.insert(input())

