
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

    if balance > 1:
        if getBalance(root.left) >= 0:
            return RotationL(root)
        else:
            root.right = RotationR(root.right)
            return RotationL(root)
 
    if balance < -1:
        if getBalance(root.right) <= 0:
            return RotationR(root)
        else:
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

def inOrder(root, temp = []):
    if not root:
        return temp
    temp = inOrder(root.left, temp)
    temp.append(root.key)
    temp = inOrder(root.right, temp)
    return temp

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

def Merge(array1, array2):
    if len(array1) < len(array2):
        return Merge(array2, array1)
    else:
        i,j = 0,0
        m = len(array1)
        n = len(array2)
        merged = []
        while i < m and j < n:
            if array1[i] < array2[j]:
                merged.append(array1[i])
                i+=1
            elif array1[i] == array2[j]:
                merged.append(array1[i])
                i+=1
                j+=1
            else:
                merged.append(array2[j])
                j+=1
        if i == m:
            while j < n:
                merged.append(array2[j])
                j+=1
        else:
            while i < m:
                merged.append(array1[i])
                i+=1
    return merged

def ConstructTree(tree, root, array):
    if not array:
        return None
    n = len(array) // 2
    root = Node(array[n])
    root.left = ConstructTree(tree, root.left, array[:n])
    root.right = ConstructTree(tree, root.right, array[n+1:])
    tree.count+=1
    return root
    

trees = {}
roots = {}
file = open('AVL.txt', 'r')
myTree = Tree()
Tre = Tree()
koren = None
root = None
Insert = [50, 30, 70, 15]
for num in Insert:
    root = insert(myTree, root, num)

while True:
    line = file.readline()
    if not line:
        break
    l = line.split()
    if l[0] == 'insert':
        if not l[1] in trees:
            key = l[1]
            l[1] = Tree()
            trees[key] = l[1]
            l[1] = None
            roots[key] = l[1]
            l[1] = key
        for num in l[2:]:
            roots[l[1]] = insert(trees[l[1]], roots[l[1]], int(num))
    elif l[0] == 'delete':
        if not l[1] in trees:
            key = l[1]
            l[1] = Tree()
            trees[key] = l[1]
            l[1] = None
            roots[key] = l[1]
            l[1] = key
        for num in l[2:]:
            roots[l[1]] = delete(trees[l[1]], roots[l[1]], int(num))
    elif l[0] == 'find':
        if not l[1] in trees:
            key = l[1]
            l[1] = Tree()
            trees[key] = l[1]
            l[1] = None
            roots[key] = l[1]
            l[1] = key
        for num in l[2:]:
            print(find(trees[l[1]], roots[l[1]], int(num)))
    elif l[0] == 'merge':
        if not l[3] in trees:
            key = l[3]
            l[3] = Tree()
            trees[key] = l[3]
            l[3] = None
            roots[key] = l[3]
            l[3] = key
        temp = []
        arr1 = inOrder(roots[l[1]], temp)
        temp = []
        arr2 = inOrder(roots[l[2]], temp)
        arr3 = Merge(arr1, arr2)
        k = ConstructTree(trees[l[3]],roots[l[3]],arr3)
        arr1,arr2,arr3 = [],[],[]
    elif l[0] == 'print':
        for tree in l[1:]:
            preOrder(roots[tree])
