using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT.Scene;
namespace NextRT.Import
{
    public class EntityImport
    {

        public static Dictionary<string, Importer> Importers = new Dictionary<string, Importer>();
        
        public static void RegisterDefaults()
        {
            Register(new ImportAssImp(), ".3ds");
        }

        public static void Register(Importer imp,string ext)
        {
            Importers.Add(ext.ToLower(), imp);
        }

        public static NodeEntity Import(string path)
        {

            string ext = new FileInfo(path).Extension;

            if (Importers.ContainsKey(ext.ToLower()))
            {

                var imp = Importers[ext.ToLower()];

                return imp.Import(path);

            }

            return null;
        }

    }
}
