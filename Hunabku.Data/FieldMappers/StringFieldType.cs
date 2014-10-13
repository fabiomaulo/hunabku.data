using System;
using System.Data;

namespace Hunabku.Data.FieldMappers
{
	public class StringFieldType : NullableFieldType<string, string>
	{
		public override string Get(IDataReader dr, string name)
		{
			return NullSafeGet(dr, name);
		}

		public override void Set(IDbCommand cmd, string name, string value)
		{
			NullSafeSet(cmd, name, value);
		}

		protected override string Get(IDataReader dr, int index)
		{
			return Convert.ToString(dr[index]);
		}

		protected override void Set(IDbCommand cmd, int index, string value)
		{
			// NOTE: Does not check the length of the field
			var parameter = (IDbDataParameter)cmd.Parameters[index];
			parameter.Value = value;
		}
	}
}