using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int turn = 1;
        int pOneWins = 0;
        int pTwoWins = 0;
        bool isWinner = false;
        int[,] state = new int[,]{ { -1, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } };
        public MainWindow()
        {
            InitializeComponent();
        }
        void HandleClick(Object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int i=-1, j=-1;
            try { j = Int32.Parse(b.Name[2].ToString()); } catch (FormatException) { return; }
            try { i = Int32.Parse(b.Name[1].ToString()); } catch (FormatException) { return; }
            
            
            if (state[i,j]!=-1||isWinner) {
                return;
            }
            if (turn == 1)
            {
                (sender as Button).Content = "X";
                state[i, j] = turn;
            }
            else {
                (sender as Button).Content = "O";
                state[i, j] = turn;
            }
            
            if (CheckWinner(turn)==1) {
                //b.Content = "Winner!";
                TextBlock wt = this.FindName("winText") as TextBlock;
                isWinner = true;
                if (turn == 1)
                {
                    pOneWins++;
                    TextBlock t = this.FindName("playerOneWins") as TextBlock;
                    t.Text = pOneWins.ToString();
                    wt.Text = "Player One Wins!";
                }
                else {
                    pTwoWins++;
                    TextBlock t = this.FindName("playerTwoWins") as TextBlock;
                    t.Text = pTwoWins.ToString();
                    wt.Text = "Player Two Wins!";
                }
            }
            turn ^= 1;
        }
        int CheckWinner(int lastMove) {
            
            for (int i = 0;i<state.GetLength(0);i++) {
                if (state[i, 0] == lastMove && state[i,1]==lastMove&&state[i,2]==lastMove)
                {
                    return 1;
                }
            }

            for (int j = 0; j<state.GetLength(1);j++) {
                if (state[0, j] == lastMove && state[1, j] == lastMove && state[2, j] ==lastMove) {
                    return 1;
                }
            }
            
            if (state[0, 0] == lastMove && state[1, 1] == lastMove && state[2, 2] == lastMove) {
                return 1;
            }

            if (state[0,2]==lastMove&&state[1,1]==lastMove&&state[2,0]==lastMove) {
                return 1;
            }
            return 0;
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            turn = 1;
            isWinner = false;
            for (int i = 0;i<state.GetLength(0);i++) {
                for (int j = 0;j<state.GetLength(1);j++) {
                    state[i,j]=-1;
                }
            }
            UniformGrid grid = this.FindName("gameBoard") as UniformGrid;
            TextBlock tb = this.FindName("winText") as TextBlock;
            tb.Text = "";
            for (int i = 0;i<grid.Children.Count;i++) {
                (grid.Children[i] as Button).Content = "";
            }
        }
    }
}
