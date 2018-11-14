namespace Easy_Five
{
    class Playing
    {
        public const int N = 15;   //棋盘大小
        public int[,] ChessBoard = new int[N, N];
        public int[] Cursor = new int[2];
        public int whiteCount = 0;
        public int blackCount = 0;

        public Playing()
        {

        }

        public bool InBoundry(int x, int y)
        {
            // 判断光标是否在棋盘内，N为棋盘规格，x,y为棋盘坐标
            if (x == -1 || x == N || y == -1 || y == N) return false;
            else return true;
        }

        public bool Win(int v)
        {
            int i, j;
            // 判断输赢的方法是遍历ChessBoard数组，寻找相同的棋子在同一条线上的数目

            // 添加x,y是为了方便调用InBoundry()函数
            i = 1; j = 1;
            // 左对角线连续5个棋子
            try
            {
                while (ChessBoard[Cursor[0] - i, Cursor[1] - i] == v && InBoundry(Cursor[0] - i, Cursor[1] - i)) i++;
            }
            catch { }
            try
            {
                while (ChessBoard[Cursor[0] + j, Cursor[1] + j] == v && InBoundry(Cursor[0] + j, Cursor[1] + j)) j++;
            }
            catch { }
            if (i + j >= 6) return true;

            i = 1; j = 1;
            // 右对角线连续5个棋子
            try
            {
                while (ChessBoard[Cursor[0] + i, Cursor[1] - i] == v && InBoundry(Cursor[0] + i, Cursor[1] - i)) i++;
            }
            catch { }
            try
            {
                while (ChessBoard[Cursor[0] - j, Cursor[1] + j] == v && InBoundry(Cursor[0] - j, Cursor[1] + j)) j++;
            }
            catch { }
            if (i + j >= 6) return true;

            i = 1; j = 1;
            // 同一列上连续5个棋子
            try
            {
                while (ChessBoard[Cursor[0] - i, Cursor[1]] == v && InBoundry(Cursor[0] - i, Cursor[1])) i++;
            }
            catch { }
            try
            {
                while (ChessBoard[Cursor[0] + j, Cursor[1]] == v && InBoundry(Cursor[0] + j, Cursor[1])) j++;
            }
            catch { }
            if (i + j >= 6) return true;

            i = 1; j = 1;
            // 同一行上连续5个棋子
            try
            {
                while (ChessBoard[Cursor[0], Cursor[1] - i] == v && InBoundry(Cursor[0] - 1, Cursor[1] - i)) i++;
            }
            catch { }
            try
            {
                while (ChessBoard[Cursor[0], Cursor[1] + j] == v && InBoundry(Cursor[0] - 1, Cursor[1] + j)) j++;
            }
            catch { }
            if (i + j >= 6) return true;

            return false;
        }

    }
}
