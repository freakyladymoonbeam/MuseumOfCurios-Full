namespace MuseumOfCurios.Systems
{
    public class Pronouns
    {
        // The five pronoun forms (immutable, which means they can't be changed without creating a new set of pronouns)
        public string Subject { get; init; }           // e.g. "they"
        public string Object { get; init; }            // e.g. "them"
        public string Possessive { get; init; }        // e.g. "their"
        public string PossessivePronoun { get; init; } // e.g. "theirs"
        public string Reflexive { get; init; }         // e.g. "themself"
        public bool IsCustom { get; init; }            // Indicates if this is a custom pronoun set
        public bool IsPlural { get; init; }            // Indicates if pronouns use plural verb agreement (i.e. "he walks" vs. "they walk")

        // Predefined pronoun sets
        public static readonly Pronouns HeHim = new Pronouns("he", "him", "his", "his", "himself", false, false);
        public static readonly Pronouns SheHer = new Pronouns("she", "her", "her", "hers", "herself", false, false);
        public static readonly Pronouns TheyThem = new Pronouns("they", "them", "their", "theirs", "themself", false, true);

        // Private constructor
        private Pronouns(string subject, string obj, string possessive, string possessivePronoun, string reflexive, bool isCustom, bool isPlural)
        {
            Subject = subject;
            Object = obj;
            Possessive = possessive;
            PossessivePronoun = possessivePronoun;
            Reflexive = reflexive;
            IsCustom = isCustom;
            IsPlural = isPlural;
        }

        // Factory method for custom pronouns
        public static Pronouns CreateCustom(string subject, string obj, string possessive, string possessivePronoun, string reflexive, bool isPlural = false)
        {
            Validate(subject, obj, possessive, possessivePronoun, reflexive);
            return new Pronouns(subject, obj, possessive, possessivePronoun, reflexive, true, isPlural);
        }

        // Returns a new instance (i.e. a new set) instead of mutating an existing instance (set)
        public Pronouns With(string subject, string obj, string possessive, string possessivePronoun, string reflexive, bool? isPlural = null)
        {
            Validate(subject, obj, possessive, possessivePronoun, reflexive);
            return new Pronouns(subject, obj, possessive, possessivePronoun, reflexive, true, isPlural ?? this.IsPlural);
        }

        // Validation helper to ensure all fields are filled
        private static void Validate(string subject, string obj, string possessive, string possessivePronoun, string reflexive)
        {
            if (string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(obj) || string.IsNullOrWhiteSpace(possessive) || string.IsNullOrWhiteSpace(possessivePronoun) || string.IsNullOrWhiteSpace(reflexive))
            {
                throw new ArgumentException("All pronoun fields must be filled.");
            }
        }

        // Debug (displays a set of pronouns to make sure everything's correct)
        public override string ToString()
        {
            return $"Subject: {Subject}\nObject: {Object}\nPossessive: {Possessive}\nPossessivePronoun: {PossessivePronoun}\nReflexive: {Reflexive}\nIsCustom: {IsCustom}\nIsPlural: {IsPlural}";
        }

        // =====================
        //    Grammar Helpers
        // =====================

        // This makes the first letter of a word capitalized.
        private static string Capitalize(string value)
        {
            return string.IsNullOrEmpty(value) ? value : char.ToUpper(value[0]) + value.Substring(1);
        }

        // Pronoun capitalization helpers (e.g. "He's ready." vs. "he's ready.")
        public string SubjectCapitalized => Capitalize(Subject);
        public string ObjectCapitalized => Capitalize(Object);
        public string PossessiveCapitalized => Capitalize(Possessive);
        public string PossessivePronounCapitalized => Capitalize(PossessivePronoun);
        public string ReflexiveCapitalized => Capitalize(Reflexive);

        // Core verb helpers (e.g. "He is" vs. "They are")
        public string Be => IsPlural ? "are" : "is";
        public string BePast => IsPlural ? "were" : "was";
        public string Have => IsPlural ? "have" : "has";
        public string Do => IsPlural ? "do" : "does";

        // Verb capitalization helpers (e.g. "Was he running?" v.s. "Were they running?")
        public string BeCapitalized => Capitalize(Be);
        public string BePastCapitalized => Capitalize(BePast);
        public string HaveCapitalized => Capitalize(Have);
        public string DoCapitalized => Capitalize(Do);

        // Verb suffix helper
        // NOTE:
        // {s} is the default (walk -> walks)
        // {es} must be used manually for verbs like "watch(es)", "fix(es)", "pass(es)", etc.
        // If I need to add more later (like for "try" -> "tries"), I can add more helpers as needed.
        // I don't even know how often I'll need this, since it's basically for narration specifically referring to the protag, instead of regular dialogue.
        public string S => IsPlural ? "" : "s";
        public string Es => IsPlural ? "" : "es";

        // Verb contraction helper (e.g. "he's" vs. "they're")
        public string BeContractionSuffix => IsPlural ? "'re" : "'s";

        // Formatting helper for dialogue/templates
        public string Format(string template)
        {
            // Pronoun helpers
            return template
                // Pronouns (lowercase)
                .Replace("{sub}", Subject)
                .Replace("{obj}", Object)
                .Replace("{pos}", Possessive)
                .Replace("{posp}", PossessivePronoun)
                .Replace("{ref}", Reflexive)

                // Pronouns (capitalized)
                .Replace("{Sub}", SubjectCapitalized)
                .Replace("{Obj}", ObjectCapitalized)
                .Replace("{Pos}", PossessiveCapitalized)
                .Replace("{Posp}", PossessivePronounCapitalized)
                .Replace("{Ref}", ReflexiveCapitalized)

                // Verbs (lowercase)
                .Replace("{be}", Be)
                .Replace("{bePast}", BePast)
                .Replace("{beShort}", BeContractionSuffix)
                .Replace("{have}", Have)
                .Replace("{do}", Do)

                // Verbs (capitalized)
                .Replace("{Be}", BeCapitalized)
                .Replace("{BePast}", BePastCapitalized)
                .Replace("{Have}", HaveCapitalized)
                .Replace("{Do}", DoCapitalized)

                // Verb suffix
                .Replace("{s}", S)
                .Replace("{es}", Es);

            // God I hope this shit works.
        }
    }
}