using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;
    [SerializeField] string parameterName = "파라미터 이름";
    private float value;

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

    }

    public void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { Test(); });
    }

    public void Test()
    {
        value = volumeSlider.value;
        Debug.Log("value : " + value);
        Debug.Log("audioMixer :" + audioMixer.name);
        audioMixer.SetFloat(parameterName, Mathf.Log10(value) * 20);
    }
}
