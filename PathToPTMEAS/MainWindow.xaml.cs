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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PathToPTMEAS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            rbAllAxesTouch.IsChecked = true;
        }

        private void tbInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            CreatePTMEAS();

        }

        private void CreatePTMEAS()
        {
            try
            {
                string inputText = tbInput.Text.ToUpper();

                List<List<string>> points = new List<List<string>>();

                string[] splitInput = inputText.Split(new[] { ',' });
                List<int> pointStarts = new List<int>();
                for (int i = 0; i < splitInput.Length; i++)
                {
                    if (splitInput[i] == "PTDATA") pointStarts.Add(i);
                }

                string output = string.Empty;
                for (int i = 0; i < pointStarts.Count; i++)
                {
                    int start = pointStarts[i] + 1,
                        end = i == pointStarts.Count - 1 ? splitInput.Length : pointStarts[i + 1];
                    List<string> point = new List<string>();
                    point.Add("PTMEAS/CART");
                    for (int j = start; j < end; j++)
                    {
                        point.Add(splitInput[j]);
                    }
                    if ((bool)rbAllAxesTouch.IsChecked)
                        point.Add("ALLAXESTOUCH");
                    points.Add(point);
                }

                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = 0; j < points[i].Count; j++)
                    {
                        if (j > 0)
                            output += ",";
                        output += points[i][j];
                    }
                    output += Environment.NewLine;
                }

                if (output.Length == 0 && inputText.Trim().Length > 0)
                    output = "Input in incorrect format.";
                tbOutput.Text = output;
            }
            catch(Exception)
            {
                tbOutput.Text = "Input in incorrect format.";
            }
        }

        private void rbAllAxesTouch_Checked(object sender, RoutedEventArgs e)
        {
            CreatePTMEAS();   
        }

        private void rbAllAxesTouch_Unchecked(object sender, RoutedEventArgs e)
        {
            CreatePTMEAS();
        }
    }
}
