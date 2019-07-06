using System;
using System.Text;
using System.Collections;

namespace PptMusic
{
	/// <summary>
	/// A singleton class that controls the custom soundtrack.
	/// </summary>
	public class MusicEngine
	{
		// Singleton stuff
		static MusicEngine _me;

		/// <summary>
		/// The single instance of the music engine.
		/// </summary>
		public static MusicEngine Instance
		{
			get 
			{
				return _me;
			}
		}

		static MusicEngine()
		{
			_me = new MusicEngine();
		}

		MusicEngine()
		{
			Ppmusau.MT_LoadMusicEngine();
			_style = null;
			_personality = null;
			_band = null;
		}

		~MusicEngine() // XXX: Disposable?
		{
			Ppmusau.MT_StopMusic();
			Ppmusau.MT_FreeMusicEngine();
		}

		// Not singleton stuff
		Style _style;
		Personality _personality;
		string _band;

		/// <summary>
		/// The current style being played.
		/// </summary>
		public Style Style
		{
			get 
			{
				return _style;
			}
		}

		/// <summary>
		/// The current personality being played.
		/// </summary>
		public Personality Personality
		{
			get 
			{
				return _personality;
			}
		}

		/// <summary>
		/// The current band being played.
		/// </summary>
		public string Band
		{
			get 
			{
				return _band;
			}
		}

		/// <summary>
		/// Stops the music from being played.
		/// </summary>
		public void StopMusic()
		{
			Ppmusau.MT_StopMusic();
		}

		/// <summary>
		/// Starts the music with the specified parameters.
		/// </summary>
		/// <param name="style">The style of music to play.</param>
		/// <param name="personality">The personality that the music takes on.</param>
		/// <param name="band">The band that plays the instruments.</param>
		public void StartMusic(Style style, Personality personality, string band)
		{
			// sanity check
			if (style != personality.Style)
				throw new ArgumentException("Personality style doesn't match");
			if (band == null || band == "")
				throw new ArgumentNullException("Band cannot be empty");

			_style = style;
			_personality = personality;
			_band = band;
			// XXX: Why not Queue?
			Ppmusau.MT_StartMusic(style.Pointer, personality.Name, band);
		}

		/// <summary>
		/// Starts the music with the last style specified.
		/// </summary>
		public void ResumeMusic()
		{
			// sanity check
			if (Style == null || Personality == null || Band == null || Band == "")
				throw new InvalidOperationException("Can't resume playback without it being previously set");

			// XXX: Why not Queue?
			Ppmusau.MT_StartMusic(Style.Pointer, Personality.Name, Band);
		}

		/// <summary>
		/// Plays a non-repeating motif.
		/// </summary>
		/// <param name="style">The style that the motif comes from.</param>
		/// <param name="motif">The motif to play.</param>
		public void PlayMotif(Style style, string motif)
		{
			// XXX: Why not Queue?
			Ppmusau.MT_PlayMotif(style.Pointer, motif);
		}

		/// <summary>
		/// Plays a non-repeating motif.
		/// </summary>
		/// <param name="motif">The motif to play.</param>
		public void PlayMotif(string motif)
		{
			// XXX: Why not Queue?
			Ppmusau.MT_PlayMotif(Style.Pointer, motif);
		}

		// XXX: Iterators
		// XXX: Consider refactoring so categories are an object

		/// <summary>
		/// Returns a list of categories that have styles.
		/// </summary>
		/// <returns>A list of categories that have styles.</returns>
		public ArrayList GetCategories()
		{
			StringBuilder buffer = new StringBuilder(0x100);
			Ppmusau.MT_GetFirstCategory(buffer);
			ArrayList al = new ArrayList();
			al.Add(buffer.ToString());
			while (Ppmusau.MT_GetNextCategory(buffer, buffer.ToString()) > 0)
			{
				al.Add(buffer.ToString());
			}
			return al;
		}

		/// <summary>
		/// Returns a list of styles.
		/// </summary>
		/// <param name="category">The category to pull the styles from.</param>
		/// <returns>A list of styles.</returns>
		public ArrayList GetStyles(string category)
		{
			StringBuilder buffer = new StringBuilder(0x100);
			Ppmusau.MT_GetFirstStyle(buffer, category);
			ArrayList al = new ArrayList();
			al.Add(new Style(category, buffer.ToString()));
			while (Ppmusau.MT_GetNextStyle(buffer, category, buffer.ToString()) > 0)
			{
				al.Add(new Style(category, buffer.ToString()));
			}
			return al;
		}
	}
}
