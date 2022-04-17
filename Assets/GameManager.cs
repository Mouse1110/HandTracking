using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private Vector3 trackedPosition;
    public bool start;
    public GameObject pnStart;
    public GameObject[] pn;
    public GameObject[] btnYes15;
    public GameObject[] btnNo15;
    public GameObject panelFinally;
    public bool[] anwser;
    public Text txtPos;
    private int init;
    private int index;
    public bool debug;
    public GameObject next;
    public string[] info;
    public GameObject textInfo;
    public GameObject txtInit;
    private bool isNext;
    public GameObject lineTrue;
    public GameObject lineFalse;
    // Start is called before the first frame update
    private int core;

    public Text finallyText;
    public Text finallyAwa;

    public GameObject point;
    /// point
    public GameObject startButton;
    void Start()
    {
       // pnStart.SetActive(true);
       // pn15.SetActive(false);
        start = false;
        init = 0;
        core = 0;
        debug = true;
        isNext = false;
    }

    // Update is called once per frame
    void Update()
    {
    	trackedPosition = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.palm_center;
        txtPos.text = "X: " + trackedPosition.x.ToString() + "  Y: " + trackedPosition.y.ToString();
        point.GetComponent<RectTransform>().position = new Vector3(Screen.width*trackedPosition.x,Screen.height*trackedPosition.y,point.GetComponent<RectTransform>().position.z);
    }
    public void Click(string tag)
    {
        if (init > 0 && init <=5)
        {
            if (tag=="btnYes")
            {
                if (anwser[index]){
                    btnYes15[index].GetComponent<RectTransform>().position = new Vector3(Screen.width/2,btnYes15[index].GetComponent<RectTransform>().position.y,btnYes15[index].GetComponent<RectTransform>().position.z);
                   btnYes15[index].GetComponent<Collider2D>().isTrigger = false;
                    btnNo15[index].SetActive(false);
                    textInfo.GetComponent<Text>().text=info[index];
                    textInfo.SetActive(true);
                    next.SetActive(true);
                    lineTrue.SetActive(true);
                    core++;
                } else{
                   btnNo15[index].GetComponent<RectTransform>().position = new Vector3(Screen.width/2,btnNo15[index].GetComponent<RectTransform>().position.y,btnNo15[index].GetComponent<RectTransform>().position.z);
                    btnNo15[index].GetComponent<Collider2D>().isTrigger = false;
                    btnYes15[index].SetActive(false);
                    textInfo.GetComponent<Text>().text=info[index];
                    textInfo.SetActive(true);
                    next.SetActive(true);
                    lineFalse.SetActive(true);
                }
                
                isNext = true;
            }
            else  if (tag=="btnNo")
            {
                if (!anwser[index]){
                    btnNo15[index].GetComponent<RectTransform>().position = new Vector3(Screen.width/2,btnNo15[index].GetComponent<RectTransform>().position.y,btnNo15[index].GetComponent<RectTransform>().position.z);
                    btnNo15[index].GetComponent<Collider2D>().isTrigger = false;
                    btnYes15[index].SetActive(false);
                    textInfo.GetComponent<Text>().text=info[index];
                    textInfo.SetActive(true);
                    next.SetActive(true);
                    lineTrue.SetActive(true);
                    core++;
                } else{
                    btnYes15[index].GetComponent<RectTransform>().position = new Vector3(Screen.width/2,btnYes15[index].GetComponent<RectTransform>().position.y,btnYes15[index].GetComponent<RectTransform>().position.z);
                    btnNo15[index].SetActive(false);
                    btnYes15[index].GetComponent<Collider2D>().isTrigger = false;
                    textInfo.GetComponent<Text>().text=info[index];
                    textInfo.SetActive(true);
                    next.SetActive(true);
                    lineFalse.SetActive(true);
                }
                
                isNext = true;
            }
        } else{
             txtInit.SetActive(false);
              start = false;
            panelFinally.SetActive(true);
            finallyText.text = core.ToString()+"/5 câu trả lời đúng";
           finallyAwa.text = core>2?"Phần quà của bạn là 1 bình nước đến từ Mastercard":"Phần quà của bạn là 1 hộp nến thơm đến từ Mastercard";
        }
    }

    public void StartGame(string tag)
    {
        if (!debug){
            if (!start)
            {
                if (init == 0){
                    
                    if (tag=="start")
                    {
                        pnStart.SetActive(false);
                        start = true;
                        init = 1;
                        index =  Random.Range(0, pn.Length);
                        pn[index].SetActive(true);
                        txtInit.SetActive(true);
                        txtInit.GetComponent<Text>().text = init.ToString()+"/5";
                    }
                } else {
                    SceneManager.LoadScene(0);
                }
               
            } else
            {
                if (!isNext){
                    Click(tag);
                } else if (tag=="next"){
                    pn[index].SetActive(false);
                    RemoveAt(ref pn,index);
                    next.SetActive(false);
                    lineFalse.SetActive(false);
                    lineTrue.SetActive(false);
                    index =  Random.Range(0, pn.Length);
                    pn[index].SetActive(true);
                    txtInit.SetActive(true);
                    txtInit.GetComponent<Text>().text = init.ToString()+"/5";
                    isNext = false;
                    init++;
                }
                
            }
        }
      
    }



    public void SetDebug(){
        debug = !debug;
        point.SetActive(!debug);
    }

    public static void RemoveAt<T>(ref T[] arr, int index)
  {
       // replace the element at index with the last element
      arr[index] = arr[arr.Length - 1];
        // finally, let's decrement Array's size by one
        System.Array.Resize(ref arr, arr.Length - 1);
  }

}
