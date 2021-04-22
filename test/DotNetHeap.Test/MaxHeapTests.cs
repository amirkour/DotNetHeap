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
    }
}
