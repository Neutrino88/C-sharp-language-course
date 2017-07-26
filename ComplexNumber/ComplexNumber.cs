using System;

namespace CompNumber
{
    public struct ComplexNumber
    {
        public double rPart { get; private set; }
        public double iPart { get; private set; }

        public ComplexNumber(double realPart, double imagPart)
        {
            this.rPart = realPart;
            this.iPart = imagPart;
        }

        public static ComplexNumber Sum(ComplexNumber a, ComplexNumber b)
        {
            return a + b;
        }
        public static ComplexNumber Sub(ComplexNumber a, ComplexNumber b)
        {
            return a - b;
        }
        public static ComplexNumber Mul(ComplexNumber a, ComplexNumber b)
        {
            return a * b;
        }
        public static ComplexNumber Div(ComplexNumber a, ComplexNumber b)
        {
            return a / b;
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.rPart + b.rPart, a.iPart + b.iPart);
        }
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.rPart - b.rPart, a.iPart - b.iPart);
        }
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            double realPart = (a.rPart * b.rPart) - (a.iPart * b.iPart);
            double imagPart = (a.rPart * b.iPart) + (a.iPart * b.rPart);

            return new ComplexNumber(realPart, imagPart);
        }
        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
        {
            double denominator = (Math.Pow(b.rPart, 2) + Math.Pow(b.iPart, 2));

            double realPart = ((a.rPart * b.rPart) + (a.iPart * b.iPart)) / denominator;
            double imagPart = ((a.rPart * b.iPart) - (a.iPart * b.rPart)) / denominator;

            return new ComplexNumber(realPart, imagPart);
        }

        public override string ToString()
        {
            return $"{rPart} + {iPart}i";
        }
    }
}
