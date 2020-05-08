using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Estruct
{
	public class CompressionLZW
	{
		public static List<int> Compresion(string uncompressed)
		{
		
			int TamañoDiccionario = 256;

			
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			Dictionary<string, int> dictionaryP = new Dictionary<string, int>();
			for (int i = 0; i < 256; i++)
			{
				dictionary["" + (char)i] = i;
			}

			string w = "";
			List<int> result = new List<int>();

			foreach (char c in uncompressed.ToCharArray())
			{
				string wc = w + c;
				if (dictionary.ContainsKey(wc))
				{
					w = wc;
				}
				
				else
				{
					result.Add(dictionary[w]);
					dictionary[wc] = TamañoDiccionario++;
					w = "" + c;
				}
			}

			if (!w.Equals(""))
			{
				result.Add(dictionary[w]);
			}

			int x = 1;
			string res = "";
			int[] vec = result.ToArray();
			for (int i = 0; i < result.Count; i++)
			{

				res = vec[i].ToString();
				dictionaryP[res] = x++;
			}



			return result;
		}

		public static string Descompresion(List<int> compressed)
		{
		
			int dictSize = 256;
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			for (int i = 0; i < 256; i++)
			{
				dictionary[i] = "" + (char)i;
			}

			string w = "" + (char)(int)compressed.ElementAt(0);
			compressed.RemoveAt(0);
			StringBuilder result = new StringBuilder(w);
			foreach (int k in compressed)
			{
				string entry;
				if (dictionary.ContainsKey(k))
				{
					entry = dictionary[k];
				}
				else if (k == dictSize)
				{
					entry = w + w[0];
				}
				else
				{
					throw new System.ArgumentException("Bad compressed k: " + k);
				}

				result.Append(entry);

				dictionary[dictSize++] = w + entry[0];

				w = entry;
			}
			return result.ToString();
		}


	}
}
