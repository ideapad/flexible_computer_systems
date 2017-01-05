using System;
using System.Collections;
using System.Windows.Forms;

namespace Lab1
{
    // Implements the manual sorting of items by column.
    class ListViewItemComparer : IComparer
    {
        private int col;
        private bool sortByLength;
        public ListViewItemComparer()
        {
            col = 0;
            sortByLength = false;
        }
        public ListViewItemComparer(int column)
        {
            col = column;
        }

        public ListViewItemComparer(int column, bool byLength)
        {
            col = column;
            sortByLength = byLength;
        }

        public int CompareInt(int a, int b)
        {
            if (a > b) return -1;
            else return 1;
        }
        public int Compare(object x, object y)
        {
            int returnVal = -1;

            if(sortByLength)
                returnVal = CompareInt(((ListViewItem)x).SubItems[col].Text.Length,
                    ((ListViewItem)y).SubItems[col].Text.Length);
            else
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                    ((ListViewItem)y).SubItems[col].Text);

            return returnVal;
        }
    }
}
