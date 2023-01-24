using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //카메라 위치는 차 위치랑 항상 같아야함
    [SerializeField] GameObject thingToFollow;
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        transform.position = thingToFollow.transform.position + new Vector3(0,0,-10);
    }
}
