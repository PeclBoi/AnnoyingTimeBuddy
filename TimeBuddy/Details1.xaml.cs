using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TimeBuddy
{
    /// <summary>
    /// Interaktionslogik für Details1.xaml
    /// </summary>
    public partial class Details1 : Window
    {
        public Details1()
        {
            InitializeComponent();
        }

        static string EventFilePath = @"C:\Users\alexa\Documents\TimeEvents";
        static string path = System.IO.Path.Combine(EventFilePath, "events.txt");


        private void Add_Programm_Click(object sender, RoutedEventArgs e)
        {
            int timeOut = 0;
            if (Time_tb.Text != string.Empty)
            {
                int hour = Convert.ToInt32(Time_tb.Text.Split(':')[0]);
                int min = Convert.ToInt32(Time_tb.Text.Split(':')[1]);

                timeOut = ConvertTimeToTimer(hour, min);
            }

            if (timer_s.Text != string.Empty)
            {
                timeOut = Convert.ToInt32(timer_s.Text);
            }

            var checkedValue = StackPanel_1.Children.OfType<RadioButton>()
                 .FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value);

            ComboBoxItem typeItem = (ComboBoxItem)c_Box.SelectedItem;
            string value = typeItem.Content.ToString();

            bool open = checkedValue.Content.ToString() == "Open";

            StartEvent(value, open, timeOut);
            AddProgramToList(value, open, timeOut);

        }


        private void StartEvent(string value, bool open, int v)
        {
            if (open)
            {
                Task.Delay(TimeSpan.FromMilliseconds(v * 1000)).ContinueWith(task => openApps(value, v));
            }
            else
            {
                Task.Delay(TimeSpan.FromMilliseconds(v * 1000)).ContinueWith(task => closeApps(value, v));
            }
        }

        private void closeApps(string value, int v)
        {
            var lines = System.IO.File.ReadAllLines(path).ToList();
            lines.Remove($"{value}, False, {v}");
            System.IO.File.WriteAllLines(path, lines);

            foreach (Process p in Process.GetProcessesByName(value))
            {
                p.Kill();
            }

        }

        private int ConvertTimeToTimer(int future_hour, int future_minutes)
        {
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            int current_hours = currentTime.Hours;
            int current_minutes = currentTime.Minutes;

            int timer_hours;
            int timer_minutes;
            int set_seconds;
            if (current_minutes < future_minutes)
            {
                timer_hours = future_hour - current_hours;
                timer_minutes = future_minutes - current_minutes;
                set_seconds = timer_hours * 60 * 60 + timer_minutes * 60;
            }
            else
            {
                timer_hours = future_hour - current_hours - 1;
                timer_minutes = future_minutes + 60 - current_minutes;
                set_seconds = timer_hours * 60 * 60 + timer_minutes * 60;
                if (set_seconds < 0)
                    set_seconds = set_seconds + 24 * 60 * 60;
            }
            return set_seconds;
        }

        private static void openApps(string app, int v)
        {

            var lines = System.IO.File.ReadAllLines(path).ToList();

            lines.Remove($"{app}, True, {v}");

            System.IO.File.WriteAllLines(path, lines);

            try
            {
                if (app == "Discord")
                {
                    Process discord = new Process();
                    discord.StartInfo.FileName = "C://Users//alexa//AppData//Local//Discord//Update.exe";
                    discord.StartInfo.Arguments = "--processStart Discord.exe";
                    discord.Start();
                }
                else
                {
                    Process.Start(app);
                }
            }
            catch
            {
                MessageBox.Show("Der Prozess konnte nicht gestartet werden.", "Fehlender Pfad");
            }

        }

        private void AddProgramToList(string programPath, bool open, int timeout)
        {

            string formatString = $"{programPath}, {open}, {timeout}\r\n";



            System.IO.File.AppendAllText(path, formatString);
        }

    }
}
