using NuGet;

namespace WebApplication1.AutoUpdate
{
    public class InstallationState
    {
        public IPackage Installed { get; set; }

        public IPackage Update { get; set; }
    }
}
