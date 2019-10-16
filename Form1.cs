using System;
using System.Threading;
using System.Windows.Forms;

namespace MyElectricTeapot
{
    public partial class Form1 : Form
    {
        DateTime time = DateTime.Now;
        public Teapot Teapot { get; set; }

        public Form1()
        {
            InitializeComponent();
            WaterContainer container = new WaterContainer(volume: 2000);
            Heater heater = new Heater(power: 1500, efficioncy: 0.85f);
            PowerButton button = new PowerButton();
            Teapot = new Teapot(container, heater, button);
            Teapot.AddWater(new Water(500, 20));
            DoubleBuffered = true;

            // Костыль
            ThreadStart start = new ThreadStart(() =>
            {
                Thread.Sleep(1000);
                while (ActiveForm != null)
                {
                    float timedelta = (float)(DateTime.Now - time).TotalSeconds;
                    if (timedelta > 0.03f)
                    {
                        time = DateTime.Now;
                        Teapot.Update(timedelta);
                        try
                        {
                            Form1.ActiveForm.Invoke(new Action(() => Refresh()));
                        }
                        catch
                        {
                            Thread.CurrentThread.Abort();
                        }
                    }
                }
            });
            Thread thread = new Thread(start);
            thread.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Teapot.PowerOn();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Teapot.PowerOff();
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            label1.Text = Teapot.ToString();
        }
    }
}
