using System;
using DotNetCoords.Datum;

namespace DotNetCoords
{
  /// <summary>
  /// Enumerated type used to indicate the required precision
  /// </summary>
  public enum Precision
  {
    /// <summary>
    /// Used to indicate a required precision of 10000m (10km).
    /// </summary>
    Precision10000M = 10000,
    /// <summary>
    /// Used to indicate a required precision of 1000m (1km).
    /// </summary>
    Precision1000M = 1000,
    /// <summary>
    /// Used to indicate a required precision of 100m.
    /// </summary>
    Precision100M = 100,
    /// <summary>
    /// Used to indicate a required precision of 10m.
    /// </summary>
    Precision10M = 10,
    /// <summary>
    /// Used to indicate a required precision of 1m.
    /// </summary>
    Precision1M = 1
  }

  /// <summary>
  /// Class to represent a Military Grid Reference System (MGRS) reference.
  /// <p>
  /// 	<h3>Military Grid Reference System (MGRS)</h3>
  /// </p>
  /// <p>
  /// The Military Grid Reference System (MGRS) is an extension of the Universal
  /// Transverse Mercator (UTM) reference system. An MGRS reference is made from 5
  /// parts:
  /// </p>
  /// <h4>UTM Longitude Zone</h4>
  /// <p>
  /// This is a number indicating which UTM longitude zone the reference falls
  /// into. Zones are numbered from 1 (starting at 180°W) through 60. Each zone
  /// is 6° wide.
  /// </p>
  /// <h4>UTM Latitude Zone</h4>
  /// <p>
  /// Latitude is split into regions that are 8° high, starting at 80°S.
  /// Latitude zones are lettered using C through X, but omitting I and O as they
  /// can easily be confused with the numbers 1 and 0.
  /// </p>
  /// <h4>100,000m Square identification</h4>
  /// <p>
  /// Each UTM zone is treated as a square 100,000m to a side. The 50,000m easting
  /// is centred on the centre-point of the UTM zone. 100,000m squares are
  /// identified using two characters - one to identify the row and one to identify
  /// the column.
  /// </p>
  /// <p>
  /// Row identifiers use the characters A through V (omitting I and O again). The
  /// sequence is repeated every 2,000,000m from the equator. If the UTM longitude
  /// zone is odd, then the lettering is advanced by five characters to start at F.
  /// </p>
  /// <p>
  /// Column identifiers use the characters A through Z (again omitting I and O).
  /// </p>
  /// <h4>Easting and northing</h4>
  /// <p>
  /// Each 100,000m grid square is further divided into smaller squares
  /// representing 1m, 10m, 100m, 1,000m and 10,000m precision. The easting and
  /// northing are given using the numeric row and column reference of the square,
  /// starting at the bottom-left corner of the square.
  /// </p>
  /// <h4>MGRS Reference Example</h4>
  /// <p>
  /// 18SUU8362601432 is an example of an MGRS reference. '18' is the UTM longitude
  /// zone, 'S' is the UTM latitude zone, 'UU' is the 100,000m square
  /// identification, 83626 is the easting reference to 1m precision and 01432 is
  /// the northing reference to 1m precision.
  /// </p>
  /// <h3>MGRSRef</h3>
  /// <p>
  /// Methods are provided to query an <see cref="MGRSRef" /> object for its
  /// parameters. As MGRS references are related to UTM references, a
  /// <see cref="MGRSRef.ToUTMRef" /> method is provided to
  /// convert an <see cref="MGRSRef" /> object into a <see cref="UTMRef" />
  /// object. The reverse conversion can be made using the
  /// <see cref="MGRSRef(UTMRef, bool)" /> constructor.
  /// </p>
  /// <p>
  /// 	<see cref="MGRSRef" /> objects can be converted to
  /// <see cref="LatLng" /> objects using the
  /// <see cref="MGRSRef.ToLatLng" /> method. The reverse
  /// conversion is made using the
  /// <see cref="LatLng.ToMGRSRef" /> method.
  /// </p>
  /// <p>
  /// Some MGRS references use the Bessel 1841 ellipsoid rather than the Geodetic
  /// Reference System 1980 (GRS 1980), International or World Geodetic System 1984
  /// (WGS84) ellipsoids. Use the constructors with the optional boolean parameter
  /// to be able to specify whether your MGRS reference uses the Bessel 1841
  /// ellipsoid. Note that no automatic determination of the correct ellipsoid to
  /// use is made.
  /// </p>
  /// <p>
  /// 	<b>Important note</b>: There is currently no support for MGRS references in
  /// polar regions north of 84°N and south of 80°S. There is also no
  /// account made for UTM zones with slightly different sizes to normal around
  /// Svalbard and Norway.
  /// </p>
  /// </summary>
  public sealed class MGRSRef : CoordinateSystem 
  {
    /**
     * The initial precision of this MGRS reference. Must be one of
     * MGRSRef.PRECISION_1M, MGRSRef.PRECISION_10M, MGRSRef.PRECISION_100M,
     * MGRSRef.PRECISION_1000M or MGRSRef.PRECISION_10000M.
     */

