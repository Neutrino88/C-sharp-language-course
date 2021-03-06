﻿using System;

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

        public static ComplexNumber Sum(params ComplexNumber[] nums)
        {
            ComplexNumber result = new ComplexNumber(0, 0);

            foreach(var num in nums)
            {
                result += num;
            }

            return result;
        }
        public static ComplexNumber Mul(params ComplexNumber[] nums)
        {
            ComplexNumber result = new ComplexNumber(1, 0);

            foreach (var num in nums)
            {
                result *= num;
            }

            return result;
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

        public static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            return (a.rPart == b.rPart) && (a.iPart == b.iPart);
        }
        public static bool operator !=(ComplexNumber a, ComplexNumber b)
        {
            return (a.rPart != b.rPart) || (a.iPart != b.iPart);
        }

        public static ComplexNumber operator +(ComplexNumber num)
        {
            return num;
        }
        public static ComplexNumber operator -(ComplexNumber num)
        {
            return new ComplexNumber(-num.rPart, -num.iPart);
        }

        public static explicit operator double(ComplexNumber num)
        {
            return Math.Sqrt(Math.Pow(num.rPart, 2) + Math.Pow(num.iPart, 2));
        }

        public ComplexNumber Sum(ComplexNumber num)
        {
            return (this + num);
        }
        public ComplexNumber Sub(ComplexNumber num)
        {
            return (this - num);
        }
        public ComplexNumber Mul(ComplexNumber num)
        {
            return (this * num);
        }
        public ComplexNumber Div(ComplexNumber num)
        {
            return (this / num);
        }
        public double Abs()
        {
            return (double)this;
        }

        public ComplexNumber SumNums(params ComplexNumber[] nums)
        {
            ComplexNumber result = this;

            foreach (var num in nums)
            {
                result += num;
            }

            return result;
        }
        public ComplexNumber MulNums(params ComplexNumber[] nums)
        {
            ComplexNumber result = this;

            foreach (var num in nums)
            {
                result *= num;
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                return this == (ComplexNumber)obj;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (int) (this.iPart * 1000000) + (int) (this.rPart * 1000) ;
        }

        public override string ToString()
        {
            return $"{rPart} + {iPart}i";
        }
    }
}
