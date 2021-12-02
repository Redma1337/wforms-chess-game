using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Chess_Game_WForms.chess.objects
{
    public enum PieceType
    {
        NONE,

        PAWN_W,
        KNIGHT_W,
        BISHOP_W,
        ROOK_W,
        QUEEN_W,
        KING_W,

        PAWN_B,
        KNIGHT_B,
        BISHOP_B,
        ROOK_B,
        QUEEN_B,
        KING_B
    }

    public enum PieceState
    {
        ALIVE,
        DEAD
    }

    abstract class Piece
    {
        public PieceState State { get; set; }
        public ChessColor Color { get; set; }
        public PieceType Type { get; set; }
        public Image Image { get => ResourceController.getImage(Type); }
        public List<Tile> LegalTiles { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Piece(int _x, int _y, ChessColor _color, PieceType _type)
        {
            X = _x;
            Y = _y;
            Color = _color;
            Type = _type;
            State = PieceState.ALIVE;

            LegalTiles = new List<Tile>();
        }

        public void drawPiece(PaintEventArgs ctx, int x, int y)
        {
            RectangleF srcRect = new RectangleF(0, 0, 100, 100);
            GraphicsUnit units = GraphicsUnit.Pixel;
            ctx.Graphics.DrawImage(Image, x, y, srcRect, units);
        }
        public bool isValidMove(Board b, Move m)
        {
            updateMoves(b);
            return LegalTiles.Contains(m.End);
        }

        public abstract void updateMoves(Board b);
    }
}
