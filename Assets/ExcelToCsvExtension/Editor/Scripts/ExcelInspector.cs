#if UNITY_EDITOR
using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Lion.Utility
{
    [CustomEditor(typeof(DefaultAsset), true)]
    public class ExcelInspector : Editor
    {
        private static string relativePath = "ExcelToCsvExtension\\Editor\\ExcelToCsvSingle.exe";
        private static string fullPath = Path.Combine(Application.dataPath.Replace("/", "\\"), relativePath);

        public override void OnInspectorGUI()
        {
            // デフォルトのインスペクタを表示
            base.OnInspectorGUI();

            EditorGUI.EndDisabledGroup();
            // ファイルのパスを取得
            string assetPath = AssetDatabase.GetAssetPath(target);

            // 拡張子が .xls または .xlsx の場合にカスタムインスペクタを表示
            if (assetPath.EndsWith(".xls") || assetPath.EndsWith(".xlsx"))
            {
                if (GUILayout.Button("Convert to Csv"))
                {
                    // 実行するExcelファイルのパス
                    string excelFilePath = fullPath;

                    // プロセスの設定
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = fullPath, // 実行可能ファイルのパス
                        Arguments = $"\"{assetPath}\"", // 引数としてExcelファイルのパスを渡す
                        UseShellExecute = false, // シェルを使用しない
                        RedirectStandardOutput = true, // 標準出力をリダイレクト
                        RedirectStandardError = true, // 標準エラーをリダイレクト
                        CreateNoWindow = true // コンソールウィンドウを作成しない
                    };

                    try
                    {
                        using (Process process = Process.Start(startInfo))
                        {
                            // 標準出力の読み取り
                            string output = process.StandardOutput.ReadToEnd();
                            UnityEngine.Debug.Log("Log: " + output);

                            // 標準エラーの読み取り
                            string error = process.StandardError.ReadToEnd();
                            if (!string.IsNullOrEmpty(error))
                            {
                                UnityEngine.Debug.LogError("エラー: " + error);
                            }

                            // プロセスの終了を待つ
                            process.WaitForExit();
                        }
                    }
                    catch (Exception ex)
                    {
                        UnityEngine.Debug.LogError("プロセスの起動に失敗しました: " + ex.Message);
                    }

                    AssetDatabase.Refresh();
                }
            }
        }
    }
}
#endif