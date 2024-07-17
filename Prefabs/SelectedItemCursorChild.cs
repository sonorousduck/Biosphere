using Microsoft.Xna.Framework.Graphics;
using SequoiaEngine;
using Microsoft.Xna.Framework;

namespace Biosphere
{
    public static class SelectedItemCursorChild
    {
        public static GameObject Create(GameObject cursorParent)
        {
            Transform parentTransform = cursorParent.GetComponent<Transform>();
            // Make a copy of it, not use it directly. Using it directly makes it get applied to both
            Transform transform = new Transform(new Vector2(parentTransform.position.X, parentTransform.position.Y), parentTransform.rotation, parentTransform.scale, true);

            GameObject gameObject = new GameObject(transform, parent: cursorParent);

            gameObject.Add(new Sprite(ResourceManager.Get<Texture2D>("plainsStoreTile"), Color.White));

            gameObject.Add(new SelectedItemCursorScript(gameObject));

            return gameObject;
        }
    }
}