    /**
     * 
     */
    private readonly bool _isBessel;

    /**
     * Northing characters
     */
    private static readonly char[] NorthingIDs =
        { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M',
            'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V' };

    /// <summary>
    /// Create a new MGRS reference object from the given UTM reference. It is
    /// assumed that this MGRS reference represents a point using the GRS 1980,
    /// International or WGS84 ellipsoids. It is assumed that the UTMRef object is
    /// valid.
    /// </summary>
    /// <param name="utm">A UTM reference.</param>
    /// <param name="isBessel">true if the parameters represent an MGRS reference using the
    /// Bessel 1841 ellipsoid; false is the parameters represent an MGRS
    /// reference using the GRS 1980, International or WGS84 ellipsoids.</param>
    public MGRSRef(UTMRef utm, bool isBessel = false) : base(utm.Datum)
    {
      var lngZone = utm.LngZone;
      var set = ((lngZone - 1) % 6) + 1;
      var eId =
          (int) Math.Floor(utm.Easting / 100000.0) + (8 * ((set - 1) % 3));
      var nId = (int) Math.Floor((utm.Northing % 2000000) / 100000.0);

      if (eId > 8)
        eId++; // Offset for no I character
      if (eId > 14)
        eId++; // Offset for no O character

      var eIDc = (char) (eId + 64);

      // Northing ID offset for sets 2, 4 and 6
      if (set % 2 == 0) {
        nId += 5;
      }

      if (isBessel) {
        nId += 10;
      }
      
      if (nId > 19) {
        nId -= 20;
      }

      var nIDc = NorthingIDs[nId];

      UtmZoneNumber = lngZone;
      UtmZoneChar = utm.LatZone;
      EastingId = eIDc;
      NorthingId = nIDc;
      Easting = (int) Math.Round(utm.Easting) % 100000;
      Northing = (int) Math.Round(utm.Northing) % 100000;
      Precision = Precision.Precision1M;
      _isBessel = isBessel;
    }

    /// <summary>
    /// Create a new MGRS reference object from the given parameters. An
    /// ArgumentException is thrown if any of the parameters are invalid.
    /// </summary>
    /// <param name="utmZoneNumber">The UTM zone number representing the longitude.</param>
    /// <param name="utmZoneChar">The UTM zone character representing the latitude.</param>
    /// <param name="eastingId">Character representing the 100,000km easting square.</param>
    /// <param name="northingId">Character representing the 100,000km northing square.</param>
    /// <param name="easting">Easting in metres.</param>
    /// <param name="northing">Northing in metres.</param>
    /// <param name="precision">The precision of the given easting and northing.</param>
    /// <param name="isBessel">true if the parameters represent an MGRS reference using the
    /// Bessel 1841 ellipsoid; false is the parameters represent an MGRS
    /// reference using the GRS 1980, International or WGS84 ellipsoids.</param>
    /// <exception cref="ArgumentException">If any of the given parameters are invalid. Note that the
    /// parameters are only checked for the range of values that they can
    /// take on. Being able to create an MGRSRef object does not
    /// necessarily imply that the reference is valid.</exception>
    public MGRSRef(int utmZoneNumber, char utmZoneChar, char eastingId,
        char northingId, int easting, int northing, Precision precision,
        bool isBessel = false) : base(WGS84Datum.Instance)
    {
      if (utmZoneNumber < 1 || utmZoneNumber > 60) {
        throw new ArgumentException("Invalid utmZoneNumber ("
            + utmZoneNumber + ")");
      }
      if (utmZoneChar < 'A' || utmZoneChar > 'Z') {
        throw new ArgumentException("Invalid utmZoneChar (" + utmZoneChar
            + ")");
      }
      if (eastingId < 'A' || eastingId > 'Z' || eastingId == 'I'
          || eastingId == 'O') {
            throw new ArgumentException("Invalid eastingId (" + eastingId
            + ")");
      }
      if (northingId < 'A' || northingId > 'Z' || northingId == 'I'
          || northingId == 'O') {
            throw new ArgumentException("Invalid northingID (" + northingId
            + ")");
      }
      if (easting < 0 || easting > 99999) {
        throw new ArgumentException("Invalid easting (" + easting + ")");
      }
      if (northing < 0 || northing > 99999) {
        throw new ArgumentException("Invalid northing (" + northing + ")");
      }
  
      UtmZoneNumber = utmZoneNumber;
      UtmZoneChar = utmZoneChar;
      EastingId = eastingId;
      NorthingId = northingId;
      Easting = easting;
      Northing = northing;
      Precision = precision;
      _isBessel = isBessel;
    }


