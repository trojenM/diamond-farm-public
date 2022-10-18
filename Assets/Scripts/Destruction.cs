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
       
        /*
        Rigidbody rb;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        */

        private void FixedUpdate()
        {
            if (Physics.SphereCast(cam.ScreenPointToRay(Input.mousePosition), sphereRadius, out RaycastHit hitInfo))
            {
                if (!LevelManager.Instance.ObjectsToBeDestruct.Contains(hitInfo.collider.gameObject))
                {
                    LevelManager.Instance.AddToList(hitInfo.collider.gameObject);
                }
            }
        }

        private void Update()
        {
            
            for (int i = 0; i < LevelManager.Instance.ObjectsToBeDestruct.Count; i++)
            {
                //burdakileri LevelManager.Instance.ObjectsToBeDestruct[i].rb yapýnca hata verdi
                LevelManager.Instance.ObjectsToBeDestruct[i].GetComponent<Rigidbody>().isKinematic = false;
                LevelManager.Instance.ObjectsToBeDestruct[i].GetComponent<Rigidbody>().mass *=multiplicationCoefficient;
                LevelManager.Instance.RemoveFromList(LevelManager.Instance.ObjectsToBeDestruct[i]);
            }
        }

    }
}
