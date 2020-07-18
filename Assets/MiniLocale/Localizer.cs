using System.Collections.Generic;
using MiniLocale.Serialization;
using UnityEngine;

namespace MiniLocale
{
	/// <summary>
	/// Basic localizer, loads strings from a LanguageData source, and gets translated strings by tag
	/// </summary>
	public static class Localizer
	{
		private const string NO_STRING_FOUND = "";
		
		/// <summary>
		/// Currently loaded dictionary of translated strings
		/// </summary>
		private static Dictionary<string, string> strings = new Dictionary<string, string>(0);

		/// <summary>
		/// Simple delegate containing no data for localizer events
		/// </summary>
		public delegate void LanguageEvent();

		/// <summary>
		/// Event raised if the language source is changed
		/// Intended for components to auto-update their texts upon changing languages
		/// </summary>
		public static event LanguageEvent SourceChanged;

		/// <summary>
		/// Get the translated string from a given tag
		/// </summary>
		/// <param name="tag">Input tag to find the associated string for</param>
		/// <returns>The translated string, null if the tag does not exist in loaded source</returns>
		public static string GetString(string tag)
		{
			return strings.TryGetValue(tag, out var found) ? found : NO_STRING_FOUND;
		}

		/// <summary>
		/// Load a new language source from a language data scriptable object
		/// </summary>
		/// <param name="data">The language data to load</param>
		public static void LoadSource(LanguageData data)
		{
			// Make sure the language data inputted actually exists
			if (data == null)
			{
				Debug.LogError("Null language input to localizer!");
				return;
			}
			
			// Transform the strings into the localizer dictionary
			strings = new Dictionary<string, string>(data.strings.Length);
			foreach (var s in data.strings)
			{
				strings.Add(s.tag, s.text);
			}

			// Raise the event for changing the language source
			SourceChanged?.Invoke();
		}
	}
}
