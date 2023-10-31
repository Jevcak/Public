package org.example;
import java.io.*;
import java.util.Scanner;


public class Main {
    public static void main(String[] args) {
        MyString s = new MyString("hello");
        s.append(" world");
        s.delete(0, 1);
        s.insert(0, 'H');
        s.append("!");
        System.out.println(s);
        Scanner in = new Scanner(System.in);
        String g = in.nextLine();
        try { BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
            int c;
            while ((c = input.read()) != -1) {
                System.out.print((char)c);
                }
        } catch (IOException e) {
            System.out.println("Nastala IOException");
        }
        try {
            File myObj = new File(g);
            Scanner myReader = new Scanner(myObj);
            while (myReader.hasNext()) {
                String data = myReader.next();
                System.out.println(data);
            }
            myReader.close();
        } catch (FileNotFoundException e) {
            System.out.println("An error occurred.");
            e.printStackTrace();
        }
    }
}