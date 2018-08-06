using FoodTips.Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FoodTips
{

    public class ApllicationOptions
    {
        public Dictionary<int, string> map_language = new Dictionary<int, string>()
        {
            {1, "en-us" }
            , {2, "ru"}
            , {3, "pl"}
        };
        public enum enum_fontColor
        {
            LawnGreen
                , Red
                , Green
                , Blue
                , Yellow
                , Purple
                , Black
                , White
                , Brown
        }


        private string file = "data\\options";
        public string[] imageExtenctions = new string[] { "jpg", "png", "gif" };
        public string languageName { get; private set; }
        public DictionaryType language { get; private set; }
        public string fontColor { get; private set; }
        public string background { get; private set; }
        public string wineSource { get; private set; }
        public string dishSource { get; private set; }
        public string spiceSource { get; private set; }

        public ApllicationOptions()
        {
            load();
        }

        private void load()
        {
            string content;

            if (System.IO.File.Exists(file))
                using (StreamReader sr = new StreamReader(file))
                    content = sr.ReadToEnd().ToString();
            else
                throw new OptionFileNotExistException();

            languageName = getTag_langueage(content);
            languageInit();
            fontColor = getTag_fontColor(content);
            background = getTag_background(content);
            wineSource = getTag_wineSource(content);
            dishSource = getTag_dishSource(content);
            spiceSource = getTag_spiceSource(content);
            //System.Windows.MessageBox.Show(language + "\n" + fontColor + "\n" + background + "\n" + wineSource + "\n" + dishSource + "\n" + spiceSource);
        }

        private void overwrite()
        {
            StringBuilder content = new StringBuilder();
            content.Append("<generaloptions>").Append(Environment.NewLine).Append("\t");
            content.Append("<language>").Append(languageName).Append("</language>").Append(Environment.NewLine).Append("\t");
            content.Append("<fontColor>").Append(fontColor).Append("</fontColor>").Append(Environment.NewLine).Append("\t");
            content.Append("<background>").Append(background).Append("</background>").Append(Environment.NewLine).Append("\t");
            content.Append("<wineSource>").Append(wineSource).Append("</wineSource>").Append(Environment.NewLine).Append("\t");
            content.Append("<dishSource>").Append(dishSource).Append("</dishSource>").Append(Environment.NewLine).Append("\t"); ;
            content.Append("<spiceSource>").Append(spiceSource).Append("</spiceSource>").Append(Environment.NewLine);
            content.Append("</generaloptions>");

            using (FileStream fileStream = new FileStream(file, FileMode.Open))
            {
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(content);
                streamWriter.Close();
                fileStream.Close();
            }
        }

        private void languageInit()
        {
            int[] languageCodes = map_language.Keys.ToArray();
            int languageCode = -1;
            foreach (int lc in languageCodes)
                if (map_language[lc].Equals(languageName))
                {
                    languageCode = lc;
                    break;
                }

            switch (languageCode)
            {
                case 1:
                    language = new EnglishDictionary();
                    break;
                case 2:
                    language = new RussianDictionary();
                    break;
                case 3:
                    language = new PolishDictionary();
                    break;
                default:
                    throw new NotExistingDictionaryException();
            }
        }

        private string getTag_langueage(string tags)
        {
            string regex = "<language>(.*)</language>";
            Match match = Regex.Match(tags, regex);

            return checkLanguageAvailability(match.Groups[1].Value);
        }

        private string getTag_fontColor(string tags)
        {
            string regex = "<fontColor>(.*)</fontColor>";
            Match match = Regex.Match(tags, regex);

            return checkFontColorAvailability(match.Groups[1].Value);
        }

        private string getTag_background(string tags)
        {
            string regex = "<background>(.*)</background>";
            Match match = Regex.Match(tags, regex);

            return checkFileFormat(match.Groups[1].Value, imageExtenctions, "MainPage_tymczasowa.jpg");
        }

        private string getTag_wineSource(string tags)
        {
            string regex = "<wineSource>(.*)</wineSource>";
            Match match = Regex.Match(tags, regex);

            return checkFileFormat(match.Groups[1].Value, new string[] { "csv" }, "wines.csv");
        }

        private string getTag_dishSource(string tags)
        {
            string regex = "<dishSource>(.*)</dishSource>";
            Match match = Regex.Match(tags, regex);

            return checkFileFormat(match.Groups[1].Value, new string[] { "csv" }, ">dishes.csv");
        }

        private string getTag_spiceSource(string tags)
        {
            string regex = "<spiceSource>(.*)</spiceSource>";
            Match match = Regex.Match(tags, regex);

            return checkFileFormat(match.Groups[1].Value, new string[] { "csv"}, "spices.csv");
        }

        public string checkLanguageAvailability(string languageName)
        {
            if (map_language.ContainsValue(languageName))
                return languageName;
            return map_language[1];
        }

        public string checkFontColorAvailability(string enumColorName)
        {
            if (Enum.IsDefined(typeof(enum_fontColor), enumColorName))
                return enumColorName;
            return default(enum_fontColor).ToString();
        }

        public string checkFileFormat(string fileName, string[] fileTypes, string defaultValue)
        {
            string types = "(" + string.Join("|", fileTypes) + ")";
            string regex = ".+\\." + types;
            Match match = Regex.Match(fileName, regex);
            
            if (match.Success)
                return fileName;
            return defaultValue;
        }

        public void newOptionSet(string[] o)
        {
            languageName = o[0];
            fontColor = o[1];
            background = o[2];
            wineSource = o[3];
            dishSource = o[4];
            spiceSource = o[5];

            overwrite();
            load();
        }
    }

    public class OptionFileNotExistException : Exception
    {
        public OptionFileNotExistException() : base("Fatal error. Cannot find option file. Please reinstall program.") { }

        public OptionFileNotExistException(string message) : base(message) { }
    }

    public class NotExistingDictionaryException : Exception
    {
        public NotExistingDictionaryException() : base("The dictionary you chose in options does not exist in program") { }

        public NotExistingDictionaryException(string message) : base(message) { }
    }
}
