import sys
vstup=[int(x) for x in sys.stdin.readline().split()]
def eratostenovo_sito(n):
    prvocisla=[True]*(n+1)
    for i in range(2,n):
        if prvocisla[i]==True:
            j=i*i
            while j<=n:
                prvocisla[j]=False
                j+=i
    return prvocisla
prvocisla=eratostenovo_sito(max(vstup))
prvocisla[0],prvocisla[1]=False,False
def g(n):
    g=0
    global prvocisla
    for i in range((n//2)+1):
        if prvocisla[i] and prvocisla[n-i] and n-i>=i:
            g+=1
    return g
for i in vstup:
    print(g(i),end=' ')