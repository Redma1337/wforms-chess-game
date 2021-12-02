using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess_Game_WForms.chess.objects.pieces
{
    class Pawn : Piece
    {
        public bool FirstMove { get; set; }

        public Pawn(int _x, int _y, ChessColor _color) : base(_x, _y, _color, _color == ChessColor.WHITE ? PieceType.PAWN_W : PieceType.PAWN_B)
        {
            FirstMove = true;
        }

        public override void updateMoves(Board b)
        {
            LegalTiles.Clear();
            //first Move
            if (FirstMove)
            {
                Tile farFrontTile = b.getTile(X, Y - 2);
                if (farFrontTile != null && farFrontTile.isEmpty())
                    LegalTiles.Add(farFrontTile);
            }
            Tile frontTile = b.getTile(X, Y - 1);
            if (frontTile != null && frontTile.isEmpty())
                LegalTiles.Add(frontTile);

            Tile leftFrontTile = b.getTile(X - 1, Y - 1);
            Tile rightFrontTile = b.getTile(X + 1, Y - 1);
            if (leftFrontTile != null && !leftFrontTile.hasPiece(Color) && !leftFrontTile.isEmpty())
                LegalTiles.Add(leftFrontTile);
            if (rightFrontTile != null && !rightFrontTile.hasPiece(Color) && !rightFrontTile.isEmpty())
                LegalTiles.Add(rightFrontTile);

        }
    }
}
