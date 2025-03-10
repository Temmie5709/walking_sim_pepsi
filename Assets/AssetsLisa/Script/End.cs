using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    int taskVita = 0;
    int taskPlay = 0;
    void DoVITATask()
    {
        taskVita++;
    }
    void DoPlayerTask()
    {
        taskPlay++;
    }

    // Update is called once per frame
    void Update()
    {
        if (taskPlay == 12)
        {

        }
    }
}
