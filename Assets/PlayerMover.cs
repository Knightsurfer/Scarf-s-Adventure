using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;

namespace Player
{
    public class PlayerMover : Variables
    {

        // Start is called before the first frame update
        void Start()
        {
            SetMainVariables();
        }

        // Update is called once per frame
        void Update()
        {
            CameraAdjust();
        }


        void CameraRotator()
        {
            if (game)
            {
                horizontal = game.cameraX * cam_rotateSpeed_X * Time.deltaTime;
                rotator = currentYaw + 150;
                if (canMove)
                {
                    currentYaw += game.cameraX * cam_rotateSpeed_X * Time.deltaTime;
                }
                switch (viewType)
                {

                    case "ThirdPerson":
                        if (Input.GetKeyDown(game.button_Select))
                        {
                            Camera.main.transform.parent = lookObject.transform;
                            viewType = "FirstPerson";
                        }
                        ThirdPerson();
                        break;
                }
            }
        }

        private void ThirdPerson()
        {
            currentZoom -= game.cameraY * 3.5f * Time.deltaTime;
            currentZoom = Mathf.Clamp(currentZoom, 2f, 4f);

            Camera.main.transform.position = player.transform.position - new Vector3(0, -1, -2) * currentZoom;
            Camera.main.transform.LookAt(player.transform.position + Vector3.up * 2f);
            Camera.main.transform.RotateAround(player.transform.position, Vector3.up, currentYaw);
        }



        void LateUpdate()
        {
            ThirdPerson();
           
            CameraRotator();
        }
    }








    public class Variables : MonoBehaviour
    {
       protected GameManager game;

        protected float currentZoom = 2f;
        protected float currentYaw = 120;
        protected GameObject player;
        protected string viewType = "ThirdPerson";
        protected Transform lookObject;
        protected float rotator = 150;
        protected float cam_rotateSpeed_X = 180;
        protected bool canMove = true;
        protected float horizontal;
        protected Vector3 offset;
        protected Transform skeleton;
        public _Character playerInfo;
        protected Animator anim;
        protected float elapsed;
        protected int timerSpeed;
        protected Vector3 moveDirection;

        /// <summary>
        /// The handler for the actual angle the player rotates at.
        /// </summary>
        protected Quaternion theRotation;

        /// <summary>
        /// How fast the player is allowed to return.
        /// </summary>
        protected float player_rotateSpeed = 10;
        protected void MoveCharacter()
        {


            if (canMove)
            {
                if (game.moveX != 0 || game.moveY != 0)
                {
                    player.transform.rotation = Quaternion.Euler(0, rotator + 30, 0);
                    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 90, moveDirection.z));
                    theRotation = newRotation;
                    skeleton.rotation = Quaternion.Slerp(skeleton.rotation, newRotation, player_rotateSpeed * Time.deltaTime);

                }
            }
        }


        protected void SetMainVariables()
        {
            game = FindObjectOfType<GameManager>();
            player = gameObject;
            
            AssignPlayer();
            CameraSetup();
          

        }
        protected void AssignPlayer()
        {
            if (!GetComponentInChildren<SkinnedMeshRenderer>())
            {
                GameObject playerPrefab;
                playerPrefab = Instantiate(playerInfo.prefab, this.transform);
                anim = playerPrefab.GetComponent<Animator>();

                Destroy(GetComponent<MeshFilter>());
                Destroy(GetComponent<MeshRenderer>());
            }
            else
            {
                anim = GetComponentInChildren<Animator>();
            }
        }
        protected void CameraSetup()
        {
            foreach (Transform look in GetComponentsInChildren<Transform>())
            {
                if (look.name == "Look Object")
                {
                    lookObject = look;
                }
            }
            skeleton = GetComponentInChildren<SkinnedMeshRenderer>().transform;
            offset = transform.position - Camera.main.transform.position;
        }

        protected void CameraAdjust()
        {
            if (!canMove)
            {
                if (currentYaw != 90)
                {
                    currentYaw -= 1 * 5;
                }
                else
                {
                    elapsed += Time.deltaTime;
                    if (!canMove)
                    {
                        if (elapsed >= timerSpeed)
                        {
                            GameObject.Find("KH UI").GetComponent<Canvas>().enabled = true;
                            FindObjectOfType<MenuChooser>().enabled = true;
                            FindObjectOfType<MenuChooser>().enabled = true;
                            canMove = true;
                        }
                    }
                }
            }
        }
    }
}