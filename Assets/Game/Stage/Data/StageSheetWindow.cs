#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Lion.Stage
{
    public class StageSheetWindow : Lion.GameDataSheet.SheetWindowBase<StageData, StageSheet, StageSheetWindowLayout>
    {
        [MenuItem("Window/Game Data Sheet/StageSheetWindow")]
        public static void Init()
        {
            GetWindow(typeof(StageSheetWindow)).Show();
        }
    }
}
#endif