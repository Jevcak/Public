package org.example.MyCollection;

public class Node {
    Node left;
    Node right;
    int value;
    int height;
    Node(int a) {
        value = a;
        left = null;
        right = null;
    }
    public Node GetRight() {
        return right;
    }
    public Node GetLeft() {
        return left;
    }
    public int GetValue() {
        return value;
    }
    public void UpdateHeight() {
        if (right.height < left.height) {
            this.height = left.height;
        }
        else {
            this.height = right.height;
        }
    }
}
