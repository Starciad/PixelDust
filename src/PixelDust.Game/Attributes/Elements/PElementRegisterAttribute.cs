﻿using PixelDust.Game.Databases;

using System;

namespace PixelDust.Game.Attributes.Elements
{
    /// <summary>
    /// Attribute used by the <see cref="PElementDatabase"/> class as a guide for registering elements during game startup.
    /// </summary>
    /// <remarks>
    /// It is especially useful in contexts where elements are inherited, which can have a certain level of search complexity that is mitigated by the <see cref="PElementRegisterAttribute"/>.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PElementRegisterAttribute : Attribute
    {
        /// <summary>
        /// Id number of the element.
        /// </summary>
        public byte Id { get; private set; }

        public PElementRegisterAttribute(byte id)
        {
            this.Id = id;
        }
    }
}
