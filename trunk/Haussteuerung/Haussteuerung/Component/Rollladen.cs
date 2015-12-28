using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.Hardware;
using GHIElectronics.NETMF.FEZ;
using System.Threading;

namespace Haussteuerung.Component
{
	public class Rollladen

	{
		private int stromDauer;
		private OutputPort portRauf;
		private OutputPort portRunter;

		public Rollladen(FEZ_Pin.Digital portRauf, FEZ_Pin.Digital portRunter, int stromDauer)
		{
			this.stromDauer = stromDauer;
			this.portRauf = new OutputPort((Cpu.Pin)portRauf, false);
			this.portRunter = new OutputPort((Cpu.Pin)portRunter, false);
		}

		public void Rauf()
		{
			portRauf.Write(true);
			Thread.Sleep(this.stromDauer);
			portRauf.Write(false);
		}

		public void Runter()
		{
			portRunter.Write(true);
			Thread.Sleep(this.stromDauer);
			portRunter.Write(false);
		}
	}
}
