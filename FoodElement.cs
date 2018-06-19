using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame
{
    class FoodElement
    {
        public Point Position { get; set; }
        public Ellipse Food { get; set; }

        public FoodElement(Point position)
        {
            Position = position;
            Food = new Ellipse();
            Food.Width = 10;
            Food.Height = 10;
            Food.Fill = new SolidColorBrush(Colors.Red);
        }
    }
}
