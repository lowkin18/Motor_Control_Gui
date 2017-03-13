using System.Windows.Controls;

namespace sensors_gui
{
    /// <summary>
    /// Interaction logic for setup_content.xaml
    /// </summary>
    public partial class setup_content : UserControl
    {
        private string[] combo_box;

        public setup_content()
        {
            InitializeComponent();
        }

        public string[] port_combo
        {
            get { return combo_box; }
            set
            {
                combo_box = value;
                foreach (string s in combo_box)
                {
                    port_box.Items.Add(s);
                }
            }
        }

        public bool checkData()
        {
            if (port_box.Text == "" || baudrate_box.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string baud_combo
        {
            get { return baudrate_box.Text; }
        }
    }
}