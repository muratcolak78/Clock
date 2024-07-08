using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Clock
{
    public class MainWindowVM : ViewModelBase
    {
        DispatcherTimer timer;
        DispatcherTimer CountDown;
        DispatcherTimer StufeController;

        Geraet grt;
        public ICommand Start { get; }
        public double Geschwindigkeit
        {
            get { return grt.Geschwindigkeit; }
            set
            {
                grt.Geschwindigkeit = value;
                OnPropertyChanged(nameof(Geschwindigkeit));
            }
        }
      
        public int Stufe
        {
            get { return grt.Stufe; }
            set
            {
                grt.Stufe = value;
                OnPropertyChanged(nameof(Stufe));
            }
        }
        
        public int Minute
        {
            get => grt.Minute; set
            {
                grt.Minute = value;
                OnPropertyChanged(nameof(Minute));
                Second = Minute * 60;
            }
        }

        private int _second;
        public int Second
        {
            get => _second;
            set
            {
                _second = value;
                OnPropertyChanged(nameof(Second));
            }
        }

        private double _rest;
        public double Rest
        {
            get { return _rest; }
            set
            {
                if (_rest != value)
                {
                    _rest = value;
                    OnPropertyChanged(nameof(Rest));
                    Puls = Rest;
                    Stufe = 1;
                }

            }
        }

    
        public double Puls
        {
            get { return grt.Puls; }
            set
            {
                grt.Puls = value;
                OnPropertyChanged(nameof(Puls));
            }
        }


        private string _clock;
        public string Clock
        {
            get { return DateTime.Now.ToString("HH:mm:ss"); } // bur formatta saati strin yap anlik saati cek ve stringe cevir
            set { _clock = value; }
        }



        public MainWindowVM()
        {
            grt = new Geraet();
            
            Start = new RelayCommand(Startubung);
            
            timer = new DispatcherTimer();              // eylemlerin düzenli aralıklarla yürütülmesini sağlar
            timer.Interval = TimeSpan.FromMilliseconds(100);   // her saniyede bir tetikleme talimati verilir
            timer.Tick += DispatcherTimer_Tick;          // belirtilen aralikta tetiklenecek metot eklenir
                                                         // teikleme baslatilir
            timer.Start();
            CountDown = new DispatcherTimer();
            CountDown.Interval = TimeSpan.FromMilliseconds(100);
            CountDown.Tick += CountDown_Tick;

            StufeController = new DispatcherTimer();
            StufeController.Interval= TimeSpan.FromSeconds(6);
            StufeController.Tick += StufeController_Tick;

         
        }

        private void StufeController_Tick(object? sender, EventArgs e)
        {
            if (grt.Puls < grt.MaxPuls)
            {
                grt.erhoeheLeistungsStufe();
                OnPropertyChanged(nameof(Geschwindigkeit));
                OnPropertyChanged(nameof(Stufe));
            }
            else
            {
                grt.vermindereleistunsStufe();
                OnPropertyChanged(nameof(Geschwindigkeit));
                OnPropertyChanged(nameof(Stufe));
            }
            }

        private void CountDown_Tick(object? sender, EventArgs e)
        {

            Second--;
            if (Second == 0)
            {
                CountDown.Stop();
                MessageBox.Show("Training beendet");
                StufeController.Stop();
            }
            Puls += grt.Geschwindigkeit;
            //OnPropertyChanged(nameof(Puls));
            OnPropertyChanged(nameof(Second));

        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Clock));

        }


        private void Startubung()
        {
            if (!IsRunning)
            {
                // Başlangıç durumu
               
                CountDown.Start();
                StufeController.Start();
               
            }
            else
            {
                // Durdurma durumu
               
                CountDown.Stop();
                StufeController.Stop();
                MessageBox.Show("Training beendet");
                Rest = 0;
                Puls = 0;
                Minute = 0;
                Stufe = 0;
                Geschwindigkeit = 0.17;
            }
            IsRunning = !IsRunning; // Durumu tersine çevir
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
                OnPropertyChanged(nameof(StartButtonText));
            }
        }

        public string StartButtonText
        {
            get { return IsRunning ? "Stop" : "Start"; }
        }




    }
}
