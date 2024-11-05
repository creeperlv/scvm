using scvm.core;
using System.Diagnostics;

namespace DirectVM.core;
public class BSDStyleInterruptHandlerSetup
{
	public static void SetupInterruptHandlers(SCVMProcessor processor)
	{
		processor.InterruptHandlers.Add(SyscallNames.exit, BSDStyleSyscalls.exit);
		processor.InterruptHandlers.Add(SyscallNames.getpid, BSDStyleSyscalls.getpid);
	}
}
