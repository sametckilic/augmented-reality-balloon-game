using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TapBalloon : MonoBehaviour
{
    public GameObject obj;
    public Transform [] spawnPos;
    public TextMeshProUGUI text;
    int count = 0;
    int layerMask = 1 << 8;
    void Start()
    {
        obj = Instantiate(obj, spawnPos[0].position, Quaternion.identity);
        CreateRandomObj();
    }
    void Update()
    {
        Vector3 hareket = new Vector3(0f, 1.5f, 0f);
        obj.GetComponent<Rigidbody>().velocity = hareket;
        if (Input.touchCount > 0)
        {
            Touch tap = Input.GetTouch(0);

            if (tap.phase == TouchPhase.Began)
            {
                Debug.Log("TIKLADIK");
                Ray ray = Camera.main.ScreenPointToRay(tap.position);
                RaycastHit hit;

                // Ray, bir collider ile çarpıştı mı kontrol et
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("VURDUK" + hit.collider.gameObject.tag + " ---------- " + obj.tag);
                    // Çarpışan collider bir 3D nesnenin collider'ı ise
                    if (hit.collider.gameObject.tag == obj.tag)
                    {
                        // Dokunma algılandığında bu mesajı yazdır         
                        text.SetText("PATLAYAN BALON : " + ++count);                
                        if(count > 20)
                        {
                            text.SetText("TEBRİKLER");
                        }
                        else
                        {
                            CreateRandomObj();
                        }
                    }                  
                }
            }
        }
        
        if(obj.transform.position.y > 7)
        {
            CreateRandomObj();
        }
    }

    public void CreateRandomObj()
    {
        System.Random rnd = new System.Random();
        int sayi = rnd.Next(5);
        obj.transform.position = spawnPos[sayi].position;
    }
}
