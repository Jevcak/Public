package org.example.MyCollection;

import java.util.Arrays;
import java.util.Iterator;

public abstract class Collection implements MyCollection{
    public Object[] pole = new Object[1];
    int s = 0;
    public void add(Object o) {
        if (pole == null) {
            pole = new Object[1];
            pole[s++] = o;
        }
        if (!iterator().hasNext()) {
            Object[] np = new Object[pole.length+1];
            for (int i = 0; i < pole.length; i++) {
                np[i] = pole[i];
            }
            pole = new Object[pole.length*2];
            for (int i = 0; i < np.length; i++) {
                np[i] = pole[i];
            }
        }
        else {
            pole[s++] = o;
        }
    }
    public void remove(Object o) {
        int i = 0;
        while (!pole[i].equals(o) & iterator().hasNext())
        {
            i++;
        }
        if (pole[i].equals(o))
        {
            pole[i] = null;
        }
    }

    public void remove(int i) {
        pole[i] = null;

    }
    public Iterator iterator() {
        return new Iterator() {
            private int index = 0;
            @Override
            public boolean hasNext() {
                return index < s;
            }

            @Override
            public Object next() {
                return pole[index++];
            }
        };
    }
}
