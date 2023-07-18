using UnityEngine;

public class Pentacle : MonoBehaviour
{
    private void OnMouseEnter()
    {
        FindAnyObjectByType<ClassSelectController>().SelectSummon();
    }
    private void OnMouseExit()
    {
        FindAnyObjectByType<ClassSelectController>().DeSelectSummon(); 
    }
}
