using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class Cell(ushort row, ushort col, CellType type = CellType.NotFigure)
    {
        public ushort Row { get => row; set => row = value; }
        public ushort Col { get => col; set => col = value; }
        public CellType Type { get => type; set => type = value; }
        public static bool IsActiveField { get; set; } = false;
        
        public Cell(Cell cell, CellType type) : this(cell.Row, cell.Col, type) { } //конструктор
        public Cell(Cell cell) : this(cell.Row, cell.Col, cell.Type) { } //конструктор

       /* public override bool Equals(object? obj)
        {
            if (obj is Cell cell)
            {
                return Row == cell.Row && Col == cell.Col;
            }
            return false;
        }*/

        public Cell CellClone()
        {
            return new Cell(this);
        }

        /*public override int GetHashCode()
        {
            return HashCode.Combine(Row.GetHashCode(), Col.GetHashCode());
        }*/
    }
}
