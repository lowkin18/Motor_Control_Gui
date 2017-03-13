using System;
using System.IO.Ports;
using System.Windows;

namespace sensors_gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private setup_content setup = new setup_content();  // setup object
        private keypad_gui keypad = new keypad_gui();       // setup keypad gui object
        private SerialPortProgram port_con;                 // instantiate Serial Port

        public MainWindow()
        {
            InitializeComponent();

            getAvailablePorts();
            contentControl.Content = setup;
        }

        private void getAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames(); //get serial ports
            foreach (string s in ports)
            {
                setup.port_box.Items.Add(s);            //display in comboBox
            }
        }

        //Button Click event
        private void connect_button_Click(object sender, RoutedEventArgs e)
        {
            if (connect_button.Content.ToString() == "Disconnect") //If already connected disconnect
            {
                contentControl.Content = setup;  //Set the Content control
                enter_button.Visibility = Visibility.Hidden; //Hide Button
                connect_button.Content = "Connect";         //Add Connect Button
                port_con.disconnect();                      //Call function Disconnect
                update_button.Visibility = Visibility.Hidden;
                slider.Visibility = Visibility.Hidden;
                pwm_progress.Visibility = Visibility.Hidden;
                pwmVal_label.Visibility = Visibility.Hidden;
                pwm_label.Visibility = Visibility.Hidden;
            }
            else
            {
                //exception handling
                try
                {
                    if (setup.checkData())
                    {
                        System.Windows.MessageBox.Show("You have entered an incorrect configuration");
                    }
                    else
                    {
                        port_con = new SerialPortProgram(setup.port_box.Text, int.Parse(setup.baudrate_box.Text));
                        contentControl.Content = keypad;
                        connect_button.Content = "Disconnect";
                        enter_button.Visibility = Visibility.Visible;
                        update_button.Visibility = Visibility.Visible;
                        slider.Visibility = Visibility.Visible;
                        pwm_progress.Visibility = Visibility.Visible;
                        pwmVal_label.Visibility = Visibility.Visible;
                        pwm_label.Visibility = Visibility.Visible;
                    }
                }
                catch
                {
                }
            }
        }

        //Connection Button Clicked
        private void enter_button_Click(object sender, RoutedEventArgs e)
        {
            port_con.send_data(keypad);
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            pwmVal_label.Content = slider.Value.ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            byte charVal = Convert.ToByte(slider.Value);
            port_con.send_pwm(charVal);
        }
    }

    // Serial Port Class - Connects to the com-Port for UART communication to MSP430
    public class SerialPortProgram

    {
        private SerialPort port_connect;
        private string v_prof = "emt";

        public SerialPortProgram(string port, int baudrate)
        {
            port_connect = new SerialPort(port, baudrate, Parity.None, 8, StopBits.One);
            port_connect.Open();
        }

        //Function sends the data two the COM port to MSP430
        //this function will send the PWM duty cycle byte and direction
        //function will also send the type of speed control
        public void send_data(keypad_gui key)
        {
            byte byter;
            byte[] bytes = { 0 };
            if (String.ReferenceEquals("Rapid", key.velocity_text.Text.ToString()) && !String.ReferenceEquals(v_prof, key.velocity_text.Text.ToString()))
            {
                byter = (byte)Convert.ToSByte(101);
                bytes[0] = byter;
                port_connect.Write(bytes, 0, 1);
            }
            if (String.ReferenceEquals("Ramp", key.velocity_text.Text.ToString()) && !String.ReferenceEquals(v_prof, key.velocity_text.Text.ToString()))
            {
                byter = (byte)Convert.ToSByte(102);
                bytes[0] = byter;
                port_connect.Write(bytes, 0, 1);
            }
            v_prof = key.velocity_text.Text.ToString();

            byter = (byte)Convert.ToSByte(key.data_text.Text);
            bytes[0] = byter;
            port_connect.Write(bytes, 0, 1);
        }

        //Function sends the data two the COM port to MSP430
        //This function will change the PWM period based on slider
        public void send_pwm(byte data)
        {
            byte val = 103;
            byte val1 = data;
            byte[] bytes = { val, val1 };
            port_connect.Write(bytes, 0, 2);
        }

        public void disconnect()
        {
            port_connect.Close();
        }
    }
}