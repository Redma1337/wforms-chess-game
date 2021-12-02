using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Chess_Game_WForms.chess.objects;
using Chess_Game_WForms.chess.objects.pieces;
using System.Drawing;

namespace Chess_Game_WForms.chess
{
    public enum GameState
    {
        WHITE_WIN,
        BLACK_WIN,
        STALEMATE,
        CANCELLED,
        WHITE_TO_MOVE,
        BLACK_TO_MOVE,
        LOADING
    }

    public enum ChessColor
    {
        WHITE,
        BLACK
    }

    class Game
    {
        private Dictionary<string, List<Piece>> killedPieces = new Dictionary<string, List<Piece>>();

        private Player localPlayer = null;
        private Player externalPlayer = null;

        private Board board = new Board();
        private GameState state;
        private Player currentlyMoving;

        private NetController netController;

        public void init()
        {
            state = GameState.LOADING;

            netController = new NetController();

            localPlayer = new Player("Marc", ChessColor.WHITE);
            killedPieces.Add(localPlayer.Name, new List<Piece>());
            externalPlayer = new Player("Corvin", ChessColor.BLACK);
            killedPieces.Add(externalPlayer.Name, new List<Piece>());

            currentlyMoving = localPlayer;

            board.reset(currentlyMoving.PlaySide == ChessColor.WHITE);
            ResourceController.loadImages();

            //TODO hier net code glaube

            state = GameState.WHITE_TO_MOVE;
        }

        public void drawGame(PaintEventArgs ctx)
        {
            ctx.Graphics.DrawString(
                "Spieler 2 \"" + externalPlayer.Name + "\"",
                new Font("Arial", 16),
                new SolidBrush(Color.Gray),
                new PointF(10, 10)
            );   

            ctx.Graphics.DrawString(
                "Spieler 1 \"" + localPlayer.Name + "\"",
                new Font("Arial", 16),
                new SolidBrush(Color.Gray),
                new PointF(10, 910)
            );
            board.drawBoard(ctx);

        }
        public bool onMove(Point start, Point end)
        {
            for (int i = 0; i < Globals.BOARD_WITH; i++)
            {
                for (int j = 0; j < Globals.BOARD_HEIGHT; j++)
                {
                    board.getTile(i, j).Selected = false;
                }
            }

            Tile startTile = board.getTile(start);
            Tile endTile = board.getTile(end);
            Move move = new Move(localPlayer, startTile, endTile);

            return onMove(move);
        }

        public bool onMove(Move m)
        {
            if (isIllegalMove(m)) return false;

            bool pseudoCheck = board.inPseudoCheck(m, currentlyMoving.PlaySide);
            if (pseudoCheck)
                return false;

            if (m.End.Piece.Color != currentlyMoving.PlaySide)
            {
                m.End.Piece.State = PieceState.DEAD;
                killedPieces[m.Player.Name].Add(m.End.Piece);
            }

            if (m.End.Piece is Pawn)
            {
                ((Pawn)m.End.Piece).FirstMove = false;
            }
            else if (m.End.Piece is King)
            {
                var lastLine = Globals.BOARD_HEIGHT - 1;
                King k = (King) m.End.Piece;
                if (!k.isCastled && k.Castle == CastleType.LONG)
                {
                    Tile r_LeftTile = board.getTile(0, lastLine);
                    Tile newTile = board.getTile(3, lastLine);
                    newTile.setPiece(r_LeftTile.Piece);
                    r_LeftTile.setPiece(null);
                    k.isCastled = true;
                } else if (!k.isCastled && k.Castle == CastleType.SHORT)
                {
                    Tile r_RightTile = board.getTile(7, lastLine);
                    Tile newTile = board.getTile(5, lastLine);
                    newTile.setPiece(r_RightTile.Piece);
                    r_RightTile.setPiece(null);
                    k.isCastled = true;
                }
            }

            //switch turns
            return true;
        }

        public bool isIllegalMove(Move m)
        {
            return m.Start.isEmpty() //leeres feld
                || m.Start == m.End //selbes Feld
                || m.Player != currentlyMoving //nicht am zug
                || currentlyMoving.PlaySide != m.Start.Piece.Color //nicht eigenes piece
                || !m.Start.Piece.isValidMove(board, m); //piece kann da nicht hin
        }

        public void setSelected(Point pos)
        {
            Tile t = board.getTile(pos);
            if (t != null)
                t.Selected = true;
        }
    }
}
