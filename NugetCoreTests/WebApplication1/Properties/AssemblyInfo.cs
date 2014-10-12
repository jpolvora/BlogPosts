using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
using WebApplication1.Properties;

[assembly: AssemblyTitle("WebApplication1")]
[assembly: AssemblyDescription("Teste")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Jone Polvora")]
[assembly: AssemblyProduct("WebApplication1")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("Jone Polvora Trademark")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("a0c83e28-6eae-4c2a-8a4b-e64e1fee5127")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers
// by using the '*' as shown below:
[assembly: AssemblyVersion(AssemblyConstants.AssemblyVersion)]
[assembly: AssemblyFileVersion(AssemblyConstants.AssemblyVersion)]
[assembly: AssemblyInformationalVersion(AssemblyConstants.PackageVersion)]

// ReSharper disable once CheckNamespace
namespace WebApplication1.Properties
{
    internal static class AssemblyConstants
    {
        internal const string PackageVersion = "0.0.3";
        internal const string AssemblyVersion = PackageVersion + ".0";
        internal const string PrereleaseVersion = ""; // Until we ship 1.0, this isn't necessary.
    }
}
