using DormLife.State;
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
        public static void Update()
        {
            Game1.currentState.Update();

            CheckGameOver();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && Game1.currentState is MenuState)
            {
                CreateGame();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Escape) && Game1.currentState is GameState)
            {
                SetPause();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && Game1.currentState is PauseState)
            {
                BackToGame();
            }
        }

        public static void CreateGame(object sender = null, EventArgs e = null)
        {
            Game1.gameState.NewGame();
            Game1.currentState = Game1.gameState;
        }

        public static void BackToGame(object sender = null, EventArgs e = null)
        {
            Game1.currentState = Game1.gameState;
        }

        public static void SetPause(object sender = null, EventArgs e = null)
        {
            Game1.currentState = Game1.pauseState;
        }

        public static void GoToMenu(object sender = null, EventArgs e = null)
        {
            Game1.currentState = Game1.menuState;
        }


        public static void CheckGameOver()
        {
            if (Game1.currentState is GameState)
            {
                if ((Game1.currentState as GameState).cake.HP <= 0)
                {
                    Game1.currentState = Game1.gameOverState;
                }
            }
        }
    }
}
