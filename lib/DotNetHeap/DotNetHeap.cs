using System;
using System.Text;

namespace DotNetHeap
{
    public class DotNetHeap<T> where T : IComparable<T>
    {
        protected T[] _queue;
        public T this[int i]
        {
            get { return _queue[i+1]; }
        }

        protected int _numElements;
        public int NumElements { get { return _numElements; } }
        protected Func<T, T, int> _magnitudeCompareTo;

        public DotNetHeap()
        {
            _queue = new T[10]; // TODO
            _numElements = 0;
        }

        public DotNetHeap(Func<T, T, int> mct) : this()
        {
            _magnitudeCompareTo = mct;
        }

        public DotNetHeap<T> Enqueue(T thing)
        {
            if (_numElements + 1 == _queue.Length)
                _queue = this.GetABiggerQueue();

            _queue[++_numElements] = thing;
            int i = _numElements;
            while (i > 1 && i / 2 >= 1 && _queue[i].CompareTo(_queue[i / 2]) > 0)
            {
                this.Swap(i, i / 2);
                i /= 2;
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

        public T Dequeue()
        {
            if (_numElements <= 0)
                return default(T);

            T toRet = _queue[1];
            _queue[1] = _queue[_numElements];
            _queue[_numElements--] = default(T);
            int i = 1;
            T left;
            T right;
            int leftComparisonValue = 0;
            int rightComparisonValue = 0;
            int leftIndex = 0;
            int rightIndex = 0;
            int iSwap = 0;
            while (this.HasAtLeastOneChild(i, out left, out right, out leftIndex, out rightIndex))
            {
                if (!Object.Equals(left, default(T)))
                    leftComparisonValue = _magnitudeCompareTo(_queue[i], left);
                if (!Object.Equals(right, default(T)))
                    rightComparisonValue = _magnitudeCompareTo(_queue[i], right);

                // if both children are smaller, swap w/ the one
                // that's smallest by comparison
                if (leftComparisonValue < 0 && rightComparisonValue < 0)
                {
                    iSwap = (Math.Abs(leftComparisonValue) > Math.Abs(rightComparisonValue)) ? leftIndex : rightIndex;
                }
                else if (leftComparisonValue < 0)
                {
                    iSwap = leftIndex;
                }
                else if (rightComparisonValue < 0)
                {
                    iSwap = rightIndex;
                }
                else
                {
                    // neither child is smaller - we're done
                    break;
                }

                this.Swap(i, iSwap);
                i = iSwap;
                leftComparisonValue = 0;
                rightComparisonValue = 0;
                iSwap = 0;
                leftIndex = 0;
                rightIndex = 0;
            }

            return toRet;
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

        protected void Swap(int i, int j)
        {
            T temp = _queue[i];
            _queue[i] = _queue[j];
            _queue[j] = temp;
        }

        protected T[] GetABiggerQueue()
        {
            T[] newQ = new T[_queue.Length + 20];
            for (int i = 0; i < _queue.Length; i++)
                newQ[i] = _queue[i];

            return newQ;
        }
    }
}
