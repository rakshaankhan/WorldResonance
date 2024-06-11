using UnityEngine;

[CreateAssetMenu(menuName = "Instrument/Instrument Information")]
public class InsturmentInformation : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    [SerializeField]
    public string insturmentName;
    [SerializeField]
    public AudioClip insturmentSound;
    [SerializeField]
    public PlayerInstrument.InstrumentType instrumentType;
    [SerializeField]
    public Sprite instrumentUiVisual;

}
