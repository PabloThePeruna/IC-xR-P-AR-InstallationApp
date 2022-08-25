using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeColliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        The idea for this is as follows:
        1.We give all mesh vertices a 0 sized sphere collider Gameobject.
        2.We check if the boiler collider contains one of these colliders, if not ->color all no collision->return
        3.We let the boiler collider return the colliders it interacted with ->color them collision, hide all others

        Foreseeable problems:
        - Getting from the collider array in 3. back to the vertices ->Since collider array and vertex array have same amount, numbers could stay the same
        
        Conclusion
        Looking over this plan, it is probable that this will not be more efficient than the current method
        If one of you wants to try writing this, be my guest
        */

    }
}
