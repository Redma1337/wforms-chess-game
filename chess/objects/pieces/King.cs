using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess_Game_WForms.chess.objects.pieces
{
    public enum CastleType
    {
        SHORT,
        LONG,
        NONE
    }

    class King : Piece
    {
        public bool FirstMove { get; set; }
        public CastleType Castle { get; set; }
        public bool isCastled { get; set; }

        public King(int _x, int _y, ChessColor _color) : base(_x, _y, _color, _color == ChessColor.WHITE ? PieceType.KING_W : PieceType.KING_B)
        {
            FirstMove = true;
            Castle = CastleType.NONE;
            isCastled = false;
        }

        public override void updateMoves(Board b)
        {
            LegalTiles.ForEach(t => t.Selected = false);
            LegalTiles.Clear();

            for (var i = X - 1; i < X + 2; i++)
            {
                for (var j = Y - 1; j < Y + 2; j++)
                {
                    Tile dest = b.getTile(i, j);
                    if (dest == null) continue;
                    if (dest.isEmpty() && !dest.hasPiece(Color))
                        LegalTiles.Add(dest);
                }
            }

            if (FirstMove)
            {
                var lastLine = Globals.BOARD_HEIGHT - 1;
                if (X == 4 && Y == lastLine)
                {
                    Tile r_RightTile = b.getTile(7, lastLine);
                    if (r_RightTile.Piece is Rook && r_RightTile.hasPiece(Color))
                    {
                        Tile k_RightTile = b.getTile(6, lastLine);
                        Tile b_RightTile = b.getTile(5, lastLine);
                        if (k_RightTile != null && b_RightTile != null)
                        {
                            if (k_RightTile.isEmpty() && b_RightTile.isEmpty())
                            {
                                LegalTiles.Add(k_RightTile);
                                Castle = CastleType.SHORT;
                            }
                        }
                    }

                    Tile r_LeftTile = b.getTile(0, lastLine);
                    if (r_LeftTile.Piece is Rook && r_LeftTile.hasPiece(Color))
                    {
                        Tile k_LeftTile = b.getTile(1, lastLine);
                        Tile b_LeftTile = b.getTile(2, lastLine);
                        Tile q_LeftTile = b.getTile(3, lastLine);
                        if (k_LeftTile != null && b_LeftTile != null && q_LeftTile != null)
                        {
                            if (k_LeftTile.isEmpty() && b_LeftTile.isEmpty() && q_LeftTile.isEmpty())
                            {
                                LegalTiles.Add(b_LeftTile);
                                Castle = CastleType.LONG;
                            }
                        }

                    }
                }
            }
            LegalTiles.ForEach(t => t.Selected = true);
        }
    }
}
