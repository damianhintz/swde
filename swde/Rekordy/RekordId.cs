using System;
using System.Collections.Generic;

namespace swde.Rekordy
{
    class RekordId : IComparable<RekordId>, IEquatable<RekordId>
    {
        private static int _uidBase = 0;

        private int _uid;

        private string _id;

        /// <summary>
        /// Identyfikator obiektu.
        /// </summary>
        public string Id { get { return _id; } }

        private string _idr;

        /// <summary>
        /// Identyfikator rekordu.
        /// </summary>
        public string Idr { get { return _idr; } }

        internal RekordId(string id, string idr)
        {
            _uid = NextUid();

            _id = id;
            _idr = idr;
        }

        private static int NextUid()
        {
            return ++_uidBase;
        }

        public static void Reset(int uidBase = 0)
        {
            _uidBase = 0;
        }

        public override int GetHashCode()
        {
            return _uid;
        }

        public override string ToString()
        {
            return _uid.ToString();
        }

        public int CompareTo(RekordId other)
        {
            return _uid.CompareTo(other._uid);
        }

        public bool Equals(RekordId other)
        {
            return _uid.Equals(other._uid);
        }
    }
}
