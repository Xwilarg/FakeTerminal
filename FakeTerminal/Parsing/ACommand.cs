namespace FakeTerminal.Parsing;

public abstract class ACommand
{
    protected ACommand(string name)
    {
        Name = name;
    }

    public abstract bool DoAction(Client client, Parameter[] parameters, out string output);

    internal protected Parameter[]? ValidateParameters(Parameter[] parameters)
    {
        var validationsRequired = AllParameters.Count(x => x.Mandatory);

        // Remove dupplicates
        parameters = parameters.Distinct(new ParameterEqualityComparer()).ToArray();

        foreach (var p in parameters)
        {
            var cParam = AllParameters.FirstOrDefault(x =>
                (x.ShortName.HasValue && x.ShortName.Value.ToString() == p.Name) ||
                x.LongName == p.Name
            );
            if (cParam == null) return null;

            p.Description = cParam.Description;
            if (cParam.Mandatory) validationsRequired--;
        }

        if (validationsRequired > 0) return null; // One of the mandatory parameter wasn't given!

        return parameters;
    }

    public abstract string Description { get; }

    public string Name { private set; get; }

    internal abstract CommandParameter[] AllParameters { get; }
}