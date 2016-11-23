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
	/// Interaction logic for TrybKondycyjnyZestawCwiczen.xaml
	/// </summary>
	public partial class TrybKondycyjnyZestawCwiczen : Window
	{
		MAGICZNE_ZAKLECIE magia= null;
		TRYB_TRENINGOWY tryb;
		CWICZENIE cw;

		KinectSensor kinectSensor = null;
		Thread t1 = null;

		private bool[] watek = { false, false, false, false, false, false, false, false, false, false, false, false, false };
		private bool watek_active = false;
		private bool kinect_active = false;

		private int pozostalych_cwiczen = 6;

		//
		[DllImportAttribute("user32.dll", EntryPoint = "SetCursorPos")]
		[return: MarshalAsAttribute(UnmanagedType.Bool)]
		public static extern bool SetCursorPos(int x, int y);

		public TrybKondycyjnyZestawCwiczen()
		{
			InitializeComponent();
		}

		public void Wlacz(TRYB_TRENINGOWY t, MAGICZNE_ZAKLECIE magia)
		{
			this.tryb = t;

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
			
			//
			Button[] but = { button1, button2, button3, button4, button5, button6, button_czas1, button_czas2, button_czas3, button_dalej,
							 button_powtorzenia1, button_powtorzenia2, button_powtorzenia3};

			for (int i = 0; i < 13; i++)
			{
				Point b = canvas1.TranslatePoint(punkt, but[i]);

				if (b.X >= 0 && b.Y >= 0 && but[i].Width >= b.X && but[i].Height >= b.Y)
				{
					Button button = but[i];

					if (!watek[i] && watek_active)
					{
						t1.Abort();

						for (int j = 0; j < but.Length; j++)
						{
							watek[j] = false;
						}
					}

					if (!watek[i])
					{
						button.RaiseEvent(new MouseEventArgs(Mouse.PrimaryDevice, 0) { RoutedEvent = Mouse.MouseEnterEvent });

						try
						{
							watek[i] = true;
							watek_active = true;
							progressBar1.Visibility = Visibility.Visible;

							t1 = new Thread(() => czekaj(button));
							t1.Start();
						}
						catch { }
					}

					return;
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
							watek_active = false;
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
			watek_active = false;

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
			watek[0] = false;
			watek_active = false;

			progressBar1.Visibility = Visibility.Hidden;

			//
			int cwiczenie = 1;
			tryb.utworz_cwiczenie(cwiczenie);

			Zmien_zaznaczenie_elementu(cwiczenie, tryb.cwiczenie_status[cwiczenie]);
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			watek[1] = false;
			watek_active = false;

			progressBar1.Visibility = Visibility.Hidden;

			//
			int cwiczenie = 2;
			tryb.utworz_cwiczenie(cwiczenie);

			Zmien_zaznaczenie_elementu(cwiczenie, tryb.cwiczenie_status[cwiczenie]);
		}

		private void button3_Click(object sender, RoutedEventArgs e)
		{
			watek[2] = false;
			watek_active = false;

			progressBar1.Visibility = Visibility.Hidden;

			//
			int cwiczenie = 3;
			tryb.utworz_cwiczenie(cwiczenie);

			Zmien_zaznaczenie_elementu(cwiczenie, tryb.cwiczenie_status[cwiczenie]);
		}

		private void button4_Click(object sender, RoutedEventArgs e)
		{
			watek[3] = false;
			watek_active = false;

			progressBar1.Visibility = Visibility.Hidden;
			
			//
			int cwiczenie = 4;
			tryb.utworz_cwiczenie(cwiczenie);

			Zmien_zaznaczenie_elementu(cwiczenie, tryb.cwiczenie_status[cwiczenie]);
		}

		private void button5_Click(object sender, RoutedEventArgs e)
		{
			watek[4] = false;
			watek_active = false;

			progressBar1.Visibility = Visibility.Hidden;
			
			//
			int cwiczenie = 5;
			tryb.utworz_cwiczenie(cwiczenie);

			Zmien_zaznaczenie_elementu(cwiczenie, tryb.cwiczenie_status[cwiczenie]);
		}

		private void button6_Click(object sender, RoutedEventArgs e)
		{
			watek[5] = false;
			watek_active = false;

			progressBar1.Visibility = Visibility.Hidden;
			
			//
			int cwiczenie = 6;
			tryb.utworz_cwiczenie(cwiczenie);

			Zmien_zaznaczenie_elementu(cwiczenie, tryb.cwiczenie_status[cwiczenie]);
		}

		private void button_powtorzenia1_Click(object sender, RoutedEventArgs e)
		{
			watek[6] = false;
			watek_active = false;

			progressBar1.Visibility = Visibility.Hidden;
			
			//
			int powtorzenia = 11;
			int ilosc_powtorzen = 1;
			tryb.aktualizuj_parametry_treningu(powtorzenia, ilosc_powtorzen);

			Zmien_zaznaczenie_elementu(powtorzenia);
		}

		private void button_powtorzenia2_Click(object sender, RoutedEventArgs e)
		{
			watek[7] = false;
			watek_active = false;

			progressBar1.Visibility = Visibility.Hidden;
			
			//
			int powtorzenia = 12;
			int ilosc_powtorzen = 2;
			tryb.aktualizuj_parametry_treningu(powtorzenia, ilosc_powtorzen);

			Zmien_zaznaczenie_elementu(powtorzenia);
		}

		private void button_powtorzenia3_Click(object sender, RoutedEventArgs e)
		{
			watek[8] = false;
			watek_active = false;

			progressBar1.Visibility = Visibility.Hidden;
			
			//
			int powtorzenia = 13;
			int ilosc_powtorzen = 4;
			tryb.aktualizuj_parametry_treningu(powtorzenia, ilosc_powtorzen);

			Zmien_zaznaczenie_elementu(powtorzenia);
		}

		private void button_czas1_Click(object sender, RoutedEventArgs e)
		{
			watek[9] = false;
			watek_active = false;

			progressBar1.Visibility = Visibility.Hidden;
			
			//
			int czas = 21;
			int ilosc_czasu = 5;
			tryb.aktualizuj_parametry_treningu(czas, ilosc_czasu);

			Zmien_zaznaczenie_elementu(czas);
		}

		private void button_czas2_Click(object sender, RoutedEventArgs e)
		{
			watek[10] = false;
			watek_active = false;

			progressBar1.Visibility = Visibility.Hidden;
			
			//
			int czas = 22;
			int ilosc_czasu = 10;
			tryb.aktualizuj_parametry_treningu(czas, ilosc_czasu); ;

			Zmien_zaznaczenie_elementu(czas);
		}

		private void button_czas3_Click(object sender, RoutedEventArgs e)
		{
			watek[11] = false;
			watek_active = false;

			progressBar1.Visibility = Visibility.Hidden;
			
			//
			int czas = 23;
			int ilosc_czasu = 15;
			tryb.aktualizuj_parametry_treningu(czas, ilosc_czasu);

			Zmien_zaznaczenie_elementu(czas);
		}

		private void button_dalej_Click(object sender, RoutedEventArgs e)
		{
			watek[12] = false;

			progressBar1.Visibility = Visibility.Hidden;

			if (watek_active)
			{
				t1.Abort();
				watek_active = false;
			}

			if (tryb.wlacz_trening())
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
				}
				Thread.Sleep(500);

				//
				TrybKondycyjny a = new TrybKondycyjny();
				a.Wlacz(tryb, kinect_active, magia);
				a.Show();

				kinect_active = false;
				this.Close();
			}
			else
			{
				textBlock1.Text = "Niepoprawnie uzupełnione \ndane! Nie zaznaczyłeś \nwszystkich elementów!";
			}
		}

		private void Zmien_zaznaczenie_elementu(int numer, bool zaznacz)
		{
			if (numer == 1)
			{
				if (zaznacz)
				{
					b_1.BorderBrush = Brushes.Red;
				}
				else
				{
					b_1.BorderBrush = Brushes.White;
				}
			}

			if (numer == 2)
			{
				if (zaznacz)
				{
					b_2.BorderBrush = Brushes.Red;
				}
				else
				{
					b_2.BorderBrush = Brushes.White;
				}
			}

			if (numer == 3)
			{
				if (zaznacz)
				{
					b_3.BorderBrush = Brushes.Red;
				}
				else
				{
					b_3.BorderBrush = Brushes.White;
				}
			}

			if (numer == 4)
			{
				if (zaznacz)
				{
					b_4.BorderBrush = Brushes.Red;
				}
				else
				{
					b_4.BorderBrush = Brushes.White;
				}
			}

			if (numer == 5)
			{
				if (zaznacz)
				{
					b_5.BorderBrush = Brushes.Red;
				}
				else
				{
					b_5.BorderBrush = Brushes.White;
				}
			}

			if (numer == 6)
			{
				if (zaznacz)
				{
					b_6.BorderBrush = Brushes.Red;
				}
				else
				{
					b_6.BorderBrush = Brushes.White;
				}
			}

			if (zaznacz)
			{
				pozostalych_cwiczen--;
				label1.Content = pozostalych_cwiczen + label1.Content.ToString().Remove(0, 1);
			}
			else
			{
				pozostalych_cwiczen++;
				label1.Content = pozostalych_cwiczen + label1.Content.ToString().Remove(0, 1);
			}
		}

		private void Zmien_zaznaczenie_elementu(int numer)
		{
			if (numer - 10 == 1 || numer - 10 == 2 || numer - 10 == 3)
			{
				Border[] bor = { b_ip_1, b_ip_2, b_ip_3 };

				for (int i = 0; i < 3; i++)
				{
					bor[i].BorderBrush = Brushes.White;
				}

				bor[numer - 11].BorderBrush = Brushes.Red;
			}
			else if (numer - 20 == 1 || numer - 20 == 2 || numer - 20 == 3)
			{
				Border[] bor = { b_c_1, b_c_2, b_c_3 };

				for (int i = 0; i < 3; i++)
				{
					bor[i].BorderBrush = Brushes.White;
				}

				bor[numer - 21].BorderBrush = Brushes.Red;
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

		private void Window_MouseEnter(object sender, MouseEventArgs e)
		{

		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				this.Close();
			}
		}

		private void button1_MouseEnter(object sender, MouseEventArgs e)
		{
			cw = new CWICZENIE();
			textBlock1.Text = cw.pobierz_opis(1, 1);
		}

		private void button2_MouseEnter(object sender, MouseEventArgs e)
		{
			cw = new CWICZENIE();
			textBlock1.Text = cw.pobierz_opis(1, 2);
		}

		private void button3_MouseEnter(object sender, MouseEventArgs e)
		{
			cw = new CWICZENIE();
			textBlock1.Text = cw.pobierz_opis(1, 3);
		}

		private void button4_MouseEnter(object sender, MouseEventArgs e)
		{
			cw = new CWICZENIE();
			textBlock1.Text = cw.pobierz_opis(1, 4);
		}

		private void button5_MouseEnter(object sender, MouseEventArgs e)
		{
			cw = new CWICZENIE();
			textBlock1.Text = cw.pobierz_opis(1, 5);
		}

		private void button6_MouseEnter(object sender, MouseEventArgs e)
		{
			cw = new CWICZENIE();
			textBlock1.Text = cw.pobierz_opis(1, 6);
		}
	}
}
