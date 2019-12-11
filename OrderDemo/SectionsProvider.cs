using System.Collections.Generic;

namespace OrderDemo
{
    public static class SectionsProvider
    {
        public static IReadOnlyCollection<Section> GetSections()
        {
            return new List<Section>
            {
                new Section(1, "Section 1", null),
                new Section(2, "Section 2", 1),
                new Section(3, "Section 3", 2),
                new Section(4, "Section 4", 3),
                new Section(5, "Section 5", 4)

            };
        }
    }

    public class Section
    {
        public Section(int id, string name, int? previousId)
        {
            Id = id;
            Name = name;
            PreviousId = previousId;
        }

        public int Id { get; }
        public string Name { get; }
        public int? PreviousId { get; }
    }
}