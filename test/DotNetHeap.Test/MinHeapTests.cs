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
    }
}
