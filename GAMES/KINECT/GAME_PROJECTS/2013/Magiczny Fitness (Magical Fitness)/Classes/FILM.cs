using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WpfApplication2
{
	public class FILM
	{
		private static int[] ID = 
		{
			/// <summary>
			/// ID film 1
			1,
			/// </summary>
			///

			/// <summary>
			/// ID film 2
			2,
			/// </summary>
			///

			/// <summary>
			/// ID film 3
			3,
			/// </summary>
			///

			/// <summary>
			/// ID film 4
			4,
			/// </summary>
			///

			/// <summary>
			/// ID film 5
			5,
			/// </summary>
			///

			/// <summary>
			/// ID film 6
			6,
			/// </summary>
			///

			/// <summary>
			/// ID film 7
			7,
			/// </summary>
			///

			/// <summary>
			/// ID film 8
			8,
			/// </summary>
			///

			 /// <summary>
			/// ID film 9
			9,
			/// </summary>
			///

			/// <summary>
			/// ID film 10
			10,
			/// </summary>
			///

			/// <summary>
			/// ID film 11
			11,
			/// </summary>
			///

			/// <summary>
			/// ID film 12
			12,
			/// </summary>
			///

			/// <summary>
			/// ID film 13
			13,
			/// </summary>
			///

			/// <summary>
			/// ID film 14
			14,
			/// </summary>
			///

			/// <summary>
			/// ID film 15
			15,
			/// </summary>
			///
			
			/// <summary>
			/// ID film 16
			16,
			/// </summary>
			///

			/// <summary>
			/// ID film 17
			17,
			/// </summary>
			///

			/// <summary>
			/// ID film 18
			18,
			/// </summary>
			///

			/// <summary>
			/// ID film 19
			19,
			/// </summary>
			///

			/// <summary>
			/// ID film 20
			20,
			/// </summary>
			///

			/// <summary>
			/// ID film 21
			21,
			/// </summary>
			///

			/// <summary>
			/// ID film 22
			22,
			/// </summary>
			///

			/// <summary>
			/// ID film 23
			23,
			/// </summary>
			///

			/// <summary>
			/// ID film 24
			24,
			/// </summary>
			///

			/// <summary>
			/// ID film 25
			25
			/// </summary>
			///
		};

		private static string[] adres_zrodlowy_film =
		{
			/// <summary>
			/// film 1
			"film/filmy/cw1/cw1- 4 pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 2
			"film/filmy/cw1/cw1- 8 pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 3
			"film/filmy/cw1/cw1- 16pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 4
			"film/filmy/cw2/prawa/cw2p- 4pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 5
			"film/filmy/cw2/prawa/cw2p- 8pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 6
			"film/filmy/cw2/prawa/cw2p- 16pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 7
			"film/filmy/cw3/cw3- 4 pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 8
			"film/filmy/cw3/cw3- 8pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 9
			"film/filmy/cw3/cw3- 16pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 10
			"film/filmy/cw4/cw4- 4pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 11
			"film/filmy/cw4/cw4-8pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 12
			"film/filmy/cw4/cw4- 16 pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 13
			"film/shenlong.mp4",
			/// </summary>
			///

			/// <summary>
			/// film 14
			"film/filmy/cw5/cw5- 4 pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 15
			"film/filmy/cw5/cw5- 8pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 16
			"film/filmy/cw5/cw5- 16 pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 17
			"film/filmy/cw6/cw6- 4 pow.avi",
			/// </summary>
			///
			
			/// <summary>
			/// film 18
			"film/filmy/cw6/cw6- 8 pow.avi",
			/// </summary>
			///
			
			/// <summary>
			/// film 19
			"film/filmy/cw6/cw6-16 pow.avi",
			/// </summary>
			///

			/// <summary>
			/// film 20
			"film/filmy2/1.avi",
			/// </summary>
			///

			/// <summary>
			/// film 21
			"film/filmy2/2.avi",
			/// </summary>
			///

			/// <summary>
			/// film 22
			"film/filmy2/3.avi",
			/// </summary>
			///

			/// <summary>
			/// film 23
			"film/filmy2/4- lewa.avi",
			/// </summary>
			///

			/// <summary>
			/// film 24
			"film/filmy2/5- w lewo.avi",
			/// </summary>
			///

			/// <summary>
			/// film 25
			"film/filmy2/6.avi"
			/// </summary>
			///
		};

		public void wlacz_film(MediaElement me, int ID_filmu)
		{
			int index_ID = 0;
			for (int i = 0; i < ID.Length; i++)
			{
				if (ID[i] == ID_filmu)
				{
					index_ID = i;
					break;
				}
			}

			me.Source = new Uri(adres_zrodlowy_film[index_ID], UriKind.Relative);

			me.LoadedBehavior = MediaState.Manual;
			me.UnloadedBehavior = MediaState.Stop;
			me.Volume = 0;
			me.Play();
		}
	}
}
