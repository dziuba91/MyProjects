using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread t1;
        bool watek = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            t1 = new Thread(czekaj);
            t1.Start();

            watek = true;
        }

        public void czekaj()
        {
            Thread.Sleep(5000);

            watek = false;

            try
            {
                this.Dispatcher.Invoke(
                DispatcherPriority.Normal,
                new Action(
                delegate()
                {
                    this.RaiseEvent(new KeyEventArgs(Keyboard.PrimaryDevice,
                                    PresentationSource.FromVisual(this), 0, Key.Enter) { RoutedEvent = Keyboard.KeyDownEvent });
                }));
            }
            catch { }
        }

        private void win1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                /*
                if (watek)
                {
                    t1.Abort();
                }*/

                WybierzTryb a = new WybierzTryb();
                a.Show();

                Close();
            }
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void win1_Closed(object sender, EventArgs e)
        {
            if (watek)
            {
                t1.Abort();
            }
        }
    }
}
