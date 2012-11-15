using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Immersion
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Hero myHero;
        Background background;
        List<PlatformSprite> myPlatforms = new List<PlatformSprite>();
        Vector2 myScreenSize, offset = new Vector2();
        List<Sprite> mySprites = new List<Sprite>();
        float worldScale = 1;

        MapData map;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();
            myScreenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            map = MapData.ReadFromFile("Map1.map");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Vector2 center = myScreenSize / 2;

            Texture2D bgSprite = Content.Load<Texture2D>("space");
            background = new Background(bgSprite, (int)myScreenSize.X, (int)myScreenSize.Y);

            // TODO: use this.Content to load your game content here
            // Make the hero
            Texture2D heroImage = Content.Load<Texture2D>("hero");
            Texture2D shadow = Content.Load<Texture2D>("shadow");
            myHero = new Hero(heroImage, shadow, center);

            //Make a Platform
            Texture2D plat45 = Content.Load<Texture2D>("platform45squished");
            foreach (PlatformData data in map.Platforms)
            {
                PlatformSprite platform = new PlatformSprite(plat45, data);
                mySprites.Add(platform);
                myPlatforms.Add(platform);
            }

            myHero.myPosition = myPlatforms[0].myPosition;
            offset = center;

            mySprites.Add(myHero);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Here's where the input manager is told to deal with the input
            InputManager.ActKeyboard(Keyboard.GetState());
            InputManager.ActMouse(Mouse.GetState());
            // TODO: Add your update logic here
            myHero.UpdateCurrentPlatform(myPlatforms);
            foreach (Sprite s in mySprites)
            {
                s.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            int buffer = 300;
            Vector2 heroPos = myHero.myPosition + offset;
            if (heroPos.X < buffer) heroPos.X = buffer;
            if (heroPos.X > myScreenSize.X - buffer) heroPos.X = myScreenSize.X - buffer;
            if (heroPos.Y < buffer) heroPos.Y = buffer;
            if (heroPos.Y > myScreenSize.Y - buffer) heroPos.Y = myScreenSize.Y - buffer;
            offset = offset * 0.9f + (heroPos - myHero.myPosition) * 0.1f;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (worldScale > 0.2f)
                    worldScale *= 0.99f;
                offset = myScreenSize / worldScale / 2;
            }
            else
            {
                if (worldScale < 1)
                    worldScale *= 1.01f;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aqua);

            // TODO: Add your drawing code here
            Matrix m = Matrix.CreateScale(worldScale);

            spriteBatch.Begin();
            background.Draw(spriteBatch, offset, (float)Math.Sqrt(worldScale));
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, m);
            foreach (Sprite s in mySprites)
            {
                s.Draw(spriteBatch, offset);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
