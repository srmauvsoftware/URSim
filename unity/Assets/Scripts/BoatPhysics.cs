using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BoatTutorial
{
    public class BoatPhysics : MonoBehaviour
    {
        //Drags
        public GameObject underWaterObj;

        //Script that's doing everything needed with the boat mesh, such as finding out which part is above the water
        private ModifyBoatMesh modifyBoatMesh;

        //Mesh for debugging
        private Mesh underWaterMesh;

        //The boats rigidbody
        private Rigidbody boatRB;

        //The density of the water the boat is traveling in
        private float rhoWater = 1027f;

        void Start()
        {
            //Get the boat's rigidbody
            boatRB = gameObject.GetComponent<Rigidbody>();

            //Init the script that will modify the boat mesh
            modifyBoatMesh = new ModifyBoatMesh(gameObject);

            //Meshes that are below and above the water
            underWaterMesh = underWaterObj.GetComponent<MeshFilter>().mesh;
        }

        void Update()
        {
            //Generate the under water mesh
            modifyBoatMesh.GenerateUnderwaterMesh();

            //Display the under water mesh
            // modifyBoatMesh.DisplayMesh(underWaterMesh, "UnderWater Mesh", modifyBoatMesh.underWaterTriangleData);
        }

        void FixedUpdate()
        {
            //Add forces to the part of the boat that's below the water
            if (modifyBoatMesh.underWaterTriangleData.Count > 0)
            {
                AddUnderWaterForces();
            }
        }

        //Add all forces that act on the squares below the water
        void AddUnderWaterForces()
        {
            //Get all triangles
            List<TriangleData> underWaterTriangleData = modifyBoatMesh.underWaterTriangleData;

            for (int i = 0; i < underWaterTriangleData.Count; i++)
            {
                //This triangle
                TriangleData triangleData = underWaterTriangleData[i];

                //Calculate the buoyancy force
                Vector3 buoyancyForce = BuoyancyForce(rhoWater, triangleData);

                //Add the force to the boat
                boatRB.AddForceAtPosition(buoyancyForce, triangleData.center);


                //Debug

                //Normal
                Debug.DrawRay(triangleData.center, triangleData.normal * 3f, Color.white);

                //Buoyancy
                Debug.DrawRay(triangleData.center, buoyancyForce.normalized * -3f, Color.blue);
            }
        }

        //The buoyancy force so the boat can float
        private Vector3 BuoyancyForce(float rho, TriangleData triangleData)
        {
            //Buoyancy is a hydrostatic force - it's there even if the water isn't flowing or if the boat stays still

            // F_buoyancy = rho * g * V
            // rho - density of the mediaum you are in
            // g - gravity
            // V - volume of fluid directly above the curved surface 

            // V = z * S * n 
            // z - distance to surface
            // S - surface area
            // n - normal to the surface
            Vector3 buoyancyForce = rho * Physics.gravity.y * triangleData.distanceToSurface * triangleData.area * triangleData.normal / (boatRB.transform.localScale.x * boatRB.transform.localScale.y * boatRB.transform.localScale.z);

            //The vertical component of the hydrostatic forces don't cancel out but the horizontal do
            buoyancyForce.x = 0f;
            buoyancyForce.z = 0f;

            return buoyancyForce;
        }
    }
}
