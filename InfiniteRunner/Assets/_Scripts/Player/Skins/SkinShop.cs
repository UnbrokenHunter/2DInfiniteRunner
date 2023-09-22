using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinShop : MonoBehaviour
{

    [SerializeField] private GameObject _shopTemplate;
    [SerializeField] private List<SkinItem> _items;


    private void Start()
    {
        
        foreach (var skin in SkinManager.instance.GetSkins())
        {
            var item = Instantiate(_shopTemplate, transform);
            _items.Add(item.GetComponent<SkinItem>());

            item.name = skin.name;

            var texts = item.GetComponentsInChildren<TMP_Text>();
            texts[0].text = skin.name;
            texts[1].text = skin.cost.ToString();
            item.GetComponentsInChildren<Image>()[1].sprite = skin.image;

            item.GetComponentInChildren<SkinItem>().skin = skin;
        }
    }
    
    private readonly Color _blue = new Color(112f/255f, 235f/255f, 254f/255f);
    private readonly Color _green = new Color(186f/255f, 220f/255f, 88f/255f);
    private readonly Color _red = new Color(255f/255f, 121f/255f, 121f/255f);
    
    private void Update()
    {
        foreach (var item in _items)
        {
            var itemTemp = item.GetComponentsInChildren<Image>()[0];
            
            if (SkinManager.instance.SelectedSkin() == (item.skin))
                itemTemp.color = _blue;

            else if (item.skin.purchased)
                itemTemp.color = _green;

            else
                itemTemp.color = _red;
        }
    }
}
