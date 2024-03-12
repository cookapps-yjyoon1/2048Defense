using UnityEngine;

public class DecompositionController : MonoBehaviour
{
    [SerializeField] private GameObject _goHead;
    [SerializeField] private GameObject _goLeftArm;
    [SerializeField] private GameObject _goRightArm;

    public void BodyReset()
    {
        _goHead.SetActive(true);
        _goLeftArm.SetActive(true);
        _goRightArm.SetActive(true);
    }
    
    public void ActiveRandom()
    {
        var random = Random.Range(0, 3);

        switch (random)
        {
            case 0:
                DropHead();
                break;
            case 1:
                DropLeftArm();
                break;
            case 2:
                DropRightArm();
                break;
        }
    }

    public void DropHead()
    {
        if (_goHead == null)
        {
            return;
        }

        _goHead.SetActive(false);
        var dec = DecompositionPool.Instance.Get(0, _goHead.transform.position).GetComponent<FadeAwayEffect>();
        dec.OnEffect(0);
    }

    public void DropLeftArm()
    {
        if (_goLeftArm == null)
        {
            return;
        }

        _goLeftArm.SetActive(false);
        var dec = DecompositionPool.Instance.Get(1, _goLeftArm.transform.position).GetComponent<FadeAwayEffect>();
        dec.OnEffect(1);
    }

    public void DropRightArm()
    {
        if (_goRightArm == null)
        {
            return;
        }

        _goRightArm.SetActive(false);
        var dec = DecompositionPool.Instance.Get(2, _goRightArm.transform.position).GetComponent<FadeAwayEffect>();
        dec.OnEffect(1);
    }
}