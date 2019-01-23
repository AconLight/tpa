using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using System.IO;
using System.Configuration;
using Data;
using System.Diagnostics;

namespace Composition
{
    public class MEF
    {
        static private CompositionContainer compositionContainer;
        [Import(typeof(DataBridgeInterface))]
        public DataBridgeInterface dataBridgeInterface;
        [Import(typeof(ITracer))]
        public ITracer tracer;

        public MEF()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            string root = "tpa";
            while (!(path.Substring(path.Length - root.Length) == root))
            {
                path = path.Remove(path.Length - 1);
            }
            string path1 =path + "\\TPA\\Plugins";
            var catalog = new AggregateCatalog();
            var directorycatalog = new DirectoryCatalog(path1, "Database.dll");
            catalog.Catalogs.Add(directorycatalog);
            compositionContainer = new CompositionContainer(catalog);

            try
            {
                compositionContainer.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }

            //tracer.Info(ConfigurationManager.AppSettings["DataSerOpt"]);
            dataBridgeInterface = compositionContainer.GetExportedValue<DataBridgeInterface>(ConfigurationManager.AppSettings["DataSerOpt"]);
            //tracer.Info(path);
        }
        public static CompositionContainer getContainer()
        {
            return compositionContainer;
        }
    }
}