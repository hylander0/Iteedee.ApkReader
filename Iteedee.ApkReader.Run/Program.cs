using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iteedee.ApkReader.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadApk.ReadApkFromPath(@"SampleAPK\ApkReaderSample.apk");
            Console.ReadKey();
        }
    }
}
