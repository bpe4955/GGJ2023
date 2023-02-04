using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{

    public GameObject root; //Reference to prefab to create root objects

    private Stack<GameObject> roots = new Stack<GameObject>(); //Queue to store root objects

    private int maxRoots; //Max number of roots that can be on the screen

    // Start is called before the first frame update
    void Start()
    {
        maxRoots = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //If mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Get position of the mouse and create an object
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Only create root if there is room
            if (roots.Count < maxRoots)
            {
                GameObject temp = (GameObject)Instantiate(root, new Vector3(worldPosition.x, worldPosition.y, 1), Quaternion.identity);
                roots.Push(temp);
            }

            
        }

        // H - Delete most recent root
        if (Input.GetKeyDown(KeyCode.H))
        {
            Destroy(roots.Pop());
        }

        // G - Delete all roots
        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach (GameObject root in roots)
            {
                Destroy(root);
            }
        }
    }
}
