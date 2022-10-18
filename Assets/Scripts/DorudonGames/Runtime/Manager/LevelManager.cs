using DorudonGames.Runtime.Misc;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DorudonGames.Runtime.Manager
{
    public class LevelManager : Singleton<LevelManager>
    {

        public List<GameObject> ObjectsToBeDestruct = new List<GameObject>();


        public void AddToList(GameObject gObject)
        {
            ObjectsToBeDestruct.Add(gObject);
        }
        public void RemoveFromList(GameObject gObject)
        {
            ObjectsToBeDestruct.Remove(gObject);
        }

    }
}
