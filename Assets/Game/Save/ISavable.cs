using System;
using UnityEngine;

namespace Lion.Save
{
    public interface ISavable
    {
        int LoadOrder { get; } // 読み込み順序
        void Save();
        void Load();
    }
}