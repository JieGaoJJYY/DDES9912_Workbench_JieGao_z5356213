using UnityEngine;

public class PuzzleLight : MonoBehaviour
{
    public GameObject puzzleLight;
    public Transform[] puzzleParent;
   
    void Update()
    {
        if (puzzleParent[0].childCount!=0|| puzzleParent[1].childCount != 0 || puzzleParent[2].childCount != 0 || puzzleParent[3].childCount != 0)
        {
            puzzleLight.SetActive(true);
        }
    }
}
