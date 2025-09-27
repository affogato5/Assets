using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guis : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> categories;

    public void selectCategory(int ID)
    {
        for (int i = 0; i < categories.Count; i++)
        {
            categories[i].SetActive(false);
        }
        categories[ID].SetActive(true);
    }
}
