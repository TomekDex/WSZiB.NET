using System;
using System.Data;
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

namespace WpfCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string rownanie = "21+2/2-5*2";
        int indexCounter = 0;
        string wyniczek;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void przyciskPrzeliczenia_Click(object sender, RoutedEventArgs e)
        {
            //wyniczek = Calculator.Wynik(rownanie);
            wyniczek = Calculator.Wynik(rownanie, indexCounter, out int IndexCounterNew);
            wynikPrzeliczenia.Text = wyniczek;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            rownanie = podaneRownanie.Text;
        }


    }
}
