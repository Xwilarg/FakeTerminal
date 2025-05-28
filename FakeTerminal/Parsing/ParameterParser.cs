using System.Text;

namespace FakeTerminal.Parsing;

internal class ParameterParser
{
    List<Parameter> _allParams = [];
    Parameter _currParam = new();

    StringBuilder _currWord = new();

    private void AddFlag()
    {
        var word = _currWord.ToString();
        if (word.StartsWith('-')) // We found a flag
        {
            if (_currParam.Name != null)
            {
                _allParams.Add(_currParam);
                _currParam = new();
            }
            if (word.StartsWith("--")) // Example: --format abc
            {
                _currParam.Name = word[2..];
            }
            else
            {
                foreach (var flag in word[1..]) // Example -vf abc
                {
                    if (_currParam.Name != null)
                    {
                        _allParams.Add(_currParam);
                        _currParam = new();
                    }
                    _currParam.Name = flag.ToString();
                }
            }
        }
        else // We are currently parsing a value
        {
            if (_currParam.Value != null)
            {
                _allParams.Add(_currParam);
                _currParam = new();
            }
            _currParam.Value = word;
        }
    }

    internal Parameter[] Parse(string parameters)
    {
        bool escapeNext = false;
        bool isQuoted = false;
        foreach (var p in parameters)
        {
            if (_currWord.Length == 0 && char.IsWhiteSpace(p))
            {
                ;// Ignore whitespaces at the start
            }
            else if (!escapeNext && p == '\\')
            {
                escapeNext = true; // Escape sequence detected
                continue;
            }
            else if (!escapeNext && p == '"')
            {
                if (isQuoted)
                {
                    AddFlag();

                    _currWord = new();
                    isQuoted = false;
                }
                else isQuoted = true;
            }
            else if (!escapeNext && !isQuoted && p == ' ')
            {
                AddFlag();

                _currWord = new();
            }
            else
            {
                _currWord.Append(p);
            }

            escapeNext = false;
        }
        if (_currWord.Length > 0)
        {
            AddFlag();
        }
        if (_currParam.Name != null || _currParam.Value != null)
        {
            _allParams.Add(_currParam);
        }

        return _allParams.ToArray();
    }
}
