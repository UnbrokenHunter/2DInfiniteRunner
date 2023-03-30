using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class SceneSorterGlobals
{
    static public Texture2D LogoTexture { get; set; }
    static public Texture2D LoadTexture { get; set; }
    static public Texture2D PlayTexture { get; set; }

    static public Texture2D LoveTexture { get; set; }
    static public Texture2D LoveFilledTexture { get; set; }

    static public Texture2D UpTexture { get; set; }
    static public Texture2D DownTexture { get; set; }

    static readonly public float ButtonWidth = 32;
    static readonly public float ButtonHeight = 32;

    static readonly public float LeftMargin = 20.0f;
    static readonly public float TopMargin = 45;

    static readonly public float HorizPad = 5.0f;
    static readonly public float VertPad = 5.0f;

    static readonly public float LabelTopMargin = 8.0f;    

    static readonly public float NameWidth = 600.0f;
    static readonly public float NameHeight = 32.0f;
    
    public static void LoadMyResources()
    {
        // Load the textures we need.
        LogoTexture = Resources.Load("SSGLogo") as Texture2D;
        LoadTexture = Resources.Load("SSGLoad") as Texture2D;
        PlayTexture = Resources.Load("SSGPlay") as Texture2D;

        LoveTexture = Resources.Load("SSGLove") as Texture2D;
        LoveFilledTexture = Resources.Load("SSGLoveFilled") as Texture2D;

        UpTexture = Resources.Load("SSGUp") as Texture2D;
        DownTexture = Resources.Load("SSGDown") as Texture2D;

        Assert.IsNotNull(LogoTexture, "Logo texture is missing, try reinstalling unity package");
        Assert.IsNotNull(LoadTexture, "Load button texture is missing, try reinstalling unity package");
        Assert.IsNotNull(PlayTexture, "Play button texture is missing, try reinstalling unity package");

        Assert.IsNotNull(LoveTexture, "Love button texture is missing, try reinstalling unity package");
        Assert.IsNotNull(LoveFilledTexture, "LoveFilled button texture is missing, try reinstalling unity package");

        Assert.IsNotNull(UpTexture, "Up button texture is missing, try reinstalling unity package");
        Assert.IsNotNull(DownTexture, "Down button texture is missing, try reinstalling unity package");
    }
}

