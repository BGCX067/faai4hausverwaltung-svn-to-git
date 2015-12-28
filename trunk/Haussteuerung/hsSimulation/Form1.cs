using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SPOT.Emulator;

namespace hsSimulation
{
    public partial class Form1 : Form
    {
        private Emulator _emulator;

        //Licht bzw. Stromstossschalter
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht1;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht2;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht3;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht4;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht5;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht6;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht7;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht8;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht9;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht10;

        //Taster
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht1Rueckgabe;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht2Rueckgabe;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht3Rueckgabe;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht4Rueckgabe;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht5Rueckgabe;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht6Rueckgabe;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht7Rueckgabe;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht8Rueckgabe;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht9Rueckgabe;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Licht10Rueckgabe;

        //Rollo
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloab1;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloauf1;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloab2;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloauf2;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloab3;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloauf3;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloab4;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloauf4;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloab5;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloauf5;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloab6;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloauf6;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloab7;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloauf7;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloab8;
        public Microsoft.SPOT.Emulator.Gpio.GpioPort port_Rolloauf8;

        public Form1(Emulator emulator)
        {
            _emulator = emulator;

            InitializeComponent();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
         


            //Rueckgabe Pinzuweisung
            port_Licht1Rueckgabe = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Licht1Rueckgabe.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)68;
            port_Licht1Rueckgabe.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.InputPort;
            port_Licht1Rueckgabe.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.InputPort;
            _emulator.RegisterComponent(port_Licht1Rueckgabe);

            //Stromstossschalter Pinzuweisung

            port_Licht1 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Licht1.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)52;
            port_Licht1.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht1.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht1.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Licht1_OnGpioActivity);
            _emulator.RegisterComponent(port_Licht1);

            port_Licht2 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Licht2.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)51;
            port_Licht2.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht2.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht2.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Licht2_OnGpioActivity);
            _emulator.RegisterComponent(port_Licht2);

            port_Licht3 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Licht3.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)54;
            port_Licht3.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht3.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht3.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Licht3_OnGpioActivity);
            _emulator.RegisterComponent(port_Licht3);

            port_Licht4 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Licht4.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)53;
            port_Licht4.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht4.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht4.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Licht4_OnGpioActivity);
            _emulator.RegisterComponent(port_Licht4);

            port_Licht5 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Licht5.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)56;
            port_Licht5.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht5.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht5.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Licht5_OnGpioActivity);
            _emulator.RegisterComponent(port_Licht5);

            port_Licht6 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Licht6.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)55;
            port_Licht6.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht6.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht6.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Licht6_OnGpioActivity);
            _emulator.RegisterComponent(port_Licht6);

            port_Licht7 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Licht7.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)58;
            port_Licht7.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht7.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht7.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Licht7_OnGpioActivity);
            _emulator.RegisterComponent(port_Licht7);

            port_Licht8 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Licht8.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)57;
            port_Licht8.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht8.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht8.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Licht8_OnGpioActivity);
            _emulator.RegisterComponent(port_Licht8);

            port_Licht9 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Licht9.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)60;
            port_Licht9.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht9.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht9.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Licht9_OnGpioActivity);
            _emulator.RegisterComponent(port_Licht9);

            port_Licht10 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Licht10.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)59;
            port_Licht10.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht10.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Licht10.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Licht10_OnGpioActivity);
            _emulator.RegisterComponent(port_Licht10);

