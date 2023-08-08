class Tree:
    def __init__(self):
        print("incializace stromu")
        

class Node:
    def __init__(self, parent):
        self.parent = parent
        self.left = None
        self.right = None
        self.balance = getBalance(self)