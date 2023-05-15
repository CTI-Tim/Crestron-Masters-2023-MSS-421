using System;
using System.Collections.Concurrent;


/// A quick and rough fixed sized queue to simplify logging data.
/// Note: As the ConcorrentQueue.Enqueue method is not virtual, the programmer 'could' access the base method directly.

namespace SimplWindowsCWSIntegration
{
    /// <summary>
    /// Represents a fixed size first in - first out (FIFO) collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class FixedSizedQueue<T> : ConcurrentQueue<T>
    {
        /// <summary>
        /// Lock object to maintain some level of thread safety with Enqueue method.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// The maximum size of the collection.
        /// </summary>
        public readonly int Size;

        /// <summary>
        /// Initializes a new instance of the FixedSizeQueue class.
        /// </summary>
        /// <param name="size">The maximum size of the queue.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public FixedSizedQueue(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException("Queue size must be greater than zero (non-negative)");
            }

            this.Size = size;
        }

        /// <summary>
        /// Adds an object to the end of the queue.
        /// </summary>
        public new void Enqueue(T item)
        {
            lock (_lock)
            {
                base.Enqueue(item);
                while (base.Count > Size)
                {
                    base.TryDequeue(out _);
                }
            }
        }

        /// <summary>
        /// Clears all items from the queue.
        /// </summary>
        public void Clear()
        {
            lock (_lock)
            {
                while (base.TryDequeue(out _))
                {
                }
            }
        }
    }
}