using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//you will need to change Scenes
using UnityEngine.SceneManagement;
public class CustomisationSet : MonoBehaviour
{

    #region Variables
    [Header("Texture List")]
    //Texture2D List for skin,hair, mouth, eyes, clothes and armour
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    [Header("Index")]
    //index numbers for our current skin, hair, mouth, eyes textures
    public int skinIndex;
    public int hairIndex, mouthIndex, eyesIndex, clothesIndex, armourIndex;
    [Header("Renderer")]
    //renderer for our character mesh so we can reference a material list
    public Renderer charRend;
    [Header("Max Index")]
    //max amount of skin, hair, mouth, eyes, clothes and armour textures that our lists are filling with
    public int skinMax;
    public int hairMax, mouthMax, eyesMax, clothesMax, armourMax;
    [Header("Character Name")]
    //name of our character that the user is making
    public string characterName = "Adventurer";

    public Vector2 scr;
    #endregion

    #region Start
    //in start we need to set up the following
    private void Start()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;

        #region for loop to pull textures from file
        //for loop looping from 0 to less than the max amount of textures we need
        for (int i = 0; i < skinMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Skin_" + i) as Texture2D;
            //add our temp texture that we just found to the List
            skin.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of textures we need
        for (int i = 0; i < hairMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Hair_" + i) as Texture2D;
            //add our temp texture that we just found to the List
            hair.Add(temp);
        }//for loop looping from 0 to less than the max amount of textures we need
        for (int i = 0; i < mouthMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            //add our temp texture that we just found to the List
            mouth.Add(temp);
        }//for loop looping from 0 to less than the max amount of textures we need
        for (int i = 0; i < eyesMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            //add our temp texture that we just found to the List
            eyes.Add(temp);
        }//for loop looping from 0 to less than the max amount of textures we need
        for (int i = 0; i < clothesMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Clothes_" + i) as Texture2D;
            //add our temp texture that we just found to the List
            clothes.Add(temp);
        }//for loop looping from 0 to less than the max amount of textures we need
        for (int i = 0; i < armourMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Armour_" + i) as Texture2D;
            //add our temp texture that we just found to the List
            armour.Add(temp);
        }

        #endregion
        //connect and find the SkinnedMeshRenderer thats in the scene to the variable we made for Renderer
        charRend = GameObject.Find("Mesh").GetComponent<Renderer>();
        #region do this after making the function SetTexture
        //SetTexture skin, hair, mouth, eyes to the first texture 0
        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Clothes", 0);
        SetTexture("Armour", 0);
        #endregion
    }
    #endregion

