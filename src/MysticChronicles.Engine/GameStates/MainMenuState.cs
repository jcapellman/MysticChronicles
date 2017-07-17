﻿using System.Linq;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using MysticChronicles.Engine.GameObjects.GUI;
using MysticChronicles.Engine.Objects.Common;

namespace MysticChronicles.Engine.GameStates
{
    public class MainMenuState : BaseGameState
    {
        public MainMenuState(GameStateContainer container) : base(container) { }

        public override void HandleInput(GamePadState gamePadState, KeyboardState keyboardState, TouchCollection tocuCollection)
        {
            if (!keyboardState.GetPressedKeys().Any() && tocuCollection.All(a => a.State != TouchLocationState.Pressed))
            {
                return;
            }
            
            RequestStateChange(new InBattleState(GsContainer));
        }

        public override void LoadContent()
        {
            var mObject = new MenuObject(EContainer);

            var content = mObject.LoadContent();

            AddGraphicElementRange(content.Item1);
            AddTextElementRange(content.Item2);
        }
    }
}