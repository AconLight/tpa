using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composition
{
    [Export(typeof(ITracer))]
    public class Tracer : ITracer
    {
        public void Trace(string msg)
        {
            System.Diagnostics.Trace.WriteLine(" [ " + DateTime.Now + " ] " + msg);
        }

        public void Info(string msg)
        {
            Debug.WriteLine(" [ " + DateTime.Now + " ] " + msg);
        }
    }
}
