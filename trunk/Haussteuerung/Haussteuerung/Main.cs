using System;
//using Microsoft.SPOT;
//using Microsoft.SPOT.Hardware;
using System.IO;
using System.Threading;
//using System.Net;
//using GHIElectronics.NETMF.Hardware;
using GHIElectronics.NETMF.FEZ;
using Haussteuerung.Component;
//using System.Net.Sockets;
using System.Text;
using ElzeKool.net;


namespace Haussteuerung
{
	public class Haussteuerung
	{
		public static void Main()
		{
			// initialisierung
			// lampen
			Lampe lampeStiegenhaus = new Lampe(FEZ_Pin.Digital.Di20);
			Lampe lampeWC = new Lampe(FEZ_Pin.Digital.Di21);
			Lampe lampeBad = new Lampe(FEZ_Pin.Digital.Di22);
			Lampe lampeSchlafzimmer = new Lampe(FEZ_Pin.Digital.Di23);
			Lampe lampeAbstellraum = new Lampe(FEZ_Pin.Digital.Di24);
			Lampe lampeKueche = new Lampe(FEZ_Pin.Digital.Di25);
			Lampe lampeEssbereich = new Lampe(FEZ_Pin.Digital.Di26);
			Lampe lampeWohnbereich = new Lampe(FEZ_Pin.Digital.Di27);
			Lampe lampeKinderzimmer = new Lampe(FEZ_Pin.Digital.Di28);
			Lampe lampeParterre = new Lampe(FEZ_Pin.Digital.Di29);

			// rollos
			Rollladen rolloBad = new Rollladen(FEZ_Pin.Digital.Di30, FEZ_Pin.Digital.Di38, 10000);
			Rollladen rolloSchlafzimmer = new Rollladen(FEZ_Pin.Digital.Di31, FEZ_Pin.Digital.Di39, 10000);
			Rollladen rolloAbstellraum = new Rollladen(FEZ_Pin.Digital.Di32, FEZ_Pin.Digital.Di40, 10000);
			Rollladen rolloTerrassentuer = new Rollladen(FEZ_Pin.Digital.Di33, FEZ_Pin.Digital.Di41, 20000);
			Rollladen rolloEssbereichLinks = new Rollladen(FEZ_Pin.Digital.Di34, FEZ_Pin.Digital.Di42, 10000);
			Rollladen rolloEssbereichRechts = new Rollladen(FEZ_Pin.Digital.Di35, FEZ_Pin.Digital.Di43, 10000);
			Rollladen rolloWohnbereich = new Rollladen(FEZ_Pin.Digital.Di36, FEZ_Pin.Digital.Di44, 10000);
			Rollladen rolloKinderzimmer = new Rollladen(FEZ_Pin.Digital.Di37, FEZ_Pin.Digital.Di45, 10000);

			// Create our Webserver
			HTTPServer WebServer = new HTTPServer(8080, new RequestHandler());

			// Infinite sleep
			System.Threading.Thread.Sleep(-1);

		}
	}
}
