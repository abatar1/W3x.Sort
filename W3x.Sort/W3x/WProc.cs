using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using W3x.Sort.Domain;

namespace W3x.Sort.W3x
{
    public class WProc
    {       
        private const string W3Descriptor = "HM3W";
        private const string IgnoreFileName = "ignore.json";

        private static List<Rule> _rules;

        public static readonly Func<WRequest, string> KeyProcessor = request =>
        {
            return !request.Filepath.Contains(request.Key) ? null : request.Key;
        };

        public static readonly Func<WRequest, string> NumberProcessor = request =>
        {
            var fileContent = File.ReadAllBytes(request.Filepath)
                .Select(t => (char)t)
                .ToArray();
            var descriptor = string.Join("", fileContent.Take(4));
            if (!descriptor.Equals(W3Descriptor)) return null;

            var startOffset = 8;
            while (fileContent[startOffset] != 0) startOffset += 1;
            startOffset += 5;

            var playerNumArr = fileContent.Skip(startOffset).Take(4).Reverse().ToArray();
            var playerNum = (playerNumArr[0] << 24)
                            | (playerNumArr[1] << 16)
                            | (playerNumArr[2] << 8)
                            | playerNumArr[3];
            return playerNum == 0 ? null : playerNum.ToString();                   
        };

        public static readonly Func<WRequest, string> IgnoreProcessor = request =>
        {
            var fileContent = File.ReadAllText(IgnoreFileName);
            if (_rules == null) _rules = JsonConvert.DeserializeObject<List<Rule>>(fileContent);
            var requiredRule = _rules.FirstOrDefault(rule =>
            {
                return rule.Keywords
                    .Any(keyword => request.Filepath.Contains(keyword));
            });
            return requiredRule == null ? NumberProcessor(request) : requiredRule.Name;     
        };
    }
}
