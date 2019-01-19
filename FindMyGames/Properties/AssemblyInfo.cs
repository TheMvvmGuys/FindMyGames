using System;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using TheMvvmGuys.FindMyGames.UI;
using static AssemblyData;
// oops visual studio spit some french lmao
// Les informations générales relatives à un assembly dépendent de
// l'ensemble d'attributs suivant. Changez les valeurs de ces attributs pour modifier les informations
// associées à un assembly.
[assembly: AssemblyTitle("FindMyGames")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("FindMyGames")]
[assembly: AssemblyCopyright("Copyright ©  2019")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
// L'affectation de la valeur false à ComVisible rend les types invisibles dans cet assembly
// aux composants COM. Si vous devez accéder à un type dans cet assembly à partir de
// COM, affectez la valeur true à l'attribut ComVisible sur ce type.
[assembly: ComVisible(false)]

//Pour commencer à générer des applications localisables, définissez
//<UICulture>CultureUtiliséePourCoder</UICulture> dans votre fichier .csproj
//dans <PropertyGroup>.  Par exemple, si vous utilisez le français
//dans vos fichiers sources, définissez <UICulture> à fr-FR. Puis, supprimez les marques de commentaire de
//l'attribut NeutralResourceLanguage ci-dessous. Mettez à jour "fr-FR" dans
//la ligne ci-après pour qu'elle corresponde au paramètre UICulture du fichier projet.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, // For Windows-specific themes like
    ResourceDictionaryLocation.SourceAssembly // For Themes/Generic.xaml
)]

// XAML Namespaces
[assembly: XmlnsDefinition(FindMyGamesXamlNamespace, Base + "UI")]
[assembly: XmlnsDefinition(FindMyGamesXamlNamespace, Base + "UI.Commands")]
[assembly: XmlnsDefinition(FindMyGamesXamlNamespace, Base + "UI.Controls")]
[assembly: XmlnsDefinition(FindMyGamesXamlNamespace, Base + "Extensions.Xaml")]
[assembly: XmlnsDefinition(FindMyGamesXamlNamespace, Base + "Themes")]
[assembly: XmlnsDefinition(FindMyGamesXamlNamespace, Base + "Converters")]
[assembly: XmlnsPrefix(FindMyGamesXamlNamespace, "c")]

// You can use * for random numbers lol
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

internal static class AssemblyData
{
    public const string FindMyGamesXamlNamespace = "http://findmygames.com/xaml";
    public const string Base = "TheMvvmGuys.FindMyGames.";
    public const string AssemblyName = "FindMyGames";
}