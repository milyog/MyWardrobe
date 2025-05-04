namespace MyWardrobe.ErrorHandling
{
    public static class WardrobeItemErrors
    {
        public static Error NotFound(Guid id) =>  new Error(
            "WardrobeItem.NotFound", $"Post med id {id} hittades ej.");

        public static Error NotFound() => new Error(
            "WardrobeItem.NotFound", "Inga poster hittades.");
    }
}
