using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActiveImagination.Model.Tests
{
	[TestClass]
	public class Dialogue_Tester
	{
		[TestMethod]
		public void TestCreateDialogue()
		{
			var dialogue = new Dialogue();
		}

		[TestMethod]
		public void TestAddSection()
		{
			var dialogue = new Dialogue();
			var section1 = dialogue.AddSection(null, SetFigureMethod.Rotate);
			var section2 = dialogue.AddSection(section1, SetFigureMethod.Create);
			var section3 = dialogue.AddSection(section2, SetFigureMethod.Create);
		}

		[TestMethod]
		public void TestChangeFigure()
		{
			var dialogue = new Dialogue();
			var section1 = dialogue.AddSection(null, SetFigureMethod.Rotate);
			var section2 = dialogue.AddSection(section1, SetFigureMethod.Create);
			var section3 = dialogue.AddSection(section2, SetFigureMethod.Create);

			dialogue.ChangeFigure(section1, SetFigureMethod.Rotate);
			dialogue.ChangeFigure(section1, SetFigureMethod.Rotate);
			dialogue.ChangeFigure(section1, SetFigureMethod.Rotate);
			dialogue.ChangeFigure(section1, SetFigureMethod.Rotate);
			dialogue.ChangeFigure(section1, SetFigureMethod.Rotate);
			dialogue.ChangeFigure(section1, SetFigureMethod.Rotate);
			dialogue.ChangeFigure(section1, SetFigureMethod.Rotate);
			dialogue.ChangeFigure(section1, SetFigureMethod.Rotate);
			dialogue.ChangeFigure(section1, SetFigureMethod.Rotate);
		}
	}
}
