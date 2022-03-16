﻿using System;

namespace Infrastructure.Services
{
    public interface ISceneLoader
    {
        void Load(string name, Action onLoaded = null);
    }
}