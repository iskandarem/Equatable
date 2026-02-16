using System.Collections;

namespace Equatable
{
    /// <summary>
    /// An abstract class that provides a base implementation for value-based equality comparison 
    /// in derived classes by comparing a list of properties (Props). This class implements the 
    /// <see cref="IEquatable{Equatable}"/> interface to allow for custom equality checks.
    /// </summary>
    public abstract class BaseEquatable : IEquatable<BaseEquatable>
    {
        /// <summary>
        /// Gets the list of property values that will be used for equality comparison.
        /// Derived classes should override this property to provide the actual properties to compare.
        /// </summary>
        public abstract List<object?> Props { get; }

        /// <summary>
        /// Gets a value indicating whether the object's string representation should be constructed
        /// from its properties. Derived classes should override this property to control the stringification
        /// behavior of the object.
        /// </summary>
        public abstract bool? Stringify { get; }

        /// <summary>
        /// Compares the current instance with another <see cref="BaseEquatable"/> object for equality.
        /// The comparison is done by checking if the <see cref="Props"/> of both instances are equal.
        /// </summary>
        /// <param name="other">The other <see cref="BaseEquatable"/> instance to compare with.</param>
        /// <returns>
        /// <c>true</c> if the current instance and the other instance are considered equal;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(BaseEquatable? other)
        {
            return EqualsIterable(Props, other?.Props);
        }

        /// <summary>
        /// Compares the current instance with another object for equality. This method
        /// calls <see cref="Equals(object?)"/> if the object is of type <see cref="BaseEquatable"/>.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>
        /// <c>true</c> if the object is an <see cref="BaseEquatable"/> and is equal to the current instance;
        /// otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is BaseEquatable other) return Equals(other);
            return false;
        }

        /// <summary>
        /// Generates a hash code for the current instance based on the values in the <see cref="Props"/>.
        /// This method is used to ensure that equal instances have the same hash code, enabling
        /// proper use in collections such as <see cref="HashSet{T}"/> and <see cref="Dictionary{TKey, TValue}"/>.
        /// </summary>
        /// <returns>The hash code of the current instance.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var prop in Props)
                hash = hash * 31 + GetDeepHashCode(prop);
            return hash;
        }

        private static int GetDeepHashCode(object? obj)
        {
            if (obj == null)
                return 0;

            // If it’s a collection, combine hash codes of each element
            if (obj is IEnumerable enumerable && !(obj is string))
            {
                int hash = 17;
                foreach (var item in enumerable)
                    hash = hash * 31 + GetDeepHashCode(item);
                return hash;
            }

            // Normal object
            return obj.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the current instance. If <see cref="Stringify"/> is true,
        /// the string representation will be constructed using the <see cref="Props"/> of the object. 
        /// Otherwise, the base <see cref="Object.ToString"/> method is called.
        /// </summary>
        /// <returns>A string representation of the current instance.</returns>
        public override string? ToString()
        {
            if (Stringify != null && Stringify == true)
            {
                return string.Join(", ", Props);
            }
            return base.ToString();
        }


        private static bool EqualsIterable(ICollection<object?>? a, ICollection<object?>? b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            if (a.Count != b.Count) return false;
            for (int i = 0; i < a.Count; ++i)
            {
                var aElement = a.ElementAt(i);
                var bElement = b.ElementAt(i);
                if (ReferenceEquals(aElement, null) && ReferenceEquals(bElement, null)) continue;
                // If both elements are not null, check if they are equal
                if (ReferenceEquals(aElement, null) || ReferenceEquals(bElement, null)) return false;
                bool isAICollection = IsICollectionGeneric(aElement!.GetType());
                bool isBCollection = IsICollectionGeneric(bElement!.GetType());
                if (!ReferenceEquals(aElement, null) && (!bElement!.Equals(aElement) && !(isAICollection && isBCollection))) return false;
                if (isAICollection && isBCollection)
                {
                    var icollectionA = GetICollectionGeneric(aElement as ICollection);
                    var icollectionB = GetICollectionGeneric(bElement as ICollection);
                    if (!EqualsIterable(icollectionA, icollectionB)) return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Checks if the given type implements the generic ICollection interface.
        /// </summary>
        /// <param name="type">
        /// The type to check.
        /// </param>
        /// <returns> 
        /// <c>true</c> if the type implements <c>ICollection&lt;T&gt;</c>; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsICollectionGeneric(Type type)
        {
            return type.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                                i.GetGenericTypeDefinition() == typeof(ICollection<>));
        }
        /// <summary>
        /// Converts a non-generic ICollection to a generic ICollection of objects.
        /// This is used to handle collections of different types in the equality comparison.
        /// </summary>
        /// <param name="collection">
        /// The non-generic ICollection to convert.
        /// </param>
        /// <returns>
        /// A generic ICollection of objects containing the elements of the original collection.
        /// Returns null if the input collection is null.
        /// </returns>
        private static ICollection<object?>? GetICollectionGeneric(ICollection? collection)
        {
            if (collection == null) return null;
            var list = new List<object?>(collection.Count);
            foreach (var item in collection)
            {
                list.Add(item);
            }
            return list;
        }

    }
}