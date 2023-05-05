using DormLife.Models;
using DormLife.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DormLife
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int Width;
        public static int Height;


        private MainHero hero;
        private Cake cake;
        private List<Wall> walls;

        private Enemies enemies;


        private List<GameObject> sprites;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            sprites = new List<GameObject>();

            Width = _graphics.PreferredBackBufferWidth;
            Height = _graphics.PreferredBackBufferHeight;


            var playerTexture = Content.Load<Texture2D>("sprites/player");
            var cakeTexture = Content.Load<Texture2D>("sprites/cake");
            var enemyTexture = Content.Load<Texture2D>("sprites/monster");
            var gorizontalWallTexture = Content.Load<Texture2D>("walls/gorizontal_wall");
            var verticalWallTexture = Content.Load<Texture2D>("walls/vertical_wall");





            hero = new MainHero(playerTexture, new Vector2(560, 300), 7);
            cake = new Cake(cakeTexture, new Vector2(Width / 2, Height / 2 - 50));
            walls = new List<Wall>{
                new Wall(gorizontalWallTexture, new Vector2(250, 100)),
                new Wall(gorizontalWallTexture, new Vector2(300, 100)),
                new Wall(gorizontalWallTexture, new Vector2(350, 100)),
                new Wall(gorizontalWallTexture, new Vector2(400, 100)),
                new Wall(gorizontalWallTexture, new Vector2(450, 100)),



                new Wall(verticalWallTexture, new Vector2(Width / 2 + 30, Height / 2 + 210)),
                new Wall(verticalWallTexture, new Vector2(Width / 2 + 30, Height / 2 + 160)),

                new Wall(gorizontalWallTexture, new Vector2(Width / 2 + 70, Height / 2 + 110)),
                new Wall(gorizontalWallTexture, new Vector2(Width / 2 + 120, Height / 2 + 110)),
                new Wall(gorizontalWallTexture, new Vector2(Width / 2 + 170, Height / 2 + 110)),
                new Wall(gorizontalWallTexture, new Vector2(Width / 2 + 220, Height / 2 + 110)),
                new Wall(gorizontalWallTexture, new Vector2(Width / 2 + 270, Height / 2 + 110)),
                new Wall(gorizontalWallTexture, new Vector2(Width / 2 + 320, Height / 2 + 110)),

                new Wall(verticalWallTexture, new Vector2(Width / 2 + 360, Height / 2 + 55)),
                new Wall(verticalWallTexture, new Vector2(Width / 2 + 360, Height / 2)),
            };

            enemies = new Enemies(enemyTexture, 5);

            sprites.Add(cake);
            sprites.AddRange(walls);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            hero.Update(gameTime, sprites, enemies.list);
            cake.Update(gameTime);
            foreach (var wall in walls)
                wall.Update(gameTime);
            enemies.Update(gameTime);

            base.Update(gameTime);
        }

        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            cake.Draw(_spriteBatch);
            foreach (var wall in walls)
                wall.Draw(_spriteBatch);
            enemies.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
