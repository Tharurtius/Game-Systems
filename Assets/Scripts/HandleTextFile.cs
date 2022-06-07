using UnityEngine; //Resources.Load
using UnityEditor; //to make our own tool, update our editor
using System; //convert a string to an enum
using System.IO; //have access to characters from a byte stream
/*
 * bit ==  is a singular 0 or 1
 * byte == 8 bits
 * so a byte is a group of bits
 * a stream is known as a sequence
 * a byte stream is a succession of a group of bits
 */
public class HandleTextFile
{
    //at this file location
    //static string path = "Assets/Resources/Save/KeyBinds.txt";
    static string path = Path.Combine(Application.streamingAssetsPath, "Keybinds.txt");
    //Unity Editor allows me to create a tool in my Menus
    [MenuItem("Tool/Save/Write File")]
    //This is public static behavious that we can call in our scripts
    public static void WriteSaveFile()
    {
        //true = add to file
        //false = override file
        StreamWriter writer = new StreamWriter(path, false);
        //write each of our keys in the file
        foreach (var key in KeyBinds.keys)
        {
            //each key name and key value will be written in with a : to seperate them
            writer.WriteLine(key.Key + ":" + key.Value.ToString());
        }
        //writing is done
        writer.Close();
        AssetDatabase.ImportAsset(path);
        TextAsset asset = Resources.Load(path) as TextAsset;
    }
    [MenuItem("Tool/Save/Read File")]
    public static void ReadSaveFile()
    {
        //Read text from file
        StreamReader reader = new StreamReader(path);
        //ref to the line we are reading
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(':');
            //if we have keys and are just updating
            if (KeyBinds.keys.Count > 0)
            {
                KeyBinds.keys[parts[0]] = (KeyCode)System.Enum.Parse(typeof(KeyCode), parts[1]);
            }
            //else we need to also make the keys when we load
            else //add key
            {
                KeyBinds.keys.Add(parts[0], (KeyCode)System.Enum.Parse(typeof(KeyCode), parts[1]));
            }
        }
        reader.Close();
    }
}
