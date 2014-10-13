using System.Data;
using Hunabku.Data.FieldMappers;

namespace Hunabku.Data
{
	public class FieldMapper
	{
		private static readonly StringFieldType stringToString = new StringFieldType();
		private static readonly Int32FieldType int32ToInt32 = new Int32FieldType();

		/// <summary>
		/// Lee un field de tipo varchar o similar.
		/// </summary>
		/// <param name="dr">Data reader</param>
		/// <param name="name">Nombre del campo o del alias del query</param>
		/// <returns>El valor del field.</returns>
		/// <remarks>
		/// Podría usarse para leer cualquier field convertido a string pero no esa la idea ya que la conversion
		/// sería incontrolada por la app (usa Converter de .NET).
		/// </remarks>
		public static string FromString(IDataReader dr, string name)
		{
			return stringToString.Get(dr, name);
		}

		public static int FromInt32(IDataReader dr, string name)
		{
			return int32ToInt32.Get(dr, name);
		} 
	}
}