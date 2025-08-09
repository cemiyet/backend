using System;
using System.IO;
using System.Xml.Linq;

if (args.Length != 2)
{
    Console.WriteLine("Usage: CsprojRootNamespaceSetter <csprojPath> <RootNamespace>");
    return 1;
}

var csprojPath = args[0];
var rootNamespace = args[1];

if (!File.Exists(csprojPath))
{
    Console.WriteLine($"File not found: {csprojPath}");
    return 1;
}

try
{
    var doc = XDocument.Load(csprojPath);
    var ns = doc.Root.Name.Namespace;

    var propertyGroup = doc.Root.Elements(ns + "PropertyGroup").LastOrDefault();
    if (propertyGroup == null)
    {
        propertyGroup = new XElement(ns + "PropertyGroup");
        doc.Root.Add(propertyGroup);
    }

    var rootNsElement = propertyGroup.Element(ns + "RootNamespace");
    if (rootNsElement != null)
    {
        rootNsElement.Value = rootNamespace;
    }
    else
    {
        propertyGroup.Add(new XElement(ns + "RootNamespace", rootNamespace));
    }

    doc.Save(csprojPath);
    Console.WriteLine($"Set RootNamespace='{rootNamespace}' in {csprojPath}");
    return 0;
}
catch (Exception ex)
{
    Console.WriteLine($"Error processing file: {ex.Message}");
    return 1;
}
