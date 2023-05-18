using DormLife;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DormLife.GameObjects;
using System.Collections.Generic;
using DormLife.State;
using DormLife.Managers;

namespace DormLife;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public static BaseState currentState;

    public static GameState gameState;
    public static MenuState menuState;
    public static PauseState pauseState;
    public static GameOverState gameOverState;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        Globals.Bounds = new(1500, 900);
        _graphics.PreferredBackBufferWidth = Globals.Bounds.X;
        _graphics.PreferredBackBufferHeight = Globals.Bounds.Y;
        _graphics.ApplyChanges();
        Globals.Content = Content;

        gameState = new GameState();
        menuState = new MenuState();
        pauseState = new PauseState();
        gameOverState = new GameOverState();

        currentState = menuState;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.SpriteBatch = _spriteBatch;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            Exit();
        Globals.Update(gameTime);

        StateManager.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.RosyBrown);

        _spriteBatch.Begin();
        currentState.Draw(GraphicsDevice);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}