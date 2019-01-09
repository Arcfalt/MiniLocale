using System.IO;
using System.Linq;
using CsvHelper;

namespace MiniLocale.Serialization.Editor
{
	/// <summary>
	/// CSV file loading and saving for string data arrays
	/// Utilizes CSVHelper
	/// </summary>
	public static class CsvParser
	{
		/// <summary>
		/// Save an array of string data to a csv file at a given path
		/// Will overwrite if file already exists
		/// </summary>
		/// <param name="path">Path to save the csv file</param>
		/// <param name="data">String data to save to the file</param>
		public static void WriteCsv(string path, StringData[] data)
		{
			using (var writer = new StreamWriter(path))
			{
				using (var csv = new CsvWriter(writer))
				{
					csv.WriteRecords(data);
				}
			}
		}

		/// <summary>
		/// Load an array of string data from a given csv file
		/// </summary>
		/// <param name="path">Path to the csv file to be loaded</param>
		/// <returns>String data from the csv file</returns>
		public static StringData[] ReadCsv(string path)
		{
			StringData[] array;
			using (var reader = new StreamReader(path))
			{
				using (var csv = new CsvReader(reader))
				{
					var records = csv.GetRecords<StringData>();
					array = records.ToArray();
				}
			}
			return array;
		}
	}
}
