using Equatable;

class MyDictionaryClass : BaseEquatable
{
    public Dictionary<object, object>? MyProp { get; set; }
    public override List<object?> Props => [MyProp];

    public override bool? Stringify => true;
}