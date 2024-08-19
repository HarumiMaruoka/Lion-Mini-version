#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Lion.GameDataSheet
{
    public class AssetFinder
    {
        public static T FindAssetsByType<T>() where T : UnityEngine.Object
        {
            // �����t�B���^���g�p���ăA�Z�b�g������
            string filter = "t:" + typeof(T).Name;
            string[] guids = AssetDatabase.FindAssets(filter);

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                return AssetDatabase.LoadAssetAtPath<T>(path); // �ŏ��Ɍ����������Ԃ��B
            }

            return null;
        }
    }
}
#endif