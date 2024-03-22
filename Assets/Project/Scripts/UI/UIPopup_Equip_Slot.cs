using UnityEngine;
using UnityEngine.UI;

public class UIPopup_Equip_Slot : MonoBehaviour
{
    [SerializeField] private Text Level;
    [SerializeField] private Text Damage;
    
    public void Refresh(int index)
    {
        var arrow = PlayerDataManager.ArrowData.Arrows[index];
        var arrowExp = PlayerDataManager.ArrowData.OwnArrowPiece[index];
        
        Level.text = $"Lv.{arrow:00}";
        Damage.text = $"({arrowExp}/100)";
    }
}