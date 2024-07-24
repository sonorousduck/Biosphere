using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SequoiaEngine;
using System;
using MonoGame.Extended.BitmapFonts;
using System.Diagnostics;

namespace Biosphere
{
    public static class SpeedControl
    {
        public static GameObject Create(SystemManager systemManager)
        {
            GameObject parentObject = new GameObject(new Transform(new Vector2(GameManager.Instance.RenderWidth - 200, 20), 0f, new Vector2(186, 37), isHUD: true));

            Sprite testLocationSprite = new Sprite(ResourceManager.Get<Texture2D>("default"), Color.Blue);

            float correctX = 0f;

            parentObject.Add(testLocationSprite);


            Button doubleSlow = new Button(
                Vector2.Zero,
                0f,
                Vector2.One,
                ResourceManager.Get<Texture2D>("doubleSlow"),
                ResourceManager.Get<Texture2D>("doubleSlow_hover"),
                ResourceManager.Get<Texture2D>("doubleSlow_pressed"),
                anchorLocation: AnchorLocation.MiddleLeft,
                onPress: (GameObject button) => {
                    TimeManager.Instance.SetGameSpeed(0.3f);
                    Debug.WriteLine("Set to 0.3x");
                },
                parent: parentObject
            );

            systemManager.Add(doubleSlow.GameObject);



            doubleSlow.GameObject.GetComponent<Transform>().position.X += doubleSlow.GameObject.GetComponent<Sprite>().center.X;

            correctX += doubleSlow.GameObject.GetComponent<Sprite>().center.X;

            Button slow = new Button(
                Vector2.Zero,
                0f,
                Vector2.One,
                ResourceManager.Get<Texture2D>("slow"),
                ResourceManager.Get<Texture2D>("slow_hover"),
                ResourceManager.Get<Texture2D>("slow_pressed"),
                anchorLocation: AnchorLocation.MiddleLeft,
                onPress: (GameObject button) => {
                    TimeManager.Instance.SetGameSpeed(0.7f);
                    Debug.WriteLine("Set to 0.7x");
                    button.GetComponent<Sprite>().sprite = button.GetComponent<ButtonComponent>().SpriteImagePressed;
                },
                parent: parentObject
            );

            slow.GameObject.GetComponent<Transform>().position.X += slow.GameObject.GetComponent<Sprite>().size.X + correctX;

            correctX += slow.GameObject.GetComponent<Sprite>().size.X;

            systemManager.Add(slow.GameObject);

            Button play = new Button(
                Vector2.Zero,
                0f,
                Vector2.One,
                ResourceManager.Get<Texture2D>("play"),
                ResourceManager.Get<Texture2D>("play_hover"),
                ResourceManager.Get<Texture2D>("play_pressed"),
                anchorLocation: AnchorLocation.MiddleLeft,
                onPress: (GameObject button) => {
                    TimeManager.Instance.SetGameSpeed(1f);
                    Debug.WriteLine("Set to 1.0x");
                },
                parent: parentObject
            );

            play.GameObject.GetComponent<Transform>().position.X += play.GameObject.GetComponent<Sprite>().size.X + correctX;

            correctX += play.GameObject.GetComponent<Sprite>().size.X;

            systemManager.Add(play.GameObject);

            Button fast = new Button(
                Vector2.Zero,
                0f,
                Vector2.One,
                ResourceManager.Get<Texture2D>("fast"),
                ResourceManager.Get<Texture2D>("fast_hover"),
                ResourceManager.Get<Texture2D>("fast_pressed"),
                anchorLocation: AnchorLocation.MiddleLeft,
                onPress: (GameObject button) => {
                    TimeManager.Instance.SetGameSpeed(2f);
                    Debug.WriteLine("Set to 2.0x");
                },
                parent: parentObject
            );

            fast.GameObject.GetComponent<Transform>().position.X += fast.GameObject.GetComponent<Sprite>().size.X + correctX;
            correctX += fast.GameObject.GetComponent<Sprite>().size.X;

            systemManager.Add(fast.GameObject);


            Button doubleFast = new Button(
                Vector2.Zero,
                0f,
                Vector2.One,
                ResourceManager.Get<Texture2D>("doubleFast"),
                ResourceManager.Get<Texture2D>("doubleFast_hover"),
                ResourceManager.Get<Texture2D>("doubleFast_pressed"),
                anchorLocation: AnchorLocation.MiddleLeft,
                onPress: (GameObject button) => {
                    TimeManager.Instance.SetGameSpeed(4f);
                    Debug.WriteLine("Set to 4.0x");
                },
                parent: parentObject
);

            doubleFast.GameObject.GetComponent<Transform>().position.X += doubleFast.GameObject.GetComponent<Sprite>().size.X + correctX;

            systemManager.Add(doubleFast.GameObject);

            /*Action onTimeSpeedDown = () =>
            {
                TimeManager.Instance.DecreaseGameSpeed(0.1f);
            };

            Action onTimeSpeedUp = () =>
            {
                TimeManager.Instance.IncreaseGameSpeed(0.1f);
            };*/




            /* Button plusButton = new Button(
                 Vector2.Zero,
                 0f,
                 Vector2.One,
                 ResourceManager.Get<Texture2D>("speedTimeButton"),
                 ResourceManager.Get<Texture2D>("speedTimeButton_hover"),
                 ResourceManager.Get<Texture2D>("speedTimeButton_pressed"),
                 anchorLocation: AnchorLocation.MiddleRight,
                 onPress: onTimeSpeedUp,
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
                 onPress: onTimeSpeedDown,
                 parent: parentObject
             );

             systemManager.Add(minusButton.GameObject);*/

            /*GameObject textObject = new GameObject(new Transform(isHUD: true), parent: parentObject);

            // TODO: Create script that updates this

            Text text = new Text(TimeManager.Instance.GetGameSpeed().ToString(), Color.White, Color.Transparent, ResourceManager.Get<BitmapFont>("default_pixel_18"));


            Anchor anchor = new(AnchorLocation.MiddleMiddle);

            textObject.Add(new GameSpeedTextScript(textObject));

            textObject.Add(text);
            textObject.Add(anchor);

            textObject.GetComponent<Transform>().position += anchor.GetAnchorPoint(textObject);


            systemManager.Add(textObject); */


            return parentObject;
        }

    }
}
