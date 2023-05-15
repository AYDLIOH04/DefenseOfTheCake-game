using DormLife.State;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Managers
{
    public static class StateManager
    {
        public static void Update(ref BaseState currentState,
                                  GameState gameState,
                                  MenuState menuState,
                                  PauseState pauseState)
        {
            currentState.Update();

            CheckGameOver(ref currentState, menuState);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && currentState is MenuState)
            {
                gameState.NewGame();
                currentState = gameState;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Escape) && currentState is GameState)
            {
                currentState = pauseState;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                currentState = gameState;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                currentState = menuState;
            }
        }

        public static void CheckGameOver(ref BaseState currentState, MenuState menuState)
        {
            if (currentState is GameState)
            {
                if ((currentState as GameState)._cake.HP <= 0)
                {
                    currentState = menuState;
                }
            }
        }
    }
}
