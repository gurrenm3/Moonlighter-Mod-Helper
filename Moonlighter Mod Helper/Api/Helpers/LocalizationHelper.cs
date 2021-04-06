using I2.Loc;

namespace Moonlighter_Mod_Helper.Api
{
    public class LocalizationHelper
    {
        public static void AddTermData(string identifier, string text)
        {
            LocalizationManager.InitializeIfNeeded();
            for (int i = 0; i < LocalizationManager.Sources.Count; i++)
            {
                var source = LocalizationManager.Sources[i];
                if (source.ContainsTerm(identifier))
                    continue;

                TermData termData = new TermData();
                termData.Term = identifier;
                termData.TermType = eTermType.Text;
                PopulateArray(ref termData.Languages, source.mLanguages.Count, text);
                PopulateArray(ref termData.Languages_Touch, source.mLanguages.Count, text);
                termData.Flags = new byte[source.mLanguages.Count];
                source.mTerms.Add(termData);
                source.mDictionary.Add(identifier, termData);
            }
        }

        private static void PopulateArray<T>(ref T[] parent, int arrayLength, T populateValue)
        {
            parent = new T[arrayLength];
            PopulateArray(ref parent, populateValue);
        }

        private static void PopulateArray<T>(ref T[] parent, T populateValue)
        {
            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = populateValue;
            }
        }
    }
}
