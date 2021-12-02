using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game_WForms.chess.objects
{
    class Player
    {
        public string Name { get; set; }
        public ChessColor PlaySide { get; set; }
        public Player(string _name, ChessColor _playSide)
        {
            Name = _name;
            PlaySide = _playSide;
        }
    }
}
