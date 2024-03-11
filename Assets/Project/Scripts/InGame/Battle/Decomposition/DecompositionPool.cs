public class DecompositionPool : ObjectPool
{
    public static DecompositionPool Instance;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
}