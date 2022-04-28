using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script can be found in the Component section under the option Intro PRG/Character Movement
[AddComponentMenu("RPG Game/Character/Movement")]
//This script requires the component Character controller to be attached to the Game Object
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    #region Extra Study
    //Input Manager(https://docs.unity3d.com/Manual/class-InputManager.html)
    //Input(https://docs.unity3d.com/ScriptReference/Input.html)
    //CharacterController allows you to move the character kinda like Rigidbody (https://docs.unity3d.com/ScriptReference/)
    #endregion

    #region Variables
    [Header("Character")]
    [Tooltip("use this to apply movement in worldspace")]
    public Vector3 moveDir; //we will use this to apply movement in worldspace
    public CharacterController charC; //this is out reference variable to the character controller
    [Header("Speeds")]//headers create a header for the variable directly below
    public float moveSpeed;
    public float walkSpeed = 5f, crouchSpeed = 2.5f, runSpeed = 10f;
    public float jumpSpeed = 8f, gravity = 20f;
    public Vector2 input;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //assign a component to our reference
        charC = GetComponent<CharacterController>();
#if UNITY_EDITOR
        HandleTextFile.ReadSaveFile();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gamePlayStates == GamePlayStates.Game)
        {
            #region Old stuff
            ////if our character is grounded
            //if (charC.isGrounded)
            //{
            //    //set moveDir to the inputs direction
            //    moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //    //moveDir's forward is changed from global z (forward) to the Game Objects local Z (forward)
            //    //allows us to move where player is facing
            //    moveDir = transform.TransformDirection(moveDir);
            //    //moveDir is multiplied by speed so we move at a decent pace
            //    moveDir *= moveSpeed;
            //    //if the input button for jump is pressed then
            //    if (Input.GetButton("Jump"))
            //    {
            //        //our moveDir.y is equal to our jump speed
            //        moveDir.y = jumpSpeed;
            //    }
            //}
            #endregion
            #region Keybinds
            if (charC.isGrounded)
            {
                /*Using ? which is known as a ternary conditional operator
                 * ? allows us to evaluate a boolean expression and return results based off which expression is met.
                 * you can also return a catch all default value if neither are met
                 * */

                /*if (Input.GetKey(KeyBinds.keys["Forward"]))
                 * {
                 *     input.y = 1;
                 * }
                 * else if (Input.GetKey(KeyBinds.keys["Backward"))
                 * {
                 *     input.y = -1;
                 * }
                 * else 
                 * {
                 *     input.y = 0;
                 * }
                 * The Above text means the same as the below text
                 */
                //the input.y equals 1 if Foward is pressed, equals -1 if backward is pressed and 0 if neither is pressed
                //Will be in moveDir z axis
                input.y = Input.GetKey(KeyBinds.keys["Forward"]) ? 1 : Input.GetKey(KeyBinds.keys["Backward"]) ? -1 : 0;
                //Will be in moveDir x axis
                input.x = Input.GetKey(KeyBinds.keys["Left"]) ? -1 : Input.GetKey(KeyBinds.keys["Right"]) ? 1 : 0;
                //Speed
                moveSpeed = Input.GetKey(KeyBinds.keys["Sprint"]) ? runSpeed : Input.GetKey(KeyBinds.keys["Crouch"]) ? crouchSpeed : walkSpeed;
                //Moving according to our inputs and forward direction
                moveDir = transform.TransformDirection(new Vector3(input.x, moveDir.y, input.y));
                //movement is affected by our speed
                moveDir.x *= moveSpeed;
                moveDir.z *= moveSpeed;
                //jump
                moveDir.y = Input.GetKey(KeyBinds.keys["Jump"]) ? jumpSpeed : moveDir.y;
            }
            #endregion
            //regardless of if we are grounded or not the players moveDir.y is always affected by gravity multiplied by time.deltaTime to normalize it
            moveDir.y -= gravity * Time.deltaTime;
            //we then tell the character Controller that if is moving in a direction multiplied Time.deltaTime
            charC.Move(moveDir * Time.deltaTime);
        }
    }
}
