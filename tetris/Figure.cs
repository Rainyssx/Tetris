using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class Figure: List<Cell>// взял из snake
    {

        
        private TypeFigure _form = TypeFigure.Squere;
        public TypeFigure Form { get =>_form; set{ _form = value; } }
        public Figure(int row, int col, TypeFigure typefigure)
        {
            switch(typefigure)
            {
                case TypeFigure.Squere:

                    this.Add(new Cell((ushort)(row ), (ushort)(col + 1), CellType.Figure));
                    this.Add(new Cell((ushort)(row), (ushort)(col), CellType.Figure));
                    this.Add(new Cell((ushort)(row + 1), (ushort)(col + 1), CellType.Figure));
                    this.Add(new Cell((ushort)(row + 1), (ushort)(col), CellType.Figure));
                    break;
                case TypeFigure.leftL:

                    this.Add(new Cell((ushort)(row - 1), (ushort)(col - 1), CellType.Figure));
                    this.Add(new Cell((ushort)(row), (ushort)(col), CellType.Figure));
                    this.Add(new Cell((ushort)(row + 1), (ushort)(col), CellType.Figure));
                    this.Add(new Cell((ushort)(row - 2), (ushort)(col - 2), CellType.Figure));
                    break;
                case TypeFigure.RightL:

                    this.Add(new Cell((ushort)(row +1), (ushort)(col), CellType.Figure));
                    this.Add(new Cell((ushort)(row), (ushort)(col), CellType.Figure));
                    this.Add(new Cell((ushort)(row + 1), (ushort)(col + 1), CellType.Figure));
                    this.Add(new Cell((ushort)(row + 2), (ushort)(col + 2), CellType.Figure));
                    break;
                case TypeFigure.Line:
                    
                    this.Add(new Cell((ushort)(row-1), (ushort)(col-1), CellType.Figure));
                    this.Add(new Cell((ushort)(row), (ushort)(col ), CellType.Figure));
                    this.Add(new Cell((ushort)(row + 1), (ushort)(col + 1), CellType.Figure));
                    this.Add(new Cell((ushort)(row +2), (ushort)(col +2), CellType.Figure));
                    break;
                case TypeFigure.Rsnake:

                    this.Add(new Cell((ushort)(row+1), (ushort)(col), CellType.Figure));
                    this.Add(new Cell((ushort)(row), (ushort)(col), CellType.Figure));
                    this.Add(new Cell((ushort)(row), (ushort)(col + 1), CellType.Figure));
                    this.Add(new Cell((ushort)(row -1), (ushort)(col -1), CellType.Figure));
                    break;
                case TypeFigure.Lsnake:

                    this.Add(new Cell((ushort)(row), (ushort)(col - 1), CellType.Figure));
                    this.Add(new Cell((ushort)(row), (ushort)(col), CellType.Figure));
                    this.Add(new Cell((ushort)(row + 1), (ushort)(col), CellType.Figure));
                    this.Add(new Cell((ushort)(row + 1), (ushort)(col + 1), CellType.Figure));
                    break;
                case TypeFigure.T:

                    this.Add(new Cell((ushort)(row ), (ushort)(col - 1), CellType.Figure));
                    this.Add(new Cell((ushort)(row), (ushort)(col), CellType.Figure));
                    this.Add(new Cell((ushort)(row), (ushort)(col + 1), CellType.Figure));
                    this.Add(new Cell((ushort)(row + 1), (ushort)(col), CellType.Figure));
                    break;

            }
            /*if ( (TypeFigure)typefigure  == TypeFigure.cubic)
            {
                this.Add(new Cell((ushort)row, (ushort)col, (CellType)0));
            }
            else
            {
                _form = TypeFigure.cubic;
            }*/

        }


        public int  MinRow()  //Метод должен вернуть минимальный row из сell in figure
        {
            var row = 100000;
            foreach (var item in this)
            {
                if(item.Row < row) row = item.Row;
            }
            return row;
        }
        public int MaxRow()  //Метод должен вернуть минимальный row из сell in figure
        {
            var row = -10000;
            foreach (var item in this)
            {
                if (item.Row > row) row = item.Row;
            }
            return row;
        }
        public int LeftElem()
        {
            var col = 19999;
            foreach (var item in this)
            {
                if (item.Col < col) col = item.Col;
            }
            return col;

        }
        public int RightElem()
        {
            var col = -1000000;
            foreach (var item in this)
            {
                if (item.Col > col) col = item.Col;
            }
            return col;

        }

        public void Fall()
        {
            foreach (var item in this)
            {
                item.Row += 1;
            }
        }
        
        public Figure Direction(Directions dir) 
        {
            switch (dir)
            {
                case Directions.ToLeft:
                    foreach (var item in this)
                    {
                        item.Col = (ushort)(item.Col - 1);
                    }
                    break;
                case Directions.ToRight:
                     foreach (var item in this)
                    {
                        item.Col = (ushort)(item.Col + 1);
                    }
                    
                    break;
                
                case Directions.ToTopOnClockRotation:
                    foreach (var item in this)
                    {
                        item.Col = (ushort)(item.Col + 1);
                    }

                    break;
                case Directions.ToDownOffClockRotation: 
                    foreach (var item in this)
                    {
                        item.Col = (ushort)(item.Col + 1);
                    }

                    break;
                default:
                    foreach (var item in this)
                    {
                        item.Col = (ushort)(item.Col);
                    }
                    break;
                    
            }
            return this;

        }
        



    }
}
