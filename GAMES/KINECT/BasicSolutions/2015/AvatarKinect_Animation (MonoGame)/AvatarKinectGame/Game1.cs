using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Kinect;

using AvatarKinectGame.GameComponents;
using AvatarKinectGame.TEST;

using aplikacja2__XNA_.BasicComponent;

namespace AvatarKinectGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // COMPONENTS VARIABLE 

        KinectSkinnedModelAnimation kinectAvatar;

        fpsCounter fps;

        private KeyboardState previousKeyboard;

        Label label1;


        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            this.graphics.PreparingDeviceSettings += this.GraphicsDevicePreparingDeviceSettings;

            Content.RootDirectory = "Content";

            this.Window.Title = "Avatar-Kinect [TEST]";

            // COMPONENTS
            this.kinectAvatar = new KinectSkinnedModelAnimation(this);
            this.Components.Add(this.kinectAvatar);

            fps = new fpsCounter();

            this.graphics.SynchronizeWithVerticalRetrace = false;
            this.graphics.ApplyChanges();
            //this.IsFixedTimeStep = false;

            label1 = new Label(this, new Vector2(5.0f, 5.0f));
            this.Components.Add(this.label1);
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), this.spriteBatch);

            //SetScreenMode();
        }

        protected override void UnloadContent()
        { }

        protected override void Update(GameTime gameTime)
        {
            fps.countFPS(gameTime.TotalGameTime.Seconds, 1);
            this.Window.Title = "Avatar-Kinect [TEST]" + "    -> FPS: " + fps.fps;

            label1.message = fps.fps.ToString();

            HandleInput();

            base.Update(gameTime);
        }

        public void HandleInput()
        {
            KeyboardState currentKeyboard = Keyboard.GetState();

            if (currentKeyboard.IsKeyDown(Keys.Escape))
            {
                if (!this.previousKeyboard.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
            }

            this.previousKeyboard = currentKeyboard;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            base.Draw(gameTime);
        }

        private void SetScreenMode()
        {
            this.graphics.PreferredBackBufferWidth = GraphicsDevice.Viewport.Width;
            this.graphics.PreferredBackBufferHeight = GraphicsDevice.Viewport.Height; //750

            this.graphics.IsFullScreen = false;
            this.graphics.ApplyChanges();
        }
        
        private void GraphicsDevicePreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
        }
    }
}
