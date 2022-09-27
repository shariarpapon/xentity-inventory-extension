using UnityEngine.EventSystems;
using UnityEngine;
namespace XEntity
{
    public class ContainerPanelDragger : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private bool isDragging = false;
        private Vector2 offset;
        private Transform mainPanel;

        private void Awake() 
        {
            mainPanel = transform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            offset = new Vector2(mainPanel.localPosition.x, mainPanel.localPosition.y) - new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            mainPanel.parent.SetAsLastSibling();
            isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isDragging == false) return;
            mainPanel.localPosition = Input.mousePosition + new Vector3(offset.x, offset.y, 0);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
        }
    }
}
