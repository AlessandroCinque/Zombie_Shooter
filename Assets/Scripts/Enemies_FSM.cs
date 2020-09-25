using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering.Behaviours
{
    public class Enemies_FSM : SteeringBehaviour
    {
        public enum EnemyType { Chaser, Shooter }
        public EnemyType currentEnemyType;
        bool alive = true;
        [SerializeField] private Seek _seekBehaviour;
        [SerializeField] HitPoints hp;
        

        [Space]
        [Header("Character attributes")]
        public float BULLET_OFFSET = 0.5f;
        public float attack_slowdown = 0.0f;
        public float attack_ratio = 2.0f;
        public float attack_range = 5.0f;
        public int attack_damage = 3;

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
            _currState = Seek();
            StartCoroutine(FSM());
           
        }
        private void Reset()
        {
            var maxHP = hp.GetMaxHP();
            hp.AdjustHealth(maxHP);
        }


        private void OnHealthDepleted()
        {
            alive = false;
            Destroy(gameObject);
            //throw new NotImplementedException();
        }

        private IEnumerator Seek()
        {
            attack_slowdown -= Time.deltaTime;

            var player = GameObject.FindObjectOfType<PlayerController>();
            var target = player ? player.transform : null;
            _seekBehaviour.Target = target;
            while (_nextState == null)
            {
                float distance = Vector2.Distance(target.position, transform.position);
                if (distance <= attack_range && currentEnemyType == EnemyType.Shooter)
                {
                    _nextState = Shoot(target);
                }
                else if (distance <= attack_range && currentEnemyType == EnemyType.Chaser)
                {
                    _nextState = Melee_Attack(target);
                }
                //kind of a temporary "break", a pause button
                yield return null;
            }
        }
        private IEnumerator Shoot(Transform target)
        {

            _seekBehaviour.Target = null;
            while (_nextState == null)
            {
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
        private IEnumerator Melee_Attack(Transform target)
        {

            _seekBehaviour.Target = null;
            while (_nextState == null)
            {
                float distance = Vector2.Distance(target.position, transform.position);
                var hitPoints = target.gameObject.GetComponent<HitPoints>();
                hitPoints.AdjustHealth(-attack_damage);
                yield return new WaitForSeconds(1f);
                if (distance > attack_range)
                {
                    _nextState = Seek();
                }

            }
        }


        public override Vector2 SteeringForce
        {
            get => _seekBehaviour.SteeringForce;
        }

        public bool IsAlive()
        {
            return alive;
        }
    }
}
