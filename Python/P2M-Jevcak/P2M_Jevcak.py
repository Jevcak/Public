from fractions import Fraction
import sys
p1={}
p2={}
g=0
f=0
for line in sys.stdin:
    l=line.strip().split(' ')
    if f==0:
        funkce=l[0]
        f=1
    elif l[0]=='-1' and l[1]=='-1' and g==1:
        break
    elif l[0]=='-1' and l[1]=='-1':
        g=1
    elif g==0:
        p1[int(l[0])]=int(l[1])
    else:
        p2[int(l[0])]=int(l[1])

def add(p1,p2):
  result={}
  for i in p1:
    if i in result:
        result[i]+=p1[i]
    else:
        result[i]=p1[i]
  for j in p2:
    if j in result:
        result[j]+=p2[j]
    else:
        result[j]=p2[j]
  for key, value in dict(result).items():
    if value==0:
            del result[key]
  return result

def sub(p1,p2):
  for k in p2:
      p2[k]=-p2[k]
  return add(p1,p2)

def mul(p1,p2):
  result={}
  for i in p1:
      for j in p2:
          if j+i not in result:
              result[j+i]=p1[i]*p2[j]
          else:
              result[j+i]+=p1[i]*p2[j]
  for key, value in dict(result).items():
    if value==0:
            del result[key]
  k=list(result.keys())
  k.sort(reverse=True)
  result2={i:result[i] for i in k}
  return result2

def div(p1,p2):
    result={}
    d=0
    doc={}
    while list(p1.keys())[0]-list(p2.keys())[0]>=0 and d<2:
        result[list(p1.keys())[0]-list(p2.keys())[0]]=Fraction(list(p1.values())[0],list(p2.values())[0])
        doc.clear()
        doc[list(p1.keys())[0]-list(p2.keys())[0]]=Fraction(list(p1.values())[0],list(p2.values())[0])
        p1=sub(p1,mul(doc,p2))
        if list(p1.keys())[0]-list(p2.keys())[0]==0:
            d+=1
    k=list(p1.keys())
    k.sort(reverse=True)
    result2={i:p1[i] for i in k}
    return result,result2

if funkce=='add':
    result=add(p1,p2)
    for i in result:
        print(i,result[i],sep=' ',end='\n')
if funkce=='sub':
    result=sub(p1,p2)
    for i in result:
        print(i,result[i],sep=' ',end='\n')
if funkce=='mul':
    result=mul(p1,p2)
    for i in result:
        print(i,result[i],sep=' ',end='\n')
if funkce=='div':
    result=list(div(p1,p2))
    for i in result[0]:
        print(i,result[0][i],sep=' ',end='\n')
    print(-1,-1,sep=' ')
    for i in result[1]:
        print(i,result[1][i],sep=' ',end='\n')
print(-1,-1,sep=' ')