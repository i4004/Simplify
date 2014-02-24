using System;
using System.Collections;
using System.Windows.Forms;

namespace Simplify.Windows.Forms
{
	/// <summary>
	/// System.Windows.Forms.ListView class columns sorter provider
	/// </summary>
	public class ListViewColumnSorter : IComparer
	{
		/// <summary>
		/// Specifies the column to be sorted
		/// </summary>
		private int _columnToSort;

		/// <summary>
		/// Specifies the order in which to sort (i.e. 'Ascending').
		/// </summary>
		private SortOrder _orderOfSort;

		/// <summary>
		/// Class constructor.  Initializes various elements
		/// </summary>
		public ListViewColumnSorter()
		{
			// Initialize the column to '0'
			_columnToSort = 0;

			// Initialize the sort order to 'none'
			_orderOfSort = SortOrder.None;
		}

		/// <summary>
		/// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
		/// </summary>
		/// <param name="x">First object to be compared</param>
		/// <param name="y">Second object to be compared</param>
		/// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
		public int Compare(object x, object y)
		{
			var itemX = "";
			var itemY = "";

			if(((ListViewItem)x).SubItems.Count > _columnToSort)
				itemX = ((ListViewItem)x).SubItems[_columnToSort].Text;

			if(((ListViewItem)y).SubItems.Count > _columnToSort)
				itemY = ((ListViewItem)y).SubItems[_columnToSort].Text;

			var compareResult = 0;
			DateTime dateItemX;
			DateTime dateItemY;
			int integerItemX;
			int integerItemY;

			if(DateTime.TryParse(itemX, out dateItemX) && DateTime.TryParse(itemY, out dateItemY))
				compareResult = DateTime.Compare(dateItemX, dateItemY);
			else if(int.TryParse(itemX, out integerItemX) && int.TryParse(itemY, out integerItemY))
			{
				if(integerItemX == integerItemY)
					compareResult = 0;
				else if(integerItemX < integerItemY)
					compareResult = -1;
				else if(integerItemX > integerItemY)
					compareResult = 1;
			}
			else
				compareResult = String.CompareOrdinal(itemX, itemY);

			// Calculate correct return value based on object comparison
			if(_orderOfSort == SortOrder.Ascending)
				return compareResult; // Ascending sort is selected, return normal result of compare operation

			if(_orderOfSort == SortOrder.Descending)
				return (-compareResult); // Descending sort is selected, return negative result of compare operation

			return 0;
		}

		/// <summary>
		/// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
		/// </summary>
		public int SortColumn
		{
			set
			{
				_columnToSort = value;
			}
			get
			{
				return _columnToSort;
			}
		}

		/// <summary>
		/// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
		/// </summary>
		public SortOrder Order
		{
			set
			{
				_orderOfSort = value;
			}
			get
			{
				return _orderOfSort;
			}
		}
	}
}
