﻿using System;
using System.Reflection;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            new NUnit.Framework.Internal.TestExecutionContext().EstablishExecutionEnvironment();

            Assembly TestDll = Assembly.LoadFrom("MyLinkedListTests.dll");

            foreach (var type in TestDll.GetTypes())
            {
                if (type.GetMethods().Length > 4)
                {
                    PrintTestsResults(type, RunTestsInClass(type));
                }
            }

            Console.ReadKey();
        }

        static Dictionary<string, bool> RunTestsInClass(Type testClass)
        {
            Dictionary<string, bool> results = new Dictionary<string, bool>();

            Object obj = Activator.CreateInstance(testClass);
            MethodInfo setUpMethod = testClass.GetMethod("SetUp");
            setUpMethod?.Invoke(obj, null);

            foreach (var method in testClass.GetMethods())
            {
                try
                {
                    if (method.IsDefined(typeof(TestAttribute)))
                    {
                        method.Invoke(obj, null);
                        results.Add(method.Name, true);
                    }
                } catch (System.Reflection.TargetInvocationException e)
                {
                    results.Add(method.Name, false);
                }
            }

            return results;
        }

        static void PrintTestsResults(Type testClass, Dictionary<string, bool> results)
        {
            Console.ForegroundColor = ConsoleColor.White;

            if (results.Count > 0)
            {
                Console.WriteLine(testClass);
            }

            foreach(var item in results)
            {
                Console.ResetColor();
                Console.Write("  " + item.Key + " - ");

                if (item.Value)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                } else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine(item.Value ? "OK" : "Failed");
            }
        }
    }
}
