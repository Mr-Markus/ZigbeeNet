using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CodeGenerator.Zcl
{
	public class DataTypeMap
	{
		public string DataClass { get; set; }
		public int Id { get; set; }
		public int Length { get; set; }
		public uint Invalid { get; set; }
		public bool Analogue { get; set; }

		public DataTypeMap(string dataClass, int id, int length, bool analogue)
			: this(dataClass, id, length, analogue, 0)
		{
		}

		public DataTypeMap(string dataClass, int id, int length, bool analogue, uint invalid)
		{
			this.DataClass = dataClass;
			this.Id = id;
			this.Length = length;
			this.Invalid = invalid;
			this.Analogue = analogue;
		}
	};
}
