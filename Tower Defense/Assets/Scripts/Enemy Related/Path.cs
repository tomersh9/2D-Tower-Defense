using System.Collections.Generic;
using UnityEngine;

namespace Enemy_Related
{
    public class Path : MonoBehaviour
    {
        //build and return List of points from Path Prefab
        public List<Transform> GetPath()
        {
            List<Transform> waypoints = new List<Transform>();
            foreach (Transform point in transform)
            {
                waypoints.Add(point);
            }
            return waypoints;
        }
    }
}
