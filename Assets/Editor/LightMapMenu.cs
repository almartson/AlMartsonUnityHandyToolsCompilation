using UnityEngine;
using UnityEditor;

/// <summary>
/// Light map menu. My Emissive Material/Shader Does Not Appear In The Lightmap.
/// Taken from: https://support.unity3d.com/hc/en-us/articles/214718843-My-Emissive-material-shader-does-not-appear-in-the-Lightmap-
///
/// ''This script will set the Global Illumination option to Baked in each object tagged as Emissive_to_Lightmap. 
/// It will then execute the Bake function to create the Lightmap.  Now, everything should work as expected and the Emissive Light should appear in the Lightmap
/// even with custom shaders.''
///
/// </summary>
public class LightMapMenu : ScriptableWizard
{

    /// <summary>
    /// NAME of the TAG to Bake EMISSION (the objects must be in this Layer). FLAG for TURNING THIS OPTION ON: BAKE EMISSION MAPS (for objects in that particular Layer).
    /// </summary>
    [Tooltip("NAME of the TAG to Bake EMISSION (the objects must be in this Layer). FLAG for TURNING THIS OPTION ON: BAKE EMISSION MAPS (for objects in taht particular Layer).")]
    public string _myTagOfGameObjects_Emissive_to_baked = "Emissive_to_baked";

    /// <summary>
    /// NAME of the LAYER to Bake EMISSION (the objects must be in this Layer). FLAG for TURNING THIS OPTION ON: BAKE EMISSION MAPS (for objects in that particular Layer).
    /// </summary>
    [Tooltip("NUMBER (you have to lookup for it manually in the LAYERS options) of the LAYER to Bake EMISSION (the objects must be in this Layer). FLAG for TURNING THIS OPTION ON: BAKE EMISSION MAPS (for objects in that particular Layer).")]
    public int _myLayerNumberOfGameObjects_Emissive_to_baked = 8;

    /// <summary>
    /// You want AMBIENT OCCLUSION?
    /// </summary>
    [Tooltip("You want AMBIENT OCCLUSION?")]
    public bool _enableAmbientOcclusion = true;


    /// <summary>
    /// The SUCCES/ERROR message of the Process.
    /// </summary>
    private string msg = "";


    void OnWizardCreate()
    {
        // Starts the process
        //
        //this.OnOkButtonPressed();
        //
        // Display a Prompt to ask the user:
        //
        if (EditorUtility.DisplayDialog("CONFIRM", "Do you REALLY want to ''Bake All Objects Including Emission (GameObjects) Light Into Lightmaps''?", "YES", "NO"))
        {

            // Clean the string-error-messages
            //
            this.msg = "";

            // Bake all LIGHTMAPS, including EMISSION MAPS
            //
            this.Bake();

        }//end if

    }//End Metodo


//    void OnWizardUpdate()
//    {
//
//        // Starts the process
//        //        
//        this.OnOkButtonPressed();
//
//    }//End Metodo


