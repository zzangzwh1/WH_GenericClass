//*************************************************************************************************
//Description:  Extension Methods, Gererics, and Indexers
//Date:         Jan. 7/2022
//Author:       Wonhyuk Cho
//Course:       CMPE2800
//Instructor:   Simon Walker
//*************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICA1_CMPE2800_Extension_Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            Random _rnd = new Random();
            //PART A

            Console.WriteLine("PART A --------public static Dictionary<int, int> Categorize(this List<int> sourcelist)");
            List<int> iNums = new List<int>(new int[] { 4, 12, 4, 3, 5, 6, 7, 6, 12 });
            foreach (KeyValuePair<int, int> scan in iNums.Categorize())
                Console.WriteLine($"{scan.Key:d3} x {scan.Value:d5}");

            //PART B
            Console.WriteLine("PART B--------public static Dictionary<T, int> Categorize<T>(this List<T> sourcelist)");
            List<string> names = new List<string>(new string[] {
            "Rick", "Glenn", "Rick", "Carl", "Michonne", "Rick", "Glenn" });
            foreach (KeyValuePair<string, int> scan in names.Categorize())
                Console.WriteLine($"{scan.Key} x {scan.Value:d5}");

            //PART C
            Console.WriteLine("Part C-----public static Dictionary<K, int> Categorize<K>(this IEnumerable<K> sourcelist)");

            LinkedList<char> llfloats = new LinkedList<char>();
            while (llfloats.Count < 1000)
                llfloats.AddLast((char)_rnd.Next('A', 'Z' + 1));
            foreach (KeyValuePair<char, int> scan in llfloats.Categorize())
                Console.WriteLine($"{scan.Key} x {scan.Value:d5}");
            string TestString = "This is the test string, do not panic!";
            foreach (KeyValuePair<char, int> scan in TestString.Categorize())
                Console.WriteLine($"{scan.Key} x {scan.Value:d5}");

            //PART D
            Console.WriteLine("Part D---Rando");

            List<int> testdataA = new List<int>(new int[] { 0, 1, 2, 3, 4, 5 });
            for (int i = 0; i < 10; ++i)
                Console.WriteLine($"Rando from list : {testdataA.Rando()}");


            Dictionary<string, int> testdataB = new Dictionary<string, int>();
            testdataB.Add("First", 1);
            testdataB.Add("Second", 2);
            testdataB.Add("Third", 3);
            testdataB.Add("Forth", 4);
            testdataB.Add("Fifth", 5);
            for (int i = 0; i < 10; ++i)
                Console.WriteLine($"Rando from dictionary : {testdataB.Rando()}");
            Console.WriteLine($"AdjacentDuplicate with no duplicate :{ testdataA.AdjacentDuplicate()}");
       /*     testdataA.Add(3);
            testdataA.Add(5);*/
            testdataA.Add(5);
            testdataA.Add(7);
            Console.WriteLine($"AdjacentDuplicate with duplicate (5):{ testdataA.AdjacentDuplicate()}");
            List<float> testdataC = new List<float>();
            while (testdataC.Count < 10)
                testdataC.Add((float)(_rnd.NextDouble() * 10));
            foreach (float f in testdataC.ToOrderedLinkedList())
                Console.WriteLine($"OrderedLL from List : {f}");

            CatStack<string> myStack = new CatStack<string>();
            myStack.Push("this");
            myStack.Push("is");
            myStack.Push("what");
            myStack.Push("this");
            myStack.Push("is");
            Console.WriteLine($"Cat element 1 : {myStack[1]}");
            Console.WriteLine($"Cat count 'is' : {myStack["is"]}");

            Console.ReadLine();
        }
    }
    //extension methods
    public static class Extension
    {
        //initiliaze Random
        static Random rnd = new Random();
        /// <summary>
        ///  public static Dictionary<int, int> Categorize(this List<int> sourcelist)
        /// Uses int value in list of int as key and its frequency as value
        /// </summary>
        /// <param name="sourceList">list of int to be categorized</param>
        /// <returns>categorized dictionary</returns>
        public static Dictionary<int, int> Categorize(this List<int> sourcelist)
        {
            //create a dictionary to retrun
            Dictionary<int, int> val = new Dictionary<int, int>();
            //iterate trhough the list
            sourcelist.ForEach(i =>
            {
                //if the element is not in the dictionary
                if (!val.ContainsKey(i))
                    //add it with a count 1
                    val.Add(i, 1);
                else
                    //otherwise increase count by 1
                    ++val[i];
            });
            //order the list by ascending order of key
            val = val.OrderBy(kvp => kvp.Key).ToDictionary(k => k.Key, v => v.Value);

            return val;
        }
        /// <summary>
        /// public static Dictionary<T, int> Categorize<T>(this List<T> sourcelist)
        /// Used any type of object in list of generic type as key and its frequency as value
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="sourceList">list of generic type to be categorized</param>
        /// <returns>categorized dictionary</returns>
        public static Dictionary<T, int> Categorize<T>(this List<T> sourcelist)
        {
            //create a dictionary to return
            Dictionary<T, int> val = new Dictionary<T, int>();
            //iterate through the list
            sourcelist.ForEach(i =>
            {
                //if the element is not in the dictionary
                if (!val.ContainsKey(i))
                    //add it with a count 1
                    val.Add(i, 1);
                else
                    //otherwise increate the count by 1
                    ++val[i];
            });
            //order the list by ascending order of key
            val = val.OrderBy(kvp => kvp.Key).ToDictionary(k => k.Key, v => v.Value);

            return val;
        }
        /// <summary>     
        /// public static Dictionary<K, int> Categorize<K>(this IEnumerable<K> sourcelist)
        /// Uses generic type of element in any collection as key and its frequency as value
        /// </summary>
        /// <typeparam name="K">generic type</typeparam>
        /// <param name="sourceList">collection to be categorized</param>
        /// <returns>categorized dictionary</returns>
        public static Dictionary<K, int> Categorize<K>(this IEnumerable<K> sourcelist)
        {
            //create a dictionary to return
            Dictionary<K, int> val = new Dictionary<K, int>();
            //iterate through the list
            sourcelist.ToList().ForEach(i => {
                // if the element is not in the dictionary
                if (!val.ContainsKey(i))
                    //add it with a count 1
                    val.Add(i, 1);
                else
                    //otherwise increase the count by 1
                    ++val[i];
            });
            // order the list by ascending order of key
            val = val.OrderBy(kvp => kvp.Key).ToDictionary(k => k.Key, v => v.Value);

            return val;
        }
        /// <summary>
        ///   public static T Rando<T>(this IEnumerable<T> sourcelist)
        ///   Must operate on any IEnumerable. Will return a random element from the source collection. If
        //      the collection is empty, throw a suitable ArgumentException.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourcelist">IEnumerable</param>
        /// <returns></returns>
        public static T Rando<T>(this IEnumerable<T> sourcelist)
        {
            // If the collection is empty, throw a suitable ArgumentException.
            if (!sourcelist.Any()) throw new ArgumentException("Error");
            // Will return a random element from the source collection.
            return sourcelist.ElementAt(rnd.Next(0, sourcelist.Count()));

        }
        /// <summary>
        ///  public static (T, K) Rando<T, K>(this Dictionary<T, K> sourcelist)\
        ///  Must operate on any Dictionary. Will return a random element from the source dictionary as a
        //tuple, with key and val as field names.If the collection is empty, throw a suitable ArgumentException.
        /// </summary>
        /// <typeparam name="T">Tuple T</typeparam>
        /// <typeparam name="K">Tuple K</typeparam>
        /// <param name="sourcelist"> Dictionary</param>
        /// <returns></returns>
        public static (T, K) Rando<T, K>(this Dictionary<T, K> sourcelist)
        {
            // If the collection is empty, throw a suitable ArgumentException.
            if (!sourcelist.Any()) throw new ArgumentException("Error");

            //Will return a random element from the source dictionary as a
            //tuple, with key and val as field names
            int randNum = rnd.Next(0, sourcelist.Count());
            return (sourcelist.ElementAt(randNum).Key, sourcelist.ElementAt(randNum).Value);



        }
        /// <summary>
        ///  public static T AdjacentDuplicate<T>(this IEnumerable<T> sourcelist)
        ///  – Must operate on any IEnumerable. Will return the first instance of an adjacent pair
        // of values encountered in the collection.If no adjacent pairs are found, return the default for the generic
        // type.Suitable constraint(s) are required for this method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourcelist"></param>
        /// <returns></returns>
        public static T AdjacentDuplicate<T>(this IEnumerable<T> sourcelist)
        {

            // IEnumerable<T> temp = sourcelist;
            //count sourcelist
            for (int i = 0; i < sourcelist.Count(); ++i)
            {
                //count temp(IEnumberable<T>)
                for (int j = 0; j < sourcelist.Count(); ++j)
                {
                    //when temp and source is same and different index 
                    if (sourcelist.ElementAt(j).Equals(sourcelist.ElementAt(i)) && i != j)
                        //Will return the first instance of an adjacent pair
                        //of values encountered in the collection
                        return sourcelist.ElementAt(i);

                }
            }
            // return the default for the generic type
            return sourcelist.ElementAt(default);
        }
        /// <summary>
        ///  public static LinkedList<T> ToOrderedLinkedList<T>(this IEnumerable<T> sourcelist) where T : IComparable
        ///  – Must operate on any IEnumerable with IComparable elements. This method will
        //return a LinkedList with the same generic type.You must manually order the linked list.You may not
        //use any framework methods that would provide ordering.This will require quadratic iteration and
        //insertion of the new values at the appropriate positions in the linked list.
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <param name="sourcelist">IEnmberable</param>
        /// <returns></returns>
        public static LinkedList<T> ToOrderedLinkedList<T>(this IEnumerable<T> sourcelist) where T : IComparable
        {
            LinkedList<T> temp = new LinkedList<T>();
        
            //check every elements in sourcelist
            foreach (T tem in sourcelist)
            {
                //when it sequence doesnt contain any element
                if (!temp.Any())
                    //add first
                    temp.AddFirst(tem);
                else
                {
                    //listnode point as temp's first
                    LinkedListNode<T> point = temp.First;                  
                   
                    for(int i =0; i<temp.Count(); i++)
                    {
                        // compare tem and temp's element at whether it is greater than 0
                        if (tem.CompareTo(temp.ElementAt(i)) > 0)
                        {
                            //when listnode point is not null then point is next
                            if (point.Next != null)
                                point = point.Next;
                        }
                    }
                    
                    if (point.Next != null && point.Previous != null)
                        temp.AddBefore(point, tem);
                    //when the element is max add last
                    else if (tem.CompareTo(temp.Max()) > 0)
                        temp.AddLast(tem);
                    //when the element is min then add first
                    else if (tem.CompareTo(temp.Min()) < 0)
                        temp.AddFirst(tem);
                }
            }
          
            return temp;


        }

    }
    //Generice class dervied from stack
    public class  CatStack<F> : Stack<F>
    {
      
        public F this[int index]
        {
            //Take an int, return the key that is the nth categorized element (throw an
            //IndexOutOfRangeException if the index is invalid with respect to the Categorize return count)
            get
            {
                if (index >= this.Count)
                    throw new IndexOutOfRangeException();
                else return this.ElementAt(index);
            }
        }
        public int this[F key]
        {            
            get
            {
               // Take a key, return the count for that categorized element(throw an ArgumentException if the
               //key does not exist in the Categorize return collection) 
                if (!this.Contains(key))
                    throw new ArgumentException("Key does not exits");
                else
                {
                    int n = 0;
                    
                    for(int i =0; i<this.Count; ++i)
                    {
                        //when index i equals key then count up
                        if (this.ElementAt(i).Equals(key))
                            n += 1;
                    }
                    return n;
                }
            }
        }


    }

}
