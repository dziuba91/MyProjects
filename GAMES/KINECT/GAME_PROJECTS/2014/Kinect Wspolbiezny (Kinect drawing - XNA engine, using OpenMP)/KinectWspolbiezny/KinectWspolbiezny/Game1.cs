using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.Interaction;
using System.Runtime.InteropServices;

namespace KinectWspolbiezny
{
    //public delegate Vector2 SkeletonPointMap(SkeletonPoint point);

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Texture2D blank;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Skeleton rawSkeleton;

        //private readonly SkeletonPointMap mapMethod;

        int a, b;
        int sum1, sum2;
        int b1, b2;

        float odleglosc = 0;
        int iloscJoint = 0;

        private bool kinectDetected;
        private bool skeletonDetected;

        SpriteFont font;

        private KeyboardState previousKeyboard;

        private KeyboardState currentKeyboard;

        private KinectStatusCheck statusCheck;

        private DepthStreamRenderer depthComponent;

        private Plane plane;

        //private KanjiPanel kanji_panel;
        
        private readonly Kinect chooser;

        private bool info1 = false;
        private bool info2 = false;
        private bool info3 = false;
        private bool handRecognise = false;

        private const int WindowedWidth = 800;

        private bool drawDepth = false;


        [DllImport("obliczenia.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Add(bool a);

        [DllImport("obliczenia.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Balabala();

        public Game1()
        {
            //this.IsFixedTimeStep = false;
            this.IsMouseVisible = true;

            graphics = new GraphicsDeviceManager(this);
            //this.graphics.SynchronizeWithVerticalRetrace = true;
            //this.graphics.PreferredBackBufferWidth = WindowedWidth;
            //this.graphics.PreferredBackBufferHeight = ((WindowedWidth / 4) * 3) + 110;
            //this.graphics.IsFullScreen = false;
            this.graphics.PreparingDeviceSettings += this.GraphicsDevicePreparingDeviceSettings;

            Content.RootDirectory = "Content";

            this.chooser = new Kinect(this, ColorImageFormat.RgbResolution640x480Fps30, DepthImageFormat.Resolution640x480Fps30);
            this.Services.AddService(typeof(Kinect), this.chooser);

            this.Components.Add(this.chooser);

            this.statusCheck = new KinectStatusCheck(this);
            this.Components.Add(this.statusCheck);

            this.depthComponent = new DepthStreamRenderer(this);
            //this.Components.Add(this.depthComponent);

            this.plane = new Plane(this);
            this.Components.Add(this.plane);

            UpdateStreamSizeAndLocation();

            // Obliczenia
            a = 7;
            b = 3;

            sum1 = Add(true);
            b1 = Balabala();

            sum2 = Add(false);
            b2 = Balabala();

            //a = kinus();
        }

        protected override void Initialize()
        {
            font = Content.Load<SpriteFont>("SpriteFont1");

            blank = new Texture2D(GraphicsDevice, 1, 1);
            blank.SetData(new[] { Color.White });

            base.Initialize();
        }

        public Kinect Chooser
        {
            get
            {
                return (Kinect)this.Services.GetService(typeof(Kinect));
            }
        }

        private static Skeleton[] SkeletonData { get; set; }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), this.spriteBatch);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void UpdateStreamSizeAndLocation()
        {
            int depthStreamWidth = this.graphics.PreferredBackBufferWidth / 4;
            Vector2 size = new Vector2(depthStreamWidth + 50, (depthStreamWidth / 4) * 3);
            Vector2 pos = new Vector2(this.graphics.PreferredBackBufferWidth - depthStreamWidth - 60, 10);

            if (null != this.depthComponent)
            {
                this.depthComponent.Size = size;
                this.depthComponent.Position = pos;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            drawDepth = false;

            this.previousKeyboard = this.currentKeyboard;

            this.HandleInput();

            if (null == this.chooser || null == this.Chooser.Sensor || false == this.Chooser.Sensor.IsRunning || this.Chooser.Sensor.Status != KinectStatus.Connected || this.Chooser.intStream == null)
            {
                kinectDetected = false;
                return;
                // sprawdzanie czy komunikacja z sensorem zachodzi, jeżeli nie - omiń późniejsze operacje pobierania danych typu "Skeleton"
            }
            else kinectDetected = true;

            bool newFrame = false;

            using (var skeletonFrame = this.Chooser.Sensor.SkeletonStream.OpenNextFrame(0))
            {
                // pobierz kolejną "ramkę" danych dotyczących interfejsu SkeletalTracking
                if (null != skeletonFrame)
                {
                    // instrukcje zawarte w tych klamrach zostaną wykonane, jeżeli "ramka" danych została poprawnie pobrana
                    newFrame = true;

                    if (null == SkeletonData || SkeletonData.Length != skeletonFrame.SkeletonArrayLength)
                    {
                        SkeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];
                        // otwórz nową tablicę obiektów typu "Skeleton" - tyle obiektów znajdzie się w tablicy ile osób zostało wychwyconych przez urządzenie
                    }

                    skeletonFrame.CopySkeletonDataTo(SkeletonData);
                    // przekopiuj dane z "ramki" do utworzonej nowej tablicy obiektów typu "Skeleton"

                    rawSkeleton =
                        (from s in SkeletonData
                         where s != null && s.TrackingState == SkeletonTrackingState.Tracked
                         select s).FirstOrDefault();
                    // ustaw do obiektu rawSkeleton tylko 1, wybrany "szkielet" z pośród wszystkich dostępnych podzczas wychwycania osób przez sensor -> wybierz pierwszy z brzegu lub "domyślny" szkielet
                    // ważne w sytuacji gdy apliakcja wymaga tylko jednej danej typu "Skeleton" (dane tylko o 1 użytkowniku stojącego przed sensorem)

                    // ...
                    if (null != rawSkeleton)
                    {
                        this.skeletonDetected = true;
                        kinectInfo(rawSkeleton);

                        ProcessDepthFrame();
                        drawDepth = true;

                        var accelerometerReading = this.Chooser.Sensor.AccelerometerGetCurrentReading();
                        this.Chooser.intStream.ProcessSkeleton(SkeletonData, accelerometerReading, skeletonFrame.Timestamp);
                        //
                        //InteractionFrame iFrame = this.Chooser.intStream.OpenNextFrame(0);
                        //handRecognise = true;
                        using (var iFrame = this.Chooser.intStream.OpenNextFrame(0))
                        {
                            if (iFrame != null)
                            {
                                UserInfo[] curUsers = new UserInfo[InteractionFrame.UserInfoArrayLength];

                                iFrame.CopyInteractionDataTo(curUsers);

                                if (curUsers != null)
                                {
                                    for (int i = 0; i < InteractionFrame.UserInfoArrayLength; i++)
                                    {
                                        UserInfo curUser = curUsers[i];

                                        foreach (var handPointer in curUser.HandPointers)
                                        {
                                            if (handPointer.HandEventType == InteractionHandEventType.Grip && handPointer.HandType == InteractionHandType.Right)
                                            {
                                                info1 = true;
                                                //break;
                                            }

                                            //else info1 = false;
                                            if (handPointer.HandEventType == InteractionHandEventType.GripRelease && handPointer.HandType == InteractionHandType.Right)
                                            {
                                                info3 = true;
                                                //break;
                                            }

                                            if (handPointer.IsInteractive == true && handPointer.HandType == InteractionHandType.Left && rawSkeleton.Joints[JointType.HandLeft].Position.Y > rawSkeleton.Joints[JointType.ShoulderCenter].Position.Y && rawSkeleton.Joints[JointType.HipCenter].Position.Z > rawSkeleton.Joints[JointType.HandLeft].Position.Z + 0.4)
                                            {
                                                plane.clear();
                                            }

                                            if (handPointer.IsInteractive == true && rawSkeleton.Joints[JointType.HipCenter].Position.Z > rawSkeleton.Joints[JointType.HandRight].Position.Z + 0.45)
                                            {
                                                plane.realize();
                                            }
                                            //else info2 = false;*/
                                        }
                                        
                                    }
                                    if (info1) { info1 = false; info2 = true; }
                                    if (info3) { info3 = false; info2 = false; }
                                    //else { info2 = false; }
                                    //if (!info1) { info1 = false; }
                                    //if (!info2) { info1 = false; }
                                }

                                handRecognise = true;
                            }
                            else handRecognise = false;
                        }
                    }
                    else
                    {
                        this.skeletonDetected = false;
                        odleglosc = 0;
                        iloscJoint = 0;
                    }
                }
            }

            if (newFrame)
            {
                //
                // w tym miejscu mozna wykonać odpowiednie operacje opierając się na wiedzy, że "ramka" danych dotyczących "NUI Skeletal Tracking" została poprawnie pobrana
                //
                /*
                if (null != this.depthComponent)
                {
                    this.depthComponent.Update(gameTime);
                }*/
            }

            if (drawDepth == false) ProcessDepthFrame();
            base.Update(gameTime);
        }

        void ProcessDepthFrame()
        {
            using (DepthImageFrame dif = this.Chooser.Sensor.DepthStream.OpenNextFrame(0))
            {
                if (dif != null && Chooser != null && Chooser.intStream != null)
                {
                    //DepthImagePixel[] data = new DepthImagePixel[dif.PixelDataLength];
                    //dif.CopyDepthImagePixelDataTo(data);
                    //Feed depth data to interactionStream
                    //try
                    //{
                    this.Chooser.intStream.ProcessDepth(dif.GetRawPixelData(), dif.Timestamp);
                    //}
                    //catch { }
                }

                if (Chooser != null && dif != null)
                {
                    if (null == this.depthComponent.depthData)
                    {
                        this.depthComponent.depthData = new short[dif.PixelDataLength];

                        this.depthComponent.depthTexture = new Texture2D(
                            this.GraphicsDevice,
                            dif.Width,
                            dif.Height,
                            false,
                            SurfaceFormat.Bgra4444);

                        this.depthComponent.backBuffer = new RenderTarget2D(
                            this.GraphicsDevice,
                            dif.Width,
                            dif.Height,
                            false,
                            SurfaceFormat.Color,
                            DepthFormat.None,
                            this.GraphicsDevice.PresentationParameters.MultiSampleCount,
                            RenderTargetUsage.PreserveContents);
                    }

                    dif.CopyPixelDataTo(this.depthComponent.depthData);
                    this.depthComponent.needToRedrawBackBuffer = true;
                }
            }
        }

        private void HandleInput()
        {
            this.currentKeyboard = Keyboard.GetState();

            if (this.currentKeyboard.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            this.previousKeyboard = this.currentKeyboard;
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.Silver);

            if (null != this.depthComponent)
            {
                this.depthComponent.Draw(gameTime);
            }

            spriteBatch.Begin();

            spriteBatch.DrawString(font, "TEST OpenMP:", new Vector2(20, 20), Color.Black);

            spriteBatch.DrawString(font, "T1= " + sum1.ToString(), new Vector2(20, 40), Color.Black);
            spriteBatch.DrawString(font, "T2 (OMP)= " + sum2.ToString(), new Vector2(20, 60), Color.Black);

            spriteBatch.DrawString(font, "b1= " + b1.ToString(), new Vector2(200, 40), Color.Black);
            spriteBatch.DrawString(font, "b2= " + b2.ToString(), new Vector2(200, 60), Color.Black);

            spriteBatch.DrawString(font, "Odległość Użytkownika od sensora= " + odleglosc.ToString(), new Vector2(20, 110), Color.Black);
            spriteBatch.DrawString(font, "Ilosc widocznych części ciała= [" + iloscJoint.ToString() + "/20]", new Vector2(20, 130), Color.Black);
            //spriteBatch.DrawString(font, "Kinect Funkcja= " + a.ToString(), new Vector2(20, 150), Color.Black);

            spriteBatch.DrawString(font, "RĘKA= " + handRecognise.ToString(), new Vector2(20, 160), Color.Black);
            spriteBatch.DrawString(font, "RĘKA STAN 1= " + info1.ToString(), new Vector2(20, 180), Color.Black);
            spriteBatch.DrawString(font, "RĘKA STAN 2= " + info2.ToString(), new Vector2(20, 200), Color.Black);

            //if (this.Chooser.intStream== null)
            //    spriteBatch.DrawString(font, "BŁĄD", new Vector2(20, 250), Color.Black);

            /*
            if (null != this.rawSkeleton && rawSkeleton.Joints[JointType.HandRight].TrackingState==JointTrackingState.Tracked)
                spriteBatch.Draw(blank, new Rectangle((int)this.SkeletonPointToScreen(rawSkeleton.Joints[JointType.HandRight].Position).X, (int)this.SkeletonPointToScreen(rawSkeleton.Joints[JointType.HandRight].Position).Y, 5, 5), Color.Black);
            */
            spriteBatch.End();
            
            statusCheck.Draw(gameTime,kinectDetected,skeletonDetected);


            bool check = false;
            Vector2 p1 = Vector2.Zero;
            Vector2 p2 = Vector2.Zero;

            if (null != this.rawSkeleton && rawSkeleton.Joints[JointType.HandRight].TrackingState == JointTrackingState.Tracked)
            {
                p1.X = SkeletonPointToScreen(rawSkeleton.Joints[JointType.HandRight].Position).X;
                p1.Y = SkeletonPointToScreen(rawSkeleton.Joints[JointType.HandRight].Position).Y;
                p2.X = rawSkeleton.Joints[JointType.HandRight].Position.X;
                p2.Y = rawSkeleton.Joints[JointType.HandRight].Position.Y;
                check = true;
            }
            
            plane.Draw(gameTime, p1, p2, check, info2);
            //kanji_panel.Draw(gameTime);

            base.Draw(gameTime);
        }


        private void kinectInfo(Skeleton skeleton)
        {
            int i = 0;

            if (skeleton.Joints[JointType.ShoulderRight].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.AnkleLeft].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.AnkleRight].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.ElbowLeft].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.ElbowRight].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.FootLeft].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.FootRight].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.HandLeft].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.HandRight].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.Head].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.HipCenter].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.HipLeft].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.HipRight].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.KneeLeft].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.KneeRight].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.ShoulderCenter].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.ShoulderLeft].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.Spine].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.WristLeft].TrackingState == JointTrackingState.Tracked) i++;
            if (skeleton.Joints[JointType.WristRight].TrackingState == JointTrackingState.Tracked) i++;
            
            odleglosc = skeleton.Joints[JointType.HipCenter].Position.Z;
            iloscJoint = i;
        }

        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            DepthImagePoint depthPoint = this.Chooser.Sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution320x240Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }

        private Vector2 SkeletonToColorMap(SkeletonPoint point)
        {
            if ((null != Chooser.Sensor) && (null != Chooser.Sensor.ColorStream))
            {
                // This is used to map a skeleton point to the color image location
                var colorPt = Chooser.Sensor.CoordinateMapper.MapSkeletonPointToColorPoint(point, Chooser.Sensor.ColorStream.Format);
                return new Vector2(colorPt.X, colorPt.Y);
            }

            return Vector2.Zero;
        }

        private void GraphicsDevicePreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
        }
    }
}
