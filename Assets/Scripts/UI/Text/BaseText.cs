using TMPro;
using UnityEngine;

namespace CarParkingChaos.UI.Text
{
    public class BaseText : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text Value { get; private set; }
    }
}
