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

        private List<string> appsToClose;
        private List<string> appsToOpen;



        public MainWindow()
        {
            InitializeComponent();
        }



      

        private void AddProgramToCloseList(string programPath)
        {
            var path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));
            System.IO.File.AppendAllText(path, programPath);
        }


        private void ShowAddEventWindow_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            appsToOpen = new List<string>();

            appsToOpen.Add("Notepad++");
            appsToOpen.Add("msedge");


            foreach (var appToOpen in appsToOpen)
            {
                Process.Start(appToOpen);
            }
        }

        private void Close_Apps_Click(object sender, RoutedEventArgs e)
        {
            appsToClose = new List<string>();
            appsToClose.Add("Notepad++");
            appsToClose.Add("msedge");


            foreach (var appToClose in appsToClose)
            {
                foreach (Process p in Process.GetProcessesByName(appToClose))
                {
                    p.Kill();
                }
            }
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
