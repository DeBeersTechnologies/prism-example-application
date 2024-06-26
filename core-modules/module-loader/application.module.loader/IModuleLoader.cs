﻿namespace application.module.loader;

public interface IModuleLoader
{
    int ScanAndLoadModules(in string path, bool doNotScanChildDirectories = false);

    void LoadModules(string path);
}