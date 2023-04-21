def funkce(k,m=0,l=[]):
    for i in range(1,k+1):
        if i>m:
            l.append(i)
            print(*l)
            funkce(k,m=i)
            l.pop()

k=int(input())
if 0<k<20:
    funkce(k)