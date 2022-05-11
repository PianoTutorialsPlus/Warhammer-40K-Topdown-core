using UnityEngine;

namespace WH40K.UI
{
    public interface IUIRangeIndicator
    {
        float BaseSize { get; set; }
        Vector3 Position { get; set; }
        Vector3 LocalScale { get; set; }
    }
}