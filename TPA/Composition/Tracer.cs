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
        private string _path = "test";
        public string path
        {
            get
            {
                FileInfo fileInfo = new FileInfo(this._path);
                if (!fileInfo.Exists)
                {
                    throw new FileNotFoundException();
                }
                return this.path;
            }
            set
            {
                string pathTemp = Directory.GetCurrentDirectory() + "\\" + value + ".txt";
                FileInfo fileInfo = new FileInfo(pathTemp);
                if (!fileInfo.Exists)
                {
                    File.Create(pathTemp);
                }
                this.path = pathTemp;
            }
        } = "test";
        public void TraceData(TraceEventType eventType, int id, object data)
        {
            using (FileStream fs = new FileStream(path, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write("[" + DateTime.Now + "]  ");
                writer.Write(eventType.ToString() + ": " + data.ToString());
            }
        }
    }
}