    /// <summary>
    /// Create a new MGRS reference object from the given String. Must be correctly
    /// formatted otherwise an ArgumentException will be thrown.
    ///
    /// Matching regex: (\d{1,2})([A-Z])([A-Z])([A-Z])(\d{2,10})
    /// </summary>
    /// <param name="gridRef">a string to create an MGRS reference from.</param>
    /// <param name="isBessel">True if the parameters represent an MGRS reference using the
    /// Bessel 1841 ellipsoid; false is the parameters represent an MGRS
    /// reference using the GRS 1980, International or WGS84 ellipsoids.</param>
    /// <exception cref="ArgumentException">if the given String is not correctly formatted.</exception>
    public MGRSRef(string gridRef, bool isBessel = false) : base(WGS84Datum.Instance) 
    {
      if (string.IsNullOrEmpty(gridRef) || gridRef.Length < 6)
        throw new ArgumentException("Invalid MGRS reference (" + gridRef + ")", nameof(gridRef));
      var begin = 0;
      var length = 1;
      var gridRefArray = gridRef.ToCharArray();
      if (char.IsDigit(gridRefArray[1]))
        length = 2;
      UtmZoneNumber = int.Parse(gridRef.Substring(begin, length));
      begin += length;
      if (!(char.IsUpper(gridRefArray[begin]) &&
          char.IsUpper(gridRefArray[begin + 1]) &&
          char.IsUpper(gridRefArray[begin + 2])))
        throw new ArgumentException("Invalid MGRS reference (" + gridRef + ")", nameof(gridRef));
      UtmZoneChar = gridRefArray[begin];
      EastingId = gridRefArray[begin + 1];
      NorthingId = gridRefArray[begin + 2];
      begin += 3;
      for (length = 0; begin + length < gridRefArray.Length; length++)
      {
        if (!char.IsDigit(gridRefArray[begin + length]))
          throw new ArgumentException("Invalid MGRS reference (" + gridRef + ")", nameof(gridRef));
      }
      if (length < 2 || length % 2 != 0)
        throw new ArgumentException("Invalid MGRS reference (" + gridRef + ")", nameof(gridRef));
      Precision = (Precision)Math.Pow(10, 5 - (length / 2));
      Easting =
          int.Parse(gridRef.Substring(begin, length / 2)) * (int)Precision;
      Northing = int.Parse(gridRef.Substring(begin + length / 2)) * (int)Precision;
      _isBessel = isBessel;
    }

