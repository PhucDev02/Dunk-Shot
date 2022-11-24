using UnityEngine;

public class AnchorNet : MonoBehaviour
{
    [SerializeField] Transform bottomPosition, anchorPosition;
    [SerializeField] Vector3 distance;
    //rball=0.2785
    private void Start()
    {
        distance = anchorPosition.localPosition - bottomPosition.localPosition;
    }
    void Update()
    {
        anchorPosition.localPosition = bottomPosition.localPosition + distance / DragPanel.GetScale();
    }
}
