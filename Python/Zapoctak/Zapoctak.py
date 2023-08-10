class Tree:
    def __init__(self, root):
        print("Initialization of the tree")
        self.root = root
    def insert(self,key):
        self.root.insert(key)



class Node:
    def __init__(self, key):
        self.key = key
        self.parent = None
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
                self.right = Node(key)
        elif self.key > key:
            if self.left != None:
                self.left.insert(key)
            else:
                self.left = Node(key)
        self.balance = self.getBalance()


strom = Tree(Node(input()))
strom.root.insert(input())

