using System.Text.Json;
using TowardAgarioStepTwo;

//Person person = new Person("Aspen", (float)3.6);

//string message = JsonSerializer.Serialize(person);
//Console.WriteLine(message);

//Person temp = JsonSerializer.Deserialize<Person>(message) ?? throw new Exception("bad json");

//List<Person> persons = new();
//persons.Add(new Person("Jim", (float)3.0));
//persons.Add(new Person("Dav", (float)3.3));
//persons.Add(new Person("Erin", (float)3.6));
//persons.Add(new Person("Mary", (float)3.8));
//persons.Add(new Person("Pat", (float)3.2));

//string list = JsonSerializer.Serialize(persons, new JsonSerializerOptions {WriteIndented = true});
//List<Person> deserializedPersons = JsonSerializer.Deserialize<List<Person>>(list) ?? throw new Exception("bad json");

Person p = new Student((float)3.0, 4, "Jimmy");
string pSerialized = JsonSerializer.Serialize(p);

Console.ReadLine();