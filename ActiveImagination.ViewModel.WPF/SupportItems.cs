using System;
using System.Linq;

using static FLib.Common.EnumUtilities;

using ActiveImagination.Model;

namespace ActiveImagination.ViewModel
{
	internal static class Extensions
	{
		internal static SetFigureMethod ToSetFigureMethod(this string value)
		{
			return GetEnumDescriptions<SetFigureMethod>().First(d => d.Key == value).Value;
		}
	}
}
