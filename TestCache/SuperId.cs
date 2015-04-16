using System;
using Nucleus.Data;
using Nucleus.JetBrains;

namespace TestCache
{
    /// <summary>
    ///     Strongly typed SuperId
    /// </summary>
    [StronglyTypedObject(typeof(int)), UsedImplicitly]
    public class SuperId : IEquatable<SuperId>
    {
        private readonly int _data;

        private SuperId(int data)
        {
            _data = data;
        }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(SuperId other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || Equals(other._data, _data);
        }

        /// <summary>
        ///     The op_ implicit.
        /// </summary>
        /// <param name="data">
        ///     The data.
        /// </param>
        /// <returns> The strongly typed data </returns>
        public static implicit operator SuperId(int data)
        {
            return new SuperId(data);
        }

        /// <summary>
        ///     The op_ implicit.
        /// </summary>
        /// <param name="data">
        ///     The data.
        /// </param>
        /// <returns> The strongly typed data </returns>
        public static implicit operator int(SuperId data)
        {
            return ReferenceEquals(data, null) ? default(int) : data._data;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        ///     true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
        /// </returns>
        /// <param name="obj">
        ///     The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.
        /// </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var objType = obj.GetType();
            var thisType = typeof(SuperId);
            var dataTypeType = typeof(int);
            return
                (objType == thisType && Equals((SuperId)obj))
                || (objType == dataTypeType && Equals((int)obj));
        }

        /// <summary>
        ///     Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        ///     A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return (_data != default(int) ? _data.GetHashCode() : 0);
        }

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator ==(SuperId left, SuperId right)
        {
            if (ReferenceEquals(left, null)) return ReferenceEquals(right, null);
            return ReferenceEquals(left, right) || left.Equals(right);
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator !=(SuperId left, SuperId right)
        {
            return !(left == right);
        }
    }
}
