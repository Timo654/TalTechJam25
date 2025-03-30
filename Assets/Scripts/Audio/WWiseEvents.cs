using UnityEngine;

public class WWiseEvents : MonoBehaviour
{
    [field: Header("SFX")]
    [field: SerializeField] public AK.Wwise.Event TestSound { get; private set; }
    [field: SerializeField] public AK.Wwise.Event WrongSound { get; private set; }

    [field: SerializeField] public AK.Wwise.Event PostHit { get; private set; }
    [field: SerializeField] public AK.Wwise.Event BenchHit { get; private set; }
    [field: SerializeField] public AK.Wwise.Event BinHit { get; private set; }
    [field: SerializeField] public AK.Wwise.Event PedHit { get; private set; }
    [field: SerializeField] public AK.Wwise.Event GetCourage { get; private set; }
    [field: SerializeField] public AK.Wwise.Event LoseCourage { get; private set; }
    [field: SerializeField] public AK.Wwise.Event TrafficSignHit { get; private set; }
    [field: SerializeField] public AK.Wwise.Event ScooterHit { get; private set; }

    [field: Header("UI")]
    [field: SerializeField] public AK.Wwise.Event ButtonClick { get; private set; }
    [field: SerializeField] public AK.Wwise.Event ButtonHover { get; private set; }
    [field: SerializeField] public AK.Wwise.Event ButtonBack { get; private set; }
    [field: SerializeField] public AK.Wwise.Event GameStartClick { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public AK.Wwise.Event PlaySwitcher { get; private set; }

    [field: Header("Switches")]
    [field: SerializeField] public AK.Wwise.Switch MenuMusic { get; private set; }
    [field: SerializeField] public AK.Wwise.Switch CreditsMusic { get; private set; }
    [field: SerializeField] public AK.Wwise.Switch GameMusic1 { get; private set; }
    [field: SerializeField] public AK.Wwise.Switch GameMusic2 { get; private set; }

    public static WWiseEvents Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }
}
