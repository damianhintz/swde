using System.Collections;
using System.Collections.Generic;

using swde.Komponenty;

namespace swde.Sekcje
{
    /// <summary>
    /// Kontekst danych – dane organizacyjne.
    /// </summary>
    internal class SekcjaMetadanychSwde : SekcjaSwdeBase, IEnumerable<MetadaneSwde>
    {
        protected List<MetadaneSwde> _metadane;

        /// <summary>
        /// Zwraca kolekcję metadanych.
        /// </summary>
        public IEnumerable<MetadaneSwde> Metadane { get { return _metadane; } }

        public SekcjaMetadanychSwde()
        {
            _metadane = new List<MetadaneSwde>();
        }

        /// <summary>
        /// Dodaj metadane.
        /// </summary>
        /// <param name="komponent"></param>
        public override void DodajMetadane(MetadaneSwde komponent)
        {
            _metadane.Add(komponent);
        }

        public IEnumerator<MetadaneSwde> GetEnumerator()
        {
            return _metadane.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
