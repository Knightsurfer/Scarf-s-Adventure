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
                break;

            case 1:
                PlayerController player = gameObject.AddComponent<PlayerController>();
                player.playerInfo = entity.playerInfo[entity.character];
                break;

            case 2:
                BotReciever bot = gameObject.AddComponent<BotReciever>();
                bot.playerInfo = entity.playerInfo[entity.character];
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
