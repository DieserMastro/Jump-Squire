using Unity.VisualScripting;
using UnityEngine;

public class Floor_Controller : MonoBehaviour
{
    [SerializeField]
    private bool isLava = false;
    

    public void SetIsLava()
    {
        isLava = true;
        transform.tag = "Damage";
    }
}
