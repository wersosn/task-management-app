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

		void Start();
		void Complete();

	}

	public class NotStarted : ITaskState
	{
		public void Start()
		{

		}

		public void Complete()
		{

		}
	}

	public class InProgress : ITaskState
	{
		public void Start()
		{

		}

		public void Complete()
		{

		}
	}

	public class Completed : ITaskState
	{
		public void Start()
		{

		}

		public void Complete()
		{

		}
	}
}
