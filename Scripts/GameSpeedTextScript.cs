using Microsoft.Xna.Framework;
using MonoGame.Extended;
using SequoiaEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosphere
{
    public class GameSpeedTextScript : Script
    {
        public GameSpeedTextScript(GameObject gameObject) : base(gameObject) { }

        private Text text;
        private Transform transform;
        private Anchor anchor;

        public override void Start()
        {
            text = gameObject.GetComponent<Text>();
            transform = gameObject.GetComponent<Transform>();
            anchor = gameObject.GetComponent<Anchor>();
        }

        public override void Update(GameTime gameTime)
        {
            text.text = TimeManager.Instance.GetGameSpeed().ToString("0.#");
            SizeF size = text.bitmapFont.MeasureString(text.text);

            transform.position = anchor.GetAnchorPoint(gameObject) - new Vector2((int)(size.Width / 2), (int)(size.Height / 2)); 
        }
    }
}
