using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Iteedee.ApkReader.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            string APKfilePath = @"SampleAPK\ApkReaderSample.apk";
            string iconOutputlocation = @"SampleAPK\icons\";

            ApkInfo info = ReadApk.ReadApkFromPath(APKfilePath);

            Directory.CreateDirectory(iconOutputlocation);
            for (var i = 0; i < info.iconFileName.Count; i++)
            {

                ExtractApkFile.ExtractFileAndSave(APKfilePath, info.iconFileName[i], @"SampleAPK\icons\", i);
            }

            Console.ReadKey();
        }
    }
}
