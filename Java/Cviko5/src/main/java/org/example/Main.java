package org.example;

import org.example.MyCollection.Collection;

public class Main {
    public static void main(String[] args) {
        Collection Pole = new Collection();
        for (String s : args) {
            Pole.add(s);
        }
        Pole.print();
        Collection arr = Collection.of("hello", "world", "!");
        arr.print();
    }
}