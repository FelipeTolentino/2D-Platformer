using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    [SerializeField] bool showGroudDetector;
    [SerializeField] Transform groundDetector;
    [SerializeField] LayerMask platformsLayer;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] float walkSpeed;
    [SerializeField] bool isPatroller = true;
    [SerializeField] float ReturnPatrolTime;

    //Distância para o player em que o inimigo tenta se aproximar do mesmo
    [SerializeField] bool showApproachDistance;
    [SerializeField] float approachDistance;
    //Distancia para o player em que o inimigo tenta atacar
    [SerializeField] bool showEngageArea;
    [SerializeField] float engageDistance;

    [SerializeField] float timeBetweenAttacks;

    //Configurações do ataque corpo-a-corpo
    [SerializeField] Transform meleeHitBox;
    [SerializeField] float hitBoxXSize, hitBoxYSize;
    [SerializeField] bool showMeleeRange;
    [SerializeField] int meleeDamage;
    [SerializeField] AudioClip meleeSound;
    [SerializeField] int animationsQuantity;

    //Configurações do ataque a distância
    [SerializeField] bool isShooter;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float bulletForce;
    [SerializeField] float throwHight;
    [SerializeField] AudioClip shotSound;

    Rigidbody2D body;
    Transform player;
    Animator animator;
    AudioSource audioSrc;
    EnemyHealth enemyHealth;

    int direction = 1; // positivo (1) para direita | negativo (-1) para esquerda
    float groudDetectionCircleRadio = 0.1f; //Tamanho do círculo de overlap usado para detectar o chão
    float distanceToPlayer;
    bool mustPatrol = true;
    //bool mustTurn;
    bool wallAhead;
    bool canAttack = true;
    bool waiting;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
        enemyHealth = GetComponent<EnemyHealth>();
    }


    void Update()
    {
        if (Time.timeScale == 0)
            audioSrc.mute = true;
        else
            audioSrc.mute = false;

        //Desativa o inimigo
        if (enemyHealth.Died) return;

        // Faz o inimigo entrar em modo patrulha
        if (mustPatrol && isPatroller)
        {
            Patrol();
            animator.SetBool("Running", true);
        }

        distanceToPlayer = Vector2.Distance(transform.position, player.position);


        // Quando o inimigo entra no "campo de visão"
        if (distanceToPlayer <= approachDistance)
        {
            mustPatrol = false;
            body.velocity = Vector2.zero;
            animator.SetBool("Running", false);

            // Vira o inimigo para o player
            if (player.position.x > transform.position.x && transform.localScale.x < 0 ||
            player.position.x < transform.position.x && transform.localScale.x > 0)
                Turn();

            // Não persegue o player se ele estiver no raio
            // de aproximação e atrás de uma parede/em cima/abaixo
            if (!wallAhead)
                GoToPlayer();
            else
            {
                body.velocity = Vector2.zero;
                animator.SetBool("Running", false);
                StartCoroutine(WaitToPatrolAgain());
            }
                

            if (distanceToPlayer <= engageDistance)
            {
                animator.SetBool("Running", false);
                if (canAttack)
                {
                    body.velocity = Vector2.zero;
                    StartCoroutine(Attack());

                }
            }

        }
        
        // Caso o player não esteja no "campo de visão"
        // mas tenha estado a pouco tempo (mustPatrol setado
        // como false pelo if acima), espera um tempo (para 
        // o player poder escapar) e volta a patrulhar.
        else if (!mustPatrol)
        {
            animator.SetBool("Running", false);
            StartCoroutine(WaitToPatrolAgain());
        }
    }


    void GoToPlayer()
        // Avança em direção ao player até chegar
        // a distância de engage
        // -- como o inimigo já é virado em direção ao player
        // antes da chamada dessa função, é dado a ele apenas o movimento
    {
        animator.SetBool("Running", true);
        if (distanceToPlayer > engageDistance)
            body.velocity = new Vector2(direction * walkSpeed * Time.fixedDeltaTime, body.velocity.y);
    }

    void Patrol()
        // Da movimento ao inimigo e o vira quando não
        // é encontrado mais chão
    {
        if (wallAhead)
            Turn();

        body.velocity = new Vector2(direction * walkSpeed * Time.fixedDeltaTime, body.velocity.y);
    }

    void Turn()
        // Inverte a escala e a direção o movimento
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        direction *= -1;
    }

    void FixedUpdate()
        // Checa se um colisor está encostando em alguma plataforma
        // se estiver, sinaliza para virar o inimigo
    {
        if (isPatroller && !enemyHealth.Died)
            wallAhead  = !Physics2D.OverlapCircle(groundDetector.position, groudDetectionCircleRadio, platformsLayer);
    }

    IEnumerator WaitToPatrolAgain()
        // Aguarda um tempo, decide aleatoriamente
        // se irá virar e volta ao patrulhamento
    {
        if (!waiting)
        {
            waiting = true;
            yield return new WaitForSeconds(ReturnPatrolTime);

            int whichSide = Random.Range(1, 101);
            if (whichSide <= 50)
            {
                Turn();
            }

            mustPatrol = true;
            waiting = false;
        }  
    }

    IEnumerator Attack()
        // Ataca e inicia o cooldown para o próximo
        // ataque.
        // -- As funções de ataque são chamadas através de eventos de animação
        // -- para ter o timing certo
    {
        if (isShooter)
            animator.SetTrigger("Throw");
        else
        {
            // -- AS ANIMAÇÕES DE ATAQUE COROPO-A-CORPO tem que ser nomeadas
            // -- "MeleeAttack_numero-sequencial".
            // Sorteia uma das animações de ataque e a realiza
            string animation = "MeleeAttack_" + Random.Range(1, animationsQuantity + 1); // descorbir pq Range n está Inclusivo
            animator.SetTrigger(animation);
            
            audioSrc.clip = meleeSound;
            audioSrc.Play();
        }
        
        canAttack = false;
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }

    void shotBullet()
        // Instancia um objeto a partir do prefab de projétil
        // definido no inspector e dá a ele uma velocidade
        // na direção do player
        // -- A detecção de acerto e o dano ficam no script
        // do projétil
    {
        audioSrc.clip = shotSound;
        audioSrc.Play();
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Vector2 playerDirection = player.transform.position - bullet.transform.position;

        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(playerDirection.x, playerDirection.y + throwHight);
    }

    void MeleeAttack()
        // Cria uma caixa de colisão momentânea que 
        //verifica apenas a camada do player
        // -- Tamanho da caixa é definido pelo inspector
    {
        Collider2D coll = Physics2D.OverlapBox(meleeHitBox.position, new Vector2(hitBoxXSize, hitBoxYSize), 0, playerLayer);
        if (coll != null)
            coll.gameObject.GetComponent<PlayerHealth>().takeDamage(meleeDamage);
    }

    private void OnDrawGizmos()
    {
        if (showApproachDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, approachDistance);
        }
        if (showEngageArea)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, engageDistance);
        }

        if (showGroudDetector)
        {

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(groundDetector.position, groudDetectionCircleRadio);
        }

        if (showMeleeRange)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(meleeHitBox.position, new Vector3(hitBoxXSize, hitBoxYSize, 0));
        }
    }

}
