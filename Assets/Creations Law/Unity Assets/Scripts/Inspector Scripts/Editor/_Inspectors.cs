//||================================||
//||                                                                        ||
//||                     SCRIPT INSPECTORS                    ||
//||                                                                        ||
//||                                                                        ||
//||================================||
//||                                                                        ||
//||    When a script is being viewed as a component, ||
//||    this script comes into action in order to make    ||
//||    it easier to use.                                              ||
//||                                                                        ||
//||================================||

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using ScriptableObjects;

namespace Inspectors
{
    [CustomEditor(typeof(GameManager))]
    public class Viewer_GameManager : Editor
    {
        #region Main Layer
        protected int debugView = 0;
        protected int defaultView;
        protected bool Initialized;

        GameManager unit;
        public BotReciever[] bots = new BotReciever[] { };
        PlayerController[] people = new PlayerController[] { };

        /// <summary>
        /// Initialise the current selected script of the type to be under this script's scope.
        /// </summary>
        void Init()
        {
            if (!Initialized)
            {
                unit = (GameManager)target;
                Initialized = true;
            }
        }
        #endregion

        public override void OnInspectorGUI()
        {
            debugView = GUILayout.Toolbar(debugView, new[] { "Default", "Debug" }, GUILayout.Height(40));
            switch (debugView)
            {
                case 0:
                    #region Find Objects
                    people = FindObjectsOfType<PlayerController>();
                    bots = FindObjectsOfType<BotReciever>();
                    #endregion
                    defaultView = GUILayout.Toolbar(defaultView, new[] { "Controller Test", "Game Settings", "Party", "Inventory", "Misc" });
                    switch (defaultView)
                    {
                        default:
                            break;

                        case 0:
                            ControllerViewer();
                            break;

                        case 1:
                            SettingsViewer();
                            break;

                        case 2:
                            PartyViewer();
                            break;

                        case 3:
                            InventoryViewer();
                            break;
                    }
                    break;

                case 1:
                    base.OnInspectorGUI();
                    break;
            }
            Init();
        }

        /// <summary>
        /// Displays stats about party members.
        /// </summary>
        private void PartyViewer()
        {
            if (people.Length > 0)
            {
                foreach (PlayerController player in FindObjectsOfType<PlayerController>())
                {
                    GUILayout.BeginVertical("In BigTitle");

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label(player.playerInfo.name);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("HP: ", GUILayout.Width(70));
                    player.health = EditorGUILayout.IntField(player.health);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("MP: ", GUILayout.Width(70));
                    player.magic = EditorGUILayout.IntField(player.magic);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Level: ", GUILayout.Width(70));
                    player.level = EditorGUILayout.IntField(player.level);
                    EditorGUILayout.EndHorizontal();

                    GUILayout.EndVertical();
                }
                foreach (BotReciever bot in FindObjectsOfType<BotReciever>())
                {
                    GUILayout.BeginVertical("In BigTitle");

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label(bot.playerInfo.name + " (BOT)");
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("HP: ", GUILayout.Width(70));
                    bot.health = EditorGUILayout.IntField(bot.health);

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("MP: ", GUILayout.Width(70));
                    bot.magic = EditorGUILayout.IntField(bot.magic);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Level: ", GUILayout.Width(70));
                    bot.level = EditorGUILayout.IntField(bot.level);
                    EditorGUILayout.EndHorizontal();

                    GUILayout.EndVertical();
                }
            }
        }

