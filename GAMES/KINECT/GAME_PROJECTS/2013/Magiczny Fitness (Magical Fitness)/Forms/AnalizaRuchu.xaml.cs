using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Kinect;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for AnalizaRuchu.xaml
    /// </summary>
    public partial class AnalizaRuchu : Window
    {
        MAGICZNE_ZAKLECIE magia = null;
        TRYB_KONTROLI_RUCHOW tryb;
        CWICZENIE cw;
        MUZYKA muz;

        KinectSensor kinectSensor = null;
        Thread czas_t = null;
        Thread t1 = null;

        private bool watek = false;
        private bool watek_czas = false;
        //private bool watek_active = false;
        private bool kinect_active = false;
        private bool media_active = false;
        private bool wlacz_kinect = false;

        private int czas_calkowity;
        private int czas;
        private int wynik = 0;


        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage imageSource;

        byte[] pixelData;

        //        WriteableBitmap BImage;
        BitmapSource l;

        PixelFormat pf = PixelFormats.Bgr32;

        private const double JointThickness = 14;
        private DrawingGroup drawingGroup;

        private const float RenderWidth = 640.0f;

        private const float RenderHeight = 480.0f;
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 5);


        public AnalizaRuchu()
        {
            InitializeComponent();
        }

        public void Wlacz(TRYB_KONTROLI_RUCHOW t, bool wlacz_kinect, MAGICZNE_ZAKLECIE magia)
        {
            this.tryb = t;
            this.wlacz_kinect = wlacz_kinect;

            this.magia = magia;
            if (magia != null)
            {
                magia.efekt_zaklecia(canvas1);
            }

            muz = new MUZYKA();
            muz.lista_odtwarzania(mediaElement1);

            //czas_t = new Thread(czas_treningu);
            //czas_t.Start();
            Point[] p = { new Point(0, 0) };
            tryb.analiza_cwiczenia(progressBar2, p);
            czas_calkowity = tryb.czas_treningu;

            czas_t = new Thread(czas_treningu);
            czas_t.Start();

            media_active = true;
            watek_czas = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (wlacz_kinect)
            {
                this.drawingGroup = new DrawingGroup();
                // Create an image source that we can use in our image control
                this.imageSource = new DrawingImage(this.drawingGroup);

                // Display the drawing using our image control
                Image.Source = this.imageSource;

                foreach (var potentialSensor in KinectSensor.KinectSensors)
                {
                    if (potentialSensor.Status == KinectStatus.Connected)
                    {
                        this.kinectSensor = potentialSensor;
                        break;
                    }
                }

                var parameters = new TransformSmoothParameters
                {
                    Smoothing = 0.75f,
                    Correction = 0.0f,
                    Prediction = 0.0f,
                    JitterRadius = 0.05f,
                    MaxDeviationRadius = 0.04f
                };

                //Inicjalizacja trybu Skeletal tracking
                kinectSensor.SkeletonStream.Enable(parameters);
                //kinectSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

                //Dodanie zdarzenia, przechwytujacego dane
                kinectSensor.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(nui_SkeletonFrameReady);
                //kinectSensor.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(ColorFrameReady);

                kinectSensor.Start();
                //this.kinectSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
            }
        }

        void nui_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];

            bool receivedData = false;

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    //    if (skeletons == null) //allocate the first time
                    //     {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                    //     }

                    receivedData = true;
                }
                else
                {

                    // apps processing of skeleton data took too long; it got more than 2 frames behind.

                    // the data is no longer avabilable.

                }
            }

            if (receivedData)
            {

                if (skeletons.Length != 0)
                {
                    foreach (Skeleton skel in skeletons)
                    {
                        if (skel.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            //SetEllipsePosition(headEllipse, skel);
                            DrawPoint(skel);
                        }
                        else if (skel.TrackingState == SkeletonTrackingState.PositionOnly)
                        {
                            //SetEllipsePosition(headEllipse, skel);
                            DrawPoint(skel);
                        }
                    }
                }
            }
        }

        private void DrawPoint(Skeleton szkielet)
        {
            kinect_active = true;

            using (DrawingContext dc = this.drawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));

                this.DrawBone(szkielet, dc, JointType.ShoulderCenter, JointType.ElbowLeft);
                this.DrawBone(szkielet, dc, JointType.ShoulderCenter, JointType.ElbowRight);
                this.DrawBone(szkielet, dc, JointType.ElbowLeft, JointType.HandLeft);
                this.DrawBone(szkielet, dc, JointType.ElbowRight, JointType.HandRight);
                this.DrawBone(szkielet, dc, JointType.HipCenter, JointType.FootLeft);
                this.DrawBone(szkielet, dc, JointType.HipCenter, JointType.FootRight);
                this.DrawBone(szkielet, dc, JointType.ShoulderCenter, JointType.Head);
                this.DrawBone(szkielet, dc, JointType.ShoulderCenter, JointType.HipCenter);


                dc.DrawEllipse(Brushes.Red, null, this.SkeletonPointToScreen(szkielet.Joints[JointType.Head].Position), JointThickness, JointThickness);

                dc.DrawEllipse(Brushes.Gray, null, this.SkeletonPointToScreen(szkielet.Joints[JointType.HandLeft].Position), JointThickness, JointThickness);

                dc.DrawEllipse(Brushes.Gray, null, this.SkeletonPointToScreen(szkielet.Joints[JointType.HandRight].Position), JointThickness, JointThickness);

                dc.DrawEllipse(Brushes.HotPink, null, this.SkeletonPointToScreen(szkielet.Joints[JointType.FootLeft].Position), JointThickness, JointThickness);

                dc.DrawEllipse(Brushes.HotPink, null, this.SkeletonPointToScreen(szkielet.Joints[JointType.FootRight].Position), JointThickness, JointThickness);

                dc.DrawEllipse(Brushes.Green, null, this.SkeletonPointToScreen(szkielet.Joints[JointType.HipCenter].Position), JointThickness, JointThickness);

                dc.DrawEllipse(Brushes.Aqua, null, this.SkeletonPointToScreen(szkielet.Joints[JointType.ShoulderCenter].Position), 2, 2);

                dc.DrawEllipse(Brushes.Aqua, null, this.SkeletonPointToScreen(szkielet.Joints[JointType.ElbowLeft].Position), 2, 2);

                dc.DrawEllipse(Brushes.Aqua, null, this.SkeletonPointToScreen(szkielet.Joints[JointType.ElbowRight].Position), 2, 2);

                //textBox1.Text = SkeletonPointToScreen(szkielet.Joints[JointType.Head].Position).X + ", " + SkeletonPointToScreen(szkielet.Joints[JointType.Head].Position).Y;

            }

            Joint[] joints = 
            { 
                szkielet.Joints[JointType.Head],
                szkielet.Joints[JointType.HandLeft],
                szkielet.Joints[JointType.HandRight],
                szkielet.Joints[JointType.FootLeft],
                szkielet.Joints[JointType.FootRight],
                szkielet.Joints[JointType.HipCenter],
                szkielet.Joints[JointType.ShoulderCenter],
                szkielet.Joints[JointType.ElbowLeft],
                szkielet.Joints[JointType.ElbowRight]
            };

            Point [] points = new Point [joints.Length];
            for (int i = 0; i < joints.Length; i++)
            {
                points[i] = SkeletonPointToScreen(joints[i].Position);
            }

            if (tryb.analiza_cwiczenia(progressBar2, points) && wlacz_kinect)
            {
                b_zakoncz.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            
            Joint joint = szkielet.Joints[JointType.FootLeft];
            Point punkt = new Point(0, 0);

            if (joint.TrackingState == JointTrackingState.Tracked || joint.TrackingState == JointTrackingState.Inferred)
            {
                punkt = SkeletonPointToScreen(joint.Position);

                //Canvas.SetLeft(ellipse, punkt.X);
                //Canvas.SetTop(ellipse, punkt.Y);

                Canvas.SetLeft(progressBar3, punkt.X - 9.5);
                Canvas.SetTop(progressBar3, punkt.Y - 12);
            }
            else
            {
                return;
            }


            Point b = canvas1.TranslatePoint(punkt, b_zakoncz);
            b.X = b.X - 7;
            b.Y = b.Y - 7;


            if (b.X >= 0 && b.Y >= 0 && b_zakoncz.Width >= b.X && b_zakoncz.Height >= b.Y)
            {
                if (!watek && wlacz_kinect)
                {
                    b_zakoncz.RaiseEvent(new MouseEventArgs(Mouse.PrimaryDevice, 0) { RoutedEvent = Mouse.MouseEnterEvent });

                    try
                    {
                        watek = true;
                        progressBar3.Visibility = Visibility.Visible;

                        t1 = new Thread(czekaj);
                        t1.Start();
                    }
                    catch { }
                }
            }
            else
            {
                if (watek)
                {
                    if (t1.IsAlive == true)
                    {
                        t1.Abort();

                        progressBar3.Visibility = Visibility.Hidden;
                        watek = false;
                    }
                }
            }
        }

        // do analizy
        private void DrawBone(Skeleton skeleton, DrawingContext drawingContext, JointType jointType0, JointType jointType1)
        {
            Joint joint0 = skeleton.Joints[jointType0];
            Joint joint1 = skeleton.Joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }
            /*
            // Don't draw if both points are inferred
            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }
            */
            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = this.inferredBonePen;
            /*
            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = this.trackedBonePen;
            }
            */
            drawingContext.DrawLine(drawPen, this.SkeletonPointToScreen(joint0.Position), this.SkeletonPointToScreen(joint1.Position));
        }
        //

        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.kinectSensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X - 7, depthPoint.Y - 7);
        }

        private void czekaj()
        {
            int czas = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (; ; )
            {
                try
                {
                    //Thread.Sleep(1000);
                    czas = sw.Elapsed.Seconds;
                    czas++;

                    this.progressBar3.Dispatcher.Invoke(
                    DispatcherPriority.Normal,
                    new Action(
                    delegate()
                    {
                        this.progressBar3.Value = Convert.ToDouble(((double)czas / 3.0) * 100.0);
                    }));

                    if (czas >= 3)
                    {
                        break;
                    }
                }
                catch
                {
                    t1.Abort();
                }
            }

            sw.Stop();
            //kinect_active = true;
            watek = false;

            b_zakoncz.Dispatcher.Invoke(
            DispatcherPriority.Normal,
            new Action(
            delegate()
            {
                b_zakoncz.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }));
        }

        private void czas_treningu()
        {
            czas = tryb.czas_treningu;

            while (czas != 0)
            {
                this.progressBar1.Dispatcher.Invoke(
                DispatcherPriority.Normal,
                new Action(
                delegate()
                {
                    this.progressBar1.Value = ((double)czas / czas_calkowity) * 100;
                }));

                Thread.Sleep(1000);

                czas--;
            }

            this.progressBar1.Dispatcher.Invoke(
            DispatcherPriority.Normal,
            new Action(
            delegate()
            {
                this.progressBar1.Value = ((double) czas / czas_calkowity)* 100;
            }));

            /*
            this.label3.Dispatcher.Invoke(
            DispatcherPriority.Normal,
            new Action(
            delegate()
            {
                this.label3.Content = czas;
            }));
            */

            b_zakoncz.Dispatcher.Invoke(
            DispatcherPriority.Normal,
            new Action(
            delegate()
            {
                b_zakoncz.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }));

            /*
            this.mediaElement1.Dispatcher.Invoke(
            DispatcherPriority.Normal,
            new Action(
            delegate()
            {
                this.mediaElement1.Stop();
                this.mediaElement1.Close();
            }));

            this.mediaElement2.Dispatcher.Invoke(
            DispatcherPriority.Normal,
            new Action(
            delegate()
            {
                this.mediaElement2.Stop();
            }));*/

            watek_czas = false;


            //media_active = false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (watek)
            {
                t1.Abort();
            }

            if (media_active)
            {
                this.mediaElement1.Stop();
                this.mediaElement1.Close();
            }

            if (kinect_active)
            {
                kinectSensor.Stop();
            }

            if (watek_czas)
            {
                czas_t.Abort();
            }
        }

        private void b_zakoncz_Click(object sender, RoutedEventArgs e)
        {
            if (kinect_active)
            {
                Thread.Sleep(100);
                kinectSensor.Stop();

                foreach (var potentialSensor in KinectSensor.KinectSensors)
                {
                    if (potentialSensor.Status == KinectStatus.Disconnected)
                    {
                        //this.kinectSensor = potentialSensor;
                        kinectSensor = null;
                        break;
                    }
                }
                this.kinectSensor.SkeletonFrameReady -= this.nui_SkeletonFrameReady;
                this.kinectSensor.SkeletonStream.Disable();

                //kinect_active = false;
                wlacz_kinect = false;
            }
            /*
            tryb.wyswietl_liste_filmow(mediaElement1);
            muz.lista_odtwarzania(mediaElement2);
            */

            if (watek)
            {
                t1.Abort();
            }

            utworz_podsumowanie();
        }

        private void utworz_podsumowanie()
        {
            progressBar3.Visibility = Visibility.Hidden;


            if (media_active)
            {
                this.mediaElement1.Stop();
                this.mediaElement1.Close();
            }
            //this.Close();
            media_active = false;

            wynik = tryb.wynik_cwiczenia;

            PODSUMOWANIE p = new PODSUMOWANIE(czas, tryb.czas_treningu, wynik);

            if (wynik < 95)
            {
                Podsumowanie_statyczne a = new Podsumowanie_statyczne();
                a.Wlacz(p, this, magia, kinect_active);
                a.Show();
            }
            else
            {
                Podsumowanie_magiczne_zaklecie a = new Podsumowanie_magiczne_zaklecie();
                a.Wlacz(p, this, kinect_active);
                a.Show();
            }

            kinect_active = false;
            wlacz_kinect = false;
        }

        private void mediaElement1_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (media_active)
            {
                muz.lista_odtwarzania(mediaElement1);
            }
        }
    }
}
