using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Railek.Unibase.Utilities;

namespace Railek.Unilocalize
{
    public static class LocalizationImporter
    {
        private static Dictionary<string, List<string>> languageStrings = new Dictionary<string, List<string>>();

        private static List<string> EmptyList = new List<string>();

        private const string KeyNotFound = "[{0}]";

        private static TextAsset _inputFile;

        static LocalizationImporter()
        {
            Initialize();
        }

        private static void Initialize()
        {
            var settings = Localization.Instance;
            if (settings == null)
            {
                Debug.LogError("Could not find a Localization Settings file in Resources.");
                return;
            }

            languageStrings.Clear();
            ImportInputFiles();
        }

        private static void ImportInputFiles()
        {
            _inputFile = Localization.Instance.InputFiles;
            if (_inputFile != null)
            {
                ImportTextFile(_inputFile.text);
            }
        }

        public static List<string> Languages;

        private static void ImportTextFile(string text)
        {
            List<List<string>> rows;
            text = text.Replace("\r\n", "\n");

            rows = CSVReader.Parse(text);

            Languages = rows.First();

            foreach (var row in rows)
            {
                var key = row[0];

                if (string.IsNullOrEmpty(key) || IsLineBreak(key) || row.Count <= 1)
                {
                    continue;
                }

                row.RemoveAt(0);
                row.RemoveAt(0);

                if (languageStrings.ContainsKey(key))
                {
                    Debug.Log("The key '" + key + "' already exist, but is now overwritten");
                    languageStrings[key] = row;
                    continue;
                }
                languageStrings.Add(key, row);
            }
        }

        public static bool IsLineBreak(string currentString)
        {
            return currentString.Length == 1 && (currentString[0] == '\r' || currentString[0] == '\n')
                || currentString.Length == 2 && currentString.Equals(Environment.NewLine);
        }

        public static List<string> GetLanguages(string key)
        {
            if (languageStrings == null || languageStrings.Count == 0)
            {
                ImportInputFiles();
            }

            if (string.IsNullOrEmpty(key) || !languageStrings.ContainsKey(key))
            {
                return EmptyList;
            }

            var supportedLanguageStrings = new List<string>();

            for (var i = 0; i < Languages.Count; i++)
            {
                if (languageStrings != null)
                {
                    supportedLanguageStrings.Add(languageStrings[key][i]);
                }
            }

            return supportedLanguageStrings;
        }

        public static Dictionary<string, List<string>> GetLanguagesStartsWith(string key)
        {
            if (languageStrings == null || languageStrings.Count == 0)
            {
                ImportInputFiles();
            }

            var multipleLanguageStrings = new Dictionary<string, List<string>>();
            foreach (var languageString in languageStrings)
            {
                if (languageString.Key.ToLower().StartsWith(key.ToLower()))
                {
                    multipleLanguageStrings.Add(languageString.Key, languageString.Value);
                }
            }

            return multipleLanguageStrings;
        }

        public static Dictionary<string, List<string>> GetLanguagesContains(string key)
        {
            if (languageStrings == null || languageStrings.Count == 0)
            {
                ImportInputFiles();
            }

            var multipleLanguageStrings = new Dictionary<string, List<string>>();
            foreach (var languageString in languageStrings)
            {
                if (languageString.Key.ToLower().Contains(key.ToLower()))
                {
                    multipleLanguageStrings.Add(languageString.Key, languageString.Value);
                }
            }

            return multipleLanguageStrings;
        }

        public static string Get(string key)
        {
            return Get(key, Localization.Instance.SelectedLanguage);
        }

        private static string Get(string key, string language)
        {
            var languages = GetLanguages(key);

            var selected = Languages.IndexOf(language);

            if (languages.Count <= 0 || selected < 0 || selected >= languages.Count)
            {
                return string.Format(KeyNotFound, key);
            }

            var currentString = languages[selected];

            if (!string.IsNullOrEmpty(currentString) && !IsLineBreak(currentString))
            {
                return currentString;
            }

            Debug.LogWarning("Could not find key " + key + " for current language " + language);
            currentString = languages[selected];

            return currentString;
        }

        public static string GetFormat(string key, params object[] arguments)
        {
            if (string.IsNullOrEmpty(key) || arguments == null || arguments.Length == 0)
            {
                return Get(key);
            }

            return string.Format(Get(key), arguments);
        }

        public static void Refresh()
        {
            Initialize();
            if (Localization.Instance != null)
            {
                Localization.Instance.InvokeOnLocalize();
            }
        }
    }
}
