using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using System.IO;
using System.Configuration;
using Data;

namespace Composition
{
    public class MEF
    {
        private CompositionContainer compositionContainer;
        //[Import(typeof(DataBridgeInterface))]
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
            string path1 =path + "\\TPA\\Database\\bin\\Debug";
            string path2 = path + "\\TPA\\Composition\\bin\\Debug";
            string path3 = path + "\\TPA\\Serialization\\bin\\Debug";
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(MEF).Assembly));
            var directorycatalog = new DirectoryCatalog(path1, "*.dll");
            catalog.Catalogs.Add(directorycatalog);
            directorycatalog = new DirectoryCatalog(path2, "Composition.dll");
            catalog.Catalogs.Add(directorycatalog);
            directorycatalog = new DirectoryCatalog(path3, "*.dll");
            catalog.Catalogs.Add(directorycatalog);
            compositionContainer = new CompositionContainer(catalog);

            try
            {
                this.compositionContainer.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
            tracer.Info(ConfigurationManager.AppSettings["DataSerOpt"]);
            dataBridgeInterface = compositionContainer.GetExportedValue<DataBridgeInterface>(ConfigurationManager.AppSettings["DataSerOpt"]);
            //tracer.Info(path);
        }
    }
}
