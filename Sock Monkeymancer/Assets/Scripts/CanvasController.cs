using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject[] content;

    void Start()
    {
        HideCanvasItems();
    }

    public void HideCanvasItems()
    {
        foreach (GameObject obj in content)
        {
            obj.SetActive(false);
        }
    }

    public void ShowCanvasItems()
    {
        foreach (GameObject obj in content)
        {
            obj.SetActive(true);
        }
    }
    public void HandleWin()
    {
        ShowCanvasItems();
    }
}
