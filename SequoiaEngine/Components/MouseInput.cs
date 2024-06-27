using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;


namespace SequoiaEngine
{
    /// <summary>
    /// Represents the possible buttons on a mouse. Left middle and right are self explanatory, while x1 and x2 are the potential side mouse buttons
    /// </summary>
    public enum MouseButton
    {
        LeftButton,
        MiddleButton,
        RightButton,
        x1Button,
        x2Button,
        scrollWheelUp,
        scrollWheelDown,
    }
    public class MouseInput : Input
    {
        public float RelativeX = 0;
        public float RelativeY = 0;
        
        public float PositionX = 0;
        public float PositionY = 0;
        public float PreviousX = 0;
        public float PreviousY = 0;

        public Vector2 Position = new();
        public Vector2 PreviousPosition = new();
        public Vector2 RelativePosition = new();

        public int ScrollPosition = 0;

        public Dictionary<MouseButton, string> Bindings = new();
        public Dictionary<string, Action> OnPressActions = new();
        public Dictionary<string, Action> OnHeldActions = new();
        public Dictionary<string, Action> OnReleaseActions = new();


        public Action OnMouseMove;


        public void SetMousePosition(Vector2 absolutePosition, Vector2 relativePosition)
        {
            PreviousX = PositionX;
            PreviousY = PositionY;

            PreviousPosition = Position;
            Position = absolutePosition;
            RelativePosition = relativePosition;



            PositionX = absolutePosition.X;
            PositionY = absolutePosition.Y;

            RelativeX = relativePosition.X;
            RelativeY = relativePosition.Y;
        }

        public void SaveMouseBindings()
        {
            Debug.WriteLine("TODO: Implement!");
        }

        public void LoadMouseBindings()
        {
            Debug.WriteLine("TODO: Implement!");
        }




        /*public Vector2 position;
        public Vector2 previousPosition;
        public int previousScrollWheelValue;
        public Dictionary<string, MouseButton> actionButtonPairs;
        public Dictionary<string, bool> actions;
        public Dictionary<string, bool> previousActions;

        public MouseInput()
        {
            position = new Vector2();
            previousPosition = new Vector2();
            actionButtonPairs = new Dictionary<string, MouseButton>();
            actions = new Dictionary<string, bool>();
            previousActions = new Dictionary<string, bool>();
            previousScrollWheelValue = 0;
        }

        /// <summary>
        /// Performs the conversion between the mouse's pixel position to the world's physics coordinates
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCurrentPhysicsPosition()
        {
            Vector2 distanceFromCenterPixels = position - RenderingSystem.centerOfScreen;
            Vector2 physicsDistanceFromCenter = distanceFromCenterPixels;
            Vector2 truePosition = physicsDistanceFromCenter + new Vector2(PhysicsSystem.PHYSICS_DIMENSION_WIDTH, PhysicsSystem.PHYSICS_DIMENSION_HEIGHT) / 2;

            return truePosition;
        }

        /// <summary>
        /// Gets the location of the cursor relative to the camera's position
        /// </summary>
        /// <param name="cameraLocation"></param>
        /// <returns></returns>
        public Vector2 PhysicsPositionCamera(Transform cameraLocation)
        {
            Vector2 distanceFromCenterPixels = position - RenderingSystem.centerOfScreen;
            Vector2 physicsDistanceFromCenter = distanceFromCenterPixels;
            Vector2 truePosition = physicsDistanceFromCenter + cameraLocation.position;

            return truePosition;
        }*/
    }
}