using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace testmedformsapp
{
    public partial class Form1 : Form
    {
        Thread workerThread = null;
        ManualResetEvent threadInterrupt = new ManualResetEvent(false);

        public Form1()
        {
            InitializeComponent();

        
        }

       







        [DllImport("k8055d.dll")]
        public static extern int OpenDevice(int CardAddress);

        [DllImport("k8055d.dll")]
        public static extern void CloseDevice();

        [DllImport("k8055d.dll")]
        public static extern int ReadAnalogChannel(int Channel);

        [DllImport("k8055d.dll")]
        public static extern void ReadAllAnalog(ref int Data1, ref int Data2);

        [DllImport("k8055d.dll")]
        public static extern void OutputAnalogChannel(int Channel, int Data);

        [DllImport("k8055d.dll")]
        public static extern void OutputAllAnalog(int Data1, int Data2);

        [DllImport("k8055d.dll")]
        public static extern void ClearAnalogChannel(int Channel);

        [DllImport("k8055d.dll")]
        public static extern void SetAllAnalog();

        [DllImport("k8055d.dll")]
        public static extern void ClearAllAnalog();

        [DllImport("k8055d.dll")]
        public static extern void SetAnalogChannel(int Channel);

        [DllImport("k8055d.dll")]
        public static extern void WriteAllDigital(int Data);

        [DllImport("k8055d.dll")]
        public static extern void ClearDigitalChannel(int Channel);

        [DllImport("k8055d.dll")]
        public static extern void ClearAllDigital();

        [DllImport("k8055d.dll")]
        public static extern void SetDigitalChannel(int Channel);

        [DllImport("k8055d.dll")]
        public static extern void SetAllDigital();

        [DllImport("k8055d.dll")]
        public static extern bool ReadDigitalChannel(int Channel);

        [DllImport("k8055d.dll")]
        public static extern int ReadAllDigital();

        [DllImport("k8055d.dll")]
        public static extern int ReadCounter(int CounterNr);

        [DllImport("k8055d.dll")]
        public static extern void ResetCounter(int CounterNr);

        [DllImport("k8055d.dll")]
        public static extern void SetCounterDebounceTime(int CounterNr, int DebounceTime);

        [DllImport("k8055d.dll")]
        public static extern int Version();

        [DllImport("k8055d.dll")]
        public static extern int SearchDevices();

        [DllImport("k8055d.dll")]
        public static extern int SetCurrentDevice(int lngCardAddress);

        [DllImport("k8055d.dll")]
        public static extern int ReadBackDigitalOut();

        [DllImport("k8055d.dll")]
        public static extern void ReadBackAnalogOut(int[] Buffer);



        private void button1_Click(object sender, EventArgs e)
        {
            OpenDevice(0);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseDevice();
        }
    

       


        private void timer2_Tick(object sender, EventArgs e)
        {
         
            
            
            
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (this.workerThread == null)
            {
                this.threadInterrupt.Reset();
                this.workerThread = new Thread(() =>
                {
                    
                    while (!this.threadInterrupt.WaitOne(0))
                    {
                        int n = 250;
                        int i = n * -1;
                        while (i <= n)
                        {
                            var val = n - Math.Abs(i++) + 1;
                            //  Console.WriteLine(val);
                            OutputAnalogChannel(1, i);
                            SetDigitalChannel(i);
                            ClearAllDigital();
                            Thread.Sleep(100);
                        }





                    }
                    this.workerThread = null;
                    // worker thread finished in here..
                });
                this.workerThread.IsBackground = true;
                // start worker thread in here
                this.workerThread.Start();
            }
            else
            {
                // stop worker thread in here
                threadInterrupt.Set();
            }
        }
