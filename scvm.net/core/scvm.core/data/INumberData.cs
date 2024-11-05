using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace scvm.core.data
{
	public interface INumbericData<T> where T : unmanaged
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		T Add(T R);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		T Sub(T R);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		T Mul(T R);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		T Div(T R);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		SCVMSimpleResult<T> AddOF(T R);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		SCVMSimpleResult<T> SubOF(T R);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		SCVMSimpleResult<T> DivOF(T R);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		SCVMSimpleResult<T> MulOF(T R);
	}
	public struct SCVMSimpleResult<T> where T : unmanaged
	{
		public bool IsSuccess;
		public T Value;

		public SCVMSimpleResult(bool isSuccess, T value)
		{
			IsSuccess = isSuccess;
			Value = value;
		}
	}
}
