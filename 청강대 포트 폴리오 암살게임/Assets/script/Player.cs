using UnityEngine;

public class Player : MonoBehaviour
{
    public bool die;
    Animator anime;
    PlayerAttack plattack;
    PlayerMove plmove;
    // Start is called before the first frame update
    void Start()
    {
        plattack = GetComponent<PlayerAttack>();
        plmove = GetComponent<PlayerMove>();
        anime = GetComponent<Animator>();
        die = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (die==true)
        {
            die = false;
            player_die();
        }
        if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && anime.GetCurrentAnimatorStateInfo(0).IsName("die"))
        {
            Scene_Mamager.instance.gameover = true;
        }
    }
    void player_die()
    {
        anime.SetTrigger("die");
        plattack.enabled = false;
        plmove.enabled = false;
        

    }
}
