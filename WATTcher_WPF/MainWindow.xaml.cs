using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace WATTcher_WPF
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer writeTimeOnTick;
        private DispatcherTimer flashTextOnTick;
        private DispatcherTimer monitorBatteryOnTick;
        private DispatcherTimer updateTimeToElapseOnTick;
        private DateTime start;
        private TimeSpan TimeToElapse;
        private BatteryModel batteryInformation = new BatteryModel();

        public MainWindow()
        {
            InitializeComponent();
            monitorBatteryOnTick = new DispatcherTimer(new TimeSpan(0, 0, 0, 5, 0), DispatcherPriority.Background, MonitorBattery, Dispatcher.CurrentDispatcher); monitorBatteryOnTick.IsEnabled = false;
            writeTimeOnTick = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 25), DispatcherPriority.Background, WriteTime, Dispatcher.CurrentDispatcher); writeTimeOnTick.IsEnabled = false;
            updateTimeToElapseOnTick = new DispatcherTimer(new TimeSpan(0, 0, 1, 0, 0), DispatcherPriority.Background, UpdateTimeToElapse, Dispatcher.CurrentDispatcher); writeTimeOnTick.IsEnabled = false;
            flashTextOnTick = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 500), DispatcherPriority.Background, doFlashTime, Dispatcher.CurrentDispatcher); flashTextOnTick.IsEnabled = false;
        }

        private void MonitorBattery(object sender, EventArgs e)
        {
            if (!isPowerConnected())
            {
                int tempBatteryCharge = GetBatteryCharge();
                if (tempBatteryCharge > 90)
                {
                    monitorBatteryOnTick.Interval = new TimeSpan(0, 0, 20, 0, 0);
                }
                else if (tempBatteryCharge > 70)
                {
                    monitorBatteryOnTick.Interval = new TimeSpan(0, 0, 15, 0, 0);
                }
                else if (tempBatteryCharge > 50)
                {
                    monitorBatteryOnTick.Interval = new TimeSpan(0, 0, 10, 0, 0);
                }
                else if (tempBatteryCharge > 30)
                {
                    monitorBatteryOnTick.Interval = new TimeSpan(0, 0, 5, 0, 0);
                }
                else if (tempBatteryCharge > 20)
                {
                    monitorBatteryOnTick.Interval = new TimeSpan(0, 0, 3, 0, 0);
                }
                else if (tempBatteryCharge > 10)
                {
                    monitorBatteryOnTick.Interval = new TimeSpan(0, 0, 1, 10, 0);
                }
                else if (tempBatteryCharge <= 10)
                {
                    monitorBatteryOnTick.Stop();
                    StartWriteOnTick(((int)(GetEstimatedRunTime() * 0.95) - 5));
                }
            }
        }

        private void StartWriteOnTick(int seconds)
        {
            this.Show();
            start = DateTime.Now;
            TimeToElapse = TimeSpan.FromMinutes(seconds);
            writeTimeOnTick.Start();
        }

        private void WriteTime(object sender, EventArgs e)
        {
            if (!isPowerConnected())
            {
                string endValue = Convert.ToString(DateTime.Now - start - TimeToElapse);
                Timer_Block.Text = endValue.Substring(1);

                if (!endValue.Contains("-"))
                {
                    writeTimeOnTick.Stop();
                    Timer_Block.Text = "0:00:00";
                    flashTextOnTick.Start();
                }
            }
            else
            {
                this.Hide();
                writeTimeOnTick.Stop();
                monitorBatteryOnTick.Start();
            }
        }

        private void doFlashTime(object sender, EventArgs e)
        {
            if (isPowerConnected())
            {
                this.Hide();
                flashTextOnTick.Stop();
                monitorBatteryOnTick.Start();
            }
            else if (Timer_Block.Text.Contains("0:00:00"))
            {
                Timer_Block.Text = " ";
            }
            else
            {
                Timer_Block.Text = "0:00:00";
            }
        }

        private void UpdateTimeToElapse(object sender, EventArgs e)
        {
            TimeToElapse = new TimeSpan(0, 0, (int)((GetEstimatedRunTime() * 0.95) - 4), 0, 0);
        }

        private int GetEstimatedRunTime()
        {
            batteryInformation.UpdateValues();
            int value = int.Parse(batteryInformation.EstimatedRunTime.ToString());
            return value;
        }

        private bool isPowerConnected()
        {
            batteryInformation.UpdateValues();
            return batteryInformation.BatteryStatus != 1;
        }

        private int GetBatteryCharge()
        {
            batteryInformation.UpdateValues();
            return batteryInformation.EstimatedChargeRemaining;
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseTImer_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void WATTcher_Activated(object sender, EventArgs e)
        {
            this.Topmost = true;
            var screenBuffer = System.Windows.SystemParameters.WorkArea;
            this.Left = screenBuffer.Left;
            this.Top = screenBuffer.Top;
        }

        private void WATTcher_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("WATTcher is running in the background, \n A countdown will occur once the battery reaches 10% \n informing you of how much battery life you have left.", "WATTcher says hello.", MessageBoxButton.OK, MessageBoxImage.None);
            if (MessageBoxResult.OK == result)
            {
                this.Hide();
                monitorBatteryOnTick.Start();
            }
            else
            {
                this.Close();
            }
        }
    }
}