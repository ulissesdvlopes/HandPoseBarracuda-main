using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite oldSprite;
    public Sprite newSprite;
    AudioSource audioData;
    public GameObject[] words;
    bool itMatch = false;
    float offset = 0.01f;
    const float initialX = 0.25f;
    float currentTyperX = initialX;
    float currentTyperY = 1.0f; // 1.4 a 1.0
    bool isFirst = true;
    int lineCount = 1;
    public int numberOfLines = 4;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        anim.SetBool("Yes", false);
        /*for (int i = 0; i < 93; i++) {
            names[i] = words[i].name;
            print(words[i].name);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDisable()
    {
        lineCount = 1;
    }

    void FixedUpdate()
    {
        foreach(GameObject word in words)
            {
                var rb = word.GetComponent<Rigidbody2D>();
                if(rb != null)
                {
                    rb.velocity = Vector2.ClampMagnitude(rb.velocity, 0.02f);
                }
            }

    }

    public void setFalse()
    {
        anim.SetBool("Yes", false);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (GameObject word in words) {
            if (other.name == word.name) {
                anim.SetBool("Yes", true);
                audioData.Play(0);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        foreach (GameObject word in words) {
            if (other.name == word.name) {
                itMatch = true;
            }
        }
        if (itMatch) {
            SpriteRenderer sprite = other.GetComponent<SpriteRenderer>();
            if (!isFirst) {
                currentTyperX += sprite.bounds.size.x / 4; // + offset;
            } else {
                // currentTyperX += sprite.bounds.size.x / 4;
                // print(other.name);
                // print("currentTyperY");
                // print(currentTyperY);
                // print("currentTyperX");
                // print(currentTyperX);
            }
            isFirst = false;
            //change object
            other.transform.position = new Vector3((float)currentTyperX + offset, (float)currentTyperY, 0);
            //print(other.transform.position);
            Destroy(other.GetComponent<Rigidbody2D>());
            Destroy(other.GetComponent<BoxCollider2D>());
            other.transform.rotation = Quaternion.Euler(0, 0, 0);
            other.transform.localScale = new Vector3(other.transform.localScale.x / 2, other.transform.localScale.y / 2, other.transform.localScale.z / 2);
            //print(other.GetComponent<SpriteRenderer>().bounds.size.x);
            

            if (currentTyperX + (sprite.bounds.size.x + offset) < 0.6) {
                currentTyperX += sprite.bounds.size.x / 2 + offset;
            } else {
                //print("NEW LINE");
                
                //currentTyperX = 0.25f + other.GetComponent<SpriteRenderer>().bounds.size.x / 4;
                currentTyperX = initialX;
                currentTyperY -= sprite.bounds.size.y * 1.2f;
                other.transform.position = new Vector3((float)currentTyperX + offset, (float)currentTyperY, 0);
                currentTyperX += sprite.bounds.size.x / 2 + offset;
                // print(other.name);
                // print("currentTyperY");
                // print(currentTyperY);
                // print("currentTyperX");
                // print(currentTyperX);
                lineCount++;
            }
            if (lineCount > numberOfLines)
            {
                print("END WORDS");
                other.gameObject.tag = "lastWord";
            }
            // if (lineCount > 3) {
            //     Destroy(other);
            // }
            // print("currentTyperX");
            // print(currentTyperX);
            
            //print("Deu certo!");
        }
        itMatch = false;
        //print(other.name);
    }


}

