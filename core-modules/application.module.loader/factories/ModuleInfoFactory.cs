using System.Reflection;
using Prism.Modularity;

namespace application.module.loader.factories;

internal class ModuleInfoFactory : IModuleInfoFactory
{
    public IModuleInfo CreateModuleInfo(Type type)
    {
        var moduleName = type.Name;
        var onDemand = false;
        var moduleAttribute =
            CustomAttributeData.GetCustomAttributes(type)
                .FirstOrDefault(attributeData
                    => attributeData.Constructor.DeclaringType?.FullName == typeof(ModuleAttribute).FullName);

        if (moduleAttribute != null)
        {
            foreach (var argument in moduleAttribute.NamedArguments)
            {
                var argumentName = argument.MemberInfo.Name;
                switch (argumentName)
                {
                    case "ModuleName":
                        moduleName = (string)argument.TypedValue.Value!;
                        break;

                    case "OnDemand":
                        onDemand = (bool)argument.TypedValue.Value!;
                        break;

                    case "StartupLoaded":
                        onDemand = !((bool)argument.TypedValue.Value!);
                        break;
                }
            }
        }

        var moduleDependencyAttributes =
            CustomAttributeData.GetCustomAttributes(type).Where(
                cad => cad.Constructor.DeclaringType!.FullName == typeof(ModuleDependencyAttribute).FullName);

        var dependsOn = moduleDependencyAttributes.Select(cad => cad.ConstructorArguments[0].Value as string)
            .ToList();

        var moduleInfo = new ModuleInfo(moduleName, type.AssemblyQualifiedName)
        {
            InitializationMode = onDemand ? InitializationMode.OnDemand : InitializationMode.WhenAvailable,
            Ref = type.Assembly.Location,
        };

        foreach (var dependency in dependsOn)
        {
            moduleInfo.DependsOn.Add(dependency);
        }
        return moduleInfo;
    }

    public IEnumerable<IModuleInfo> GetModuleInfos(in DirectoryInfo directory, Type moduleType)
    {
        var validAssemblies = new List<Assembly>();
        var fileInfos = directory.GetFiles("*.dll").ToList();

        fileInfos.ForEach(fileInfo
            => {
                try { validAssemblies.Add(Assembly.LoadFrom(fileInfo.FullName)); }
                catch (BadImageFormatException) { }
            });

        return validAssemblies.SelectMany(assembly
            => assembly.GetExportedTypes()
                .Where(moduleType.IsAssignableFrom)
                .Where(t => t != moduleType)
                .Where(t => !t.IsAbstract)
                .Select(CreateModuleInfo));
    }

}