        /// <summary>
        /// Used to display things like audio settings.
        /// </summary>
        private void SettingsViewer()
        {

            List<string> musicNames = new List<string>();
            foreach (AudioClip audio in unit.Music)
            {
                musicNames.Add(audio.name);
            }

            GUILayout.BeginVertical("In BigTitle");
            GUILayout.Label("SOUND\n-------- ", GUILayout.Width(70));

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Music:", GUILayout.Width(70));
            unit.startMusic = EditorGUILayout.Popup(unit.startMusic, musicNames.ToArray());
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("BGM: ", GUILayout.Width(70));
            unit.GetComponent<AudioSource>().volume = EditorGUILayout.Slider(unit.GetComponent<AudioSource>().volume, 0, 0.300f);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("SFX: ", GUILayout.Width(70));
            unit.sound[1] = EditorGUILayout.IntSlider(unit.sound[1], 0, 100);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Voices: ", GUILayout.Width(70));
            unit.sound[2] = EditorGUILayout.IntSlider(unit.sound[2], 0, 100);
            EditorGUILayout.EndHorizontal();

            GUILayout.EndVertical();

            EditorGUILayout.Space();

            GUILayout.BeginVertical("In BigTitle");
            GUILayout.Label("Menus\n-------- ", GUILayout.Width(70));

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Pause Menu", GUILayout.Width(80));
            unit.openMenus[0] = GUILayout.Toggle(unit.openMenus[0], "");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Save Menu", GUILayout.Width(80));
            unit.openMenus[1] = GUILayout.Toggle(unit.openMenus[1], "");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Other Menu", GUILayout.Width(80));
            unit.openMenus[2] = GUILayout.Toggle(unit.openMenus[2], "");
            EditorGUILayout.EndHorizontal();

            GUILayout.EndVertical();

        }

        /// <summary>
        /// Displays controller input(A bit bugged since it's refreshing by standard inspector rules)
        /// </summary>
        private void ControllerViewer()
        {
            List<bool> button = new List<bool>
            {
            Input.GetKey(KeyCode.JoystickButton0),
            Input.GetKey(KeyCode.JoystickButton1),
            Input.GetKey(KeyCode.JoystickButton2),
            Input.GetKey(KeyCode.JoystickButton3),
            Input.GetKey(KeyCode.JoystickButton4),
            Input.GetKey(KeyCode.JoystickButton5),
            Input.GetKey(KeyCode.JoystickButton6),
            Input.GetKey(KeyCode.JoystickButton7),
            Input.GetKey(KeyCode.JoystickButton8),
            Input.GetKey(KeyCode.JoystickButton9),
            Input.GetKey(KeyCode.JoystickButton10),
            Input.GetKey(KeyCode.JoystickButton11),
            Input.GetKey(KeyCode.JoystickButton12)
            };
            string[] buttonName = new string[] { };
            if (unit)
            {
                switch (unit.controller)
                {
                    default:
                        break;

                    case "PS4":
                        buttonName = new string[14];
                        buttonName[0] = "";
                        buttonName[1] = "Cross";
                        buttonName[2] = "Circle";
                        buttonName[3] = "Triangle";
                        buttonName[4] = "L1";
                        buttonName[5] = "R1";
                        buttonName[6] = "Options";
                        buttonName[13] = "Share";
                        break;

                    case "Xbox":
                        buttonName = new string[8];
                        buttonName[0] = "A";
                        buttonName[1] = "B";
                        buttonName[2] = "X";
                        buttonName[3] = "Y";
                        buttonName[4] = "LB";
                        buttonName[5] = "RB";
                        buttonName[7] = "Start";
                        buttonName[6] = "Back";
                        break;

                    case "Snes":
                        buttonName = new string[12];
                        buttonName[2] = "A";
                        buttonName[5] = "B";
                        buttonName[6] = "";
                        buttonName[7] = "L";
                        buttonName[8] = "R";
                        buttonName[11] = "Start";
                        buttonName[12] = "Select";
                        break;

                }
                if (unit.controller != "Keyboard")
                {
                    GUILayout.BeginHorizontal("In BigTitle");
                    GUILayout.Label("Controller: ", GUILayout.Width(80));

                    switch (unit.controller)
                    {
                        default:
                            GUILayout.Label(unit.controller);
                            break;

                        case "":
                            GUILayout.Label("None");
                            break;

                        case "Wireless Controller":
                            GUILayout.Label("PS4");
                            break;

                        case "Controller (Xbox 360 Wireless Receiver for Windows)":
                            GUILayout.Label("Xbox");
                            break;

                        case "Keyboard":
                            GUILayout.Label("Keyboard");
                            break;

                        case "SFC30 Joystick":
                            GUILayout.Label("Snes");
                            break;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                switch (unit.controller)
                {
                    default:
                        GUILayout.BeginVertical("In BigTitle");
                        for (int i = 0; i < buttonName.Length; i++)
                        {
                            if (buttonName[i] != null)
                            {
                                GUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField(buttonName[i], GUILayout.Width(30));
                                EditorGUILayout.Toggle(button[i]);
                                GUILayout.EndHorizontal();

                                Repaint();
                            }
                        }
                        GUILayout.EndVertical();

                        GUILayout.BeginVertical("In BigTitle");

                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Left X");
                        EditorGUILayout.FloatField(Input.GetAxisRaw("Horizontal"));
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Left Y");
                        EditorGUILayout.FloatField(Input.GetAxisRaw("Vertical"));
                        GUILayout.EndHorizontal();

                        GUILayout.EndVertical();
                        break;

                    case "Keyboard":
                        GUILayout.BeginVertical("In BigTitle");
                        EditorGUILayout.LabelField("No Controller");
                        GUILayout.EndVertical();
                        break;

                    case "":
                        GUILayout.BeginVertical("In BigTitle");
                        EditorGUILayout.LabelField(unit.controller);
                        GUILayout.EndVertical();
                        break;
                }
                switch (unit.controller)
                {
                    default:
                        break;

                    case "PS4":
                        GUILayout.BeginVertical("In BigTitle");

                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Right X");
                        EditorGUILayout.FloatField(Input.GetAxisRaw("Axis3"));
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Right Y");
                        EditorGUILayout.FloatField(Input.GetAxisRaw("Axis6"));
                        GUILayout.EndHorizontal();

                        GUILayout.EndVertical();
                        break;

                    case "Xbox":
                        GUILayout.BeginVertical("In BigTitle");

                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Right X");
                        EditorGUILayout.FloatField(Input.GetAxisRaw("Axis4"));
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Right Y");
                        EditorGUILayout.FloatField(Input.GetAxisRaw("Axis5"));
                        GUILayout.EndHorizontal();

                        GUILayout.EndVertical();
                        break;
                }
            }
        }

        /// <summary>
        /// Displays the items the player has picked up.
        /// </summary>
        private void InventoryViewer()
        {
            GUILayout.BeginVertical("In BigTitle");
            int i = 0;
            foreach (string names in unit.itemNames)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(unit.itemNames[i] + ": ", GUILayout.Width(80));
                unit.itemAmount[i] = EditorGUILayout.IntField(unit.itemAmount[i]);
                GUILayout.EndHorizontal();
                i++;
            }
            GUILayout.EndVertical();

        }
    }

