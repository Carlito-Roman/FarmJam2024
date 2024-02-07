using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        base.Interact();
        Debug.Log("Interacted with" + gameObject.name);

        gameObject.GetComponent<MeshRenderer>().enabled = !gameObject.GetComponent<MeshRenderer>().enabled;
    }
}
