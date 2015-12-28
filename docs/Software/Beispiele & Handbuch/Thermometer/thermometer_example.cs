using System;
using Microsoft.SPOT;
using System.Threading;
using GHIElectronics.NETMF.FEZ;

public class Program
{
    public static void Main()
    {
        // Create Thermometer object assigned to the a Thermometer Component connected to An0 with analog
        input feature FEZ_Components.Thermometer myThermometer = new FEZ_Components.Thermometer(FEZ_Pin.AnalogIn.An0);
        int value = 0;
        
        while (true)
        {
            value = myThermometer.GetTemperatureCelsius();
            Debug.Print("myThermometer reading is: " + value.ToString());
            Thread.Sleep(100);
        }
    }
}