using System;
using UnityEngine;

namespace MiniLocale.Serialization
{
	[Serializable]
	public class StringData
	{
		public string tag;
		
		[Multiline]
		public string text;

		public string Tag
		{
			get { return tag; }
			set { tag = value; }
		}

		public string Text
		{
			get { return text; }
			set { text = value; }
		}
	}
}
