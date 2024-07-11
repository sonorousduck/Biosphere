using Microsoft.Xna.Framework.Graphics;
using SequoiaEngine;
using Microsoft.Xna.Framework;

namespace Biosphere
{
    public static class SelectedItemCursorChild
    {
        public static GameObject Create(GameObject cursorParent)
        {
            GameObject gameObject = new GameObject(cursorParent.GetComponent<Transform>(), parent: cursorParent);

            gameObject.Add(new Sprite(ResourceManager.Get<Texture2D>("plainsStoreTile"), Color.White));


            return gameObject;
        }
    }
}
