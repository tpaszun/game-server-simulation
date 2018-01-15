package com.yggdrasilAssignment.probability;

import junit.framework.Test;
import junit.framework.TestCase;
import junit.framework.TestSuite;

public class FractionTests extends TestCase {
    public FractionTests( String testName )
    {
        super( testName );
    }

    public static Test suite()
    {
        return new TestSuite( FractionTests.class );
    }

    public void testReductionOnCreation() {
        Fraction x = new Fraction(3, 6);

        assertEquals(1, x.getNumerator());
        assertEquals(2, x.getDenominator());
    }

    public void testAddition() {
        Fraction x = new Fraction(1, 2);
        Fraction y = x.add(new Fraction(1, 3));

        assertEquals(5, y.getNumerator());
        assertEquals(6, y.getDenominator());
    }

    public void testMultiplication() {
        Fraction x = new Fraction(1, 2);
        Fraction y = x.multiply(new Fraction(1, 3));

        assertEquals(1, y.getNumerator());
        assertEquals(6, y.getDenominator());
    }
}
