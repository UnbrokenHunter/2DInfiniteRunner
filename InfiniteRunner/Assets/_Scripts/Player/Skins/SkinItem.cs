using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinItem : MonoBehaviour
{
    public Skin skin;
    
    public void SelectSkin()
    {
        var has = SkinManager.instance.SelectSkin(skin);

    }
}
