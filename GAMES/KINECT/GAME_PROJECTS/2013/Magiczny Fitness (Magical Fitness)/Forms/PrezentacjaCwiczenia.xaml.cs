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
	/// Interaction logic for PrezentacjaCwiczenia.xaml
	/// </summary>
	public partial class PrezentacjaCwiczenia : Window
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
		private bool kinect_active = false;
		private bool media_active = false;
		private bool wlacz_kinect = false;

		private int czas;

		//
		[DllImportAttribute("user32.dll", EntryPoint = "SetCursorPos")]
		[return: MarshalAsAttribute(UnmanagedType.Bool)]
		public static extern bool SetCursorPos(int x, int y);

		public PrezentacjaCwiczenia()
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

			tryb.wyswietl_film(mediaElement1);

			muz = new MUZYKA();
			muz.lista_odtwarzania(mediaElement2);

			cw = new CWICZENIE();
			textBlock1.Text = cw.pobierz_opis(2, tryb.ID_cwiczenie);

			media_active = true;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (wlacz_kinect)
			{
				foreach (var potentialSensor in KinectSensor.KinectSensors)
				{
					if (potentialSensor.Status == KinectStatus.Connected)
					{
						this.kinectSensor = potentialSensor;
						break;
					}
				}

				//Dodatkowe parametry pozwalajace na usuniecie drgan
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

				//Dodanie zdarzenia, przechwytujacego dane
				this.kinectSensor.SkeletonFrameReady += this.nui_SkeletonFrameReady;

				kinectSensor.Start();
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
					skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
					skeletonFrame.CopySkeletonDataTo(skeletons);

					receivedData = true;
				}
				else
				{
					border_kursor_1.Visibility = Visibility.Hidden;
					border_kursor_2.Visibility = Visibility.Hidden;
				}
			}

			if (receivedData)
			{
				if (skeletons.Length != 0)
				{
					foreach (Skeleton skel in skeletons)
					{
						if (skel.TrackingState == SkeletonTrackingState.Tracked) //wlacz_kinect
						{
							border_kursor_1.Visibility = Visibility.Visible;
							border_kursor_2.Visibility = Visibility.Visible;
							SetCursorPosition(border_kursor_1, border_kursor_2, skel.Joints[JointType.HandRight]);
							return;
						}
						else if (skel.TrackingState == SkeletonTrackingState.PositionOnly)
						{
							border_kursor_1.Visibility = Visibility.Visible;
							border_kursor_2.Visibility = Visibility.Visible;
							SetCursorPosition(border_kursor_1, border_kursor_2, skel.Joints[JointType.HandRight]);
							return;
						}
						else  //?
						{
							border_kursor_1.Visibility = Visibility.Hidden;
							border_kursor_2.Visibility = Visibility.Hidden;
						}
					}
				}
			}
		}

		private void SetCursorPosition(FrameworkElement cursor1, FrameworkElement cursor2, Joint joint)
		{
			kinect_active = true;
			Point punkt = new Point(0, 0);

			if (joint.TrackingState == JointTrackingState.Tracked || joint.TrackingState == JointTrackingState.Inferred)
			{
				punkt = SkeletonPointToScreen(joint.Position);

				Canvas.SetLeft(cursor1, punkt.X);
				Canvas.SetTop(cursor1, punkt.Y);

				Canvas.SetLeft(cursor2, punkt.X);
				Canvas.SetTop(cursor2, punkt.Y);

				Canvas.SetLeft(progressBar1, punkt.X - 9.5);
				Canvas.SetTop(progressBar1, punkt.Y - 12);
			}
			else
			{
				cursor1.Visibility = Visibility.Visible;
				cursor2.Visibility = Visibility.Visible;
				return;
			}

			if (kinect_active)
			{
				Point b1 = punkt;
				try
				{
					b1 = canvas1.PointToScreen(punkt);
					SetCursorPos((int)b1.X + 12, (int)b1.Y + 10);
				}
				catch { }
			}

			//
			Point b = canvas1.TranslatePoint(punkt, button1);

			if (b.X >= 0 && b.Y >= 0 && button1.Width >= b.X && button1.Height >= b.Y)
			{
				if (!watek && wlacz_kinect)
				{
					button1.RaiseEvent(new MouseEventArgs(Mouse.PrimaryDevice, 0) { RoutedEvent = Mouse.MouseEnterEvent });

					try
					{
						watek = true;
						progressBar1.Visibility = Visibility.Visible;

						t1 = new Thread(() => czekaj(button1));
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

						progressBar1.Visibility = Visibility.Hidden;
						watek = false;
					}
				}
			}
		}

		private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
		{
			DepthImagePoint depthPoint = this.kinectSensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
			return new Point(depthPoint.X, depthPoint.Y);
		}

		private void czekaj(Button button)
		{
			int czas = 0;
			Stopwatch sw = new Stopwatch();
			sw.Start();

			for (; ; )
			{
				try
				{
					czas = sw.Elapsed.Seconds;
					czas++;

					this.progressBar1.Dispatcher.Invoke(
					DispatcherPriority.Normal,
					new Action(
						delegate()
						{
							this.progressBar1.Value = Convert.ToDouble(((double)czas / 3.0) * 100.0);
						})
					);

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
			kinect_active = true;
			watek = false;

			button.Dispatcher.Invoke(
			DispatcherPriority.Normal,
			new Action(
				delegate()
				{
					button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
				})
			);
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			if (kinect_active)
			{
				Thread.Sleep(100);
				kinectSensor.Stop();

				foreach (var potentialSensor in KinectSensor.KinectSensors)
				{
					if (potentialSensor.Status == KinectStatus.Disconnected)
					{
						kinectSensor = null;
						break;
					}
				}
				this.kinectSensor.SkeletonFrameReady -= this.nui_SkeletonFrameReady;
				this.kinectSensor.SkeletonStream.Disable();

				wlacz_kinect = false;
			}
			
			if (watek)
			{
				t1.Abort();
			}

			if (media_active)
			{
				this.mediaElement1.Stop();
				this.mediaElement1.Close();

				this.mediaElement2.Stop();
			}

			AnalizaRuchu a = new AnalizaRuchu();
			a.Wlacz(tryb, kinect_active, magia);
			a.Show();

			this.Close();

			media_active = false;
			kinect_active = false;
		}

		private void mediaElement1_MediaEnded(object sender, RoutedEventArgs e)
		{
			if (media_active)
			{
				tryb.wyswietl_film(mediaElement1);
			}
		}

		private void mediaElement2_MediaEnded(object sender, RoutedEventArgs e)
		{
			if (media_active)
			{
				muz.lista_odtwarzania(mediaElement2);
			}
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

				this.mediaElement2.Stop();
			}

			if (kinect_active)
			{
				kinectSensor.Stop();
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				this.Close();
			}
		}
	}
}