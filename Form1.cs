using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Chess_Game_WForms.chess;

namespace Chess_Game_WForms
{
    public partial class Form1 : Form
    {
        Point start = new Point(0, 0);
        Point end = new Point(0, 0);
        bool selecting = false;
        private Game chessGame = new Game();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chessGame.init();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            chessGame.drawGame(e);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Point realPos = new Point(e.X - Globals.BOARD_POS.X, e.Y - Globals.BOARD_POS.Y);
            if (!selecting)
            {
                start.X = realPos.X / Globals.TILE_SIZE;
                start.Y = realPos.Y / Globals.TILE_SIZE;
                selecting = true;
                chessGame.setSelected(start);
                Invalidate();
            } else
            {
                end.X = realPos.X / Globals.TILE_SIZE;
                end.Y = realPos.Y / Globals.TILE_SIZE;
                chessGame.setSelected(end);
                chessGame.onMove(start, end);
                Invalidate();
                selecting = false;
            }
        }
    }
}
