using NUnit.Framework;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Platform_Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject platform;
    private Game_Manager gm;

    

    private int currentScore; 
    
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;


    [SerializeField]
    private List<GameObject> platformsList;
    private int listCount;
    [SerializeField]
    private bool isGenerating = false;



    void Start()
    {
        gm = gameObject.GetComponentInParent<Game_Manager>();
        listCount = platformsList.Count;

        for (int i = 0; i < platformsList.Count; i++) 
        {
            if (platformsList[i] != null) continue;
            else if (platformsList[i] == null)
            {
                float startPos = platformsList[i - 1].transform.position.y;
                GameObject tmp = GeneratePlatform(startPos, i);
                platformsList[i] = tmp;
                tmp.SetActive(true);

            }
        }
        
    }

    private void Update()
    {

        currentScore = gm.GetCurrentScore();
        if(currentScore > (int)(listCount/ 2))
        {
            isGenerating = true;
            
        }

        if (isGenerating)
        {
            GameObject lastPlatform = platformsList[listCount - 1];
            float lastHeight = lastPlatform.transform.position.y;
            int lastIndex = lastPlatform.GetComponent<Platform_Controller>().index +1;
            if (currentScore > lastIndex - 5) {
                GameObject temp = GeneratePlatform(lastHeight, lastIndex);
                temp.SetActive(true);
                Destroy(platformsList[0]);
                platformsList.Remove(platformsList[0]);
                platformsList.Add(temp);

            }
        }
    }
    private float3 Randomizer()
    {
        float height = UnityEngine.Random.Range(minHeight, maxHeight);
        float scale = UnityEngine.Random.Range(minScale, maxScale);
        float speed = UnityEngine.Random.Range(maxSpeed, minSpeed);

        return new float3 (height, scale, speed);
    }
    private GameObject GeneratePlatform(float startHeight, int index)
    {
        float3 mods = Randomizer(); // height, scale, speed in that order
        Vector3 pos = Vector3.zero;
        pos.y = startHeight + mods.x;
        
        //Randomize the prefabs
        GameObject temp = Instantiate(platform, pos, Quaternion.identity);
        temp.SetActive(false);
        temp.transform.parent = transform.parent;
        temp.transform.localScale = new Vector3(mods.y, 1.0f, 1.0f);
        temp.GetComponent<Platform_Controller>().modifySpeed(mods.z);
        temp.GetComponent<Platform_Controller>().setIndex(index);
        return temp;
       
    }
}
