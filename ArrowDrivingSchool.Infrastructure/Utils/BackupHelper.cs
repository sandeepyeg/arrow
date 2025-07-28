namespace ArrowDrivingSchool.Infrastructure.Utils;

public static class BackupHelper
{
    public static void BackupDatabase(string sourcePath)
    {
        try
        {
            var backupFolder = Path.Combine(Path.GetDirectoryName(sourcePath)!, "backups");
            if (!Directory.Exists(backupFolder))
                Directory.CreateDirectory(backupFolder);

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var backupPath = Path.Combine(backupFolder, $"arrow_backup_{timestamp}.db");

            File.Copy(sourcePath, backupPath, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Backup Error] {ex.Message}");
        }
    }
}