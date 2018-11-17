using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using static System.Environment;

using static ActiveImagination.Model.Properties.InternalResources;

namespace ActiveImagination.Model
{
	public class Dialogue
	{
		private readonly StringBuilder _stringBuilder = new StringBuilder();
		private readonly List<string> _figures = new List<string>();
		private readonly List<Section> _sections = new List<Section>();

		private readonly string _filePath;

		public Dialogue()
		{
			_filePath = GenerateFilePath();
			_figures.Add(FigureName_Ego);
		}

		public Section AddSection(Section previousSection, SetFigureMethod method)
		{
			var figure = GetFigure(method, previousSection);

			var section = new Section() { Body = string.Empty, Figure = figure };
			_sections.Add(section);

			return section;
		}

		public void ChangeFigure(Section currentSection, SetFigureMethod method)
		{
			var figure = GetFigure(method, currentSection);
			currentSection.Figure = figure;
		}

		public void CommitNameEdit(Section section, string newName)
		{
			var index = _figures.IndexOf(section.Figure);
			_figures[index] = newName;
			section.Figure = newName;
		}

		public void SetBodyText(Section section, string text)
		{
			section.Body = text;
		}

		private string GetFigure(SetFigureMethod method, Section section = null)
		{
			string result = null;
			switch (method)
			{
				case SetFigureMethod.Rotate:
					result = (section != null) ? RotateFigure(section.Figure) : _figures[0];
					break;
				case SetFigureMethod.Create:
					var num = 1;
					while (_figures.Contains($"{FigureName_Other} {num}")) { num++; }
					result = $"{FigureName_Other} {num}";
					_figures.Add(result);
					break;
			}
			return result;
		}

		private string RotateFigure(string name)
		{
			Debug.Assert(!string.IsNullOrWhiteSpace(name));
			if (string.IsNullOrWhiteSpace(name)) { return null; }
			if (!_figures.Contains(name)) { return null; }

			var index = _figures.IndexOf(name);

			if (index == (_figures.Count - 1)) { index = 0; }
			else { index++; }

			return _figures[index];
		}

		private string GenerateFilePath()
		{
			var directory = Path.Combine(Directory.GetCurrentDirectory(), "Dialogs");

			if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }

			var dateString = DateTime.Today.ToString("yyyyMMdd");
			var num = 1;
			while (File.Exists(Path.Combine(directory, $"{dateString}{num.ToString().PadLeft(2, '0')}.txt")))
			{
				num++;
			}
			return Path.Combine(directory, $"{dateString}{num.ToString().PadLeft(2, '0')}.txt");
		}

		public void Save()
		{
			var text = GetDialogLines();
			File.WriteAllText(_filePath, text);
		}

		private string GetDialogLines()
		{
			_stringBuilder.Clear();

			foreach (var section in _sections)
			{
				if (!string.IsNullOrWhiteSpace(section.Body))
				{
					_stringBuilder.AppendLine($"{section.Figure.ToUpper()}: {section.Body}{NewLine}");
				}
			}

			return _stringBuilder.ToString();
		}
	}
}
