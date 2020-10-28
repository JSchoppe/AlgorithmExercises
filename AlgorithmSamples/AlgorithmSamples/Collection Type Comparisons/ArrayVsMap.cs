using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Collection_Type_Comparisons
{
    public static class ArrayVsMap
    {
        private static void Example()
        {
            // These four collection types are all provide
            // the same functionality: use an index to access
            // string data. They each have applicable use cases.
            string[] namesArray = new string[0];
            List<string> namesList = new List<string>();
            Dictionary<int, string> namesDictionary = new Dictionary<int, string>();
            Hashtable namesHashtable = new Hashtable();
            
            // Arrays are useful for simple enumeration and storage of
            // collections with fixed sizes. They can be annoying to
            // manage if you are constantly changing their size:
            Array.Resize(ref namesArray, namesArray.Length + 1);
            namesArray[0] = "charlie";
            // You might use an array to print a credits list:
            Console.WriteLine("--Contributors--");
            for (int i = 0; i < namesArray.Length; i++)
                Console.WriteLine(namesArray[i]);

            // Lists are functionally very similar to arrays but
            // are designed with resizeability in mind. The List
            // will resize itself and reorder elements when you
            // add or delete from it. List are also best for simple
            // enumeration. They should not be used if you have
            // to access specific elements very often.
            namesList.Add("david");
            // You might use a list to store recipients of a newsletter.
            foreach (string email in namesList)
                SendEmailMock(email, "buy buy buy!");

            // Dictionaries deviate from arrays and lists as they
            // implement hashtables under the hood. Dictionaries are
            // designed for fast access. Dictionaries should not be
            // used if they are primarily enumerated through. In that
            // case a class or struct is a better way to link values.
            // Any value of the dictionary type can be used as a key.
            // So you could tie a contact to their phone number
            // without having to worry about have a large array:
            namesDictionary.Add(1114432, "mikey");
            // You might use a dictionary to access user information based on a numerical id.
            Console.WriteLine($"{namesDictionary[ReadIDCardMock()]} clocked in at {DateTime.Now.ToShortTimeString()}");

            // Hashtables are similar to dictionaries, except that they
            // are not typesafe. This also means that they introduce some
            // overhead in type checking (boxing/unboxing). The benefit of
            // the hashtable is that it is thread-safe, meaning that you can
            // having multiple threads accessing it at once. This is relevent
            // if you are tackling large tasks using parallel processing.
            namesHashtable.Add(187915, "anna");
            // You might use a hashtable to quickly check and remove data from a collection.
            int[] keys = new int[namesHashtable.Count];
            namesHashtable.Keys.CopyTo(keys, 0);
            Parallel.For(0, keys.Length, (index) =>
            {
                // Remove all users whose names are palindromes!!!
                string name = (string)namesHashtable[keys[index]];
                bool isPalindrome = true;
                for (int i = 0; i < name.Length / 2; i++)
                    if (name[i] != name[name.Length - 1 - i])
                        isPalindrome = false;
                if (isPalindrome)
                    namesHashtable.Remove(keys[index]);
            });
            // (excuse my poor implementation of parallelization to force an example)
            // The idea is that time consuming operations that need access
            // to the hashtable can be done concurrently.
        }

        public static void DemoEnumeration()
        {
            Stopwatch stopwatch = new Stopwatch();

            // A scenario where you should use an array/list:
            List<MockUpdateEntity> listUpdaters =
                new List<MockUpdateEntity>();
            for (int i = 0; i < 1000000; i++)
                listUpdaters.Add(new MockUpdateEntity());
            // Dictionary consumes more memory.
            Dictionary<int, MockUpdateEntity> dictionaryUpdaters =
                new Dictionary<int, MockUpdateEntity>();
            for (int i = 0; i < 1000000; i++)
                dictionaryUpdaters.Add(i, new MockUpdateEntity());

            // Running these tests you will notice that neither
            // type offers a faster approach, but the dictionary
            // is much larger as it is storing additional information.
            // We do not need this information because direct access
            // is rare in our scenario.
            stopwatch.Restart();
            foreach (KeyValuePair<int, MockUpdateEntity> kvp in dictionaryUpdaters)
                kvp.Value.Update();
            stopwatch.Stop();
            Console.WriteLine($"Dictionary enumeration millis: {stopwatch.ElapsedMilliseconds}");

            stopwatch.Restart();
            foreach (MockUpdateEntity entity in listUpdaters)
                entity.Update();
            stopwatch.Stop();
            Console.WriteLine($"List enumeration millis: {stopwatch.ElapsedMilliseconds}");
        }

        public static void DemoAccess()
        {
            Stopwatch stopwatch = new Stopwatch();

            List<string> allUsersList = new List<string>();

            Dictionary<int, string> allUsersDictionary = new Dictionary<int, string>();

            for (int i = 0; i < 1000000; i++)
            {
                allUsersList.Add($"user {i}");
                allUsersDictionary.Add(i, $"user {i}");
            }

            int userID = 925000;

            stopwatch.Restart();
            Console.WriteLine($"Username for {userID} is {allUsersList[userID]}");
            stopwatch.Stop();
            Console.WriteLine($"List access millis: {stopwatch.ElapsedMilliseconds}");

            stopwatch.Restart();
            Console.WriteLine($"Username for {userID} is {allUsersDictionary[userID]}");
            stopwatch.Stop();
            Console.WriteLine($"Dictionary access millis: {stopwatch.ElapsedMilliseconds}");

        }


        #region Mock Functions
        private class MockUpdateEntity
        {
            public void Update()
            {
                // Waste some time.
                double x = 4f;
                for (int i = 0; i < 1000; i++)
                    x = x * Math.Sin(x);
            }
        }
        private static void SendEmailMock(string recipient, string message)
        {
            Console.WriteLine($"Sent email to {recipient}: {message}");
        }
        private static int ReadIDCardMock()
        {
            return 1114432;
        }
        #endregion
    }
}
