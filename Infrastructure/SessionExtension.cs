using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Miljoboven.Infrastructure
{
    // Static klass för att utöka ISession med metoder för att spara och hämta komplexa objekt
    public static class SessionExtension
    {
        // Metod för att spara ett objekt av valfri typ i sessionen
        public static void Set<T> (this ISession session, string key, T value)
        {
            // Serialiserar objektet till en JSON-sträng och sparar det under den angivna nyckeln
            session.SetString(key, JsonSerializer.Serialize (value));
        }

        // Metod för att hämta ett objekt av valfri typ från sessionen
        public static T? Get<T> (this ISession session, string key)
        {
            // Hämtar JSON-strängen från sessionen baserat på nyckeln
            var value = session.GetString(key);
            // Returnerar deserialiserat objekt av typen T om det finns, annars returneras null (default)
            return value == null ? default : JsonSerializer.Deserialize<T> (value);
        }
    }
}
