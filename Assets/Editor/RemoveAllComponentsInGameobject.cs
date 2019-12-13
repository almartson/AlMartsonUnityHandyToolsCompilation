using UnityEngine;
using UnityEditor;
//using System.Collections;

/// RemoveAllComponentsInGameobject - by Almarza.
/// June 2018.
/// TODO:
/// 1-  I am creating next: the METHOD-ROUTINE ''RemoveAllComponentsAndTag'', 
/// 2-  and adding it to the next of options in ''onOkButtonPressed()'' Method.
///
public class RemoveAllComponentsInGameobject : ScriptableWizard 
/* EditorWindow  */
{ 	

    /// <summary>
    /// Options for the Wizard to work
    /// </summary>
    public enum _WIZARD_OPTIONS {   DoNothing,
                                    RemoveAllComponentsButNotTheTag //,
                                    // RemoveAllComponentsAndTag
                                    //,  RemoveOnlyTheTagAndSetToDefault, DestroyTheGameObject 
                                };

    /// <summary>
    /// Options for the Wizard to work.
    /// </summary>
    [Tooltip("PLease Choose an Option for the Wizard to work...")]
    public _WIZARD_OPTIONS _myWizardOptionOfChoice = _WIZARD_OPTIONS.DoNothing;



    /// <summary>
    /// The Target Object to REMOVE the Components
    /// </summary>
    [Tooltip("The Target Object to which the Components will be PASTED")]
    public GameObject _myGameobject;



	void OnWizardCreate()
    {

        // Starts the Process:
        //
        this.OnOkButtonPressed();

	}//End Metodo


    void OnWizardUpdate()
    {

        // Starts the Process:
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
            
            case _WIZARD_OPTIONS.RemoveAllComponentsButNotTheTag:


                // Display a Prompt to ask the user:
                //
                if (EditorUtility.DisplayDialog("CONFIRM", "Do you REALLY want to REMOVE 'All Components But Not The Tag' in MY GAME OBJECT?", "YES", "NO"))
                {

                    // Remove Components acording to the OPTION SELECTED:
                    //
                    this.StartRemoveAllComponentsButNotTheTag();

                }//end if

            break;


            //... some other options..:


//            case _WIZARD_OPTIONS.RemoveAllComponentsButNotTheTag:
//
//
//            break;


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
    /// Starts the Removal Process of all ''Components but Not the Tag''.
    /// </summary>
    void StartRemoveAllComponentsButNotTheTag()
    {

        // List of Components:
        //
        Component[] _myListOfComponents = this._myGameobject.GetComponents(typeof(Component));

        string msg = "";

        int myListOfComponentsLength = _myListOfComponents.Length;

        msg += "''My GameObject'' total components count is: " + myListOfComponentsLength;


        // 1-   REMOVE every Component from 'My Game Object':
        //
        // Delete in reverse order (to avoid errors when RequireComponent is used)
        //
        for (int i = myListOfComponentsLength - 1; i > 0; i--)
        {

            if ((_myListOfComponents != null) && (_myListOfComponents[i] != null) && (_myListOfComponents[i] != this))
            {

                // Ack message for each step of the Loop:
                //
                msg += "\n*** Ending REMOVAL Operation for Component:\n* " + i + " - " + _myListOfComponents[i].GetType();

                // Remove component
                //
                UnityEngine.Object.DestroyImmediate(_myListOfComponents[i]);

            }//End if ((_myListOfComponents != null) && (_myListOfComponents[i] != null) && (_myListOfComponents[i] != this))

        }//End for

        // IT IS DONE!
        // Ack final message:
        //
        EditorUtility.DisplayDialog("Results of the REMOVAL Process",  msg , "OK", "");

    }//End Metodo
 




    #endregion My Methods
   
}
