using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEditor;
[System.Serializable]
class Enemy_Patrol_Point
{
    public Transform patrol_point;

    public bool End;// 이위치가 마지막인지 확인
    public bool idle_Searching;// 이위치에서 잠깐 멈춰서 수색(좌우보기) 할건지
}
public enum EnamyState
{ 
Idle,Patrol,Tracking,Attack,find, Searching,die
}
public class Enamy : MonoBehaviour
{
    public float angle;
    public float angleView;
    public List<Transform> patrol_Point;
    NavMeshAgent agent;
    public Transform tf;
   [SerializeField] EnamyState state;
    Animator anime;
    public GameObject create_enemy;
    public int targe_patrol_Point=0;
    int a=0;// 나중에 다시 만들기
    public GameObject player;
    public LayerMask layer;
    public float findTime;
    public float times;
    bool look=true;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        state = EnamyState.Patrol;
        tf = patrol_Point[0];
    }

    // Update is called once per frame
    void Update()
    {
        GameUpdate();
        if (agent.desiredVelocity.sqrMagnitude >= 0.1f * 0.1f)
        {
            Vector3 dir = agent.desiredVelocity;
            Quaternion target = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 8);
            
        }
       
        switch (state)
        {
            case EnamyState.Idle:
                idleUpdate();
                break;
            case EnamyState.Patrol:              
                PatrolUpdate();
                break;
            case EnamyState.find:
                findUpdate();
                
                break;
            case EnamyState.Tracking:
                TrackingUpdate();
                break;
            case EnamyState.Attack:
                break;
            case EnamyState.die:
                state = EnamyState.Attack;
                anime.SetTrigger("die");
                break;

        }
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 dir = Quaternion.Euler(0, -angle * 0.5f,0) * transform.forward;


        Handles.color = new Color(1, 1, 1, 0.2f);
        Handles.DrawSolidArc(transform.position, Vector3.up, dir, angle, angleView);
    }
    void Patrolenter()
    { 
    
    }
    void findUpdate()
    {
        times += Time.deltaTime;
        if (times >= findTime)
        {
            state = EnamyState.Tracking;
        }
    }
    void TrackingUpdate()
    {
        anime.SetBool("walk", true);
        agent.SetDestination(player.transform.position);
    }
    void idleUpdate()
    {
        anime.SetBool("walk", false);
    }
    void PatrolUpdate()
    {
        agent.SetDestination(tf.position);
        anime.SetBool("walk", true);
        if (transform.position.x == patrol_Point[targe_patrol_Point].position.x && transform.position.z == patrol_Point[targe_patrol_Point].position.z)
        {
            a += 1;
            if (patrol_Point.Count-1 > targe_patrol_Point)
            {
                targe_patrol_Point += 1;
                
            }
            if (patrol_Point.Count  == a)
            {
                state = EnamyState.Idle;
            }
            tf = patrol_Point[targe_patrol_Point];
        }
        
       
    }
    void GameUpdate()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= angleView)
        {
            Vector3 dir = player.transform.position - transform.position;
            if (Vector3.Angle(dir, transform.forward) < +angle * 0.5f)
            {
                Debug.DrawRay(transform.position, dir*angleView, Color.green);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, dir,out hit, angleView))
                {

                    if (hit.transform.gameObject.tag == "Player"&&look)
                    {
                        look = false;
                        state = EnamyState.find;
                    }
                    
                }

               
            }
        }
        else 
        {
            if (state == EnamyState.Tracking)
            {
                state = EnamyState.Patrol;
            }
           
        }
    }
    void changeState(EnamyState state)
    { 
    
    }
    void die()
    {
        state = EnamyState.die;
        anime.SetTrigger("die");
    }
}
