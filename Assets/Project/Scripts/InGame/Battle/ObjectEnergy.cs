using System.Collections;
using UnityEngine;
using DG.Tweening;
public class ObjectEnergy : MonoBehaviour
{
    float moveDuration = 0.4f; // �̵��ϴ� �� �ɸ��� �ð�
    Ease easeType = Ease.OutQuad; // �̵��� ���� ����
    Vector3 targetPos;
    int energy;
    Vector3 wallPos;

    public void InitData(int _energy)
    {
        wallPos = GameManager.Instance.enemyPool.target.transform.position;

        energy = _energy;
        targetPos = new Vector3(transform.position.x + Random.Range(-0.3f, 0.3f), transform.position.y + Random.Range(0.2f, 0.3f), transform.position.z + Random.Range(-0.3f, 0.3f));
        transform.DOMove(targetPos, moveDuration).SetEase(easeType);

        StartCoroutine(CoMoveDown());
    }

    private void OnMouseEnter()
    {
        GameManager.Instance.vfxPool.Spawn(3, 0, transform.position);
        GameManager.Instance.obPool.SpawnTxt(1, transform.position, "+"+energy.ToString());
        HandleCoinPickup();
        SoundManager.Instance.Play(Enum_Sound.Effect, "EnergyGet");
    }

    private void HandleCoinPickup()
    {
        GameManager.Instance.MoveEnergyFull(energy);
        gameObject.SetActive(false);
    }

    IEnumerator CoMoveDown()
    {
        while (true)
        {
            if (Mathf.Abs(transform.position.y - wallPos.y) > 0.2f)
            {
                //print();
                transform.Translate(Vector3.down * 0.4f * Time.deltaTime);
            }
            else
            {
                //OnMouseDown();
                yield break;
            }

            yield return null;
        }
    }
}
