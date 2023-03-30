using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneSorterEditor : EditorWindow
{
    private static List<SceneSorterElement> allSceneLocations;

    [MenuItem("Window/Scene Sorter")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<SceneSorterEditor>(false, "Scene Sorter", true).Show();
    }

    void OnEnable()
    {
        if (SceneSorterGlobals.LogoTexture == null)
        {
            SceneSorterGlobals.LoadMyResources();
        }
        RefreshSceneList();
    }

    void OnDisable()
    {
        SceneSorterSerializer.Save(allSceneLocations);
    }

    #region Loading and refreshing
    private void RefreshSceneList()
    {
        allSceneLocations = SceneSorterSerializer.Load();

        int trimLeftLen = Application.dataPath.Length + 1;
        string[] scenes = Directory.GetFiles(Application.dataPath, "*.unity", SearchOption.AllDirectories);
        foreach (string sceneName in scenes)
        {
            // Remove the data path.
            // Convert backslashes to forward slashes.
            int len = sceneName.Length;
            string name = sceneName.Substring(trimLeftLen, len - trimLeftLen - 6).Replace('\\', '/');

            // If the scene isn't already in our list we should add it.
            if (ContainsSceneName(name) == false)
            {
                SceneSorterElement element = new SceneSorterElement();
                element.favorite = false;
                element.name = name;
                allSceneLocations.Add(element);
            }
        }
    }

    private bool ContainsSceneName(string testName)
    {
        foreach (var element in allSceneLocations)
        {
            if (element.name == testName)
                return true;
        }
        return false;
    }
    #endregion

    #region GUI
    void OnGUI()
    {
        GUIDrawLogo();
        GUIDrawAndTestButtons();
    }

    void GUIDrawLogo()
    {
        if (SceneSorterGlobals.LogoTexture != null)
        {
            Rect logoRect = new Rect(5, 5, SceneSorterGlobals.LogoTexture.width, SceneSorterGlobals.LogoTexture.height);
            GUI.DrawTexture(logoRect, SceneSorterGlobals.LogoTexture);
        }
    }

    void GUIDrawAndTestButtons()
    {
        int loveSceneIndex = -1;
        string loadScene = null;
        string runScene = null;

        if (allSceneLocations != null)
        {
            int total = allSceneLocations.Count;
            for (int i = 0; i < total; i++)
            {
                float xPos = SceneSorterGlobals.LeftMargin;
                float yPos = SceneSorterGlobals.TopMargin;
                yPos += i * (SceneSorterGlobals.ButtonHeight + SceneSorterGlobals.VertPad);

                Rect loveRect = new Rect(xPos, yPos, SceneSorterGlobals.ButtonWidth, SceneSorterGlobals.ButtonHeight);
                xPos += SceneSorterGlobals.ButtonWidth + SceneSorterGlobals.HorizPad;

                Rect loadRect = new Rect(xPos, yPos, SceneSorterGlobals.ButtonWidth, SceneSorterGlobals.ButtonHeight);
                xPos += SceneSorterGlobals.ButtonWidth + SceneSorterGlobals.HorizPad;

                Rect playRect = new Rect(xPos, yPos, SceneSorterGlobals.ButtonWidth, SceneSorterGlobals.ButtonHeight);
                xPos += SceneSorterGlobals.ButtonWidth + SceneSorterGlobals.HorizPad;
                yPos += SceneSorterGlobals.LabelTopMargin;
                Rect nameRect = new Rect(xPos, yPos, SceneSorterGlobals.NameWidth, SceneSorterGlobals.NameHeight);

                if (allSceneLocations[i].favorite)
                {
                    if (GUI.Button(loveRect, SceneSorterGlobals.LoveFilledTexture))
                    {
                        loveSceneIndex = i;
                    }
                }
                else
                {
                    if (GUI.Button(loveRect, SceneSorterGlobals.LoveTexture))
                    {
                        loveSceneIndex = i;
                    }
                }
                if (GUI.Button(loadRect, SceneSorterGlobals.LoadTexture))
                {
                    // Load this scene.
                    loadScene = allSceneLocations[i].name;
                }
                if (GUI.Button(playRect, SceneSorterGlobals.PlayTexture))
                {
                    // Load and play this scene.
                    runScene = allSceneLocations[i].name;
                }
                GUI.Label(nameRect, allSceneLocations[i].name);
            }
        }

        if (loveSceneIndex >= 0)
        {
            // Favorite this index and move it to the top.
            allSceneLocations[loveSceneIndex].favorite = !allSceneLocations[loveSceneIndex].favorite;
            SortSceneLocationsByLove();
            this.Repaint();
        }
        else if (string.IsNullOrEmpty(loadScene) == false)
        {
            LoadThisScene(loadScene);
        }
        else if (string.IsNullOrEmpty(runScene) == false)
        {
            RunThisScene(runScene);
        }
    }

    private void SortSceneLocationsByLove()
    {
        // Sort "favorite" tags to the top.
        int total = allSceneLocations.Count;
        if (total == 0)
            return;

        List<SceneSorterElement> newList = new List<SceneSorterElement>(total);
        // Add the faves first.
        for(int i = 0; i < total; i++ )
        {
            if (allSceneLocations[i].favorite == true)
                newList.Add(allSceneLocations[i]);
        }
        // Add everything else.
        for (int i = 0; i < total; i++)
        {
            if (allSceneLocations[i].favorite == false)
                newList.Add(allSceneLocations[i]);
        }

        allSceneLocations = newList;
    }
    #endregion

    #region Scene Actions
    void LoadThisScene(string sceneName)
    {
        if (EditorApplication.isPlaying == false && EditorApplication.isPaused == false)
        {
            EditorSceneManager.OpenScene("Assets/" + sceneName + ".unity");
        }
        else
        {
            EditorApplication.isPaused = false;
            EditorApplication.isPlaying = false;
        }
    }

    void RunThisScene(string sceneName)
    {
        if (EditorApplication.isPlaying == false && EditorApplication.isPaused == false)
        {
            EditorSceneManager.OpenScene("Assets/" + sceneName + ".unity");

            // Start play mode.
            EditorApplication.isPlaying = true;
        }
        else
        {
            EditorApplication.isPaused = false;
            EditorApplication.isPlaying = false;
        }
    }
    #endregion

}

