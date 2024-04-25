using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace tetris
{
    public class GameLogic(Field field)
    {
        private Thread? _gameThread;    //Поток
        private readonly object _sync = new(); //Что и зачем, наверное для потока но ?

        private List<Cell> Figures;      // список клеток,которые будут закрашены под фигуры
        private Figure? _currentfigure;  // что вообще такое логика игры
        private List<Cell> _lastMoveFigure = new List<Cell>();                // и должна ли в один момент здесь быть только одна движ фигура или вообще фигура


        public event Action? OnStopGame; //Событие?! Не команда?!

        public bool IsActive //Ничего непонятно
                             //получить значение поле клетки для чего
                             //зачем обновлять клетки
                             //и вызов события стоп?
        {
            get => Cell.IsActiveField; // откуда и что берется здесь , дерьмо какое
            private set
            {
                Cell.IsActiveField = value;
                field.UpdateCells();
                if (!value) OnStopGame?.Invoke();
            }
        }






        private void UpdateField(List<Cell> cells, List<Cell> lastcells, [Optional] bool boo) //Здесь возникает ошибка при вызове метода из Field так как cells null
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    field.ChangeCells(cells,lastcells,boo);  
                }
                catch
                {
                    IsActive = false;
                }
            });
        } //изменяет нужные клетки в списке

        public void Start() //Запуск игры с потоком(описание работы потока)
        {
            if (IsActive) return;
            IsActive = true;
            InitGame();
            _gameThread = new Thread(_ =>
            {
                while (IsActive)
                {
                    UpdateField(NextMove(), _lastMoveFigure);
                    Thread.Sleep(250);
                }
            })
            {
                IsBackground = true
            };
            _gameThread.Start();
        }

        private void InitGame() // иницилизация игры и фигуры
        {
            IsActive = true;
            List<Cell> result = InitFigure(); //Здесь добавляем фигуру, но это шаблон для змейки,
                                              //она одна, фигур будет много 

            UpdateField(result,null);
        }

        public void Stop()
        {
            if (IsActive)
            {
                IsActive = false;
            }
        }  //Клетка стоп



        public Cell startCell = new Cell(3, 8); // клетка где появляется фигура верх середины
        private readonly Random _rand = new((int)DateTime.Now.ToFileTime()); //Случайное число
        private int countTypeFigure = Enum.GetNames(typeof(TypeFigure)).Length; // количество элементов в энаме
        //_rand.Next(countTypeFigure)

        private List<Cell> InitFigure() //инициализация фигуры при старте InitGame
        {
            
            lock (_sync)
            {
                if(_currentfigure == null)
                {
                    _currentfigure = new Figure(startCell.Row, startCell.Col, (TypeFigure)(_rand.Next(countTypeFigure)) );
                }
                
                    
                //result = result.Concat(_currentfigure).ToList();
            }
            return _currentfigure;
        }

        private List<Cell> NextMove()
        {
             //новое состояние фигуры
            if (_currentfigure != null)
            {
                if ((_currentfigure.MaxRow() < (int)field.Rows - 1) && (!CurrentFigureUnderFallenFigure(field)))    // проверить что фигура может двигаться вниз
                                                                        // тоесть край карты или внизу нет другой фигуры
                {
                    lock (_sync)
                    {

                        foreach (var item in _currentfigure)
                        {
                            _lastMoveFigure.Add(item.CellClone());
                        }
                        _currentfigure.Fall();

                    }
                    
                }
                else //Добавить фигуру в список фигур и слепить новую
                {
                    UpdateField(_currentfigure, null, true);
                    _lastMoveFigure.Clear();
                    _currentfigure.Clear();
                    _currentfigure = null;
                    InitFigure();
                }
            }
            return _currentfigure;
        }





        public void Rotatefigure(Directions dir)   //Поворот фигуры, непонятно как работающий
                                                  //вызывается с помощью команды
        {
            if (_currentfigure != null)
            {

                if (dir == Directions.ToLeft && _currentfigure.LeftElem() == 0)
                {
                    
                }
                else if (dir == Directions.ToRight && _currentfigure.RightElem() == field.Cols - 1)
                {
                    
                }
                else
                {
                    lock (_sync) 
                    {
                        foreach (var item in _currentfigure)
                        {
                            _lastMoveFigure.Add(item.CellClone());
                        }
                        _currentfigure.Direction(dir);


                    }
                }
            }


        }

        public bool CurrentFigureUnderFallenFigure(Field field)
        {
            if ((_currentfigure.MaxRow() < (int)field.Rows - 1))
            {
                foreach (var item in _currentfigure)
                {
                    if (field[(item.Row + 1) * (field.Cols) + item.Col].Type == CellType.fallenFigure) //получить ячейку из field по индексу
                    {
                        return true;
                    }
                }
            }
            return false;
        }





    }

}

     




