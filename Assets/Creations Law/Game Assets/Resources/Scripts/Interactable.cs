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





public class Interactable : MonoBehaviour
{
     public int selectedType;
    [HideInInspector] public int selectedLocked;
    [HideInInspector] public int lockRequirement;
    [HideInInspector] public int requiredAmount;
    [HideInInspector] public int selectedItem;
    [HideInInspector] public GameManager game;

    private Gamepad gamepad;
    
    public Animator anim;
    public Item item;

    public string type;


    public string[] items = new[] { "None","Key","NPC" };

    public bool locked;
    public bool obtained;

    public bool hasInteracted;
    public bool botOverride;
    public int itemsObtained;


   readonly float elapsed;
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
        
        if (locked)
        {
            itemsObtained = 0;
            foreach (Item item in game.items)
            {
                itemsObtained++;
            }
        }


        switch(type)
        {
            case "Chest":
                anim = GetComponentInChildren<Animator>();
                if (hasInteracted && gamepad.button_Action)
                {
                    Chest(!anim.GetBool("Open"));
                }
               
                if (obtained)
                {
                    if (selectedItem == 1)
                    {
                        bool wasPickedUp = PlayerInventory.instance.Add(item);
                         if (wasPickedUp)
                        {
                            GetComponentInChildren<MeshRenderer>().enabled = false;
                            selectedItem = 0;
                        }
                    }
                }



                break;

            case "Door":
                GetComponent<NavMeshObstacle>().enabled = locked;
                anim = GetComponentInChildren<Animator>();
                if (hasInteracted && gamepad.button_Action || hasInteracted && botOverride)
                {
                    if (!anim.GetBool("Approached"))
                    {
                        Door();
                    }
                }
                break;



        }
 
    }

    private void Door()
    {
            if (!locked)
            {
                anim.SetBool("Approached", !anim.GetBool("Approached"));
            }
            if (locked == true && itemsObtained >= requiredAmount)
            {
                selectedLocked = 0;
                locked = false;
                game.items.RemoveAt(itemsObtained - 1);
                
                anim.SetBool("Approached", !anim.GetBool("Approached"));

            }
    }
    private void Chest(bool open)
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Open", open);
    }
    public virtual void Item()
    {
        if (!hasInteracted)
        {
            bool wasPickedUp = PlayerInventory.instance.Add(item);
            if (wasPickedUp)
            {
                Destroy(gameObject);
            }
            hasInteracted = true;
        }
    }

}
