using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.Hardware;
using GHIElectronics.NETMF.FEZ;
using System.Threading;

namespace Haussteuerung.Component
{
	public class Lampe
	{
		private OutputPort portPhase;

		public Lampe(FEZ_Pin.Digital port)
		{
			portPhase = new OutputPort((Cpu.Pin)port, false);
		}

		// stromstoﬂ
		public void Strom()
		{
			portPhase.Write(true);
			Thread.Sleep(2000);
			portPhase.Write(false);
		}
	}
}
