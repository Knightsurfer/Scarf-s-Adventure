using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPerson_Interact : ThirdPersonController
{
    protected GameObject buttonPrompt;
    protected Collider chest;

    protected void InteractStart()
    {
        buttonPrompt = GameObject.Find("Prompt");

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            player.transform.localPosition = new Vector3(51, 0, 34);
        }
    }
    protected void InteractUpdate()
    {
        ObjectBehaviour();
        LevelBounds();
    }

    protected void ButtonPrompt()
    {
        //buttonPrompt.transform.LookAt(cam.transform.position);
    }
    protected void LevelBounds()
    {
        if (transform.localPosition.y < -15)
        {
            transform.localPosition = new Vector3(47, 1, 38);
        }
    }
    protected void ObjectBehaviour()
    {
        if (chest != null)
        {
            if (button_Action)
            {
                chest.GetComponent<Animator>().SetBool("Open", true);

                if (anim.GetBool("Working") == true)
                {
                    anim.SetBool("Working", false);

                }
                else if (anim.GetBool("Working") == false)
                {
                    anim.SetBool("Working", true);
                }
            }
            if (chest.GetComponent<Animator>().GetBool("Open") == true)
            {
                if (button_Attack)
                {
                    chest.GetComponent<Animator>().SetBool("Open", false);
                }
            }
        }
    }


    protected void OnTriggerEnter(Collider other)
{
    if (other.name == "Chest")
    {
        chest = other;
    }
    if (other.name == "Sprite Light")
    {
        other.enabled = false;
        other.GetComponent<SpriteAI>().triggered = true;
    }
    if (other.tag == "NPC")
    {
        buttonPrompt.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 1.8f, other.transform.position.z + 0.5f);
    }
}
    protected void OnTriggerExit(Collider other)
{
    if (other.tag == "NPC")
    {
        buttonPrompt.transform.position = new Vector3();
    }
    if (other.name == "Chest")
    {
        chest = null;
        other.GetComponent<Animator>().SetBool("Open", false);
    }
}
}
