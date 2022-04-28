using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomisationGet : MonoBehaviour
{
    public Renderer characterRenderer;
    [Header("Index")]
    public int skinIndex;
    public int hairIndex, mouthIndex, eyesIndex, clothesIndex, armourIndex;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        characterRenderer = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        //load from save file
        Load();
    }

    void Load()
    {
        SetTexture("Skin", skinIndex);
        SetTexture("Hair", hairIndex);
        SetTexture("Mouth", mouthIndex);
        SetTexture("Eyes", eyesIndex);
        SetTexture("Clothes", clothesIndex);
        SetTexture("Armour", armourIndex);
    }
    void SetTexture(string type, int index)
    {
        Texture2D tex = null;
        int matIndex = 0;
        switch (type)
        {
            case "Skin":
                tex = Resources.Load("Character/Skin_" + skinIndex) as Texture2D;
                matIndex = 1;
                break;
            case "Eyes":
                tex = Resources.Load("Character/Eyes_" + eyesIndex) as Texture2D;
                matIndex = 2;
                break;
            case "Mouth":
                tex = Resources.Load("Character/Mouth_" + mouthIndex) as Texture2D;
                matIndex = 3;
                break;
            case "Hair":
                tex = Resources.Load("Character/Hair_" + hairIndex) as Texture2D;
                matIndex = 4;
                break;
            case "Clothes":
                tex = Resources.Load("Character/Clothes_" + clothesIndex) as Texture2D;
                matIndex = 5;
                break;
            case "Armour":
                tex = Resources.Load("Character/Armour_" + armourIndex) as Texture2D;
                matIndex = 6;
                break;
            default:
                Debug.Log("Error!");
                break;
        }
        Material[] mats = characterRenderer.materials;
        mats[matIndex].mainTexture = tex;
        characterRenderer.materials = mats;
    }
}
