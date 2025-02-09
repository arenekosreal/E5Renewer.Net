﻿using E5Renewer.Models.Modules;

namespace E5Renewer.Models.ModulesCheckers;

/// <summary>The api interface of checking module.</summary>
public interface IModulesChecker : IModule
{
    /// <summary>Check the module.</summary>
    /// <param name="module">The module to check.</param>
    public void CheckModules(IModule module);
}
