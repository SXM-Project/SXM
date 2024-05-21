﻿using CitizenFX.Core;
using Sxm.Core.Server;
using Sxm.Core.Server.Extensions;
using Sxm.DependencyInjection;
using Sxm.DependencyInjection.Extensions;

namespace Sxm.Server;

public sealed class Main : BaseScript
{
    public Main()
    {
        var services = new ServiceCollection();

        services.AddSingleton(typeof(ExportDictionary), typeof(ExportDictionary), Exports);
        services.AddSingleton(typeof(EventHandlerDictionary), typeof(EventHandlerDictionary), EventHandlers);
        services.AddSingleton(typeof(StateBag), typeof(StateBag), GlobalState);

        services.AddCore(options =>
        {
            options.Assemblies =
            [
                typeof(Main).Assembly,
                typeof(Script).Assembly,
                typeof(Sxm.Economy.Server.Main).Assembly
            ];
        });
    }
}