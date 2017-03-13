using System;
using System.Windows;
using System.Windows.Controls;

namespace sensors_gui
{
    /// <summary>
    /// Interaction logic for keypad_gui.xaml
    /// </summary>
    public partial class keypad_gui : UserControl
    {
        private int profile;
        private bool speed_direction;

        public keypad_gui()
        {
            InitializeComponent();
            speed_direction = true;
            int profile = 0;
        }

        public void text_add(string num)
        {
            if (Int32.Parse(data_text.Text) == 100 || Int32.Parse(data_text.Text) > 10 || Int32.Parse(data_text.Text) < -10)
            {
                data_text.Text = "0";
            }
            if (Int32.Parse(data_text.Text) == 10 && Int32.Parse(num) > 1)
            {
                data_text.Text = "0";
            }
            int number = Math.Abs(Int32.Parse(data_text.Text) * 10) + Int32.Parse(num);
            if (number > 100) number = 100;
            if (number < -100) number = -100;

            if (speed_direction && number < 0)
            {
                number *= -1;
            }
            if (!speed_direction && number > 0)
            {
                number *= -1;
            }

            data_text.Text = number.ToString();
        }

        private void button_one_Click(object sender, RoutedEventArgs e)
        {
            text_add("1");
        }

        private void button_two_Click(object sender, RoutedEventArgs e)
        {
            text_add("2");
        }

        private void button_D_Click(object sender, RoutedEventArgs e)
        {
            data_text.Text = "0";
        }

        private void button_A_Click(object sender, RoutedEventArgs e)
        {
            speed_direction = true;
            int speed = Int32.Parse(data_text.Text);
            if (speed < 0) speed *= -1;
            data_text.Text = speed.ToString();
        }

        private void button_B_Click(object sender, RoutedEventArgs e)
        {
            data_text.Text = "0";
        }

        private void button_C_Click(object sender, RoutedEventArgs e)
        {
            speed_direction = false;
            int speed = Int32.Parse(data_text.Text);
            if (speed > 0) speed *= -1;
            data_text.Text = speed.ToString();
        }

        private void button_three_Click(object sender, RoutedEventArgs e)
        {
            text_add("3");
        }

        private void button_four_Click(object sender, RoutedEventArgs e)
        {
            text_add("4");
        }

        private void button_five_Click(object sender, RoutedEventArgs e)
        {
            text_add("5");
        }

        private void button_six_Click(object sender, RoutedEventArgs e)
        {
            text_add("6");
        }

        private void button_seven_Click(object sender, RoutedEventArgs e)
        {
            text_add("7");
        }

        private void button_eight_Click(object sender, RoutedEventArgs e)
        {
            text_add("8");
        }

        private void button_nine_Click(object sender, RoutedEventArgs e)
        {
            text_add("9");
        }

        private void button_zero_Click(object sender, RoutedEventArgs e)
        {
            text_add("0");
        }

        private void V_profile_Click(object sender, RoutedEventArgs e)
        {
            switch (profile)
            {
                case 0:
                    velocity_text.Text = "Ramp";
                    break;

                case 1:
                    velocity_text.Text = "Rapid";
                    break;

                case 2:
                    velocity_text.Text = "Sinusoidal";
                    break;

                case 3:
                    velocity_text.Text = "S-Curve";
                    break;
            }

            profile++;
            if (profile == 4)
            { profile = 0; }
        }
    }
}