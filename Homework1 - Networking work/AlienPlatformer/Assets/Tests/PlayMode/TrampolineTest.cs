using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TrampolineTest
    {
        [UnityTest]
        public IEnumerator TrampolineTestWithEnumeratorPasses()
        {
            GameObject trampoline = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Element Prefabs/Trampoline") as GameObject, new Vector3(0,0,0), Quaternion.identity);
            
            GameObject player = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Player") as GameObject, new Vector3(0,1,0), Quaternion.identity);

            yield return new WaitForSeconds(0.1f);
            float yPosOnSpawn = player.transform.position.y;
            yield return new WaitForSeconds(1f);
            float yPosOnImpact = player.transform.position.y;

            GameObject.Destroy(player);
            GameObject.Destroy(trampoline);

            Assert.That(yPosOnImpact > yPosOnSpawn);
        }
    }
}
