public sealed class Maybe<T> : IEquatable<Maybe<T>>
{
    private readonly T _value;

    private Maybe(T value) => _value = value;

    public bool HasValue => !HasNoValue;

    public bool HasNoValue => _value is null;

    public T Value => HasValue
        ? _value
        : throw new InvalidOperationException("The value cannot be accessed because it does not exist.");

    public static Maybe<T> None => new Maybe<T>(default);

    // Check if the value is an activator instance
    public bool IsActivatorInstance => Equals(_value, Activator.CreateInstance<T>());

    // Static factory method to create a Maybe<T> from a value
    public static Maybe<T> From(T value) => new Maybe<T>(value);

    public static implicit operator Maybe<T>(T value) => From(value);

    public static implicit operator T(Maybe<T> maybe) => maybe.Value;

    public bool Equals(Maybe<T> other)
    {
        if (other is null) return false;
        if (HasNoValue && other.HasNoValue) return true;
        if (HasNoValue || other.HasNoValue) return false;
        return Value.Equals(other.Value);
    }

    public override bool Equals(object obj) =>
        obj switch
        {
            null => false,
            T value => Equals(new Maybe<T>(value)),
            Maybe<T> maybe => Equals(maybe),
            _ => false
        };

    public override int GetHashCode() => HasValue ? Value.GetHashCode() : 0;
}
