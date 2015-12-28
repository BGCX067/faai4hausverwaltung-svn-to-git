using System;
using GHIElectronics.NETMF.Hardware;
using GHIElectronics.NETMF.FEZ;

namespace GHIElectronics.NETMF.FEZ
{
    public static partial class FEZ_Components
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
}