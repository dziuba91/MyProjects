using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WpfApplication2
{
	class MUZYKA
	{
		Random r1 = new Random(System.DateTime.Now.Millisecond);
		Random r2 = new Random(System.DateTime.Now.Millisecond);

		private int [] play_lista;
		private int index_play_listy = 0;

		private static int[] ID = 
		{
			/// <summary>
			/// ID muzyka 1
			1,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 2
			2,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 3
			3,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 4
			4,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 5
			5,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 6
			6,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 7
			7,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 8
			8,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 9
			9,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 10
			10,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 11
			11,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 12
			12,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 13
			13,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 14
			14,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 15
			15,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 16
			16,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 17
			17,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 18
			18,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 19
			19,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 19
			20,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 21
			21,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 22
			22,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 23
			23,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 24
			24,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 25
			25,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 26
			26,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 27
			27,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 28
			28,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 29
			29,
			/// </summary>
			///

			/// <summary>
			/// ID muzyka 30
			30
			/// </summary>
			///
		};

		private static string[] adres_zrodlowy_film =
		{
			/// <summary>
			/// muzyka 1
			"muzyka/1 (1).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 2
			"muzyka/1 (2).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 3
			"muzyka/1 (3).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 4
			"muzyka/1 (4).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 5
			"muzyka/1 (5).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 6
			"muzyka/1 (6).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 7
			"muzyka/1 (7).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 8
			"muzyka/1 (8).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 9
			"muzyka/1 (9).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 10
			"muzyka/1 (10).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 11
			"muzyka/1 (11).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 12
			"muzyka/1 (12).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 13
			"muzyka/1 (13).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 14
			"muzyka/1 (14).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 15
			"muzyka/1 (15).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 16
			"muzyka/1 (16).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 17
			"muzyka/1 (17).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 18
			"muzyka/1 (18).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 19
			"muzyka/1 (19).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 20
			"muzyka/1 (20).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 21
			"muzyka/1 (21).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 22
			"muzyka/1 (22).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 23
			"muzyka/1 (23).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 24
			"muzyka/1 (24).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 25
			"muzyka/1 (25).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 26
			"muzyka/1 (26).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 27
			"muzyka/1 (27).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 28
			"muzyka/1 (28).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 29
			"muzyka/1 (29).mp3",
			/// </summary>
			///

			/// <summary>
			/// muzyka 30
			"muzyka/dragon.mp3"
			/// </summary>
			///
		};

		private static bool[] status_muzyki =
		{
			/// <summary>
			/// muzyka 1
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 2
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 3
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 4
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 5
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 6
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 7
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 8
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 9
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 10
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 11
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 12
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 13
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 14
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 15
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 16
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 17
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 18
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 19
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 20
			true,
			/// </summary>
			///

			/// <summary>
			/// muzyka 21
			false
			/// </summary>
			///
		};

		public MUZYKA()
		{
			int tmp = r1.Next(1, 30);

			play_lista = new int[tmp];

			for (int i = 0; i < play_lista.Length; i++)
			{
				int tmp2 = r2.Next(1, 30);
				play_lista[i] = tmp2;
			}
		}

		public void wlacz_muzyke(MediaElement me, int ID_muzyka)
		{
			int index_ID = 0;
			for (int i = 0; i < ID.Length; i++)
			{
				if (ID[i] == ID_muzyka)
				{
					index_ID = i;
					break;
				}
			}

			me.Source = new Uri(adres_zrodlowy_film[index_ID], UriKind.Relative);

			me.LoadedBehavior = MediaState.Manual;
			me.UnloadedBehavior = MediaState.Stop;
			me.Play();
		}

		public void lista_odtwarzania(MediaElement me)
		{
			wlacz_muzyke(me, play_lista[index_play_listy]);

			index_play_listy++;
			if (index_play_listy >= play_lista.Length)
			{
				index_play_listy = 0;
			}
		}
	}
}
