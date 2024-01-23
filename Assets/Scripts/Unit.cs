using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.layer == 11)
        {
            UnitSelections.Instance.unitList.Add(this.gameObject);
        }
        else if (this.gameObject.layer == 12)
        {
            UnitSelections.Instance.EnemyList.Add(this.gameObject);
        }

    }

    // Update is called once per frame
    void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }

    private void OnDisable()
    {
        if (this.gameObject.layer == 11)
        {
            UnitSelections.Instance.unitList.Remove(this.gameObject);
            UnitSelections.Instance.unitSelected.Remove(this.gameObject);
        }
        else if (this.gameObject.layer == 12)
        {
            UnitSelections.Instance.EnemyList.Remove(this.gameObject);
        }

        
    }
}
