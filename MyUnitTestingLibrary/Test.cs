using System;

namespace MyUnitTestingLibrary
{
    public static class Test
    {
        public static void AreEqual(Object a, Object b)
        {
            if (!Object.Equals(a, b))
            {
                throw new Exception("Objects aren't equal");
            }
        }

        public static void AreNotEqual(Object a, Object b)
        {
            if (Object.Equals(a, b))
            {
                throw new Exception("Objects are equal");
            }
        }

        public static void IsNull(Object a)
        {
            if (a != null)
            {
                throw new Exception("Object isn't null");
            }
        }

        public static void LessOrEqual(IComparable a, IComparable b)
        {
            if (a.CompareTo(b) > 0)
            {
                throw new Exception("Object is more");
            }
        }

        public static void Less(IComparable a, IComparable b)
        {
            if (a.CompareTo(b) >= 0)
            {
                throw new Exception("Object is equal or more");
            }
        }
    }
}
