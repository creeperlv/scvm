namespace scvm.core.dispatchers
{
	public class SimpleDispatcher : IDispatcher
	{
		SCVMMachine machine;
		public void AttachMachine(SCVMMachine machine)
		{
			this.machine = machine;
		}

		public void StartExecute()
		{
			while (true)
			{
				machine.CPU.Processors[0].Execute();
			}
		}

		public void StartExecute(ulong PC)
		{

			machine.CPU.Processors[0].state.PC = PC;
			while (true)
			{
				machine.CPU.Processors[0].Execute();
			}
		}

		public void StartExecute(int ProcessorID)
		{
			while (true)
			{
				machine.CPU.Processors[ProcessorID].Execute();
			}
		}

		public void StartExecute(int ProcessorID, ulong PC)
		{
			machine.CPU.Processors[ProcessorID].state.PC = PC;
			while (true)
			{
				machine.CPU.Processors[ProcessorID].Execute();
			}
		}
	}
}
