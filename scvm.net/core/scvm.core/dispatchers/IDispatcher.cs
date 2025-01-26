using System;
using System.Collections.Generic;
using System.Text;

namespace scvm.core.dispatchers
{
	public interface IDispatcher
	{
		void AttachMachine(SCVMMachine machine);
		void StartExecute();
		void StartExecute(int ProcessorID);
		void StartExecute(int ProcessorID, ulong PC);
		void StartExecute(ulong PC);
	}
}