    const int LetterA = 0;   /* ARRAY INDEX FOR LETTER A               */
    const int LetterB               = 1;   /* ARRAY INDEX FOR LETTER B               */
    const int LetterC               =2;   /* ARRAY INDEX FOR LETTER C               */
    const int LetterD               =3;   /* ARRAY INDEX FOR LETTER D               */
    const int LetterE               =4;   /* ARRAY INDEX FOR LETTER E               */
    const int LetterF               =5;   /* ARRAY INDEX FOR LETTER E               */
    const int LetterG               =6;   /* ARRAY INDEX FOR LETTER H               */
    const int LetterH               =7;   /* ARRAY INDEX FOR LETTER H               */
    const int LetterI               =8;   /* ARRAY INDEX FOR LETTER I               */
    const int LetterJ               =9;   /* ARRAY INDEX FOR LETTER J               */
    const int LetterK              =10;   /* ARRAY INDEX FOR LETTER J               */
    const int LetterL              =11;   /* ARRAY INDEX FOR LETTER L               */
    const int LetterM              =12;   /* ARRAY INDEX FOR LETTER M               */
    const int LetterN              =13;   /* ARRAY INDEX FOR LETTER N               */
    const int LetterO              =14;   /* ARRAY INDEX FOR LETTER O               */
    const int LetterP              =15;   /* ARRAY INDEX FOR LETTER P               */
    const int LetterQ              =16;   /* ARRAY INDEX FOR LETTER Q               */
    const int LetterR              =17;   /* ARRAY INDEX FOR LETTER R               */
    const int LetterS              =18;   /* ARRAY INDEX FOR LETTER S               */
    const int LetterT              =19;   /* ARRAY INDEX FOR LETTER S               */
    const int LetterU              =20;   /* ARRAY INDEX FOR LETTER U               */
    const int LetterV              =21;   /* ARRAY INDEX FOR LETTER V               */
    const int LetterW              =22;   /* ARRAY INDEX FOR LETTER W               */
    const int LetterX              =23;   /* ARRAY INDEX FOR LETTER X               */
    const int LetterY              =24;   /* ARRAY INDEX FOR LETTER Y               */
    const int LetterZ = 25;   /* ARRAY INDEX FOR LETTER Z               */

    private struct LatitudeBand
    {
      private long _letter;            /* letter representing latitude band  */
      internal readonly double MinNorthing;    /* minimum northing for latitude band */
      internal double North;           /* upper latitude for latitude band   */
      internal double South;           /* lower latitude for latitude band   */

      public LatitudeBand(long letter, double minNorthing, double north, double south)
      {
        _letter = letter; 
        MinNorthing = minNorthing; 
        North = north;
        South = south;
      }
    }

    private readonly LatitudeBand[] _latitudeBandTable = {
        new LatitudeBand(LetterC, 1100000.0, -72.0, -80.5), 
        new LatitudeBand(LetterD, 2000000.0, -64.0, -72.0),
        new LatitudeBand(LetterE, 2800000.0, -56.0, -64.0),
        new LatitudeBand(LetterF, 3700000.0, -48.0, -56.0),
        new LatitudeBand(LetterG, 4600000.0, -40.0, -48.0),
        new LatitudeBand(LetterH, 5500000.0, -32.0, -40.0),
        new LatitudeBand(LetterJ, 6400000.0, -24.0, -32.0),
        new LatitudeBand(LetterK, 7300000.0, -16.0, -24.0),
        new LatitudeBand(LetterL, 8200000.0, -8.0, -16.0),
        new LatitudeBand(LetterM, 9100000.0, 0.0, -8.0),
        new LatitudeBand(LetterN, 0.0, 8.0, 0.0),
        new LatitudeBand(LetterP, 800000.0, 16.0, 8.0),
        new LatitudeBand(LetterQ, 1700000.0, 24.0, 16.0),
        new LatitudeBand(LetterR, 2600000.0, 32.0, 24.0),
        new LatitudeBand(LetterS, 3500000.0, 40.0, 32.0),
        new LatitudeBand(LetterT, 4400000.0, 48.0, 40.0),
        new LatitudeBand(LetterU, 5300000.0, 56.0, 48.0),
        new LatitudeBand(LetterV, 6200000.0, 64.0, 56.0),
        new LatitudeBand(LetterW, 7000000.0, 72.0, 64.0),
        new LatitudeBand(LetterX, 7900000.0, 84.5, 72.0)
      };

