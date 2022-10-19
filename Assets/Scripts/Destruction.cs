using System.Collections;
using DorudonGames.Runtime.Misc;
using System.Collections.Generic;
using UnityEngine;


namespace DorudonGames.Runtime.Manager
{
    public class Destruction : Singleton<Destruction>
    {

        [SerializeField] private float multiplicationCoefficient;
        public List<Rigidbody> ObjectsToBeDestruct = new List<Rigidbody>();
     
        private void Update()
        {
            
            for (int i = 0; i < ObjectsToBeDestruct.Count; i++)
            {
                ObjectsToBeDestruct[i].isKinematic = false;
                ObjectsToBeDestruct[i].mass *= multiplicationCoefficient;
                ObjectsToBeDestruct.RemoveAt(i);
            }
        }
    }
}