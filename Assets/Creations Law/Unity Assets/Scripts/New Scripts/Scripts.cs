using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive
{
    public class Scripts : I_Event
    {
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
                    actorNames = new [] { "Error" };
                    StartTalking(new[] { "The script you have tried to call is currently not available.", "Please try again later..." });
                    break;

                case 0:
                    actorNames = new[] { "Scarf" };
                    StartTalking(new[] { "oops, I'm in the wrong world.", "Just ignore me, I'm not supposed to be here." });
                    if (!currentlyActing)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z);
                    }
                    if (GetComponent<BotReciever>()) GetComponent<BotReciever>().botType = 0;
                    break;

                case 1:
                    actorNames = new[] { "Lamp" };
                    StartTalking(new[] { "How's the sculpture coming on?" });
                    if (!currentlyActing)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z);
                    }
                    break;

                case 2:
                    actorNames = new[] { "Player" };
                    StartTalking(new[] { "I need a key for this door..." });
                    break;

                case 3:
                    actorNames = new[] { "Player" };
                    StartTalking(new[] { "This message portait should change with the character.", "Did it change?" });
                    break;

                case 4:
                    actorNames = new[] { "Actor" };
                    StartTalking(new[] { "Buy something will ya?" });
                    break;

                case 5:
                    actorNames = new[] { "Player","Actor" };
                    StartTalking(new[] { "Hey!", "Hi." });
                    break;

            }
        }
    }
}
