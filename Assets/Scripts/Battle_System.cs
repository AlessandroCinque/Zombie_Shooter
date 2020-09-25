using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Steering.Behaviours
{
    public class Battle_System : MonoBehaviour
    {

        [SerializeField] private Wave[] waves;
        
        bool inBattle = false;
        private void Start()
        {
            StartBattle();
        }
        private void StartBattle()
        {
            inBattle = true;
            //enemyTransform
        }

        public void Update()
        {
            foreach (Wave wave in waves)
            {
                wave.Update();
            }
        }




        [System.Serializable]
        private class Wave
        {
            [SerializeField] Transform[] spawnerLocations;
            [SerializeField] private GameObject[] enemyToSpawn;
            [SerializeField] private float timer;

            public void Update()
            {

                if (timer >= 0.0f)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0.0f)
                    {
                    
                        SpawnEnemies();
                    }
                }


            }
            public void SpawnEnemies()
            {
                foreach (GameObject enemy in enemyToSpawn)
                {
                    
                    int where_to_spawn = Random.Range(0, spawnerLocations.Length);
                    // make stuff spawn
                    Instantiate(enemy,spawnerLocations[where_to_spawn].position, Quaternion.Euler(0, 0, 0));
                }
            }
        }
    }
}
