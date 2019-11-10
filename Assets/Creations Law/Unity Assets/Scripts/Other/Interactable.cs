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


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ScriptableObjects;


public class Interactable : Interactive.I_Item
{
        
    public void Start()
    {
        game = FindObjectOfType<GameManager>();

        switch (type)
        {
            case "Chest":
                if (item != null)
                {
                    GetComponentInChildren<MeshFilter>().mesh = item.mesh;
                    GetComponentInChildren<MeshRenderer>().material = item.material;
                }
                break;

            case "Item":
                if (item != null)
                {
                    GetComponentInChildren<MeshFilter>().mesh = item.mesh;
                    GetComponentInChildren<MeshRenderer>().material = item.material;
                }
                break;

            case "Actor":
                ActorStart();
                break;

            case "Door":
                scriptNo = 7;
                ActorStart();
                gameObject.AddComponent<NavMeshObstacle>();
                gameObject.AddComponent<Animator>();
                BoxCollider doorCollider = gameObject.AddComponent<BoxCollider>();
                BoxCollider doorTrigger = gameObject.AddComponent<BoxCollider>();
                doorTrigger.center = new Vector3(-0, 0.3f, -0.2f);
                doorTrigger.size = new Vector3(2.8f, 4.5f, 8);
                doorTrigger.isTrigger = true;
                break;
        }
    }
    public void Update()
    {
        switch(type)
        {
            case "Door":
               
                GetComponent<NavMeshObstacle>().enabled = locked;
                if (hasInteracted)
                {

                    if (locked && game.itemAmount[0] < requiredAmount)
                    {
                        if (Input.GetKeyDown(game.button_Action) && !currentlyActing)
                        {
                            Script_Handler();
                        }
                    }

                    if (locked && game.itemAmount[0] >= requiredAmount)
                    {
                        game.itemAmount[0] -= requiredAmount;
                        selectedLocked = 0;
                        locked = false;
                    }

                    if (!locked)
                    {
                        if (!anim.GetBool("Approached"))
                        {
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
                if(currentlyActing && Input.GetKeyDown(game.button_Attack))
                {
                    Script_Handler();
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
                        bool wasPickedUp = game.Add(item);
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
                if (hasInteracted && Input.GetKeyDown(game.button_Action) && !game.paused)
                {
                    foreach(MenuChooser menu in FindObjectsOfType<MenuChooser>())
                    {
                        if(menu.name == "Save Menu")
                        {
                            menu.menuActivator = true;
                        }
                       
                    }
                    
                }
                break;

            case "Event":
                if (hasInteracted && !game.paused)
                {
                    Event_Handler();
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
    



    public class I_Item : Scripts
    {
        public virtual void Item()
        {
            wasPickedUp = game.Add(item);
            if (wasPickedUp)
            {
                Destroy(gameObject);
            }
        }

        protected void Chest(bool open)
        {
            anim = GetComponent<Animator>();
            anim.SetBool("Open", open);
        }
    }
    public class I_Event : Variables
    {
        public void Event_Handler()
        {
            switch(eventType)
            {
                default:
                    break;

                case 1:
                    if(game.moveY > 0)
                    {
                        if (game.player[0].playerInfo.name == "Clownface")
                        {
                            game.player[0].moveType = "Climbing";
                        }
                    }
                    break;
            }
        }





        #region TalkScripts

        protected void ChangeHeader(string actorName)
        {
            switch (actorName)
            {
                default:
                    dialogue.actorPortrait.sprite = null;
                    break;

                case "Scarf":
                    dialogue.actorPortrait.sprite = dialogue.actorPortraits[0];
                    break;

                case "Clownface":
                    dialogue.actorPortrait.sprite = dialogue.actorPortraits[1];
                    break;

                case "Perry":
                    dialogue.actorPortrait.sprite = dialogue.actorPortraits[2];
                    break;

                case "Player":
                    dialogue.dialogue[0].text = game.player[0].playerInfo.name;

                    switch (game.player[0].playerInfo.name)
                    {
                        case "Scarf":
                            dialogue.actorPortrait.sprite = dialogue.actorPortraits[0];
                            break;

                        case "Clownface":
                            dialogue.actorPortrait.sprite = dialogue.actorPortraits[1];
                            break;
                    }
                    break;

                case "Actor":
                    dialogue.dialogue[0].text = GetComponent<BotReciever>().playerInfo.name;

                    switch (GetComponent<BotReciever>().playerInfo.name)
                    {
                        case "Scarf":
                            dialogue.actorPortrait.sprite = dialogue.actorPortraits[0];
                            break;

                        case "Clownface":
                            dialogue.actorPortrait.sprite = dialogue.actorPortraits[1];
                            break;
                    }
                    break;
            }
        }

        
        protected void StartTalking(string[] text)
        {
            scriptType = "Sign";
            speech = text;
            game.player[0].canMove = false;
            currentlyActing = true;
            
            if (!dialogue.dialogueBox.enabled && Input.GetKeyDown(game.button_Action) && text.Length > 0)
            {
                dialogue.dialogueBox.enabled = true;
                ChangeHeader(actorNames[0]);
                dialogue.dialogue[1].text = text[0];
            }
        }
        protected void ContinueTalking()
        {

            if (dialogue.dialogueBox.enabled && currentlyActing)
            {
                if (speech.Length > 0)
                {
                    if (dialogue.currentPage == speech.Length)
                    {
                        dialogue.dialogueBox.enabled = false;
                        dialogue.currentPage = 0;
                        currentlyActing = false;
                        game.player[0].canMove = true;
                    }
                    if (dialogue.currentPage <= speech.Length)
                    {
                        ChangeHeader(actorNames[dialogue.currentPage]);
                        dialogue.dialogue[1].text = speech[dialogue.currentPage];
                        dialogue.currentPage++;
                    }
                }
            }
        }
        #endregion
    } 
    public class Variables : InspectorVariables
    {
        public void Awake()
        {
            anim = GetComponentInChildren<Animator>();
        }

        #region Setup
        public GameManager game;
        public string type;
        public bool hasInteracted;
        #endregion

        #region Common
        public Animator anim;
        public _Item item;
        public bool obtained;
        protected bool wasPickedUp;
        #endregion

        #region Door
        public int requiredAmount;
        public bool locked;
        #endregion

        #region Actor

        #endregion
    }

    public class InspectorVariables : Events.Functions
    {
        public int selectedType;
        public int selectedLocked;

        public int eventType;
        [HideInInspector] public int lockRequirement;
        [HideInInspector] public int selectedItem;
    }
}

namespace Events
{
    public class Functions : Script_Mechanics
    {
        public string scriptType;
    }

    public class Script_Mechanics : MonoBehaviour
    {
        public int scriptNo;
        #region Actor
        public _Character playerInfo;
        public bool currentlyActing;
        public Sprite actorPortrait;
        public Vector2 startPos;
        public Vector2 destination;

        protected string[] speech = { };
        protected string[] actorNames = { };
        protected Game.UI.Dialogue_Manager dialogue;

        public GameObject prefab;
        protected void ActorStart()
        {
            BoxCollider actorBox;

            if (playerInfo)
            {
                prefab = Instantiate(playerInfo.prefab, transform);
                actorBox = gameObject.AddComponent<BoxCollider>();
                actorBox.isTrigger = true;
                actorBox.center = new Vector3(0, 0, 0.5f);
                actorBox.size = new Vector3(1, 1, 2);
            }
            //Destroy(GetComponent<MeshFilter>());
            //Destroy(GetComponent<MeshRenderer>());
            dialogue = FindObjectOfType<Game.UI.Dialogue_Manager>();
        }
        #endregion
    }
}
