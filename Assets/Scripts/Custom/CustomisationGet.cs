using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CustomisationGet : MonoBehaviour
{
    public Renderer characterRenderer;
    [Header("Index")]
    public int skinIndex;
    public int hairIndex, mouthIndex, eyesIndex, clothesIndex, armourIndex;
    public GameObject player;
    public string playerName;
    public string path = Path.Combine(Application.streamingAssetsPath, "Customization.txt");
    // Start is called before the first frame update
    void Start()
    {
        characterRenderer = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Warrior");
        GetCustom();
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
    public void GetCustom()
    {
        //Read text from file
        StreamReader reader = new StreamReader(path);
        //reference to the line we are reading
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(':');
            switch (parts[0])
            {
                case "Skin":
                    skinIndex = int.Parse(parts[1]);
                    break;
                case "Eyes":
                    eyesIndex = int.Parse(parts[1]);
                    break;
                case "Mouth":
                    mouthIndex = int.Parse(parts[1]);
                    break;
                case "Hair":
                    hairIndex = int.Parse(parts[1]);
                    break;
                case "Clothes":
                    clothesIndex = int.Parse(parts[1]);
                    break;
                case "Armour":
                    armourIndex = int.Parse(parts[1]);
                    break;
                case "Name":
                    playerName = parts[1];
                    break;
                default:
                    Debug.Log("Error!");
                    break;

            }

        }
        reader.Close();
    }
}
