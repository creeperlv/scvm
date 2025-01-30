using System;
using System.Threading;
using System.Threading.Tasks;

namespace scvm.core.dispatchers
{
	public class SimpleDispatcher : IDispatcher
	{
		SCVMMachine machine;
		bool willRun = true;
		public void AttachMachine(SCVMMachine machine)
		{
			this.machine = machine;
		}

		public void Dispose()
		{
		}

		public void StartExecute()
		{
			willRun = true;
			while (willRun)
			{
				machine.CPU.Processors[0].Execute();
			}
		}

		public void StartExecute(ulong PC)
		{

			machine.CPU.Processors[0].state.PC = PC;
			willRun = true;
			while (willRun)
			{
				machine.CPU.Processors[0].Execute();
			}
		}

		public void StartExecute(int ProcessorID)
		{
			willRun = true;
			while (willRun)
			{
				machine.CPU.Processors[ProcessorID].Execute();
			}
		}

		public void StartExecute(int ProcessorID, ulong PC)
		{
			machine.CPU.Processors[ProcessorID].state.PC = PC;
			willRun = true;
			while (willRun)
			{
				machine.CPU.Processors[ProcessorID].Execute();
			}
		}

		public void StopExecute()
		{
			willRun = false;
		}
	}
	public class AsyncDispatcher : IDispatcher
	{
		SCVMMachine machine;
		bool willRun = true;
		public void AttachMachine(SCVMMachine machine)
		{
			this.machine = machine;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public void StartExecute()
		{
			willRun = true;
			Task.Run(() =>
			{
				while (willRun)
				{
					machine.CPU.Processors[0].Dispose();
					machine.CPU.Processors[0].Execute();
				}
			});
		}

		public void StartExecute(ulong PC)
		{
			willRun = true;
			Task.Run(() =>
			{
				machine.CPU.Processors[0].state.PC = PC;
				while (willRun)
				{
					machine.CPU.Processors[0].Execute();
				}
			});
		}

		public void StartExecute(int ProcessorID)
		{
			willRun = true;
			while (willRun)
			{
				machine.CPU.Processors[ProcessorID].Execute();
			}
		}

		public void StartExecute(int ProcessorID, ulong PC)
		{
			machine.CPU.Processors[ProcessorID].state.PC = PC;
			willRun = true;
			while (willRun)
			{
				machine.CPU.Processors[ProcessorID].Execute();
			}
		}

		public void StopExecute()
		{
			willRun = false;
		}
	}
}
