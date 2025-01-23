using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Model
{
	public interface ITaskState
	{
		//jak są przechowywane polecenia?
		///co powinno być przekazywane?
		string GetStateName();
		void Start(Task task);

		string ButtonLabel { get; }
		bool IsButtonEnabled { get; }

	}

	public class NotStarted : ITaskState
	{
		public string GetStateName()
		{
			return "Not started";
		}
		public void Start(Task task)
		{
			task.ChangeState(new InProgress());
		}

		public string ButtonLabel => "Set as In Progress";
		public bool IsButtonEnabled => true;

	}

	public class InProgress : ITaskState
	{
		public string GetStateName()
		{
			return "In progress";
		}
		public void Start(Task task)
		{
			task.ChangeState(new Completed());
		}

		public string ButtonLabel => "Set as Completed";
		public bool IsButtonEnabled => true;
	}

	public class Completed : ITaskState
	{
		public string GetStateName()
		{
			return "Completed";
		}
		public void Start(Task task)
		{
			// nie można bo nie ma innego stanu
		}

		public string ButtonLabel => "Completed";
		public bool IsButtonEnabled => false;
	}

	
}