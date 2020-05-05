using System;
using System.Drawing;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Easy_Five
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            board = new GameBoard();
        }

        #region Game

        private GameBoard board = null;

        private void AddBlack(int index)
        {
            Image image = new Image();
            BitmapImage bm = new BitmapImage();
            bm.UriSource = new Uri("ms-appx:///Images/black.png");
            image.Source = bm;
            image.VerticalAlignment = VerticalAlignment.Stretch;
            image.HorizontalAlignment = HorizontalAlignment.Stretch;
            (boardgrid.Children.ElementAt(index) as Grid).Children.Add(image);
        }

        private void AddWhite(int index)
        {
            Image image = new Image();
            BitmapImage bm = new BitmapImage();
            bm.UriSource = new Uri("ms-appx:///Images/white.png");
            image.Source = bm;
            image.VerticalAlignment = VerticalAlignment.Stretch;
            image.HorizontalAlignment = HorizontalAlignment.Stretch;
            (boardgrid.Children.ElementAt(index) as Grid).Children.Add(image);
        }

        private void ClearGame()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board.ChessBoard[i, j] != 0)
                    {
                        board.ChessBoard[i, j] = 0;
                        (boardgrid.Children.ElementAt(i * 15 + j) as Grid).Children.Clear();
                    }
                }
            }
            board.IsPlaying = false;
        }

        private async void Play(int row, int col)
        {
            int index = row * 15 + col;
            if (board.IsCurrentBlack)
            {
                AddBlack(index);
                board.Cursor.X = row;
                board.Cursor.Y = col;
                board.ChessBoard[row, col] = 1;
                board.IsCurrentBlack = !board.IsCurrentBlack;
                if (board.GameWin(1))
                {
                    await new MessageDialog("Black Win").ShowAsync();
                    return;
                }

                if (!board.IsPersonWhite)
                {
                    Point next = board.ComputerNextStep(2);
                    AddWhite(next.X * 15 + next.Y);
                    board.Cursor.X = next.X;
                    board.Cursor.Y = next.Y;
                    board.ChessBoard[next.X, next.Y] = 2;
                    board.IsCurrentBlack = !board.IsCurrentBlack;
                    if (board.GameWin(2))
                    {
                        await new MessageDialog("White Win").ShowAsync();
                    }
                }
            }
            else
            {
                AddWhite(index);
                board.Cursor.X = row;
                board.Cursor.Y = col;
                board.ChessBoard[row, col] = 2;
                board.IsCurrentBlack = !board.IsCurrentBlack;
                if (board.GameWin(2))
                {
                    await new MessageDialog("White Win").ShowAsync();
                    return;
                }

                if (!board.IsPersonBlack)
                {
                    Point next = board.ComputerNextStep(1);
                    AddBlack(next.X * 15 + next.Y);
                    board.Cursor.X = next.X;
                    board.Cursor.Y = next.Y;
                    board.ChessBoard[next.X, next.Y] = 1;
                    board.IsCurrentBlack = !board.IsCurrentBlack;
                    if (board.GameWin(1))
                    {
                        await new MessageDialog("Black Win").ShowAsync();
                    }
                }
            }
        }

        #endregion

        #region Control

        private void Board_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (!board.IsPlaying)
                return;

            Image image = sender as Image;
            Windows.Foundation.Point point = e.GetCurrentPoint(image).Position;
            int row, col;
            row = (int)Math.Floor(point.Y * 15 / image.ActualHeight);
            col = (int)Math.Floor(point.X * 15 / image.ActualWidth);
            Play(row, col);
        }

        private void WhiteMan_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (board.IsPlaying)
                return;

            FontIcon icon = (FontIcon)sender;
            if (icon.Tag as string == "robot")
            {
                icon.Glyph = "\uE13D";
                icon.Tag = "person";
                board.IsPersonWhite = true;
            }
            else
            {
                icon.Glyph = "\uE99A";
                icon.Tag = "robot";
                board.IsPersonWhite = false;
            }
        }

        private void BlackMan_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (board.IsPlaying)
                return;

            FontIcon icon = (FontIcon)sender;
            if (icon.Tag as string == "person")
            {
                icon.Glyph = "\uE99A";
                icon.Tag = "robot";
                board.IsPersonBlack = false;
            }
            else
            {
                icon.Glyph = "\uE13D";
                icon.Tag = "person";
                board.IsPersonBlack = true;
            }
        }

        private void Reset_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FontIcon icon = (FontIcon)sender;
            if (icon.Tag as string == "end")
            {
                icon.Glyph = "\uE15B";
                icon.Tag = "start";
                board.StartGame();
            }
            else
            {
                icon.Glyph = "\uE102"; 
                icon.Tag = "end";
                ClearGame();
            }
        }

        private void Return_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!board.IsPlaying)
                return;

            board.ChessBoard[board.Cursor.X, board.Cursor.Y] = 0;
            int index = board.Cursor.X * 15 + board.Cursor.Y;
            UIElementCollection children = (boardgrid.Children.ElementAt(index) as Grid).Children;
            if (children.Count() != 0)
            {
                children.Clear();
                board.IsCurrentBlack = !board.IsCurrentBlack;
                if (board.IsCurrentBlack)
                {
                    ; // blackCount--
                }
                else
                {
                    ; // whiteCount--
                }
            }
        }

        #endregion

    }
}
