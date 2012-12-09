using System;
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
    public class GameState
    {
        public AnimatedHero myAnimatedHero;
        public Vector2 myScreenSize, offset = new Vector2();
        public List<PlatformSprite> myPlatforms = new List<PlatformSprite>();
        public List<Sprite> mySprites = new List<Sprite>();
        public List<WordSprite> myWordSprites = new List<WordSprite>();
    }
}
