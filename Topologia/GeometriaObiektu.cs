using System.Collections.Generic;
using GeoAPI.Geometries;
using NetTopologySuite.IO;

namespace Topologia
{
    public class GeometriaObiektu
    {
        private string _wkt;
        private IGeometry _geometria;

        public GeometriaObiektu(string wkt)
        {
            _wkt = wkt.Trim();
            WKTReader wktReader = new WKTReader();
            _geometria = wktReader.Read(_wkt);
            if (!_geometria.IsValid) { Logger.writeTopo("Nieprawidłowa geometria: " + _wkt); }
            if (_geometria.IsEmpty) { Logger.writeTopo("Pusta geometria: " + _wkt); }
        }

        public string toWkt() { return _wkt; }
        public IGeometry geometry() { return _geometria; }
        public bool overlaps(IEnumerable<IGeometry> geometrie)
        {
            foreach (var geometria in geometrie)
            {
                if (_geometria.Overlaps(geometria)) Logger.writeTopo("Przecięcie geometrii: " + _wkt);
                if (_geometria.EqualsTopologically(geometria)) Logger.writeTopo("Nachodzenie podobnych geometrii: " + _wkt);
            }
            return true;
        }
    }
}
