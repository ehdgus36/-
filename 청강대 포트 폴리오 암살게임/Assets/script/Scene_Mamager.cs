using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
[System.Serializable]
public class button_on
{
    public Button GetButton;
    public bool button_on_of;
}
 public class Scene_Mamager : MonoBehaviour
{
    public static Scene_Mamager instance;
    public bool[] button_s;
    public static Button[] buttons;
    [SerializeField] public static int levelcount;
    public int nextScene;
    public int isScene;
    public Player player;
    public bool gameover;
    public bool menuScene;
    public GameObject GameOverWindow;
    public GameObject[] enemy;
    public GameObject Qbutton;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        button_s = new bool[5];
       
        if (menuScene==true)
        {
            for (int i = 0; i < button_s.Length; i++)
            {
                button_s[i] = false;
            }
            button_s[0] = true;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = button_s[i];
            }
        }
    }
    void Start()
    {
        Debug.Log(levelcount);
      

    }

    // Update is called once per frame
    void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (player != null && gameover == true)
        {
            gameover = false;
            GameOverWindow.SetActive(true);
        }
    }
    public void Button(int a)
    {
        SceneManager.LoadScene(a);
        levelcount += 1;
    }
    public void gameExit()
    {
        Application.Quit();
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("emfdjdha");
            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i].GetComponent<Enamy>().enabled != false)
                {
                    return;
                }
               
            }
            Qbutton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Q))
            {
               

                button_s[isScene] = true;

                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
