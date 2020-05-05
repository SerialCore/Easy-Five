using System.Drawing;

namespace Easy_Five
{
    public partial class GameBoard
    {

        public const int N = 15;   //棋盘大小
        public int[,] ChessBoard;
        public Point Cursor;   //最后一步棋子位置

        public bool IsPlaying;
        public bool IsCurrentBlack;
        public bool IsPersonBlack;
        public bool IsPersonWhite;

        public GameBoard()
        {
            this.IsPlaying = false;
            this.IsCurrentBlack = true;
            this.IsPersonBlack = true;
            this.IsPersonWhite = false;

            this.Cursor = new Point(0, 0);
            this.ChessBoard = new int[N, N];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    ChessBoard[i, j] = 0;
                }
            }
        }

        public void StartGame()
        {
            this.IsPlaying = true;
            this.IsCurrentBlack = true;
        }

        public bool GameWin(int color)
        {
            int i, j;

            i = 1; j = 1;
            // 左对角线连续5个棋子
            try
            {
                while (ChessBoard[Cursor.X - i, Cursor.Y - i] == color && NoCrossBorder(Cursor.X - i, Cursor.Y - i)) i++;
            }
            catch { }
            try
            {
                while (ChessBoard[Cursor.X + j, Cursor.Y + j] == color && NoCrossBorder(Cursor.X + j, Cursor.Y + j)) j++;
            }
            catch { }
            if (i + j >= 6)
            {
                this.IsPlaying = false;
                return true;
            }

            i = 1; j = 1;
            // 右对角线连续5个棋子
            try
            {
                while (ChessBoard[Cursor.X + i, Cursor.Y - i] == color && NoCrossBorder(Cursor.X + i, Cursor.Y - i)) i++;
            }
            catch { }
            try
            {
                while (ChessBoard[Cursor.X - j, Cursor.Y + j] == color && NoCrossBorder(Cursor.X - j, Cursor.Y + j)) j++;
            }
            catch { }
            if (i + j >= 6)
            {
                this.IsPlaying = false;
                return true;
            }

            i = 1; j = 1;
            // 同一列上连续5个棋子
            try
            {
                while (ChessBoard[Cursor.X - i, Cursor.Y] == color && NoCrossBorder(Cursor.X - i, Cursor.Y)) i++;
            }
            catch { }
            try
            {
                while (ChessBoard[Cursor.X + j, Cursor.Y] == color && NoCrossBorder(Cursor.X + j, Cursor.Y)) j++;
            }
            catch { }
            if (i + j >= 6)
            {
                this.IsPlaying = false;
                return true;
            }

            i = 1; j = 1;
            // 同一行上连续5个棋子
            try
            {
                while (ChessBoard[Cursor.X, Cursor.Y - i] == color && NoCrossBorder(Cursor.X - 1, Cursor.Y - i)) i++;
            }
            catch { }
            try
            {
                while (ChessBoard[Cursor.X, Cursor.Y + j] == color && NoCrossBorder(Cursor.X - 1, Cursor.Y + j)) j++;
            }
            catch { }
            if (i + j >= 6)
            {
                this.IsPlaying = false;
                return true;
            }

            return false;
        }

    }
}
