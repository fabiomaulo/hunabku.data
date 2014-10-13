using System;
using System.Data;

namespace Hunabku.Data.FieldMappers
{
	public class Int32FieldType : NotNullableFieldType<int, int>
	{
		public override int Get(IDataReader dr, string name)
		{
			return NullSafeGet(dr, name);
		}

		public override void Set(IDbCommand cmd, string name, int value)
		{
			NullSafeSet(cmd, name, value);
		}

		protected override int Get(IDataReader dr, int index)
		{
			return Convert.ToInt32(dr[index]);
		}

		protected override void Set(IDbCommand cmd, int index, int value)
		{
			var parameter = (IDbDataParameter)cmd.Parameters[index];
			parameter.Value = value;
		}
	}

}