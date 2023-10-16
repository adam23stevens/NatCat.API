using System;
namespace NatCat.DAL.Seed
{
    internal static class SeedData
    {
        internal static IEnumerable<string> AdminEmails => new string[] { "adam23stevens@gmail.com" };
        internal static IEnumerable<string> UserEmails => new string[] { "julia23stevens@gmail.com", "user_1@natcat.com", "user_2@natcat.com" };
        internal static IEnumerable<string> DemoUserEmails => new string[] { "demo_user_1@natcat.com", "demo_user_2@natcat.com" };
        internal static IEnumerable<string> GenreNames => new string[] { "Horror", "Adventure", "Fantasy" };
        internal static IEnumerable<string> HorrorKeywords => new string[] { "Skull", "Skeleton", "Spider", "Ghost", "Mansion", "Basement", "Zombie", "Boo", "Cobweb", "Knife", "Stab", "Run", "Creak", "Scream", "Cry", "Shriek", "Freeze", "Lock", "Tighten", "Scare", "Bruise" };
        internal static IEnumerable<string> AdventureKeywords => new string[] { "Island", "Journey", "Backpack", "Compass", "Water", "Sands", "Skies", "Trek", "Travel", "Fight", "Conquer", "Build", "Shout", "Bones", "Push", "Jump", "Reach", "Swim", "Warmth", "Bear" };
        internal static IEnumerable<string> FantasyKeywords => new string[] { "Magic", "Dust", "Fly", "Explode", "Sunset", "Dwarf", "Book", "Spell", "Axe", "Creep", "Angel", "Flight", "Land", "Pixie", "Jump", "Tree", "Root", "Goblin", "Giant", "Ghost", "Climb", "Wand" };
        internal static IEnumerable<string> StoryTypes => new string[] { "Novel", "Short Story", "Script", "Limerick", "Poem A", "Poem B" };
        internal static IEnumerable<string> StoryTypeRuleDescriptions => new string[] {
            "A longer story up to 50 pages",
            "A shorter story up to 20 pages",
            "Desribe who says what in a short play",
            "AABBA rhyming pattern",
            "AABB rhyming",
            "ABAB rhyming"
        };
        internal static IEnumerable<string> StoryTitles => new string[] { "Silent Scream", "To the Mountains", "The Magic Diary" };
        internal static IEnumerable<string> HorrorStoryPartTexts => new string[]
        {
            "One night, there was something scary in the attic",
            "Bernard decided to take a look and he found",
            "some smelly socks. The mansion creaked and he turned and spotted"
        };
        internal static IEnumerable<string> AdventureStoryPartTexts => new string[]
        {
            "There was some pirate who had hiccups and",
            "she hiccupped so badly, it caused an explosion",
            "which killed several thousand islanders who "
        };
        internal static IEnumerable<string> FantasyStoryPartTexts => new string[]
        {
            "The dwarf's wive went out for tea",
            "She had to get some more magic dust for the tea",
            "but then she farted and this meant"
        };

        internal static string BookClubName => "Smarty Pants Stories";

        internal static IEnumerable<string> RhymingPatternNames => new string[] { "No Rhyming", "ABAB Rhyming", "AABB Rhyming", "Limeric" };
        internal static IEnumerable<string> RhymingPatterns => new string[] { string.Empty, "ABAB", "AABB", "AABBA" };
        internal static IEnumerable<string> RhymingPatternDescriptions => new string[]
        {
            "Standard story that has no rhyming.",
            "The first line and third line must rhyme. The second and fourth line must rhyme. And so on.",
            "The first and second lines must rhyme. The third and fourth lines must rhyme. And so on.",
            "Five lines only. The first, second and fifth lines must rhyme. The third and fourth lines must rhyme."
        };
    }
}

