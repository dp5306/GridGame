using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GridGame
{
    public class GridElement
    {
        int id;
        int column;
        int row;
        int objectType;
        System.Windows.Controls.Image test;
        bool initialized;

        public GridElement(int id, int column, int row, int objectType)
        {
            this.id = id;
            this.column = column;
            this.row = row;
            this.objectType = objectType;
            this.test = new System.Windows.Controls.Image();
            this.initialized = false;
        }

        public bool checkCollision(GridElement e)
        {
            return this.column == e.column && this.row == e.row;
        }

        public int getColumn()
        {
            return this.column;
        }

        public int getRow()
        {
            return this.row;
        }

        public int getId()
        {
            return this.id;
        }

        public void addCol(int toAdd)
        {
            this.column = Math.Max(Math.Min(this.column + toAdd, 9), 0);
        }

        public void addRow(int toAdd)
        {
            this.row = Math.Max(Math.Min(this.row + toAdd, 9), 0);
        }

        public void draw(System.Windows.Controls.Grid MainGrid)
        {
            if (!this.initialized)
            {
                this.initialized = true;
                String imagePath = "./car.png";
                if (objectType == 0)
                    imagePath = "./bomb.png";

                this.test = new System.Windows.Controls.Image();
                BitmapImage bmp = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                test.Source = bmp;
                MainGrid.Children.Add(test);
            }

            Grid.SetColumn(test, column);
            Grid.SetRow(test, row);
        }

        public void remove(System.Windows.Controls.Grid MainGrid)
        {
            MainGrid.Children.Remove(test);
        }
    }
}
