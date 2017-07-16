﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MysticChronicles.Engine.Objects.Common;
using MysticChronicles.Engine.Managers;
using MysticChronicles.Engine.Objects.Element;

namespace MysticChronicles.Engine.GameStates
{
    public abstract class BaseGameState
    {
        protected TextureManager textureManager;
        protected int width, height;
        protected List<BaseGraphicElement> graphicElements;

        #region State Change Event
        public event EventHandler<BaseGameState> OnRequestStateChange;

        public void RequestStateChange(BaseGameState gameState)
        {
            var handler = OnRequestStateChange;

            handler?.Invoke(null, gameState);
        }
        #endregion

        protected GameStateContainer GSContainer => new GameStateContainer
        {
                Window_Height = height,
                Window_Width = width,
                TManager = textureManager
        };

        protected BaseGameState(GameStateContainer container)
        {
            textureManager = container.TManager;
            graphicElements = new List<BaseGraphicElement>();

            width = container.Window_Width;
            height = container.Window_Height;
        }

        public ElementContainer EContainer => new ElementContainer
        {
            Window_Width = width,
            Window_Height = height,
            TextureManager = textureManager
        };
        
        protected void AddGraphicElement(BaseGraphicElement element)
        {
            graphicElements.Add(element);
        }

        public abstract void HandleInput(GamePadState gamePadState, KeyboardState keyboardState, TouchCollection touchCollection);
        
        public abstract void LoadContent();

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var element in graphicElements)
            {
                element.Render(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}