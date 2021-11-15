using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Thread workerThread = null;
        Thread workerThread1 = null;

        ManualResetEvent threadInterrupt = new ManualResetEvent(false);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.workerThread == null)
            {
                button1.Text = "Disconnect";

                this.threadInterrupt.Reset();
                this.workerThread = new Thread(() =>
                {
                    Run(0);
                    this.workerThread = null;
                    // worker thread finished in here..
                });
                this.workerThread.IsBackground = true;
                // start worker thread in here

                this.workerThread.Start();
            }
            else
            {
                button1.Text = "Connect";
                // stop worker thread in here
                this.workerThread.Interrupt();
                threadInterrupt.Set();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.workerThread1 == null)
            {
                button2.Text = "Disconnect";

                this.threadInterrupt.Reset();
                this.workerThread1 = new Thread(() =>
                {
                    Run(1);
                    this.workerThread1 = null;
                    // worker thread finished in here..
                });
                this.workerThread1.IsBackground = true;
                // start worker thread in here

                this.workerThread1.Start();
            }
            else
            {
                button2.Text = "Connect";
                // stop worker thread in here
                this.workerThread1.Interrupt();
                threadInterrupt.Set();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() => Run(2)));
            thread.Start();
        }

        public static void Run(int idx)

        {
            try
            {
                Console.WriteLine(string.Format("Run {0} Start", idx));
                for (int i = 0; i < 1000; i++)
                {

                    Console.WriteLine(string.Format("Run {0} :: {1}", idx, i));
                    Thread.Sleep(10);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR : ", e);
            }finally
            {
                Console.WriteLine(string.Format("Run {0} End", idx));
            }


        }

        private void stp_btn_Click(object sender, EventArgs e)
        {
            startHello();
        }

        // SetInterval 함수 **
        public static async Task SetInterval(Action action, TimeSpan timeout)
        {
            await Task.Delay(timeout).ConfigureAwait(false);
            action();
            await SetInterval(action, timeout);
        }

        public static void Hello()
        {
            Console.Write("hi");
        }

        public void startHello()
        {
            SetInterval(() => Hello(), TimeSpan.FromSeconds(2));
        }
    }
}
