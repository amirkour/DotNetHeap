using System;
using System.Text;
using System.Collections.Generic;
using DotNetHeap;

namespace DotNetHeap.Utils
{
    public static class DotNetHeapUtils
    {
        public const int HEAP_DEFAULT_SIZE = 20;

        /// <summary>
        /// Array extension that swaps the items at the given indices i and j
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public static void Swap<T>(this T[] arr, int i, int j)
        {
            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        /// <summary>
        /// Helper that expands the given original array by the given
        /// <paramref name="additionalSpace"/> amount, and copies the
        /// contents of <paramref name="original"/> into the returned
        /// array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="additionalSpace"></param>
        /// <returns></returns>
        public static T[] TransferToBiggerArray<T>(T[] original, int additionalSpace = HEAP_DEFAULT_SIZE)
        {
            additionalSpace = additionalSpace > 0 ? additionalSpace : HEAP_DEFAULT_SIZE;
            if (original == null || original.Length <= 0)
                return new T[additionalSpace];

            T[] newQ = new T[original.Length + additionalSpace];
            for (int i = 0; i < original.Length; i++)
                newQ[i] = original[i];

            return newQ;
        }

        public static List<T> Push<T>(this List<T> stack, T thing)
        {
            if (stack == null)
                return null;

            stack.Add(thing);
            return stack;
        }
        public static T Pop<T>(this List<T> stack)
        {
            if (stack == null || stack.Count <= 0)
                return default(T);

            T thing = stack[stack.Count - 1];
            stack.RemoveAt(stack.Count - 1);
            return thing;
        }
    }
}
