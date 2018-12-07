using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FindingWordsDistance
{
	public static class Executor
	{
		public static void Execute(string fileName, string[] words)
		{
			var wordsDictionary = words.ToDictionary(w => w, a => new List<int>() as IList<int>);
			FindWords(fileName, wordsDictionary);
			var distances = GetDistances(wordsDictionary);
			Console.Write($"min distance: {distances.min}{Environment.NewLine}max distance:{distances.max}");
		}

		private static void FindWords(string fileName, IDictionary<string, IList<int>> wordsDictionary)
		{
			int position = 0;
			using (var sr = new StreamReader(fileName))
			{
				while (!sr.EndOfStream)
				{
					var lineWords = sr.ReadLine()?.Split(' ')
						.Where(w => w.Length > 0).ToList();

					if (lineWords == null || !lineWords.Any())
					{
						continue;
					}

					for (var i = 0; i != lineWords.Count; ++i)
					{
						foreach (var word in wordsDictionary)
						{
							if (word.Key.Equals(lineWords[i]))
							{
								word.Value.Add(position + i);
							}
						}
					}

					position += lineWords.Count;
				}
			}
		}

		private static (int max, int min) GetDistances(IDictionary<string, IList<int>> wordsDictionary)
		{
			var distances = (max:0, min:int.MaxValue);
			var positions = wordsDictionary.Values;
			foreach (var firstWordPosition in positions.First())
			{
				foreach (var secondWordPosition in positions.Last())
				{
					var distance = Math.Abs(firstWordPosition - secondWordPosition) - 1;
					if (distance > distances.max)
					{
						distances.max = distance;
					}

					if (distance < distances.min)
					{
						distances.min = distance;
					}
				}
			}
			return distances;
		}

		
	}
}