    [CustomEditor(typeof(Interactable))]
    public class Viewer_Interactable : Editor
    {
        protected int debugView;
        protected bool Initialized;
        public Interactable unit;
        protected int currentPickerWindow;

        void Init()
        {
            if (!Initialized)
            {
                unit = (Interactable)target;
                Initialized = true;
            }
        }
        #region Debug View Tweaker
        public override void OnInspectorGUI()
        {
            Init();
            debugView = GUILayout.Toolbar(debugView, new[] { "Default", "Debug" });
            switch (debugView)
            {
                case 0:
                    Inspector();
                    break;


                case 1:
                    base.OnInspectorGUI();
                    break;
            }
        }
        #endregion

        void Inspector()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Object Type: ", GUILayout.Width(80));
            unit.selectedType = EditorGUILayout.Popup(unit.selectedType, new string[] { "Item", "Chest", "Door", "Save Point", "Actor", "Event" });
            EditorGUILayout.EndHorizontal();

            switch (unit.selectedType)
            {
                case 0:
                    unit.type = "Item";
                    GUILayout.BeginVertical("In BigTitle");
                    EditorGUILayout.BeginHorizontal();
                    ItemSelection();
                    EditorGUILayout.EndHorizontal();


                    GUILayout.EndVertical();
                    break;



                case 1:
                    unit.type = "Chest";
                    GUILayout.BeginVertical("In BigTitle");
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Contains: ", GUILayout.Width(70));
                    unit.selectedItem = EditorGUILayout.Popup(unit.selectedItem, new string[] { "Empty", "Item", "NPC" });
                    EditorGUILayout.EndHorizontal();
                    switch (unit.selectedItem)
                    {
                        default:
                            unit.item = null;
                            break;

                        case 1:

                            ItemSelection();
                            break;
                    }

                    GUILayout.EndVertical();
                    break;

                case 2:
                    unit.type = "Door";
                    GUILayout.BeginVertical("In BigTitle");
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Locked: ", GUILayout.Width(70));
                    unit.selectedLocked = EditorGUILayout.Popup(unit.selectedLocked, new string[] { "Unlocked", "Locked" });
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                    switch (unit.selectedLocked)
                    {
                        case 0:
                            unit.locked = false;
                            break;


                        case 1:

                            LockedDoor();
                            break;
                    }

                    if (unit.selectedLocked == 1)
                    {



                    }
                    break;

                case 3:
                    unit.type = "Save Point";
                    //GUILayout.BeginVertical("In BigTitle");
                    //GUILayout.EndVertical();
                    break;

                case 4:
                    unit.type = "Actor";
                    GUILayout.BeginVertical("In BigTitle");
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Script Number: ", GUILayout.Width(90));
                    unit.scriptNo = EditorGUILayout.IntField(unit.scriptNo);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                    break;

                case 5:
                    unit.type = "Event";
                    GUILayout.BeginVertical("In BigTitle");
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Event Type: ", GUILayout.Width(70));
                    unit.eventType = EditorGUILayout.Popup(unit.eventType, new string[] { "None", "Ladder", "Partner Action" });
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                    break;

            }
        }
        void ItemSelection()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Item: ", GUILayout.Width(70));
            unit.item = (_Item)EditorGUILayout.ObjectField(unit.item, typeof(_Item), false);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Obtained: ", GUILayout.Width(70));
            unit.obtained = EditorGUILayout.Toggle(!unit.obtained);
            EditorGUILayout.EndHorizontal();
        }
        void LockedDoor()
        {
            unit.locked = true;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Lock Requirment: ", GUILayout.Width(110));
            unit.lockRequirement = EditorGUILayout.Popup(unit.lockRequirement, new string[] { "Item Requirement", "Enemies Defeated" });
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            switch (unit.lockRequirement)
            {
                case 0:
                    GUILayout.Label("Items Required: ", GUILayout.Width(110));
                    unit.requiredAmount = EditorGUILayout.IntField(unit.requiredAmount);
                    break;


                case 1:
                    break;

            }
            EditorGUILayout.EndHorizontal();
        }


