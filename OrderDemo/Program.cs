using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace OrderDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IReadOnlyCollection<Section> sections = UpdateOrder(2, 1);

            IReadOnlyCollection<Section> sectionsToDisplay = OrderSections(sections);

            foreach (var section in sectionsToDisplay)
                Console.WriteLine(section.Name);
        }

        static IReadOnlyCollection<Section> OrderSections(IReadOnlyCollection<Section> sections)
        {
            List<Section> orderSections = new List<Section>();

            Section firstSectionToAdd = sections.Single(x => x.PreviousId == null);

            orderSections.Add(firstSectionToAdd);

            List<Section> remainingSections = sections
                .Where(x => x.PreviousId != null)
                .ToList();

            while (remainingSections.Any())
            {
                Section lastEntry = orderSections.Last();
                Section nextSection = remainingSections.Single(x => x.PreviousId == lastEntry.Id);

                orderSections.Add(nextSection);
                remainingSections.Remove(nextSection);
            }

            return orderSections.ToArray();
        }

        static IReadOnlyCollection<Section> UpdateOrder(int sectionId, int? targetId)
        {
            IReadOnlyCollection<Section> sections = SectionsProvider.GetSections();

            Section sectionToMove = sections.Single(x => x.Id == sectionId);
            Section targetSection = sections.Single(x => x.Id == targetId);
            Section nextFromTarget = sections.Single(x => x.PreviousId == sectionId);

            Section newMovedSection = new Section(sectionToMove.Id, sectionToMove.Name, targetSection.PreviousId);
            Section newTargetSection = new Section(targetSection.Id, targetSection.Name, sectionToMove.Id);
            Section newNextFromTargetSection = new Section(nextFromTarget.Id, nextFromTarget.Name, sectionToMove.PreviousId);

            List<Section> newSections = sections
                .Where(x =>
                    x.Id != sectionId &&
                    x.Id != targetId &&
                    x.PreviousId != sectionId)
                .ToList();

            newSections.AddRange(new List<Section>
            {
                newMovedSection,
                newTargetSection,
                newNextFromTargetSection
            });

            return newSections.ToArray();
        }
    }
}
