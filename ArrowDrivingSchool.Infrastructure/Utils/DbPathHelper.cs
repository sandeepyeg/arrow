namespace ArrowDrivingSchool.Infrastructure.Utils;

public static class DbPathHelper
{
    public static string GetDbPath()
    {
        var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var driveFolder = Path.Combine(userProfile, "Google Drive", "ArrowDrivingSchool", "data");

        if (!Directory.Exists(driveFolder))
            Directory.CreateDirectory(driveFolder);

        return Path.Combine(driveFolder, "arrow.db");
    }
}