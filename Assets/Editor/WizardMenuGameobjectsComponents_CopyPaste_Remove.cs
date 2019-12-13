//using UnityEngine;
using UnityEditor;
//using System.Collections;

/// <summary>
/// WizardMenuGameobjectsComponents_CopyPaste_Remove - by Almarza.
/// June 2018.
/// 
/// This is the MAIN MENU of the Tool for: 
///   1- Copying multiple (all) Components from GameObject A to B.
///   2- Deleting all Components from GameObject A.
///   3- Baking: All Objects Including Emission (GameObjects) Light Into Lightmaps (it solves a 2017.1 Known Unity problem: https://support.unity3d.com/hc/en-us/articles/214718843-My-Emissive-material-shader-does-not-appear-in-the-Lightmap- ).
/// </summary>
public class WizardMenuGameobjectsComponents_CopyPaste_Remove : ScriptableWizard 
/* EditorWindow  */
{ 	

    // Attributes:
    // ...

    // Buttons or Wizard Options:

    [MenuItem ("GameObject and Components/Copy Paste All Components")]    
	static void CreateWizardCopyComponents ()
	{
        ScriptableWizard.DisplayWizard("Copy All Components", typeof(CopyPasteAllComponentsFromGameobjectAtoB), "Copy");
	}

    [MenuItem ("GameObject and Components/Remove All Components")]    
    static void CreateWizardRemoveAllComponents ()
    {
        ScriptableWizard.DisplayWizard("Remove All Components", typeof(RemoveAllComponentsInGameobject), "Remove");
    }

    /// <summary>
    /// The menu item. Bake this instance.
    /// </summary>
    [MenuItem ("MyCustomBake/Bake All Objects Including Emission (GameObjects) Light Into Lightmaps")]
    static void CreateWizardBakeAllObjectsIncludingEmissionLightIntoLightmaps ()
    {
        ScriptableWizard.DisplayWizard("Bake All Objects Including Emission (GameObjects) Light Into Lightmaps", typeof(LightMapMenu), "Bake");
    }


    #region My Methods


    #endregion My Methods
   
}
