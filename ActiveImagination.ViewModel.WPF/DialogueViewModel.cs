using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using FLib.Commands;

using ActiveImagination.Model;

using static FLib.Mathematics.Functions;
using static FLib.Presentation.ColorUtilities;

namespace ActiveImagination.ViewModel
{
	public class DialogueViewModel
	{
		private readonly double colorSaturation = 0.1958d;
		private readonly double colorValue = 0.9136d;
		private readonly double hueIncrement = 161.8d;

		private double _currentHue = new Random((int)DateTime.Now.Ticks).Next(0, 360);

		private readonly Dialogue _dialogue;

		private readonly Dictionary<string, SolidColorBrush> _figureColors;

		private readonly ObservableCollection<SectionViewModel> _sections;
		public ReadOnlyObservableCollection<SectionViewModel> Sections { get; }

		public ICommand SaveCommand { get; private set; }

		public DialogueViewModel()
		{
			_dialogue = new Dialogue();

			_figureColors = new Dictionary<string, SolidColorBrush>();

			_sections = new ObservableCollection<SectionViewModel>();
			Sections = new ReadOnlyObservableCollection<SectionViewModel>(_sections);

			SaveCommand = new RelayCommand(Save);
		}

		private void Save()
		{
			_dialogue.Save();
		}

		protected void ConstructBindables()
		{
		}

		internal SolidColorBrush ChangeFigure(Section currentSection, SetFigureMethod method)
		{
			_dialogue.ChangeFigure(currentSection, method);
			return RegisterFigureColor(currentSection.Figure);
		}

		private SolidColorBrush RegisterFigureColor(string name)
		{
			if (!_figureColors.ContainsKey(name))
			{ _figureColors.Add(name, GenerateColor()); }
			return _figureColors[name];
		}

		internal SolidColorBrush GetFigureColor(string name)
		{
			return _figureColors[name];
		}

		internal void AddSection(Section previousSection, SetFigureMethod method)
		{
			try
			{
				var newSection = _dialogue.AddSection(previousSection, method);
				if (newSection != null)
				{
					RegisterFigureColor(newSection.Figure);
					_sections.Add(new SectionViewModel(this, newSection, method == SetFigureMethod.Create));
				}
			}
			catch (Exception exception)
			{
				var ex = exception.ToString();
			}
		}

		internal void CommitNameEdit(SectionViewModel section, string newName)
		{
			if (_figureColors.ContainsKey(newName))
			{
				var color = _figureColors[newName];
				section.Background.Value = color;
			}
			else
			{
				if (_figureColors.ContainsKey(section.Model.Figure))
				{
					var color = _figureColors[section.Model.Figure];
					_figureColors.Remove(section.Model.Figure);
					_figureColors.Add(newName, color);
				}
			}
						

			_dialogue.CommitNameEdit(section.Model, newName);
		}

		internal void CommitTextEdit(Section section, string text)
		{
			_dialogue.SetBodyText(section, text);
		}

		public void Initialize()
		{
			if (_sections.Count == 0)
			{
				AddSection(null, SetFigureMethod.Rotate);
			}
		}

		private SolidColorBrush GenerateColor()
		{
			_currentHue = DegreeClamp(_currentHue + hueIncrement);
			HsvToRgb(_currentHue, colorSaturation, colorValue, out byte red, out byte green, out byte blue);
			return new SolidColorBrush(Color.FromArgb(255, red, green, blue));
		}
	}
}
