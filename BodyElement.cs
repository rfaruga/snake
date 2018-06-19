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
    class BodyElement
    {
        public Point Position { get; set; }
        public Rectangle Body { get; set; }

        public BodyElement(Point position)
        {
            Position = position;
            Body = new Rectangle();
            Body.Width = 10;
            Body.Height = 10;
            Body.Fill = new SolidColorBrush(Colors.Yellow);
        }
    }
}
