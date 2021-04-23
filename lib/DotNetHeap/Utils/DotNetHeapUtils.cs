using System;
using System.Text;
using DotNetHeap;

namespace DotNetHeap.Utils
{
    public static class DotNetHeapUtils
    {
        public const int HEAP_DEFAULT_SIZE = 20;
        public static void Swap<T>(this T[] heap, int i, int j)
        {
            T temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }

        public static T[] TransferToBiggerArray<T>(T[] original, int additionalSpace = HEAP_DEFAULT_SIZE)
        {
            additionalSpace = additionalSpace > 0 ? additionalSpace : HEAP_DEFAULT_SIZE;
            if(original == null || original.Length <= 0)
                return new T[additionalSpace];

            T[] newQ = new T[original.Length + additionalSpace];
            for (int i = 0; i < original.Length; i++)
                newQ[i] = original[i];

            return newQ;
        }
    }
}
