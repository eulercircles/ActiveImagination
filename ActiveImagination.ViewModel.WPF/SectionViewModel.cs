using System;
using System.Windows.Input;

using FlintLib.MVVM;

using ActiveImagination.Model;
using System.Windows.Media;
using System.ComponentModel;

namespace ActiveImagination.ViewModel
{
	public class SectionViewModel
	{
		private readonly DialogueViewModel _parent;
		private readonly Section _model;

		public ICommand ChangeFigureCommand { get; private set; }
		public ICommand AddSectionCommand { get; private set; }

		public IBindable<bool> IsNameEditable { get; private set; }
		public IBindable<bool> IsBodyEditable { get; private set; }
		public IBindable<string> FigureName { get; private set; }
		public IBindable<SolidColorBrush> Background { get; private set; }
		public IBindable<string> BodyText { get; private set; }

		internal SectionViewModel(DialogueViewModel parent, Section model, bool isNewFigure)
		{
			_parent = parent;
			_model = model;

			ChangeFigureCommand = new RelayCommand<SetFigureMethod>(ChangeFigure);
			AddSectionCommand = new RelayCommand<SetFigureMethod>(AddSection);

			IsBodyEditable = BindablesFactory.Create(true);
			IsNameEditable = BindablesFactory.Create(isNewFigure);
			FigureName = BindablesFactory.Create(_model.Figure);
			Background = BindablesFactory.Create(_parent.GetFigureColor(_model.Figure));
			BodyText = BindablesFactory.Create(_model.Body);

			FigureName.PropertyChanged += FigureName_PropertyChanged;
			BodyText.PropertyChanged += BodyText_PropertyChanged;
		}

		private void BodyText_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			_parent.CommitTextEdit(_model, BodyText.Value);
		}

		private void FigureName_PropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (IsNameEditable.Value)
			{
				_parent.CommitNameEdit(_model, FigureName.Value);
				IsNameEditable.Value = false;
			}
		}

		private void ChangeFigure(SetFigureMethod method)
		{
			var color = _parent.ChangeFigure(_model, method);

			FigureName.Value = _model.Figure;
			Background.Value = color;
		}

		private void AddSection(SetFigureMethod method)
		{
			IsBodyEditable.Value = false;
			_parent.AddSection(_model, method);
		}

		public void Initialize()
		{
			//Background.Bump();
		}
	}
}
