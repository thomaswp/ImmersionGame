﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Immersion
{
    //Holds the importand variables for the game that
    //most classes need access to
    public class GameState
    {
        public const float TIME_MULTIPLIER = 30;
        public const float DISTANCE_MULTIPLIER = MapData.DISTANCE_MULTIPLIER;

        public AnimatedHero myAnimatedHero;
        public Vector2 myScreenSize, offset = new Vector2();
        public List<PlatformSprite> myPlatforms = new List<PlatformSprite>();
        public List<Sprite> mySprites = new List<Sprite>();
        public List<WordSprite> myWordSprites = new List<WordSprite>();
    }
}
