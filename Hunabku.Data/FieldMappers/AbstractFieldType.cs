using System.Data;

namespace Hunabku.Data.FieldMappers
{
	public abstract class AbstractFieldType<TField, TProperty>
	{
		public abstract TProperty Get(IDataReader dr, string name);
		public abstract void Set(IDbCommand cmd, string name, TProperty value);

		protected abstract TProperty NullSafeGet(IDataReader dr, string name);
		protected abstract void NullSafeSet(IDbCommand cmd, string name, TField value);
	}
}