using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour
{
    public bool useSpring = false;
    public LineRenderer lineDrag;
    Rigidbody2D grabbedObject;
    SpringJoint2D springJoint;
    float dragSpeed = 6f;

    void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);
            Vector2 dir = Vector2.zero;

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, dir);

            if (hit.collider != null)
            {
                // hit has a reference to collider, which has reference to all collider objects including this
                if (hit.collider.GetComponent<Rigidbody2D>() != null)
                {
                    grabbedObject = hit.collider.GetComponent<Rigidbody2D>();
                    if (useSpring)
                    {
                        // for the ANCHOR POINT ON THE OBJECT //
                        springJoint = grabbedObject.gameObject.AddComponent<SpringJoint2D>();
                        // set anchor to the spot on the object that we clicked

                        Vector3 localHitPoint = grabbedObject.transform.InverseTransformPoint(hit.point);
                        springJoint.distance = 0.25f;
                        springJoint.dampingRatio = 1;
                        springJoint.frequency = 5;
                        // hit.point tells the point in WORLD SPACE, hence this.
                        springJoint.anchor = localHitPoint; // auto-upscaling of some V2 to V3, not sure. 

                        // EITHER OR OF THESE IS NEEDED TO WAKE UP THE SPRING, EVEN FOR NULL
                        //springJoint.enableCollision = true; // allow for collisions while "springed"
                        springJoint.connectedBody = null; // extremely redundant line... but required. 
                    }
                    else
                    {
                        grabbedObject.gravityScale = 0;
                    }
                    lineDrag.enabled = true;

                }
            }
        }

        if (Input.GetMouseButtonUp(0) && grabbedObject != null)
        {
            if (useSpring)
            {
                Destroy(springJoint);
                springJoint = null;
            }
            else
            {
                grabbedObject.gravityScale = 1;
            }

            grabbedObject = null;
            lineDrag.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (grabbedObject != null)
        {
          // for the CONNECTED ANCHOR POINT ON THE MOUSE //
            Vector2 mouseWorldPos2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (useSpring)
            {
                springJoint.connectedAnchor = mouseWorldPos2D; // THIS IS LOCAL.
            }
            else
            {
                grabbedObject.velocity = (mouseWorldPos2D - grabbedObject.position) * dragSpeed;
            }
        }
    }

    void LateUpdate()
    {
        if (grabbedObject != null)
        {
            if (useSpring)
            {
                Vector3 worldAnchor = grabbedObject.transform.TransformPoint(springJoint.anchor); // local to world

                lineDrag.SetPosition(0, new Vector3(worldAnchor.x, worldAnchor.y, -1));
                lineDrag.SetPosition(1, new Vector3(springJoint.connectedAnchor.x, springJoint.connectedAnchor.y, -1));
            }
            else
            {
                Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lineDrag.SetPosition(0, new Vector3(grabbedObject.position.x, grabbedObject.position.y, -1));
                lineDrag.SetPosition(1, new Vector3(mouseWorldPos3D.x, mouseWorldPos3D.y, -1));
            }
        }
    }
}
