using UnityEngine.EventSystems;
using UnityEngine;
namespace XEntity
{
    public class ContainerPanelDragger : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private bool isDragging = false;
        private Vector2 offset;

        public void OnBeginDrag(PointerEventData eventData)
        {
            offset = new Vector2(transform.localPosition.x, transform.localPosition.y) - new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isDragging == false) return;
            transform.localPosition = Input.mousePosition + new Vector3(offset.x, offset.y, 0);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
        }
    }
}
