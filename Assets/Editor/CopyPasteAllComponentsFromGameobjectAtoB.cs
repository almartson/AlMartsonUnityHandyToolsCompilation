using UnityEngine;
using UnityEditor;
//using System.Collections;

/// CopyPasteAllComponentsFromGameobjectAtoB - by Almarza.
/// June 2018.
/// TODO: 
/// 1-  I am creating the next Method-Routine:  ''CopyPasteJustTheTag'', 
/// 2-  and adding it to the Method: 'OnOkButtonPressed()' and rest of options.
///
public class CopyPasteAllComponentsFromGameobjectAtoB : ScriptableWizard 
/* EditorWindow  */
{ 	

    /// <summary>
    /// Options for the Wizard to work
    /// </summary>
    public enum _WIZARD_OPTIONS {   DoNothing,
                                    CopyPasteAllComponentsButNotTheTag,
                                    CopyPasteAllComponentsAndTag

                                    //, CopyPasteJustTheTag
                                };

    /// <summary>
    /// Options for the Wizard to work.
    /// </summary>
    [Tooltip("PLease Choose an Option for the Wizard to work...")]
    public _WIZARD_OPTIONS _myWizardOptionOfChoice = _WIZARD_OPTIONS.DoNothing;

       
    /// <summary>
    /// The Object from which we will Copy the Components.
    /// </summary>
    [Tooltip("The Object from which we will COPY the Components")]
    public GameObject _fromObject;

    /// <summary>
    /// The Target Object to which the Components will be PASTED.
    /// </summary>
    [Tooltip("The Target Object to which the Components will be PASTED")]
    public GameObject _toObject;

    /// <summary>
    /// The SUCCES/ERROR message of the Process.
    /// </summary>
    private string msg = "";


	void OnWizardCreate()
    {
        // Starts the process
        //
        this.OnOkButtonPressed();

	}//End Metodo


    void OnWizardUpdate()
    {

        // Starts the process
        //        
        this.OnOkButtonPressed();

    }//End Metodo


    #region My Methods

    /// <summary>
    /// (It is my custom method) Raises the ok button pressed event.
    /// </summary>
    void OnOkButtonPressed()
    {

        switch (_myWizardOptionOfChoice)
        {
            
            case _WIZARD_OPTIONS.CopyPasteAllComponentsButNotTheTag:

                // Display a Prompt to ask the user:
                //
                if (EditorUtility.DisplayDialog("CONFIRM", "Do you REALLY want to Copy-and-Paste 'All Components But Not The Tag' from the GameObject Above, to the one bellow?", "YES", "NO"))
                {

                    // Clean the string-error-messages
                    //
                    this.msg = "";

                    // Copy components acording to the OPTION SELECTED:
                    //
                    this.StartCopyingAllComponentsButNotTheTag();

                }//end if

            break;


            case _WIZARD_OPTIONS.CopyPasteAllComponentsAndTag:

                // Display a Prompt to ask the user:
                //
                if (EditorUtility.DisplayDialog("CONFIRM", "Do you REALLY want to Copy-and-Paste 'All Components And Tag' from the GameObject Above, to the one bellow?", "YES", "NO"))
                {

                    // Clean the string-error-messages
                    //
                    this.msg = "";

                    // Copy components acording to the OPTION SELECTED:
                    //
                    this.StartCopyingAllComponentsAndTag();

                }//end if

            break;


            //... some other options..:


//            default:
//
//                // Display a Prompt to ask the user:
//                //
//                EditorUtility.DisplayDialog("YOU DID NOT SELECT A VALID OPTION",  "You must SELECT one of the OPTIONS for the Wizard to do something :P" , "OK");
//
//            break;

        }//End switch

    }//End Metodo


    /// <summary>
    /// Starts Copying Process of ''All Components but Not the Tag''.
    /// </summary>
    void StartCopyingAllComponentsButNotTheTag()
    {
        
        Component[] fromComps = _fromObject.GetComponents(typeof(Component));
        Component[] toComps = _toObject.GetComponents(typeof(Component));

        msg += "''FromObject'' total components count is: " + fromComps.Length;
        msg += "\n''ToObject'' total components count is: " + toComps.Length + "\n";

        // lOOP
        //
        for (int i = 0; i < fromComps.Length; ++i)
        {

            // Foor-loop which will go throught every Component in 'fromObject' GameObjetc:
            //
            if ((fromComps[i] != null) && (fromComps[i] != this))
            {

                // 1-   COPY every Component from 'fromObject' GameObjetc, into 'toObject' GameObject:
                //
                if (UnityEditorInternal.ComponentUtility.CopyComponent(fromComps[i]))     // COPY-PASTE
                {

                    // 2-   Message of acknowledgment:
                    //
                    msg += "\nCOPYING Component [" + i + "]: " + fromComps[i].name + ";  TYPE: " + fromComps[i].GetType();


                    // 3-   PASTE, based on 2 use-cases:
                    //
                    if ((toComps[0].GetType() == fromComps[i].GetType()) && (toComps[0].GetType() == typeof(Transform)))
                    {

                        // 3.1- Case 1:  Transform 
                        //      Component MUST only get its VALUES REPLACED.
                        //
                        if (UnityEditorInternal.ComponentUtility.PasteComponentValues(toComps[0]))
                        {

                            // OK
                            // 3.2- Message of acknowledgment:
                            //
                            msg += "\n* ''PasteComponentValues()'':\n Component [" + i + "]: " + fromComps[i].name + "\n TYPE: " + fromComps[i].GetType();

                        }//End if ( UnityEditorInternal.ComponentUtility.PasteComponentValues(toComps[0]) )
                        else
                        {

                            // ERROR
                            // 3.2- Message of acknowledgment:
                            //
                            // Unsucessful PASTE:
                            //
                            Debug.LogWarning("\nERROR: COULD NOT ''PASTE ONLY VALUES'' of Component n° " + i + "\nComponent name: " + fromComps[i].name + ";  TYPE: " + fromComps[i].GetType());
                            //
                            // 2-   Message of acknowledgment:
                            //
                            msg += "\n* ERROR: COULD NOT ''PASTE ONLY VALUES'' of Component n° " + i + "\nComponent name: " + fromComps[i].name + "\n TYPE: " + fromComps[i].GetType();

                        }//End else

                    }//End if
                    else
                    {

                        // 3.1- Case 2:  Other Components
                        //      Component MUST be COPIED ENTIRELY.
                        //
                        if (UnityEditorInternal.ComponentUtility.PasteComponentAsNew(_toObject))
                        {

                            // OK
                            // 3.2- Message of acknowledgment:
                            //
                            msg += "\n* ''PasteComponentAsNew()'': Component [" + i + "]: " + fromComps[i].name + "\n TYPE: " + fromComps[i].GetType();

                        }//End if (UnityEditorInternal.ComponentUtility.PasteComponentAsNew(toObject))
                        else
                        {

                            // ERROR
                            // 3.2- Message of acknowledgment:
                            //
                            // Unsucessful PASTE:
                            //
                            Debug.LogWarning("\nERROR: COULD NOT ''PASTE COMPONENT AS NEW'': Component n° " + i + "\nComponent name: " + fromComps[i].name + "\n TYPE: " + fromComps[i].GetType());
                            //
                            // 2-   Message of acknowledgment:
                            //
                            msg += "\n* ERROR: COULD NOT ''PASTE ONLY VALUES'' of Component n° " + i + "\nComponent name: " + fromComps[i].name + "\n TYPE: " + fromComps[i].GetType();

                        }//End else

                    }//End else del if ( (toComps[0].GetType() == fromComps[i].GetType()) && (toComps[0].GetType() == typeof( Transform ) ) )


                }//End if (UnityEditorInternal.ComponentUtility.CopyComponent(fromComps[i]))     // COPY-PASTE
                else
                {
                    // Unsucessful Copying:
                    //
                    Debug.LogWarning("\nERROR: COULD NOT COPY Component n°" + i + ", because it is NULL\n");
                    //
                    // 2-   Message of acknowledgment:
                    //
                    msg += "\nERROR: COULD NOT COPY Component [" + i + "]: because it is NULL.\nDetail of Component: " + fromComps[i].name + "\n TYPE: " + fromComps[i].GetType();

                }//End else de if COPYING...

            }//End if ((fromComps[i] != null) && (fromComps[i] != this))
            else
            {

                if (fromComps[i] == null)
                {     
                           
                    Debug.LogWarning("\nWARNING: Component n°" + i + "not found in \n ''fromComps[Component n° = " + i + "]'' this project.\nIT IS NULL!\n");
                    //
                    // 3.2- Message of acknowlegment:
                    //
                    msg += "\nWARNING: Component n°" + i + "not found in \n ''fromComps[Component n° = " + i + "]'' this project.\nIT IS NULL!\n";

                }//End if

            }//End else

            // Ack message for each step of the Loop:
            //
            msg += "\n*** Ending COPY-PASTE Operation for Component[" + i + "].\n";

        }//End For

        // IT IS DONE!
        // Ack final message:
        //
        EditorUtility.DisplayDialog("Results of the Copy Process",  msg , "OK", "");

    }//End Metodo
 

    /// <summary>
    /// Starts Copying Process of ''All Components And Tag''.
    /// </summary>
    void StartCopyingAllComponentsAndTag()
    {

        // 1.1-   Copy the TAG:
        //
        this._toObject.tag = this._fromObject.tag;
        //
        // Ack message
        //
        msg += "\n*** Ending COPY-PASTE Operation of TAG: \n" + this._toObject.tag + "\n";
        //
        // 1.2-   Copy the Gameobject LAYER (not ''Sorting Layer''):
        //
        this._toObject.layer = this._fromObject.layer;
        //
        // Ack message
        //
        msg += "\n*** Ending COPY-PASTE Operation of LAYER: \n" + this._toObject.layer + "\n\n";


        // 2-   Copy Components
        //
        // Copy components, only:
        //
        this.StartCopyingAllComponentsButNotTheTag();

    }//End Metodo


    #endregion My Methods
   
}
