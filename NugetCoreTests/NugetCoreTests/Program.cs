using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet;

namespace NugetCoreTests
{
    class Program
    {
        static readonly IPackageRepository Repository =
            PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
        static readonly string Path =
            System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");
        static void Main(string[] args)
        {
            while (true)
            {
            init:
                Console.WriteLine("Digite o id do pacote, por exemplo: EntityFramework");
                var id = Console.ReadLine();
                Console.WriteLine("Digite a versão do pacote, por exemplo: 6.1.1");
                var version = Console.ReadLine();
                //Install(id, SemanticVersion.Parse("5.0.0"));
                try
                {
                    Install(id, SemanticVersion.Parse(version));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    goto init;
                }

                Console.WriteLine("Continuar ? [S]/[n]");
                var keyInfo = Console.ReadKey();
                bool continueLoop = false;
                switch (keyInfo.Key)
                {
                    case ConsoleKey.S:
                        {
                            continueLoop = true;
                            try
                            {
                                Uninstall(id, SemanticVersion.Parse(version));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                        }
                    default:
                        {
                            continueLoop = false;
                            break;
                        }
                }
                if (!continueLoop)
                {
                    Console.WriteLine("Desinstalar ? [S]/[n]");
                    var uninstall = Console.ReadKey();
                    if (uninstall.Key == ConsoleKey.S)
                    {
                        try
                        {
                            Uninstall(id, SemanticVersion.Parse(version));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    break;
                }
            }
            Process.Start("explorer", AppDomain.CurrentDomain.BaseDirectory);
        }

        static void Install(string pkgName, SemanticVersion version)
        {
            //Initialize the package manager

            var packageManager = new PackageManager(Repository, Path);
            //Download and unzip the package
            packageManager.InstallPackage(pkgName, version);
        }

        static void Uninstall(string pkgName, SemanticVersion version)
        {
            var packageManager = new PackageManager(Repository, Path);

            packageManager.UninstallPackage(pkgName, version);
        }
    }
}
