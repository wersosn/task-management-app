using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
	public class Task
	{
		public string Title { get; set; }
		public string Description { get; set; }
		// public Tag Tag { get; set; }
		//public Category Category { get; set; }
		public DateTime DueDate { get; set; }
		public string Priority { get; set; }

		// do poprawienia po dodaniu Tag i Category
		public Task(string title, string description, DateTime dueDate, string priority)
		{
			Title = title;
			Description = description;
			DueDate = dueDate;
			Priority = priority;
		}
	}
}
