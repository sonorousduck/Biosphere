using System;
using Microsoft.Xna.Framework;

namespace SequoiaEngine
{
    public class RectangleCollider : Collider
    {
        // Centered at object's position
        public Vector2 size;
        public bool IsColliding;

        public RectangleCollider(Vector2 size, bool isStatic, CollisionLayer layer = CollisionLayer.All, CollisionLayer layersToCollideWith = CollisionLayer.All, float xOffset = 0, float yOffset = 0)
        {
            offset = new Vector2(xOffset, yOffset);
            this.size = size;
            this.isStatic = isStatic;

            this.Layer = layer;
            this.LayersToCollideWith = layersToCollideWith;
        }
    }
}