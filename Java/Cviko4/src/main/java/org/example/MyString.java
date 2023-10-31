package org.example;

public class MyString{
    private String string;
    public MyString(String str) {
        this.string = str;
    }
    public void append(String str) {
        this.string += str;
    }
    public void insert(int pos, String str) {
        char[] pom = string.toCharArray();
        String temp = "";
        for (int i = 0; i < pos; i++) {
            temp += pom[i];
        }
        temp += str;
        for (int i = pos; i < pom.length; i++) {
            temp += pom[i];
        }
        this.string = temp;
    }
    public void insert(int pos, char ch) {
        char[] pom = this.string.toCharArray();
        String temp = "";
        for (int i = 0; i < pos; i++) {
            temp += pom[i];
        }
        temp += ch;
        for (int i = pos; i < pom.length; i++) {
            temp += pom[i];
        }
        this.string = temp;
    }
    public void delete(int pos, int length) {
        char[] pom = this.string.toCharArray();
        String temp = "";
        for (int i = 0; i < pos; i++) {
            temp += pom[i];
        }
        for (int i = pos+length; i < pom.length; i++) {
            temp += pom[i];
        }
        this.string = temp;
    }
    @Override
    public String toString() {
        return this.string;
    }
}
