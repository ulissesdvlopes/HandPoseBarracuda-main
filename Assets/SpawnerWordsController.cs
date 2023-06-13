using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWordsController : MonoBehaviour
{
    AudioSource audioData;
    public GameObject[] words;
    GameObject prefab;
    bool itMatch = false;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        //anim.SetBool("Yes", false);
        /*for (int i = 0; i < 93; i++) {
            names[i] = words[i].name;
            print(words[i].name);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setFalse()
    {
        //anim.SetBool("Yes", false);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (GameObject word in words) {
            if (other.name == word.name) {
                audioData.Play(0);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        //print("Deu certo!");
        foreach (GameObject word in words) {
            if (other.name == word.name) {
                itMatch = true;
                prefab = word;
            }
        }
        if (itMatch) {
            other.transform.localPosition = new Vector3(Random.Range(-11.3f, 9.2f), 200.0f, 0);
            var rb = other.gameObject.GetComponent<Rigidbody2D>();
            //print(rb.velocity);
            rb.velocity = new Vector2(0,0);
            rb.rotation = 0;
            rb.angularVelocity = 0;
        }
        /*foreach (GameObject word in words) {
            if (other.name == word.name) {
                itMatch = true;
            }
        }
        if (itMatch) {
            

            print("Deu certo!");
        }
        itMatch = false;
        //print(other.name);*/
    }


}


