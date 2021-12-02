using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess_Game_WForms.chess.objects.pieces
{
    class Knight : Piece
    {
        public Knight(int _x, int _y, ChessColor _color) : base(_x, _y, _color, _color == ChessColor.WHITE ? PieceType.KNIGHT_W : PieceType.KNIGHT_B)
        {
        }

        public override void updateMoves(Board b)
        {
            LegalTiles.Clear();
            for (var i = -2; i < 3; i++)
            {
                for (var j = -2; j < 3; j++)
                {
                    if (Math.Abs(i) != Math.Abs(j) && i != 0 && j != 0)
                    {
                        Tile tile = b.getTile(X + i, Y + j);
                        if (tile == null) continue;
                        if (tile.isEmpty() || !tile.hasPiece(Color))
                        {
                            LegalTiles.Add(tile);
                        }
                    }
                }
            }
        }
    }
}
