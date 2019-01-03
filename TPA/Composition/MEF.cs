using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Reflection;

namespace Composition
{
    public class MEF
    {
        private CompositionContainer compositionContainer { get; }
        [Import(typeof(DataBridgeInterface))]
        public DataBridgeInterface dataBridgeInterface;

        public MEF()
        {
            var catalog = new DirectoryCatalog(".", "*");

            compositionContainer = new CompositionContainer(catalog);
            try
            {
                this.compositionContainer.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
    }
}
