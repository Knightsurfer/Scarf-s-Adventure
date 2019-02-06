using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;


//###################################//
//                                                                              //
//            INTERACTABLES                                         //
//                                                                            //
//                                                                           //
//#################################//
//                                                                          //
//    Any item with this script attached                     //
//    can be interacted with in some way.                //
//                                                                      //
//                                                                     //
///////////////////////////////////////////////////////////





public class Interactable : Interactive.I_Door
{
   
   // float timerspeed = 2f;

    private void Start()
    {
        game = FindObjectOfType<GameManager>();
        gamepad = FindObjectOfType<Gamepad>();

        if (type == "Chest")
        {
            if (item != null)
            {
                GetComponentInChildren<MeshFilter>().mesh = item.mesh;
                GetComponentInChildren<MeshRenderer>().material = item.material;
            }
        }
    }
    private void Update()
    {
        switch(type)
        {
            case "Door":
                GetComponent<NavMeshObstacle>().enabled = locked;
                if (hasInteracted)
                {

                    if (locked && game.keys < requiredAmount)
                    {
                        if (game.button_Action)
                        {
                            Debug.Log(locked);
                            Debug.Log("You need a key!");
                        }
                    }

                    if (locked && game.keys >= requiredAmount)
                    {
                        game.keys -= requiredAmount;
                        locked = false;
                    }

                    if (!locked)
                    {
                        if (!anim.GetBool("Approached"))
                        {
                            Debug.Log(locked);
                            anim.SetBool("Approached", true);
                        }
                    }
                }
                if (!hasInteracted)
                {
                    if (anim.GetBool("Approached"))
                    {

                        anim.SetBool("Approached", false);
                    }
                }
                break;

            case "Chest":

                if (hasInteracted)
                {
                    if(anim.GetBool("Open"))
                    {
                        Chest(true);
                        hasInteracted = false;
                    }
                    if (!anim.GetBool("Open"))
                    {
                        Chest(false);
                        hasInteracted = false;
                    }


                    Chest(!anim.GetBool("Open"));
                }



                if (obtained)
                {
                    if (selectedItem == 1)
                    {
                        bool wasPickedUp = GameHandler.PlayerInventory.instance.Add(item);
                        if (wasPickedUp)
                        {
                            GetComponentInChildren<MeshRenderer>().enabled = false;
                            selectedItem = 0;
                            
                            obtained = true;
                        }
                    }
                }
                break;

            case "Save Point":
                if (hasInteracted && gamepad.button_Action)
                {
                    FindObjectOfType<SaveMenu>().GetComponent<Canvas>().enabled = true;
                }
                break;
        }



        //    if (locked)
        //{
        //    itemsObtained = 0;
        //    foreach (Item item in game.items)
        //    {
        //        itemsObtained++;
        //    }

        //}


        //switch(type)
        //{
        //    



        //    

        //    case "Door":

        //    
        //        break;



        //}


    }


}










namespace Interactive
{
    public class I_Door : I_Item
    {
        




    }

    public class I_Item : I_Chest
    {
        public virtual void Item()
        {
            if (!hasInteracted)
            {
                bool wasPickedUp = GameHandler.PlayerInventory.instance.Add(item);
                if (wasPickedUp)
                {
                    Destroy(gameObject);
                }
                hasInteracted = true;
            }
        }
    }

    public class I_Chest : Variables
    { 
        protected void Chest(bool open)
        {
            anim = GetComponent<Animator>();
            anim.SetBool("Open", open);
        }


    }

    public class Variables : InspectorVariables
    {
        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
        }




         public int requiredAmount;

        [HideInInspector] public GameManager game;

        protected Gamepad gamepad;

        public Animator anim;
        public Item item;

        public string type;


       

        public bool locked;
        public bool obtained;

        public bool hasInteracted;
        public bool botOverride;
        public int itemsObtained;


        readonly float elapsed;
    }

    public class InspectorVariables : MonoBehaviour
    {
        public int selectedType;
         public int selectedLocked;

        [HideInInspector] public int lockRequirement;
        [HideInInspector] public int selectedItem;

    }
}
