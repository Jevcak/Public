
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
def insert(tree, root, key):
    if not root:
        tree.count+=1
        return Node(key)
    elif root.key == key:
        return root
    elif root.key > key:
        root.left = insert(tree, root.left, key)
    else:
        root.right = insert(tree, root.right, key)
    root.height = 1 + max(getHeight(root.left),getHeight(root.right))
    balance = getBalance(root)
    if balance > 1:
        if key > root.right.key:
            return RotationL(root)
        else:
            root.right = RotationR(root.right)
            return RotationL(root)
    if balance < -1:
        if key < root.left.key:
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

def delete(tree, root, key):
    if not root:
        return root
    elif root.key < key:
        root.right = delete(tree, root.right, key)
    elif root.key > key:
        root.left = delete(tree, root.left, key)
    else:
        tree.count-=1
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
            root.left = delete(tree,root.left, x.key)
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

def find(tree, root, key):
    if not root:
        return False
    elif root.key < key:
        return find(tree, root.right, key)
    elif root.key > key:
        return find(tree,root.left, key)
    else:
        return True

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

functions = {}
file = open('AVL.txt', 'r')
myTree = Tree()
root = None
Insert = [50, 30, 70, 15]
for num in Insert:
    root = insert(myTree, root, num)

Insert = [38, 48, 42, 75, 98, 99]
for num in Insert:
    root = insert(myTree, root, num)
functions = {'insert':insert}
preOrder(root)
while True:
    line = file.readline()
    if not line:
        break
    l = line.split()
    m = l.count
    k = l[2:]

    if l[0] == 'insert':
        for num in k:
            insert(myTree, root, int(num))
    elif l[0] == 'delete':
        for num in k:
            delete(myTree, root, int(num))
    elif l[0] == 'find':
        for num in k:
            print(find(myTree, root, int(num)))
    print(l)
#preOrder(root)