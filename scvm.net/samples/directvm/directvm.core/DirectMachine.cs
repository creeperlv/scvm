using scvm.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectVM.core
{
	public class DirectMachine
	{
		SCVMMachine machine = new SCVMMachine();
		public DirectMachine()
		{
			machine.MMU = new RelayedMemoryManagementUnit() { machine = machine };
		}
	}
}
