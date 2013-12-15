using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Win32;//SaveFileDialog
using System.IO;//StreamWriter

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
        private string name;//jmeno zviratka, pouzito i pro ulozeni do souboru
        public Zviratko(MainWindow mw, string name) {
            this.name = name; 
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
        //ukladani doladit na nejaky format? XML?
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0},{1},{2},{3}", name, Played, Eaten, Slept);
            return sb.ToString();
        }

        //cas potrebujeme nekdy zastavit/spustit
        public void start_stop()
        {
            if (timer.IsEnabled) { timer.Stop(); } else { timer.Start();  }
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
        //ukladani, viz John Sharp: Visual C# 2010, strana 475
        public void save ()
        {
            //behem dialogu ukladani potrebujeme zastavit cas/timer!
            bool timerEnabled = this.timer.IsEnabled;
            if (timerEnabled) { this.timer.Stop(); } //pokud timer bezi, zastavime
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "txt";
            dlg.AddExtension = true;
            dlg.FileName = "Zviratko";
            dlg.InitialDirectory = @"c:\Users\ela\Documents\";
            dlg.OverwritePrompt = true;
            dlg.Title = "Zviratko";
            dlg.ValidateNames = true;
            if (dlg.ShowDialog().Value)
            {
                using (StreamWriter writer = new StreamWriter(dlg.FileName))
                {
                    writer.WriteLine(this.ToString());
                }
                MessageBox.Show("Udaje Zviratka byly ulozeny", "Ulozeno");
            }
            if (timerEnabled) { this.timer.Start(); }//pokud timer bezel, zase pustime

        }


    //doplnit nejak pravidla mezi jidlem, hranim a spanim?
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
