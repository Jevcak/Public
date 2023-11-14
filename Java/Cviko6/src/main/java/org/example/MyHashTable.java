package org.example;

import java.util.Dictionary;
import java.util.Iterator;

public class MyHashTable {

    Object get(String key) {
        return key;
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

