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
using System.Timers;

// http://www.wpf-tutorial.com/misc-controls/the-progressbar-control/  <--- procenta na progress baru


namespace tamagoci
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Zviratko pet;
        public MainWindow()
        {
            InitializeComponent();
            this.pet = new Zviratko(this);
            this.progress_play.Value = this.pet.Played;
            this.progress_eat.Value = this.pet.Eaten;
            this.progress_sleep.Value = this.pet.Slept;
        }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            //System.Environment.Exit(0);
            this.Close();
        }

        private void progress_play_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           //ProgressBar Foreground = "LightGreen";
            ProgressBar pplay = sender as ProgressBar;
           // this.progress_eat.Value = pplay.Value;
           this.pet.Played = pplay.Value;
        }

        private void progress_eat_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ProgressBar peat = sender as ProgressBar;
            this.pet.Eaten = peat.Value;
        }

        private void progress_sleep_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ProgressBar psleep = sender as ProgressBar;
            this.pet.Slept = psleep.Value;
        }

        private void do_play_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            this.progress_play.Value += 1;
        }

        private void do_eat_Click(object sender, RoutedEventArgs e)
        {
            this.progress_eat.Value += 1;
        }

        private void do_sleep_Click(object sender, RoutedEventArgs e)
        {
            this.progress_sleep.Value += 1;
        }

    }
}
