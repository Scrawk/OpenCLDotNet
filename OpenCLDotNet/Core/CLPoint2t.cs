using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


using REAL = OpenCLDotNet.Core.size_t;

namespace OpenCLDotNet.Core
{

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public record struct CLPoint2t
    {
        public REAL x, y;

        /// <summary>
        /// The dimension is the number components in the point.
        /// </summary>
        public const int Dimension = 2;

        /// <summary>
        /// The unit x point.
        /// </summary>
	    public readonly static CLPoint2t UnitX = new CLPoint2t(1, 0);

        /// <summary>
        /// The unit y point.
        /// </summary>
	    public readonly static CLPoint2t UnitY = new CLPoint2t(0, 1);

        /// <summary>
        /// A point of zeros.
        /// </summary>
	    public readonly static CLPoint2t Zero = new CLPoint2t(0);

        /// <summary>
        /// A point of ones.
        /// </summary>
	    public readonly static CLPoint2t One = new CLPoint2t(1);

        /// <summary>
        /// A point of max value.
        /// </summary>
        public readonly static CLPoint2t MaxValue = new CLPoint2t(ulong.MaxValue);

        /// <summary>
        /// A point of min value.
        /// </summary>
        public readonly static CLPoint2t MinValue = new CLPoint2t(ulong.MinValue);

        /// <summary>
        /// A point all with the value v.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CLPoint2t(REAL v)
        {
            this.x = v;
            this.y = v;
        }

        /// <summary>
        /// A point from the varibles.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CLPoint2t(REAL x, REAL y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// A point from the varibles.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CLPoint2t(double x, double y)
        {
            this.x = (REAL)x;
            this.y = (REAL)y;
        }

        /// <summary>
        /// Array accessor for variables. 
        /// </summary>
        /// <param name="i">The variables index.</param>
        /// <returns>The variable value</returns>
        unsafe public REAL this[int i]
        {
            get
            {
                if ((uint)i >= Dimension)
                    throw new IndexOutOfRangeException("CLPoint2t index out of range.");

                fixed (CLPoint2t* array = &this) { return ((REAL*)array)[i]; }
            }
            set
            {
                if ((uint)i >= Dimension)
                    throw new IndexOutOfRangeException("CLPoint2t index out of range.");

                fixed (REAL* array = &x) { array[i] = value; }
            }
        }

        /// <summary>
        /// The sum of the points components.
        /// </summary>
        public REAL Sum
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return x + y;
            }
        }

        /// <summary>
        /// The product of the points components.
        /// </summary>
        public REAL Product
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return x * y;
            }
        }

        /// <summary>
        /// The length of the point.
        /// </summary>
        public double Magnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Math.Sqrt(SqrMagnitude);
            }
        }

        /// <summary>
        /// The length of the point squared.
        /// </summary>
		public REAL SqrMagnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (x * x + y * y);
            }
        }

        /// <summary>
        /// Add two points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint2t operator +(CLPoint2t v1, CLPoint2t v2)
        {
            return new CLPoint2t(v1.x + v2.x, v1.y + v2.y);
        }

        /// <summary>
        /// Add point and scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint2t operator +(CLPoint2t v1, REAL s)
        {
            return new CLPoint2t(v1.x + s, v1.y + s);
        }

        /// <summary>
        /// Add point and scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint2t operator +(REAL s, CLPoint2t v1)
        {
            return new CLPoint2t(s + v1.x, s + v1.y);
        }

        /// <summary>
        /// Subtract two points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint2t operator -(CLPoint2t v1, CLPoint2t v2)
        {
            return new CLPoint2t(v1.x - v2.x, v1.y - v2.y);
        }

        /// <summary>
        /// Subtract point and scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint2t operator -(CLPoint2t v1, REAL s)
        {
            return new CLPoint2t(v1.x - s, v1.y - s);
        }

        /// <summary>
        /// Subtract point and scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint2t operator -(REAL s, CLPoint2t v1)
        {
            return new CLPoint2t(s - v1.x, s - v1.y);
        }

        /// <summary>
        /// Multiply two points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint2t operator *(CLPoint2t v1, CLPoint2t v2)
        {
            return new CLPoint2t(v1.x * v2.x, v1.y * v2.y);
        }

        /// <summary>
        /// Multiply a point and a scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint2t operator *(CLPoint2t v, REAL s)
        {
            return new CLPoint2t(v.x * s, v.y * s);
        }

        /// <summary>
        /// Multiply a point and a scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint2t operator *(REAL s, CLPoint2t v)
        {
            return new CLPoint2t(v.x * s, v.y * s);
        }

        /// <summary>
        /// Divide two points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint2t operator /(CLPoint2t v1, CLPoint2t v2)
        {
            return new CLPoint2t(v1.x / v2.x, v1.y / v2.y);
        }

        /// <summary>
        /// Divide a point and a scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint2t operator /(CLPoint2t v, REAL s)
        {
            return new CLPoint2t(v.x / s, v.y / s);
        }

        /// <summary>
        /// Divide a scalar and a point.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint2t operator /(REAL s, CLPoint2t v)
        {
            return new CLPoint2t(s / v.x, s / v.y);
        }

        /// <summary>
        /// Vector as a string.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0},{1}", x, y);
        }

        /// <summary>
        /// Distance between two points.
        /// </summary>
        public static double Distance(CLPoint2t v0, CLPoint2t v1)
        {
            return Math.Sqrt(SqrDistance(v0, v1));
        }

        /// <summary>
        /// Square distance between two points.
        /// </summary>
        public static REAL SqrDistance(CLPoint2t v0, CLPoint2t v1)
        {
            REAL x = v0.x - v1.x;
            REAL y = v0.y - v1.y;
            return x * x + y * y;
        }

        /// <summary>
        /// The minimum value between s and each component in point.
        /// </summary>
        public static CLPoint2t Min(CLPoint2t v, REAL s)
        {
            v.x = Math.Min(v.x, s);
            v.y = Math.Min(v.y, s);
            return v;
        }

        /// <summary>
        /// The minimum value between each component in points.
        /// </summary>
        public static CLPoint2t Min(CLPoint2t v0, CLPoint2t v1)
        {
            v0.x = Math.Min(v0.x, v1.x);
            v0.y = Math.Min(v0.y, v1.y);
            return v0;
        }

        /// <summary>
        /// The maximum value between s and each component in point.
        /// </summary>
        public static CLPoint2t Max(CLPoint2t v, REAL s)
        {
            v.x = Math.Max(v.x, s);
            v.y = Math.Max(v.y, s);
            return v;
        }

        /// <summary>
        /// The maximum value between each component in points.
        /// </summary>
        public static CLPoint2t Max(CLPoint2t v0, CLPoint2t v1)
        {
            v0.x = Math.Max(v0.x, v1.x);
            v0.y = Math.Max(v0.y, v1.y);
            return v0;
        }

        /// <summary>
        /// Clamp each component to specified min and max.
        /// </summary>
        public static CLPoint2t Clamp(CLPoint2t v, REAL min, REAL max)
        {
            v.x = Math.Max(Math.Min(v.x, max), min);
            v.y = Math.Max(Math.Min(v.y, max), min);
            return v;
        }

        /// <summary>
        /// Clamp each component to specified min and max.
        /// </summary>
        public static CLPoint2t Clamp(CLPoint2t v, CLPoint2t min, CLPoint2t max)
        {
            v.x = Math.Max(Math.Min(v.x, max.x), min.x);
            v.y = Math.Max(Math.Min(v.y, max.y), min.y);
            return v;
        }

        /// <summary>
        /// Create a new point by reordering the componets.
        /// </summary>
        /// <param name="i">The index to take x value from.></param>
        /// <param name="j">The index to take y value from.</param>
        /// <returns>The new vector</returns>
        public CLPoint2t Permutate(int i, int j)
        {
            return new CLPoint2t(this[i], this[j]);
        }

    }
}
