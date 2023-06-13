using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    float countMax = 2000;
    float count = 0;
    float countCracked = 0;
    float countCrackedMax = 300;
    bool concluded = false;
    bool isGoing = false;
    bool rotated = false;
    bool instruIsPlaying = false;
    bool fimIsPlaying = false;
    bool fimIsOver = false;
    bool active = false;
    bool cracked = false;
    bool onlyOnce = false;
    bool concluido = false;
    public bool Ending = false;
    int countCollider = 0;
    public GameObject bar;
    AudioSource audioData;
    public GameObject[] hand;
    public GameObject[] allOthers;
    public UDPReceive udpReceive;
    public GameObject instru;
    public GameObject fim;
    // Start is called before the first frame update
    private bool displayInstru = false;
    private UnityEngine.Video.VideoPlayer instruComponent;
    private UnityEngine.Video.VideoPlayer fimComponent;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        instruComponent = instru.GetComponent<UnityEngine.Video.VideoPlayer>();
        fimComponent = fim.GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (cracked) {
            countCracked++;
            print(countCracked);
        }
        if (countCracked >= countCrackedMax) {

            countCracked = 0;
            count = 0;
            concluded = false;
            isGoing = false;
            rotated = false;
            active = false;
            cracked = false;
            onlyOnce = false;
        }*/
        /*if (active) {
            if (!onlyOnce) {
                audioData.Play(0);
                onlyOnce = true;
            }
        }
        if (count > 0 && !concluido) {
            bar.SetActive(true);
            float progress = count / 30000;
            bar.transform.localScale = new Vector3((float)progress, 0.01f, 0.01f);
        } else {
            bar.SetActive(false);
        }
        */

        //print(instru.activeSelf);

        if(countCollider >= countMax) {

            if(instru.activeSelf == false)
                instru.SetActive(true);
            if (instruComponent.isPlaying) {
                instruIsPlaying = true;
            }   

            if (instruIsPlaying && !instruComponent.isPlaying) {
                concluido = true;
                countCollider = 0;
                instru.SetActive(false);
            }
        }

        if (concluido) {
            //for (int i = 0; i < 52; i ++) {
                //allOthers[i].SetActive(false);
                
                    if(fim.activeSelf == false)
                        fim.SetActive(true);
                    
                    if (fimComponent.isPlaying) {
                        fimIsPlaying = true;
                    }   

                    if (fimIsPlaying && !fimComponent.isPlaying) {
                        fimIsOver = true;
                        fim.SetActive(false);
                    }
                    if (fimIsOver) {
                        print("reinicia");
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                
            //}
        }
        //if (isGoing && slider2.transform.localPosition[0] < -0.12) {
            //slider2.transform.localPosition = new Vector3(slider2.transform.localPosition.x + 0.0005f, slider2.transform.localPosition.y, slider2.transform.localPosition.z);
        //} else if (isGoing && slider2.transform.localPosition[0] > -0.13) {
            //slider2.transform.localPosition = new Vector3(slider2.transform.localPosition.x - 0.0005f, slider2.transform.localPosition.y, slider2.transform.localPosition.z);
        //} else if (isGoing) {
            //rotated = true;
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        countCollider++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(countCollider > countMax) return;
        active = false;
        countCollider--;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(concluido) return;
        countCollider++;
        //print(countCollider);
        // foreach (GameObject point in hand) {
        //     if (other.name == point.name) {

        //                 concluido = true;

                    
                
        //     }
        // }
        
    }


}
