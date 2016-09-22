using System;
using System.Linq;
using Pragmatic.Kontrakty;

namespace egib
{
    public class Klasouzytek : ObiektPowierzchniowy, IEquatable<Klasouzytek>, IComparable<Klasouzytek>
    {
        private string _ofu;
        private string _ozu;
        private string _ozk;

        public Klasouzytek(string ofu, string ozu, string ozk, Powierzchnia powierzchnia)
            : base(powierzchnia)
        {
            _ofu = ofu;
            _ozu = ozu;
            _ozk = ozk;
        }

        public string ofu() { return _ofu; }
        public string ozu() { return _ozu; }
        public string ozk() { return _ozk; }
        public string oznaczenie(FabrykaKlasouzytku klu) { return klu.map(_ofu, _ozu, _ozk); }

        public bool Equals(Klasouzytek other)
        {
            return other.CompareTo(this) == 0;
        }

        public override bool Equals(object obj)
        {
            return (obj as Klasouzytek).Equals(this);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public int CompareTo(Klasouzytek other)
        {
            return string.Compare(ToString(), other.ToString());
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", ofu(), ozu(), ozk());
        }

        public bool jestZabudowany()
        {
            string klu = ToString();
            if (klu.StartsWith("(Bz")) return false;
            if (klu.Contains(",Bz")) return false;
            return klu.StartsWith("(B") || klu.Contains(",B");
        }

        public bool jestLs()
        {
            return ToString().Contains("Ls");
        }
    }
}
