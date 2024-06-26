using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    [SerializeField] public Color[] blockColors;
    [SerializeField] private List<GameObject> _goBulletSlot;
    [SerializeField] public List<Image> _goBullet;
    [SerializeField] private GameObject _goReloadText;
    public TextMeshProUGUI txtEquip;

    private int _count = 0;

    public void UpdateUI(int count, int currentIndex)
    {
        _count = count;

        // 1개 장착했을때 맨 처음것만 작동
        // count = 1, i = 0
        for (int i = _goBullet.Count - 1; i >= 0; i--)
        {
            if (i >= count)
            {
                _goBullet[i].gameObject.SetActive(false);
                continue;
            }

            _goBullet[i].gameObject.SetActive(i - currentIndex >= 0);
        }
    }

    public void Reload()
    {
        StartCoroutine(CoReload());
    }

    public void BlockColor()
    {
        StartCoroutine(CoReload());
    }

    IEnumerator CoReload()
    {
        yield return new WaitForSeconds(0.3f);
        WaitForSeconds wfs = new WaitForSeconds(0.4f / _count);

        _goReloadText.SetActive(true);

        for (int i = _count - 1; i >= 0; i--)
        {
            _goBullet[i].gameObject.SetActive(true);
            yield return wfs;
        }
        SoundManager.Instance.Play(Enum_Sound.Effect, "Reload", 0, 0.7f, 1f);

        _goReloadText.SetActive(false);
    }
}
