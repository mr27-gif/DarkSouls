using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIUerInput : PlayerInput
{
    IEnumerator Start()
    {
        while (true)
        {
            rb = true;
            yield return 0;
        }

    }

    void Update()
    {
        Dmag = Mathf.Sqrt((Dup * Dup) + (Dright * Dright));
        Dvec = Dright * transform.right + Dup * transform.forward;
    }
}
