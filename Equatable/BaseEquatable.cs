using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Compares the current instance with another <see cref="global::BaseEquatable"/> object for equality.
        /// The comparison is done by checking if the <see cref="Props"/> of both instances are equal.
        /// </summary>
        /// <param name="other">The other <see cref="global::BaseEquatable"/> instance to compare with.</param>
        /// <returns>
        /// <c>true</c> if the current instance and the other instance are considered equal;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(BaseEquatable? other)
        {
            if (other == null) return false;
            if (ReferenceEquals(other, this)) return true;
            if (Props.Count != other.Props.Count) return false;
            for (int i = 0; i < Props.Count; ++i)
            {
                if (ReferenceEquals(Props[i], null) != ReferenceEquals(other.Props[i], null)) return false;
                if (!ReferenceEquals(Props[i], null) && !other.Props[i]!.Equals(Props[i])) return false;
            }
            return true;
        }

        /// <summary>
        /// Compares the current instance with another object for equality. This method
        /// calls <see cref="Equals(Equatable?)"/> if the object is of type <see cref="global::BaseEquatable"/>.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>
        /// <c>true</c> if the object is an <see cref="global::BaseEquatable"/> and is equal to the current instance;
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
            {
                //Combine the hash codes of the properties
                hash = hash * 31 + (prop?.GetHashCode() ?? 0);
            }
            return hash;
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

    }
}