using UnityEngine;

public class WWiseEvents : MonoBehaviour
{
    [field: Header("SFX")]
    [field: SerializeField] public AK.Wwise.Event TestSound { get; private set; }
    [field: SerializeField] public AK.Wwise.Event WrongSound { get; private set; }

    [field: Header("UI")]
    [field: SerializeField] public AK.Wwise.Event ButtonClick { get; private set; }
    [field: SerializeField] public AK.Wwise.Event ButtonHover { get; private set; }
    [field: SerializeField] public AK.Wwise.Event ButtonBack { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public AK.Wwise.Event MenuMusic { get; private set; }
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
