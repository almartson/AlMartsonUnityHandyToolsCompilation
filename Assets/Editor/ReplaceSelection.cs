using UnityEngine;
using UnityEditor;
//using System.Collections;

/// <summary>
/// Source: https://wiki.unity3d.com/index.php/ReplaceSelection
///  ReplaceSelection
/// From Unify Community Wiki
/// Author: Dave A and yesfish
/// 
/// This wizard will replace a selection with an object or prefab.
/// Scene objects will be cloned (destroying their prefab links).
/// Original coding by 'yesfish', nabbed from Unity Forums
///  'keep parent' added by Dave A (also removed 'rotation' option, using localRotation)
///
/// </summary>
public class ReplaceSelection : ScriptableWizard
{
    static GameObject replacement = null;
    static bool keep = false;

    [Tooltip("This wizard will replace a selection with an object or prefab.")]
    public GameObject ReplacementObject = null;

    /// <summary>
    /// The keep original GameObject loaded in the Box Above.
    /// </summary>
    public bool KeepOriginals = false;
 
    [MenuItem("GameObject and Components/Replace GameObject Selected in ''Hierarchy View''...")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard(
            "Replace Selection", typeof(ReplaceSelection), "Replace");
    }
 
    public ReplaceSelection()
    {
        ReplacementObject = replacement;
        KeepOriginals = keep;
    }
 
    void OnWizardUpdate()
    {
        replacement = ReplacementObject;
        keep = KeepOriginals;
    }
 
    void OnWizardCreate()
    {
        if (replacement == null)
            return;
 
        Undo.RegisterSceneUndo("Replace Selection");
 
        Transform[] transforms = Selection.GetTransforms(
            SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);
 
        foreach (Transform t in transforms)
        {
            GameObject g;
            PrefabType pref = EditorUtility.GetPrefabType(replacement);
 
            if (pref == PrefabType.Prefab || pref == PrefabType.ModelPrefab)
            {
                g = (GameObject)EditorUtility.InstantiatePrefab(replacement);
            }
            else
            {
                g = (GameObject)Editor.Instantiate(replacement);
            }
 
            Transform gTransform = g.transform;
            gTransform.parent = t.parent;
            g.name = replacement.name;
            gTransform.localPosition = t.localPosition;
            gTransform.localScale = t.localScale;
            gTransform.localRotation = t.localRotation;
        }
 
        if (!keep)
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                GameObject.DestroyImmediate(g);
            }
        }
    }
}