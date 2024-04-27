using UnityEngine;

public class OSKRow : MonoBehaviour
{
    [SerializeField] private OSK          _osk;
    [SerializeField] private OSKeyRegular _keyRegularPrefab;
    [SerializeField] private string       _letters;

    private void Start()
    {
        var letterKeys = _letters.Split(",");
        foreach (var key in letterKeys)
        {
            var oskey = Instantiate(_keyRegularPrefab, transform);
            oskey.Init(key,_osk);
        }
    }
}