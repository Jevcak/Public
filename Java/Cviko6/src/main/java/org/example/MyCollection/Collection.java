package org.example.MyCollection;

import java.util.Iterator;

public class Collection implements MyCollection{
    public Object[] pole = new Object[1];
    int s = 0;
    public Collection(String... args) {
        pole = null;
    }
    public static Collection of(String... args) {
        Collection temp = new Collection();
        for (String arg : args) {
            temp.add(arg);
        }
        return temp;
    }
    public void add(Object o) {
        if (pole == null) {
            pole = new Object[1];
            pole[s++] = o;
        }
        else if (!iterator().hasNext()) {
            Object[] np = new Object[pole.length+1];
            for (int i = 0; i < pole.length; i++) {
                np[i] = pole[i];
            }
            pole = new Object[np.length];
            for (int i = 0; i < np.length; i++) {
                pole[i] = np[i];
            }
            pole[s++] = o;
        }
        else {
            pole[s++] = o;
        }
    }
    public void remove(Object o) {
        for (int i = 0; i < pole.length; i++) {
            if (o.equals(pole[i])) {
                s--;
                Object[] np = new Object[pole.length-1];
                for (int j = 0; j < np.length; j++) {
                    if (i > j) {
                        np[j] = pole[j];
                    }
                    else {
                        np[j] = pole[j+1];
                    }
                }
                pole = new Object[np.length];
                for (int j = 0; j < pole.length; j++) {
                    pole[j] = np[j];
                }
            }
        }
    }
    public void print() {
        for (int i = 0; i < pole.length; i++) {
            System.out.println(pole[i]);
        }
    }
    public Object get(int i) {
        return pole[i];
    }
    public int getIndex(Object o) {
        for (int i = 0; i < pole.length; i++) {
            if (o.equals(pole[i])) {
                return i;
            }
        }
        return -1;
    }
    public void remove(int i) {
        if (i < pole.length) {
            remove(pole[i]);
        }
    }
    public Iterator iterator() {
        return new Iterator() {
            private int index = 0;
            @Override
            public boolean hasNext() {
                index = pole.length;
                return index > s;
            }

            @Override
            public Object next() {
                return pole[index++];
            }
        };
    }
}
