using UnityEngine;

namespace RunasDev.Core.Enabled.Adapters
{
    public readonly struct EnabledGameObjectAsBehaviourAdapter<T> : IEnabled where T : Behaviour
    {
        private T Behaviour { get; }


        public EnabledGameObjectAsBehaviourAdapter(T behaviour)
        {
            Behaviour = behaviour;
        }

        public bool IsEnable => Behaviour.isActiveAndEnabled;

        public void Enable()
        {
            Behaviour.gameObject.SetActive(true);
        }

        public void Disable()
        {
            Behaviour.gameObject.SetActive(false);
        }

        public static implicit operator EnabledGameObjectAsBehaviourAdapter<T>(T behaviour) =>
            new EnabledGameObjectAsBehaviourAdapter<T>(behaviour);

        public static implicit operator EnabledGameObjectAsBehaviourAdapter<T>(GameObject gameObject)
        {
            var behaviour = gameObject.GetComponent<T>();
            return behaviour == null ? default : new EnabledGameObjectAsBehaviourAdapter<T>(behaviour);
        }

        public static implicit operator GameObject(EnabledGameObjectAsBehaviourAdapter<T> adapter) =>
            adapter.Behaviour.gameObject;

        public static implicit operator T(EnabledGameObjectAsBehaviourAdapter<T> adapter) =>
            adapter.Behaviour;
    }
}