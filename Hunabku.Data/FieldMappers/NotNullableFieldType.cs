using System;
using System.Data;

namespace Hunabku.Data.FieldMappers
{
	public abstract class NotNullableFieldType<TField, TProperty> : AbstractFieldType<TField, TProperty> where TProperty : struct
	{
		protected abstract TProperty Get(IDataReader dr, int index);
		protected abstract void Set(IDbCommand cmd, int index, TField value);

		protected override TProperty NullSafeGet(IDataReader dr, string name)
		{
			int index;
			try
			{
				index = dr.GetOrdinal(name);
			}
			catch (IndexOutOfRangeException e)
			{
				throw new IndexOutOfRangeException(string.Format("No fue posible ver el valor del campo '{0}'", name), e);
			}

			if (dr.IsDBNull(index))
			{
				return default(TProperty);
			}
			try
			{
				return Get(dr, index);
			}
			catch (InvalidCastException ice)
			{
				throw new InvalidCastException(
					string.Format(
						"Could not cast the value in field {0} of type {1} to the Type {2}.",
						name, dr[index].GetType().Name, GetType().Name), ice);
			}
		}

		protected override void NullSafeSet(IDbCommand cmd, string name, TField value)
		{
			int index = cmd.Parameters.IndexOf(name);
			Set(cmd, index, value);
		}
	}

}