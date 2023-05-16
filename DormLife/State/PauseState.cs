﻿using DormLife.Components.MenuState;
using DormLife.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.State
{
    public class PauseState : BaseState
    {
        private Button _continueGameButton;
        private Button _menuButton;

        public PauseState()
        {
            _continueGameButton = new Button("Continue game", new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2 + 30));
            _menuButton = new Button("Menu", new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2 - 50));

            _continueGameButton.Clicked += StateManager.BackToGame;
            _menuButton.Clicked += StateManager.GoToMenu;

        }
        public override void Update()
        {
            _continueGameButton.Update();
            _menuButton.Update();
        }

        public override void Draw()
        {
            _continueGameButton.Draw();
            _menuButton.Draw();
        }
    }
}
