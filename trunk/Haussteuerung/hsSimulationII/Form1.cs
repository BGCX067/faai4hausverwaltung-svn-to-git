using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SPOT.Emulator;

namespace hsSimulationII
{
    public partial class Form1 : Form
    {
        private Emulator _emulator;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht1;
        public Form1(Emulator emulator)
        {
            _emulator = emulator;
            port_Licht1 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Licht1.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)52;
            port_Licht1.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht1.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht1.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Licht1_OnGpioActivity);
            _emulator.RegisterComponent(port_Licht1);

            InitializeComponent();
        }

        void port_Licht1_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
        {
            this.Text = edge.ToString();
        }
    }
}
