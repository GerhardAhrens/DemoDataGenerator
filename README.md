<a id="readme-top"></a>
# Demo Daten Generator

[![NET Framework](https://img.shields.io/badge/NET%20Core-%208.0-red.svg)](#)
[![Version](https://img.shields.io/badge/Version-%201.0.2025.10-blue.svg)](#)
![License](https://img.shields.io/badge/License-MIT-yellow.svg)
![VS2022](https://img.shields.io/badge/Visual%20Studio-2022-white.svg)

Das Tool erstellt f�r eine Klasse Demodaten f�r den jeweiligen Typ.
Demodaten k�nnen f�r viele Zwecke, z.B. zum Test von Control um Daten f�r die Darstellen anzeigen zu k�nnen.
Demodaten k�nnen als Liste oder DataTable erstellt werden.

### Demodaten Generator: Einsatzm�glichkeit

- CreateForList f�r eine Liste List< T>
- CreateForICollectionView f�r ein ICollectionView
- CreateForDataTable f�r eine DataTable auf Basis einer Modelklasse
- CreateForDictionary f�r ein Dictionary< Tkey, Tvalue> auf Basis eines KeyValuePair<Tkey, Tvalue >

#### Beispiel f�r eine Model-Klasse
```csharp
public class UserDemoDaten
{
        public string UserName { get; set; }
        public bool IsDeveloper { get; set; }
        public decimal Betrag { get; set; }
        public DateTime CreateOn { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
}
```

### Die Demodaten werden erstellt und als ein Collection Type zur�ckgegeben
#### Die Demodaten werden erstellt und als IEnumerable<T> zur�ckgeben.
```csharp
IEnumerable<UserDemoDaten> users = DemoDataGenerator<UserDemoDaten>.CreateForList<UserDemoDaten>(ConfigObject, 100);
foreach (UserDemoDaten user in users)
{
   Console.WriteLine($"{user.UserName};{user.Betrag.ToString("C2")};{user.IsDeveloper}");
}
```

#### Die Demodaten werden erstellt und als Dictionary< Tkey, Tvalue> zur�ckgeben.
```csharp
Dictionary<object, object> users = BuildDemoData<KeyValuePair<int, string>>.CreateForDictionary<KeyValuePair<int, string>>(ConfigObjectInt, 100);
foreach (KeyValuePair<object, object> user in users)
{
    Console.WriteLine($"{user.Key};{user.Value}");
}
```

#### Konfiguration der Demodaten �ber eine Callback-Klasse
```csharp
private static UserDemoDaten ConfigObject(UserDemoDaten demoDaten)
{
    var timeStamp = TestDataGenerator.SetTimeStamp();
    demoDaten.UserName = TestDataGenerator.Username();
    demoDaten.Betrag = TestDataGenerator.Currency(1_000, 10_000);
    demoDaten.IsDeveloper = TestDataGenerator.Boolean();
    demoDaten.CreateOn = timeStamp.CreateOn;
    demoDaten.CreateBy = timeStamp.CreateBy;
    demoDaten.ModifiedOn = timeStamp.ModifiedOn;
    demoDaten.ModifiedBy = timeStamp.ModifiedBy;

    return demoDaten;
}
```

#### Ergebnis

![Demodaten](DomodatenListOfT.png)

### Methoden zum Erstellen von Demodaten

|Methode|Typ|Beschreibung|
|:------|:--|:-----------|
|Letters()|string|Erstellen eines String mit Buchstaben|
|AlphabetAndNumeric()|string|erstellen eines Strings mit Buchstaben und Zahlen|
|Username()|string|Erstellen eines String im Format xxxx9999|
|Double()|double|Erstellen einer Double Zahl|
|Decimal()|decimal|Erstellen einer Decimal Zahl|
|Integer()|Int|Erstellen einer Int Zahl|
|Boolean()|bool|Erstellen eines True/False Inhalt|
|Word()|string|Erstellen eines Strings aus einer Wortliste|
|City()|string|Erstellen eines string mit einer Stadt|
|ColorName()|string|Farbname|
|Symbols()|string|Symbol aus PathGeometry Koordinaten|
|SetTimeStamp()|Tuple<>|Timestamp erstellen|

# History

![Version](https://img.shields.io/badge/Version-1.0.2025.10-blue.svg)
![Stand](https://img.shields.io/badge/Datum-20.04.2025.0-geen.svg)

- Demodaten f�r den Typ ICollectionView hinzugef�gt

![Version](https://img.shields.io/badge/Version-1.0.2025.1-blue.svg)
![Stand](https://img.shields.io/badge/Datum-02.01.2025.0-geen.svg)

- Erste Version des Demodatengenerator
