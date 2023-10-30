package org.example.MyCollection;

import java.util.Iterator;

public class Tree {
    Node root;
    public void add(int a) {
        root = addRec(root,a);
    }
    public Node addRec(Node current, int val) {
        if (current==null) {
            return new Node(val);
        }
        if (val < current.value) {
            current.left = addRec(current.left, val);
        }
        else if (val > current.value) {
            current.right = addRec(current.right, val);
        }
        else return current;
        return current;
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
