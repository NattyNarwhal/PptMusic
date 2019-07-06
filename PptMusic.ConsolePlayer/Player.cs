using System;
using System.Text;
using System.Collections;

namespace PptMusic.ConsolePlayer
{
	class Player
	{
		static void ListEverything()
		{
			foreach (string c in MusicEngine.Instance.GetCategories())
			{
				Console.WriteLine(c);
				foreach (Style s in MusicEngine.Instance.GetStyles(c)) 
				{
					Console.WriteLine("\t{0}", s.Name);
					Console.WriteLine("\t\tBands");
					string defaultBand = s.GetDefaultBand();
					foreach (string b in s.GetBands())
					{
						Console.WriteLine("\t\t\t{0}{1}", b, b == defaultBand ? " (default)" : "");
					}
					Console.WriteLine("\t\tPersonalities");
					Personality defaultPersonality = s.GetDefaultPersonality();
					foreach (Personality p in s.GetPersonalities())
					{
						Console.WriteLine("\t\t\t{0}{1}", p, p == defaultPersonality ? " (default)" : "");
					}
					Console.WriteLine("\t\tMotifs");
					foreach (string m in s.GetMotifs())
					{
						Console.WriteLine("\t\t\t{0}", m);
					}
				}
			}
		}

		static void InteractiveStart()
		{
			ArrayList categories = MusicEngine.Instance.GetCategories();
			foreach (string c in categories)
			{
				Console.WriteLine(c);
			}
			Console.Write("Which category? ");
			string category = Console.ReadLine();
			if (!categories.Contains(category))
				return;

			ArrayList styles = MusicEngine.Instance.GetStyles(category);
			foreach (Style s in styles) 
			{
				Console.WriteLine(s.Name);
			}
			Console.Write("Which style? ");
			string styleName = Console.ReadLine();

			// XXX: LINQ
			Style style = null;
			foreach (Style s in styles)
			{
				if (s.Name == styleName)
					style = s;
			}
			if (style == null)
				return;

			// TODO: Get listing
			string band = style.GetDefaultBand();
			Personality personality = style.GetDefaultPersonality();

			ArrayList bands = style.GetBands();
			foreach (string b in bands)
			{
				Console.WriteLine("{0}{1}", b, b == band ? " (default)" : "");
			}
			Console.Write("Which band? ");
			string bandName = Console.ReadLine();
			foreach (string b in bands)
			{
				if (b == bandName)
					band = b;
			}

			ArrayList personalities = style.GetPersonalities();
			foreach (Personality p in personalities)
			{
				Console.WriteLine("{0}{1}", p.Name, p == personality ? " (default)" : "");
			}
			Console.Write("Which personality? ");
			string personalityName = Console.ReadLine();
			foreach (Personality p in personalities)
			{
				if (p.Name == personalityName)
					personality = p;
			}

			// Now start the music...
			MusicEngine.Instance.StartMusic(style, personality, band);
		}

		static void InteractiveMotif()
		{
			Style style = MusicEngine.Instance.Style;
			if (style == null)
				return;
			ArrayList motifs = style.GetMotifs();
			foreach (string m in motifs)
			{
				Console.WriteLine(m);
			}
			Console.Write("Which motif? ");
			string motif = Console.ReadLine();
			MusicEngine.Instance.PlayMotif(style, motif);
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			bool quit = false;
			while (!quit) 
			{
				Console.Write("PptMusic> ");
				string command = Console.ReadLine();
				switch (command)
				{
					case "listall":
						ListEverything();
						break;
					case "start":
						InteractiveStart();
						break;
					case "motif":
						InteractiveMotif();
						break;
					case "stop":
						MusicEngine.Instance.StopMusic();
						break;
					case "exit":
					case "quit":
						quit = true;
						break;
					default:
						Console.WriteLine("Unknown command");
						break;
				}
			}
		}
	}
}
