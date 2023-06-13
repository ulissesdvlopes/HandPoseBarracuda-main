//This example shows how RigidbodyConstraints is used to freeze the position and rotation of a Rigidbody in the z axis at start-up.
//It also shows what happens when these constraints are removed, when you press the space key
//Attach this to a GameObject with a Rigidbody to see it in action

using UnityEngine;

public class RigidBodyConstraitsExample : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    Vector3 m_ZAxis;
    public GameObject[] sliders;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        //This locks the RigidBody so that it does not move or rotate in the z axis (can be seen in Inspector).
        m_Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }
    /*
    void Update() {
        for (int i = 0; i < 3; i++) {
            if (sliders[i].transform.localPosition.x < 0) {
                sliders[i].transform.localPosition.x = 0;
            }
            if (sliders[i].transform.localPosition.x > 0.2f) {
                sliders[i].transform.localPosition.x = 0.2f;
            }
        }
    }*/

}
