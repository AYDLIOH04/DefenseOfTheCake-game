using DormLife;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DormLife.GameObjects;
using System.Collections.Generic;
using DormLife.State;
using DormLife.Managers;
using System;

namespace DormLife;

public class Game1 : Game
{
    private static Game1 _gameRef; 
    public static Game1 GameRef => _gameRef;

    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;


    #region States
    public static BaseState currentState;

    public static GameState gameState;
    public static MenuState menuState;
    public static PauseState pauseState;
    public static GameOverState gameOverState;
    public static ShopState shopState;
    #endregion

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.IsFullScreen = true;
        
        _gameRef = this;
    }

    protected override void Initialize()
    {
        Globals.Bounds = new(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        //Globals.Bounds = new(1920, 980);
        _graphics.PreferredBackBufferWidth = Globals.Bounds.X;
        _graphics.PreferredBackBufferHeight = Globals.Bounds.Y;
        _graphics.ApplyChanges();
        Globals.Content = Content;
        Globals.Graphics = GraphicsDevice;

        gameState = new GameState();
        menuState = new MenuState();
        pauseState = new PauseState();
        gameOverState = new GameOverState();
        shopState = new ShopState();

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
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) Exit();

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