using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

namespace TheGame
{
    using UnityEngine;
    using Unity.Jobs;
    using Unity.Collections;
    /*
    public struct DisableRigidbodyJob : ijobpa
    {
        public NativeArray<Rigidbody> rigidbodies;
        [ReadOnly] public NativeArray<GameObject> gameObjects;
        public void Execute(int index)
        {
            // Disable the Rigidbody
            if (rigidbodies[index] != null)
            {
                rigidbodies[index].isKinematic = true;
            }
        }
    }
    //*/
}