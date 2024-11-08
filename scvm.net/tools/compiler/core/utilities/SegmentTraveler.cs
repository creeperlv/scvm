using LibCLCC.NET.TextProcessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace scvm.tools.compiler.core.utilities
{
	public class SegmentTraveler
	{
		public Segment HEAD;
		public Segment Current;
		public SegmentTraveler(Segment HEAD)
		{
			this.HEAD = HEAD;
			Current = HEAD;
		}
		public bool GoNext()
		{
			if (Current.Next == null)
			{
				return false;
			}
			if (Current.Next.content == "" && Current.Next.Next == null)
			{
				return false;
			}
			Current = Current.Next;
			return true;
		}
		public void Traverse(Action<Segment> action)
		{
			while (true)
			{
				action(Current);
				if (!GoNext())
				{
					return;
				}
			}
		}
	}
}
