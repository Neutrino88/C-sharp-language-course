using System;

namespace MyUnitTestingLibrary
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TestClassAttribute : System.Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Method)]
    public class TestInitAttribute : System.Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Method)]
    public class TestAttribute : System.Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Method)]
    public class TestCleanUpAttribute : System.Attribute
    {

    }
}
