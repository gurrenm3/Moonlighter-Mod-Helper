namespace Moonlighter_Mod_Helper.Api
{
    public interface IConsumable
    {
        void CanConsume(ConsumableChecker consumableChecker);

        void Consume();
    }
}
