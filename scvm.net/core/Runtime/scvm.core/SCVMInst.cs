using System;

namespace scvm.core
{
	public static class SCVMInst
	{
		public static readonly Version InstructionVersion = new Version(1, 0, 0, 0);

		/*
		 * Legend
		 * $L - Left Hand Side Operand
		 * $R - Right Hand Side Operand
		 * $T - Target Register
		 * $S - Source Register
		 */

		/*
		 * Register Value Range: 0x00 - 0xFF
		 * Flag Value Range: 0x00 - 0xFF
		 * 
		 * Machine State
		 *	0:ulong - Kernel Base Address
		 *	1:byte - Privilege Level
		 * 
		 * Interrupt: 0x0000 - 0xFFFF
		 */

		public const ushort NOP = 0x0000;
		//ADD <Type> $L $R $T
		public const ushort ADD = 0x0001;
		public const ushort SUB = 0x0002;
		public const ushort MUL = 0x0003;
		public const ushort DIV = 0x0004;
		public const ushort OFC_ADD = 0x0009;
		public const ushort OFC_SUB = 0x000A;
		public const ushort OFC_MUL = 0x000B;
		public const ushort OFC_DIV = 0x000C;
		//SET $T [VALUE]
		//Assembly: Set $reg <type> <value>
		public const ushort SET = 0x0005;
		//CVT $L $T <SourceType> <TargetType>
		public const ushort CVT = 0x0006;
		//Single Calc:
		//SCALC $S $T <OP>
		public const ushort SCALC = 0x0007;
		//DCALC Calc:
		//SCALC $L $R $T <OP>
		public const ushort DCALC = 0x0008;
		//Save Register
		//SR [IsRegister] $Register $PTR <length/$length>
		public const ushort SR = 0x000D;
		public const ushort LR = 0x000E;
		public const ushort SH = 0x000F;

		//LG <Type> $T $L $R
		public const ushort LG = 0x0020;

		//JMP [IsRegister:0|1] <($)TargetPC>
		public const ushort JMP = 0x0010;
		//JF [IsRegister:0|1] <($)TargetPC> <register>
		//Jump If Non Zero
		public const ushort JF = 0x0011;
		//JFF [IsRegister:0|1] <($)TargetPC> <TargetFlag>
		// Jump If Flag Non Zero
		public const ushort JFF = 0x0012;
		//CMP <OP> <Type> $L $R $T
		public const ushort CMP = 0x0013;
		//Copy
		//CP $src_ptr $tgt_ptr $len
		public const ushort CP = 0x0014;
		//Invoke a syscall/interrupt.
		//SYSCALL <Type> <Value>
		public const ushort SYSCALL = 0x0015;
		//System Return
		//SYSRET $RET
		public const ushort SYSRET = 0x0016;
		//Reset Flag
		//RF FlagID
		public const ushort RF = 0x0017;
		//System Register Write
		//SYSREGW <id> $S
		public const ushort SYSREGW = 0xF000;
		//System Register Read
		//SYSREGR <id> $T
		public const ushort SYSREGR = 0xF001;
		//Set Interrupt
		//SETINT <Machine|Software> <Interrupt ID> $Configuration
		//Ptr is used to store registers from caller.
		//Configuration:
		//[ReturnValueRegister] [ReturnValueLength] [$TargetPC_InKernel] [$StateStoragePtr] [$RetV]
		//StateStorage Ptr will write:
		//Ptr: PC from caller
		//Ptr: PageTable Ptr
		//struct MStat: Machine State
		//byte: Flags
		public const ushort SETINT = 0xF002;
		public const ushort CLRINT = 0xF003;
		//Set Privilege
		public const ushort SETPRI = 0xF004;
		// Jump and restore registers
		//JMPWRST $StateStoragePtr
		public const ushort JMPWRST = 0xF005;
		//Jump and restore using registers
		//JMPWLRST $PC $Page_Table_Ptr $MStat_Ptr $Flags
		public const ushort JMPURST = 0xF006;
		//Start A Core With Address
		//CSTART $PC_Ptr
		public const ushort CSTART = 0xF007;
		//Get system info
		//GETSYS $T INFO_ID
		public const ushort GETSYS = 0x0030;
		//Read From Port
		//Pri. 1
		//in $reg port/$port length/$length
		//0038 01 30 3A 40
		public const ushort IN = 0x0038;
		//Write To Port
		//Pri. 1
		//out $reg port/$port length/$length
		public const ushort OUT = 0x0039;
		//Map Memory for write
		//OMAP $reg $length
		public const ushort OMAP = 0x003A;
		//Map Memory for read
		//IMAP $reg $length
		public const ushort IMAP = 0x003B;
		//Put the machine into halt state (Or call OnHalt callback in VM.)
		public const ushort HALT = 0xF100;

	}
	public enum SCVMCmpOps : byte
	{
		LT = 0, GT = 1, GE = 2, EQ = 3, LE = 4, NE = 5,
	}
}
