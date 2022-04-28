using System.Collections.Generic;//allow us to access and use Dictionaries
using UnityEngine;//connects to unity
using UnityEngine.UI;//allows us to access and use canvas UI elements

public class KeyBinds : MonoBehaviour
{
    [SerializeField]
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    [System.Serializable]
    public struct KeyUISetup
    {
        public string keyName;
        public Text keyDisplayText;
        public string defaultKey;
    }
    public KeyUISetup[] baseSetup;
    public GameObject currentKey;
    public Color32 changedKey = new Color32(39, 171, 249, 255);
    public Color32 selectedKey = new Color32(239, 116, 36, 255);

    void Start()
    {
        //forloop to add the keys to the dictionary on start, with the save or default data depending if we have save data
        if (PlayerPrefs.HasKey("FirstLoad"))
        {
            for (int i = 0; i < baseSetup.Length; i++)
            {
                //add key according to the saved string or default value
                keys.Add(baseSetup[i].keyName, (KeyCode)System.Enum.Parse(typeof(KeyCode), baseSetup[i].defaultKey));
                //for all the UI text elements change the display to what the bind is in our dictionary
                baseSetup[i].keyDisplayText.text = keys[baseSetup[i].keyName].ToString();
            }
            HandleTextFile.WriteSaveFile();
            PlayerPrefs.SetString("FirstLoad", "");
        }
        else
        {
            HandleTextFile.ReadSaveFile();
        }
        for (int i = 0; i < baseSetup.Length; i++)
        {
            baseSetup[i].keyDisplayText.text = keys[baseSetup[i].keyName].ToString();
        }
        
    }
    public void SaveKeys()
    {
        //for each entry in our dictionary
        //foreach (var key in keys)
        //{
        //playerprefs is our inbuilt way to save and load values from our registry editor
        //PlayerPrefs.SetString(key.Key, key.Value.ToString());
        //}
        //PlayerPrefs.Save();
        HandleTextFile.WriteSaveFile();
    }
    public void ChangeKey(GameObject clickedKey)
    {
        currentKey = clickedKey;
        //if we have a key selected
        if (clickedKey != null)
        {
            //change the colour of the key to the selected key colour
            clickedKey.GetComponent<Image>().color = selectedKey;
        }
    }
    private void OnGUI()//allow us to run events...such as key press
    {
        //temp reference to the string value of our keycode
        string newKey = "";
        //temp reference to the current event
        Event e = Event.current;
        //if we have a key selected
        if (currentKey != null)
        {
            //if the event is a key press
            if (e.isKey)
            {
                //our temp key reference is the event key that was pressed
                newKey = e.keyCode.ToString();
            }
            //there is an issue with unity in getting the left and right shift keys
            //the following part fixes this issue
            if (Input.GetKey(KeyCode.LeftShift))
            {
                newKey = "LeftShift";
            }
            if (Input.GetKey(KeyCode.RightShift))
            {
                newKey = "RightShift";
            }
            if (e.isMouse)
            {
                newKey = e.button.ToString();
            }
            //if (e.isScrollWheel)
            //{

            //}

            //if we have set a key
            if (newKey != "")
            {
                //container for old key
                string oldKey = currentKey.GetComponentInChildren<Text>().text;
                //this should stop you from changing key to already set key
                if (!keys.ContainsValue((KeyCode)System.Enum.Parse(typeof(KeyCode), newKey)))//if we dont already have key
                {
                    //change the key value in the dictionary
                    keys[currentKey.name] = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKey);
                    //change the display text to match the changed key
                    currentKey.GetComponentInChildren<Text>().text = newKey;
                    //change the colour of our button to the changed colour
                    currentKey.GetComponent<Image>().color = changedKey;
                    //forget the objexty we were editing
                    currentKey = null;
                }
                else//change key to older key
                {
                    //change the key value in the dictionary to old value
                    keys[currentKey.name] = (KeyCode)System.Enum.Parse(typeof(KeyCode), oldKey);
                    //change the display text to match the changed key
                    currentKey.GetComponentInChildren<Text>().text = oldKey;
                    //change the colour of our button to the changed colour
                    currentKey.GetComponent<Image>().color = changedKey;
                    //forget the objexty we were editing
                    currentKey = null;
                }
            }

        }
    }
}
