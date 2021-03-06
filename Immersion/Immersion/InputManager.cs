﻿// This object keeps up with a mapping of Keys to actions, handling keyboard input and mouse input

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Immersion
{
    // This is a static object.  Only one exists, (you can't say "new") and
    // it is accessible by anyone.
    public class InputManager
    {
        // XNA doesn't provide me with a collection of mouse things, so I ennumerate them here.
        public const int LEFT_BUTTON = 0;
        public const int RIGHT_BUTTON = 1;
        public const int POSITION = 2;

        // I now let one key or mouse position map to lots of actions.
        static Dictionary<int, List<GameAction>> myMouseMap = new Dictionary<int, List<GameAction>>();

        // This is the mapping from Keyboard Keys to GameAction objects
        static Dictionary<Keys, List<GameAction>> myKeyboardMap = new Dictionary<Keys, List<GameAction>>();

        static Dictionary<Keys, List<GameAction>> myKeyboardPressMap = new Dictionary<Keys, List<GameAction>>();

        static Dictionary<Buttons, List<GameAction>> myControllerMap = new Dictionary<Buttons, List<GameAction>>();

        //Used for single presses, instead of holds
        static Dictionary<Buttons, List<GameAction>> myControllerPressMap = new Dictionary<Buttons, List<GameAction>>();

        static List<Keys> myPressedKeys = new List<Keys>();
        static List<Buttons> myPressedButtons = new List<Buttons>();

        static InputManager myInstance = new InputManager();

        public static bool IsSliding()
        {
            return Keyboard.GetState().IsKeyDown(Keys.LeftShift) ||
                Keyboard.GetState().IsKeyDown(Keys.RightShift) ||
                GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None).IsButtonDown(Buttons.X);
        }

        public static void AddToMap<T>(Dictionary<T, List<GameAction>> map, T key, GameAction action)
        {
            myInstance.AddToMyMap<T>(map, key, action);
        }

        // Anyone can add a new mapping from key to action.  This method is generic, since I don't
        // want to copy and paste this method for the two different types of Dictionaries I deal with.
        public void AddToMyMap<T>(Dictionary<T, List<GameAction>> map, T key, GameAction action)
        {

            List<GameAction> keyList = new List<GameAction>();

            if (map.ContainsKey(key))
            {
                keyList = map[key];
                map.Remove(key);
            }
            keyList.Add(action);
            map.Add(key, keyList);
        }

        public static void AddToMouseMap(int button, GameAction action)
        {
            AddToMap<int>(myMouseMap, button, action);
        }

        public static void AddToKeyboardMap(Keys key, GameAction action)
        {
            AddToMap<Keys>(myKeyboardMap, key, action);
        }

        public static void AddToKeyboardPressMap(Keys key, GameAction action)
        {
            AddToMap<Keys>(myKeyboardPressMap, key, action);
        }

        public static void AddToControllerMap(Buttons button, GameAction action)
        {
            AddToMap<Buttons>(myControllerMap, button, action);
        }

        public static void AddToControllerPressMap(Buttons button, GameAction action)
        {
            AddToMap<Buttons>(myControllerPressMap, button, action);
        }



        public static Vector2 GetStickPosition()
        {
            GamePadState padState = GamePad.GetState(PlayerIndex.One);
            return padState.ThumbSticks.Left;
        }





        // Perform the functions in the MouseDictionary, given the current MouseState.
        public static void ActMouse(MouseState mouseState)
        {
            // I'm predicting that any method that cares about the mouse clicks will also care WHERE
            // the mouse click happened.
            object[] parameterList = new object[1];
            parameterList[0] = new Vector2(mouseState.X, mouseState.Y);

            // There's no ennumeration for the mouse buttons, so I have to have separate if statements.
            // The good news is that more buttons aren't likely to be added to the mouse any time soon.
            if (mouseState.LeftButton == ButtonState.Pressed && myMouseMap.ContainsKey(LEFT_BUTTON))
            {
                foreach (GameAction a in myMouseMap[LEFT_BUTTON])
                {
                    a.Invoke(parameterList);
                }
            }
            if (mouseState.RightButton == ButtonState.Pressed && myMouseMap.ContainsKey(RIGHT_BUTTON))
            {
                foreach (GameAction a in myMouseMap[RIGHT_BUTTON])
                {
                    a.Invoke(parameterList);
                }
            }
            if (myMouseMap.ContainsKey(POSITION))
            {
                foreach (GameAction a in myMouseMap[POSITION])
                {
                    a.Invoke(parameterList);
                }
            }
        }

        // This is called by the game - it gives the current 
        // state of the keyboard and makes the corresponding actions happen.
        public static void ActKeyboard(KeyboardState keyState)
        {
            Keys[] allPressed = keyState.GetPressedKeys();
            foreach (Keys k in allPressed)
            {
                if (myKeyboardMap.ContainsKey(k))
                {
                    List<GameAction> actionList = myKeyboardMap[k];
                    foreach (GameAction action in actionList)
                    {
                        action.Invoke();
                    }
                }

                //Same as above, but check if the key is unpressed
                if (myKeyboardPressMap.ContainsKey(k))
                {
                    if (!myPressedKeys.Contains(k))
                    {
                        List<GameAction> actionList = myKeyboardPressMap[k];
                        foreach (GameAction action in actionList)
                        {
                            action.Invoke();
                        }
                    }
                }
            }

            myPressedKeys.Clear();
            myPressedKeys.AddRange(allPressed);
        }

        public static void ActController(GamePadState padState)
        {
            GamePadState state = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);

            foreach (Buttons button in myControllerMap.Keys)
            {
                if (state.IsButtonDown(button))
                {
                    foreach (GameAction action in myControllerMap[button])
                    {
                        action.Invoke();
                    }
                }
            }

            //Same as above, but check if the button is unpressed
            foreach (Buttons button in myControllerPressMap.Keys)
            {
                if (state.IsButtonDown(button))
                {
                    if (!myPressedButtons.Contains(button))
                    {
                        foreach (GameAction action in myControllerPressMap[button])
                        {
                            action.Invoke();
                        }
                        myPressedButtons.Add(button);
                    }
                }
                else
                {
                    myPressedButtons.Remove(button);
                }
            }
        }

    }
}