    /// <summary>
    /// The menu item. Bake this instance.
    /// </summary>
    //[MenuItem ("MyCustomBake/Bake")]
    public void Bake ()
    {
        // 0- Solving Bug: Computing lightmaps from script is not available in iterative mode. Please change Lightmapping.giWorkflowMode to GIWorkflowMode.OnDemand.   UnityEditor.Lightmapping:Bake()
        //
        Lightmapping.giWorkflowMode = Lightmapping.GIWorkflowMode.OnDemand;  // GIWorkflowMode.OnDemand

        // 1-   By TAG:
        //
        // Find all objects with the tag <Emissive_to_baked>
        // We have to set the tag “Emissive_to_baked” on each GO to be baked.
        //
        GameObject[] _emissiveObjsByTag = GameObject.FindGameObjectsWithTag( _myTagOfGameObjects_Emissive_to_baked );

        if ( _emissiveObjsByTag != null )
        {

            // Then, by each object, set the globalIlluminationFlags to BakedEmissive.
            //
            foreach (GameObject tmpObj in _emissiveObjsByTag)
            {

                // Access SHARED MATERIAL, and change the BAKE EMISSION ''FLAG'':
                //
                this.MarkGameObjectMeshForBakeEmisionLightmap( tmpObj );


            // Codigo original:
            //
//                Material tmpMaterial = tmpObj.GetComponent<Renderer> ().sharedMaterial;
//                tmpMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;

            }//End for

        }//End if ( _emissiveObjsByTag != null )


        // 2-   By LAYER:
        //
        // Find all objects with the tag <Emissive_to_baked>
        // We have to set the tag “Emissive_to_baked” on each GO to be baked.
        //
        GameObject[] _emissiveObjsByLayer = this.FindGameObjectsWithLayer( _myLayerNumberOfGameObjects_Emissive_to_baked ); // GameObject.fin  .FindGameObjectsWithTag( this._myLayerNameOfGameObjects_Emissive_to_baked );


        if ( _emissiveObjsByLayer != null )
        {
            // Then, by each object, set the globalIllumiationFlags to BakedEmissive.
            //
            foreach (GameObject tmpObj in _emissiveObjsByLayer)
            {

                // Access SHARED MATERIAL, and change the BAKE EMISSION ''FLAG'':
                //
                this.MarkGameObjectMeshForBakeEmisionLightmap( tmpObj );


                // Codigo original:
                //
//                Material tmpMaterial = tmpObj.GetComponent<Renderer> ().sharedMaterial;
//                tmpMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;

            }//End for

        }//End if ( _emissiveObjsByLayer != null )


        // 3.1-   Bake OPTION: DISABLE AMBIENT OCCLUSION for this OBJETCs.
        //
        LightmapEditorSettings.enableAmbientOcclusion = this._enableAmbientOcclusion;

        //LightmapParameters

        // 3.2-   Bake the lightmap.
        //
        Lightmapping.Bake ();

    }//End Method Bake




    #region Important Method

    /// <summary>
    /// Set the globalIlluminationFlags to BakedEmissive for the INPUT GameObject. 
    /// Marks the game object mesh for bake emission lightmap.
    /// </summary>
    /// <param name="tmpObj">Tmp object.</param>
    private void MarkGameObjectMeshForBakeEmisionLightmap (GameObject tmpObj)
    {

        Material tmpMaterial = tmpObj.GetComponent<Renderer> ().sharedMaterial;
        tmpMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;

    }//End for

    #endregion


    #region Utility

    public GameObject[] FindGameObjectsWithLayer( int myLayerNumber )
    {
        // List of all object in scene:
        //
        GameObject[] goArray = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
        //
        // Length of array:
        //
        int longitudDe_GoArray = goArray.Length;


        // End List of objects ""by LAYER"":
        //
        System.Collections.Generic.List<GameObject> goList = new System.Collections.Generic.List<GameObject>();

        // Search in every GameObject by the Layer it has
        //
        for (int i = 0; i < longitudDe_GoArray; i++)
        {
            if ( goArray[i].layer == myLayerNumber )
            {
                // Add to List:
                //
                goList.Add( goArray[i] );

            }//End if

        }//End for
        //
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();

    }//End Method Bake



//    GameObject[] FindGameObjectsWithLayer (int layer)
//    {
//        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
//        List goList = new List();
//
//        for (int i = 0; i < goArray.Length; i++)
//        {
//            if (goArray[i].layer == layer)
//            {
//                goList.Add(goArray[i]);
//            }
//        }
//
//        if (goList.Count == 0)
//        {
//            return null;
//        }
//        return goList.ToArray();
//    }
//         }
//     }
//     if (goList.Count == 0) {
//         return null;
//     }
//     return goList.ToArray();
//    }
//
//
//GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
//foreach(GameObject go in gos)
//{
//    if(go.layer=="LayerName" && go.CompareTag("Tagname")
//    {
//        //do something
//    }
//} 

    #endregion



}
