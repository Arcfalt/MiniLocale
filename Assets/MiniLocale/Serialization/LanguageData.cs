using System.Collections.Generic;
using UnityEngine;

namespace MiniLocale.Serialization
{
	/// <summary>
	/// Language data container that holds an array of translated strings
	/// Saved via unity serialization as a scriptable object, also allowing editor functionality for modification
	/// </summary>
	[CreateAssetMenu(fileName = "Language_", menuName = "Language Data", order = 690)]
	public class LanguageData : ScriptableObject
	{
		/// <summary>
		/// Translated string database for this language
		/// </summary>
		public StringData[] strings;
		
#if UNITY_EDITOR
		/// <summary>
		/// Load strings from a CSV file
		/// </summary>
		[ContextMenu("Load From CSV")]
		public void LoadFromCsv()
		{
			var path = UnityEditor.EditorUtility.OpenFilePanelWithFilters("Select Language CSV File", "",
				new[] {"CSV files", "csv", "All files", "*"});
			if (string.IsNullOrEmpty(path)) return;
			strings = Editor.CsvParser.ReadCsv(path);
		}

		/// <summary>
		/// Save the current strings to a CSV file
		/// </summary>
		[ContextMenu("Save To CSV")]
		public void SaveToCsv()
		{
			var path = UnityEditor.EditorUtility.SaveFilePanel("Save Language CSV File", "", "", "csv");
			if (string.IsNullOrEmpty(path)) return;
			Editor.CsvParser.WriteCsv(path, strings);
		}

		[ContextMenu("Strip Duplicates")]
		public void StripDuplicates()
		{
			var exists = new List<string>(strings.Length);
			var s = new List<StringData>(strings);
			for (int i = 0; i < s.Count; i++)
			{
				if (string.IsNullOrEmpty(s[i].tag)) continue;
				if (!exists.Contains(s[i].tag))
				{
					exists.Add(s[i].tag);
					continue;
				}
				s.RemoveAt(i);
				i--;
			}
			strings = s.ToArray();
		}
#endif
	}
}
