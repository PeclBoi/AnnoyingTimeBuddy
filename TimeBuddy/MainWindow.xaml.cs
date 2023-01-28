using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Shapes;

namespace TimeBuddy
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            Details1 details = new Details1();
            details.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
              string EventFilePath = @"C:\Users\alexa\Documents\TimeEvents\events.txt";

            Output_Lb.Content = System.IO.File.ReadAllText(EventFilePath);
    }
    }
}
