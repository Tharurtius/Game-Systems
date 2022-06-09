using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save : MonoBehaviour
{
    public string path = Path.Combine(Application.streamingAssetsPath, "Save/Save.txt");
    public string quickPath = Path.Combine(Application.streamingAssetsPath, "Save/QuickSave.txt");

    public void SaveGame()
    {
        //links writer to file
        StreamWriter writer = new StreamWriter(path, false);//true = add, false = overwrite
        writer.WriteLine("This file has something in it");
        GameObject player = GameObject.Find("Player");
        //write position
        writer.WriteLine(player.transform.position.x);
        writer.WriteLine(player.transform.position.y);
        writer.WriteLine(player.transform.position.z);
        //write rotation
        writer.WriteLine(player.transform.forward.x);
        writer.WriteLine(player.transform.forward.y);
        writer.WriteLine(player.transform.forward.z);
        //writing is done
        writer.Close();
    }
    public void Load()
    {
        //Read text from file
        StreamReader reader = new StreamReader(path);
        //reference to the line we are reading
        //first line confirms file has something in it, does not contain actual data
        if(reader.ReadLine() == null)
        {
            Debug.Log("Error! No save file detected!");
            return;
        }
        GameObject player = GameObject.Find("Player");
        Vector3 container = new Vector3();
        //read file for position
        container.x = float.Parse(reader.ReadLine());
        container.y = float.Parse(reader.ReadLine());
        container.z = float.Parse(reader.ReadLine());
        player.transform.position = container;

        //read file for rotation
        container.x = float.Parse(reader.ReadLine());
        container.y = float.Parse(reader.ReadLine());
        container.z = float.Parse(reader.ReadLine());
        player.transform.forward = container;

        //reading is done
        reader.Close();
    }

    public void QuickSave()
    {
        //links writer to file
        StreamWriter writer = new StreamWriter(quickPath, false);//true = add, false = overwrite
        writer.WriteLine("This file has something in it");
        GameObject player = GameObject.Find("Player");
        //write position
        writer.WriteLine(player.transform.position.x);
        writer.WriteLine(player.transform.position.y);
        writer.WriteLine(player.transform.position.z);
        //write rotation
        writer.WriteLine(player.transform.forward.x);
        writer.WriteLine(player.transform.forward.y);
        writer.WriteLine(player.transform.forward.z);
        //writing is done
        writer.Close();
    }
    public void QuickLoad()
    {
        //Read text from file
        StreamReader reader = new StreamReader(quickPath);
        //reference to the line we are reading
        //first line confirms file has something in it, does not contain actual data
        if (reader.ReadLine() == null)
        {
            Debug.Log("Error! No save file detected!");
            return;
        }
        GameObject player = GameObject.Find("Player");
        Vector3 container = new Vector3();
        //read file for position
        container.x = float.Parse(reader.ReadLine());
        container.y = float.Parse(reader.ReadLine());
        container.z = float.Parse(reader.ReadLine());
        player.transform.position = container;

        //read file for rotation
        container.x = float.Parse(reader.ReadLine());
        container.y = float.Parse(reader.ReadLine());
        container.z = float.Parse(reader.ReadLine());
        player.transform.forward = container;

        //reading is done
        reader.Close();
    }
}
