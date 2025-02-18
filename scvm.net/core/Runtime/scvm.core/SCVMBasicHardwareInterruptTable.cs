namespace scvm.core
{
	public static class SCVMBasicHardwareInterruptTable
	{
		public const ushort DivError = 0x0000;
		public const ushort Debug = 0x0001;
		public const ushort PageFault = 0x0002;
		public const ushort InvOpC = 0x0003;
		public const ushort InvalidPage = 0x0004;
		public const ushort ICTimer = 0x0005;
		public const ushort ControlProtect = 0x0006;
	}
}
