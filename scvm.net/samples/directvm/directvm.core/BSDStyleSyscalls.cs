using scvm.core;

namespace DirectVM.core;

public class BSDStyleSyscalls
{
	public static void getpid(SCVMProcessor processor)
	{
		processor.Register.SetData(CallingConvention.A0, Environment.ProcessId);
	}

	public static void exit(SCVMProcessor processor)
	{
		Environment.Exit(processor.Register.GetData<int>(CallingConvention.A0));
	}
}
