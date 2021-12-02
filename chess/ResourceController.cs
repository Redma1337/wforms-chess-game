using System;
using System.Drawing;
using System.Collections.Generic;
using Chess_Game_WForms.chess.objects;

namespace Chess_Game_WForms.chess
{
    class ResourceController
    {
        private static Dictionary<int, Image> imageCache = new Dictionary<int, Image>();
 
        private static string resBase = "C:/Users/UserAdmin/Desktop/Coding/Schule/Chess-Game-WForms/chess/resources/";

        public static void loadImages()
        {
            load(PieceType.PAWN_W);
            load(PieceType.KNIGHT_W);
            load(PieceType.BISHOP_W);
            load(PieceType.ROOK_W);
            load(PieceType.QUEEN_W);
            load(PieceType.KING_W);

            load(PieceType.PAWN_B);
            load(PieceType.KNIGHT_B);
            load(PieceType.BISHOP_B);
            load(PieceType.ROOK_B);
            load(PieceType.QUEEN_B);
            load(PieceType.KING_B);
        }

        private static void load(PieceType type)
        {
            string resOffset = type.ToString() + ".png";
            imageCache.Add((int)type, Image.FromFile(resBase + resOffset));
        }

        public static Image getImage(PieceType key)
        {
            return imageCache[(int)key];
        }
    }
}
