public class NullableStruct<T>
{
    public readonly T Value;
    public readonly bool IsSet;

    public NullableStruct(T value, bool isSet)
    {
        Value = value;
        IsSet = isSet;
    }
}