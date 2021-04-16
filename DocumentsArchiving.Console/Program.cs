using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentsArchiving.INTEG;

namespace DocumentsArchiving.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new DocumentsArchiving.INTEG.ServiceReferenceCountryInfo.CountryInfoServiceSoapTypeClient();

            var list = client.ListOfCountryNamesByCode();

            foreach (var item in list)
            {
                System.Console.WriteLine(item.sName);
            }

            System.Console.ReadLine();

        }
    }
}
