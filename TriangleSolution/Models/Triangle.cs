using System;
using System.Linq;

namespace TriangleSolution.Models
{
    public class Triangle
    {
        public double Side1 { get; set; }
        public double Side2 { get; set; }
        public double Side3 { get; set; }

        public Triangle() { }

        public Triangle(double side1, double side2, double side3)
        {
            Side1 = side1;
            Side2 = side2;
            Side3 = side3;
        }

        public bool IsValid()
        {
            var sides = GetSortedSides();
            return sides[0] > 0 && 
                   sides[1] > 0 && 
                   sides[2] > 0 && 
                   (sides[0] + sides[1] > sides[2]);
        }

        public double[] GetSortedSides()
        {
            var sides = new[] { Side1, Side2, Side3 };
            Array.Sort(sides);
            return sides;
        }

        public double Perimeter()
        {
            return Side1 + Side2 + Side3;
        }

        public double Area()
        {
            double p = Perimeter() / 2.0;
            return Math.Sqrt(p * (p - Side1) * (p - Side2) * (p - Side3));
        }

        public bool IsRightAngled()
        {
            var s = GetSortedSides();
            double c2 = s[2] * s[2];
            double ab2 = s[0] * s[0] + s[1] * s[1];
            return AlmostEqual(c2, ab2);
        }

        public bool IsEquilateral()
        {
            var s = GetSortedSides();
            return AlmostEqual(s[0], s[1]) && AlmostEqual(s[1], s[2]);
        }

        public bool IsIsosceles()
        {
            var s = GetSortedSides();
            return AlmostEqual(s[0], s[1]) || AlmostEqual(s[1], s[2]);
        }

        public bool IsCongruentTo(Triangle other)
        {
            var s1 = GetSortedSides();
            var s2 = other.GetSortedSides();
            return AlmostEqual(s1[0], s2[0]) &&
                   AlmostEqual(s1[1], s2[1]) &&
                   AlmostEqual(s1[2], s2[2]);
        }

        public bool IsSimilarTo(Triangle other)
        {
            var s1 = GetSortedSides();
            var s2 = other.GetSortedSides();

            double ratio1 = s1[0] / s2[0];
            double ratio2 = s1[1] / s2[1];
            double ratio3 = s1[2] / s2[2];

            return AlmostEqual(ratio1, ratio2) && AlmostEqual(ratio2, ratio3);
        }

        public string GetInfo()
        {
            var s = GetSortedSides();
            double p = Perimeter();
            
            return $"Triangle:\n" +
                   $"({s[0]}, {s[1]}, {s[2]})\n" +
                   $"Reduced:\n" +
                   $"({s[0] / p}, {s[1] / p}, {s[2] / p})\n\n" +
                   $"Area = {Area()}\n" +
                   $"Perimeter = {p}";
        }

        public static bool AlmostEqual(double a, double b)
        {
            if (a == 0 && b == 0) return true;
            return Math.Abs(a - b) < (0.00001 * a);
        }
    }
}