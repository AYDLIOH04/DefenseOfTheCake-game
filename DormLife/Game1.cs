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

    private BaseState _currentState;

    private GameState _gameState;
    private MenuState _menuState;
    private PauseState _pauseState;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        Globals.Bounds = new(1920, 1024);
        _graphics.PreferredBackBufferWidth = Globals.Bounds.X;
        _graphics.PreferredBackBufferHeight = Globals.Bounds.Y;
        _graphics.ApplyChanges();
        Globals.Content = Content;

        _gameState = new GameState();
        _menuState = new MenuState();
        _pauseState = new PauseState();

        _currentState = _menuState;

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

        StateManager.Update(ref _currentState, _gameState, _menuState, _pauseState);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.RosyBrown);

        _spriteBatch.Begin();

        _currentState.Draw();

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}