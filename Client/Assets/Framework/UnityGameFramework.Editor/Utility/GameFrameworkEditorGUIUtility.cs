using System;
using UnityEditor;

namespace UnityGameFramework.Editor
{
    /// <summary>
    /// 游戏框架EditorGUI 工具类。
    /// </summary>
    public static class GameFrameworkEditorGUIUtility
    {
        /// <summary>
        /// 创建缩进级别改变范围。
        /// </summary>
        public static IDisposable MakeIndentLevelChangedScope(int modify)
        {
            return new IndentLevelChangedScope(modify);
        }

        private sealed class IndentLevelChangedScope : IDisposable
        {
            private readonly int m_OriginIndentLevel;
            
            public IndentLevelChangedScope(int modify)
            {
                m_OriginIndentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel += modify;
            }

            public void Dispose()
            {
                EditorGUI.indentLevel = m_OriginIndentLevel;
            }
        }
    }
}