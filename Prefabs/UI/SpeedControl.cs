using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SequoiaEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.BitmapFonts;

namespace Biosphere
{
    public static class SpeedControl
    {
        public static GameObject Create(SystemManager systemManager)
        {
            GameObject parentObject = new GameObject(new Transform(new Vector2(GameManager.Instance.RenderWidth - 150, 40), 0f, new Vector2(96, 32), isHUD: true));

            Sprite testLocationSprite = new Sprite(ResourceManager.Get<Texture2D>("default"), Color.LightCoral, isHUD: true);


            parentObject.Add(testLocationSprite);



            Action onTimeSpeedDown = () =>
            {
                TimeManager.Instance.DecreaseGameSpeed(0.1f);
            };

            Action onTimeSpeedUp = () =>
            {
                TimeManager.Instance.IncreaseGameSpeed(0.1f);
            };




            Button plusButton = new Button(
                Vector2.Zero,
                0f,
                Vector2.One,
                ResourceManager.Get<Texture2D>("speedTimeButton"),
                ResourceManager.Get<Texture2D>("speedTimeButton_hover"),
                ResourceManager.Get<Texture2D>("speedTimeButton_pressed"),
                anchorLocation: AnchorLocation.MiddleRight,
                onPress: onTimeSpeedDown,
                parent: parentObject
            );

            systemManager.Add(plusButton.GameObject);


            Button minusButton = new Button(
                new Vector2(0, 0),
                0f,
                Vector2.One,
                ResourceManager.Get<Texture2D>("slowTimeButton"),
                ResourceManager.Get<Texture2D>("slowTimeButton_pressed"),
                ResourceManager.Get<Texture2D>("slowTimeButton_hover"),
                anchorLocation: AnchorLocation.MiddleLeft,
                onPress: onTimeSpeedUp,
                parent: parentObject
            );

            systemManager.Add(minusButton.GameObject);

            GameObject textObject = new GameObject(new Transform(), parent: parentObject);

            // TODO: Create script that updates this

            Text text = new Text(TimeManager.Instance.GetGameSpeed().ToString(), Color.White, Color.Transparent, ResourceManager.Get<BitmapFont>("default_pixel_18"));

            textObject.Add(text);



            systemManager.Add(textObject);


            return parentObject;
        }

    }
}
