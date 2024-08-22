using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lion.Stage
{
    [CreateAssetMenu(
    fileName = "StageSheet",
    menuName = "Game Data Sheets/StageSheet")]
    public class StageSheet : Lion.GameDataSheet.SheetBase<StageData>
    {
        private Dictionary<int, StageData> _stageDataDictionary = new Dictionary<int, StageData>();

        public void Initialize()
        {
            foreach (var stage in this)
            {
                _stageDataDictionary.Add(stage.ID, stage);
                stage.Initialize();
            }
        }

        public StageData GetStageData(int id)
        {
            if (_stageDataDictionary.TryGetValue(id, out var stageData))
            {
                return stageData;
            }
            return null;
        }
    }


#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(StageSheet))]
    public class StageSheetDrawer : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Window"))
            {
                StageSheetWindow.Init();
            }

            base.OnInspectorGUI();
        }
    }
#endif
}