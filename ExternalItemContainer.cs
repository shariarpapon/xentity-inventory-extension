using UnityEngine;
using UnityEngine.UI;

namespace XEntity
{
    public class ExternalItemContainer : ItemContainer
    {
        [SerializeField] private GameObject uiPrefab;
        [SerializeField] private Transform uiCanvas;
        [SerializeField] private bool toggleOnClick = true;
        [SerializeField] private KeyCode closeUIKey = KeyCode.Space;
        [SerializeField] private string containerTitle = "Container"; 

        protected override void InitContainer()
        {
            GameObject containerPanel = Instantiate(uiPrefab, uiCanvas);

            if (containerPanel.TryGetComponent(out ItemContainer cont))
                Destroy(cont);
            
            containerUI = containerPanel.transform.Find("Inventory UI");
            slotOptionsUI = containerPanel.transform.Find("Slot Options").gameObject;

            containerUI.Find("Title").GetComponentInChildren<Text>().text = containerTitle;

            itemUseButton = slotOptionsUI.transform.Find("Use Button").GetComponent<Button>();
            itemRemoveButton = slotOptionsUI.transform.Find("Remove Button").GetComponent<Button>();

            Transform slotHolder = containerUI.Find("Slot Holder");
            slots = new ItemSlot[slotHolder.childCount];
            for (int i = 0; i < slots.Length; i++)
            {
                ItemSlot slot = slotHolder.GetChild(i).GetComponent<ItemSlot>();
                slots[i] = slot;
                slot.GetComponent<Button>().onClick.AddListener(delegate { OnSlotClicked(slot); });
            }

            slotOptionsUI.SetActive(false);
            containerUI.gameObject.SetActive(false);
        }

        protected override void Update() 
        {
            if (Input.GetKeyDown(closeUIKey) && containerUI.gameObject.activeSelf == true) 
            {
                if (containerUI.gameObject.activeSelf)
                {
                    StartCoroutine(Utils.TweenScaleOut(containerUI.gameObject, 50, false));
                }
                else
                {
                    StartCoroutine(Utils.TweenScaleIn(containerUI.gameObject, 50, Vector3.one));
                }
            } 
            if (UIToggleKey == KeyCode.None) return;
            base.Update();
        }

        private void OnMouseDown() 
        {
            if (!toggleOnClick) return;
            ToggleUI();
        }
    }
}
