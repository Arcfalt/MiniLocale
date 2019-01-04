using System.Collections.Generic;
using System.IO;

namespace MiniLocale.Serialization.Editor
{
	/// <summary>
	/// Basic language TSV sheet parser
	/// Assumes sheet is formatted (in order):
	///   tag  text  plurality
	/// Tag being the string id used to get texts
	/// Text being the translated text
	/// Plurality being a plurality index of the string if required
	/// </summary>
	public static class TsvParser
	{
		/// <summary>
		/// Deliminator of the TSV (Tab)
		/// </summary>
		private const char _DELIMINATOR = '\t';

		/// <summary>
		/// Parse a TSV at given path for its localized string data
		/// </summary>
		/// <param name="path">Path of the TSV to parse</param>
		/// <returns>Array of localized string data</returns>
		public static StringData[] ParseTsv(string path)
		{
			var lines = File.ReadAllLines(path);
			return ParseLines(lines);
		}

		/// <summary>
		/// Parse all of the lines from a TSV string container
		/// </summary>
		/// <param name="lines">The lines from TSV</param>
		/// <returns>Array of string data containers for the localized strings</returns>
		public static StringData[] ParseLines(string[] lines)
		{
			var dataList = new List<StringData>(lines.Length);
			foreach (var line in lines)
			{
				var data = ParseLine(line);
				if (data == null) continue;
				dataList.Add(data);
			}
			return dataList.ToArray();
		}

		/// <summary>
		/// Parse a single line from a language's TSV string container
		/// </summary>
		/// <param name="line">Single line from TSV</param>
		/// <returns>String data container for a single localized string</returns>
		public static StringData ParseLine(string line)
		{
			if (string.IsNullOrEmpty(line)) return null;
			
			// Split up the input line and ensure it has enough parts to be valid data
			var strings = line.Split(_DELIMINATOR);
			if (strings.Length < 2) return null;
			
			// Create a new data container and fill it with the essential data
			StringData data = new StringData {Tag = strings[0], Text = strings[1]};
			return data;
		}

		/// <summary>
		/// Write an array of string data containers to a TSV at the given path
		/// </summary>
		/// <param name="path">Path to write the TSV to</param>
		/// <param name="data">String data containers to write into the TSV</param>
		/// <returns>If the data was valid to write</returns>
		public static bool WriteTsv(string path, StringData[] data)
		{
			if (data == null) return false;
			File.WriteAllLines(path, ToLines(data));
			return true;
		}

		/// <summary>
		/// Convert an array of string data containers into the lines of a TSV
		/// </summary>
		/// <param name="data">String data container array to convert</param>
		/// <returns>Lines ready to be written to TSV</returns>
		public static string[] ToLines(StringData[] data)
		{
			var lineList = new List<string>(data.Length);
			foreach (var lineData in data)
			{
				var line = ToLine(lineData);
				if (string.IsNullOrEmpty(line)) continue;
				lineList.Add(line);
			}
			return lineList.ToArray();
		}

		/// <summary>
		/// Convert a string data container into a TSV line
		/// </summary>
		/// <param name="data">String data container to convert</param>
		/// <returns>TSV valid line to be written to language TSV container</returns>
		public static string ToLine(StringData data)
		{
			// Ensure the data exists and has a valid tag format, and its text isn't null
			if (data == null || string.IsNullOrEmpty(data.Tag) || data.Text == null) return null;
			
			// Write the data into a single string line with a tab separator
			return $"{data.Tag}\t{data.Text}";
		}
	}
}