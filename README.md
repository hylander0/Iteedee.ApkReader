Iteedee.ApkReader
=================

.NET library written in C# for reading/parsing APK manifest (AndroidManifest.xml) and resource data (Resources.arsc)

## Using the ApkReader Library

The library handles everything after you have uncompressed/unzipped the APK using your choice tool or library. I have used the [ICSharpCode.SharpZipLib][5] library to uncompressed the APK in my example.

Below I am uncompromising the **AndroidManifest.xml** and **Resources.arsc** files and passing the byte array data to the ApkReader library:

    byte[] manifestData = null;
    byte[] resourcesData = null;
    using (ICSharpCode.SharpZipLib.Zip.ZipInputStream zip = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(File.OpenRead(path)))
    {
        using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            ICSharpCode.SharpZipLib.Zip.ZipFile zipfile = new ICSharpCode.SharpZipLib.Zip.ZipFile(filestream);
            ICSharpCode.SharpZipLib.Zip.ZipEntry item;
            while ((item = zip.GetNextEntry()) != null)
            {
                if (item.Name.ToLower() == "androidmanifest.xml")
                {
                    manifestData = new byte[50 * 1024];
                    using (Stream strm = zipfile.GetInputStream(item))
                    {
                        strm.Read(manifestData, 0, manifestData.Length);
                    }
    
                }
                if (item.Name.ToLower() == "resources.arsc")
                {
                    using (Stream strm = zipfile.GetInputStream(item))
                    {
                        using (BinaryReader s = new BinaryReader(strm))
                        {
                            resourcesData = s.ReadBytes((int)s.BaseStream.Length);
    
                        }
                    }
                }
            }
        }
    }
    

After you have uncompressed and extracted the necessary data, simply pass in the data objects and it returns an **APKInfo** object.

    ApkReader apkReader = new ApkReader();
    ApkInfo info = apkReader.extractInfo(manifestData, resourcesData);
    

**APKInfo** object contains all of the meta-data for that APK.

    Console.WriteLine(string.Format("Package Name: {0}", info.packageName));
    Console.WriteLine(string.Format("Version Name: {0}", info.versionName));
    Console.WriteLine(string.Format("Version Code: {0}", info.versionCode));
    
    Console.WriteLine(string.Format("App Has Icon: {0}", info.hasIcon));
    if(info.iconFileName.Count > 0)
        Console.WriteLine(string.Format("App Icon: {0}", info.iconFileName[0]));
    Console.WriteLine(string.Format("Min SDK Version: {0}", info.minSdkVersion));
    Console.WriteLine(string.Format("Target SDK Version: {0}", info.targetSdkVersion));
    
    if (info.Permissions != null && info.Permissions.Count > 0)
    {
        Console.WriteLine("Permissions:");
        info.Permissions.ForEach(f =>
        {
            Console.WriteLine(string.Format("   {0}", f));
        });
    }
    else
        Console.WriteLine("No Permissions Found");
    
    Console.WriteLine(string.Format("Supports Any Density: {0}", info.supportAnyDensity));
    Console.WriteLine(string.Format("Supports Large Screens: {0}", info.supportLargeScreens));
    Console.WriteLine(string.Format("Supports Normal Screens: {0}", info.supportNormalScreens));
    Console.WriteLine(string.Format("Supports Small Screens: {0}", info.supportSmallScreens));
    
