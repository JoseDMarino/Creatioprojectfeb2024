namespace Terrasoft.Configuration.Utils
{
	using System;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	#region Class: MacrosValueFormatter

	/// <summary>
	/// Describes entity column value formatter.
	/// </summary>
	public class MacrosValueFormatter
	{

		#region Constructors: Public

		/// <summary>
		/// Constructor for <see cref="MacrosValueFormatter"/>.
		/// </summary>
		/// <param name="userConnection">Instance of <see cref="UserConnection"/>.</param>
		public MacrosValueFormatter(UserConnection userConnection) {
			UserConnection = userConnection;
		}

		#endregion

		#region Properties: Public

		/// <summary>
		/// Current user connection.
		/// </summary>
		public UserConnection UserConnection { get; set; }

		#endregion

		#region Methods: Private

		private bool IsEmptyValue(object value) {
			return value == null || (value is string && string.IsNullOrWhiteSpace((string)value));
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Default implementation of entity column value formatting.
		/// </summary>
		/// <param name="value">Source entity column value.</param>
		/// <param name="column">Entity schema column.</param>
		/// <returns></returns>
		public virtual string GetStringValue(EntityColumnValue columnValue) {
			var column = columnValue.Column;
			if (IsEmptyValue(columnValue.Value)) {
				return string.Empty;
			}
			if (column != null && column.DataValueType is DateTimeDataValueType) {
				return column.DataValueType.GetColumnDisplayValue(column, columnValue.Value);
			}
			return Convert.ToString(columnValue.Value);
		}

		#endregion

	}

	#endregion

}

