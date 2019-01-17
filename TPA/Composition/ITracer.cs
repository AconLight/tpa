using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Composition
{
    public interface ITracer
    {
        void TraceData(TraceEventType eventType,int id,object data);
    }
}
