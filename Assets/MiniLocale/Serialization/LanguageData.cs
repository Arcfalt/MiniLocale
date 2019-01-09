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
		
		/// <summary>
		/// Load strings from a TSV file
		/// NOTE: TSV does not support line breaks correctly!
		/// </summary>
		[ContextMenu("Load From TSV")]
		public void LoadFromTsv()
		{
			var path = UnityEditor.EditorUtility.OpenFilePanelWithFilters("Select Language TSV File", "",
				new[] {"TSV files", "tsv", "All files", "*"});
			if (string.IsNullOrEmpty(path)) return;
			strings = Editor.TsvParser.ParseTsv(path);
		}

		/// <summary>
		/// Save the current strings to a TSV file
		/// NOTE: TSV does not support line breaks correctly!
		/// </summary>
		[ContextMenu("Save To TSV")]
		public void SaveToTsv()
		{
			var path = UnityEditor.EditorUtility.SaveFilePanel("Save Language TSV File", "", "", "tsv");
			if (string.IsNullOrEmpty(path)) return;
			Editor.TsvParser.WriteTsv(path, strings);
		}
#endif
	}
}
