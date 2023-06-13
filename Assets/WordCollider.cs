using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.name == "A boca da noite" || other.name == "Ajoelhou-se") {
            Destroy(other);
        //}

    }


}
