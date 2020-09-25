using System;
using System.Collections;
using UnityEngine;

namespace Steering.Behaviours
{
    public class Boss_FSM : SteeringBehaviour
    {
        [SerializeField] private Seek _seekBehaviour;
        [SerializeField] HitPoints hp;

        [Space]
        [Header("Character attributes")]
        public float BULLET_OFFSET = 0.5f;
        public float attack_range = 5.0f;
        private bool Can_be_Stun;
        private bool reloading;
        public int shotnum = 12;
        public int attack_damage = 5;

        [Space]
        [Header("Prefab")]
        public GameObject bulletPrefab;

        private IEnumerator _currState;
        private IEnumerator _nextState;

        private IEnumerator FSM()
        {
            while (_currState != null)
            {
                yield return StartCoroutine(_currState);
                _currState = _nextState;
                _nextState = null;
            }
        }

        private void Start()
        {
            hp.OnHealthDepleted += OnHealthDepleted;
            hp.OnWounded += OnWounded;
            _currState = Seek();
            StartCoroutine(FSM());
        }

        private void OnWounded()
        {
            if (Can_be_Stun)
            {
                _nextState = Stun();
            }
        }

        private void OnHealthDepleted()
        {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().Victory();

        }

        private IEnumerator Seek()
        {

            var player = GameObject.FindObjectOfType<PlayerController>();
            var target = player ? player.transform : null;
            _seekBehaviour.Target = target;
            Can_be_Stun = true;
            while (_nextState == null)
            {
                float distance = Vector2.Distance(target.position, transform.position);
                if (distance <= attack_range)
                {
                    _nextState = Shoot(target);
                }
                //kind of a temporary "break", a pause button
                yield return null;
            }
        }

        private IEnumerator Stun()
        {
            _seekBehaviour.Target = null;
            Can_be_Stun = false;
            while (_nextState == null)
            {
             
                //stun variable later
                yield return new WaitForSeconds(1f);
                _nextState = Area_Attack();
            }
        }

        private IEnumerator Shoot(Transform target)
        {

            _seekBehaviour.Target = null;
            Can_be_Stun = true;
            while (_nextState == null)
            {
                reloading = false;
                //TODO: Shooting the target
                Vector2 shootingDricetion = target.position - transform.position;
                float distance = Vector2.Distance(target.position, transform.position);
                var bullet = Instantiate(bulletPrefab, transform.position + new Vector3(BULLET_OFFSET * shootingDricetion.x, BULLET_OFFSET * shootingDricetion.y, 0f), Quaternion.identity).GetComponent<Bullet>();
                if (bullet)
                {
                    bullet.Fire(shootingDricetion, attack_damage);
                    yield return new WaitForSeconds(1f);

                }

                if (distance > attack_range)
                {
                    _nextState = Seek();
                }

            }
        }
        private IEnumerator Area_Attack()
        {
            _seekBehaviour.Target = null;
            Can_be_Stun = false;
            while (_nextState == null)
            {
                //TODO: area attack
                Vector2 shootingDricetion = new Vector3(5, 0);
                for (int i = 0; i < shotnum; i++)
                {
                    //========================================================

                    shootingDricetion = Quaternion.AngleAxis(-30, Vector2.up) * shootingDricetion;
                    shootingDricetion = Quaternion.Euler(0, 0, -30) * shootingDricetion;
                    //========================================================
                    var bullet = Instantiate(bulletPrefab, transform.position + new Vector3(shootingDricetion.x, shootingDricetion.y, 0f), Quaternion.identity).GetComponent<Bullet>();
                    bullet.transform.Rotate(new Vector3(0, 0, 30 * (i + 1)));
                    if (bullet)
                    {

                        bullet.Fire(shootingDricetion, attack_damage);
                    }
                }

                    yield return new WaitForSeconds(1f);
                _nextState = Seek();
            }
        }

        public override Vector2 SteeringForce
        {
            get => _seekBehaviour.SteeringForce;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            //if (other.gameObject.GetComponent<Bumper>())
            //{
            //    _seekBehaviour.Target = null;
            //}
        }
    }
}
