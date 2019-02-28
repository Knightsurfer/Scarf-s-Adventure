using UnityEngine;
using UnityEngine.UI;


//||=================================||
//||                                                                          ||
//||            CINEMA SCRIPTS                                     ||
//||                                                                          ||
//||                                                                          ||
//||=================================||
//||                                                                          ||
//||    Ingame Cinematics are programmed here.         ||            
//||                                                                          ||
//||=================================||

public class Cinema_Script : Cinema.Variables
{
    void Start()
    {
        speechLines = GameObject.Find("Speech Lines").GetComponent<Text>();
        //start = SceneManager.GetActiveScene().buildIndex;
        transform.localPosition = new Vector3(91.75f, -72.33f, -194.28f);
        transform.localRotation = Quaternion.Euler(79.87f, 80.58f, 0f);
        if (FindObjectOfType<PlayerController>())
        {
            FindObjectOfType<PlayerController>().canMove = false;
        }
    }
    void Update()
    {
        speechLines.text = "Text Line " + start;
        switch (start)
        {
            case 1:

                if (transform.localPosition != destination[start])
                {

                    transform.localPosition = Vector3.MoveTowards(transform.position, destination[start], 2 * Time.deltaTime);
                }
                if (transform.localPosition == destination[start])
                {
                    transform.localPosition = startLocation[start - 1];
                    start++;
                }
                break;


            case 2:

                transform.localPosition = Vector3.MoveTowards(transform.position, destination[start], 2 * Time.deltaTime);
                if (transform.localPosition == destination[start])
                {
                    transform.localPosition = startLocation[start - 1];
                    transform.localRotation = startRotation[start - 1];
                    start++;
                    transform.localPosition = new Vector3(94.31001f, -75.3f, -207.32f);
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                break;

            case 3:
                GameObject.Find("Scarf(cinema)").GetComponent<Animator>().SetBool("StartMotion", true);
                if (GameObject.Find("Scarf(cinema)").transform.localRotation == Quaternion.Euler(0.3f, 180, 0))
                {

                    GameObject.Find("Cinematic Objects").SetActive(false);
                    GameObject.Find("Scarf").GetComponentInChildren<Animator>().enabled = true;
                    GameObject.Find("Scarf").GetComponentInChildren<Camera>().enabled = true;
                    GameObject.Find("Scarf").GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                    GameObject.Find("Scarf").GetComponent<PlayerController>().enabled = true;


                }
                break;
        }
    }
    void StartingLocations()
    {
        transform.localPosition = startLocation[start];
    }
}

namespace Cinema
{
   public class Variables : MonoBehaviour
    {
        protected int start;
        protected Text speechLines;

        protected Vector3[] startLocation = new Vector3[]
        {
        new Vector3(87.12f, -74.65f, -206.31f),
        new Vector3(87, -74.7f, -206.3f),
        new Vector3(),
        new Vector3(),
        new Vector3()
        };
        protected Quaternion[] startRotation = new Quaternion[]
        {
        Quaternion.Euler(45.67f, -3.7f, 0),
        Quaternion.Euler(0, 0, 0),
        Quaternion.Euler(0, 0, 0),
        Quaternion.Euler(0, 0, 0),
        Quaternion.Euler(0, 0, 0)
        };

        protected Vector3[] destination = new Vector3[]
        {
        new Vector3(),
        new Vector3(94.63f, -72.33f, -211.64f),
        new Vector3(102.16f, -74.65f, -205.34f),
        new Vector3(),
        new Vector3()
        };
    }
}