/* 
Directory creation script for quick creation of directories in a new Unity3D or 2D project...
Use :
1. Create a new C# script and save as buildProjectFolders, put in a flder for future use
2. Create a folder in your project called Editor, import or drag script into it
3. Click Edit -> SadGames/Tools/Create Project Folders * as long as there are no build errors, you will see a new menu item near the bootom of the Edit menu
4.  If you want to include a Resources folde, clicking the checkbox will add or remove it
5. If you are using Namespaces, clicking the checkbox will include three basic namespce folders
6.  Right clicking on a list item will let you delete the item, if you want
7.  Increasing the List size will add another item with the prior items name, click in the space to rename.
8.  Clicking "Create" will create all the files listed, the Namespace folders will be added to the script directory.
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR

public class BuildProjectFolders : ScriptableWizard
{
    public bool IncludeDataFolder = false;
    public bool IncludePrefabsFolder = false;
    public bool IncludeScriptableObjectFolder = false;
    public string Manager = "NewManager" ;
    [MenuItem("Greenvillex/Tools/Create Manager")]

    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Create Project Folders", typeof(BuildProjectFolders), "Create");
    }

    //Called when the window first appears
    void OnEnable()
    {
    }
    //Create button click
    void OnWizardCreate()
    {
        string guid = AssetDatabase.CreateFolder("Assets/[Code]", Manager);
        string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
        if (IncludeDataFolder)
        {
            string dataFolderGUID = AssetDatabase.CreateFolder($"{newFolderPath}", "Data");
            string newFolderPathData = AssetDatabase.GUIDToAssetPath(dataFolderGUID);

            string fileName = newFolderPathData + $"/{Manager}.cs";
            var textManager = RegisterScript(Manager);
            File.WriteAllText(fileName, textManager);
        }
        if (IncludePrefabsFolder)
        {
            string dataFolderGUID = AssetDatabase.CreateFolder($"{newFolderPath}", "Prefabs");
            string newFolderPathData = AssetDatabase.GUIDToAssetPath(dataFolderGUID);
        }
        if (IncludeScriptableObjectFolder)
        {
            string dataFolderGUID = AssetDatabase.CreateFolder($"{newFolderPath}", "ScriptableObject");
            string newFolderPathData = AssetDatabase.GUIDToAssetPath(dataFolderGUID);

            string fileName = newFolderPathData + $"/{Manager}.cs";
            var textManager = RegisterScriptableObject(Manager);
            File.WriteAllText(fileName, textManager);
        }

        AssetDatabase.Refresh();
    }

    string RegisterScript(string manager)
    {
        string newScript = 
            "using UnityEngine; " +
            "\npublic class " + manager + " : MonoBehaviour " +
            "\n{" +
            "\n}";
        return newScript;
    }

    string RegisterScriptableObject(string manager)
    {
        string newScript = 
            $"using UnityEngine; " +
            $"\n[CreateAssetMenu(menuName = {'"'}Greenvillex/{manager}SO{'"'})] " +
            $"\npublic class {manager}SO : ScriptableObject" +
            $"\n{'{'} " +
            $"\n{'}'} ";
        return newScript;
    }
}

#endif
