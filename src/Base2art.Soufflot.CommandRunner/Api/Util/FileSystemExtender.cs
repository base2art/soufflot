namespace Base2art.Soufflot.CommandRunner.Api.Util
{
    using System.IO;

    using Base2art.IO;

    public static class FileSystemExtender
    {
        public static void CopyTo(this DirectoryInfo sourceDir, DirectoryInfo destDir, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo[] dirs = sourceDir.GetDirectories();

            if (!sourceDir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDir);
            }

            // If the destination directory doesn't exist, create it. 
            if (!destDir.Exists)
            {
                destDir.Create();
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = sourceDir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDir.FullName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    CopyTo(subdir, destDir.Dir(subdir.Name), true);
                }
            }
        }
    }
}
