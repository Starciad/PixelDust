﻿using System;
using Microsoft.Xna.Framework;

namespace PixelDust.Core.Mathematics
{
    public struct Size2Int : IEquatable<Size2Int>
    {
        public static readonly Size2Int Empty = new();
        public readonly bool IsEmpty => Width == 0 && Height == 0;

        public int Width { get; set; }
        public int Height { get; set; }

        public Size2Int(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static implicit operator Size2Int(Point point)
        {
            return new Size2Int(point.X, point.Y);
        }
        public static implicit operator Point(Size2Int size)
        {
            return new Point(size.Width, size.Height);
        }

        public static explicit operator Size2Int(Size2 size)
        {
            return new Size2Int((int)size.Width, (int)size.Height);
        }

        public static Size2Int operator +(Size2Int first, Size2Int second)
        {
            return Add(first, second);
        }
        public static Size2Int operator -(Size2Int first, Size2Int second)
        {
            return Subtract(first, second);
        }
        public static Size2Int operator *(Size2Int size, int value)
        {
            return new Size2Int(size.Width * value, size.Height * value);
        }
        public static Size2Int operator /(Size2Int size, int value)
        {
            return new Size2Int(size.Width / value, size.Height / value);
        }

        public static bool operator ==(Size2Int first, Size2Int second)
        {
            return first.Equals(ref second);
        }
        public static bool operator !=(Size2Int first, Size2Int second)
        {
            return !(first == second);
        }

        public static Size2Int Add(Size2Int first, Size2Int second)
        {
            Size2Int size = Empty;
            size.Width = first.Width + second.Width;
            size.Height = first.Height + second.Height;
            return size;
        }
        public static Size2Int Subtract(Size2Int first, Size2Int second)
        {
            Size2Int size = Empty;
            size.Width = first.Width - second.Width;
            size.Height = first.Height - second.Height;
            return size;
        }

        public readonly override string ToString()
        {
            return $"{{ Width: {Width}, Height: {Height} }}";
        }
        public readonly override int GetHashCode()
        {
            unchecked
            {
                return (Width.GetHashCode() * 397) ^ Height.GetHashCode();
            }
        }

        public readonly bool Equals(Size2Int size)
        {
            return Equals(ref size);
        }
        public readonly bool Equals(ref Size2Int size)
        {
            return Width == size.Width && Height == size.Height;
        }
        public readonly override bool Equals(object obj)
        {
            if (obj is Size2Int @int)
                return Equals(@int);
            return false;
        }
    }
}