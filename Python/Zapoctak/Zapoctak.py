class Tree:
    def __init__(self):
        print("Initialization of the tree")

class Node:
    def __init__(self, parent):
        self.parent = parent
        self.left = None
        self.right = None
        self.balance = self.getBalance()
        
    def getBalance(self):
        left = getHeight(self.left)
        right = getHeight(self.right)
        return left-right

    def getHeight(self):
        return 0