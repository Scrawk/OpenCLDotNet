using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core

namespace OpenCLDotNet.Utility
{
    /// <summary>
    /// Cache to reuse arrays.
    /// Thread safe?
    /// </summary>
    public class ArrayCache
    {
        [ThreadStatic]
        public static bool Disable = false;

        [ThreadStatic]
        private static CLPoint2t[] m_points2t;

        [ThreadStatic]
        private static CLPoint3t[] m_points3t

        [ThreadStatic]
        private static int[] m_int;
        
        [ThreadStatic]
        private static uint[] m_uint;
        
        [ThreadStatic]
        private static float[] m_float;
        
        [ThreadStatic]
        private static byte[] m_byte;

        public void Clear()
        {
            m_points2t = null;
            m_points3t = null;
            m_int = null;
            m_uint = null;
        }
        
        /// <returns>Returns a array of Segment2d objects that is at least the size of count.</returns>
        public static Segment2d[] Segment2dArray(int count, bool clear = false)
        {
            if (MakeNewArray(m_segments2d, count))
                m_segments2d = new Segment2d[count];

            if (clear)
                Array.Clear(m_segments2d, 0, m_segments2d.Length);

            return m_segments2d;
        }
        
        /// Returns a array of ints that is at least the size of count.
        /// </summary>
        /// <param name="count">The minimum size of the array.</param>
        /// <param name="clear">Should the array be cleared first.</param>
        /// <returns>Returns a array of ints that is at least the size of count.</returns>
        public static int[] IntArray1(int count, bool clear = false)
        {
            if (MakeNewArray(m_int1, count))
                m_int1 = new int[count];

            if (clear)
                Array.Clear(m_int1, 0, m_int1.Length);

            return m_int1;
        }

        /// <summary>
        /// Returns a array of ints that is at least the size of count.
        /// </summary>
        /// <param name="count">The minimum size of the array.</param>
        /// <param name="clear">Should the array be cleared first.</param>
        /// <returns>Returns a array of ints that is at least the size of count.</returns>
        public static int[] IntArray2(int count, bool clear = false)
        {
            if (MakeNewArray(m_int2, count))
                m_int2 = new int[count];

            if (clear)
                Array.Clear(m_int2, 0, m_int2.Length);

            return m_int2;
        }

        /// <summary>
        /// Should a new array be created.
        /// </summary>
        /// <param name="arr">The current array.</param>
        /// <param name="count">The required new array size.</param>
        /// <returns>Creates a new array if disabled, the current one is null or to small.</returns>
        private static bool MakeNewArray(Array arr, int count)
        {
            return (Disable || arr == null || arr.Length < count);
        }

    }
}
