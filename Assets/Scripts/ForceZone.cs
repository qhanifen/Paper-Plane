using UnityEngine;
using System.Collections;

namespace ForceZones
{
    [ExecuteInEditMode]
    public class ForceZone : MonoBehaviour
    {
        public float radius = 3f;
        public float height = 5f;
        public float length = 5f;
        public float width = 3f;

        public Vector3 forceDirection;

        public enum ForceZoneShape
        {
            Box,
            Cylinder
        }
        public ForceZoneShape forceZoneShape;


        // Use this for initialization
        void Start()
        {
            forceDirection = transform.forward;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
