using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class AudioStreamer : MonoBehaviour
{
    [Header("Audio URL")]
    public string AudioURL;

    [Header("Audio Components")]
    public AudioSource source;
    private AudioClip clip;
    public Slider audioTimeline;

    [Header("Image Components")]
    public Texture musicImage;
    public RawImage targetImage;
    public Texture LoaderImage;

    [Header("Text Components")]
    public TextMeshProUGUI LoadingText;
    public TextMeshProUGUI audioDuration;

    private void Start()
    {
        LoadingText.text = "Welcome to AR Musicana";
        targetImage.texture = LoaderImage;
    }

    IEnumerator GetAudioClip()
    {
        source.Stop();
        LoadingText.text = "Loading Music";
        targetImage.texture = LoaderImage;
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(AudioURL, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                LoadingText.text = "Now Playing";
                clip = DownloadHandlerAudioClip.GetContent(www);
                source.clip = clip;

                audioTimeline.direction = Slider.Direction.LeftToRight;
                audioTimeline.minValue = 0;
                audioTimeline.maxValue = source.clip.length;

                source.Play();
                targetImage.texture = musicImage;
                Debug.Log("Audio is playing.");

                float minutes = Mathf.Round(clip.length / 60);
                audioDuration.text = minutes.ToString() + " Minutes";

            }
        }
    }

    private void Update()
    {
        audioTimeline.value = source.time;
    }

    public void PlayAudio()
    {
        StartCoroutine(GetAudioClip());
        Debug.Log("Downloading Audio");
    }

    public void PauseAudio()
    {
        source.Pause();
    }

    public void StopAudio()
    {
        source.Stop();
    }

}
