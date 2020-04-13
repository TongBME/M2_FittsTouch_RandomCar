using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCircle : MonoBehaviour {

    // Use this for initialization

    public static GameObject aimObject;
    public static Vector2 screenObjPos;

    void Start()
    {
        aimObject = GameObject.Find("objcircle");

        aimObject.SetActive(false);
    }

    public static void RandomCircle(int taskNum)
    {
        float offset = SetYaml.circleSize[taskNum];
        float randomPos = SetYaml.randPos[taskNum];

        float randomPosX = randomPos;
        float randomPosY = randomPos - 0.6f;
        //color
        aimObject.GetComponent<SpriteRenderer>().color = Color.grey;
        aimObject.GetComponent<Renderer>().enabled = true; // circle shows
        aimObject.transform.SetAsFirstSibling();
        aimObject.SetActive(true);
        //postion
        aimObject.transform.position = new Vector2(randomPosX, randomPosY);
        // size
        Vector3 localscale = aimObject.transform.localScale;
        Vector3 localScale = new Vector3(localscale.x * offset, localscale.y * offset, localscale.z * offset);
        aimObject.transform.localScale = localScale;

        //Vector2 circleSize = Camera.main.ScreenToWorldPoint(new Vector2(localScale.x, localScale.y));
        //print(circleSize);

        //Vector3 pos = aimObject.transform.position;
        //Vector3 a = aimObject.GetComponent<Collider>().bounds.size;
        //Vector3 c = aimObject.GetComponent<Renderer>().bounds.size;
        //Vector2 object_size = Camera.main.ScreenToWorldPoint(new Vector2(a.x, a.y));
        //print(object_size);
    }

    public static void ChangeCircleColor(int taskNum)
    {
        switch (taskNum % 4)
        {
            case 0:
                aimObject.GetComponent<SpriteRenderer>().color = Color.blue;
                //aimObject.transform.SetAsLastSibling();
                break;
            case 1:
                aimObject.GetComponent<SpriteRenderer>().color = Color.red;
                //aimObject.transform.SetAsLastSibling();
                break;
            case 2:
                aimObject.GetComponent<SpriteRenderer>().color = Color.green;
                //aimObject.transform.SetAsLastSibling();
                break;
            case 3:
                aimObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                //aimObject.transform.SetAsLastSibling();
                break;
        }
    }
}
