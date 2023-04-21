import sys
m=[]
for line in sys.stdin:
    l=line.strip().split(" ")
    m.append(l)
    if len(m)==(int(m[0][0])+1):
        k=m.pop()
        print(m)
        break
def kontrola(p,v=[]):
    for i in range(p):
