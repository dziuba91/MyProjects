using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Kinect;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Kinect.Toolkit.Interaction;

namespace KinectWspolbiezny
{
	public class Kinect : DrawableGameComponent
	{
		private readonly Dictionary<KinectStatus, string> statusMap = new Dictionary<KinectStatus, string>();

		private readonly ColorImageFormat colorImageFormat;

		private readonly DepthImageFormat depthImageFormat;

		public KinectSensor Sensor { get; private set; }

		public KinectStatus LastStatus { get; private set; }

		public InteractionStream intStream { get; set; }

		public Kinect(Game game, ColorImageFormat colorFormat, DepthImageFormat depthFormat)
			: base(game)
		{
			this.colorImageFormat = colorFormat;
			this.depthImageFormat = depthFormat;

			KinectSensor.KinectSensors.StatusChanged += this.KinectSensors_StatusChanged;
			this.DiscoverSensor();
		}

		protected override void UnloadContent()
		{
			base.UnloadContent();

			if (null != this.Sensor)
			{
				this.Sensor.Stop();
			}
		}

		private void DiscoverSensor()
		{
			this.Sensor = KinectSensor.KinectSensors.FirstOrDefault();	// ustawienienie zmiennej Sensor, przechowujące parametry połączenia z Kinect'em

			if (null != this.Sensor) // warunek zostanie wykonany w przypadku nawiązania połączenia z sensorem Kinect
			{
				this.LastStatus = this.Sensor.Status; // ustawienie aktualnego statusu połączenia

				if (this.LastStatus == KinectStatus.Connected) // sprawdzenie, czy status osiągnął wartość "Connected"
				{
					try
					{
						this.Sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
						this.Sensor.DepthStream.Range = DepthRange.Default;
						this.Sensor.SkeletonStream.EnableTrackingInNearRange = false; // zezwolenie na przesyłanie danych dla "NUI Skeleton Tracking" w trybie bliskim (gdy nie wszytskie części składowe osoby śledzonej są widoczne z powodu bliskiej pozycji względem urządzenia)

						var parameters = new TransformSmoothParameters
						{
							Smoothing = 0.75f,
							Correction = 0.0f,
							Prediction = 0.0f,
							JitterRadius = 0.05f,
							MaxDeviationRadius = 0.04f
						};

						this.Sensor.SkeletonStream.Enable(parameters); // włączenie przesyłania danych z sensora dotyczących "NUI Skeleton Tracking"
						this.Sensor.ColorStream.Enable(this.colorImageFormat); // włączenie przesyłania danych z sensora od kamery RGB
						this.Sensor.DepthStream.Enable(this.depthImageFormat); // włączenie przesyłania danych z sensora od kamery głębokości
						this.Sensor.DepthStream.Range = DepthRange.Default;

						//
						try
						{
							this.Sensor.Start(); // uruchom działanie sensora względem danej aplikacji

							InteractionClient mic = new InteractionClient();
							this.intStream = new InteractionStream(Sensor, mic);
						}
						catch (IOException)
						{
							this.intStream = null;
							this.Sensor = null; // w przypadku błędu usunąć ustawione wcześniej dane
						}
					}
					catch (InvalidOperationException)
					{
						this.Sensor = null; // w przypadku błędu usunąć ustawione wcześniej dane
						this.intStream = null;
					}
				}
			}
			else
			{
				this.LastStatus = KinectStatus.Disconnected; // gdy połączenie nie zostało poprawnie zrealizowane ustaw status na "Disconnected" 
			}
		}

		private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
		{
			if (e.Status != KinectStatus.Connected)
			{
				e.Sensor.Stop();
			}

			this.LastStatus = e.Status;
			this.DiscoverSensor();
		}
		
		class InteractionClient : IInteractionClient
		{
			public InteractionInfo GetInteractionInfoAtLocation(int skeletonTrackingId, InteractionHandType handType, double x, double y)
			{
				var info = new InteractionInfo();
				info.IsGripTarget = true;
				info.IsPressTarget = false;
				info.PressAttractionPointX = 0f;
				info.PressAttractionPointY = 0f;
				info.PressTargetControlId = 0;

				return info;
			}
		}
	}
}