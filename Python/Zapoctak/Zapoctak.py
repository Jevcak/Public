
class Node:
    def __init__(self, key):
        self.key = key
        self.left = None
        self.right = None
        self.height = 1

def getHeight(root):
    if not root:
        return 0
    else:
        return root.height

def getBalance(root):
    if not root:
        return 0
    else:
        return getHeight(root.right) - getHeight(root.left)

def getMinNode(root):
    if not root.left:
        return root
    else:
        return getMinNode(root.left)

class Tree:
    def __init__(self):
        print("Initialization of the tree")
        self.count = 1
    def insert(self, root, key):
        if not root:
            self.count+=1
            return Node(key)
        elif root.key == key:
            return root
        elif root.key > key:
            root.left = self.insert(root.left, key)
        else:
            root.right = self.insert(root.right, key)
        root.height = 1 + max(getHeight(root.left),getHeight(root.right))
        balance = getBalance(root)
        if balance > 1:
            if not root.left or key < root.left.key:
                return RotationL(root)
            else:
                root.right = RotationR(root.right)
                return RotationL(root)
        if balance < -1:
            if not root.right or key > root.right.key:
                return RotationR(root)
            else:
                root.left = RotationL(root.left)
                return RotationR(root)
            '''
        if balance > 1 and key < root.left.key:
            return RotationL(root)
 
        if balance < -1 and key > root.right.key:
            return RotationR(root)
 
        if balance > 1 and key > root.left.key:
            root.right = RotationR(root.right)
            return RotationL(root)
 
        if balance < -1 and key < root.right.key:
            root.left = RotationL(root.left)
            return RotationR(root)
        '''

        return root

    def find(self, root, key):
        if not root:
            return False
        elif root.key < key:
            return self.find(root.right, key)
        elif root.key > key:
            return self.find(root.left, key)
        else:
            return True

    def delete(self, root, key):
        if not root:
            return root
        elif root.key < key:
            root.right = self.delete(root.right, key)
        elif root.key > key:
            root.left = self.delete(root.left, key)
        else:
            if not root.left:
                x = root.right
                root = None
                return x
            elif not root.right:
                x = root.left
                root = None
                return x
            else:
                x = getMinNode(root)
                root.key = x.key
                root.left = self.delete(root.left, x.key)
        if not root:
            return root
        root.height = 1 + max(getHeight(root.left),getHeight(root.right))
        balance = getBalance(root)

        if balance > 1 and getBalance(root.left) >= 0:
            return RotationL(root)
 
        if balance < -1 and getBalance(root.right) <= 0:
            return RotationR(root)
 
        if balance > 1 and getBalance(root.left) < 0:
            root.right = RotationR(root.right)
            return RotationL(root)
 
        if balance < -1 and getBalance(root.right) > 0:
            root.left = RotationL(root.left)
            return RotationR(root)

        return root

def preOrder(root):
    if not root:
        return
    print(root.key,end=" ")
    preOrder(root.left)
    preOrder(root.right)
    return

def RotationL(root):
    y = root.right
    B = y.left
    y.left = root
    root.right = B
    root.height = 1 + max(getHeight(root.left),getHeight(root.right))
    y.height = 1 + max(getHeight(y.left),getHeight(y.right))
    return y

def RotationR(root):
    y = root.left
    B = y.right
    y.right = root
    root.left = B
    root.height = 1 + max(getHeight(root.left),getHeight(root.right))
    y.height = 1 + max(getHeight(y.left),getHeight(y.right))
    return y

file = open('AVL.txt', 'r')
while True:
    line = file.readline()
    if not line:
        break
    print(line)

myTree = Tree()
root = None
Insert = [50, 30, 70, 20, 40, 60, 80, 10, 25, 35, 45, 55, 65, 75, 90, 5, 15]
Delete =  [40, 45, 10, 20, 25, 35]
for num in Insert:
    root = myTree.insert(root, num)

for num in Delete:
    root = myTree.delete(root, num)
Insert = [38, 48, 42]
Delete = [30, 35]
for num in Insert:
    root = myTree.insert(root, num)
for num in Delete:
    root = myTree.delete(root, num)

print("Preorder Traversal after insertion -")
preOrder(root)