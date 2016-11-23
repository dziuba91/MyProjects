using Microsoft.Kinect;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KinectWspolbiezny
{
	/// <summary>
	/// This class renders the current depth stream frame.
	/// </summary>
	public class DepthStreamRenderer : DrawableGameComponent
	{
		/// <summary>
		/// The back buffer where the depth frame is scaled as requested by the Size.
		/// </summary>
		public RenderTarget2D backBuffer;

		/// <summary>
		/// The last frame of depth data.
		/// </summary>
		public short[] depthData= null;

		/// <summary>
		/// The depth frame as a texture.
		/// </summary>
		public Texture2D depthTexture;

		/// <summary>
		/// This Xna effect is used to convert the depth to RGB color information.
		/// </summary>
		private Effect kinectDepthVisualizer;

		/// <summary>
		/// Whether or not the back buffer needs updating.
		/// </summary>
		public bool needToRedrawBackBuffer = true;

		/// <summary>
		/// Whether the rendering has been initialized.
		/// </summary>
		private bool initialized;

		public Vector2 Size { get; set; }
		public Vector2 Position { get; set; }

		/// <summary>
		/// Initializes a new instance of the DepthStreamRenderer class.
		/// </summary>
		/// <param name="game">The related game object.</param>
		public DepthStreamRenderer(Game game)
			: base(game)
		{
			this.Size = new Vector2(160, 120);

			this.initialized = false;
		}

		/// <summary>
		/// Gets the SpriteBatch from the services.
		/// </summary>
		public SpriteBatch SharedSpriteBatch
		{
			get
			{
				return (SpriteBatch)this.Game.Services.GetService(typeof(SpriteBatch));
			}
		}
		
		public Kinect Chooser
		{
			get
			{
				return (Kinect)this.Game.Services.GetService(typeof(Kinect));
			}
		}

		/// <summary>
		/// Initializes the necessary children.
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();

			this.initialized = true;
		}

		/// <summary>
		/// This method renders the color and skeleton frame.
		/// </summary>
		/// <param name="gameTime">The elapsed game time.</param>
		public override void Draw(GameTime gameTime)
		{
			// Return if we don't have a depth target or valid SpriteBatch
			if (null == this.depthTexture || null == this.SharedSpriteBatch)
			{
				return;
			}

			if (false == this.initialized)
			{
				this.Initialize();
			}

			if (this.needToRedrawBackBuffer)
			{
				// Set the backbuffer and clear
				Game.GraphicsDevice.SetRenderTarget(this.backBuffer);
				Game.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

				this.depthTexture.SetData<short>(this.depthData);

				// Draw the depth image
				this.SharedSpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, this.kinectDepthVisualizer);
				this.SharedSpriteBatch.Draw(this.depthTexture, Vector2.Zero, Color.White);
				this.SharedSpriteBatch.End();
				
				// Reset the render target and prepare to draw scaled image
				this.Game.GraphicsDevice.SetRenderTarget(null);

				// No need to re-render the back buffer until we get new data
				this.needToRedrawBackBuffer = false;
			}

			// Draw scaled image
			this.SharedSpriteBatch.Begin();
			this.SharedSpriteBatch.Draw(
				this.backBuffer,
				new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y),
				null,
				Color.White);
			this.SharedSpriteBatch.End();

			base.Draw(gameTime);
		}

		/// <summary>
		/// This method loads the Xna effect.
		/// </summary>
		protected override void LoadContent()
		{
			base.LoadContent();

			// This effect is used to convert depth data to color for display
			this.kinectDepthVisualizer = Game.Content.Load<Effect>("KinectDepthVisualizer");
		}
	}
}