    void Get_Latitude_Band_Min_Northing(char letter, out double minNorthing)
    /*
    * The function Get_Latitude_Band_Min_Northing receives a latitude band letter
    * and uses the Latitude_Band_Table to determine the minimum northing for that
    * latitude band letter.
    *
    *   letter        : Latitude band letter             (input)
    *   min_northing  : Minimum northing for that letter(output)
    */
    { /* Get_Latitude_Band_Min_Northing */

      var letterVal = letter - 65;
      if ((letter >= 'C') && (letter <= 'H'))
        minNorthing = _latitudeBandTable[letterVal - 2].MinNorthing;
      else if ((letter >= 'J') && (letter <= 'N'))
        minNorthing = _latitudeBandTable[letterVal - 3].MinNorthing;
      else if ((letter >= 'P') && (letter <= 'X'))
        minNorthing = _latitudeBandTable[letterVal - 4].MinNorthing;
      else
        throw new Exception("String error");
    } /* Get_Latitude_Band_Min_Northing */

    void Get_Grid_Values(out long ltr2LowValue, out long ltr2HighValue, out double falseNorthing)
    /*
    * The function Get_Grid_Values sets the letter range used for 
    * the 2nd letter in the MGRS coordinate string, based on the set 
    * number of the utm zone. It also sets the false northing using a
    * value of A for the second letter of the grid square, based on 
    * the grid pattern and set number of the utm zone.
    *
    *    zone            : Zone number             (input)
    *    ltr2_low_value  : 2nd letter low number   (output)
    *    ltr2_high_value : 2nd letter high number  (output)
    *    false_northing  : False northing          (output)
    */
    { /* BEGIN Get_Grid_Values */
      bool aa_pattern;    /* Pattern based on ellipsoid code */

      long setNumber = UtmZoneNumber % 6;

      if (setNumber == 0)
        setNumber = 6;

      //if (!strcmp(MGRS_Ellipsoid_Code, CLARKE_1866) || !strcmp(MGRS_Ellipsoid_Code, CLARKE_1880) ||
      //  !strcmp(MGRS_Ellipsoid_Code, BESSEL_1841) || !strcmp(MGRS_Ellipsoid_Code, BESSEL_1841_NAMIBIA))
      //  aa_pattern = false;
      //else
        aa_pattern = true;

      ltr2LowValue = 0;
      ltr2HighValue = 0;
      switch (setNumber)
      {
        case 1:
        case 4:
          ltr2LowValue = LetterA;
          ltr2HighValue = LetterH;
          break;
        case 2:
        case 5:
          ltr2LowValue = LetterJ;
          ltr2HighValue = LetterR;
          break;
        case 3:
        case 6:
          ltr2LowValue = LetterS;
          ltr2HighValue = LetterZ;
          break;
      }

      /* False northing at A for second letter of grid square */
      if (aa_pattern)
      {
        if ((setNumber % 2) == 0)
          falseNorthing = 1500000.0;
        else
          falseNorthing = 0.0;
      }
      else
      {
        if ((setNumber % 2) == 0)
          falseNorthing = 500000.0;
        else
          falseNorthing = 1000000.00;
      }
    } /* END OF Get_Grid_Values */

    /// <summary>
    /// Convert this MGRS reference to an equivalent UTM reference.
    /// This method based on http://www.stellman-greene.com/mgrs_to_utm/
    /// </summary>
    /// <returns>The equivalent UTM reference.</returns>
    public UTMRef ToUTMRef() 
    {
      var e = EastingId - 65;
      if (e >= 15)
        e--;
      if (e >= 9)
        e--;

      var ex = (Easting + ((e % 8 + 1) * 100000)) % 1000000;

      long ltr2LowValue;
      long ltr2HighValue;
      Get_Grid_Values(out ltr2LowValue, out ltr2HighValue, out var falseNorthing);

      /* Check that the second letter of the MGRS string is within
      * the range of valid second letter values 
      * Also check that the third letter is valid */
      //if ((letters[1] < ltr2_low_value) || (letters[1] > ltr2_high_value) || (letters[2] > LETTER_V))
      //  error_code |= MGRS_STRING_ERROR;

      var offsetNorthing = NorthingId - 65;
      var gridNorthing = offsetNorthing * 100000.0 + falseNorthing;

      if (offsetNorthing > LetterO)
        gridNorthing = gridNorthing - 100000.0;

      if (offsetNorthing > LetterI)
        gridNorthing = gridNorthing - 100000.0;

      if (gridNorthing >= 2000000.0)
        gridNorthing = gridNorthing - 2000000.0;

      Get_Latitude_Band_Min_Northing(UtmZoneChar, out var minNorthing);

      var scaledMinNorthing = minNorthing;
      while (scaledMinNorthing >= 2000000.0)
      {
        scaledMinNorthing = scaledMinNorthing - 2000000.0;
      }

      gridNorthing = gridNorthing - scaledMinNorthing;
      if (gridNorthing < 0.0)
        gridNorthing = gridNorthing + 2000000.0;

      gridNorthing = minNorthing + gridNorthing + Northing;

      return new UTMRef(UtmZoneNumber, UtmZoneChar, ex, gridNorthing);
    }

