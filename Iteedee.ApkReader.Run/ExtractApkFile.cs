using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Iteedee.ApkReader.Run
{
    public class ExtractApkFile
    {
        public static void ExtractFileAndSave(string APKFilePath, string fileResourceLocation, string FilePathToSave, int index)
        {
            using (ICSharpCode.SharpZipLib.Zip.ZipInputStream zip = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(File.OpenRead(APKFilePath)))
            {
                using (var filestream = new FileStream(APKFilePath, FileMode.Open, FileAccess.Read))
                {
                    ICSharpCode.SharpZipLib.Zip.ZipFile zipfile = new ICSharpCode.SharpZipLib.Zip.ZipFile(filestream);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry item;
                    while ((item = zip.GetNextEntry()) != null)
                    {
                        if (item.Name.ToLower() == fileResourceLocation)
                        {
                            string fileLocation = Path.Combine(FilePathToSave, string.Format("{0}-{1}", index, fileResourceLocation.Split(Convert.ToChar(@"/")).Last()));
                            using (Stream strm = zipfile.GetInputStream(item))
                            using (FileStream output = File.Create(fileLocation))
                            {
                                try
                                {
                                    strm.CopyTo(output);
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }

                        }
                    }
                }
            }
        }
    }
}
