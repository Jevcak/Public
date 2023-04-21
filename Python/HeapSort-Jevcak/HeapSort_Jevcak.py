import sys
vstup=[]
def vstup_hotov(vstup):
    if len(vstup)==0 or len(vstup)!=(int(vstup[0])+1):
        return True
    else: return False
for line in sys.stdin:
    if vstup_hotov(vstup):
        d=line.strip().split(' ')
        vstup.append(int(d[0]))
    else: break
vstup[0]+=1
h=len(vstup)-1
def bublej(x,i):
    global vstup,k
    if ((2*i)+1)<=h:
        if x<vstup[2*i] or x<vstup[(2*i)+1]:
            k=vstup.index(max(vstup[2*i],vstup[(2*i)+1]))
            vstup[i],vstup[k]=max(vstup[2*i],vstup[(2*i)+1]),x
            bublej(x,k)
    elif (2*i)<=h and x<vstup[2*i]:
        k=vstup.index(vstup[2*i])
        vstup[i],vstup[k]=vstup[2*i],x
        bublej(x,k)
d=0
while vstup[1]!=max(vstup[1:len(vstup)]):
    if vstup[h-d]>vstup[(h-d)//2]:
        bublej(vstup[(h-d)//2],(h-d)//2)
    d+=1
print(*vstup[1:len(vstup)+1],sep=' ')
def extract_min():
    global h
    m=vstup[1]
    h-=1
    if m!=min(vstup):
        vstup[1],vstup[h+1]=vstup[h+1],vstup[1]
        bublej(vstup[1],1)
    else: pass
if vstup[0]!=0:
    while h!=1:
        extract_min()
        print(*vstup[1:len(vstup)+1],sep=' ')