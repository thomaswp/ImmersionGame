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
    public class MapTransition : Overlay
    {
        MapData map;
        Texture2D black, end;
        bool finished;
        bool finishing;
        float alpha;

        public MapTransition(GraphicsDevice graphicsDevice, ContentManager content, GameState gameState, MapData map)
            : base(graphicsDevice, content, gameState)
        {
            this.map = map;
        }

        protected override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            black = content.Load<Texture2D>("black");
            end = content.Load<Texture2D>("EndScreen");
        }

        public override bool IsFinished()
        {
            return finished;
        }

        public override void UpdateGame(ImmersionGame game)
        {
            base.UpdateGame(game);
            
            
            if (!finishing)
            {
                if (map.Name == "Tutorial")
                {
                    bool anyKey = Keyboard.GetState().GetPressedKeys().Length > 0 || GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None).Buttons.ToString() != "{Buttons:None}";
                    if (anyKey)
                    {
                        finishing = true;
                        game.LoadMap(map);
                        alpha = 300;
                    }
                }
                else
                {
                    if (alpha >= 300)
                    {
                        finishing = true;
                        game.LoadMap(map);
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (finishing)
            {
                alpha -= gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                alpha += gameTime.ElapsedGameTime.Milliseconds;
            }

            if (finishing && alpha <= 0)
            {
                finished = true;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            Color color = new Color(255, 255, 255, Math.Min(Math.Max((int)alpha, 0), 255));
            spriteBatch.Draw(map.Name == "Tutorial" ? end : black, new Rectangle(0, 0, resolution.X, resolution.Y), color);
            spriteBatch.End();
        }
    }
}
