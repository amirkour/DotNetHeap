using System;
using Xunit;
using DotNetHeap;

namespace DotNetHeap.Test
{
    public class EnqueueTests
    {
        [Fact]
        public void Enqueue_SingleItem_Works()
        {
            DotNetHeap<int> heap = new DotNetHeap<int>();
            heap.Enqueue(12);
            Assert.Equal(12, heap[0]);
        }
    }
}