    #region SetTexture
    //Create a function that is called SetTexture it should contain a string and int
    //the string is the name of the material we are editing, the int is the direction we are changing
    void SetTexture(string type, int dir)
    {
        //we need variables that exist only within this function
        //these are ints index numbers, max numbers, material index and Texture2D array of textures
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        //inside a switch statement that is swapped by the string name of our material
        switch (type)
        {
            #region Switch Material
            //case type
            case "Skin":
                //index is the same as our type index
                index = skinIndex;
                //max is the same as our type max
                max = skinMax;
                //textures is our type list .ToArray()
                textures = skin.ToArray();
                //material index element number
                matIndex = 1;
                //break
                break;
            //case type
            case "Hair":
                //index is the same as our type index
                index = hairIndex;
                //max is the same as our type max
                max = hairMax;
                //textures is our type list .ToArray()
                textures = hair.ToArray();
                //material index element number
                matIndex = 4;
                //break
                break;
            //case type
            case "Mouth":
                //index is the same as our type index
                index = mouthIndex;
                //max is the same as our type max
                max = mouthMax;
                //textures is our type list .ToArray()
                textures = mouth.ToArray();
                //material index element number
                matIndex = 3;
                //break
                break;
            //case type
            case "Eyes":
                //index is the same as our type index
                index = eyesIndex;
                //max is the same as our type max
                max = eyesMax;
                //textures is our type list .ToArray()
                textures = eyes.ToArray();
                //material index element number
                matIndex = 2;
                //break
                break;
            //case type
            case "Clothes":
                //index is the same as our type index
                index = clothesIndex;
                //max is the same as our type max
                max = clothesMax;
                //textures is our type list .ToArray()
                textures = clothes.ToArray();
                //material index element number
                matIndex = 5;
                //break
                break;
            //case type
            case "Armour":
                //index is the same as our type index
                index = armourIndex;
                //max is the same as our type max
                max = armourMax;
                //textures is our type list .ToArray()
                textures = armour.ToArray();
                //material index element number
                matIndex = 6;
                //break
                break;
            #endregion
            default:
                Debug.Log("Error! not valid material type");
                break;
        }
        #region OutSide Switch
        //outside our switch statement
        //index plus equals our direction
        index += dir;
        //cap our index to loop back around if is is below 0 or above max take one
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max -1)
        {
            index = 0;
        }
        //Material array is equal to our characters material list
        Material[] materials = charRend.materials;
        //our material arrays current material index's main texture is equal to our texture arrays current index
        materials[matIndex].mainTexture = textures[index];
        //our characters materials are equal to the material array
        charRend.materials = materials;
        //create another switch that is goverened by the same string name of our material
        #endregion
        #region Set Material Switch
        switch (type)
        {
            //case type
            case "Skin":
                //type index equals our index
                skinIndex = index;
                //break
                break;
            //case type
            case "Hair":
                //type index equals our index
                hairIndex = index;
                //break
                break;
            //case type
            case "Mouth":
                //type index equals our index
                mouthIndex = index;
                //break
                break;
            //case type
            case "Eyes":
                //type index equals our index
                eyesIndex = index;
                //break
                break;
            //case type
            case "Clothes":
                //type index equals our index
                clothesIndex = index;
                //break
                break;
            //case type
            case "Armour":
                //type index equals our index
                armourIndex = index;
                //break
                break;
            default:
                Debug.Log("Error! in second switch");
                break;
        }
        #endregion
    }
    #endregion

    #region Save
    //Function called Save this will allow us to save our indexes to PlayerPrefs
    //SetInt for SkinIndex, HairIndex, MouthIndex, EyesIndex
    //SetString CharacterName
    #endregion

    #region OnGUI
    private void OnGUI()
    {
        //Function for our GUI elements
        //create the floats scrW and scrH that govern our 16:9 ratio
        //create an int that will help with shuffling your GUI elements under eachother
        int i = 0;
        #region Skin
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scr.x, scr.y + i * (0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Skin", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scr.x, scr.y + i * (0.5f * scr.y), scr.x, 0.5f * scr.y), "Skin");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scr.x, scr.y + i * (0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Skin", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        //set up same things for Hair, Mouth and Eyes
        #region Hair
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scr.x, scr.y + i * (0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Hair", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scr.x, scr.y + i * (0.5f * scr.y), scr.x, 0.5f * scr.y), "Hair");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scr.x, scr.y + i * (0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Hair", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Mouth
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scr.x, scr.y + i * (0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Mouth", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scr.x, scr.y + i * (0.5f * scr.y), scr.x, 0.5f * scr.y), "Mouth");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scr.x, scr.y + i * (0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Mouth", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Eyes
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scr.x, scr.y + i * (0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Eyes", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scr.x, scr.y + i * (0.5f * scr.y), scr.x, 0.5f * scr.y), "Eyes");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scr.x, scr.y + i * (0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Eyes", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Clothes
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scr.x, scr.y + i * (0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Clothes", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scr.x, scr.y + i * (0.5f * scr.y), scr.x, 0.5f * scr.y), "Clothes");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scr.x, scr.y + i * (0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Clothes", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Armour
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scr.x, scr.y + i * (0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Armour", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scr.x, scr.y + i * (0.5f * scr.y), scr.x, 0.5f * scr.y), "Armour");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scr.x, scr.y + i * (0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Armour", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Random Reset
        //create 2 buttons one Random and one Reset
        //Random will feed a random amount to the direction 
        if (GUI.Button(new Rect(0.25f * scr.x, scr.y + i * (0.5f * scr.y), scr.x, 0.5f * scr.y), "Random"))
        {
            SetTexture("Skin", Random.Range(-skinMax, skinMax));
            SetTexture("Hair", Random.Range(-hairMax, hairMax));
            SetTexture("Mouth", Random.Range(-mouthMax, mouthMax));
            SetTexture("Eyes", Random.Range(-eyesMax, eyesMax));
            SetTexture("Clothes", Random.Range(-clothesMax, clothesMax));
            SetTexture("Armour", Random.Range(-armourMax, armourMax));
        }
            //reset will set all to 0 both use SetTexture
            if (GUI.Button(new Rect(1.25f * scr.x, scr.y + i * (0.5f * scr.y), scr.x, 0.5f * scr.y), "Reset"))
        {
            SetTexture("Skin", skinMax);
            SetTexture("Hair", hairMax);
            SetTexture("Mouth", mouthMax);
            SetTexture("Eyes", eyesMax);
            SetTexture("Clothes", clothesMax);
            SetTexture("Armour", armourMax);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Character Name and Save & Play
        //name of our character equals a GUI TextField that holds our character name and limit of characters
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        characterName = GUI.TextField(new Rect(0.25f * scr.x, scr.y + i * (0.5f * scr.y), 2 * scr.x, 0.5f * scr.y), characterName, 12);
        i++;
        //GUI Button called Save and Play
        //this button will run the save function and also load into the game level
        #endregion
    }

    #endregion
}
