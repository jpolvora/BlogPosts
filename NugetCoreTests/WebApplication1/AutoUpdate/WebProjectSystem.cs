using System;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Web.Hosting;
using NuGet;

namespace WebApplication1.AutoUpdate
{
    public class WebProjectSystem : PhysicalFileSystem, IProjectSystem
    {
        private const string BinDir = "bin";

        public string ProjectName
        {
            get
            {
                return this.Root;
            }
        }

        public bool IsBindingRedirectSupported { get; private set; }

        public bool FileExistsInProject(string path)
        {
            //customizar aqui
            return FileExists(path);
        }

        public FrameworkName TargetFramework
        {
            get
            {
                return VersionUtility.DefaultTargetFramework;
            }
        }

        public WebProjectSystem(string root)
            : base(root)
        {
        }

        public void AddReference(string referencePath, Stream stream)
        {
            this.AddFile(this.GetFullPath(this.GetReferencePath(Path.GetFileName(referencePath))), stream);
        }

        public void AddFrameworkReference(string name)
        {

        }

        public object GetPropertyValue(string propertyName)
        {
            if (propertyName == null)
                return (object)null;
            if (propertyName.Equals("RootNamespace", StringComparison.OrdinalIgnoreCase))
                return (object)string.Empty;
            else
                return (object)null;
        }

        public bool IsSupportedFile(string path)
        {
            if (!path.StartsWith("tools", StringComparison.OrdinalIgnoreCase))
                return !Path.GetFileName(path).Equals("app.config", StringComparison.OrdinalIgnoreCase);
            else
                return false;
        }

        public string ResolvePath(string path)
        {
            //posso customizar o path aqui
            return path;
        }

        public void AddImport(string targetPath, ProjectImportLocation location)
        {
            //throw new NotImplementedException();
        }

        public void RemoveImport(string targetPath)
        {
            //throw new NotImplementedException();
        }

        public bool ReferenceExists(string name)
        {
            return this.FileExists(this.GetReferencePath(name));
        }

        public void RemoveReference(string name)
        {
            this.DeleteFile(this.GetReferencePath(name));
            if (this.GetFiles(BinDir, true).Any())
                return;
            this.DeleteDirectory(BinDir);
        }

        protected virtual string GetReferencePath(string name)
        {
            return Path.Combine(BinDir, name);
        }
    }
}