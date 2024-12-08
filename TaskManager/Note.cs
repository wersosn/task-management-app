using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{

	public class Note
	{
		public string Title { get; set; }
		public string Description { get; set; }
		// public Tag Tag { get; set; }
		//public Category Category { get; set; }

		// do poprawienia po dodaniu Tag i Category
		public Note(string title, string description)
		{
			Title = title;
			Description = description;
		}
	}
}
