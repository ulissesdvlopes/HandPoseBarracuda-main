using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandTracking : MonoBehaviour
{
    // hand tracking
    public UDPReceive udpReceive;
    public GameObject[] handPoints;
    public GameObject[] lines;

    // control variables
    // public bool hasStarted = false;
    // [System.NonSerialized]
    // public bool introIsOver = false;
    // [System.NonSerialized]
    // public bool introIsPlaying = false;
    // bool instrucIsOn = false;
    // bool instrucIsOver = false;
    // bool introWordIsPlaying = false;
    // bool introWordIsOver = false;
    // bool introSliderIsPlaying = false;
    // bool introSliderIsOver = false;
    // bool hasPassedByWord = false;
    // bool hasPassedBySlider = false;
    // bool hasActivateModel = false;
    // bool onlyOnce = false;
    // int intNumber = 2;

    State state;

    // videos
    public GameObject startVideo;
    public GameObject tutorialVideo;
    public GameObject introVideo;
    public GameObject instrucVideo;
    public GameObject introWordVideo;
    public GameObject introSliderVideo;
    public GameObject sliderVideo;
    public GameObject wordVideo;
    public GameObject previousEndVideo;
    public GameObject endVideo;


    // public GameObject fundo1;
    // public GameObject fundo2;
    // public GameObject[] sliders;
    // public GameObject modeloFatiado;
    // public GameObject botao;
    // public GameObject cube;
    public GameObject handHover;
    // public GameObject[] slidersAssets;
    // public GameObject[] wordsAssets;
    public GameObject endInst;
    public GameObject endReal;
    bool[] finished = {false, false};
    //public EndingController endingConcluder;
    // public int handSizeDivider = 1000; // original = 2000
    // public int handSizeDivider = 2000;
    // public int handSizeDivider = 3000;
    // public int handSizeDivider = 100000;
    public int handSizeDivider;
    

    public AudioSource audioData;
    //public SpriteRenderer spriteRenderer;
    bool test = true;

    public void SetFinished(int index)
    {
        finished.SetValue(true, index);
    }

    public bool HasFinished()
    {
        var allTrue = true;
        foreach (var item in finished)
        {
            if(!item) allTrue = false;
        }
        return allTrue;
    }

    void SetState(State newState)
    {
        if(state)
            state.gameObject.SetActive(false);
        newState.gameObject.SetActive(true);
        state = newState;

    }

    public void ToStart()
    {
        SetState(startVideo.GetComponent<StartVideo>());
    }

    public void ToTutorial()
    {
        SetState(tutorialVideo.GetComponent<TutorialVideo>());
    }

    public void ToIntro()
    {
        SetState(introVideo.GetComponent<IntroVideo>());
    }

    public void ToInstructions()
    {
        //introIsOver = true;
        SetState(instrucVideo.GetComponent<InstructionsVideo>());
    }

    public void ToIntroWords()
    {
        SetState(introWordVideo.GetComponent<IntroWordsVideo>());
    }

    public void ToWords()
    {
        SetState(wordVideo.GetComponent<Words>());
        // introWordIsOver = true;
        // introWordVideo.SetActive(false);
        // onlyOnce = true;
        // intNumber = 0;
    }

    public void ToIntroSliders()
    {
        SetState(introSliderVideo.GetComponent<IntroSlidersVideo>());
    }

    public void ToSliders()
    {
        SetState(sliderVideo.GetComponent<Sliders>());
        // introSliderIsOver = true;
        // introSliderVideo.SetActive(false);
        // onlyOnce = true;
        // intNumber = 1;
    }

    public void ToPreviousEnd()
    {
        SetState(previousEndVideo.GetComponent<PreviousEnd>());
    }

    public void ToEndVideo()
    {
        SetState(endVideo.GetComponent<EndVideo>());
    }

    public void TheEnd()
    {
        ToStart();
        // for (int i = 0; i < 42; i++)
        // {
        //     handPoints[i].SetActive(false);
        // }
        // for (int i = 0; i < 2; i++)
        // {
        //     lines[i].SetActive(false);
        // }
        // for (int i = 0; i < 5; i++) {
        //         wordsAssets[i].SetActive(false);
        // }
        // for(int i = 0; i < 5; i++) {
        //         slidersAssets[i].SetActive(false);
        // }
        // udpReceive.receiveThread.Abort();
        // udpReceive.client.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioData = handHover.GetComponent<AudioSource>();
        Physics.gravity = new Vector3(0, -0.65F, 0);

        // state setting
        // ToStart();
        ToWords();
        // ToIntroWords();
        // ToIntroSliders();
        // ToSliders();
        // ToInstructions();
        // ToPreviousEnd();
        // ToEndVideo();

    }

    public void DrawHands(string[] points)
    {
        for ( int i = 0; i<42; i++)
        {
        
            float x = -1-float.Parse(points[i * 3])/handSizeDivider;
            float y = 1-(float.Parse(points[i * 3 + 1]) / handSizeDivider);
            float z = float.Parse(points[i * 3 + 2]) / handSizeDivider;
                    
            handPoints[i].transform.localPosition = new Vector3(x, y, z);
        
        }
    }

    public void VideoTransition(UnityEngine.Video.VideoPlayer video, ITransitionable transitionable)
    {
        if(!video.isPrepared) return;

        if(video.isPrepared && !transitionable.Started) {
            transitionable.Started = true;
            return;
        }
            
        if(!video.isPlaying)
            transitionable.Transition();
    }

    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;

        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        //print(data);
        string[] points = data.Split(',');
        //print(points[63*3]);

        state.Execute(points);
        if(HasFinished())
        {
            print("-----------------------");
            print("END");
            finished.SetValue(false, 0);
            finished.SetValue(false, 1);
            ToPreviousEnd();
        }

        //0        1*3      2*3
        //x1,y1,z1,x2,y2,z2,x3,y3,z3
        //print(float.Parse(points[126]));
        /*
        if (endingConcluder.concluded) {
            print("Deus");
        }


        if (endingConcluder.concluded) {
            for (int i = 0; i < 52; i ++) {
                endingConcluder.allOthers[i].SetActive(false);
                if (float.Parse(points[126]) > 0) {
                    endingConcluder.instru.SetActive(true);
                    endingConcluder.fim.SetActive(false);
                } else {
                    endingConcluder.instru.SetActive(false);
                    endingConcluder.fim.SetActive(true);
                    var fimComponent = endingConcluder.fim.GetComponent<UnityEngine.Video.VideoPlayer>();
                    if (!fimComponent.isPlaying) {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                }
            }
        }
        */

        // place hands at the start
        // if (points[0] != "3000") {
        //     startCount++;
        //     spriteRenderer.color = Color.white;
        //     audioData.Play(0);
        //     if (startCount > 500) {

        //         ToIntro();
        //         hasStarted = true;
        //         startVideo.SetActive(false);
        //         handHover.SetActive(false);
        //     }

        // } else {
        //     spriteRenderer.color = Color.black;
        // }

        //var introComponent = introVideo.GetComponent<UnityEngine.Video.VideoPlayer>();

        // if (!introComponent.isPlaying && hasStarted == true && introIsOver == false) {
        //     //introIsPlaying = true;
        //     //introVideo.SetActive(true);
        // }

        // if (introComponent.isPlaying) {
        //     introIsPlaying = true;
        // }

        // if (!introComponent.isPlaying && introIsPlaying == true) {
        //     introIsOver = true;
        // }

        // if (introIsOver == true && !instrucIsOver) {
        //     instrucIsOn = true;
        //     introVideo.SetActive(false);
        //     instrucVideo.SetActive(true);
        // }

        // print(points[126]);
        // print(points[127]);

        // if ((float.Parse(points[126]) > 0 && instrucIsOn)) {
        //     //if (instrucIsOn) {
        //     //print(points[]);
        //     // instrucVideo.SetActive(false);
        //     // instrucIsOver = true;
        //     if (float.Parse(points[127]) == 0 || (onlyOnce && intNumber == 0)) {
        //         //Palavras
        //         // intNumber = 0;
        //         // //introWordVideo.SetActive(true);
        //         // onlyOnce = true;
        //         // var introWordComponent = introWordVideo.GetComponent<UnityEngine.Video.VideoPlayer>();
        //         // if (introWordComponent.isPlaying) {
        //         //     introWordIsPlaying = true;
        //         // }

        //         // if (introWordIsPlaying && !introWordComponent.isPlaying) {
        //         //     introWordIsOver = true;
        //         //     introWordVideo.SetActive(false);
        //         // }

        //         if (introWordIsOver) {
        //             // handHover.SetActive(true);
        //             // if (points[0] != "3000")
        //             // {
        //             //     handHover.SetActive(false);
        //             // }
        //             // foreach (GameObject element in wordsAssets) {
        //             //     element.SetActive(true);
        //             // }
        //             // wordVideo.SetActive(true);
        //             // fundo1.SetActive(false);
        //             // fundo2.SetActive(false);
        //             /*for (int i = 0; i < 42; i++)
        //             {

        //                 float x = -1 - float.Parse(points[i * 3]) / 2000;
        //                 float y = 1 - (float.Parse(points[i * 3 + 1]) / 2000);
        //                 float z = float.Parse(points[i * 3 + 2]) / 2000;

        //                 handPoints[i].transform.localPosition = new Vector3(x, y, z);

        //             }*/
        //         }
        //     } else if (float.Parse(points[127]) == 1 || (onlyOnce && intNumber == 1)) {
        //         //Sliders
        //         // intNumber = 1;
        //         // //introSliderVideo.SetActive(true);
        //         // onlyOnce = true;
        //         // var introSliderComponent = introSliderVideo.GetComponent<UnityEngine.Video.VideoPlayer>();
        //         // if (introSliderComponent.isPlaying) {
        //         //     introSliderIsPlaying = true;
        //         // }

        //         // if (introSliderIsPlaying && !introSliderComponent.isPlaying) {
        //         //     introSliderIsOver = true;
        //         //     introSliderVideo.SetActive(false);
        //         // }

        //         if (introSliderIsOver) {
        //             // handHover.SetActive(true);
        //             // if (points[0] != "3000")
        //             // {
        //             //     handHover.SetActive(false);
        //             // }
        //             // foreach (GameObject element in slidersAssets) {
        //             //     element.SetActive(true);
        //             // }
        //             // fundo1.SetActive(true);
        //             // fundo2.SetActive(false);
        //             cube.SetActive(true);
        //             if (hasActivateModel == false) {
        //                 modeloFatiado.SetActive(true);
        //                 hasActivateModel = true;
        //             }
        //             botao.SetActive(true);
        //             for (int i = 0; i < 3; i++) {
        //                 sliders[i].SetActive(true);
        //             }
        //             /*for (int i = 0; i < 42; i++)
        //             {

        //                 float x = -1 - float.Parse(points[i * 3]) / 2000;
        //                 float y = 1 - (float.Parse(points[i * 3 + 1]) / 2000);
        //                 float z = float.Parse(points[i * 3 + 2]) / 2000;

        //                 handPoints[i].transform.localPosition = new Vector3(x, y, z);

        //             }*/
        //         }
        //     }
        //} /*else {
            //     for(int i = 0; i < 5; i++) {
            //         wordsAssets[i].SetActive(false);
            //     }
            //     for(int i = 0; i < 5; i++) {
            //         slidersAssets[i].SetActive(false);
            //     }
                
            //     introWordVideo.SetActive(false);
            //     introSliderVideo.SetActive(false);
            // }*/
  
            // for ( int i = 0; i<42; i++)
            // {
            
            //     float x = -1-float.Parse(points[i * 3])/handSizeDivider;
            //     float y = 1-(float.Parse(points[i * 3 + 1]) / handSizeDivider);
            //     float z = float.Parse(points[i * 3 + 2]) / handSizeDivider;
                        
            //     handPoints[i].transform.localPosition = new Vector3(x, y, z);
            
            // }


        // if (endInst.activeSelf || endReal.activeSelf) {
        //     introVideo.SetActive(false);
        //     instrucVideo.SetActive(false);
        //     introWordVideo.SetActive(false);
        //     introSliderVideo.SetActive(false);
        //     sliderVideo.SetActive(false);
        //     wordVideo.SetActive(false);
        //     for (int i = 0; i < 42; i++)
        //     {
        //         handPoints[i].SetActive(false);
        //     }
        //     for (int i = 0; i < 2; i++)
        //     {
        //         lines[i].SetActive(false);
        //     }
        //     for (int i = 0; i < 5; i++) {
        //             wordsAssets[i].SetActive(false);
        //     }
        //     for(int i = 0; i < 5; i++) {
        //             slidersAssets[i].SetActive(false);
        //     }
        //     udpReceive.receiveThread.Abort();
        //     udpReceive.client.Close();
        // }

    }
}
