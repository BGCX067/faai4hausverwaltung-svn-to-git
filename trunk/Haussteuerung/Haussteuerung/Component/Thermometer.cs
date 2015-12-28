using System;
using Microsoft.SPOT;
using GHIElectronics.NETMF.Hardware;
using GHIElectronics.NETMF.FEZ;

namespace Haussteuerung.Component
{
        public class Thermometer : IDisposable
        {
            AnalogIn adc;

            public void Dispose()
            {
                adc.Dispose();
            }

            public Thermometer(FEZ_Pin.AnalogIn pin)
            {
                adc = new AnalogIn((AnalogIn.Pin)pin);
                adc.SetLinearScale(-22, 56);
            }

            public int GetTemperatureCelsius()
            {
                return adc.Read();
            }
        }
}