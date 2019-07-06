using System;
using System.Text;
using System.Collections;

namespace PptMusic
{
	/// <summary>
	/// A style of music.
	/// </summary>
	public class Style
	{
		string _category, _guid, _name, _filename;
		IntPtr _pointer;

		/// <summary>
		/// The category that the style belongs to.
		/// </summary>
		public string Category
		{
			get 
			{
				return _category;
			}
		}

		/// <summary>
		/// The internal ID of the style.
		/// </summary>
		public string Guid
		{
			get 
			{
				return _guid;
			}
		}

		/// <summary>
		/// The name of the style.
		/// </summary>
		public string Name
		{
			get 
			{
				return _name;
			}
		}

		/// <summary>
		/// The filename of the style on disk.
		/// </summary>
		public string Filename
		{
			get 
			{
				return _filename;
			}
		}

		internal IntPtr Pointer
		{
			get
			{
				return _pointer;
			}
		}

		internal Style(string category, string guid)
		{
			_guid = guid;
			_category = category;

			StringBuilder buffer = new StringBuilder(0x100);
			
			Ppmusau.MT_GetStyleName(buffer, category, guid);
			_name = buffer.ToString();
			Ppmusau.MT_GetStyleFilename(buffer, category, guid);
			_filename = buffer.ToString();
			
			_pointer = Ppmusau.MT_GetStylePtr(category, guid);
		}

		/// <summary>
		/// Gets a string representing the style.
		/// </summary>
		/// <returns>The name of the style.</returns>
		public override string ToString()
		{
			return Name;
		}

        // XXX: Would it make sense to convert motif and band to container types?

		/// <summary>
		/// Gets the default band associated with the style.
		/// </summary>
		/// <returns>The style's default band.</returns>
		public string GetDefaultBand()
		{
			StringBuilder buffer = new StringBuilder(0x100);
			Ppmusau.MT_GetDefaultBand(buffer, _pointer);
			return buffer.ToString();
		}

		/// <summary>
		/// Gets the default personality associated with the style.
		/// </summary>
		/// <returns>The style's default personality.</returns>
		public Personality GetDefaultPersonality()
		{
			StringBuilder buffer = new StringBuilder(0x100);
			Ppmusau.MT_GetDefaultPersonality(buffer, _pointer);
			return new Personality(this, buffer.ToString());
		}

		/// <summary>
		/// Gets a list of bands associated with the style.
		/// </summary>
		/// <returns>A list of bands associated with the style</returns>
		public ArrayList GetBands()
		{
			StringBuilder buffer = new StringBuilder(0x100);
			Ppmusau.MT_GetFirstBand(buffer, _pointer);
			ArrayList al = new ArrayList();
			al.Add(buffer.ToString());
			while (Ppmusau.MT_GetNextBand(buffer, _pointer, buffer.ToString()) > 0)
			{
				al.Add(buffer.ToString());
			}
			return al;
		}

		/// <summary>
		/// Gets a list of personalities associated with the style.
		/// </summary>
		/// <returns>A list of personalities associated with the style</returns>
		public ArrayList GetPersonalities()
		{
			StringBuilder buffer = new StringBuilder(0x100);
			Ppmusau.MT_GetFirstPersonality(buffer, _pointer);
			ArrayList al = new ArrayList();
			al.Add(new Personality(this, buffer.ToString()));
			while (Ppmusau.MT_GetNextPersonality(buffer, _pointer, buffer.ToString()) > 0)
			{
				al.Add(new Personality(this, buffer.ToString()));
			}
			return al;
		}

		/// <summary>
		/// Gets a list of motifs associated with the style.
		/// </summary>
		/// <returns>A list of motifs associated with the style</returns>
		public ArrayList GetMotifs()
		{
			StringBuilder buffer = new StringBuilder(0x100);
			Ppmusau.MT_GetFirstMotif(buffer, _pointer);
			ArrayList al = new ArrayList();
			al.Add(buffer.ToString());
			while (Ppmusau.MT_GetNextMotif(buffer, _pointer, buffer.ToString()) > 0)
			{
				al.Add(buffer.ToString());
			}
			return al;
		}

		// TODO: equality
	}
}