    /// <summary>
    /// Convert this MGRS reference to a latitude and longitude.
    /// </summary>
    /// <returns>
    /// The converted latitude and longitude.
    /// </returns>
    public override LatLng ToLatLng() 
    {
      return ToUTMRef().ToLatLng();
    }

    /// <summary>
    /// Return a string representation of this MGRS Reference to whatever precision
    /// this reference is set to.
    /// </summary>
    /// <returns>
    /// a string representation of this MGRS reference to whatever
    /// precision this reference is set to.
    /// </returns>
    public override string ToString() 
    {
      return ToString(Precision);
    }


    /// <summary>
    /// Return a String representation of this MGRS reference to 1m, 10m, 100m,
    /// 1000m or 10000m precision.
    /// </summary>
    /// <param name="precision">The required precision.</param>
    /// <returns>A string representation of this MGRS reference to the required
    /// precision.</returns>
    public string ToString(Precision precision) 
    {
      var eastingR = (int) Math.Floor((double)(Easting / (int)precision));
      var northingR = (int) Math.Floor((double)(Northing / (int)precision));

      var padding = 5;

      switch (precision) 
      {
        case Precision.Precision10M:
          padding = 4;
          break;
        case Precision.Precision100M:
          padding = 3;
          break;
        case Precision.Precision1000M:
          padding = 2;
          break;
        case Precision.Precision10000M:
          padding = 1;
          break;
      }

      var eastingRs = eastingR.ToString();
      var ez = padding - eastingRs.Length;
      while (ez > 0) {
        eastingRs = "0" + eastingRs;
        ez--;
      }

      var northingRs = northingR.ToString();
      var nz = padding - northingRs.Length;
      while (nz > 0) {
        northingRs = "0" + northingRs;
        nz--;
      }

      var utmZonePadding = "";
      if (UtmZoneNumber < 10) {
        utmZonePadding = "0";
      }

      return utmZonePadding + UtmZoneNumber + UtmZoneChar
          + EastingId + NorthingId
          + eastingRs + northingRs;
    }

    /// <summary>
    /// Gets the easting.
    /// </summary>
    /// <value>The easting.</value>
    public int Easting { get; }

    /// <summary>
    /// Gets the easting ID.
    /// </summary>
    /// <value>The easting ID.</value>
    public char EastingId { get; }

    /// <summary>
    /// Determines whether this instance represents an MGRS reference using the
    /// Bessel 1841 ellipsoid.
    /// </summary>
    /// <returns>
    /// 	<c>true</c> if the instance represents an MGRS reference using the
    /// Bessel 1841 ellipsoid; <c>false</c> if the instance represents an MGRS
    /// reference using the GRS 1980, International or WGS84 ellipsoids.
    /// </returns>
    public bool IsBessel() 
    {
      return _isBessel;
    }

    /// <summary>
    /// Gets the northing.
    /// </summary>
    /// <value>The northing.</value>
    public int Northing { get; }

    /// <summary>
    /// Gets the northing ID.
    /// </summary>
    /// <value>The northing ID.</value>
    public char NorthingId { get; }

    /// <summary>
    /// Gets the precision.
    /// </summary>
    /// <value>The precision.</value>
    public Precision Precision { get; }

    /// <summary>
    /// Gets the UTM zone character representing the latitude.
    /// </summary>
    /// <value>The UTM zone character representing the latitude.</value>
    public char UtmZoneChar { get; }

    /// <summary>
    /// Gets the UTM zone number representing the longitude.
    /// </summary>
    /// <value>The UTM zone number representing the longitude.</value>
    public int UtmZoneNumber { get; }
  }
}
