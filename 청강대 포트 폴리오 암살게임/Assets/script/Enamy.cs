using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEditor;
[System.Serializable]
public class Enemy_Patrol_Point
{
    public Transform patrol_point;   // 이위치가 마지막인지 확인
    public bool idle_Searching;// 이위치에서 잠깐 멈춰서 수색(좌우보기) 할건지
    public float idle_time;
}
public enum EnamyState
{ 
Idle,Patrol,Tracking,Attack,find,die
}
public class Enamy : MonoBehaviour
{
    [SerializeField] float angle;// 시야각
    [SerializeField] float angleView;// 시야거리
    [SerializeField] float attack_rang;// 공격 거리
    [SerializeField] EnamyState state;// 현재 상태
    [SerializeField] GameObject player;// 플레이어의 위치 파악을위한 변수
    [SerializeField] float findTime;// 상태가 find일때 몇초뒤에 대상을 인식해서 추적할건지
    [SerializeField] float times;// 현제시간
    [SerializeField] CapsuleCollider colider;// 자신의 콜라이더를 받아옴
    public int targe_patrol_Point=0;// 현제 patrol_Points의 인덱스값
    public int a=0;// 나중에 다시 만들기
    public Transform patrol_Point;// 현제patrol_Points의 트렌스폼
    public List<Enemy_Patrol_Point> patrol_Points; // patrol_Points를 리스트로 받아옴
    NavMeshAgent agent;
    Animator anime;
    AudioSource audio; 
    bool look=true;
   
   
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        anime = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
        agent.updateRotation = false;
        changeState(EnamyState.Patrol);

        patrol_Point = patrol_Points[0].patrol_point;
       
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
        switch (state)// 각상태의 Update 함수를 실행
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
                AttackUpdate();
                break;
            case EnamyState.die:
                dieUpdate();
                break;
                
        }
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 dir = Quaternion.Euler(0, -angle * 0.5f,0) * transform.forward;
        Handles.color = new Color(1, 1, 1, 0.2f);
        Handles.DrawSolidArc(transform.position, Vector3.up, dir, angle, angleView);
        Gizmos.DrawWireSphere(transform.position, attack_rang);
    }
    void idleEnter()// idle 상태를 시작할  때실행되는 함수
    {
        agent.speed = 0;
        times = 0;
        audio.Stop();
        anime.SetBool("walk", false);
    }
    void PatrolEnter()// Patrol 상태를 시작할  때실행되는 함수
    {
        agent.speed = 1.5f;
        times = 0;
        audio.Play();
        anime.SetBool("walk", true);
    }
    void TrackingEnter()// Tracking 상태를 시작할  때실행되는 함수
    {
        audio.Play();
        times = 0;
        anime.SetBool("walk", true);
    }
    void AttackEnter()//  Attack 상태를 시작할  때실행되는 함수
    {
        times = 0;
        transform.LookAt(new Vector3(player.transform.position.x
                                     ,transform.position.y
                                     ,player.transform.position.y));
        agent.speed = 0;
        anime.SetTrigger("attack");
    }

    #region Update
    void findUpdate()//find 상태가 매프레임 실행되는 함수
    {
        times += Time.deltaTime;
        if (times >= findTime)
        {
            times = 0;
            changeState(EnamyState.Tracking);
            
        }
    }
    void TrackingUpdate()//Tracking 상태가 매프레임 실행되는 함수
    {
       
        agent.SetDestination(player.transform.position);
        attack_rang_on();
    }
    void idleUpdate()//idle 상태가 매프레임 실행되는 함수
    {
        
        if (patrol_Points[a-1].idle_Searching == true)
        {
            times += Time.deltaTime;

            if (times >= patrol_Points[a-1].idle_time)
            {
                changeState(EnamyState.Patrol);
                
                times = 0;

            }
        }
    }
    void PatrolUpdate()//Patrol 상태가 매프레임 실행되는 함수
    {
       
        if (patrol_Points.Count <= a)
        {
            a = 0;
            targe_patrol_Point = 0;
        }

        agent.SetDestination(patrol_Point.position);
      
        if (transform.position.x == patrol_Points[targe_patrol_Point].patrol_point.position.x 
            && transform.position.z == patrol_Points[targe_patrol_Point].patrol_point.position.z)
        {
            a += 1;
            if (patrol_Points.Count-1 > targe_patrol_Point)
            {
                targe_patrol_Point += 1;
                Debug.Log(targe_patrol_Point);
            }                     
            if (patrol_Points[a-1].idle_Searching == true)
            {
                changeState(EnamyState.Idle);
              

                return;
            }                     
            
        }
        patrol_Point = patrol_Points[targe_patrol_Point].patrol_point;

    }
    public void dieUpdate()//die 상태가 매프레임 실행되는 함수
    {
        agent.speed = 0;
        audio.Stop();
        
        anime.SetTrigger("die");
        this.enabled = false;
    }
    void AttackUpdate()//Attack 상태가 매프레임 실행되는 함수
    {
       
        if (!anime.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            player.GetComponent<Player>().die = true;
            changeState(EnamyState.Idle);
            
        }
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            agent.speed = 0f;

        }
    }
    #endregion

    public void changeState(EnamyState _state)// 상태를 바꿀때쓰는 함수
    {
        state = _state;//상태를 변경
        switch (state)//여기서 각상태의 Enter함수를 실행
        {
            case EnamyState.Idle:
                idleEnter();
                break;
            case EnamyState.Patrol:
                PatrolEnter();
                break;
            case EnamyState.Tracking:
                TrackingEnter();
                break;
            case EnamyState.Attack:
                AttackEnter();
                break;
        }
        
    }

    void GameUpdate()// 계속해서 실행해야하는 공통된것 
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= angleView)
        {
            Vector3 dir = player.transform.position - transform.position;
            if (Vector3.Angle(dir, transform.forward) < +angle * 0.5f)
            {
                Debug.DrawRay(transform.position, dir * angleView, Color.green);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, dir, out hit, angleView))
                {

                    if (hit.transform.gameObject.tag == "Player" && look)
                    {
                        look = false;
                        changeState(EnamyState.find);
                    }
                    if (hit.transform.gameObject.tag != "Player"&& state!=EnamyState.Patrol)//여기서 버그남
                    {
                        look = true;
                        changeState(EnamyState.Patrol);
                    }
                }


            }
        }
        else
        {
            if (state == EnamyState.Tracking)
            {
                changeState(EnamyState.Patrol);
                
            }

        }
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            agent.speed = 1.5f;
        }
        if (!anime.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            agent.speed = 0f;
        }
    }
   
    public void  animedie()// 사망시 애니메이션 이벤트에서 실행하는 함수
    {
        colider.center = new Vector3(0, 1, 1);
    }
    void attack_rang_on()// Tracking상태일 때 일정범위에 들어오면 공격하는 함수
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= attack_rang)
        {
            changeState(EnamyState.Attack);
            
        }
    }
   
}
