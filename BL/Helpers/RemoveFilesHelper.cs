using System.IO;

namespace Freelance_System.BL.Helpers
{
    public static class RemoveFilesHelper
    {
        public static void RemoveFile(string FileName)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "/wwwroot/Files/Photos/" + FileName))
            {
                File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/Files/Photos/" + FileName);
            }
        }
    }
}
