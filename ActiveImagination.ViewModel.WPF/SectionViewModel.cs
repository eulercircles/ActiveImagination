using System;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;

using FLib.MVVM;
using FLib.Commands;

using ActiveImagination.Model;

namespace ActiveImagination.ViewModel
{
	public class SectionViewModel
	{
		private readonly DialogueViewModel _parent;
		internal Section Model { get; }

		public ICommand ChangeFigureCommand { get; private set; }
		public ICommand AddSectionCommand { get; private set; }

		public Bindable<bool> IsNameEditable { get; private set; }
		public Bindable<bool> IsBodyEditable { get; private set; }
		public Bindable<string> FigureName { get; private set; }
		public Bindable<SolidColorBrush> Background { get; private set; }
		public Bindable<string> BodyText { get; private set; }

		internal SectionViewModel(DialogueViewModel parent, Section model, bool isNewFigure)
		{
			_parent = parent;
			Model = model;

			ChangeFigureCommand = new RelayCommand<SetFigureMethod>(ChangeFigure);
			AddSectionCommand = new RelayCommand<SetFigureMethod>(AddSection);

			IsBodyEditable = new Bindable<bool>(true);
			IsNameEditable = new Bindable<bool>(isNewFigure);
			FigureName = new Bindable<string>(Model.Figure);
			Background = new Bindable<SolidColorBrush>(_parent.GetFigureColor(Model.Figure));
			BodyText = new Bindable<string>(Model.Body);

			FigureName.PropertyChanged += FigureName_PropertyChanged;
			BodyText.PropertyChanged += BodyText_PropertyChanged;
		}

		private void BodyText_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			_parent.CommitTextEdit(Model, BodyText.Value);
		}

		private void FigureName_PropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			_parent.CommitNameEdit(this, FigureName.Value);
		}

		private void ChangeFigure(SetFigureMethod method)
		{
			var color = _parent.ChangeFigure(Model, method);

			FigureName.Value = Model.Figure;
			Background.Value = color;
		}

		private void AddSection(SetFigureMethod method)
		{
			IsBodyEditable.Value = false;
			_parent.AddSection(Model, method);
		}

		public void Initialize()
		{
			//Background.Bump();
		}
	}
}
