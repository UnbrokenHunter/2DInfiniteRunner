using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{

    public static SkinManager instance;
    
    [SerializeField] private Skin[] skins;
    public Skin[] GetSkins() { return skins; }

    [SerializeField] private Skin selectedSkin;
    public Skin SelectedSkin() { return selectedSkin; }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool SelectSkin(Skin skin)
    {
        if (skin.purchased)
        {
            selectedSkin = skin;
            return true;
        }
        else
        {
            return PurchaseSkin(skin);
        }
    }

    private bool PurchaseSkin(Skin skin)
    {
        if (HighScore.Instance.CheckCoinCount() < skin.cost) return false;
        
        HighScore.Instance.UseCoin(skin.cost);
        skin.purchased = true;
        return true;
    }

}