using UnityEngine;
using System.Collections;
using System.IO;

public class RevolvingCam : MonoBehaviour
{
    public GameObject target = null;
    public bool orbitY  = false;
	public bool orbitX = false;
	public bool orbitZ = false;

    void Start(){
    }


    void Update(){
        if(target!=null){
            transform.LookAt(target.transform);
            if (orbitY){
                transform.RotateAround(target.transform.position, new Vector3(0,1,0),Time.deltaTime*50);
				}
			else if (orbitX){
				transform.RotateAround(target.transform.position, new Vector3(1,0,0),Time.deltaTime*50);
			}
			else if (orbitZ){
				transform.RotateAround(target.transform.position, new Vector3(0,0,1),Time.deltaTime*50);
			}
        }
    }






}