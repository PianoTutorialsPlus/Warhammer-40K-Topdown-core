using UnityEngine;

namespace WH40K.UI
{
    public class UIGameInfoManager : MonoBehaviour
    {
        [SerializeField] private UIGameInfoFiller infoItem;

        public void FillInfoPanel()
        {
            infoItem.FillInfoPanel();
        }
    }
}