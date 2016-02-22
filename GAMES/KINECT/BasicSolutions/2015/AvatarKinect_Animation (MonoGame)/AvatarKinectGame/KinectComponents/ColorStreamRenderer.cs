
namespace Kandou_v1
{
    using Microsoft.Kinect;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class ColorStreamRenderer : DrawableGameComponent
    {
        private RenderTarget2D backBuffer;

        private byte[] colorData;

        private Texture2D colorTexture;

        private Effect kinectColorVisualizer;

        private bool needToRedrawBackBuffer = true;
        private bool initialized;

        public Vector2 Position { get; set; }

        public Vector2 Size { get; set; }


        public ColorStreamRenderer(Game game)
            : base(game)
        {
            this.Size = new Vector2(160, 120);
            this.initialized = false;
        }

        public ColorStreamRenderer(Game game, Vector2 position, Vector2 size)
            : base(game)
        {
            //this.Size = new Vector2(160, 120);
            this.Size = new Vector2((float)size.X, (float)size.Y);
            this.Position = new Vector2((float)position.X, (float)position.Y);

            this.initialized = false;
        }

        public Kinect Chooser
        {
            get
            {
                return (Kinect)this.Game.Services.GetService(typeof(Kinect));
            }
        }

        public SpriteBatch SharedSpriteBatch
        {
            get
            {
                return (SpriteBatch)this.Game.Services.GetService(typeof(SpriteBatch));
            }
        }


        public override void Initialize()
        {
            base.Initialize();
            this.initialized = true;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (null == this.Chooser.Sensor ||
                false == this.Chooser.Sensor.IsRunning ||
                KinectStatus.Connected != this.Chooser.Sensor.Status)
            {
                return;
            }

            using (var frame = this.Chooser.Sensor.ColorStream.OpenNextFrame(0))
            {
                if (null == frame)
                {
                    return;
                }

                if (null == this.colorData || this.colorData.Length != frame.PixelDataLength)
                {
                    this.colorData = new byte[frame.PixelDataLength];

                    this.colorTexture = new Texture2D(
                        Game.GraphicsDevice,
                        frame.Width,
                        frame.Height,
                        false,
                        SurfaceFormat.Bgr32);
                   
                    this.backBuffer = new RenderTarget2D(
                        Game.GraphicsDevice,
                        frame.Width,
                        frame.Height,
                        false,
                        SurfaceFormat.Color,
                        DepthFormat.None,
                        this.Game.GraphicsDevice.PresentationParameters.MultiSampleCount,
                        RenderTargetUsage.PreserveContents);
                }

                frame.CopyPixelDataTo(this.colorData);
                this.needToRedrawBackBuffer = true;
            }
        }


        public override void Draw(GameTime gameTime)
        {
            if (null == this.colorTexture || null == this.SharedSpriteBatch)
            {
                return;
            }

            if (false == this.initialized)
            {
                this.Initialize();
            }

            if (this.needToRedrawBackBuffer)
            {
                Game.GraphicsDevice.SetRenderTarget(this.backBuffer);
                Game.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

                this.colorTexture.SetData<byte>(this.colorData);

                this.SharedSpriteBatch.Begin();
                //this.SharedSpriteBatch.Begin(SpriteSortMode.Texture, null, null, null, null, this.kinectColorVisualizer);
                this.SharedSpriteBatch.Draw(this.colorTexture, Vector2.Zero, Color.White);
                this.SharedSpriteBatch.End();

                this.Game.GraphicsDevice.SetRenderTarget(null);

                this.needToRedrawBackBuffer = false;
            }

            this.SharedSpriteBatch.Begin();
            this.SharedSpriteBatch.Draw(
                this.backBuffer,
                new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y),
                null,
                Color.White);
            this.SharedSpriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            this.kinectColorVisualizer = Game.Content.Load<Effect>("KinectComponentsItem/KinectColorVisualizer");
        }
    }
}
