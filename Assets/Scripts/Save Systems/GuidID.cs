using UnityEngine;



public class GuidID : MonoBehaviour
{
    [Tooltip("Do Not Use Context Menu")]
    [SerializeField]

    public string id;




    private void CreateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void OnValidate()
    {
        // Ensure id is not empty, create a new Guid if it is
        if (string.IsNullOrEmpty(id))
        {
            CreateGuid();
        }
    }
}
