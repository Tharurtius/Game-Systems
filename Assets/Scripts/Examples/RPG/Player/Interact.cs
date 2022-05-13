using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("RPG Game/Character/Interact")]
public class Interact : MonoBehaviour
{
    #region RayCasting Info
    //Ray - A ray is an infinite line starting at origin and going in some direction.
    //Raycasting - Casts a ray, from point origin, in direction of length maxDistance, against all colliders in the Scene.
    //Raycasthit - Structure used to get information back from a raycast
    #endregion

    // Update is called once per frame
    void Update()
    {
        //if our interact key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            //create ray - //this is our line../Our Ray/line doesnt a direction
            Ray interact;
            //this ray is shooting out from the main cameras screen point center of screen
            //assigning origin
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            //create hit info
            RaycastHit hitInfo;
            //if this physics raycast hits something within 10 units
            if (Physics.Raycast(interact, out hitInfo, 10))
            {
                #region NPC tag
                //and that hits into is tagged NPC
                if (hitInfo.collider.tag == "NPC")
                {
                    Debug.Log("NPC");
                    if (hitInfo.collider.GetComponent<Dialogue>())
                    {
                        hitInfo.collider.GetComponent<Dialogue>().showDlg = true;
                        GameManager.gamePlayStates = GamePlayStates.MenuPause;
                    }
                }
                #endregion
                #region Item
                //and that hits into is tagged
                if (hitInfo.collider.CompareTag("Item"))
                {
                    if (hitInfo.collider.GetComponent<ItemHandler>())
                    {
                        hitInfo.collider.GetComponent<ItemHandler>().OnCollection();
                    }
                }
                #endregion
                #region Chest
                //and that hits into is tagged
                if (hitInfo.collider.tag == "Chest")
                {
                    Debug.Log("Chest");
                }
                #endregion
            }
        }
    }
}

