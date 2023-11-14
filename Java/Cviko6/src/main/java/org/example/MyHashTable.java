package org.example;

import org.example.MyCollection.Collection;

import java.util.Iterator;

public class MyHashTable {
    private Collection keys;
    private Collection vals;
    Object get(String key) {
        int i = keys.getIndex(key);
        if (i >= 0) {
            return vals.get(i);
        }
        else return null;
    }
    void set(String key, Object value) {

    }
    public Iterator iterator() {
        return new Iterator() {
            @Override
            public boolean hasNext() {
                return false;
            }

            @Override
            public Object next() {
                return null;
            }
        };
    }
}

