using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class GameViewModel
    {
        private readonly User _player; //Игрок
        private readonly ushort _rows = 30; //Размеры поля
        private readonly ushort _cols = 17;
        public User Player
        {
            get => _player.Clone();
            init => _player = value;
        }
        public Field Field { get; init; }
        private GameLogic? _game;

        private Commands? _startCommand;
        private Commands? _rotateCommand;
        public Commands StartCommand => _startCommand ??= new(_ => Start(),
            _ => !_game?.IsActive ?? true);

        public Commands RotateCommand => _rotateCommand ??= new(RotateFigure,
            _ => _game?.IsActive ?? false);

    //Чехарда с верхним и нижним регистром для команд, дикость какая 

        private void RotateFigure(object? param)
        {
            if (param is Directions dir)
            {
                _game?.Rotatefigure(dir); //Это переносит в логику, судя по всему описание действия команды
            }
        }

        public GameViewModel(User player)
        {
            _player = player;
            Field = new(_rows, _cols);
        } //Конструктор 

        private void Start() //Создание поля и начало игры
        {
            Field.Reset();
            _game = new GameLogic(Field);
            _game.OnStopGame += Stop;
            _game.Start();
        }

        private void Stop()
        {
        } //не написано и ладно

        public CellConverter TypeConverter { get; } = new(); //зачем это здесь и что оно делать
                                                             //если принять на веру что gameviewmodel орудует(ЧЕМ?)
                                                             //всей движухой
                                                             //здесь же создается поле и команды
    }
}
