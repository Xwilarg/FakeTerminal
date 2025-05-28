namespace FakeTerminal.Parsing;

public abstract class ACommand
{
    protected ACommand(string name)
    {
        Name = name;
    }

    public virtual bool DoAction(Client client, Parameter[] parameters, out string output)
    {
        var outParameters = ValidateParameters(parameters, out var parsingError);
        if (outParameters == null)
        {
            output = $"Some arguments failed validation: {parsingError}";
            return false;
        }

        output = string.Empty;
        return true;
    }

    private Parameter[]? ValidateParameters(Parameter[] parameters, out ParsingError parsingError)
    {
        // Remove dupplicates
        parameters = parameters.Distinct(new ParameterEqualityComparer()).ToArray();

        foreach (var p in parameters)
        {
            var cParam = AllParameters.FirstOrDefault(x =>
                (x.ShortName.HasValue && x.ShortName.Value.ToString() == p.Name) ||
                x.LongName == p.Name
            );
            if (cParam == null)
            {
                parsingError = ParsingError.UnknownParameter;
                return null;
            }

            p.RefParameter = cParam;
        }

        if (parameters.Count(x => x.RefParameter.Mandatory) != AllParameters.Count(x => x.Mandatory))
        {
            // One of the mandatory parameter wasn't given!
            parsingError = ParsingError.MissingMandatoryParameter;
            return null;
        }

        parsingError = ParsingError.None;
        return parameters;
    }

    public abstract string Description { get; }

    public string Name { private set; get; }

    internal abstract CommandParameter[] AllParameters { get; }

    private enum ParsingError
    {
        None,
        MissingMandatoryParameter,
        UnknownParameter
    }
}