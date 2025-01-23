using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.State
{
	public interface ITaskState
	{
		//jak są przechowywane polecenia?
		///co powinno być przekazywane?

		void NotStarted();
		void InProgress();
		void Completed();

	}


}
