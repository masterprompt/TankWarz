using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Tangatek.ViewManagement
{
    public class ViewSpace_Editor : EditorWindow
    {
        #region Menu Item
        [MenuItem("Window/ViewSpace")]
        public static void ShowWindow()
        {
            var window = EditorWindow.GetWindow(typeof(ViewSpace_Editor));
            window.title = "ViewSpace";
        }
        #endregion
    }
}
