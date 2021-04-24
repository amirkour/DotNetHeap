using System;
using Xunit;
using DotNetHeap;

namespace DotNetHeap.Test
{
    public class MinHeapTests
    {
        protected DotNetHeap<int> _minHeap;
        public MinHeapTests()
        {
            _minHeap = new DotNetHeap<int>(DotNetHeap<int>.HEAP_TYPE.MIN);
        }

        [Fact]
        public void Enqueue_SmokeTest()
        {
            Assert.Equal(0, _minHeap.NumElements);

            _minHeap.Enqueue(5);
            Assert.Equal(5, _minHeap.Peek());
            Assert.Equal(1, _minHeap.NumElements);

            _minHeap.Enqueue(6);
            Assert.Equal(5, _minHeap.Peek());
            Assert.Equal(2, _minHeap.NumElements);

            _minHeap.Enqueue(1);
            Assert.Equal(1, _minHeap.Peek());
            Assert.Equal(3, _minHeap.NumElements);
        }

        [Fact]
        public void Dequeue_SmokeTest()
        {
            _minHeap.Enqueue(3);
            _minHeap.Enqueue(4);
            _minHeap.Enqueue(5);
            _minHeap.Enqueue(1);
            _minHeap.Enqueue(2);
            Assert.Equal(5, _minHeap.NumElements);

            Assert.Equal(1, _minHeap.Dequeue());
            Assert.Equal(4, _minHeap.NumElements);

            Assert.Equal(2, _minHeap.Dequeue());
            Assert.Equal(3, _minHeap.NumElements);

            Assert.Equal(3, _minHeap.Dequeue());
            Assert.Equal(2, _minHeap.NumElements);

            Assert.Equal(4, _minHeap.Dequeue());
            Assert.Equal(1, _minHeap.NumElements);

            Assert.Equal(5, _minHeap.Dequeue());
            Assert.Equal(0, _minHeap.NumElements);

            Assert.Equal(default(int), _minHeap.Dequeue());
            Assert.Equal(0, _minHeap.NumElements);
        }

        [Theory]
        [InlineData(new int[] { 4, 1, 6, 9, -2 }, -2, 5, new int[] { 10, 20, 30 }, 10, 3)]
        public void MinHeap_ClearSmokeTest(int[] firstList, int expectedFirstMin, int expectedFirstSize,
                                   int[] secondList, int expectedSecondMin, int expectedSecondSize)
        {
            Array.ForEach<int>(firstList, x => _minHeap.Enqueue(x));
            Assert.Equal(expectedFirstMin, _minHeap.Peek());
            Assert.Equal(expectedFirstSize, _minHeap.NumElements);

            _minHeap.Clear();
            Assert.Equal(0, _minHeap.NumElements);
            Assert.Equal(default(int), _minHeap.Peek());
            Assert.Equal(default(int), _minHeap.Dequeue());

            Array.ForEach<int>(secondList, x => _minHeap.Enqueue(x));
            Assert.Equal(expectedSecondMin, _minHeap.Peek());
            Assert.Equal(expectedSecondSize, _minHeap.NumElements);
        }

        [Fact]
        public void Remove_Less_Than_Root_Does_Nothing()
        {
            _minHeap.Enqueue(10).Enqueue(20).Enqueue(30).Remove(5);
            Assert.Equal(3, _minHeap.NumElements);
            Assert.Equal(10, _minHeap.Peek());
        }

        [Fact]
        public void Remove_Equal_To_Root_Removes_It()
        {
            _minHeap.Enqueue(10).Enqueue(20).Enqueue(30).Remove(10);
            Assert.Equal(2, _minHeap.NumElements);
            Assert.NotEqual(10, _minHeap.Peek());
        }

        [Theory]
        [InlineData(new int[] { 10, 20, 30, 29 }, 29)]
        [InlineData(new int[] { 10, 30, 20, 29 }, 29)]
        public void Remove_Less_Than_One_Child_Removes_It(int[] elements, int toRemove)
        {
            _minHeap.Enqueue(elements).Remove(toRemove);
            Assert.Equal(3, _minHeap.NumElements);
            for (int i = 0; i < _minHeap.NumElements; i++)
            {
                if (_minHeap[i] == toRemove)
                    throw new Exception("Should not have been able to find " + toRemove + " after removal");
            }
        }

        [Theory]
        [InlineData(new int[] { 10, 20, 30, 19 }, 19)]
        [InlineData(new int[] { 10, 30, 20, 19 }, 19)]
        public void Remove_Less_Than_Both_Children_Removes_It(int[] elements, int toRemove)
        {
            _minHeap.Enqueue(elements).Remove(toRemove);
            Assert.Equal(3, _minHeap.NumElements);
            for (int i = 0; i < _minHeap.NumElements; i++)
            {
                if (_minHeap[i] == toRemove)
                    throw new Exception("Should not have been able to find " + toRemove + " after removal");
            }
        }

        [Fact]
        public void Remove_Non_Existing_Item_Does_Nothing()
        {
            _minHeap.Enqueue(new int[] { 10, 20, 30 }).Remove(45);
            Assert.Equal(3, _minHeap.NumElements);
        }
    }
}
