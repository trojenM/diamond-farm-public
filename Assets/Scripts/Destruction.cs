using System.Collections;
using DorudonGames.Runtime.Misc;
using System.Collections.Generic;
using UnityEngine;


namespace DorudonGames.Runtime.Manager
{
    public class Destruction : MonoBehaviour
    {
        [SerializeField] private float sphereRadius;
        [SerializeField] Camera cam;
        [SerializeField] private float multiplicationCoefficient;
        
        private List<Rigidbody> ObjectsToBeDestruct = new List<Rigidbody>();
        
        public List<Rigidbody> GetObjectsToBeDestruct() { return ObjectsToBeDestruct; }
        
        private void FixedUpdate()
        {
            if (Physics.SphereCast(cam.ScreenPointToRay(Input.mousePosition), sphereRadius, out RaycastHit hitInfo))
            {
                if (!ObjectsToBeDestruct.Contains(hitInfo.rigidbody))
                {
                    ObjectsToBeDestruct.Add(hitInfo.rigidbody);
                }
            }
        }

        private void Update()
        {
            
            for (int i = 0; i < ObjectsToBeDestruct.Count; i++)
            {
                //burdakileri LevelManager.Instance.ObjectsToBeDestruct[i].rb yap�nca hata verdi
                ObjectsToBeDestruct[i].isKinematic = false;
                ObjectsToBeDestruct[i].mass *= multiplicationCoefficient;
                ObjectsToBeDestruct.RemoveAt(i);
            }
        }
    }
}