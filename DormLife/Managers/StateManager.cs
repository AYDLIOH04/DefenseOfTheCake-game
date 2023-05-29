using DormLife.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Managers
{
    public static class StateManager
    {
        private static bool isEscapePressed = false;
        private static bool isTabPressed = false;

        public static void Update()
        {
            Game1.currentState.Update();

            KeyboardState keyboardState = Keyboard.GetState();
            CheckGameOver();

            if (keyboardState.IsKeyDown(Keys.Space) && Game1.currentState is MenuState)
            {
                CreateGame();
            }
            else if (keyboardState.IsKeyDown(Keys.Space) && Game1.currentState is PauseState)
            {
                BackToGame();
            }

            if (keyboardState.IsKeyDown(Keys.Tab))
            {
                if (!isTabPressed)
                {
                    isTabPressed = true;

                    if (Game1.currentState is GameState) GoToShop();
                    else if (Game1.currentState is ShopState) BackToGame();
                }
            }
            else isTabPressed = false;

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                if (!isEscapePressed)
                {
                    isEscapePressed = true;

                    if (Game1.currentState is GameState) SetPause();
                    else if (Game1.currentState is PauseState) BackToGame();
                }
            }
            else isEscapePressed = false;
        }
    

        #region State Button Methods
        public static void CreateGame(object sender = null, EventArgs e = null)
        {
            SoundManager.PlayGameBackgroundMusic();
            Game1.gameState.NewGame();
            Game1.currentState = Game1.gameState;
        }

        public static void BackToGame(object sender = null, EventArgs e = null)
        {
            SoundManager.ResumeGameMusic();
            Game1.currentState = Game1.gameState;
        }

        public static void SetPause(object sender = null, EventArgs e = null)
        {
            SoundManager.PauseGameMusic();
            SoundManager.PlayBasedBackgroundMusic();
            Game1.currentState = Game1.pauseState;
        }

        public static void GoToMenu(object sender = null, EventArgs e = null)
        {
            SoundManager.PlayBasedBackgroundMusic();
            Game1.currentState = Game1.menuState;       
        }
        public static void GoToShop(object sender = null, EventArgs e = null)
        {
            SoundManager.PauseGameMusic();
            SoundManager.PlayShopBackgroundMusic();
            Game1.currentState = Game1.shopState;
        }

        public static void ExitGame(object sender = null, EventArgs e = null)
        {
            Game1.GameRef.Exit();
        }
        #endregion


        public static void CheckGameOver()
        {
            if (Game1.currentState is GameState)
            {
                if (GameState.gameManager.cake.HP <= 0)
                {
                    Game1.currentState = Game1.gameOverState;
                    SoundManager.PlayBasedBackgroundMusic();
                    SoundManager.PlaySoundEffect("gameover");
                }
            }
        }
    }
}
