using System;

namespace sg1
{
    class Program
    {
        static void Main(string[] args)
        {
            PathFinder.GetInstallationPaths().ForEach(cd =>
            {
                if (BuildUtils.Modify(cd, out string build))
                {
                    if (Config.Restart)
                    {
                        BuildUtils.RestartBuild(build);
                    }
                }
            });
            Console.ReadLine();
        }
    }
}
