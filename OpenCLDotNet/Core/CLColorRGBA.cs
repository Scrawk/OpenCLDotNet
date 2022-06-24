using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Core
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public record struct CLColorRGBA
    {
        public const int Channels = 4;

        public readonly static CLColorRGBA Red = new CLColorRGBA(1, 0, 0, 1);
        public readonly static CLColorRGBA Orange = new CLColorRGBA(1, 0.5, 0, 1);
        public readonly static CLColorRGBA Olive = new CLColorRGBA(0.5, 0.5, 0, 1);
        public readonly static CLColorRGBA VividGreen = new CLColorRGBA(0.5, 1, 0, 1);
        public readonly static CLColorRGBA Yellow = new CLColorRGBA(1, 1, 0, 1);
        public readonly static CLColorRGBA Green = new CLColorRGBA(0, 1, 0, 1);
        public readonly static CLColorRGBA SpringGreen = new CLColorRGBA(0, 1, 0.5, 1);
        public readonly static CLColorRGBA Cyan = new CLColorRGBA(0, 1, 1, 1);
        public readonly static CLColorRGBA Azure = new CLColorRGBA(0, 0.5, 1, 1);
        public readonly static CLColorRGBA Teal = new CLColorRGBA(0, 0.5, 0.5, 1);
        public readonly static CLColorRGBA Blue = new CLColorRGBA(0, 0, 1, 1);
        public readonly static CLColorRGBA Violet = new CLColorRGBA(0.5, 0, 1, 1);
        public readonly static CLColorRGBA Purple = new CLColorRGBA(0.5, 0, 0.5, 1);
        public readonly static CLColorRGBA Magenta = new CLColorRGBA(1, 0, 1, 1);

        public readonly static CLColorRGBA Black = new CLColorRGBA(0, 1);
        public readonly static CLColorRGBA DarkGrey = new CLColorRGBA(0.25f, 1);
        public readonly static CLColorRGBA Grey = new CLColorRGBA(0.5f, 1);
        public readonly static CLColorRGBA LightGrey = new CLColorRGBA(0.75f, 1);
        public readonly static CLColorRGBA White = new CLColorRGBA(1, 1);
        public readonly static CLColorRGBA Clear = new CLColorRGBA(0, 0);

        public float r, g, b, a;

        public CLColorRGBA(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public CLColorRGBA(float r, float g, float b)
            : this(r, g, b, 1)
        {

        }

        public CLColorRGBA(float v)
            : this(v, v, v, v)
        {

        }

        public CLColorRGBA(float v, float a)
            : this(v, v, v, 1)
        {

        }

        public CLColorRGBA(double r, double g, double b, double a)
            : this((float)r, (float)g, (float)b, (float)a)
        {

        }

        public CLColorRGBA(double r, double g, double b)
            : this(r, g, b, 1)
        {

        }

        public CLColorRGBA(double v)
            : this(v, v, v, v)
        {

        }

        public CLColorRGBA(double v, double a)
            : this(v, v, v, a)
        {

        }

        unsafe public float this[int i]
        {
            get
            {
                if ((uint)i >= Channels)
                    throw new IndexOutOfRangeException("CLColorRGBA index out of range.");

                fixed (CLColorRGBA* array = &this) { return ((float*)array)[i]; }
            }
            set
            {
                if ((uint)i >= Channels)
                    throw new IndexOutOfRangeException("CLColorRGBA index out of range.");

                fixed (float* array = &r) { array[i] = value; }
            }
        }

        public CLColorRGBA rrra
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new CLColorRGBA(r, r, r, a); }
        }

        public CLColorRGBA bgra
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new CLColorRGBA(b, g, r, a); }
        }

        public float Magnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return (float)Math.Sqrt(SqrMagnitude); }
        }

        public float SqrMagnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return (r * r + g * g + b * b + a * a); }
        }

        public float Intensity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return (r + g + b) / 3.0f; }
        }

        public float Luminance
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return 0.2126f * r + 0.7152f * g + 0.0722f * b; }
        }

        /// <summary>
        /// Add two colors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLColorRGBA operator +(CLColorRGBA v1, CLColorRGBA v2)
        {
            return new CLColorRGBA(v1.r + v2.r, v1.g + v2.g, v1.b + v2.b, v1.a + v2.a);
        }

        /// <summary>
        /// Add color and scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLColorRGBA operator +(CLColorRGBA v1, float s)
        {
            return new CLColorRGBA(v1.r + s, v1.g + s, v1.b + s, v1.a + s);
        }

        /// <summary>
        /// Add color and scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLColorRGBA operator +(float s, CLColorRGBA v1)
        {
            return new CLColorRGBA(v1.r + s, v1.g + s, v1.b + s, v1.a + s);
        }

        /// <summary>
        /// Subtract two colors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLColorRGBA operator -(CLColorRGBA v1, CLColorRGBA v2)
        {
            return new CLColorRGBA(v1.r - v2.r, v1.g - v2.g, v1.b - v2.b, v1.a - v2.a);
        }

        /// <summary>
        /// Subtract color and scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLColorRGBA operator -(CLColorRGBA v1, float s)
        {
            return new CLColorRGBA(v1.r - s, v1.g - s, v1.b - s, v1.a - s);
        }

        /// <summary>
        /// Subtract color and scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLColorRGBA operator -(float s, CLColorRGBA v1)
        {
            return new CLColorRGBA(s - v1.r, s - v1.g, s - v1.b, s - v1.a);
        }

        /// <summary>
        /// Multiply two colors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLColorRGBA operator *(CLColorRGBA v1, CLColorRGBA v2)
        {
            return new CLColorRGBA(v1.r * v2.r, v1.g * v2.g, v1.b * v2.b, v1.a * v2.a);
        }

        /// <summary>
        /// Multiply a color and a scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLColorRGBA operator *(CLColorRGBA v, float s)
        {
            return new CLColorRGBA(v.r * s, v.g * s, v.b * s, v.a * s);
        }

        /// <summary>
        /// Multiply a color and a scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLColorRGBA operator *(float s, CLColorRGBA v)
        {
            return new CLColorRGBA(v.r * s, v.g * s, v.b * s, v.a * s);
        }

        /// <summary>
        /// Divide two colors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLColorRGBA operator /(CLColorRGBA v1, CLColorRGBA v2)
        {
            return new CLColorRGBA(v1.r / v2.r, v1.g / v2.g, v1.b / v2.b, v1.a / v2.a);
        }

        /// <summary>
        /// Divide a color and a scalar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CLColorRGBA operator /(CLColorRGBA v, float s)
        {
            return new CLColorRGBA(v.r / s, v.g / s, v.b / s, v.a / s);
        }

        /// <summary>
        /// color as a string.
        /// </summary>
		public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", r, g, b, a);
        }

        /// <summary>
        /// color as a string.
        /// </summary>
        public string ToString(string f)
        {
            return string.Format("{0},{1},{2},{3}", r.ToString(f), g.ToString(f), b.ToString(f), a.ToString(f));
        }

        /// <summary>
        /// color from bytes.
        /// The values will be converted from a 0-255 range to a 0-1 range.
        /// </summary>
        /// <returns>A color will values in the 0-1 range.</returns>
        public static CLColorRGBA FromBytes(int r, int g, int b, int a)
        {
            int R = Math.Clamp(r, 0, 255);
            int G = Math.Clamp(g, 0, 255);
            int B = Math.Clamp(b, 0, 255);
            int A = Math.Clamp(a, 0, 255);
            return new CLColorRGBA(R, G, B, A) / 255.0f;
        }

        /// <summary>
        /// Scale and clamp each channel to 0-255 range 
        /// and copy into byte array.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="offset">Optional offset into byte array.</param>
        public void ToBytes(byte[] bytes, int offset = 0)
        {
            for (int i = 0; i < Channels; i++)
                bytes[offset + i] = (byte)Math.Clamp(this[i] * 255, 0, 255);
        }

        /// <summary>
        /// Convert the color to a integer where each byte 
        /// represents a channel in the color.
        /// </summary>
        /// <param name="abgr">are the channels packed bgr or rgb.</param>
        /// <returns>A integer where each byte represents a channel in the color.</returns>
        public int ToInteger(bool abgr = false)
        {
            int R = (int)Math.Clamp(r * 255.0f, 0.0f, 255.0f);
            int G = (int)Math.Clamp(g * 255.0f, 0.0f, 255.0f);
            int B = (int)Math.Clamp(b * 255.0f, 0.0f, 255.0f);
            int A = (int)Math.Clamp(a * 255.0f, 0.0f, 255.0f);

            if (abgr)
                return (A << 24) | (B << 16) | (G << 8) | R;
            else
                return R | (G << 8) | (B << 16) | (A << 24);
        }

        /// <summary>
        /// Apply the gamma function to the color.
        /// Gamma is not applied to the alpha channel.
        /// </summary>
        /// <param name="lambda">The power to raise each channel to.</param>
        /// <param name="A">The constant the result is multiplied by. Defaults to 1.</param>
        public void Gamma(float lambda, float A = 1)
        {
            r = (float)Math.Pow(r, lambda) * A;
            g = (float)Math.Pow(g, lambda) * A;
            b = (float)Math.Pow(b, lambda) * A;
        }

        /// <summary>
        /// The distance between two colors.
        /// </summary>
        public static float Distance(CLColorRGBA c0, CLColorRGBA c1)
        {
            return (float)Math.Sqrt(SqrDistance(c0, c1));
        }

        /// <summary>
        /// The square distance between two colors.
        /// </summary>
        public static float SqrDistance(CLColorRGBA c0, CLColorRGBA c1)
        {
            return (c0 - c1).SqrMagnitude;
        }

        /// <summary>
        /// The minimum value between s and each component in vector.
        /// </summary>
        public static CLColorRGBA Min(CLColorRGBA col, float s)
        {
            col.r = Math.Min(col.r, s);
            col.g = Math.Min(col.g, s);
            col.b = Math.Min(col.b, s);
            col.a = Math.Min(col.a, s);
            return col;
        }

        /// <summary>
        /// The maximum value between s and each component in vector.
        /// </summary>
        public static CLColorRGBA Max(CLColorRGBA col, float s)
        {
            col.r = Math.Max(col.r, s);
            col.g = Math.Max(col.g, s);
            col.b = Math.Max(col.b, s);
            col.a = Math.Max(col.a, s);
            return col;
        }

        /// <summary>
        /// Clamp the each component to specified min and max.
        /// </summary>
        public static CLColorRGBA Clamp(CLColorRGBA col, float min, float max)
        {
            col.r = Math.Max(Math.Min(col.r, max), min);
            col.g = Math.Max(Math.Min(col.g, max), min);
            col.b = Math.Max(Math.Min(col.b, max), min);
            col.a = Math.Max(Math.Min(col.a, max), min);
            return col;
        }

        /// <summary>
        /// Create a new color by reordering the componets.
        /// </summary>
        /// <param name="i">The index to take x value from.></param>
        /// <param name="j">The index to take y value from.</param>
        /// <param name="k">The index to take z value from.</param>
        /// <param name="l">The index to take z value from.</param>
        /// <returns>The new color.</returns>
        public CLColorRGBA Permutate(int i, int j, int k, int l)
        {
            return new CLColorRGBA(this[i], this[j], this[k], this[l]);
        }

    }

}
