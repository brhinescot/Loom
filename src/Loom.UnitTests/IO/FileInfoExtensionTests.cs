#region Using Directives

using System.IO;

#endregion

namespace Loom.IO
{
    public class FileInfoExtensionTests
    {
        public void SplitTets()
        {
            FileInfo file = new FileInfo(@"IO\cbs.log");
            file.Split(3);
        }

        public void SplitAndZipTets()
        {
            FileInfo file = new FileInfo(@"IO\cbs.log");
            file.Split(3, true);
        }

        public void Merge()
        {
            FileInfo file = new FileInfo(@"IO\cbs.log");
            file.Split(3);

            string[] fileInfos = Directory.GetFiles(@"IO\", "*.csd");
            fileInfos.Merge(@"IO\Combined\");
        }
    }
}