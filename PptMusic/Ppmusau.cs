using System;
using System.Text;
using System.Runtime.InteropServices;

namespace PptMusic
{
	/// <summary>
	/// PowerPoint Custom Soundtracks driver
	/// </summary>
	internal class Ppmusau
	{
		const string DLL = @"C:\Program Files\Microsoft Office\Office\ppmusau.dll";

		// (De-)Init

		[DllImport(DLL)]
		public static extern bool MT_LoadMusicEngine();
		
		[DllImport(DLL)]
		public static extern void MT_FreeMusicEngine();

		// Notify sink

		[DllImport(DLL)]
		public static extern void MT_SetNotifySink();

		[DllImport(DLL)]
		public static extern void MT_RestoreNotifySink();

		// Music

		[DllImport(DLL)]
		public static extern IntPtr MT_QueueMusic(IntPtr style, string personality, string band);

		[DllImport(DLL)]
		public static extern void MT_StartMusic(IntPtr style, string personality, string band);

		[DllImport(DLL)]
		public static extern void MT_StopMusic();

		[DllImport(DLL)]
		public static extern bool MT_FlushMusicQueue();

		[DllImport(DLL)]
		public static extern bool MT_MIDIOut(int flag);

		// Categories

		[DllImport(DLL)]
		public static extern short MT_GetFirstCategory(StringBuilder buffer);

		[DllImport(DLL)]
		public static extern short MT_GetNextCategory(StringBuilder buffer, string previous);

		// Styles

		[DllImport(DLL)]
		public static extern short MT_GetFirstStyle(StringBuilder buffer, string category);

		[DllImport(DLL)]
		public static extern short MT_GetNextStyle(StringBuilder buffer, string category, string previous);

		[DllImport(DLL)]
		public static extern short MT_GetStyleName(StringBuilder buffer, string category, string guid);

		[DllImport(DLL)]
		public static extern short MT_GetStyleFilename(StringBuilder buffer, string category, string guid);

		[DllImport(DLL)]
		public static extern IntPtr MT_GetStylePtr(string category, string guid);

		[DllImport(DLL)]
		public static extern void MT_SetStyle(IntPtr style);

		// Personalities

		[DllImport(DLL)]
		public static extern short MT_GetDefaultPersonality(StringBuilder buffer, IntPtr style);

		[DllImport(DLL)]
		public static extern short MT_GetFirstPersonality(StringBuilder buffer, IntPtr style);

		[DllImport(DLL)]
		public static extern short MT_GetNextPersonality(StringBuilder buffer, IntPtr style, string previous);

		[DllImport(DLL)]
		public static extern short MT_GetPersonalityFilename(StringBuilder buffer, string personality);

		[DllImport(DLL)]
		public static extern void MT_SetPersonality(IntPtr style, string personality, string band);

		// Bands

		[DllImport(DLL)]
		public static extern short MT_GetDefaultBand(StringBuilder buffer, IntPtr style);

		[DllImport(DLL)]
		public static extern short MT_GetFirstBand(StringBuilder buffer, IntPtr style);

		[DllImport(DLL)]
		public static extern short MT_GetNextBand(StringBuilder buffer, IntPtr style, string previous);

		[DllImport(DLL)]
		public static extern void MT_SetBand(IntPtr style, string band);

		// Motifs

		[DllImport(DLL)]
		public static extern short MT_GetFirstMotif(StringBuilder buffer, IntPtr style);

		[DllImport(DLL)]
		public static extern short MT_GetNextMotif(StringBuilder buffer, IntPtr style, string previous);

		[DllImport(DLL)]
		public static extern void MT_PlayMotif(IntPtr style, string motif);

		[DllImport(DLL)]
		public static extern void MT_QueueMotif(IntPtr style, string motif);

	}
}
