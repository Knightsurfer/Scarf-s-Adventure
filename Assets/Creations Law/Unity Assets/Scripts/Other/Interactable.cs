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








public class Interactable : Interactive.I_Item
{
    
    
    
    private void Start()
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
                break;
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

                    if (locked && game.itemAmount[0] < requiredAmount)
                    {
                        if (game.button_Action && !currentlyActing)
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
                if(currentlyActing && game.button_Attack)
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
                if (hasInteracted && game.button_Action && !game.paused)
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
    



    public class I_Item : I_Event
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



        public void Script_Handler()
        {
            switch (currentlyActing)
            {
                case false:
                    SetScript();
                    break;

                case true:
                    switch (scriptType)
                    {
                        case "Sign":
                            ContinueTalking();
                            break;
                    }
                    break;
            } 
        }
        protected void SetScript()
        {
            switch (scriptNo)
            {
                default:
                    StartTalking("Error", new[] { "The script you have tried to call is currently not available.", "Please try again later..." });
                    scriptNo = 0;
                    break;

                case 0:
                    StartTalking("Test", new[] { "Hey", "I'm Cool! 8D" });
                    scriptNo++;
                    break;

                case 1:
                    StartTalking("Navi", new[] { "Hey!", "Listen!" });
                    scriptNo++;
                    break;

                case 2:
                    StartTalking("Test Boi", new[] { "I'm a test script! ^ ^", "Did I do a good job? 83c", "Third Line testing." });
                    scriptNo++;
                    break;


                case 3:
                    StartTalking("Clownface", new[] { "This little dino doodle is so cool. ^ ^ \nI wonder who drew it. ^ ^" });
                    if(!currentlyActing)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y +15, transform.position.z);
                    }
                    break;

                case 4:
                    StartTalking("Scarf", new[] { "I ain't no fairy boy..." });
                    if (!currentlyActing)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z);
                    }
                    break;

                case 5:
                    StartTalking("Scarf", new[] { "oops, I'm in the wrong world.", "Just ignore me, I'm not supposed to be here." });
                    if (!currentlyActing)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z);
                    }
                    if(GetComponent<BotReciever>()) GetComponent<BotReciever>().isPartyMember = true;                   
                    break;

                case 6:
                    StartTalking("Lamp", new[] { "How's the sculpture coming on?" });
                    if (!currentlyActing)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z);
                    }
                    break;

                case 7:
                    StartTalking("Clownface", new[] { "I need a key for this door..." });
                    break;

                case 8:
                    StartTalking("???", new[] { "Hello?" });
                    break;
                
            }
        }

        #region TalkScripts
        protected void StartTalking(string actorName, string[] text)
        {
            scriptType = "Sign";
            speech = text;
            currentlyActing = true;
            dialogue.actorName.text = actorName;
            

            switch(actorName)
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
            }


            if (!dialogue.dialogueBox.enabled && game.button_Action && text.Length > 0)
            {
                dialogue.dialogueBox.enabled = true;
                dialogue.dialogueText.text = text[0];

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
                    }

                    if (dialogue.currentPage <= speech.Length)
                    {
                        dialogue.dialogueText.text = speech[dialogue.currentPage];
                        dialogue.currentPage++;
                    }
                }
            }
        }
        #endregion

        protected void Movement()
        {

        }



    } 
    public class Variables : InspectorVariables
    {
        private void Awake()
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

        #region Other
        public bool botOverride;
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
    public class Functions : Scripts
    {
        public string scriptType;
    }
    public class Scripts : Script_Mechanics
    {
       
    }
    public class Script_Mechanics : MonoBehaviour
    {
        public int scriptNo;
        #region Actor
        public _Character playerInfo;
        public bool currentlyActing;
        public Sprite actorPortrait;
        Vector2 startPos;
        Vector2 destination;

        protected string[] speech = { };
        protected Game.UI.Dialogue_Manager dialogue;

       protected void ActorStart()
        {

        GameObject prefab;
        BoxCollider actorBox;

            if (playerInfo)
            {
                prefab = Instantiate(playerInfo.prefab, transform);
                actorBox = gameObject.AddComponent<BoxCollider>();
                actorBox.isTrigger = true;
                actorBox.center = new Vector3(0, 0, 0.5f);
                actorBox.size = new Vector3(1, 1, 2);
            }
            Destroy(GetComponent<MeshFilter>());
            Destroy(GetComponent<MeshRenderer>());
            dialogue = FindObjectOfType<Game.UI.Dialogue_Manager>();
        }
        #endregion
    }
}
