using System;

using swde.Rekordy;

namespace swde
{
    /// <summary>
    /// Reprezentuje identyfikator obiektu SWDE.
    /// </summary>
    public class ObiektId : IComparable<ObiektId>, IEquatable<ObiektId>
    {
        private RekordId _rekordId;

        internal ObiektId(RekordId rekordId)
        {
            _rekordId = rekordId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _rekordId.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(ObiektId other)
        {
            return _rekordId.CompareTo(other._rekordId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ObiektId other)
        {
            return _rekordId.Equals(other._rekordId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("ID:{0}, IDR:{1}", _rekordId.Id, _rekordId.Idr);
        }
    }
}
