using UnityEngine;
using UnityEngine.UI;

public class UIPopup_Equip_Slot : MonoBehaviour
{
    [SerializeField] private Text Level;
    [SerializeField] private Text Damage;
    
    public void Refresh(int index)
    {
        var equip = PlayerDataManager.ArrowData.Arrows[index];
        var equipCount = PlayerDataManager.ArrowData.OwnArrowPiece[index];
        
        Level.text = $"Lv.{equip:00}";
        Damage.text = $"({equipCount}/100)";
    }
}