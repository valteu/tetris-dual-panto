using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DualPantoFramework
{
    public class BorderSpawn : PantoCollider
    {
        // Start is called before the first frame update
        void Start()
        {

            CreateObstacle();
            Enable();
        }
        public override void CreateObstacle()
        {
            UpdateId();
            CreateBoxObstacle();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
