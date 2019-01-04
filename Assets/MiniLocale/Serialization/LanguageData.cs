using UnityEngine;

namespace MiniLocale.Serialization
{
	[CreateAssetMenu(fileName = "Language_", menuName = "Language Data", order = 690)]
	public class LanguageData : ScriptableObject
	{
		public StringData[] strings;
		
#if UNITY_EDITOR
		[ContextMenu("Load From TSV")]
		public void LoadFromTsv()
		{
			var path = UnityEditor.EditorUtility.OpenFilePanelWithFilters("Select Language TSV File", "",
				new[] {"TSV files", "tsv", "All files", "*"});
			if (string.IsNullOrEmpty(path)) return;
			strings = Editor.TsvParser.ParseTsv(path);
		}

		[ContextMenu("Save To TSV")]
		public void SaveToTsv()
		{
			var path = UnityEditor.EditorUtility.SaveFilePanel("Save Language TSV File", "", "", "tsv");
			if (string.IsNullOrEmpty(path)) return;
			Editor.TsvParser.WriteTsv(path, strings);
		}
		
		[ContextMenu("Load From CSV")]
		public void LoadFromCsv()
		{
			var path = UnityEditor.EditorUtility.OpenFilePanelWithFilters("Select Language CSV File", "",
				new[] {"CSV files", "csv", "All files", "*"});
			if (string.IsNullOrEmpty(path)) return;
			strings = Editor.CsvParser.ReadCsv(path);
		}

		[ContextMenu("Save To CSV")]
		public void SaveToCsv()
		{
			var path = UnityEditor.EditorUtility.SaveFilePanel("Save Language CSV File", "", "", "csv");
			if (string.IsNullOrEmpty(path)) return;
			Editor.CsvParser.WriteCsv(path, strings);
		}
#endif
	}
}
