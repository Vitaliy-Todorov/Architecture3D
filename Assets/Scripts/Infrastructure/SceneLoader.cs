using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scrips.Infrastructure
{
    public class SceneLoader 
    {
        private readonly ICoroutineRanner _coroutineRanner;

        public SceneLoader(ICoroutineRanner coroutineRanner) =>
            _coroutineRanner = coroutineRanner;

        // _coroutineRanner - интерфейс в котором происходит непосредственный запуск корутины: LoadScene(name, onLoadwr)
        // onLoadwr - это callback, метод, который должен вызываться всякий раз, когда что-то происходит
        public void Load(string name, Action onLoadwr = null) =>
            _coroutineRanner.StartCoroutine(LoadScene(name, onLoadwr));

        // Action - делегат возвращающий void
        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            // Проверяем, если мы уже на нужной сцене, то не грузим
            if(SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            // асинхронно загружаем сцену
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            // Корутины проверяют программу на закончиность каждый кадр
            // IEnumerator двигает процесс (корутину)
            // yield смотрить что возвращает наша корутина на каждом шаге IEnumerator
            // Пока yiel - null, корутина движется вперёд
            // yield return null; Корутин идёт на следующий кадр.
            // yield return возвращает какой IEnumerator будет Current
            // null говорит о том, что нам ничего делать не нужно
            // 
            //Подождать три кадра:
            // yield return null;
            // yield return null;
            // yield return null;


            // waitNextScene.isDone - проверяем закончилась ли загрузка.
            while (!waitNextScene.isDone)
                yield return null;

            // onLoaded.Invoke() тоже самое что и простое использование onLoaded()
            // callback - метод, который должен вызываться всякий раз, когда что-то происходит
            onLoaded?.Invoke();
        }
    }
}