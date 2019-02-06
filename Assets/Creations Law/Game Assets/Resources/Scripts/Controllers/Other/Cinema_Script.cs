using UnityEngine;
using UnityEngine.UI;


//##################################//
//                                                                           //
//            CINEMA SCRIPTS                                     //
//                                                                          //
//                                                                         //
//################################//
//                                                                        //
//    Ingame Cinematics are programmed here.      //            
//                                                                      //
///////////////////////////////////////////////////////////

public class Cinema : Cinematics.DestinationLogic
{
    void Update()
    {
        CheckpointLogic();
    }
}



namespace Cinematics
{
    public class DestinationLogic : Variables
    {
        /// <summary>
        /// Moves the camera frame by frame from point A to point B.
        /// </summary>
        protected void CheckpointLogic()
        {
            switch (start)
            {
                default:
                    break;
                case 1:
                    speechLines.text = "Text Line " + start;
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
                    speechLines.text = "Text Line " + start;
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
    }
    public class Variables : MonoBehaviour
    {
        readonly GameManager game;
        /// <summary>
        /// When the level is loaded the variables stored in this function are set.
        /// </summary>
        private void Awake()
        {
            //transform.localPosition = startLocation[start];
            //start = SceneManager.GetActiveScene().buildIndex;

            speechLines = GameObject.Find("Speech Lines").GetComponent<Text>();
            transform.localPosition = new Vector3(91.75f, -72.33f, -194.28f);
            transform.localRotation = Quaternion.Euler(79.87f, 80.58f, 0f);
            if (FindObjectOfType<PauseMenu>())
            {
                FindObjectOfType<PauseMenu>().canMove = false;
            }
        }


        /// <summary>
        /// Index number for which start point and destination point to choose from.
        /// </summary>
        protected int start;

        /// <summary>
        /// If the cinematic in question calls for text on the screen this is where the text will be stored.
        /// </summary>
        protected Text speechLines;

        /// <summary>
        /// Starting checkpoints when panning from Point A to Point B.
        /// </summary>
        protected readonly Vector3[] startLocation = new Vector3[] 
        {
            new Vector3(87.12f, -74.65f, -206.31f),
            new Vector3(87, -74.7f, -206.3f),
            new Vector3(),
            new Vector3(),
            new Vector3() };

        /// <summary>
        /// Destination checkpoints when panning from Point A to Point B.
        /// </summary>
        protected readonly Vector3[] destination = new Vector3[]
        {
            new Vector3(),
            new Vector3(94.63f, -72.33f, -211.64f),
            new Vector3(102.16f, -74.65f, -205.34f),
            new Vector3(),
            new Vector3()
        };

        /// <summary>
        /// Which angle the camera should be at when panning.
        /// </summary>
        protected readonly Quaternion[] startRotation = new Quaternion[] 
        {
            Quaternion.Euler(45.67f, -3.7f, 0),
            Quaternion.Euler(0, 0, 0),
            Quaternion.Euler(0, 0, 0),
            Quaternion.Euler(0, 0, 0),
            Quaternion.Euler(0, 0, 0) };

    }
}