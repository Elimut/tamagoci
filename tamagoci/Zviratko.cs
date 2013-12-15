using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace tamagoci
{
    class Zviratko 
    {
        private MainWindow mw;
#if TIMER
        private Timer timer; //mřížka define TIMER
#else
        private DispatcherTimer timer; //běží toto
#endif
        private double played;
        private double eaten;
        private double slept;
        public Zviratko(MainWindow mw) {
          
            played = 50;
            eaten = 10;
            slept = 90;
            this.mw = mw;

            #if TIMER
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 500;
            timer.Enabled = true; //totéž co Start()

            #else
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            #endif
        }
        private void OnTimedEvent(object source, /*Elapsed*/EventArgs e)
        {
            this.played -= 1;
            this.eaten -=  1;
            this.slept -= 1;

            if (played > 0 && eaten > 0 && slept > 0)
            {
                this.mw.progress_play.Value = this.Played;
                this.mw.progress_eat.Value = this.Eaten;
                this.mw.progress_sleep.Value = this.Slept;
            }
            else
            {
               MessageBoxResult result = MessageBox.Show(
                   "Animal is dead. Do you want to quit?",
                   "OK",
                       MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No);
               if (result == MessageBoxResult.Yes) {
                   mw.Close();
               }
            }
        }

        public double Played
        {
            get
            {
                return this.played;
            }

            set
            {
                this.played = value;
            }
        }
        public double Eaten
        {
            get
            {
                return this.eaten;
            }

            set
            {
                this.eaten = value;
            }
        }
        public double Slept
        {
            get
            {
                return this.slept;
            }

            set
            {
                this.slept = value;
            }
        }

    }
}
