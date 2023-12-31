using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 40; //the # of clouds to make
    public GameObject cloudPrefab; //the prefab for the clouds
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1; //Min scale of each cloud
    public float cloudScaleMax = 3; //Max scale of each cloud
    public float cloudSpeedMult = 0.5f; //Adjusts speed of clouds
    public GameObject[] cloudInstances;

    void Awake()
    {
        cloudInstances = new GameObject[numClouds];
        GameObject anchor = GameObject.Find("CloudAnchor");
        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab);
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            cPos.z = 100 - 90 * scaleU;
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            cloud.transform.SetParent(anchor.transform);
            cloudInstances[i] = cloud;
        }
    }

    void Update()
    {
        //Iterate over each cloud that was created
        foreach (GameObject cloud in cloudInstances)
        {
            //Get the cloud scale and position
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            //Move larger clouds faster
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            //If a cloud has moved too far to the left...
            if (cPos.x <= cloudPosMin.x)
            {
                //Move it to the far right
                cPos.x = cloudPosMax.x;
            }
            //Apply the new position to cloud
            cloud.transform.position = cPos;
        }
    }
}