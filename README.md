<a id="readme-top"></a>
# Demo Daten Generator

[![NET Framework](https://img.shields.io/badge/NET%20Core-%208.0-red.svg)](#)
[![Version](https://img.shields.io/badge/Version-%201.0.2025.1-blue.svg)](#)

Das Tool erstellt für eine Klasse Demodaten für den jeweiligen Typ.
Demodaten können für viele Zwecke, z.B. zum Test von Control um Daten für die Darstellen anzeigen zu können.
Demodaten können als Liste oder DataTable erstellt werden.

#### Beispiel für eine Model-Klasse
```
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

#### Die Demodaten werden erstellt und als IEnumerable<T> zurückgeben.
```
IEnumerable<UserDemoDaten> users = DemoDataGenerator<UserDemoDaten>.CreateForList<UserDemoDaten>(ConfigObject, 100);
foreach (UserDemoDaten user in users)
{
   Console.WriteLine($"{user.UserName};{user.Betrag.ToString("C2")};{user.IsDeveloper}");
}
```

#### Konfiguration der Demodaten über eine Callback-Klasse
```
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

