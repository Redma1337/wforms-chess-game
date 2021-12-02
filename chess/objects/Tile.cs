using System;
using System.Windows.Forms;
using System.Drawing;

namespace Chess_Game_WForms.chess.objects
{
    class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Piece Piece { get; set; }
        public bool Selected { get; set; }
        public Tile(int _x, int _y)
        {
            X = _x;
            Y = _y;
            Selected = false;
        }

        public void setPiece(Piece p)
        {
            Piece = p;
            if (p != null)
            {
                Piece.X = X;
                Piece.Y = Y;
            }
        }

        public bool isEmpty()
        {
            return Piece == null;
        }

        public bool hasPiece(ChessColor color)
        {
            if (Piece == null)
                return false;
            return Piece.Color == color;
        }

        public void drawTile(PaintEventArgs ctx, bool dark)
        {
            //todo draw rectangle

            SolidBrush fillColor = new SolidBrush(Selected ? Color.Blue : dark ? Color.Brown : Color.WhiteSmoke);

            Rectangle rect = new Rectangle(
                Globals.BOARD_POS.X + X * Globals.TILE_SIZE,
                Globals.BOARD_POS.Y + Y * Globals.TILE_SIZE,
                Globals.TILE_SIZE,
                Globals.TILE_SIZE
            );

            ctx.Graphics.FillRectangle(fillColor, rect);


            if (Piece != null)
                Piece.drawPiece(
                    ctx,
                    Globals.BOARD_POS.X + X * Globals.TILE_SIZE,
                    Globals.BOARD_POS.Y + Y * Globals.TILE_SIZE
                );

            
            ctx.Graphics.DrawString(
                "X: " + X.ToString() + " | Y: " + Y.ToString(),
                new Font("Arial", 10),
                new SolidBrush(Color.Gray),
                new PointF(
                    Globals.BOARD_POS.X + X * Globals.TILE_SIZE + Globals.TILE_SIZE / 2 - 30,
                    Globals.BOARD_POS.Y + Y * Globals.TILE_SIZE + Globals.TILE_SIZE / 2
                    )
            );
            
        }
    }
}
