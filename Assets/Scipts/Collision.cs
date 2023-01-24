using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{   
    bool hasPackage =false;
    [SerializeField] float destroyDelay= 0.5f;
    [SerializeField] Color32 hasPackageColor= new Color32(255,68,1,1);
    [SerializeField] Color32 noPackageColor= new Color32(0,211,253,1);

    SpriteRenderer spriteRenderer;


    private void Start()
    {
     spriteRenderer= GetComponent<SpriteRenderer>();
    }
    
    
    //충동 작용
   void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("아우 아퍼");
    }

   
    void OnTriggerEnter2D(Collider2D other)
    {   //만약 트리거가 패키지면 
        if(other.tag =="Package" && !hasPackage)
        {
            Debug.Log("Package picked up ");
            hasPackage= true;
            spriteRenderer.color =hasPackageColor;
            Destroy(other.gameObject, destroyDelay);
        }

        if(other.tag =="Client" && hasPackage)
        {
            Debug.Log("Package delivered ");
            spriteRenderer.color =noPackageColor;
            hasPackage =false;
        }
    }
}
