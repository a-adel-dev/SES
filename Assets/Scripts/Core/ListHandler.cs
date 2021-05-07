using System.Collections.Generic;

namespace SES.Core
{
    public static class ListHandler
    {
        private static System.Random rng = new System.Random();
        public static List<T> Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public static List<T> GetRandomItemsFromList<T>(List<T> list, int number)
        {
            // this is the list we're going to remove picked items from
            List<T> tmpList = new List<T>(list);
            // this is the list we're going to move items to
            List<T> newList = new List<T>();

            // make sure tmpList isn't already empty
            while (newList.Count < number && tmpList.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, tmpList.Count);
                newList.Add(tmpList[index]);
                tmpList.RemoveAt(index);
            }

            return newList;
        }
    }
}