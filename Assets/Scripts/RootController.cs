using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{

    public GameObject root; //Reference to prefab to create root objects
    private PlayerController playerController;
    public LayerMask rootableLayerMask;

    private Stack<GameObject> roots = new Stack<GameObject>(); //Queue to store root objects

    public int maxRoots = 100; //Max number of roots that can be on the screen

    private int rootWidth = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //If mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Get position of the mouse and create an object
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            createRoot(worldPosition);
            
        }

        // H - Delete most recent root
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(roots.Pop());
        }

        // G - Delete all roots
        if (Input.GetKeyDown(KeyCode.R))
        {
            int count = roots.Count;
            for (int i = 0; i < count; i++)
            {
                Destroy(roots.Pop());
            }
        }
    }


    private void createRoot(Vector3 mousePosition)
    {
        //Only create root if there is not too many roots
        if (roots.Count < maxRoots && playerController.IsGrounded())
        {
            GameObject temp = (GameObject)Instantiate(root, new Vector3(mousePosition.x, mousePosition.y, 1), Quaternion.identity);
            Collider2D collider = temp.GetComponent<Collider2D>();
            RaycastHit2D[] hits = new RaycastHit2D[1];
            //Create root if touching player
            if (collider.Cast(Vector2.zero, hits, rootWidth) > 0)
            {
                roots.Push((GameObject)temp);
                Debug.Log("Root created on player");
                return;
            }
            //Create root if touching another root
            foreach (GameObject root in roots)
            {
                // basic collision detection since I'm unfamiliar with Collider2D
                if(temp.transform.position.x >+ root.transform.position.x-rootWidth && temp.transform.position.x <= root.transform.position.x + rootWidth
                    && temp.transform.position.y > +root.transform.position.y - rootWidth && temp.transform.position.y <= root.transform.position.y + rootWidth)
                {
                    roots.Push((GameObject)temp);
                    Debug.Log("Root created on root");
                    return;
                }
            }
            //Create root if touching "rootable" layer
            RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0f, rootableLayerMask);
            if(raycastHit.collider != null)
            {
                roots.Push((GameObject)temp);
                Debug.Log("Root created on rootable Layer");
                return;
            }


            //If not colliding with anything, destroy this object
            Destroy(temp);
        }
    }
}
