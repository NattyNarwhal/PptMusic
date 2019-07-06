using System;
using System.Text;

namespace PptMusic
{
	/// <summary>
	/// The personality that a style can take on.
	/// </summary>
	public class Personality
	{
		Style _style;
		string _name, _filename;

		/// <summary>
		/// The style associated with this personality.
		/// </summary>
		public Style Style
		{
			get
			{
				return _style;
			}
		}

		/// <summary>
		/// The name of the personality.
		/// </summary>
		public string Name
		{
			get 
			{
				return _name;
			}
		}

		/// <summary>
		/// A file containing the personality's information.
		/// </summary>
		public string Filename
		{
			get 
			{
				return _filename;
			}
		}

		internal Personality(Style style, string name)
		{
			_name = name;
			_style = style;

			StringBuilder buffer = new StringBuilder(0x100);

			Ppmusau.MT_GetPersonalityFilename(buffer, name);
			_filename = buffer.ToString();
		}

		/// <summary>
		/// Gets a string representing the personality.
		/// </summary>
		/// <returns>The name of the personality.</returns>
		public override string ToString()
		{
			return Name;
		}

		// C# 1.1 equality cruft

		public static bool operator ==(Personality left, Personality right)
		{
			if ((object)left == null || (object)right == null)
				return false;
			else
				return left.Name == right.Name;
		}

		public static bool operator !=(Personality left, Personality right)
		{
			if ((object)left == null || (object)right == null)
				return false;
			else
				return left.Name != right.Name;
		}

		public override bool Equals(object obj)
		{
			if (obj is Personality)
				return ((Personality)obj) == this;
			else
				return false;
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode ();
		}

	}
}
