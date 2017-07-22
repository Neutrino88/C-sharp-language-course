using System;
using System.Collections.Generic;

namespace MyUnitTestingLibrary
{
    public static class CollectionTest
    {
        public static void AreEqual(ICollection<Object> a, ICollection<Object> b)
        {
            if ( Object.Equals(a, b) )
            {
                throw new Exception("Collections aren't equal");
            } 
        }

        public static void AreEqual(IReadOnlyCollection<Object> a, IReadOnlyCollection<Object> b)
        {
            if ( !Object.Equals(a, b) )
            {
                throw new Exception("Collections aren't equal");
            }
        }
    }
}
