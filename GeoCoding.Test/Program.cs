using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Geocoding;
using Geocoding.Google;

namespace GeoCoding.Test
{
    internal class Program
    {
        private static void GenerateFichierPoi(IEnumerable<GoogleAddress> addresses, string date)
        {
            var fileStream = File.Open(Path.Combine(@"", date + ".ov2"), FileMode.Create);

            using (var ov2File = new BinaryWriter(fileStream))
            {
                foreach (var address in addresses)
                {
                    var length = address.FormattedAddress.Length + 14;
                    var ov2Data = new byte[length];
                    var buffer = BitConverter.GetBytes('2');
                    //ov2Data[0] = buffer[0];
                    ov2Data[0] = 2;
                    buffer = BitConverter.GetBytes((long) length);
                    Array.Copy(buffer, 0, ov2Data, 1, 3);
                    buffer = BitConverter.GetBytes((long) (address.Coordinates.Longitude * 100000));
                    Array.Copy(buffer, 0, ov2Data, 4, 3);
                    buffer = BitConverter.GetBytes((long) address.Coordinates.Latitude * 100000);
                    Array.Copy(buffer, 0, ov2Data, 7, 3);
                    var encoding = new ASCIIEncoding();
                    buffer = encoding.GetBytes(address.FormattedAddress);
                    Array.Copy(buffer, 0, ov2Data, 10, address.FormattedAddress.Length);
                    ov2File.Write(ov2Data);
                }
                ov2File.Close();
            }
        }

        public static void Main(string[] args)
        {
            var geoCoder = new GoogleGeocoder();
            var addresses = geoCoder.Geocode("53 rue de Rechèvres").ToList();

            foreach (var address in addresses)
            {
                Console.WriteLine("Formatted: {0}", address.FormattedAddress); //Formatted: 1600 Pennslyvania Avenue Northwest, Presiden'ts Park, Washington, DC 20500, USA
                Console.WriteLine("Coordinates: {0}, {1}", address.Coordinates.Latitude, address.Coordinates.Longitude); //Coordinates: 38.8978378, -77.0365123

                var reverse = geoCoder.ReverseGeocode(new Location(address.Coordinates.Latitude, address.Coordinates.Longitude));
                var toDisplay = string.Empty;

                foreach (var item in reverse)
                {
                    toDisplay = item.FormattedAddress;
                }

                Console.WriteLine(toDisplay);
            }

            Console.ReadKey();

            var date = DateTime.Now.ToString("yyyyMMdd");

            GenerateFichierPoi(addresses, date);

            Console.ReadKey();
        }
    }
}