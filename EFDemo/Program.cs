using DataStructureLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EFDemo
{
    public class TestClass
    {
        public String Name;
        private Object[] values = new Object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public Object this[int index]
        {
            get
            {
                return values[index];
            }
            set
            {
                values[index] = value;
            }
        }

        public Object Value
        {
            get
            {
                return "the value";
            }
        }

        public TestClass() : this("initialName") { }
        public TestClass(string initName)
        {
            Name = initName;
        }

        int methodCalled = 0;

        public static void SayHello()
        {
            Console.WriteLine("Hello");
        }

        public void AddUp()
        {
            methodCalled++;
            Console.WriteLine("AddUp Called {0} times", methodCalled);
        }

        public static double ComputeSum(double d1, double d2)
        {
            return d1 + d2;
        }

        public static void PrintName(String firstName, String lastName)
        {
            Console.WriteLine("{0},{1}", lastName, firstName);
        }

        public void PrintTime()
        {
            Console.WriteLine(DateTime.Now);
        }

        public void Swap(ref int a, ref int b)
        {
            int x = a;
            a = b;
            b = x;
        }
    }

    [DefaultMemberAttribute("PrintTime")]
    public class TestClass2
    {
        public void PrintTime()
        {
            Console.WriteLine(DateTime.Now);
        }
    }

    public class Base
    {
        static int BaseOnlyPrivate = 0;
        protected static int BaseOnly = 0;
    }
    public class Derived : Base
    {
        public static int DerivedOnly = 0;
    }
    public class MostDerived : Derived { }

    public class ForChange
    {
        private int Test;
        public ForChange()
        {
            Test = 10;
        }

        public void Print()
        {
            Console.WriteLine(Test);
        }
    }
    class Program
    {
        static Random random = new Random();

        static string GetRandomString()
        {
            var length = random.Next(5, 20);
            var sb = new StringBuilder(string.Empty);
            for (int i = 0; i < length; i++)
            {
                sb.Append((char)random.Next('a', 'z'));
            }

            return sb.ToString();
        }

        static void Test()
        {
            using (var db = new BloggingContext())
            {
                var response = db.GoodsOnWarehouses
                    .Include(x => x.Good)
                    .GroupBy(x => x.GoodId)
                    .Select(x => new GoodSummary
                    {
                        Id = x.Key,
                        Title = db.Goods.Where(y => y.Id == x.Key).Select(y => y.Title).First(),
                        Count = x.Sum(x => x.Count)
                    })
                    .ToList();
                foreach (var item in response)
                {
                    Console.WriteLine(item);
                }
            }
        }

        static void Main()
        {
            ForChange forChange = new ForChange();
            var fields = forChange.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach(var field in fields)
            {
                Console.WriteLine(field);
            }

            var myFieldInfo = forChange.GetType().GetField("Test",
            BindingFlags.NonPublic | BindingFlags.Instance);
            myFieldInfo.SetValue(forChange, 55);
            forChange.Print();
            // BindingFlags.InvokeMethod
            // Call a static method.
            Type t = typeof(TestClass);

            Console.WriteLine();
            Console.WriteLine("Invoking a static method.");
            Console.WriteLine("-------------------------");
            t.InvokeMember("SayHello", BindingFlags.InvokeMethod | BindingFlags.Public |
                BindingFlags.Static, null, null, new object[] { });

            // BindingFlags.InvokeMethod
            // Call an instance method.
            TestClass c = new TestClass();
            Console.WriteLine();
            Console.WriteLine("Invoking an instance method.");
            Console.WriteLine("----------------------------");
            c.GetType().InvokeMember("AddUp", BindingFlags.InvokeMethod, null, c, new object[] { });
            c.GetType().InvokeMember("AddUp", BindingFlags.InvokeMethod, null, c, new object[] { });

            // BindingFlags.InvokeMethod
            // Call a method with parameters.
            object[] args = new object[] { 100.09, 184.45 };
            object result;
            Console.WriteLine();
            Console.WriteLine("Invoking a method with parameters.");
            Console.WriteLine("---------------------------------");
            result = t.InvokeMember("ComputeSum", BindingFlags.InvokeMethod, null, null, args);
            Console.WriteLine("{0} + {1} = {2}", args[0], args[1], result);

            // BindingFlags.GetField, SetField
            Console.WriteLine();
            Console.WriteLine("Invoking a field (getting and setting.)");
            Console.WriteLine("--------------------------------------");
            // Get a field value.
            result = t.InvokeMember("Name", BindingFlags.GetField, null, c, new object[] { });
            Console.WriteLine("Name == {0}", result);
            // Set a field.
            t.InvokeMember("Name", BindingFlags.SetField, null, c, new object[] { "NewName" });
            result = t.InvokeMember("Name", BindingFlags.GetField, null, c, new object[] { });
            Console.WriteLine("Name == {0}", result);

            Console.WriteLine();
            Console.WriteLine("Invoking an indexed property (getting and setting.)");
            Console.WriteLine("--------------------------------------------------");
            // BindingFlags.GetProperty
            // Get an indexed property value.
            int index = 3;
            result = t.InvokeMember("Item", BindingFlags.GetProperty, null, c, new object[] { index });
            Console.WriteLine("Item[{0}] == {1}", index, result);
            // BindingFlags.SetProperty
            // Set an indexed property value.
            index = 3;
            t.InvokeMember("Item", BindingFlags.SetProperty, null, c, new object[] { index, "NewValue" });
            result = t.InvokeMember("Item", BindingFlags.GetProperty, null, c, new object[] { index });
            Console.WriteLine("Item[{0}] == {1}", index, result);

            Console.WriteLine();
            Console.WriteLine("Getting a field or property.");
            Console.WriteLine("----------------------------");
            // BindingFlags.GetField
            // Get a field or property.
            result = t.InvokeMember("Name", BindingFlags.GetField | BindingFlags.GetProperty, null, c,
                new object[] { });
            Console.WriteLine("Name == {0}", result);
            // BindingFlags.GetProperty
            result = t.InvokeMember("Value", BindingFlags.GetField | BindingFlags.GetProperty, null, c,
                new object[] { });
            Console.WriteLine("Value == {0}", result);

            Console.WriteLine();
            Console.WriteLine("Invoking a method with named parameters.");
            Console.WriteLine("---------------------------------------");
            // BindingFlags.InvokeMethod
            // Call a method using named parameters.
            object[] argValues = new object[] { "Mouse", "Micky" };
            String[] argNames = new String[] { "lastName", "firstName" };
            t.InvokeMember("PrintName", BindingFlags.InvokeMethod, null, null, argValues, null, null,
                argNames);

            Console.WriteLine();
            Console.WriteLine("Invoking a default member of a type.");
            Console.WriteLine("------------------------------------");
            // BindingFlags.Default
            // Call the default member of a type.
            Type t3 = typeof(TestClass2);
            t3.InvokeMember("", BindingFlags.InvokeMethod | BindingFlags.Default, null, new TestClass2(),
                new object[] { });

            // BindingFlags.Static, NonPublic, and Public
            // Invoking a member with ref parameters.
            Console.WriteLine();
            Console.WriteLine("Invoking a method with ref parameters.");
            Console.WriteLine("--------------------------------------");
            MethodInfo m = t.GetMethod("Swap");
            args = new object[2];
            args[0] = 1;
            args[1] = 2;
            m.Invoke(new TestClass(), args);
            Console.WriteLine("{0}, {1}", args[0], args[1]);

            // BindingFlags.CreateInstance
            // Creating an instance with a parameterless constructor.
            Console.WriteLine();
            Console.WriteLine("Creating an instance with a parameterless constructor.");
            Console.WriteLine("------------------------------------------------------");
            object cobj = t.InvokeMember("TestClass", BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.CreateInstance,
                null, null, new object[] { });
            Console.WriteLine("Instance of {0} created.", cobj.GetType().Name);

            // Creating an instance with a constructor that has parameters.
            Console.WriteLine();
            Console.WriteLine("Creating an instance with a constructor that has parameters.");
            Console.WriteLine("------------------------------------------------------------");
            cobj = t.InvokeMember("TestClass", BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.CreateInstance,
                null, null, new object[] { "Hello, World!" });
            Console.WriteLine("Instance of {0} created with initial value '{1}'.", cobj.GetType().Name,
                cobj.GetType().InvokeMember("Name", BindingFlags.GetField, null, cobj, null));

            // BindingFlags.DeclaredOnly
            Console.WriteLine();
            Console.WriteLine("DeclaredOnly instance members.");
            Console.WriteLine("------------------------------");
            System.Reflection.MemberInfo[] memInfo =
                t.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance |
                BindingFlags.Public);
            for (int i = 0; i < memInfo.Length; i++)
            {
                Console.WriteLine(memInfo[i].Name);
            }

            // BindingFlags.IgnoreCase
            Console.WriteLine();
            Console.WriteLine("Using IgnoreCase and invoking the PrintName method.");
            Console.WriteLine("---------------------------------------------------");
            t.InvokeMember("printname", BindingFlags.IgnoreCase | BindingFlags.Static |
                BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[]
                {"Brad","Smith"});

            // BindingFlags.FlattenHierarchy
            Console.WriteLine();
            Console.WriteLine("Using FlattenHierarchy to get inherited static protected and public members.");
            Console.WriteLine("----------------------------------------------------------------------------");
            FieldInfo[] finfos = typeof(MostDerived).GetFields(BindingFlags.NonPublic | BindingFlags.Public |
                  BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (FieldInfo finfo in finfos)
            {
                Console.WriteLine("{0} defined in {1}.", finfo.Name, finfo.DeclaringType.Name);
            }

            Console.WriteLine();
            Console.WriteLine("Without FlattenHierarchy.");
            Console.WriteLine("-------------------------");
            finfos = typeof(MostDerived).GetFields(BindingFlags.NonPublic | BindingFlags.Public |
                  BindingFlags.Static);
            foreach (FieldInfo finfo in finfos)
            {
                Console.WriteLine("{0} defined in {1}.", finfo.Name, finfo.DeclaringType.Name);
            }
        }

        //MyArray<int> myArray = new MyArray<int>();
        //MethodInfo[] mInfos = myArray.GetType().GetMethods();
        //foreach (var item in mInfos)
        //{
        //    Console.WriteLine(item);
        //}


        //User[] users = new User[3];
        //users[0] = new User
        //{
        //    FirstName = "BBBB",
        //    Age = 50
        //};
        //users[1] = new User
        //{
        //    FirstName = "CCCC",
        //    Age = 30
        //};
        //users[2] = new User
        //{
        //    FirstName = "AAAA",
        //    Age = 22
        //};
        //Func<User, User, int> comparer = SortByFN;
        //Predicate<int> predicate = IsPositive;

        //Sort(users, comparer);

        //foreach (var item in Filter(users, OlderThanTwentyFive))
        //{
        //    Console.WriteLine(item.FirstName + " " + item.Age);
        //}


        //Action<string> Print = Console.WriteLine;
        //Func<string> Read = Console.ReadLine;
        //Print(Read());
        //foreach (var item in users)
        //{
        //    Console.WriteLine(item.FirstName + " " + item.Age);
        //}
        //Random random = new Random();

        static void Sort<T>(T[] items, Func<T, T, int> comparer)
        {
            if (items == null || comparer == null)
            {
                throw new ArgumentException(nameof(items));
            }

            for (int i = 0; i < items.Length - 1; i++)
            {
                for (int j = i + 1; j < items.Length; j++)
                {
                    if (comparer(items[i], items[j]) == 1)
                    {
                        Swap(ref items[i], ref items[j]);
                    }
                }
            }
        }

        static IEnumerable<T> Filter<T>(IEnumerable<T> items, Predicate<T> predicate)
        {
            if (items == null || predicate == null)
            {
                throw new ArgumentException(nameof(items));
            }

            foreach (var item in items)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        //static void Main()
        //{
        //    method1 objDel2;
        //    objDel2 = new method1(TestMethod1);
        //    objDel2("test");
        //    objDel2.Invoke("Invoke");
        //}

        delegate void method1(string val);
        static void TestMethod1(string val)
        {
            Console.WriteLine(val);
        }
        //1,-1,0
        static int SortByAge(User a, User b)
        {
            return a.Age.CompareTo(b.Age);
        }

        static int SortByFN(User a, User b)
        {
            return a.FirstName.CompareTo(b.FirstName);
        }

        static bool IsPositive(int a)
        {
            return a > 0;
        }

        static bool OlderThanTwentyFive(User user)
        {
            return user.Age > 25;
        }

        delegate void Print(string str);
        delegate string Read();
    }
}
