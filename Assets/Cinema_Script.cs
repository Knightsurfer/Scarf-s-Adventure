using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cinema_Script : MonoBehaviour {


    public int start;
    Text speechLines;

    private Vector3[] startLocation = new Vector3[] {new Vector3(87.12f,-74.65f,-206.31f), new Vector3(), new Vector3(), new Vector3(),new Vector3() };
    private Quaternion[] startRotation = new Quaternion[] { Quaternion.Euler(45.67f,-3.7f,0), Quaternion.Euler(0,0,0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0) };


    private Vector3[] destination = new Vector3[] { new Vector3(), new Vector3(94.63f, -72.33f, -211.64f), new Vector3(102.16f,-74.65f,-205.34f), new Vector3(), new Vector3() };
   




    void Start ()
    {
        speechLines = GameObject.Find("Speech Lines").GetComponent<Text>();
        start = SceneManager.GetActiveScene().buildIndex;
        transform.localPosition = new Vector3(91.75f,-72.33f,-194.28f);
        transform.localRotation = Quaternion.Euler(79.87f, 80.58f, 0f);
        
    }

    // Update is called once per frame
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
                    transform.localRotation = startRotation[start - 1];
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
                }
                break;
        }



        

        
        
    }


    void StartingLocations()
    {
        transform.localPosition = startLocation[start];
    }














}
