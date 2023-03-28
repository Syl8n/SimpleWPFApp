using System;
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
using System.Windows.Shapes;

namespace _301004212_Suh__ASS4
{
    /// <summary>
    /// Interaction logic for FindWindow.xaml
    /// </summary>
    public partial class FindWindow : Window
    {
        public string SearchId { get; set; } = string.Empty;
        public FindWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public delegate void SearchEventHandler(int id);
        public event SearchEventHandler SearchEvent;

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(SearchId, out int id);
            if(id > 0)
            {
                SearchEvent(id);
            }
            Window.GetWindow(this).Close();
        }
    }
}
