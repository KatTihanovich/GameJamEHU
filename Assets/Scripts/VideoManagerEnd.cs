using UnityEngine;
using UnityEngine.Video;

public class VideoManagerEnd : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Отключаем зацикливание, чтобы видео не повторялось
        videoPlayer.isLooping = false;
        
        // Запускаем видео
        videoPlayer.Play();
        
        // Добавляем обработчик события окончания видео
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Останавливаем видео на последнем кадре
        videoPlayer.Pause();
    }
}