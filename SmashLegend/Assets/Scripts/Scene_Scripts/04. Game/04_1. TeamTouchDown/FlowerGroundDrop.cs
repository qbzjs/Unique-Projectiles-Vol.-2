using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGroundDrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity += new Vector3(0.0f, -0.5f, 0.0f);

        if(transform.position.y <= -50.0f)
        {
            Destroy(this.gameObject);
        }
    }


}
