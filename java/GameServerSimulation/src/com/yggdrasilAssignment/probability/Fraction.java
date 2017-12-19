package com.yggdrasilAssignment.probability;

public class Fraction {
    private long _numerator;
    private long _denominator;

    public Fraction(long numerator, long denominator) {
        _numerator = numerator;
        _denominator = denominator;
        reduce();
    }

    public Fraction add(Fraction other) {
        Fraction result = new Fraction(
                (_numerator * other.getDenominator()) + (other.getNumerator() * _denominator),
                _denominator * other.getDenominator());
        reduce();
        return result;
    }

    public Fraction multiply(Fraction other) {
        Fraction result = new Fraction(
                _numerator * other.getNumerator(),
                _denominator * other.getDenominator());
        reduce();
        return result;
    }

    public long getDenominator() {
        return _denominator;
    }

    public long getNumerator() {
        return _numerator;
    }

    public String toString() {
        return String.format("%d / %d (%f)", _numerator, _denominator, (double)_numerator / _denominator);
    }

    private void reduce() {
        long gcd = MathUtils.utilGcd(_numerator, _denominator);
        _numerator /= gcd;
        _denominator /= gcd;
    }
}

class MathUtils {
    private static long abs(long x) {
        return x > 0 ? x : -x;
    }

    static long utilGcd(long num1, long num2)
    {
        long tmp;
        num1 = abs(num1);
        num2 = abs(num2);
        while (num1 > 0) {
            tmp = num1;
            num1 = num2 % num1;
            num2 = tmp;
        }
        return num2;
    }
}
