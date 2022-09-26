using UnityEngine;

public class HapticManager : Singleton<HapticManager>
{
    private static float timeToHaptic;
    [SerializeField] private float hapticRate = .1f;
    private static float hRate;

    private void Start()
    {
        Taptic.tapticOn = true;
        hRate = hapticRate;
    }
    public void PlayHaptic()
    {
        if (Time.time > timeToHaptic)
        {
            Taptic.Light();
            Debug.Log("Haptic called");
            timeToHaptic = Time.time + hRate;
        }
    }
}
