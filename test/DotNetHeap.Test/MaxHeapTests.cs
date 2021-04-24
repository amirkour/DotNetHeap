using System;
using Xunit;
using DotNetHeap;

namespace DotNetHeap.Test
{
    public class MaxHeapTests
    {
        protected DotNetHeap<int> _heap;

        public MaxHeapTests()
        {
            _heap = new DotNetHeap<int>();
        }

        [Fact]
        public void Enqueue_SmokeTest()
        {
            _heap.Enqueue(5);
            Assert.Equal(5, _heap.Peek());

            _heap.Enqueue(10);
            Assert.Equal(10, _heap.Peek());
            Assert.Equal(2, _heap.NumElements);

            _heap.Enqueue(7);
            Assert.Equal(10, _heap.Peek());
            Assert.Equal(3, _heap.NumElements);

            _heap.Enqueue(20);
            Assert.Equal(20, _heap.Peek());
            Assert.Equal(4, _heap.NumElements);
        }

        [Fact]
        public void Dequeue_SmokeTest()
        {
            _heap.Enqueue(1);
            _heap.Enqueue(2);
            _heap.Enqueue(3);
            _heap.Enqueue(4);
            _heap.Enqueue(5);
            Assert.Equal(5, _heap.NumElements);

            Assert.Equal(5, _heap.Dequeue());
            Assert.Equal(4, _heap.Peek());
            Assert.Equal(4, _heap.NumElements);

            Assert.Equal(4, _heap.Dequeue());
            Assert.Equal(3, _heap.Peek());
            Assert.Equal(3, _heap.NumElements);

            Assert.Equal(3, _heap.Dequeue());
            Assert.Equal(2, _heap.Peek());
            Assert.Equal(2, _heap.NumElements);

            Assert.Equal(2, _heap.Dequeue());
            Assert.Equal(1, _heap.Peek());
            Assert.Equal(1, _heap.NumElements);

            Assert.Equal(1, _heap.Dequeue());
            Assert.Equal(0, _heap.Peek());
            Assert.Equal(0, _heap.NumElements);
        }

        [Theory]
        [InlineData(new int[] { 4, 1, 6, 9, -2 }, 9, 5, new int[] { 10, 20, 30 }, 30, 3)]
        public void MaxHeap_ClearSmokeTest(int[] firstList, int expectedFirstMax, int expectedFirstSize,
                                           int[] secondList, int expectedSecondMax, int expectedSecondSize)
        {
            Array.ForEach<int>(firstList, x => _heap.Enqueue(x));
            Assert.Equal(expectedFirstMax, _heap.Peek());
            Assert.Equal(expectedFirstSize, _heap.NumElements);

            _heap.Clear();
            Assert.Equal(0, _heap.NumElements);
            Assert.Equal(default(int), _heap.Peek());
            Assert.Equal(default(int), _heap.Dequeue());

            Array.ForEach<int>(secondList, x => _heap.Enqueue(x));
            Assert.Equal(expectedSecondMax, _heap.Peek());
            Assert.Equal(expectedSecondSize, _heap.NumElements);
        }

        [Fact]
        public void Remove_Greater_Than_Root_Does_Nothing()
        {
            _heap.Enqueue(10).Enqueue(20).Enqueue(30).Remove(40);
            Assert.Equal(3, _heap.NumElements);
            Assert.Equal(30, _heap[0]);
        }

        [Fact]
        public void Remove_Equal_To_Root_Removes_It()
        {
            _heap.Enqueue(10).Enqueue(20).Enqueue(30).Remove(30);
            Assert.Equal(2, _heap.NumElements);
            Assert.NotEqual(30, _heap[0]);
        }

        [Theory]
        [InlineData(new int[] { 10, 20, 30, 19 }, 19)]
        [InlineData(new int[] { 20, 10, 30, 19 }, 19)]
        public void Remove_Greater_Than_One_Child_Removes_It(int[] elements, int toRemove)
        {
            _heap.Enqueue(elements).Remove(toRemove);
            Assert.Equal(3, _heap.NumElements);
            for (int i = 0; i < _heap.NumElements; i++)
            {
                if (_heap[i] == toRemove)
                    throw new Exception("Should not have been able to find " + toRemove + " after removal");
            }
        }
    }
}
