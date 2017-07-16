using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MysticChronicles.Engine.Objects.Common;

using MysticChronicles.Engine.GameStates;
using MysticChronicles.Engine.Managers;

namespace MysticChronicles.Engine
{
    public class MainGame : Game
    {
        private SpriteBatch _spriteBatch;
        
        private BaseGameState _currentGameState;

        private GameStateContainer _gsContainer;

        public MainGame()
        {
            var graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        protected override void Initialize()
        {
            _gsContainer = new GameStateContainer
            {
                Window_Height = Window.ClientBounds.Height,
                Window_Width = Window.ClientBounds.Width,
                TManager = new TextureManager(Content)
            };

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            _currentGameState = new MainMenuState(_gsContainer);

            _currentGameState.OnRequestStateChange += CurrentGameState_OnRequestStateChange;
            _currentGameState.LoadContent();
        }

        private void CurrentGameState_OnRequestStateChange(object sender, BaseGameState e)
        {
            _currentGameState = e;

            _currentGameState.LoadContent();
        }
        
        protected override void Update(GameTime gameTime)
        {
            _currentGameState.HandleInput(GamePad.GetState(PlayerIndex.One), Keyboard.GetState());
            
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _currentGameState.Render(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}