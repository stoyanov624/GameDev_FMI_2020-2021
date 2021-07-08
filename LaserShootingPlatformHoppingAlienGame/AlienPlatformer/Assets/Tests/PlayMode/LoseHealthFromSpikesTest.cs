using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LoseHealthFromSpikesTest
    {
        [UnityTest]
        public IEnumerator LoseHealthTestWithEnumeratorPasses()
        {
            GameObject spikes = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Element Prefabs/Spikes") as GameObject, new Vector3(0,-1,0), Quaternion.identity);

            GameObject player = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Player") as GameObject, new Vector3(0,2,0), Quaternion.identity);

            yield return new WaitForSeconds(0.1f); // waiting for everything to spawn;
            Health hp = player.GetComponent<Health>();
            
            int hpOnSpawn = hp.RemainingLives;

            yield return new WaitForSeconds(0.5f);

            int hpOnImpact = hp.RemainingLives;

            GameObject.Destroy(spikes);
            GameObject.Destroy(player);
            
            Assert.That(hpOnSpawn > hpOnImpact);
        }
    }
}
