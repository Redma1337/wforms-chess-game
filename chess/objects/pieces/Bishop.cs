using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess_Game_WForms.chess.objects.pieces
{
    class Bishop : Piece
    {
        public Bishop(int _x, int _y, ChessColor _color) : base(_x, _y, _color, _color == ChessColor.WHITE ? PieceType.BISHOP_W : PieceType.BISHOP_B)
        {
        }

        public override void updateMoves(Board b)
        {
            LegalTiles.Clear();
            //top right
            int tr_y = 1;
            for (int x = X + 1; x < Globals.BOARD_WITH; x++)
            {
                Tile tile = b.getTile(x, Y + tr_y);
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
                tr_y++;
            }

            //bot right
            int br_y = 1;
            for (int x = X + 1; x < Globals.BOARD_WITH; x++)
            {
                Tile tile = b.getTile(x, Y - br_y);
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
                br_y++;
            }

            //top left
            int tl_y = 1;
            for (int x = X - 1; x >= 0; x--)
            {
                Tile tile = b.getTile(x, Y + tl_y);
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
                tl_y++;
            }

            //bot left
            int bl_y = 1;
            for (int x = X - 1; x >= 0; x--)
            {
                Tile tile = b.getTile(x, Y - bl_y);
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
                bl_y++;
            }
        }
    }
}
