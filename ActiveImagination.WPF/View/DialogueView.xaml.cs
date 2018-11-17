using System;
using System.Linq;
using System.Windows;

using System.Windows.Input;
using System.Windows.Controls;

using ActiveImagination.ViewModel;

namespace ActiveImagination.View
{
	public partial class DialogueView : UserControl
	{
		private class SectionItem
		{
			internal Grid Root { get; set; }
			internal SectionViewModel ViewModel { get; set; }
			internal TextBox FigureTextBox { get; set; }
			internal TextBox BodyTextBox { get; set; }
		}

		private readonly DialogueViewModel _viewModel;

		public DialogueView()
		{
			InitializeComponent();

			_viewModel = (DialogueViewModel)Resources["viewModel"];
		}

		private void UserControl_Loaded(object sender, EventArgs args)
		{
			_viewModel?.Initialize();
		}

		private void Item_Loaded(object sender, EventArgs args)
		{
			var item = GetItemFromGrid((Grid)sender);

			if (item.ViewModel.IsNameEditable.Value)
			{
				item.FigureTextBox?.Focus();
				item.FigureTextBox.SelectAll();
			}
			else
			{
				item.BodyTextBox.Focus();
			}
		}

		private void FigureTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				var item = GetItemFromGrid((Grid)((TextBox)sender).Parent);
				item.BodyTextBox.Focus();
			}
		}

		private SectionItem GetItemFromGrid(Grid root)
		{
			return new SectionItem()
			{
				Root = root,
				ViewModel = (SectionViewModel)root.DataContext,
				FigureTextBox = (TextBox)root.FindName("figureTextBox"),
				BodyTextBox = (TextBox)root.FindName("bodyTextBox")
			};
		}
	}
}
