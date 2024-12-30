﻿using Microsoft.Xna.Framework.Graphics;

using StardustSandbox.Core.Enums.Items;

namespace StardustSandbox.Core.Catalog
{
    public sealed class SItem(string identifier, string displayName, string description, SItemContentType contentType, SSubcategory subcategory, Texture2D iconTexture)
    {
        public string Identifier => identifier;
        public string DisplayName => displayName;
        public string Description => description;
        public SItemContentType ContentType => contentType;
        public SCategory Category => subcategory.Parent;
        public SSubcategory Subcategory => subcategory;
        public Texture2D IconTexture => iconTexture;
    }
}