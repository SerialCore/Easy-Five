using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        private Playing playing = new Playing();
        private bool IsCurrentBlack = true;

        private void AddBlack(int index)
        {
            Image image = new Image();
            BitmapImage bm = new BitmapImage();
            bm.UriSource = new Uri("ms-appx:///Images/black.png");
            image.Source = bm;
            image.VerticalAlignment = VerticalAlignment.Stretch;
            image.HorizontalAlignment = HorizontalAlignment.Stretch;
            (board_grid.Children.ElementAt(index) as Grid).Children.Add(image);
        }

        private void AddWhite(int index)
        {
            Image image = new Image();
            BitmapImage bm = new BitmapImage();
            bm.UriSource = new Uri("ms-appx:///Images/white.png");
            image.Source = bm;
            image.VerticalAlignment = VerticalAlignment.Stretch;
            image.HorizontalAlignment = HorizontalAlignment.Stretch;
            (board_grid.Children.ElementAt(index) as Grid).Children.Add(image);
        }

        private void ClearGame()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (playing.ChessBoard[i, j] != 0)
                    {
                        playing.ChessBoard[i, j] = 0;
                        (board_grid.Children.ElementAt(i * 15 + j) as Grid).Children.Clear();
                    }
                }
            }
            playing.whiteCount = 0;
            playing.blackCount = 0;
            black_count.Text = playing.blackCount.ToString();
            white_count.Text = playing.whiteCount.ToString();
            IsCurrentBlack = true;
        }

        private async void Play(int row, int col)
        {
            int index = row * 15 + col;
            if (IsCurrentBlack)
            {
                AddBlack(index);
                playing.Cursor[0] = row;
                playing.Cursor[1] = col;
                playing.ChessBoard[row, col] = 1;
                IsCurrentBlack = !IsCurrentBlack;

                playing.blackCount++;
                black_count.Text = playing.blackCount.ToString();
                if (playing.Win(1))
                {
                    await new MessageDialog("Black Win").ShowAsync();
                    ClearGame();
                }
            }
            else
            {
                AddWhite(index);
                playing.Cursor[0] = row;
                playing.Cursor[1] = col;
                playing.ChessBoard[row, col] = 2;
                IsCurrentBlack = !IsCurrentBlack;

                playing.whiteCount++;
                white_count.Text = playing.whiteCount.ToString();
                if (playing.Win(2))
                {
                    await new MessageDialog("White Win").ShowAsync();
                    ClearGame();
                }
            }
        }

        private void board_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Image image = sender as Image;
            Point point = e.GetCurrentPoint(image).Position;
            int row, col;
            row = (int)Math.Floor(point.Y * 15 / image.ActualHeight);
            col = (int)Math.Floor(point.X * 15 / image.ActualWidth);
            Play(row, col);
        }

        private void Reset_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ClearGame();
        }

        private void Return_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int index = playing.Cursor[0] * 15 + playing.Cursor[1];
            UIElementCollection children = (board_grid.Children.ElementAt(index) as Grid).Children;
            if (children.Count() != 0)
            {
                children.Clear();
                IsCurrentBlack = !IsCurrentBlack;
                if (IsCurrentBlack)
                {
                    playing.blackCount--;
                    black_count.Text = playing.blackCount.ToString();
                }
                else
                {
                    playing.whiteCount--;
                    white_count.Text = playing.whiteCount.ToString();
                }
            }
        }
    }
}
