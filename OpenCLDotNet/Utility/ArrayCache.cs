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
            m_float = null;
            m_byte = null;
        }
        
        /// Returns a array of ints that is at least the size of count.
        /// </summary>
        /// <param name="count">The minimum size of the array.</param>
        /// <param name="clear">Should the array be cleared first.</param>
        /// <returns>Returns a array of ints that is at least the size of count.</returns>
        public static int[] IntArray(int count, bool clear = false)
        {
            if (MakeNewArray(m_int, count))
                m_int = new int[count];

            if (clear)
                Array.Clear(m_int, 0, m_int.Length);

            return m_int;
        }

        /// <summary>
        /// Returns a array of uints that is at least the size of count.
        /// </summary>
        /// <param name="count">The minimum size of the array.</param>
        /// <param name="clear">Should the array be cleared first.</param>
        /// <returns>Returns a array of ints that is at least the size of count.</returns>
        public static uint[] UIntArray(int count, bool clear = false)
        {
            if (MakeNewArray(m_uint, count))
                m_uint = new uint[count];

            if (clear)
                Array.Clear(m_uint, 0, m_uint.Length);

            return m_uint;
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
