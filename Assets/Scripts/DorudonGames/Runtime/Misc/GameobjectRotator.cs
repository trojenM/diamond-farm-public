using System;
using UnityEngine;

namespace DorudonGames.Runtime.Misc
{
    public class GameobjectRotator : MonoBehaviour
    {
        [SerializeField] private Vector3 rotateSpeed;
        private Transform tr;

        private void Awake()
        {
            tr = GetComponent<Transform>();
        }
        
        void Update()
        {
            tr.Rotate(rotateSpeed * Time.deltaTime);    
        }
    }
}
