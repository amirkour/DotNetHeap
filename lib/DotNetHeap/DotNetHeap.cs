using System;
using System.Text;
using System.Collections.Generic;
using DotNetHeap.Utils;

namespace DotNetHeap
{
    public class DotNetHeap<T> where T : IComparable<T>
    {
        public enum HEAP_TYPE
        {
            MAX, // default
            MIN
        };

        protected T[] _queue;
        public T this[int i]
        {
            get { return _queue[i + 1]; }   // users can reference 0-based, but this implementation indexes starting at 1
        }

        protected int _numElements;
        public int NumElements { get { return _numElements; } }

        protected HEAP_TYPE _heapType;
        public HEAP_TYPE HeapType { get { return _heapType; } }

        public DotNetHeap()
        {
            _queue = new T[10]; // TODO
            _numElements = 0;
            _heapType = HEAP_TYPE.MAX;
        }

        public DotNetHeap(HEAP_TYPE ht) : this()
        {
            _heapType = ht;
        }

        public DotNetHeap<T> Enqueue(T thing)
        {
            if (_numElements + 1 == _queue.Length)
                _queue = DotNetHeapUtils.TransferToBiggerArray(_queue);

            _queue[++_numElements] = thing;
            int i = _numElements;
            while (i > 1 && i / 2 >= 1) // && )
            {
                if ((_heapType == HEAP_TYPE.MAX && _queue[i].CompareTo(_queue[i / 2]) > 0) ||
                    (_heapType == HEAP_TYPE.MIN && _queue[i].CompareTo(_queue[i / 2]) < 0))
                {
                    _queue.Swap(i, i / 2);
                }

                i /= 2;
            }

            return this;
        }

        public DotNetHeap<T> Enqueue(IEnumerable<T> things)
        {
            if(things != null)
            {
                foreach(T next in things)
                    this.Enqueue(next);
            }

            return this;
        }

        public T Peek()
        {
            return _numElements <= 0 ? default(T) : _queue[1];
        }

        protected bool HasAtLeastOneChild(int i, out T left, out T right, out int leftIndex, out int rightIndex)
        {
            left = default(T);
            right = default(T);
            leftIndex = 0;
            rightIndex = 0;
            if (_numElements <= 1)
                return false;

            if (i > _numElements)
                return false;

            bool found = false;
            if (i * 2 <= _numElements)
            {
                left = _queue[i * 2];
                leftIndex = i * 2;
                found = true;
            }
            if ((i * 2) + 1 <= _numElements)
            {
                right = _queue[(i * 2) + 1];
                rightIndex = (i * 2) + 1;
                found = true;
            }

            return found;
        }

        protected void HeapifyDownStartingAt(int i)
        {
            if (i < 1 || i >= _numElements)
                return;

            T left;
            T right;
            int leftIndex = 0;
            int rightIndex = 0;
            int iSwap = 0;
            while (this.HasAtLeastOneChild(i, out left, out right, out leftIndex, out rightIndex))
            {
                if ((_heapType == HEAP_TYPE.MAX && this.TryGetMaxHeapSwapIndex(i, left, right, leftIndex, rightIndex, out iSwap)) ||
                     (_heapType == HEAP_TYPE.MIN && this.TryGetMinHeapSwapIndex(i, left, right, leftIndex, rightIndex, out iSwap)))
                    _queue.Swap(i, iSwap);
                else
                    break;

                i = iSwap;
                iSwap = 0;
                leftIndex = 0;
                rightIndex = 0;
            }
        }

