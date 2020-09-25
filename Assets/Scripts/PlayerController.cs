using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Game_Logic spawner;
    [SerializeField] HitPoints hp;
    [Space]
    [Header("Character attributes")]
    public float MOVEMENT_BASE_SPEED = 5f;
    public float CROSSHAIR_DISTANCE= 1.0f;
    public float BULLET_OFFSET = 0.5f;
    public float GUN_DISTANCE = 0.5f;
    public float AIMING_BASE_PENALTY;
    public float SHOOTING_RECOIL_TIME = 0.2f;
    [Space]
    [Header("Character Stats")]
    public Vector2 movementDirection;
    public float runSpeed = 5;
    public float shootingRecoil;
    public bool endOfAiming;
    public bool isAiming;
    public bool lockPosition;
    public int attack_damage = 3;

    [Space]
    [Header("References")]
    public Rigidbody2D _rb;
    public Animator animator;

    [Space]
    [Header("Prefab")]
    public GameObject crosshair;
    public GameObject arrowPrefab;

    private void Start()
    {
        hp.OnHealthDepleted += OnHealthDepleted;
        spawner = FindObjectOfType<Game_Logic>();
    }
    void Update()
    {
        ProcessInputs();
        Move();
        Animate();
        Aim();
        Shoot();
    }

    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        runSpeed = Mathf.Clamp(movementDirection.magnitude, 0f, 1f);
        movementDirection.Normalize();

        endOfAiming = Input.GetButtonUp("Fire1");
        isAiming = Input.GetButton("Fire1");
        lockPosition = Input.GetButton("LockPosition");
        if (lockPosition)
        {
            runSpeed = 0.0f;
        }
        if (isAiming || shootingRecoil > 0.0f)
        {
            runSpeed *= AIMING_BASE_PENALTY;
        }
        if (endOfAiming)
        {
            shootingRecoil = SHOOTING_RECOIL_TIME;
        }
        if (shootingRecoil > 0.0f)
        {
            shootingRecoil -= Time.deltaTime;
        }
    }
    void Move()
    {
        _rb.velocity = movementDirection * runSpeed* MOVEMENT_BASE_SPEED;
    }
    void Animate()
    {
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
    }
    void Aim()
    {
        if (movementDirection!= Vector2.zero)
        {
            crosshair.transform.localPosition = movementDirection * CROSSHAIR_DISTANCE;
        }
    }

    void Shoot()
    {
        Vector2 shootingDricetion = crosshair.transform.localPosition;

        if (endOfAiming)
        {
            var bullet =  Instantiate(arrowPrefab,transform.position + new Vector3(BULLET_OFFSET * shootingDricetion.x, BULLET_OFFSET * shootingDricetion.y, 0f),Quaternion.identity).GetComponent<Bullet>();
            if (bullet)
            {
                bullet.Fire(shootingDricetion, attack_damage);
            }

        }
    }
    private void OnHealthDepleted()
    {
        FindObjectOfType<GameManager>().EndGame();
        spawner.Spawn(gameObject);
        //hp.OnHealthDepleted -= OnHealthDepleted;
    }
}
