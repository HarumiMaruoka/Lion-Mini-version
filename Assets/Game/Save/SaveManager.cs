using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lion.Save
{
    public class SaveManager
    {
        public static SaveManager Instance { get; } = new SaveManager();
        private SaveManager() { }

        private List<ISavable> _savables = new List<ISavable>();

        public void Register(ISavable savable)
        {
            if (_savables.Contains(savable)) return;

            _savables.Add(savable);
            _savables.Sort((a, b) => a.LoadOrder - b.LoadOrder);
        }

        public void Unregister(ISavable savable)
        {
            _savables.Remove(savable);
        }

        public void Save()
        {
            foreach (var savable in _savables)
            {
                savable.Save();
            }
        }

        public void Load()
        {
            foreach (var savable in _savables)
            {
                savable.Load();
            }
        }
    }
}