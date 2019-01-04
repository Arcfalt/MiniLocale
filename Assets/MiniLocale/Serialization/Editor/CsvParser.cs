using System.IO;
using System.Linq;
using CsvHelper;

namespace MiniLocale.Serialization.Editor
{
	public static class CsvParser
	{
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
