using DataStructureLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using static EFDemo.EventsDemo;

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
        private static int PrivateStatic;
        private int Private;
        protected int Protected;
        protected static int ProtectedStatic;
        public int Public;
        public static int PublicStatic;
        internal int Internal;
        internal static int InternalStatic;

        public int GetProtected()
        {
            return Protected;
        }

        public void SetProtected(int @protected)
        {
            Protected = @protected;
        }

        public int PublicProperty
        {
            get => Private;
            set
            {
                Private = value;
            }
        }

        public ForChange()
        {
            Private = 10;
        }

        public void Print()
        {
            Console.WriteLine(Private);
        }
    }

    public class Car
    {
        public virtual int GetMaxSpeed()
        {
            return 200;
        }

        public void Test(int a = 5, int b = 10, int c = 100)
        {

        }

        /// <summary>
        /// Sum all items
        /// </summary>
        /// <param name="test">Some input string</param>
        /// <param name="data">Data array of ints</param>
        /// <returns></returns>
        public int Sum(string test, params int[] data)
        {
            return data.Sum();
        }
    }

    public class Ferrari : Car
    {
        public override int GetMaxSpeed()
        {
            return 500;
        }
    }

    public class Gelly : Car
    {
        //
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

        static string GetData()
        {
            return random.NextDouble() > 0.9 ? "OK" : "Fail";
        }

        static bool RepeatActionUntilConditionIsFalse(Action action, Func<bool> condition, int count = 3)
        {
            bool result;
            do
            {
                action();
                result = condition();
                //timeout
            } while (!result && --count > 0);

            return result;
        }
        static Printer printer = new Printer();
        static void Main()
        {
            Random random = new Random();

            printer.IncLeak += Printer_IncLeak;
            for (int i = 0; i < 10; i++)
            {
                var document = new Document();
                document.Text = random.Next().ToString();
                document.Title = random.Next().ToString();
                printer.PrintDocument(document);
            }
        }

        private static void Printer_IncLeak(string incColor)
        {
            printer.Refill(incColor);
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
