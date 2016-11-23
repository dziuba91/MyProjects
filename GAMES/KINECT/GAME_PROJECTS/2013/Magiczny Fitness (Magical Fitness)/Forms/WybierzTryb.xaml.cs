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
	/// Interaction logic for WybierzTryb.xaml
	/// </summary>
	public partial class WybierzTryb : Window
	{
		MAGICZNE_ZAKLECIE magia = null;

		KinectSensor kinectSensor = null;
		Thread t1 = null;

		private bool[] watek = { false, false, false };
		private bool kinect_active = false;

		//
		[DllImportAttribute("user32.dll", EntryPoint = "SetCursorPos")]
		[return: MarshalAsAttribute(UnmanagedType.Bool)]
		public static extern bool SetCursorPos(int x, int y);

		public WybierzTryb()
		{
			InitializeComponent();
		}

		public void Wlacz(MAGICZNE_ZAKLECIE magia)
		{
			this.magia = magia;
			if (magia != null)
			{
				magia.efekt_zaklecia(canvas1);
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
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

			this.kinectSensor.SkeletonFrameReady += this.nui_SkeletonFrameReady;

			kinectSensor.Start();
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
						if (skel.TrackingState == SkeletonTrackingState.Tracked)
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


			Button[] but = { button1, button2, button3 };

			for (int i = 0; i < 3; i++)
			{
				Point b = canvas1.TranslatePoint(punkt, but[i]);

				if (b.X >= 0 && b.Y >= 0 && but[i].Width >= b.X && but[i].Height >= b.Y)
				{
					Button button = but[i];

					if (!watek[i])
					{
						button.RaiseEvent(new MouseEventArgs(Mouse.PrimaryDevice, 0) { RoutedEvent = Mouse.MouseEnterEvent });

						watek[i] = true;
						progressBar1.Visibility = Visibility.Visible;

						t1 = new Thread(() => czekaj(button));
						t1.Start();
					}
				}
				else
				{
					if (watek[i])
					{
						if (t1.IsAlive == true)
						{
							t1.Abort();

							progressBar1.Visibility = Visibility.Hidden;
							watek[i] = false;
						}
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

				kinect_active = false;
			}

			for (int i = 0; i < 3; i++)
			{
				if (watek[i])
				{
					t1.Abort();
				}
			}

			TRYB_TRENINGOWY t = new TRYB_TRENINGOWY();

			TrybKondycyjnyZestawCwiczen a = new TrybKondycyjnyZestawCwiczen();
			a.Wlacz(t, magia);
			a.Show();

			this.Close();
		}

		private void button2_Click(object sender, RoutedEventArgs e)
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

				kinect_active = false;
			}
			

			for (int i = 0; i < 3; i++)
			{
				if (watek[i])
				{
					t1.Abort();
				}
			}

			TRYB_KONTROLI_RUCHOW t = new TRYB_KONTROLI_RUCHOW();

			TrybStatycznyWyborCwiczenia a = new TrybStatycznyWyborCwiczenia();
			a.Wlacz(t, magia);
			a.Show();

			this.Close();
		}

		private void button3_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				this.Close();
			}
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			for (int i = 0; i < 3; i++)
			{
				if (watek[i])
				{
					t1.Abort();
				}
			}

			if (kinect_active)
			{
				kinectSensor.Stop();
			}
		}

		private void button1_MouseEnter(object sender, MouseEventArgs e)
		{
			wyswietl_opis(1);
		}

		private void button2_MouseEnter(object sender, MouseEventArgs e)
		{
			wyswietl_opis(2);
		}

		private void wyswietl_opis(int numer)
		{
			if (numer == 1)
			{
				label1.Content = "Tryb Treningowy: wielo- ćwiczeniowy";
			}
			else if (numer == 2)
			{
				label1.Content = "Tryb z kontrolą ruchów: \nSprawdź, czy poprawnie wykonujesz ćwiczenie.";
			}
		}
	}
}
