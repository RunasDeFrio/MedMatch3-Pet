namespace RunasDev.Core.Enabled
{
    public interface IEnabled
    {
        bool IsEnable { get; }
        
        /// <summary>
        /// Включить модель.
        /// </summary>
        void Enable();

        /// <summary>
        /// Выключить модель.
        /// </summary>
        void Disable();
    }
}