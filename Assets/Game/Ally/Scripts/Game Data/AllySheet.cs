using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lion.Ally
{
    [CreateAssetMenu(
    fileName = "AllySheet",
    menuName = "Game Data Sheets/AllySheet")]
    public class AllySheet : Lion.GameDataSheet.SheetBase<AllyData>
    {
        private Dictionary<int, AllyData> _allyDataByID = new Dictionary<int, AllyData>();

        public void Initialize()
        {
            foreach (var data in this)
            {
                data.Initialize();
                _allyDataByID.Add(data.ID, data);
            }
        }

        public AllyData GetAllyData(int id)
        {
            if (_allyDataByID.ContainsKey(id))
            {
                return _allyDataByID[id];
            }
            return null;
        }

        public bool TryGetAllyData(int id, out AllyData ally)
        {
            return _allyDataByID.TryGetValue(id, out ally);
        }
    }


#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(AllySheet))]
    public class AllySheetDrawer : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Window"))
            {
                AllySheetWindow.Init();
            }

            base.OnInspectorGUI();
        }
    }
#endif
}