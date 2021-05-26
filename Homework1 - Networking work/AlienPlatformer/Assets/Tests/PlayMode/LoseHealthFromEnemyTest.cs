using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LoseHealthFromEnemyTest
    {
        [UnityTest]
        public IEnumerator LoseHealthFromEnemyTestWithEnumeratorPasses()
        {
            GameObject platform = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Platform Prefabs/PlatformLarge") as GameObject, new Vector3(0,0,0), Quaternion.identity);

            GameObject player = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Player") as GameObject, new Vector3(0.5f, 0.6f, 0), Quaternion.identity);

            yield return new WaitForSeconds(0.1f); 
            Health hp = player.GetComponent<Health>();
            int hpOnSpawn = hp.RemainingLives;

            
            GameObject enemy = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Enemy") as GameObject, new Vector3(-0.6f, 0.6f, 0), Quaternion.identity);
            enemy.GetComponent<EnemyScript>().setTarget(player.transform);

            yield return new WaitForSeconds(0.5f); 
            int hpOnImpact = hp.RemainingLives;

            GameObject.Destroy(player);
            GameObject.Destroy(enemy);
            GameObject.Destroy(platform);

            Assert.That(hpOnSpawn > hpOnImpact);
        }
    }
}
