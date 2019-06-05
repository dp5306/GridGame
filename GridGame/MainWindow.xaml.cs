using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace GridGame
{
    public partial class MainWindow : Window
    {
        private GridElement[] bombs;    //Array which holds all the bomb objects
        private GridElement player;     //Car object
        private Random r = new Random();
        long milis;
        int id = -1;

        public MainWindow()
        {
            InitializeComponent();

            File.AppendAllText(@"Logs.txt", "New game:\n");

            StartGame();

            this.KeyDown += new KeyEventHandler(MainWindow_KeyHandler);
        }

        private void StartGame()
        {
            removeOld();

            milis = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            id++;
            bombs = new GridElement[15];
            player = new GridElement(0, 0, r.Next(0, 10), 1);


            // Setting location of the bombs
            for (var i = 0; i < bombs.Length; i++)
            {
                bombs[i] = new GridElement(i+1, r.Next(1, 10), r.Next(0, 10), 0);
            }

            drawBombs();
            player.draw(MainGrid);  // Draws the player car
        }

        // Draws all bombs in the array
        private void drawBombs()
        {
            if (bombs == null)
                return;

            foreach (GridElement x in bombs)
            {
                x.draw(MainGrid);
            }
        }

        // Removes old player and bombs from screen
        private void removeOld()
        {
            if (bombs != null)
            {
                foreach (GridElement x in bombs)
                {
                    x.remove(MainGrid);
                }
            }

            if (player != null)
                player.remove(MainGrid);
        }

        // Handles keyboard presses
        private void MainWindow_KeyHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.Up)
                player.addRow(-1);

            else if (e.Key == Key.S || e.Key == Key.Down)
                player.addRow(1);

            else if (e.Key == Key.D || e.Key == Key.Right)
                player.addCol(1);

            else if (e.Key == Key.A || e.Key == Key.Left)
                player.addCol(-1);

            player.draw(MainGrid);

            checkEndCondition();
        }

        // Check if game is over
        private void checkEndCondition()
        {
            foreach (GridElement x in bombs)
            {
                if (x.checkCollision(player))
                {
                    long time = DateTimeOffset.Now.ToUnixTimeMilliseconds() - milis;
                    File.AppendAllText(@"Logs.txt", id.ToString() + "|0|" + time.ToString() + "|" + x.getId() + "\n");
                    StartGame();
                }
            }

            if (player.getColumn() == 9)
            {
                long time = DateTimeOffset.Now.ToUnixTimeMilliseconds() - milis;
                File.AppendAllText(@"Logs.txt", id.ToString() + "|1|" + time.ToString() + "|-1\n");
                StartGame();
            }
        }

    }
}
