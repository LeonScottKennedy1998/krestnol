using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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

namespace krestnol
{

    public partial class MainWindow : Window
    {
        private Button[] buttons;
        private bool HumanTurn;

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[] { _1, _2, _3, _4, _5, _6, _7, _8, _9 };
            AllOff();
            HumanTurn = true;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            AllOn();
            foreach (Button button in buttons)
            {
                button.Content = " ";
            }
            HumanTurn = true;

            winlosedraw.Content = "Игра Крестики-Нолики";
        }

        private void AllOff()
        {
            foreach (var button in buttons)
            {
                button.IsEnabled = false;
            }
        }

        private void AllOn()
        {
            foreach (var button in buttons)
            {
                button.IsEnabled = true;
            }
        }

        private void _1_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content == " ")
            {
                button.Content = HumanTurn ? "X" : "O";
                button.IsEnabled = false;
                if (Winner())
                {
                    MessageBox.Show((HumanTurn ? "Крестики" : "Нолики") + " выиграли!");
                    AllOff();
                }
                else if (Draw())
                {
                    MessageBox.Show("Ничья!");
                    AllOff();
                }
                else
                {
                    HumanTurn = !HumanTurn;
                    if (!HumanTurn)
                    {
                        Move();
                    }
                }
            }
        }

        private void Move()
        {
            Random random = new Random();
            if (buttons.Any(button => button.Content == " "))
            {
                int combutt;
                do
                {
                    combutt = random.Next(0, 9);
                } while (buttons[combutt].Content != " ");

                buttons[combutt].Content = "O";
                buttons[combutt].IsEnabled = false;

                if (Winner())
                {
                   MessageBox.Show((HumanTurn ? "Крестики" : "Нолики") + " выиграли!");
                    AllOff();
                }
                else if (Draw())
                {
                    MessageBox.Show("Ничья");
                    AllOff();
                }

                HumanTurn = !HumanTurn;
            }
        }

        private bool Winner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i * 3].Content != " " && buttons[i * 3].Content == buttons[i * 3 + 1].Content && buttons[i * 3].Content == buttons[i * 3 + 2].Content)
                {
                    return true;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (buttons[i].Content != " " && buttons[i].Content == buttons[i + 3].Content && buttons[i].Content == buttons[i + 6].Content)
                {
                    return true;
                }
            }

            if (buttons[0].Content != " " && buttons[0].Content == buttons[4].Content && buttons[0].Content == buttons[8].Content)
            {
                return true;
            }

            if (buttons[2].Content != " " && buttons[2].Content == buttons[4].Content && buttons[2].Content == buttons[6].Content)
            {
                return true;
            }

            return false;
        }

        private bool Draw()
        {
            foreach (Button button in buttons)
            {
                if (button.Content == " ")
                {
                    return false;
                }
            }
            return !Winner();
        }
        
    }
}
//Смены игроков нет, увы :(