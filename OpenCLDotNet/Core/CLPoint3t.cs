using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


using REAL = OpenCLDotNet.Core.size_t;

namespace OpenCLDotNet.Core
{

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public record struct CLPoint3t
    {
        public REAL x, y, z;

        /// <summary>
        /// The dimension is the number components in the point.
        /// </summary>
        public const int Dimension = 3;

        /// <summary>
        /// The unit x point.
        /// </summary>
	    public readonly static CLPoint3t UnitX = new CLPoint3t(1, 0, 0);

        /// <summary>
        /// The unit y point.
        /// </summary>
	    public readonly static CLPoint3t UnitY = new CLPoint3t(0, 1, 0);

        /// <summary>
        /// The unit z point.
        /// </summary>
        public readonly static CLPoint3t UnitZ = new CLPoint3t(0, 0, 1);

        /// <summary>
        /// A point of zeros.
        /// </summary>
	    public readonly static CLPoint3t Zero = new CLPoint3t(0);

        /// <summary>
        /// A point of ones.
        /// </summary>
	    public readonly static CLPoint3t One = new CLPoint3t(1);

        /// <summary>
        /// A point of max value.
        /// </summary>
        public readonly static CLPoint3t MaxValue = new CLPoint3t(ulong.MaxValue);

        /// <summary>
        /// A point of min value.
        /// </summary>
        public readonly static CLPoint3t MinValue = new CLPoint3t(ulong.MinValue);

        /// <summary>
        /// 3D point to 3D swizzle point.
        /// </summary>
        public CLPoint3t xzy => new CLPoint3t(x, z, y);

        /// <summary>
        /// A point all with the value v.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CLPoint3t(REAL v)
        {
            this.x = v;
            this.y = v;
            this.z = v;
        }

        /// <summary>
        /// A point all with the value v.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CLPoint3t(CLPoint2t v, REAL z)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = z;
        }

