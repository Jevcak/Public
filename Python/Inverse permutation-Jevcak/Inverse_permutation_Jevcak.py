l=[]
for i in input().split():
    l.append(int(i))
N=l.pop(0)
k=[0]*(N)
for j in range(N):
    k[l[j]-1]=j+1
print(*k)