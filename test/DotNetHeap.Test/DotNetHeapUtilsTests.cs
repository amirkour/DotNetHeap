using System;
using Xunit;
using DotNetHeap;
using DotNetHeap.Utils;

namespace DotNetHeap.Test
{
    public class DotNetHeapUtilsTests
    {
        [Theory]
        [InlineData(new int[]{1,2,3},0,1, new int[]{2,1,3})]
        public void Swap_SmokeTest(int[] original, int i, int j, int[] afterSwap)
        {
            Assert.NotEqual(original, afterSwap);
            original.Swap(i,j);
            Assert.Equal(original,afterSwap);
            original.Swap(i, j);
            Assert.NotEqual(original, afterSwap);
        }

        [Fact]
        public void TransferToBiggerArray_null_or_empty_original_returns_default_size_array()
        {
            int[] original = null;
            int[] list = DotNetHeapUtils.TransferToBiggerArray(original);
            Assert.NotNull(list);
            Assert.Equal(DotNetHeapUtils.HEAP_DEFAULT_SIZE, list.Length);

            original = new int[0];
            list = DotNetHeapUtils.TransferToBiggerArray(original);
            Assert.NotNull(list);
            Assert.Equal(DotNetHeapUtils.HEAP_DEFAULT_SIZE, list.Length);
        }

        [Fact]
        public void TransferToBiggerArray_copies_contents_of_original_to_new_array()
        {
            int[] original = new int[]{1,2,3};
            int[] expanded = DotNetHeapUtils.TransferToBiggerArray(original);
            Assert.Equal(1, expanded[0]);
            Assert.Equal(2, expanded[1]);
            Assert.Equal(3, expanded[2]);
        }

        [Fact]
        public void TransferToBiggerArray_allows_custom_expansion_size()
        {
            int[] original = new int[]{1,2,3};
            int additionalSize = 1;
            int[] expanded = DotNetHeapUtils.TransferToBiggerArray(original, additionalSize);
            Assert.Equal(expanded.Length, original.Length + additionalSize);
        }
    }
}
