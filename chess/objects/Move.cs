using System;

namespace Chess_Game_WForms.chess.objects
{
    class Move
    {
        public Tile Start { get; set; }
        public Tile End { get; set; }
        public Piece ToMove { get; set; }
        public Player Player { get; set; }

        public Move(Player _player, Tile _start, Tile _end)
        {
            Start = _start;
            End = _end;
            Player = _player;

            ToMove = _start.Piece;
        }

    }
}
