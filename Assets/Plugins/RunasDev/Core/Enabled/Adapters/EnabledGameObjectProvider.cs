using UnityEngine;

namespace RunasDev.Core.Enabled.Adapters
{
    public readonly struct EnabledGameObjectAdapter : IEnabled
    {
        public GameObject GameObject { get; }

        public EnabledGameObjectAdapter(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public bool IsEnable => GameObject.activeInHierarchy;

        public void Enable()
        {
            GameObject.SetActive(true);
        }

        public void Disable()
        {
            GameObject.SetActive(false);
        }

        public static implicit operator EnabledGameObjectAdapter(GameObject gameObject) =>
            new EnabledGameObjectAdapter(gameObject);

        public static implicit operator GameObject(EnabledGameObjectAdapter adapter) =>
            adapter.GameObject;
    }
}