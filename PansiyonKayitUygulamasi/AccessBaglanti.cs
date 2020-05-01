using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace PansiyonKayitUygulamasi
{
    public class AccessBaglanti
    {
        public OleDbConnection baglanti()
        {
            //OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Erdogan\Desktop\PansiyonKayitUygulamasi\PansiyonKayitUygulamasi\Pansiyon.mdb");
            //CDE ÇALIŞTI OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\[ProgramFilesFolder]\[Manufacturer]\[ProductName]\Pansiyon.mdb");
           OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Pansiyon.mdb");

            baglan.Open();
            return baglan;
        }
    }
}