        public void ObjectPicked()
        {
            _Item unit = (_Item)target;
            string commandName = Event.current.commandName;
            if (commandName == "ObjectSelectorUpdated")
            {
                unit = (_Item)EditorGUIUtility.GetObjectPickerObject();
                Repaint();
            }
        }

    }

    [CustomEditor(typeof(_Character))]
    public class Viewer_Character : Editor
    {

        #region Font
        protected GUIStyle titleStyle = new GUIStyle(EditorStyles.label);
        protected GUIStyle m_TitleStyle;
        protected GUIStyle TitleStyle { get { return m_TitleStyle; } }
        protected Texture empty = null;

        protected int currentPickerWindow;

        #endregion


        public _Character unit;
        protected bool Initialized;
        protected int debugView = 1;


        /// <summary>
        /// Initialise the current selected script of the type to be under this script's scope.
        /// </summary>
        void Init()
        {
            if (!Initialized)
            {
                #region Font
                m_TitleStyle = titleStyle;
                m_TitleStyle.fontSize = 35;
                m_TitleStyle.padding = new RectOffset(0, 0, 60, 0);
                #endregion
                unit = (_Character)target;
                Initialized = true;
            }
        }


        void ObjectPicked()
        {
            _Character unit = (_Character)target;
            string commandName = Event.current.commandName;
            if (commandName == "ObjectSelectorUpdated")
            {

                //Texture newtexture = (Texture)EditorGUIUtility.GetObjectPickerObject();


                unit.portrait = (Sprite)EditorGUIUtility.GetObjectPickerObject();//Sprite.Create((Texture2D)newtexture, new Rect(0, 0, newtexture.width, newtexture.height), new Vector2(0.5f, 0.5f));
                Repaint();
            }
        }



        protected override void OnHeaderGUI()
        {
            Init();

            #region Portrait Stuff
            GUILayout.BeginHorizontal("In BigTitle");
            if (unit.portrait != null)
            {
                if (GUILayout.Button(unit.portrait.texture, GUILayout.Width(100), GUILayout.Height(100)))
                {
                    currentPickerWindow = EditorGUIUtility.GetControlID(FocusType.Passive);
                    EditorGUIUtility.ShowObjectPicker<Sprite>(null, false, unit.name, currentPickerWindow);
                }
                ObjectPicked();
            }
            if (unit.portrait == null)
            {
                if (GUILayout.Button(empty, GUILayout.Width(100), GUILayout.Height(100)))
                {
                    currentPickerWindow = EditorGUIUtility.GetControlID(FocusType.Passive);
                    EditorGUIUtility.ShowObjectPicker<Sprite>(null, false, unit.name, currentPickerWindow);
                }
                ObjectPicked();
            }
            #endregion
            #region Name Stuff
            unit.name = GUILayout.TextField(unit.name, titleStyle);
            GUILayout.EndHorizontal();
            #endregion
        }








    }


    [CustomEditor(typeof(Entity))]
    public class Viewer_Entity : Editor
    {
        protected Entity unit;
        protected bool initialized;


        void Init()
        {
            if (!initialized)
            {
                unit = (Entity)target;
                initialized = true;
            }
        }

        protected override void OnHeaderGUI()
        {   
        }
        GameManager game;

        public override void OnInspectorGUI()
        {
            Init();
            game = FindObjectOfType<GameManager>();
            EditorGUILayout.BeginHorizontal("In BigTitle");
            GUILayout.Label("Entity Type: ");
            unit.entityType = EditorGUILayout.Popup(unit.entityType, new string[] { "Default", "Player Controller", "Bot Controller", "Interactable" });
            EditorGUILayout.EndHorizontal();

            switch(unit.entityType)
            {
                case 1:
                    PlayerController();
                    break;

                case 2:
                    PlayerController();
                    break;

                case 3:
                    Interactable();
                    break;
            }
            
        }

        void PlayerController()
        {
            if (unit.character > 2) unit.character = 0;
            EditorGUILayout.BeginHorizontal("In BigTitle");
            GUILayout.Label("Character: ");
            unit.character = EditorGUILayout.Popup(unit.character, new string[] { "Scarf","ClownFace","Human"});
            EditorGUILayout.EndHorizontal();
        }

        void Interactable()
        {
            EditorGUILayout.BeginVertical("In BigTitle");
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Type: ");
            unit.character = EditorGUILayout.Popup(unit.character, new string[] { "Item","Chest","Door","Save Point", "Actor", "Event"});
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            switch (unit.character)
            {
                default:
                    
                    break;

                case 0: // Item
                    GUILayout.Label("Item: ");
                    unit.context[0] = EditorGUILayout.Popup(unit.context[0], new string[] { "Nothing", "Potion", "Key" });
                    break;

                case 1: // Chest
                    GUILayout.Label("Item: ");
                    unit.context[0] = EditorGUILayout.Popup(unit.context[0], new string[] { "Nothing", "Potion", "Key" });
                    break;

                case 2: // Door
                    GUILayout.Label("Locked: ");
                    unit.context[0] = EditorGUILayout.Popup(unit.context[0], new string[] { "Unlocked", "Locked" });
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Door Type: ");
                    unit.context[1] = EditorGUILayout.Popup(unit.context[1], new string[] { "Lift", "Swing" });
                    break;

                case 3: // Save Point:

                    break;

                case 4: // Actor
                    GUILayout.Label("Character: ");
                    unit.context[0] = EditorGUILayout.Popup(unit.context[0], new string[] { "Scarf", "ClownFace", "Human", "SignPost" });
                    break;

                case 5: // Event

                    break;


            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }





    }
}