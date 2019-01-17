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
        private string path = Directory.GetCurrentDirectory() + "\\test.txt";

        public void Trace(string msg)
        {
            System.Diagnostics.Trace.WriteLine(" [ " + DateTime.Now + " ] " + msg);
        }

        public void Info(string msg)
        {
            Debug.WriteLine(" [ " + DateTime.Now + " ] " + msg);
        }
        //public string path
        //{
        //    get
        //    {
        //        this.path = _path;
        //        FileInfo fileInfo = new FileInfo(this._path);
        //        if (!fileInfo.Exists)
        //        {
        //            throw new FileNotFoundException();
        //        }
        //        return this.path;
        //    }
        //    set
        //    {
        //        string pathTemp = Directory.GetCurrentDirectory() + "\\" + value + ".txt";
        //        FileInfo fileInfo = new FileInfo(pathTemp);
        //        if (!fileInfo.Exists)
        //        {
        //            File.Create(pathTemp);
        //        }
        //        this.path = pathTemp;
        //        _path = pathTemp;
        //    }
        //}
    }
}
