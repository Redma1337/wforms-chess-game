using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess_Game_WForms.chess.objects.pieces
{
    class Rook : Piece
    {
        public Rook(int _x, int _y, ChessColor _color) : base(_x, _y, _color, _color == ChessColor.WHITE ? PieceType.ROOK_W : PieceType.ROOK_B)
        {

        }

        public override void updateMoves(Board b)
        {
            LegalTiles.Clear();
            //up
            for (int y = Y + 1; y < Globals.BOARD_HEIGHT; y++)
            {
                Tile tile = b.getTile(X, y);
                if (tile == null) continue;
                if (tile.isEmpty())
                {
                    LegalTiles.Add(tile);
                }
                else
                {
                    if (!tile.hasPiece(Color))
                    {
                        LegalTiles.Add(tile);
                    }
                    break;
                }
            }

            //down
            for (int y = Y - 1; y >= 0; y--)
            {
                Tile tile = b.getTile(X, y);
                if (tile == null) continue;
                if (tile.isEmpty())
                {
                    LegalTiles.Add(tile);
                }
                else
                {
                    if (!tile.hasPiece(Color))
                    {
                        LegalTiles.Add(tile);
                    }
                    break;
                }
            }

            //right
            for (int x = X + 1; x < Globals.BOARD_WITH; x++)
            {
                Tile tile = b.getTile(x, Y);
                if (tile == null) continue;
                if (tile.isEmpty())
                {
                    LegalTiles.Add(tile);
                }
                else
                {
                    if (!tile.hasPiece(Color))
                    {
                        LegalTiles.Add(tile);
                    }
                    break;
                }
            }

            //left
            for (int x = X - 1; x >= 0; x--)
            {
                Tile tile = b.getTile(x, Y);
                if (tile == null) continue;
                if (tile.isEmpty())
                {
                    LegalTiles.Add(tile);
                } else
                {
                    if (!tile.hasPiece(Color))
                    {
                        LegalTiles.Add(tile);
                    }
                    break;
                }
            }
        }
    }
}
