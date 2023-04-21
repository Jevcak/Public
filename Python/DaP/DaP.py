import sys
vstup=[]
for line in sys.stdin:
    l=line.strip().split()
    if l==[]:
        break
    else:
        vstup.append(int(l[0]))
k=max(vstup)
f=0
prv=0
def eratostenovo_sito(n):
    prvocisla=[True]*(n+1)
    for i in range(2,n//2):
        if prvocisla[i]==True:
            j=i*i
            while j<=n:
                prvocisla[j]=False
                j+=i
    return prvocisla
prvoc=eratostenovo_sito(k)
def list_primes(k,primes=[]):
    global prvoc
    for i in range(2,k+1):
        if prvoc[i]:
            primes.append(i)
    return primes
primes=list_primes(k)
def rozklad(m):
    j=0
    h=[0]*len(primes)
    for i in primes:
        if m==1:
            h.pop()
        else:
            while m%i==0:
                h[j]+=1
                m=m//i
        j+=1
    return h
output=[]
def rekurze(R):
    if R==1:
        output.append('()')
    if R==0:
        output.append('.')
    elif R>1:
        output.append('(')
        for i in rozklad(R):
            rekurze(i)
        output.append(')')
    else:pass

for i in vstup:
    output=[]
    rekurze(i)
    print(*output,sep='')