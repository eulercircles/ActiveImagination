using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlintLib.MVVM;

namespace ActiveImagination.ViewModel
{
	internal class CharacterScrollMediatorArgs : MediatorArgs
	{


		internal CharacterScrollMediatorArgs(object sender, SectionViewModel lineViewModel) : base(sender)
		{
		}
	}
}
