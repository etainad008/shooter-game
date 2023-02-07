using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCrosshair : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float scaleFactor = 1.2f;
    [SerializeField] private GameObject crosshair;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 40f, layerMask))
        {
            Debug.Log("Hey");
            crosshair.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            //crosshair.transform.GetChild(0).gameObject.GetComponent<Image> = new Color32(255, 255, 225, 100);
        } 
        else
        {
            crosshair.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