        /// <summary>
        /// A point from the varibles.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CLPoint3t(REAL x, REAL y, REAL z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// A point from the varibles.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CLPoint3t(double x, double y, double z)
        {
            this.x = (REAL)x;
            this.y = (REAL)y;
            this.z = (REAL)z;
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
                    throw new IndexOutOfRangeException("CLPoint3t index out of range.");

                fixed (CLPoint3t* array = &this) { return ((REAL*)array)[i]; }
            }
            set
            {
                if ((uint)i >= Dimension)
                    throw new IndexOutOfRangeException("CLPoint3t index out of range.");

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
                return x + y + z;
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
                return x * y * z;
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
                return (x * x + y * y + z * z);
            }
        }

        /// <summary>
        /// Add two points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint3t operator +(CLPoint3t v1, CLPoint3t v2)
        {
            return new CLPoint3t(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        /// <summary>
        /// Add point and scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint3t operator +(CLPoint3t v1, REAL s)
        {
            return new CLPoint3t(v1.x + s, v1.y + s, v1.z + s);
        }

        /// <summary>
        /// Add point and scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint3t operator +(REAL s, CLPoint3t v1)
        {
            return new CLPoint3t(s + v1.x, s + v1.y, s + v1.z);
        }

        /// <summary>
        /// Subtract two points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint3t operator -(CLPoint3t v1, CLPoint3t v2)
        {
            return new CLPoint3t(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        /// <summary>
        /// Subtract point and scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint3t operator -(CLPoint3t v1, REAL s)
        {
            return new CLPoint3t(v1.x - s, v1.y - s, v1.z - s);
        }

        /// <summary>
        /// Subtract point and scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint3t operator -(REAL s, CLPoint3t v1)
        {
            return new CLPoint3t(s - v1.x, s - v1.y, s - v1.z);
        }

        /// <summary>
        /// Multiply two points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint3t operator *(CLPoint3t v1, CLPoint3t v2)
        {
            return new CLPoint3t(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        /// <summary>
        /// Multiply a point and a scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint3t operator *(CLPoint3t v, REAL s)
        {
            return new CLPoint3t(v.x * s, v.y * s, v.z * s);
        }

        /// <summary>
        /// Multiply a point and a scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint3t operator *(REAL s, CLPoint3t v)
        {
            return new CLPoint3t(v.x * s, v.y * s, v.z * s);
        }

        /// <summary>
        /// Divide two points.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint3t operator /(CLPoint3t v1, CLPoint3t v2)
        {
            return new CLPoint3t(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
        }

        /// <summary>
        /// Divide a point and a scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint3t operator /(CLPoint3t v, REAL s)
        {
            return new CLPoint3t(v.x / s, v.y / s, v.z / s);
        }

        /// <summary>
        /// Divide a scalar and a point.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLPoint3t operator /(REAL s, CLPoint3t v)
        {
            return new CLPoint3t(s / v.x, s / v.y, s / v.z);
        }

        /// <summary>
        /// Vector as a string.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0},{1},{2}", x, y, z);
        }

        /// <summary>
        /// Distance between two points.
        /// </summary>
        public static double Distance(CLPoint3t v0, CLPoint3t v1)
        {
            return Math.Sqrt(SqrDistance(v0, v1));
        }

        /// <summary>
        /// Square distance between two points.
        /// </summary>
        public static REAL SqrDistance(CLPoint3t v0, CLPoint3t v1)
        {
            REAL x = v0.x - v1.x;
            REAL y = v0.y - v1.y;
            REAL z = v0.z - v1.z;
            return x * x + y * y + z * z;
        }

        /// <summary>
        /// The minimum value between s and each component in point.
        /// </summary>
        public static CLPoint3t Min(CLPoint3t v, REAL s)
        {
            v.x = Math.Min(v.x, s);
            v.y = Math.Min(v.y, s);
            v.z = Math.Min(v.z, s);
            return v;
        }

        /// <summary>
        /// The minimum value between each component in points.
        /// </summary>
        public static CLPoint3t Min(CLPoint3t v0, CLPoint3t v1)
        {
            v0.x = Math.Min(v0.x, v1.x);
            v0.y = Math.Min(v0.y, v1.y);
            v0.z = Math.Min(v0.z, v1.z);
            return v0;
        }

        /// <summary>
        /// The maximum value between s and each component in point.
        /// </summary>
        public static CLPoint3t Max(CLPoint3t v, REAL s)
        {
            v.x = Math.Max(v.x, s);
            v.y = Math.Max(v.y, s);
            v.z = Math.Max(v.z, s);
            return v;
        }

        /// <summary>
        /// The maximum value between each component in points.
        /// </summary>
        public static CLPoint3t Max(CLPoint3t v0, CLPoint3t v1)
        {
            v0.x = Math.Max(v0.x, v1.x);
            v0.y = Math.Max(v0.y, v1.y);
            v0.z = Math.Max(v0.z, v1.z);
            return v0;
        }

        /// <summary>
        /// Clamp each component to specified min and max.
        /// </summary>
        public static CLPoint3t Clamp(CLPoint3t v, REAL min, REAL max)
        {
            v.x = Math.Max(Math.Min(v.x, max), min);
            v.y = Math.Max(Math.Min(v.y, max), min);
            v.z = Math.Max(Math.Min(v.z, max), min);
            return v;
        }

        /// <summary>
        /// Clamp each component to specified min and max.
        /// </summary>
        public static CLPoint3t Clamp(CLPoint3t v, CLPoint3t min, CLPoint3t max)
        {
            v.x = Math.Max(Math.Min(v.x, max.x), min.x);
            v.y = Math.Max(Math.Min(v.y, max.y), min.y);
            v.z = Math.Max(Math.Min(v.z, max.z), min.z);
            return v;
        }

        /// <summary>
        /// Create a new point by reordering the componets.
        /// </summary>
        /// <param name="i">The index to take x value from.></param>
        /// <param name="j">The index to take y value from.</param>
        /// <param name="k">The index to take z value from.</param>
        /// <returns>The new vector</returns>
        public CLPoint3t Permutate(int i, int j, int k)
        {
            return new CLPoint3t(this[i], this[j], this[k]);
        }

    }
}
