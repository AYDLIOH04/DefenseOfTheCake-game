using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DormLife
{
    public class Enemy
    {
        private Texture2D enemyTexture;
        public Vector2 position;
        public Rectangle enemy;

        public Enemy(GraphicsDevice graphicsDevice, Vector2 pos)
        {
            enemyTexture = new Texture2D(graphicsDevice, 1, 1);
            enemyTexture.SetData(new[] { Color.White });

            position = pos;
            enemy = new Rectangle((int)position.X, (int)position.Y, 50, 50);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyTexture, enemy, Color.Red);
        }

        public bool CheckCollision(Rectangle heroRectangle)
        {
            return enemy.Intersects(heroRectangle);
        }
    }

    public class Cake
    {
        private Texture2D cakeTexture;
        public Vector2 position;
        public Rectangle cake;
        public int HP;

        public Cake(GraphicsDevice graphicsDevice, Vector2 pos)
        {
            cakeTexture = new Texture2D(graphicsDevice, 1, 1);
            cakeTexture.SetData(new[] { Color.White });

            position = pos;
            cake = new Rectangle((int)position.X, (int)position.Y, 50, 50);
            HP = 1000;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cakeTexture, cake, Color.Green);
        }

        public bool CheckCollision(Rectangle heroRectangle)
        {
            return cake.Intersects(heroRectangle);
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                // Game1.GameOver();
            }
        }
    }

    public class MainHero
    {
        private Texture2D heroTexture;
        private Rectangle hero;

        private int ScreenWidth;
        private int ScreenHeight;

        public MainHero(GraphicsDevice graphicsDevice, int x, int y, int screenWidth, int screenHeight)
        {
            heroTexture = new Texture2D(graphicsDevice, 1, 1);
            heroTexture.SetData(new[] { Color.White });

            hero = new Rectangle(x, y, 50, 50);

            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, hero, Color.Blue);
        }

        public void Move(Keys[] keys, Cake cake)
        {
            int deltaX = 0;
            int deltaY = 0;

            if (keys.Contains(Keys.Left) || keys.Contains(Keys.A))
                deltaX -= 5;
            if (keys.Contains(Keys.Right) || keys.Contains(Keys.D))
                deltaX += 5;
            if (keys.Contains(Keys.Up) || keys.Contains(Keys.W))
                deltaY -= 5;
            if (keys.Contains(Keys.Down) || keys.Contains(Keys.S))
                deltaY += 5;

            Rectangle newHeroRect = new Rectangle(hero.X + deltaX, hero.Y + deltaY, hero.Width, hero.Height);

            if (newHeroRect.Intersects(cake.cake))
            {
                if (deltaX < 0)
                    deltaX = cake.cake.Right - hero.X;
                if (deltaX > 0)
                    deltaX = cake.cake.X - hero.Width - hero.X;
                if (deltaY < 0)
                    deltaY = cake.cake.Bottom - hero.Y;
                if (deltaY > 0)
                    deltaY = cake.cake.Y - hero.Height - hero.Y;
            }

            if (hero.X + deltaX >= 0 && hero.X + deltaX + hero.Width <= ScreenWidth)
                hero.X += deltaX;
            if (hero.Y + deltaY >= 0 && hero.Y + deltaY + hero.Height <= ScreenHeight)
                hero.Y += deltaY;
        }
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MainHero hero;
        private Cake cake;

        public List<Enemy> enemies = new List<Enemy>();
        private Random random = new Random();
        float enemyTimer = 0;
        float enemySpawnTime = 5f; 


        public void GenerateEnemy(GraphicsDevice graphicsDevice)
        {
            int xPos = random.Next(0, graphicsDevice.Viewport.Width - 50);
            int yPos = random.Next(0, graphicsDevice.Viewport.Height - 50);
            enemies.Add(new Enemy(graphicsDevice, new Vector2(xPos, yPos)));
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            hero = new MainHero(GraphicsDevice, 300, 300, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            cake = new Cake(GraphicsDevice, new Vector2(GraphicsDevice.Viewport.Width / 2 - 25, GraphicsDevice.Viewport.Height / 2 - 25));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState state = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            Keys[] keys = state.GetPressedKeys();

            hero.Move(keys, cake);

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Rectangle mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
                for (int i = enemies.Count - 1; i >= 0; i--)
                {
                    if (enemies[i].CheckCollision(mouseRectangle))
                    {
                        enemies.RemoveAt(i);
                    }
                }
            }

            enemyTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (enemyTimer >= enemySpawnTime && enemies.Count < 5)
            {
                Vector2 enemyPosition = new Vector2(random.Next(0, _graphics.PreferredBackBufferWidth), random.Next(0, _graphics.PreferredBackBufferHeight));
                Enemy enemy = new Enemy(GraphicsDevice, enemyPosition);
                enemies.Add(enemy);

                enemyTimer = 0;
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            cake.Draw(_spriteBatch);
            foreach (Enemy enemy in enemies)
                enemy.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
