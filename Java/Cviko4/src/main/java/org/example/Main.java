package org.example;

// Press Shift twice to open the Search Everywhere dialog and type `show whitespaces`,
// then press Enter. You can now see whitespace characters in your code.
public class Main {
    public static void main(String[] args) {
        // Press Alt+Enter with your caret at the highlighted text to see how
        // IntelliJ IDEA suggests fixing it.
        MyString s = new MyString("hello");
        s.append(" world");
        s.delete(0, 1);
        s.insert(0, 'H');
        s.append("!");
        System.out.println(s.ToString());
        MyString str = new MyString("Joko");
        System.out.println(str);
        str.append(" Ono");
        System.out.println(str);
        str.insert(4, "Neni");
        System.out.println(str);
        str.insert(1, 'Y');
        System.out.println(str);
        str.delete(0,1);
        System.out.println(str);
        str.insert(4,' ');
        System.out.println(str);
    }
}