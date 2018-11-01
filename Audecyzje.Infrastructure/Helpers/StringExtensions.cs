using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audecyzje.Infrastructure
{
	public static class StringExtensions
	{
		public static string Left(this string @this, int count)
		{
			if (@this.Length <= count)
			{
				return @this;
			}
			else
			{
				return @this.Substring(0, count);
			}
		}

		public static string Right(this string @this, int count)
		{
			@this = @this.Reverse();
			if (@this.Length <= count)
			{
				return @this;
			}
			else
			{
				return @this.Substring(0, count);
			}
		}

		public static string Reverse(this string text)
		{
			char[] cArray = text.ToCharArray();
			string reverse = String.Empty;
			for (int i = cArray.Length - 1; i > -1; i--)
			{
				reverse += cArray[i];
			}
			return reverse;
		}

		public static int IndexOfFirstLetter(this string text)
		{
			var pos = -1;
			var i = 0;
			while (pos == -1)
			{
				if (!Char.IsDigit(text[i]))
				{
					pos = i;
				}
				i++;
			}
			return pos;
		}

		public static int IndexOfFirstDigit(this string text)
		{
			var pos = -1;
			var i = 0;
			while (pos == -1)
			{
				if (Char.IsDigit(text[i]))
				{
					pos = i;
				}
				i++;
			}
			return pos;
		}

		public static int LevenshteinDistanceTo(this string s, string t)
		{
			int sLength = s.Length;
			int tLength = t.Length;
			int[,] d = new int[sLength + 1, tLength + 1];
			if (sLength == 0)
			{
				return tLength;
			}

			if (tLength == 0)
			{
				return sLength;
			}

			for (int i = 0; i <= sLength; d[i, 0] = i++) ;
			for (int j = 0; j <= tLength; d[0, j] = j++) ;

			for (int i = 1; i <= sLength; i++)
			{
				for (int j = 1; j <= tLength; j++)
				{
					int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
					d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
				}
			}

			return d[sLength, tLength];
		}
	}
}
