using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private Thread _workerThread;
        ManualResetEvent _event = new ManualResetEvent(true);


        public Form2()
        {
            InitializeComponent();
            _workerThread = new Thread(
                () =>
                {
                    long count = 0;
                    while (true)
                    {
                        _event.WaitOne();
                        Thread.Sleep(1000);
                        Invoke(
                            (MethodInvoker)
                            delegate ()
                            {
                                label1.Text = string.Format("{0}", count++);
                                label2.Text = string.Format("{0}", count++);
                                label3.Text = string.Format("{0}", count++);
                            });
                    }
                });

            _workerThread.Start();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            _workerThread.Abort();
            _workerThread.Join();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _event.Reset();
            MessageBox.Show("AAAAA");
            _event.Set();
        }
    }
}
