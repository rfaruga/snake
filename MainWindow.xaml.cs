// Rafał Faruga
// Silesian University of Technology
// https://github.com/rfaruga
// http://linkedin.com/in/rfaruga
// Project: Snake WPF
 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeGame
{
    public partial class MainWindow : Window
    {
        private bool CanChangeDirect;
        private DispatcherTimer Timer;
        private int Score = 3;
        private bool Start;
        private Direct DirectionOfMovement;
        private List<FoodElement> FoodList;
        private List<BodyElement> BodyList;
        public MainWindow()
        {
            CanChangeDirect = false;
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(dispatcherTimer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            Start = false;
            DirectionOfMovement = Direct.Stop;
            FoodList = new List<FoodElement>();
            BodyList = new List<BodyElement>();
            InitializeComponent();
            StartNewGame();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Move();
        }
        private void StartNewGame()
        {
            PauseResume.IsEnabled = true;
            GameOverText.Visibility = Visibility.Hidden;
            Up.Visibility = Visibility.Visible;
            Down.Visibility = Visibility.Visible;
            Left.Visibility = Visibility.Visible;
            Right.Visibility = Visibility.Visible;

            for (int i = 0; i < BodyList.Count; i++)
            {
                Board.Children.Remove(BodyList.ElementAt(i).Body);
            }

            for (int i = 0; i < FoodList.Count; i++)
            {
                Board.Children.Remove(FoodList.ElementAt(i).Food);
            }
            FoodList.Clear();

            Score = 3;
            ScoreText.Text = "Wynik: " + Score;

            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                FoodList.Add(new FoodElement(new Point(rand.Next(0, 64) * 10, rand.Next(0, 44) * 10)));
                Board.Children.Add(FoodList[i].Food);
                Canvas.SetTop(FoodList[i].Food, FoodList[i].Position.Y);
                Canvas.SetLeft(FoodList[i].Food, FoodList[i].Position.X);
            }

            BodyList.Clear();

            for (int i = 0; i < 3; i++)
            {
                BodyList.Add(new BodyElement(new Point(320, 220 - 10 * i)));

                Board.Children.Add(BodyList[i].Body);
                Canvas.SetTop(BodyList[i].Body, BodyList[i].Position.Y);
                Canvas.SetLeft(BodyList[i].Body, BodyList[i].Position.X);
            }

            DirectionOfMovement = Direct.Up;
        }
        private void StartRestert_Click(object sender, RoutedEventArgs e)
        {
            if (Start == true)
            {
                Timer.Stop();
                Start = false;
                StartRestart.Content = "Start (SPACJA)";
                StartNewGame();
            }
            else
            {
                CanChangeDirect = true;
                Timer.Start();
                Start = true;
                StartRestart.Content = "Restart (SPACJA)";
                if (PauseResume.Content.Equals("Wznów (ESC)"))
                {
                    StartRestart.Content = "Start (SPACJA)";
                    StartNewGame();
                    Start = false;
                }
            PauseResume.Content = "Pauza (ESC)";
            }
        }
        private void PauseResume_Click(object sender, RoutedEventArgs e)
        {
            if (Start == true && StartRestart.Content.Equals("Restart (SPACJA)"))
            {
                Start = false;
                Timer.Stop();
                PauseResume.Content = "Wznów (ESC)";
            }
            else if (Start == false && StartRestart.Content.Equals("Restart (SPACJA)"))
            {
                Start = true;
                CanChangeDirect = true;
                Timer.Start();
                PauseResume.Content = "Pauza (ESC)";
            }
        }
        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if (DirectionOfMovement != Direct.Down && GameOverText.Visibility != Visibility.Visible && CanChangeDirect)
            {
                DirectionOfMovement = Direct.Up;
                CanChangeDirect = false;
            }           
        }
        private void Down_Click(object sender, RoutedEventArgs e)
        {
            if (DirectionOfMovement != Direct.Up && GameOverText.Visibility != Visibility.Visible && CanChangeDirect)
            { 
                DirectionOfMovement = Direct.Down;
                CanChangeDirect = false;
            }
        }
        private void Left_Click(object sender, RoutedEventArgs e)
        {
            if (DirectionOfMovement != Direct.Right && GameOverText.Visibility != Visibility.Visible && CanChangeDirect)
            {
                DirectionOfMovement = Direct.Left;
                CanChangeDirect = false;
            }
        }
        private void Right_Click(object sender, RoutedEventArgs e)
        {
            if(DirectionOfMovement != Direct.Left && GameOverText.Visibility != Visibility.Visible && CanChangeDirect)
            { 
                DirectionOfMovement = Direct.Right;
                CanChangeDirect = true;
            }
        }
        private void Move()
        {
            if (Start == true && DirectionOfMovement != Direct.Stop)
            {
                switch (DirectionOfMovement)
                {
                    case Direct.Up:
                        BodyList.Add(new BodyElement(new Point(BodyList[BodyList.Count - 1].Position.X,
                            BodyList[BodyList.Count - 1].Position.Y - 10)));
                        break;
                    case Direct.Down:
                        BodyList.Add(new BodyElement(new Point(BodyList[BodyList.Count - 1].Position.X,
                            BodyList[BodyList.Count - 1].Position.Y + 10)));
                        break;
                    case Direct.Left:
                        BodyList.Add(new BodyElement(new Point(BodyList[BodyList.Count - 1].Position.X - 10,
                            BodyList[BodyList.Count - 1].Position.Y)));
                        break;
                    case Direct.Right:
                        BodyList.Add(new BodyElement(new Point(BodyList[BodyList.Count - 1].Position.X + 10,
                            BodyList[BodyList.Count - 1].Position.Y)));
                        break;
                }

                CanChangeDirect = true;

                if (BodyList[BodyList.Count - 1].Position.X < 0 ||
                    BodyList[BodyList.Count - 1].Position.X > 640 ||
                    BodyList[BodyList.Count - 1].Position.Y < 0 ||
                    BodyList[BodyList.Count - 1].Position.Y > 440)
                {
                    GameOver();
                    return;
                }

                for (int j = 0; j < BodyList.Count-1; j++)
                {
                    if (Convert.ToInt32(BodyList[BodyList.Count - 1].Position.X) ==
                        Convert.ToInt32(BodyList[j].Position.X))
                    {
                        if (Convert.ToInt32(BodyList[BodyList.Count - 1].Position.Y) ==
                            Convert.ToInt32(BodyList[j].Position.Y))
                        {
                            GameOver();
                            return;
                        }
                    }
                }

                Board.Children.Add(BodyList[BodyList.Count - 1].Body);
                Canvas.SetTop(BodyList[BodyList.Count - 1].Body,
                    BodyList[BodyList.Count - 1].Position.Y);
                Canvas.SetLeft(BodyList[BodyList.Count - 1].Body,
                    BodyList[BodyList.Count - 1].Position.X);

                for (int i = 0; i < FoodList.Count; i++)
                {
                    if (Convert.ToInt32(BodyList[BodyList.Count - 1].Position.X) == Convert.ToInt32(FoodList[i].Position.X))
                    {
                        if (Convert.ToInt32(BodyList[BodyList.Count - 1].Position.Y) == Convert.ToInt32(FoodList[i].Position.Y))
                        {
                            ScoreText.Text = "Wynik: " + ++Score;
                            Random rand = new Random();
                            Board.Children.Remove(FoodList[i].Food);
                            FoodList.RemoveAt(i);
                            FoodList.Add(new FoodElement(new Point(rand.Next(0, 64) * 10, rand.Next(0, 44) * 10)));
                            Board.Children.Add(FoodList[9].Food);
                            Canvas.SetTop(FoodList[9].Food, FoodList[9].Position.Y);
                            Canvas.SetLeft(FoodList[9].Food, FoodList[9].Position.X);
                            return;
                        }
                    }
                }

                Board.Children.Remove(BodyList[0].Body);
                BodyList.Remove(BodyList.ElementAt(0));
            }
        }
        private void GameOver()
        {
            PauseResume.IsEnabled = false;
            GameOverText.Visibility = Visibility.Visible;
            Start = true;
            DirectionOfMovement = Direct.Stop;
            Up.Visibility = Visibility.Hidden;
            Down.Visibility = Visibility.Hidden;
            Left.Visibility = Visibility.Hidden;
            Right.Visibility = Visibility.Hidden;
        }
    }
}