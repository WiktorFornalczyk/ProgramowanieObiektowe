System Transportowy "TransLogix"

-----------------------------------------------

Asset (klasa abstrakcyjna) – baza dla wszystkiego, co ma Id i nazwę.

Vehicle (dziedziczy po Asset) – dodaje numer rejestracyjny i stan licznika.

Truck (dziedziczy po Vehicle) – dodaje moc silnika i normę spalania.

Trailer (dziedziczy po Asset) – dodaje ładowność, numer rejestracyjny oraz typ (np. beczka, chłodnia).

HeavyTrailer (dziedziczy po Trailer) – dodaje liczbę osi i zezwolenia na gabaryty.

-----------------------------------------------

Driver (dziedziczy po Asset) – imię, nazwisko, PESEL, nr prawa jazdy.

Cargo (dziedziczy po Asset) – opis towaru, waga, czy kruchy, wymagania (np. temp.).

Route – klasa spinająca wszystko: Truck, Trailer, Driver, Cargo, StartLocation, EndLocation.

-----------------------------------------------

IRepository<T> – generyczny interfejs do operacji CRUD: GetAll(), GetById(int id), Add(T entity), Update(T entity), Delete(int id)

IImportExportable – interfejs dla klas obsługujących import/eksport plików CSV/XLS.
ExportToCsv(string path)
ImportFromCsv(string path)

-----------------------------------------------

TextFileRepository<T> – obsługa bazy na plikach tekstowych (każdy obiekt w nowej linii).

SqlRepository<T> – obsługa bazy SQL (np. przez Entity Framework lub Dapper).

-----------------------------------------------

DataValidator – statyczna klasa z metodami typu IsPeselValid(string pesel) czy IsPlateValid(string plate).

Własne Wyjątki (Custom Exceptions):
ValidationException – rzucany, gdy dane wejściowe są błędne.
DatabaseConnectionException – rzucany, gdy plik tekstowy lub SQL nie odpowiada.

-----------------------------------------------

IdGenerator – statyczna klasa generująca unikalne Id dla nowych obiektów.