// Rollo Pinzuweisung

            //.... auf
            port_Rolloauf1 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloauf1.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)16;
            port_Rolloauf1.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf1.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf1.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloauf1_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloauf1);

            port_Rolloauf2 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloauf2.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)17;
            port_Rolloauf2.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf2.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf2.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloauf2_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloauf2);

            port_Rolloauf3 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloauf3.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)14;
            port_Rolloauf3.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf3.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf3.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloauf3_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloauf3);

            port_Rolloauf4 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloauf4.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)13;
            port_Rolloauf4.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf4.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf4.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloauf4_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloauf4);

            port_Rolloauf5 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloauf5.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)12;
            port_Rolloauf5.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf5.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf5.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloauf5_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloauf5);

            port_Rolloauf6 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloauf6.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)10;
            port_Rolloauf6.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf6.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf6.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloauf6_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloauf6);

            port_Rolloauf7 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloauf7.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)8;
            port_Rolloauf7.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf7.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf7.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloauf7_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloauf7);

            port_Rolloauf8 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloauf8.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)3;
            port_Rolloauf8.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf8.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloauf8.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloauf8_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloauf8);

            //.....ab

            port_Rolloab1 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloab1.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)6;
            port_Rolloab1.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab1.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab1.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloab1_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloab1);

            port_Rolloab2 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloab2.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)5;
            port_Rolloab2.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab2.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab2.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloab2_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloab2);

            port_Rolloab3 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloab3.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)38;
            port_Rolloab3.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab3.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab3.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloab3_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloab3);

            port_Rolloab4 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloab4.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)1;
            port_Rolloab4.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab4.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab4.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloab4_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloab4);

            port_Rolloab5 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloab5.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)39;
            port_Rolloab5.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab5.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab5.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloab5_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloab5);

            port_Rolloab6 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloab6.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)11;
            port_Rolloab6.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab6.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab6.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloab6_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloab6);

            port_Rolloab7 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloab7.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)67;
            port_Rolloab7.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab7.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab7.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloab7_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloab7);

            port_Rolloab8 = new Microsoft.SPOT.Emulator.Gpio.GpioPort();
            port_Rolloab8.Pin = (Microsoft.SPOT.Hardware.Cpu.Pin)66;
            port_Rolloab8.ModesAllowed = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab8.ModesExpected = Microsoft.SPOT.Emulator.Gpio.GpioPortMode.OutputPort;
            port_Rolloab8.OnGpioActivity += new Microsoft.SPOT.Emulator.Gpio.GpioActivity(port_Rolloab8_OnGpioActivity);
            _emulator.RegisterComponent(port_Rolloab8);

        }

        //Relais Ansteuerung

            void port_Licht1_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {

                if (edge)
                {
                    this.K1.BackColor = Color.Green;
                }
                else
                {
                    this.K1.BackColor = Color.Red;
                }
            }

            void port_Licht2_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.K2.BackColor = Color.Green;
                }
                else
                {
                    this.K2.BackColor = Color.Red;
                }
            }

            void port_Licht3_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.K3.BackColor = Color.Green;
                }
                else
                {
                    this.K3.BackColor = Color.Red;
                }
            }

            void port_Licht4_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.K4.BackColor = Color.Green;
                }
                else
                {
                    this.K4.BackColor = Color.Red;
                }
            }

            void port_Licht5_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.K5.BackColor = Color.Green;
                }
                else
                {
                    this.K5.BackColor = Color.Red;
                }
            }

            void port_Licht6_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.K6.BackColor = Color.Green;
                }
                else
                {
                    this.K6.BackColor = Color.Red;
                }
            }

            void port_Licht7_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.K7.BackColor = Color.Green;
                }
                else
                {
                    this.K7.BackColor = Color.Red;
                }
            }

            void port_Licht8_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.K8.BackColor = Color.Green;
                }
                else
                {
                    this.K8.BackColor = Color.Red;
                }
            }

            void port_Licht9_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.K9.BackColor = Color.Green;
                }
                else
                {
                    this.K9.BackColor = Color.Red;
                }
            }

            void port_Licht10_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.K10.BackColor = Color.Green;
                }
                else
                {
                    this.K10.BackColor = Color.Red;
                }
          }

        //Direkte Funktion Licht
            private void K1_BackColorChanged(object sender, EventArgs e)
            {
                if (this.K1.BackColor == Color.Green)
                {
                    if ((this.Lichtaus1.Visible == true) && (this.Lichtan1.Visible == false))
                    {
                        this.Lichtaus1.Visible = false;
                        this.Lichtan1.Visible = true;
                        port_Licht1Rueckgabe.Write(true);
                    }
                    else
                    {
                        this.Lichtaus1.Visible = true;
                        this.Lichtan1.Visible = false;
                        port_Licht1Rueckgabe.Write(true);
                    }
                }
            }

            private void K2_BackColorChanged(object sender, EventArgs e)
            {
                if (this.K2.BackColor == Color.Green)
                {
                    if ((this.Lichtaus2.Visible == true) && (this.Lichtan2.Visible == false))
                    {
                        this.Lichtaus2.Visible = false;
                        this.Lichtan2.Visible = true;
                    }
                    else
                    {
                        this.Lichtaus2.Visible = true;
                        this.Lichtan2.Visible = false;
                    }
                }
            }

            private void K3_BackColorChanged(object sender, EventArgs e)
            {
                if (this.K3.BackColor == Color.Green)
                {
                    if ((this.Lichtaus3.Visible == true) && (this.Lichtan3.Visible == false))
                    {
                        this.Lichtaus3.Visible = false;
                        this.Lichtan3.Visible = true;
                    }
                    else
                    {
                        this.Lichtaus3.Visible = true;
                        this.Lichtan3.Visible = false;
                    }
                }
            }

            private void K4_BackColorChanged(object sender, EventArgs e)
            {
                if (this.K4.BackColor == Color.Green)
                {
                    if ((this.Lichtaus4.Visible == true) && (this.Lichtan4.Visible == false))
                    {
                        this.Lichtaus4.Visible = false;
                        this.Lichtan4.Visible = true;
                    }
                    else
                    {
                        this.Lichtaus4.Visible = true;
                        this.Lichtan4.Visible = false;
                    }
                }
            }

            private void K5_BackColorChanged(object sender, EventArgs e)
            {
                if (this.K5.BackColor == Color.Green)
                {
                    if ((this.Lichtaus5.Visible == true) && (this.Lichtan5.Visible == false))
                    {
                        this.Lichtaus5.Visible = false;
                        this.Lichtan5.Visible = true;
                    }
                    else
                    {
                        this.Lichtaus5.Visible = true;
                        this.Lichtan5.Visible = false;
                    }
                }
            }

            private void K6_BackColorChanged(object sender, EventArgs e)
            {
                if (this.K6.BackColor == Color.Green)
                {
                    if ((this.Lichtaus6.Visible == true) && (this.Lichtan6.Visible == false))
                    {
                        this.Lichtaus6.Visible = false;
                        this.Lichtan6.Visible = true;
                    }
                    else
                    {
                        this.Lichtaus6.Visible = true;
                        this.Lichtan6.Visible = false;
                    }
                }
            }

            private void K7_BackColorChanged(object sender, EventArgs e)
            {
                if (this.K7.BackColor == Color.Green)
                {
                    if ((this.Lichtaus7.Visible == true) && (this.Lichtan7.Visible == false))
                    {
                        this.Lichtaus7.Visible = false;
                        this.Lichtan7.Visible = true;
                    }
                    else
                    {
                        this.Lichtaus7.Visible = true;
                        this.Lichtan7.Visible = false;
                    }
                }
            }

            private void K8_BackColorChanged(object sender, EventArgs e)
            {
                if (this.K8.BackColor == Color.Green)
                {
                    if ((this.Lichtaus8.Visible == true) && (this.Lichtan8.Visible == false))
                    {
                        this.Lichtaus8.Visible = false;
                        this.Lichtan8.Visible = true;
                    }
                    else
                    {
                        this.Lichtaus8.Visible = true;
                        this.Lichtan8.Visible = false;
                    }
                }
            }

            private void K9_BackColorChanged(object sender, EventArgs e)
            {
                if (this.K9.BackColor == Color.Green)
                {
                    if ((this.Lichtaus9.Visible == true) && (this.Lichtan9.Visible == false))
                    {
                        this.Lichtaus9.Visible = false;
                        this.Lichtan9.Visible = true;
                    }
                    else
                    {
                        this.Lichtaus9.Visible = true;
                        this.Lichtan9.Visible = false;
                    }
                }
            }

            private void K10_BackColorChanged(object sender, EventArgs e)
            {
                if (this.K10.BackColor == Color.Green)
                {
                    if ((this.Lichtaus10.Visible == true) && (this.Lichtan10.Visible == false))
                    {
                        this.Lichtaus10.Visible = false;
                        this.Lichtan10.Visible = true;
                    }
                    else
                    {
                        this.Lichtaus10.Visible = true;
                        this.Lichtan10.Visible = false;
                    }
                }
            }

        //Taster Steuerung

            private void btnTaster_MouseDown(object sender, MouseEventArgs e)
            {
                string Taster = ((Button)sender).Text;

                if (Taster == "T1" ) 
                {
                    this.K1.BackColor = Color.Green;
                }
                if (Taster == "T2")
                {
                    this.K2.BackColor = Color.Green;
                }
                if (Taster == "T3")
                {
                    this.K3.BackColor = Color.Green;
                }
                if (Taster == "T4")
                {
                    this.K4.BackColor = Color.Green;
                }
                if (Taster == "T5")
                {
                    this.K5.BackColor = Color.Green;
                }
                if (Taster == "T6")
                {
                    this.K6.BackColor = Color.Green;
                }
                if (Taster == "T7")
                {
                    this.K7.BackColor = Color.Green;
                }
                if (Taster == "T8")
                {
                    this.K8.BackColor = Color.Green;
                }
                if (Taster == "T9")
                {
                    this.K9.BackColor = Color.Green;
                }
                if (Taster == "T10")
                {
                    this.K10.BackColor = Color.Green;
                }
            }

            private void btnTaster_MouseUp(object sender, MouseEventArgs e)
            {
                string Taster = ((Button)sender).Text;

                if (Taster == "T1")
                {
                    this.K1.BackColor = Color.Red;
                }
                if (Taster == "T2")
                {
                    this.K2.BackColor = Color.Red;
                }
                if (Taster == "T3")
                {
                    this.K3.BackColor = Color.Red;
                }
                if (Taster == "T4")
                {
                    this.K4.BackColor = Color.Red;
                }
                if (Taster == "T5")
                {
                    this.K5.BackColor = Color.Red;
                }
                if (Taster == "T6")
                {
                    this.K6.BackColor = Color.Red;
                }
                if (Taster == "T7")
                {
                    this.K7.BackColor = Color.Red;
                }
                if (Taster == "T8")
                {
                    this.K8.BackColor = Color.Red;
                }
                if (Taster == "T9")
                {
                    this.K9.BackColor = Color.Red;
                }
                if (Taster == "T10")
                {
                    this.K10.BackColor = Color.Red;
                }
            }

        //Relais Rollos
         //auf
            void port_Rolloauf1_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloauf1.BackColor = Color.Green;
                }
                else 
                {
                    this.Rolloauf1.BackColor = Color.Red;
                }
            }

            void port_Rolloauf2_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloauf2.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloauf2.BackColor = Color.Red;
                }
            }

            void port_Rolloauf3_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloauf3.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloauf3.BackColor = Color.Red;
                }
            }

            void port_Rolloauf4_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloauf4.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloauf4.BackColor = Color.Red;
                }
            }

            void port_Rolloauf5_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloauf5.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloauf5.BackColor = Color.Red;
                }
            }

            void port_Rolloauf6_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloauf6.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloauf6.BackColor = Color.Red;
                }
            }

            void port_Rolloauf7_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloauf7.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloauf7.BackColor = Color.Red;
                }
            }

            void port_Rolloauf8_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloauf8.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloauf8.BackColor = Color.Red;
                }
            }

        //ab

            void port_Rolloab1_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloab1.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloab1.BackColor = Color.Red;
                }
            }

            void port_Rolloab2_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloab2.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloab2.BackColor = Color.Red;
                }
            }

            void port_Rolloab3_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloab3.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloab3.BackColor = Color.Red;
                }
            }

            void port_Rolloab4_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloab4.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloab4.BackColor = Color.Red;
                }
            }

            void port_Rolloab5_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloab5.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloab5.BackColor = Color.Red;
                }
            }

            void port_Rolloab6_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloab6.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloab6.BackColor = Color.Red;
                }
            }

            void port_Rolloab7_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloab7.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloab7.BackColor = Color.Red;
                }
            }

            void port_Rolloab8_OnGpioActivity(Microsoft.SPOT.Emulator.Gpio.GpioPort sender, bool edge)
            {
                if (edge)
                {
                    this.Rolloab8.BackColor = Color.Green;
                }
                else
                {
                    this.Rolloab8.BackColor = Color.Red;
                }
            }

        }

    }

