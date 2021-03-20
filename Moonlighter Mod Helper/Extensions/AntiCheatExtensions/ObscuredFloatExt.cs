using CodeStage.AntiCheat.ObscuredTypes;

namespace Moonlighter_Mod_Helper.Extensions
{
    public static class ObscuredFloatExt
    {
        public static void SetNewValue(this ObscuredFloat obscuredFloat, float newValue)
        {
            obscuredFloat.hiddenValue = ObscuredFloat.InternalEncrypt(newValue, obscuredFloat.currentCryptoKey);
        }
    }
}