        public void Remove(T thingToRemove)
        {
            if (_numElements <= 0)
                return;

            if (_heapType == HEAP_TYPE.MAX && _queue[1].CompareTo(thingToRemove) < 0)
                return;
            if (_heapType == HEAP_TYPE.MIN && _queue[1].CompareTo(thingToRemove) > 0)
                return;

            List<Tuple<int, T>> stack = new List<Tuple<int, T>>();
            int iSwap = -1;
            stack.Push(new Tuple<int, T>(1, _queue[1]));
            while (stack.Count > 0)
            {
                Tuple<int, T> next = stack.Pop();
                if (next.Item2.Equals(thingToRemove))
                {
                    iSwap = next.Item1;
                    break;
                }

                T left;
                T right;
                int leftIndex;
                int rightIndex;
                if (!this.HasAtLeastOneChild(next.Item1, out left, out right, out leftIndex, out rightIndex))
                    break;

                if (_heapType == HEAP_TYPE.MAX)
                {
                    if (!Object.Equals(left, default(T)))
                    {
                        int comparison = thingToRemove.CompareTo(left);
                        if (comparison == 0)
                        {
                            iSwap = leftIndex;
                            break;
                        }
                        else if (comparison < 0)
                        {
                            stack.Push(new Tuple<int, T>(leftIndex, left));
                        }
                    }
                    if (!Object.Equals(right, default(T)))
                    {
                        int comparison = thingToRemove.CompareTo(right);
                        if (comparison == 0)
                        {
                            iSwap = rightIndex;
                            break;
                        }
                        else if (comparison < 0)
                        {
                            stack.Push(new Tuple<int, T>(rightIndex, right));
                        }
                    }
                }
                else
                {
                    if (!Object.Equals(left, default(T)))
                    {
                        int comparison = thingToRemove.CompareTo(left);
                        if (comparison == 0)
                        {
                            iSwap = leftIndex;
                            break;
                        }
                        else if (comparison > 0)
                        {
                            stack.Push(new Tuple<int, T>(leftIndex, left));
                        }
                    }
                    if (!Object.Equals(right, default(T)))
                    {
                        int comparison = thingToRemove.CompareTo(right);
                        if (comparison == 0)
                        {
                            iSwap = rightIndex;
                            break;
                        }
                        else if (comparison > 0)
                        {
                            stack.Push(new Tuple<int, T>(rightIndex, right));
                        }
                    }
                }
            }

            // didn't find the thing that the caller wanted to remove.
            // so ... nothin to do but bail.
            if (iSwap == -1)
                return;

            // otherwise, move the last item up to this index
            // and heapify to preserve the integrity of the heap
            _queue[iSwap] = _queue[_numElements];
            _queue[_numElements--] = default(T);
            this.HeapifyDownStartingAt(iSwap);
        }

        public T Dequeue()
        {
            if (_numElements <= 0)
                return default(T);

            T toRet = _queue[1];
            _queue[1] = _queue[_numElements];
            _queue[_numElements--] = default(T);
            this.HeapifyDownStartingAt(1);

            return toRet;
        }

        protected bool TryGetMaxHeapSwapIndex(int parentIndex, T leftChild, T rightChild, int leftIndex, int rightIndex, out int iSwap)
        {
            iSwap = 0;
            T parent = _queue[parentIndex];

            if (!Object.Equals(leftChild, default(T)) && !Object.Equals(rightChild, default(T)))
            {
                if (parent.CompareTo(leftChild) > 0 && parent.CompareTo(rightChild) > 0)
                    return false;

                iSwap = leftChild.CompareTo(rightChild) < 0 ? rightIndex : leftIndex;
            }
            else if (!Object.Equals(leftChild, default(T)))
            {
                if (parent.CompareTo(leftChild) > 0)
                    return false;

                iSwap = leftIndex;
            }
            else if (!Object.Equals(rightChild, default(T)))
            {
                if (parent.CompareTo(rightChild) > 0)
                    return false;

                iSwap = rightIndex;
            }
            else
            {
                return false;
            }

            return true;
        }

        protected bool TryGetMinHeapSwapIndex(int parentIndex, T leftChild, T rightChild, int leftIndex, int rightIndex, out int iSwap)
        {
            iSwap = 0;
            T parent = _queue[parentIndex];

            if (!Object.Equals(leftChild, default(T)) && !Object.Equals(rightChild, default(T)))
            {
                if (parent.CompareTo(leftChild) < 0 && parent.CompareTo(rightChild) < 0)
                    return false;

                iSwap = leftChild.CompareTo(rightChild) < 0 ? leftIndex : rightIndex;
            }
            else if (!Object.Equals(leftChild, default(T)))
            {
                if (parent.CompareTo(leftChild) < 0)
                    return false;

                iSwap = leftIndex;
            }
            else if (!Object.Equals(rightChild, default(T)))
            {
                if (parent.CompareTo(rightChild) < 0)
                    return false;

                iSwap = rightIndex;
            }
            else
            {
                return false;
            }

            return true;
        }

        public DotNetHeap<T> Clear()
        {
            _numElements = 0;
            return this;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[ ");
            for (int i = 1; i <= _numElements; i++)
            {
                sb.AppendFormat("{0} ", _queue[i].ToString());
            }
            sb.Append("]");

            return sb.ToString();
        }


    }
}
