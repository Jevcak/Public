package org.example;

import org.junit.jupiter.api.*;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class MyStringTests {
    @Test
    void insertChar() {
        System.out.println("Test insert char");
        MyString A = new MyString("Kostka");
        A.insert(3,'a');
        String exp = "Kosatka";
        String result = A.toString();
        assertEquals(exp, result);
    }
    @Test
    void insertString() {
        System.out.println("Test insert String");
        MyString A = new MyString("Kos");
        A.insert(2, "nipa");
        String expResult = "Konipas";
        String result = A.toString();
        assertEquals(expResult, result);
    }
    @Test
    void append() {
        System.out.println("Test append");
        MyString A = new MyString("Kos");
        A.append("ovo");
        String expResult = "Kosovo";
        String result = A.toString();
        assertEquals(expResult, result);
    }
    @Test
    void delete() {
        System.out.println("Test delete");
        MyString A = new MyString("Kostnice");
        A.delete(3,2);
        String expResult = "Kosice";
        String result = A.toString();
        assertEquals(expResult, result);
    }
}
