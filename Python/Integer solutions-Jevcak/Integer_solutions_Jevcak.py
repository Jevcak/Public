a=int(input())
b=int(input())
s=0
if a>0:
    for i in range(2,a//2+1):
        if i*(a-i)==b:
            print("X = ",i,", Y = ",a-i,sep='')
            print("X = ",a-i,", Y = ",i,sep='')
            s=1
            break
else:
    for i in range(0,a//2-1,-1):
         if i*(a-i)==b:
            print("X = ",i,", Y = ",a-i,sep='')
            print("X = ",a-i,", Y = ",i,sep='')
            s=1
            break
if s==0:
    print("No solution")
        
