using System;
using UnityEngine;

namespace MiniLocale.Serialization
{
	[Serializable]
	public class StringData
	{
		/// <summary>
		/// Localization tag of the string
		/// </summary>
		public string tag;
		
		/// <summary>
		/// Translated text of the string
		/// </summary>
		[Multiline]
		public string text;

		/// <summary>
		/// Localization tag of the string
		/// </summary>
		public string Tag
		{
			get { return tag; }
			set { tag = value; }
		}

		/// <summary>
		/// Translated text of the string
		/// </summary>
		public string Text
		{
			get { return text; }
			set { text = value; }
		}
	}
}
