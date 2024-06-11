using UnityEngine;

[CreateAssetMenu(menuName = "Instrument/Instrument Information")]
public class InstrumentInformation : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    [SerializeField]
    public string instrumentName;
    [SerializeField]
    public AudioClip instrumentSound;
    [SerializeField]
    public PlayerInstrument.InstrumentType instrumentType;
    [SerializeField]
    public Sprite instrumentUiVisual;

}
