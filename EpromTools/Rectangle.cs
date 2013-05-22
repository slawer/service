using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace EpromWorking
{
    class Comparer : IComparer<DataGridViewCell>
    {
        public int Compare(DataGridViewCell f, DataGridViewCell s)
        {
            if (f.RowIndex == s.RowIndex)
            {
                if (f.ColumnIndex > s.ColumnIndex) return 1;
                if (f.ColumnIndex < s.ColumnIndex) return -1;
                return 0;
            }
            else
            {
                if (f.RowIndex > s.RowIndex) return 1;
                if (f.RowIndex < s.RowIndex) return -1;
            }
            return 0;
        }
    }

    class Rectangle
    {
        public int RowsCount = 0;
        public int ColumnsCount = 0;

        private object[] values = null;
        private object[] native = null;
            
        public DataGridViewCell[] selected = null;

        private void InitStructure(DataGridViewSelectedCellCollection Cells)
        {
            if (Cells != null)
            {
                selected = new DataGridViewCell[Cells.Count];
                values = new string[selected.Length];

                Cells.CopyTo(selected, 0);
                Array.Sort(selected, new Comparer());

                for (int index = 0; index < values.Length; index++)
                {
                    values[index] = selected[index].Value;
                }

                native = new object[values.Length];
                values.CopyTo(native, 0);

                int minColumn = int.MaxValue, maxColumn = int.MinValue;
                RowsCount = selected[selected.Length - 1].RowIndex - selected[0].RowIndex + 1;
                
                foreach (DataGridViewCell cell in selected)
                {
                    if (cell.ColumnIndex > maxColumn) maxColumn = cell.ColumnIndex;
                    if (cell.ColumnIndex < minColumn) minColumn = cell.ColumnIndex;
                }

                ColumnsCount = maxColumn - minColumn + 1;
            }
        }

        // ----- конструктор ------

        public Rectangle(DataGridViewSelectedCellCollection Cells)
        {
            InitStructure(Cells);
        }

        // ------ 



        // ------ как с матрицей -----

        public Object GetValue(int column, int row)
        {
            if (column < ColumnsCount && row < RowsCount)
            {
                return values[GetIndex(column, row)];
            }
            return null;
        }

        public void SetValue(int column, int row, Object Value)
        {
            if (column < ColumnsCount && row < RowsCount)
            {
                selected[GetIndex(column, row)].Value = Value;
                values[GetIndex(column, row)] = Value.ToString();
            }
        }

        private int GetIndex(int column, int row)
        {
            return ColumnsCount * row + column;
        }

        // ------- вставка --------

        public void Paste(Rectangle pasted)
        {
            if (pasted != null)
            {
                int rows = RowsCount, cols = ColumnsCount;

                if (ColumnsCount >= pasted.ColumnsCount
                     && RowsCount >= pasted.RowsCount)
                {
                    rows = pasted.RowsCount;
                    cols = pasted.ColumnsCount;
                }

                for (int row = 0; row < RowsCount; row++)
                {
                    for (int col = 0; col < ColumnsCount; col++)
                    {
                        object value = pasted.GetValue(col, row);
                        if (value != null)
                        {
                            SetValue(col, row, value);
                        }
                    }
                }
            }
        }

        public void RollBack()
        {
            int index = 0;
            foreach (DataGridViewCell cell in selected)
            {
                cell.Value = native[index++];
            }
        }
    }
}