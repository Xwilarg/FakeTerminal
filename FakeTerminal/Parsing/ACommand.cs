namespace FakeTerminal.Parsing;

public abstract class ACommand
{
    protected ACommand(string name)
    {
        Name = name;
    }

    public virtual bool DoAction(Client client, Parameter[] parameters, out string output)
    {
        var outParameters = ValidateParameters(parameters);
        if (outParameters == null)
        {
            output = "Some arguments failed validation";
            return false;
        }

        output = string.Empty;
        return true;
    }

    internal protected Parameter[]? ValidateParameters(Parameter[] parameters)
    {
        // Remove dupplicates
        parameters = parameters.Distinct(new ParameterEqualityComparer()).ToArray();

        foreach (var p in parameters)
        {
            var cParam = AllParameters.FirstOrDefault(x =>
                (x.ShortName.HasValue && x.ShortName.Value.ToString() == p.Name) ||
                x.LongName == p.Name
            );
            if (cParam == null) return null;

            p.RefParameter = cParam;
        }

        if (parameters.Count(x => x.RefParameter.Mandatory) != AllParameters.Count(x => x.Mandatory))
            return null; // One of the mandatory parameter wasn't given!

        return parameters;
    }

    public abstract string Description { get; }

    public string Name { private set; get; }

    internal abstract CommandParameter[] AllParameters { get; }
}