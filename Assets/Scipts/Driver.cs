using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//클래스는 함수 특성 모음
public class Driver : MonoBehaviour
{   //시리얼라이즈 인스팩터에서 바로 접근 가능 
    [SerializeField]float steerSpeed = 1f;
    [SerializeField] float moveSpeed =0.01f;
    [SerializeField] float slowSpeed = 15f;
    [SerializeField] float boostSpeed =30f;

    // Start is called before the first frame update call back
    void Start()
    {
        //X,Y,Z
        transform.Rotate(0,0,45); 
    }

    // Update is called once per frame
    void Update()
    {       //항상 소수점 입력이 들어갔을때는  f입력
            //하드코딩  
        
        //매프레임마다 계산
        //-1 왼쪽 1 오른쪽
        float steerAmount = Input.GetAxis("Horizontal") *steerSpeed *Time.deltaTime;  
        //속도계산
         float accelelation = Input.GetAxis("Vertical")*moveSpeed *Time.deltaTime;
         transform.Rotate(0,0,-steerAmount);
         transform.Translate(0,accelelation,0);
    }
    
 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Booster")
        {   Debug.Log("Booster On!");
            moveSpeed=boostSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        moveSpeed =slowSpeed;
    }
}
