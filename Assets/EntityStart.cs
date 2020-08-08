using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Entity entity = GetComponent<Entity>();
        switch (entity.entityType)
        {
            default:
                Debug.Log("A Bug Happened.");
                break;

            case 1:
                PlayerController player = gameObject.AddComponent<PlayerController>();
                player.playerInfo = entity.playerInfo[entity.character];
                break;

            case 2:
                BotReciever bot = gameObject.AddComponent<BotReciever>();
                bot.playerInfo = entity.playerInfo[entity.character];
                break;

            case 3:
                
                Interactable interactive = gameObject.AddComponent<Interactable>();
                switch(entity.character)
                {
                    default:
                        break;

                    case 2:
                        interactive.selectedType = 2;
                        interactive.type = "Door";
                        switch(entity.context[0])
                        {
                            case 0:
                                interactive.locked = false;
                                break;

                            case 1:
                                interactive.locked = true;
                                break;
                        }
                        interactive.doortype = entity.context[1];
                        break;

                }
                break;
        }
        DestroyImmediate(entity);
        DestroyImmediate(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
