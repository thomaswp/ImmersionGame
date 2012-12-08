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
        AnimatedHero myAnimatedHero;
        Background background;
        List<PlatformSprite> myPlatforms = new List<PlatformSprite>();
        Vector2 myScreenSize, offset = new Vector2();
        List<Sprite> mySprites = new List<Sprite>();
        List<WordSprite> myWordSprites = new List<WordSprite>();
        float worldScale = 1;
        Overlay overlay;

        public float WorldScale
        {
            get { return worldScale; }
            set { worldScale = value; }
        }

        GameData game;
        MapData map;


        public Game1(String gameFile = null)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.PreferredBackBufferWidth = 2000;
            //graphics.PreferredBackBufferHeight = 1250;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 750;

            graphics.ApplyChanges();

            myScreenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            if (gameFile != null)
            {
                game = GameData.ReadFromFile(gameFile);
                game.StartMap = game.LastEditedMap;
            }

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
            if (game == null)
            {
                game = GameData.ReadFromFile("../../../../../Game1.game");
            }

            map = game.StartMap;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D bgSprite = Content.Load<Texture2D>("space");
            background = new Background(bgSprite, (int)myScreenSize.X, (int)myScreenSize.Y);


            // TODO: use this.Content to load your game content here
            // Make the hero
            Texture2D heroImage = Content.Load<Texture2D>("hero");
            Texture2D shadow = Content.Load<Texture2D>("shadow");
            Texture2D[] heroImages = {Content.Load<Texture2D>("hero0"),
                                         Content.Load<Texture2D>("hero1"),
                                         Content.Load<Texture2D>("hero2"),
                                         Content.Load<Texture2D>("hero3")};
            Vector2 center = myScreenSize / 2;
            myAnimatedHero = new AnimatedHero(heroImages, shadow, center, myScreenSize);

            LoadMap(map);

            overlay = new SplashScreen(GraphicsDevice, Content);
        }

        public void LoadMap(MapData map)
        {
            this.map = map;

            mySprites.Clear();
            myPlatforms.Clear();
            myWordSprites.Clear();

            Vector2 center = myScreenSize / 2;

            Texture2D shadow = Content.Load<Texture2D>("shadow");

            //Make a Platform
            Texture2D plat45 = Content.Load<Texture2D>("platform45squished");
            foreach (PlatformData data in map.Platforms)
            {
                if (myAnimatedHero.Items.Contains(data.Item))
                {
                    data.Item = null;
                }
                PlatformSprite platform = new PlatformSprite(plat45, data);
                mySprites.Add(platform);
                myPlatforms.Add(platform);
                platform.LoadItemTextures(Content, shadow);
            }

            SpriteFont font = Content.Load<SpriteFont>("DefaultFont");
            foreach (WordCloudData wordCloud in map.WordClouds)
            {
                foreach (WordData word in wordCloud.Words)
                {
                    myWordSprites.Add(new WordSprite(word, font));
                }
            }


            //It's important to keep Hero added after the other sprites!
            mySprites.Add(myAnimatedHero);

            myAnimatedHero.Reset();
            int startIndex = map.Platforms.IndexOf(map.startPlatform);
            if (startIndex >= 0)
            {
                PlatformSprite startPlatform = myPlatforms[startIndex];
                startPlatform.Update(0);
                myAnimatedHero.myPosition = myPlatforms[startIndex].myPosition;
                myAnimatedHero.currentPlatform = myPlatforms[startIndex];
            }

            Update(new GameTime());
            offset = center - myAnimatedHero.myPosition;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        bool escDown;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            UpdateOffset();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (!escDown && overlay == null)
                {
                    overlay = new GameMenu(GraphicsDevice, Content);
                }
                escDown = true;
            }
            else
            {
                escDown = false;
            }

            if (overlay != null)
            {
                overlay.Update(gameTime);
                overlay.UpdateGame(this);

                if (overlay.IsFinished())
                {
                    overlay = null;
                }
                else
                {
                    return;
                }
            }

            worldScale = Lerp(worldScale, 1, 0.8f);

            // Here's where the input manager is told to deal with the input
            InputManager.ActKeyboard(Keyboard.GetState());
            InputManager.ActMouse(Mouse.GetState());

            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            myAnimatedHero.UpdateCurrentPlatform(elapsedTime, myPlatforms);
            foreach (Sprite s in mySprites)
            {
                s.Update(elapsedTime);
            }
            foreach (WordSprite word in myWordSprites)
            {
                word.Update(elapsedTime);
            }

            if (myAnimatedHero.IsTransitioned)
            {
                PlatformSprite currentPlatform = myAnimatedHero.currentPlatform;
                if (currentPlatform != null)
                {
                    MapData newMap = currentPlatform.data.NextMap;
                    if (newMap != null)
                    {
                        overlay = new MapTransition(GraphicsDevice, Content, newMap);
                    }
                }
            }

            base.Update(gameTime);
        }

        private void UpdateOffset()
        {

            int buffer = 350;// (int)(350 / worldScale);
            int width = (int)myScreenSize.X; // (int)(myScreenSize.X / worldScale);
            int height = (int)(myScreenSize.Y);// / worldScale);
            Vector2 heroPos = myAnimatedHero.myPosition + offset;
            if (heroPos.X < buffer) heroPos.X = buffer;
            if (heroPos.X > width - buffer) heroPos.X = myScreenSize.X - buffer;
            if (heroPos.Y < buffer) heroPos.Y = buffer;
            if (heroPos.Y > height - buffer) heroPos.Y = myScreenSize.Y - buffer;
            offset = Lerp(offset, heroPos - myAnimatedHero.myPosition, 0.9f);
        }

        public static Vector2 Lerp(Vector2 x0, Vector2 x1, float friction)
        {
            return x0 * friction + x1 * (1 - friction);
        }

        public static float Lerp(float x0, float x1, float friction)
        {
            return x0 * friction + x1 * (1 - friction);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Aqua);

            // TODO: Add your drawing code here
            Matrix m = Matrix.Identity;
            m = Matrix.Multiply(Matrix.CreateTranslation(myScreenSize.X / 2, myScreenSize.Y / 2, 0), m);
            m = Matrix.Multiply(Matrix.CreateScale(worldScale), m);
            m = Matrix.Multiply(Matrix.CreateTranslation(offset.X - myScreenSize.X / 2, offset.Y - myScreenSize.Y / 2, 0), m);

            spriteBatch.Begin();
            background.Draw(spriteBatch, offset, (float)Math.Sqrt(worldScale));
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, m);

            Vector2 localOffset = Vector2.Zero;
            foreach (WordSprite word in myWordSprites)
            {
                word.Draw(spriteBatch, localOffset);
            }

            foreach (Sprite s in mySprites)
            {
                s.Draw(spriteBatch, localOffset);
            }
            spriteBatch.End();

            if (overlay != null)
            {
                overlay.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}

