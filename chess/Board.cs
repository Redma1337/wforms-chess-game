using System;
using System.Windows.Forms;
using System.Drawing;
using Chess_Game_WForms.chess.objects;
using Chess_Game_WForms.chess.objects.pieces;

namespace Chess_Game_WForms.chess
{
    class Board
    {
        public Tile[,] board { get; set; }

        private string[] mask =
        {
            "RKBQXBKR",
            "PPPPPPPP",
            "00000000",
            "00000000",
            "00000000",
            "00000000",
            "PPPPPPPP",
            "RKBQXBKR",
        };

        public Board()
        {
            board = new Tile[Globals.BOARD_WITH, Globals.BOARD_HEIGHT];
        }

        public void reset(bool playWhite)
        {
            for (int i = 0; i < Globals.BOARD_WITH; i++)
            {
                for (int j = 0; j < Globals.BOARD_HEIGHT; j++)
                {
                    board[i, j] = new Tile(i, j);
                }
            }

            loadDefaultMask(mask, playWhite);
        }

        public void loadDefaultMask(string[] mask, bool playWhite)
        {
            for (int i = 0; i < mask.Length; i++)
            {
                char[] line = mask[i].ToCharArray();
                //todo metadata in mask
                if (mask.Length - i == 2) playWhite = !playWhite; //last to lines are own color
                for (int j = 0; j < line.Length; j++)
                {
                    ChessColor enemyColor = playWhite ? ChessColor.BLACK : ChessColor.WHITE;
                    char letter = line[j];
                    switch (letter)
                    {
                        case 'R':
                            board[j, i].setPiece(new Rook(j, i, enemyColor));
                            break;
                        case 'K':
                            board[j, i].setPiece(new Knight(j, i, enemyColor));
                            break;
                        case 'B':
                            board[j, i].setPiece(new Bishop(j, i, enemyColor));
                            break;
                        case 'X':
                            board[j, i].setPiece(new King(j, i, enemyColor));
                            break;
                        case 'Q':
                            board[j, i].setPiece(new Queen(j, i, enemyColor));
                            break;
                        case 'P':
                            board[j, i].setPiece(new Pawn(j, i, enemyColor));
                            break;
                    }
                }
            }
        }

        public void drawBoard(PaintEventArgs ctx)
        {
            bool flag = false;
            for (int i = 0; i < Globals.BOARD_WITH; i++)
            {
                for (int j = 0; j < Globals.BOARD_HEIGHT; j++)
                {
                    board[i, j].drawTile(ctx, flag);
                    flag = !flag;
                }
                flag = !flag;
            }
        }

        public void forceMoveUpdate()
        {
            for (int i = 0; i < Globals.BOARD_WITH; i++)
            {
                for (int j = 0; j < Globals.BOARD_HEIGHT; j++)
                {
                    Tile tile = board[i, j];
                    if (tile.Piece == null) continue;
                    tile.Piece.updateMoves(this);
                }
            }
        }

        public bool inPseudoCheck(Move m, ChessColor playSide)
        {
            Piece oldEndPiece = m.End.Piece;
            m.End.setPiece(m.Start.Piece);
            m.Start.setPiece(null);


            bool check = inCheck(playSide);
            if (check)
            {
                m.Start.setPiece(m.End.Piece);
                m.End.setPiece(oldEndPiece);
            }
            return check;
        }

        public bool isCheckMate(ChessColor side)
        {
            bool check = inCheck(side);

            Tile kingTile = getKingTile(side);
            bool noMoves = kingTile.Piece.LegalTiles.Count == 0;

            return check && noMoves;
        }

        public bool inCheck(ChessColor side)
        {
            forceMoveUpdate();
            Tile king = getKingTile(side);
            for (int i = 0; i < Globals.BOARD_WITH; i++)
            {
                for (int j = 0; j < Globals.BOARD_HEIGHT; j++)
                {
                    Tile tile = board[i, j];
                    if (tile.Piece == null) continue;
                    if (tile.Piece.LegalTiles.Contains(king))
                        return true;
                }
            }
            return false;
        }


        public Tile getKingTile(ChessColor side)
        {
            for (int i = 0; i < Globals.BOARD_WITH; i++)
            {
                for (int j = 0; j < Globals.BOARD_HEIGHT; j++)
                {
                    Tile tile = board[i, j];
                    if (tile.Piece == null) continue;
                    if (tile.Piece is King && tile.Piece.Color == side)
                    {
                        return tile;
                    }
                }
            }
            return null;
        }

        public Tile getTile(Point p)
        {
            if (p.X < 0 || p.X > Globals.BOARD_WITH - 1 || p.Y < 0 || p.Y > Globals.BOARD_HEIGHT - 1)
                return null;
            return board[p.X, p.Y];
        }

        public Tile getTile(int x, int y)
        {
            if (x < 0 || x > Globals.BOARD_WITH - 1 || y < 0 || y > Globals.BOARD_HEIGHT - 1)
                return null;
            return board[x, y];
        }
    }
}
